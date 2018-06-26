using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Net.NetworkInformation;
using System.Net;
using System.Text.RegularExpressions;

namespace DasBoot
{



    public partial class Form1 : Form
    {


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.V))
            {
                HandlePaste();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void HandlePaste()
        {
            

            string strText = Clipboard.GetText();


            /*
             * BLAH
            //make sure clipboard text is a list of computers... none should contain a space
            //Regex rx = new Regex(@"^.*. ..*$", RegexOptions.Multiline); //anything char space char anything (don't care about before/after...trim later)
            string pattern = @"^.*[a-z0-9][ ][a-z0-9].*$"; //anything char space char anything (don't care about before/after...trim later)

            MatchCollection mc = Regex.Matches(strText,pattern,RegexOptions.Multiline|RegexOptions.IgnoreCase);
            Console.WriteLine("Matches:  {0}", mc.Count);
            foreach (Match m in mc)
            {
                Console.WriteLine("Match:  {0}",m.Value);
            }
            return;
            */
            strText = strText.Replace(",", "\r\n");

            string separator = "\r\n";
            
            string[] computers = strText.Split(separator.ToCharArray());

            List<string> listComputers = computers.ToList();
            foreach (string computer in listComputers.Distinct())
            {
                string tmp = computer.Replace(" ", "");
                tmp = tmp.Replace("\r", "").Replace("\n", "").Replace(",", "");
                if (!string.IsNullOrEmpty(tmp))
                {
                    Console.WriteLine(tmp);
                    AddToDataGridView(tmp);
                }
            }
        }

        private bool boolPinging = false;
        private bool boolQuerying = false;
        private bool boolRebooting = false;

        private List<string> listMachinesToPing = new List<string>();
        
        public Form1()
        {
            InitializeComponent();
        }
        

        private void AddToDataGridView(string computer)
        {
            bool boolFound = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataGridViewCell cell = row.Cells["ColumnComputer"];
                    if (cell.Value.ToString().ToLower() == computer.ToLower())
                    {
                        boolFound = true;
                        break;
                    }
                }
            }

            if (! boolFound)
            {
                int idx = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[idx];
                row.Cells["ColumnComputer"].Value = computer;

            }


        }

        
        private void Reboot(string computer)
        {
            //Clear any errors
            UpdateCellText(computer, "ColumnErrors","");

            //Clear last reboot text
            UpdateCellText(computer, "ColumnLastReboot", "");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "cmd";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = @"/c shutdown -m \\" + computer + " -r -t 0 -f";
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                // Prepend line numbers to each line of the output.
                if (!String.IsNullOrEmpty(e.Data))
                {
                    Console.WriteLine(computer + " - OutputDataReceived:  {0}",e.Data);
                }
            });

            process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                // Prepend line numbers to each line of the output.
                if (!String.IsNullOrEmpty(e.Data))
                {
                    Console.WriteLine(computer + " - ErrorDataReceived:  {0}",e.Data);
                    UpdateCellText(computer, "ColumnErrors",e.Data);
                }
            });

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        }
        
        
        private void UpdateCellText(string computer, string columnName, string data)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (row.Cells["ColumnComputer"].Value.ToString() == computer)
                    {
                        row.Cells[columnName].Value = data;
                    }
                }
            }
        }         
             

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            buttonQueryAll.Enabled = !boolQuerying;
            buttonQuerySelected.Enabled = !boolQuerying;
            buttonRebootAll.Enabled = !boolRebooting;
            buttonRebootSelected.Enabled = !boolRebooting;

            if (boolRebooting || boolQuerying || boolPinging)
            {
                return;
            }
            

            CheckStatus();
        }

        private async void CheckStatus()
        {
            Console.WriteLine("Checking Status...");


            boolPinging = true;

            listMachinesToPing.Clear();
            

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {

                   
                    string computer = row.Cells["ColumnComputer"].Value.ToString();

                    listMachinesToPing.Add(computer);
                    

                    

                }
            }

            Console.WriteLine("Count:  {0}", listMachinesToPing.Count);


            if (listMachinesToPing.Count > 0)  
            {
                Console.WriteLine("await PingAsync()");

                PingResponse[] arrResponses = null;
                TimeSpan ts = new TimeSpan(0, 0, 1);
                
                arrResponses = await NetworkInformationExtensions.PingAsync(listMachinesToPing.ToArray(),ts,false);
                

                foreach (PingResponse response in arrResponses)
                {
                    Console.WriteLine("host:  {0}, success?:  {1}",response.HostNameOrAddress, response.IsSuccess);
                    if (response.IsSuccess)
                    {
                        UpdateCellText(response.HostNameOrAddress.ToString(), "ColumnStatus", "Online");
                    }
                    else
                    {
                        string status = "Unavailable";
                        //resolve?
                        IPAddress ip = null;
                        var isIpAddress = IPAddress.TryParse(response.HostNameOrAddress, out ip);
                        if (!isIpAddress)
                        {
                            var resolve = DnsResolver.ResolveHostName(response.HostNameOrAddress, TimeSpan.FromMilliseconds(20));
                            if (resolve.IPAddress == null)
                            {
                                status = "Host not found";
                            }
                        }
                            

                        UpdateCellText(response.HostNameOrAddress, "ColumnStatus", status);

                    }
                }
                

            }


            boolPinging = false;
        }
        
        
        
         
        
        //private async Task<List<PingResult>> PingAsync()
        //{
        //    Console.WriteLine("PingAsync()");
        //    //var tasks = listComputers.Select(host => new Ping().SendPingAsync(host, 1000));

            
        //    var tasks = listComputers.Select(address =>
        //    {
        //        return new Ping().SendTaskAsync(address);

        //    });
            
            

        //    var results = await Task.WhenAll(tasks);
            

        //    boolChecking = false;

        //    return results.ToList();

        //}

        //private string DnsTest(string address)
        //{
        //    try
        //    {
        //        IPAddress[] arrIP = Dns.GetHostEntry(address).AddressList;
        //        return arrIP.Length == 0 ? "Invalid hostname" : "Unknown";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
                        
        //}

        private void buttonRebootSelected_Click(object sender, EventArgs e)
        {

            List<string> listMachines = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    string computer = row.Cells["ColumnComputer"].Value.ToString();
                    if (!string.IsNullOrEmpty(computer))
                    {
                        listMachines.Add(computer);
                    }

                }
            }

            if (listMachines.Count == 0)
            {
                return;
            }

            RebootComputers(listMachines);


        }



        private void buttonRebootAll_Click(object sender, EventArgs e)
        {


            List<string> listMachines = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string computer = row.Cells["ColumnComputer"].Value.ToString();
                    if (!string.IsNullOrEmpty(computer))
                    {
                        listMachines.Add(computer);
                    }

                }
            }

            if (listMachines.Count == 0)
            {
                return;
            }

            RebootComputers(listMachines);


        }

        private async Task RebootComputers(List<string> listMachines)
        {
            string message = String.Format("Are you sure you want to reboot {0}?", listMachines.Count > 1 ? listMachines.Count.ToString() + " computers" : listMachines[0]);
            DialogResult result = MessageBox.Show(message, "Reboot Computers", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }

            boolRebooting = true;

            var tasks = listMachines.Select(async host =>
                await Task.Run(() => Reboot(host))
            );


            await Task.WhenAll(tasks);
            boolRebooting = false;
        }





        private void GetLastReboot(string addressOrHostname)
        {
            string strLastBootupTime = "";

            ManagementObjectCollection colOpSys;
            ConnectionOptions oConn = new ConnectionOptions();

            try
            {
                ManagementPath mp = new ManagementPath($@"\\{addressOrHostname}\root\cimv2");
                ManagementScope oMs = new ManagementScope(mp);

                SelectQuery oQuery = new SelectQuery("select * from Win32_OperatingSystem");
                ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);

                colOpSys = oSearcher.Get();

                
                foreach (ManagementObject objOS in colOpSys)
                {
                    var LastBoot = objOS.GetPropertyValue("LastBootUpTime").ToString();
                    DateTime dtmLastBootupTime = ManagementDateTimeConverter.ToDateTime(LastBoot);
                    Console.WriteLine("computer:  {0}, last boot:  {1}", addressOrHostname,dtmLastBootupTime.ToLongTimeString());

                    //TimeSpan tsSystemUptime = DateTime.Now.Subtract(dtmLastBootupTime);
                    //(tsSystemUptime.TotalHours / 24).ToString("#.# days");

                    UpdateCellText(addressOrHostname, "ColumnLastReboot", dtmLastBootupTime.ToString("u"));
                    strLastBootupTime = dtmLastBootupTime.ToString("u");

                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                UpdateCellText(addressOrHostname, "ColumnLastReboot", "");
                UpdateCellText(addressOrHostname, "ColumnErrors", ex.Message);
            }

            
        }

        private void buttonQueryAll_Click(object sender, EventArgs e)
        {



            List<string> listMachines = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string computer = row.Cells["ColumnComputer"].Value.ToString();
                    if (!string.IsNullOrEmpty(computer))
                    {
                        listMachines.Add(computer);
                    }

                }
            }

            if (listMachines.Count == 0)
            {
                return;
            }

            
            QueryComputers(listMachines);
        }



        private async Task QueryComputers(List<string> listMachines)
        {
            boolQuerying = true;


            var tasks = listMachines.Select(async host =>
                await Task.Run(() => GetLastReboot(host))
            );



            await Task.WhenAll(tasks);
            boolQuerying = false;

        }

        private void buttonQuerySelected_Click(object sender, EventArgs e)
        {

            List<string> listMachines = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    string computer = row.Cells["ColumnComputer"].Value.ToString();
                    if (!string.IsNullOrEmpty(computer))
                    {
                        listMachines.Add(computer);
                    }

                }
            }

            if (listMachines.Count == 0)
            {
                return;
            }

            QueryComputers(listMachines);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            timer1.Stop();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            timer1.Start();
        }
    }
}
