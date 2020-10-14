using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShaoGuanMenJin
{
    public partial class Form1 : Form
    {
        public static uint m_ServerHandle, m_GroupHandle;

        private DATACHANGEPROC m_dataChange;
        private SHUTDOWNPROC m_shutDown;

        private string host, classId;
        private uint version = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (loadLocalConfig())
            {
                Connect();
            }
            else
            {
                MessageBox.Show("本地配置文件参数不正确");
            }
        }

        private bool loadLocalConfig()
        {
            host = ConfigWorker.GetConfigValue("host");
            classId = ConfigWorker.GetConfigValue("classId");
            try
            {
                version = uint.Parse(ConfigWorker.GetConfigValue("version"));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Disconnect();
        }

        private void Connect()
        {
            try
            {
                m_ServerHandle = DACLTSDK.ASDAC_Connect(host, classId, version);
                if (m_ServerHandle > 0)
                {
                    DACLTSDK.ASDAC_SetShutdownProc(m_ServerHandle, m_shutDown);
                    m_GroupHandle = DACLTSDK.ASDAC_AddGroup(m_ServerHandle, "CSGroup", true, 1000, -480, 0, 0);
                    if (m_GroupHandle > 0)
                    {
                        DACLTSDK.ASDAC_SetDataChangeProc(m_ServerHandle, m_dataChange);
                        DACLTSDK.ASDAC_RefreshGroup(m_ServerHandle, m_GroupHandle, 2);//2=OPC_DEVICE
                                                                                      //this.Text = "OPC DA Client by C#(Agilewill co.ltd)[" + DAC_CSDEMO.Properties.Settings.Default.ProgID + "]";
                    }
                    else
                    {
                        FileWorker.LogHelper.WriteLog("增加组失败(Add Group Failure)!");
                    }
                }
                else
                {
                    FileWorker.LogHelper.WriteLog("连接到服务器失败(Connect to Server Failure)!");
                }
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("连接服务器时出现异常："+ex.Message);
            }
        }
        private void Disconnect()
        {
            try
            {
                if (m_ServerHandle > 0)
                {
                    DACLTSDK.ASDAC_Disconnect(m_ServerHandle);
                    m_ServerHandle = 0;
                    m_GroupHandle = 0;
                }
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("断开服务器连接时出现异常：" + ex.Message);
            }
        }
    }
}
