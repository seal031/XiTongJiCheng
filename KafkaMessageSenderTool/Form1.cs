using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace KafkaMessageSenderTool
{
    public partial class Form1 : Form
    {
        Queue<string> messageQueue = new Queue<string>();
        List<DataTable> tableList = new List<DataTable>();

        public Form1()
        {
            InitializeComponent();
            setRtb();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            KafkaWorker.sendAlarmMessage(rtb_message.Text.Trim(),false,true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KafkaWorker.brokerList = txt_url.Text.Trim();
            KafkaWorker.messageTopicName = txt_topic.Text.Trim();
            if (KafkaWorker.init())
            {
                tabControl1.Enabled = true;
            }
        }

        private void btn_beforeLoad_Click(object sender, EventArgs e)
        {
            if (File.Exists(txt_filePath.Text.Trim()) == false)
            {
                MessageBox.Show("文件不存在");
            }
            else
            {
                using (var stream = File.Open(txt_filePath.Text.Trim(), FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();
                        List<string> sheetNameList = new List<string>();
                        tableList = new List<DataTable>();
                        foreach (DataTable table in result.Tables)
                        {
                            sheetNameList.Add(table.TableName);
                            tableList.Add(table);
                        }
                        //cbb_sheet.SelectedIndexChanged -= cbb_sheet_SelectedIndexChanged;
                        cbb_sheet.DataSource = sheetNameList;
                        cbb_sheet.SelectedText = "";
                        //cbb_sheet.SelectedIndexChanged += cbb_sheet_SelectedIndexChanged;
                    }
                }
            }
        }

        int sheetIndex = 0;
        private void cbb_sheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            sheetIndex = cbb_sheet.SelectedIndex;
            if (tableList.Count > sheetIndex)
            {
                lbl_sheetInfo.Text = "共有数据" + tableList[sheetIndex].Rows.Count + "条";
            }
            else
            {
                MessageBox.Show("还没有预加载到任何sheet");
            }
        }

        Timer timer;
        private void btn_start_Click(object sender, EventArgs e)
        {
            DataTable dt = tableList[sheetIndex];
            messageQueue = new Queue<string>();
            foreach (DataRow dr in dt.Rows)
            {
                messageQueue.Enqueue(dr[0].ToString());
            }
            int interval = 0;
            bool checkInterval = int.TryParse(txt_interval.Text.Trim(), out interval);
            if (checkInterval == false)
            {
                MessageBox.Show("每条发送间隔设置不正确");
            }
            else
            {
                timer = new Timer();
                timer.Interval = interval;
                timer.Tick += Timer_Tick;
                timer.Start();
                btn_start.Enabled = false;
                btn_stop.Enabled = true;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (messageQueue.Count > 0)
            {
                string message = messageQueue.Dequeue();
                KafkaWorker.sendAlarmMessage(message, ckb_showMessage.Checked);
            }
            else
            {
                timer.Stop();
                MessageBox.Show("已经没有待发的消息了");
                btn_start.Enabled = true;
                btn_stop.Enabled = false;
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Dispose();
            timer = null;
            btn_start.Enabled = true;
            btn_stop.Enabled = false;
        }

        private static RichTextBox rtb;
        private void setRtb()
        {
            rtb = this.rtb_log;
        }
        private delegate void delInfoList(string text);
        public static void SetrichTextBox(string value)
        {
            bool invokeRequired = rtb.InvokeRequired;
            if (invokeRequired)
            {
                Form1.delInfoList method = new Form1.delInfoList(SetrichTextBox);
                rtb.Invoke(method, new object[]
                {
                    value + "\n"
                });
            }
            else
            {
                //bool flag = this.rtb_log.Lines.Length > 100;
                //if (flag)
                //{
                //    this.rtb_log.Clear();
                //}
                rtb.Focus();
                rtb.Select(rtb.TextLength, 0);
                rtb.ScrollToCaret();
                rtb.AppendText(value + "\n");
            }
        }

        private void btn_clearLog_Click(object sender, EventArgs e)
        {
            rtb_log.Text = string.Empty;
        }

        private void btn_showIBM_Click(object sender, EventArgs e)
        {
            Form2 f2 = new KafkaMessageSenderTool.Form2();
            f2.ShowDialog(this);
        }
    }
    
}
