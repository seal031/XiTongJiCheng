using IBMMQ_LIB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafkaMessageSenderTool
{
    public partial class Form2 : Form
    {
        MQ_SDK mqHelper;
        List<MQ_SDK> mqList = new List<MQ_SDK>();
        Stopwatch sw;

        public Form2()
        {
            InitializeComponent();
        }

        private MQ_SDK initSdk()
        {
            MQ_SDK sdk = new MQ_SDK();
            List<ConnectorConfig> list = new List<ConnectorConfig>();
            list.Add(new ConnectorConfig(txt_sendChannel.Text.Trim(), txt_sendQueue.Text.Trim()));
            try
            {
                sdk.initial(list);
                string result = sdk.login(txt_user.Text.Trim(), txt_pwd.Text.Trim(), txt_sendQueue.Text.Trim());
                SetrichTextBox("登录返回结果（8表示成功）：" + result);
                return sdk;
            }
            catch (Exception ex)
            {
                SetrichTextBox(ex.Message);
                return null;
            }
        }
        private void btn_login_Click(object sender, EventArgs e)
        {
            if (mqHelper == null)
            {
                mqHelper = initSdk();
            }
        }
        private void btn_sendOne_Click(object sender, EventArgs e)
        {
            if (mqHelper == null)
            {
                MessageBox.Show("SDK尚未初始化");
            }
            else if (rtb_message.Text.Trim()==string.Empty)
            {
                MessageBox.Show("请输入要发送的消息内容");
            }
            else
            {
                try
                {
                    string sendResult = mqHelper.sendMSG(rtb_message.Text.Trim(), txt_sendChannel.Text.Trim(), txt_sendQueue.Text.Trim());
                    SetrichTextBox("发送结果（13表示成功）：" + sendResult);
                }
                catch (Exception ex)
                {
                    SetrichTextBox(ex.Message);
                }
            }
        }

        private void btn_sendMany_Click(object sender, EventArgs e)
        {
            int msgCount = 0;
            if (int.TryParse(txt_sendCount.Text.Trim(), out msgCount) == false)
            {
                MessageBox.Show("发送条数必须是数字");
            }
            else if (msgCount <= 0)
            {
                MessageBox.Show("发送条数必须大于0");
            }
            else
            {
                sw = new Stopwatch();
                sw.Start();
                SetrichTextBox("单线程发送开始at " + DateTime.Now.ToString("HH:mm:ss fff"));
                for (int i = 0; i < msgCount; i++)
                {
                    string sendResult = mqHelper.sendMSG(rtb_message.Text.Trim(), txt_sendChannel.Text.Trim(), txt_sendQueue.Text.Trim());
                    if (ckb_showResult.Checked)
                    {
                        SetrichTextBox(string.Format("发送结果（13表示成功）第{0}条：{1}", i.ToString(), sendResult));
                    }
                }
                sw.Stop();
                SetrichTextBox(string.Format("****** 单线程发送结束at {1}，耗时{0} ******", sw.ElapsedMilliseconds, DateTime.Now.ToString("HH:mm:ss fff")));
            }
        }
        private void btn_sendManyMulThread_Click(object sender, EventArgs e)
        {
            int threadCount = 0;
            int msgCount = 0;
            if (int.TryParse(txt_sendCount.Text.Trim(), out msgCount) == false)
            {
                MessageBox.Show("发送条数必须是数字");
            }
            else if (msgCount <= 0)
            {
                MessageBox.Show("发送条数必须大于0");
            }
            else if (int.TryParse(txt_threadCount.Text.Trim(), out threadCount) == false)
            {
                MessageBox.Show("并发数必须是数字");
            }
            else if (threadCount <= 0)
            {
                MessageBox.Show("并发数必须大于0");
            }
            else
            {
                mqList.Clear();
                for (int i = 0; i < threadCount; i++)
                {
                    MQ_SDK sdk = initSdk();
                    if (sdk == null)
                    {
                        MessageBox.Show("并发SDK初始化失败");
                        return;
                    }
                    else
                    {
                        mqList.Add(sdk);
                    }
                }
                int msgCountPerThread = msgCount / threadCount;
                SetrichTextBox(string.Format("并行化准备完毕，准备发送。共{0}个线程并发，每线程发送消息{1}条", threadCount, msgCountPerThread));
                sw = new Stopwatch();
                sw.Start();
                SetrichTextBox("并行发送开始at " + DateTime.Now.ToString("HH:mm:ss fff"));
                Parallel.ForEach(mqList, mq =>
                {
                    var r = work(mq, msgCountPerThread);
                });
            }
        }
        private async Task work(MQ_SDK sdk,int msgCountPerThread)
        {
            await send(sdk, rtb_message.Text.Trim(), msgCountPerThread);
        }
        private Task send(MQ_SDK m_mqSend, string msg, int msgCountPerThread)
        {
            return Task.Run(() =>
            {
                string channel = txt_sendChannel.Text.Trim();
                string queue = txt_sendQueue.Text.Trim();
                for (int i = 0; i < msgCountPerThread; i++)
                {
                    string sendResult = m_mqSend.sendMSG(msg, channel, queue);
                    if (ckb_showResult.Checked)
                    {
                        SetrichTextBox(string.Format("ID为{0}的线程发送第{1}条消息结果（13表示成功）：{2}", Thread.CurrentThread.ManagedThreadId, i, sendResult));
                    }
                }
                sw.Stop();
                SetrichTextBox(string.Format("****** ID为{2}的线程发送结束at {0}，耗时{1} ******", sw.ElapsedMilliseconds, DateTime.Now.ToString("HH:mm:ss fff"), Thread.CurrentThread.ManagedThreadId));
            });
        }

        private void Form2_Shown(object sender, EventArgs e)
        {

        }

        private delegate void delInfoList(string text);
        public void SetrichTextBox(string value)
        {
            bool invokeRequired = rtb_log.InvokeRequired;
            if (invokeRequired)
            {
                delInfoList method = new delInfoList(SetrichTextBox);
                rtb_log.Invoke(method, new object[]
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
                rtb_log.Focus();
                rtb_log.Select(rtb_log.TextLength, 0);
                rtb_log.ScrollToCaret();
                rtb_log.AppendText(value + "\n");
            }
        }

    }
}
