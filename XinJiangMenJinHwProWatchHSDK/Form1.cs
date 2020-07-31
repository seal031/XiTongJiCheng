using HoneywellAccess.HSDK.oBIX;
using HoneywellAccess.SmartPlus.IntegrationServer.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinJiangMenJinHwProWatchHSDK
{
    public partial class Form1 : Form
    {
        private Obj currentPropertyObj;
        private static int index = 0;
        private Hashtable htMethods = new Hashtable();
        private Obj currentInvokeObj;
        private Obj currentSearchObj;
        private List<string> checkedNodes = new List<string>();

        public System.Threading.Timer tmrPollingManager;
        private static SynchronizationContext uiThread;
        private ISmartPlusLogger logger = new SmartPlusLogger();
        private ISmartPlusLoggerManager loggerMgr = SmartPlusLoggerManager.GetInstance;
        private HttpManager httpManager;
        private MessageQueue traceQueue;
        private System.Messaging.Message traceMessage = new System.Messaging.Message();
        public System.Threading.Timer tmrInvokeActions;
        public static object invokeInProgress = new object();
        private static bool stopActions = false;
        public List<string> objectsList = new List<string>();
        public Dictionary<string, string> actionsList = new Dictionary<string, string>();
        public Form1()
        {                                                                                                                                                                                                                                               
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }
    }
}
