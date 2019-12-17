using IMOS_SDK_DEMO.sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShiPinJiChengYUSHI
{
    public partial class Form1 : Form
    {
        //登录成功返回信息
        public LOGIN_INFO_S stLoginInfo;
        public USER_LOGIN_ID_INFO_S stUserLoginIDInfo;//用户登录信息

        private uint EX_SUCCESS = 0;
        private uint EX_FAILED = 1;

        private delegate void delInfoList(string text);

        public Form1()
        {
            InitializeComponent();
        }
        private void SetrichTextBox(string value)
        {
            bool invokeRequired = this.richTextBox1.InvokeRequired;
            if (invokeRequired)
            {
                Form1.delInfoList method = new Form1.delInfoList(this.SetrichTextBox);
                this.richTextBox1.Invoke(method, new object[]
                {
                    DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " " + value + "\n"
                });
            }
            else
            {
                bool flag = this.richTextBox1.Lines.Length > 100;
                if (flag)
                {
                    this.richTextBox1.Clear();
                }
                this.richTextBox1.Focus();
                this.richTextBox1.Select(this.richTextBox1.TextLength, 0);
                this.richTextBox1.ScrollToCaret();
                this.richTextBox1.AppendText(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " " + value + "\n");
            }
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="usrLoginName"></param>
        /// <param name="usrLoginPsw"></param>
        /// <param name="srvIpAddr"></param>
        /// <param name="cltIpAddr"></param>
        /// <returns></returns>
        public LOGIN_INFO_S LoginMethod(String usrLoginName, String usrLoginPsw, String srvIpAddr, String cltIpAddr)
        {
            try
            {
                UInt32 ulRet = 0;
                uint srvPort = 8800;

                //1.初始化
                ulRet = IMOSSDK.IMOS_Initiate("N/A", srvPort, 1, 1);
                if (0 != ulRet)
                {
                    SetrichTextBox("初始化失败!" + ulRet.ToString());
                }

                //2.加密密码
                IntPtr ptr_MD_Pwd = Marshal.AllocHGlobal(sizeof(char) * IMOSSDK.IMOS_PASSWD_ENCRYPT_LEN);
                ulRet = IMOSSDK.IMOS_Encrypt(usrLoginPsw, (UInt32)usrLoginPsw.Length, ptr_MD_Pwd);

                if (0 != ulRet)
                {
                    SetrichTextBox("加密密码失败!" + ulRet.ToString());
                    Application.Exit();
                }

                String MD_PWD = Marshal.PtrToStringAnsi(ptr_MD_Pwd);
                Marshal.FreeHGlobal(ptr_MD_Pwd);

                //3.登录方法
                IntPtr ptrLoginInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(LOGIN_INFO_S)));
                ulRet = IMOSSDK.IMOS_LoginEx(usrLoginName, MD_PWD, srvIpAddr, cltIpAddr, ptrLoginInfo);
                if (0 != ulRet)
                {
                    SetrichTextBox("IMOS_Login" + ulRet.ToString());
                    Application.Exit();
                }

                stLoginInfo = (LOGIN_INFO_S)Marshal.PtrToStructure(ptrLoginInfo, typeof(LOGIN_INFO_S));
                Marshal.FreeHGlobal(ptrLoginInfo);

                //4.保活
                return stLoginInfo;
            }
            catch (Exception ex)
            {
                SetrichTextBox("Login错误:"+ex.Message);
                return stLoginInfo;
            }
        }
        /// <summary>
        /// 注销方法
        /// </summary>
        /// <returns></returns>
        private uint LogoutMethod()
        {

            //1.注销登录
            if (null != stLoginInfo.stUserLoginIDInfo.szUserLoginCode)
            {
                IMOSSDK.IMOS_LogoutEx(ref stLoginInfo.stUserLoginIDInfo);
                //IMOSSDK.IMOS_CleanUp()


            }
            else
            {
                MessageBox.Show("你还没有登录!");
                Application.Exit();
            }

            //3.注销完毕还原登录界面初始状态
            //if (true == this.loginSuccess)
            //{
            //    if (false == this.tbusrName.Enabled)
            //    {
            //        this.tbusrName.Enabled = true;
            //        this.tbusrName.ReadOnly = false;
            //    }
            //    if (false == this.tbUsrPsw.Enabled)
            //    {
            //        this.tbUsrPsw.Enabled = true;
            //        this.tbUsrPsw.ReadOnly = false;
            //    }
            //    if (false == this.tbSrvIp.Enabled)
            //    {
            //        this.tbSrvIp.Enabled = true;
            //        this.tbSrvIp.ReadOnly = false;
            //    }
            //    this.btnLogin.Enabled = true;
            //    //如果有视频播放，将所有窗格关闭
            //    this.sdkManager.StopVideo(imosPlayer, ptzPanel1);
            //}


            return EX_SUCCESS;
        }

        /// <summary>
        /// 设置根节点
        /// </summary>
        public void SetTreeRoot(TreeView treeView)
        {
            try
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = IMOSSDK.UTF8ToUnicode(stLoginInfo.szDomainName);
                ORG_RES_QUERY_ITEM_S stOrgQueryItem = new ORG_RES_QUERY_ITEM_S();
                stOrgQueryItem.szOrgCode = stLoginInfo.szOrgCode;
                stOrgQueryItem.szResName = stLoginInfo.szDomainName;
                stOrgQueryItem.szResCode = stLoginInfo.szOrgCode;
                stOrgQueryItem.ulResType = (UInt32)IMOS_TYPE_E.IMOS_TYPE_ORG;
                treeNode.Tag = stOrgQueryItem;
                treeView.Nodes.Add(treeNode);
                treeView.ExpandAll();

            }
            catch (Exception ex)
            {
                SetrichTextBox("SetTreeRoot错误:"+ex.Message);
            }
        }

        /// <summary>
        /// 构造树
        /// </summary>
        /// <param name="treeNode"></param>
        private void BuildTree(TreeNode treeNode, TreeView treeView)
        {
            try
            {
                treeView.BeginUpdate();
                //获取域数据
                List<ORG_RES_QUERY_ITEM_S> list = GetDomain(treeNode);

                //List<ORG_RES_QUERY_ITEM_S> list = getTestDomain();

                if (null != list)
                {
                    foreach (ORG_RES_QUERY_ITEM_S org in list)
                    {
                        TreeNode domainNode = new TreeNode();
                        domainNode.Text = IMOSSDK.UTF8ToUnicode(org.szResName);
                        domainNode.Tag = org;
                        domainNode.ImageIndex = 0;
                        domainNode.SelectedImageIndex = 0;
                        treeNode.Nodes.Add(domainNode);
                    }
                }
                treeNode.ExpandAll();
                //获取摄像机数据
                List<ORG_RES_QUERY_ITEM_S> cameraList = GetCamera(treeNode);
                //List<ORG_RES_QUERY_ITEM_S> cameraList = getTestCamera();
                if (null == cameraList)
                {
                    return;
                }

                foreach (ORG_RES_QUERY_ITEM_S camera in cameraList)
                {
                    TreeNode cameraNode = new TreeNode();
                    cameraNode.Text = IMOSSDK.UTF8ToUnicode(camera.szResName);
                    cameraNode.Tag = camera;
                    treeNode.Nodes.Add(cameraNode);
                }
            }
            catch (Exception ex)
            {
                SetrichTextBox("构造树错误:"+ex.Message);
            }
            finally
            {
                treeView.EndUpdate();
            }
        }
        /// <summary>
        /// 获取指定节点下的域数据列表。
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        private List<ORG_RES_QUERY_ITEM_S> GetDomain(TreeNode treeNode)
        {
            IntPtr ptrResList = IntPtr.Zero;
            IntPtr ptrRspPage = IntPtr.Zero;
            try
            {
                UInt32 ulRet = 0;
                UInt32 ulBeginNum = 0;
                UInt32 ulTotalNum = 0;
                ORG_RES_QUERY_ITEM_S st = (ORG_RES_QUERY_ITEM_S)treeNode.Tag;

                ptrResList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ORG_RES_QUERY_ITEM_S)) * 30);
                ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));

                RSP_PAGE_INFO_S stRspPageInfo;
                List<ORG_RES_QUERY_ITEM_S> list = new List<ORG_RES_QUERY_ITEM_S>();
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();
                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 30;
                    stPageInfo.bQueryCount = 1;

                    ulRet = IMOSSDK.IMOS_QueryResourceList(ref stLoginInfo.stUserLoginIDInfo, st.szResCode, (UInt32)IMOS_TYPE_E.IMOS_TYPE_ORG, 0, ref stPageInfo, ptrRspPage, ptrResList);
                    if (0 != ulRet)
                    {
                        return null;
                    }

                    stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;

                    ORG_RES_QUERY_ITEM_S stOrgResItem;
                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(ptrResList.ToInt32() + Marshal.SizeOf(typeof(ORG_RES_QUERY_ITEM_S)) * i);
                        stOrgResItem = (ORG_RES_QUERY_ITEM_S)Marshal.PtrToStructure(ptrTemp, typeof(ORG_RES_QUERY_ITEM_S));
                        list.Add(stOrgResItem);
                    }
                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return list;

            }
            catch (Exception ex)
            {
                SetrichTextBox("获取域错误:"+ex.Message);
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(ptrResList);
                Marshal.FreeHGlobal(ptrRspPage);
            }
        }
        /// <summary>
        /// 获取指定节点下的摄像机数据列表。
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        private List<ORG_RES_QUERY_ITEM_S> GetCamera(TreeNode treeNode)
        {
            try
            {
                UInt32 ulRet = 0;
                UInt32 ulBeginNum = 0;
                UInt32 ulTotalNum = 0;
                ORG_RES_QUERY_ITEM_S st = (ORG_RES_QUERY_ITEM_S)treeNode.Tag;

                IntPtr ptrResList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ORG_RES_QUERY_ITEM_S)) * 30);
                IntPtr ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));

                RSP_PAGE_INFO_S stRspPageInfo;
                List<ORG_RES_QUERY_ITEM_S> list = new List<ORG_RES_QUERY_ITEM_S>();
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();
                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 30;
                    stPageInfo.bQueryCount = 1;
                    ulRet = IMOSSDK.IMOS_QueryResourceList(ref stUserLoginIDInfo, st.szResCode, (UInt32)IMOS_TYPE_E.IMOS_TYPE_CAMERA, 0, ref stPageInfo, ptrRspPage, ptrResList);
                    if (0 != ulRet)
                    {
                        return null;
                    }

                    stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;
                    ORG_RES_QUERY_ITEM_S stOrgResItem;

                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(ptrResList.ToInt32() + Marshal.SizeOf(typeof(ORG_RES_QUERY_ITEM_S)) * i);
                        stOrgResItem = (ORG_RES_QUERY_ITEM_S)Marshal.PtrToStructure(ptrTemp, typeof(ORG_RES_QUERY_ITEM_S));
                        list.Add(stOrgResItem);
                    }

                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return list;

            }
            catch (Exception ex)
            {
                SetrichTextBox(ex.Message);
                return null;
            }
        }
        
        /// <summary>
        /// 根据当前节点，显示该节点下的子节点。
        /// </summary>
        /// <param name="parentNode"></param>
        public void OrganizeChildrenNodes(TreeNode parentNode, TreeView treeView)
        {
            if (null == parentNode)
            {
                return;
            }
            try
            {
                ORG_RES_QUERY_ITEM_S stOrgQueryItem = (ORG_RES_QUERY_ITEM_S)parentNode.Tag;
                //ORG_RES_QUERY_ITEM_S stOrgQueryItem = (ORG_RES_QUERY_ITEM_S)parentNode.Tag;

                if (stOrgQueryItem.ulResType == (UInt32)IMOS_TYPE_E.IMOS_TYPE_ORG)
                {
                    parentNode.Nodes.Clear();
                    BuildTree(parentNode, treeView);
                }
            }
            catch (Exception ex)
            {
                SetrichTextBox("获取子节点失败，详情请查询日志信息。");
            }

        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode treeNode = treeView.SelectedNode;
            OrganizeChildrenNodes(treeNode, treeView);
            ORG_RES_QUERY_ITEM_S st = (ORG_RES_QUERY_ITEM_S)treeNode.Tag;
            videoPanel.StartLive(st.szResCode);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //login
            SetrichTextBox("开始登录");
            stLoginInfo = LoginMethod("loadmin", "loadmin", "10.130.43.4", "N/A");
            SetrichTextBox("开始获取摄像头列表");
            //set tree
            SetTreeRoot(treeView);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
