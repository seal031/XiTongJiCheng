using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinJiangShouBaoSanRun
{
    /// <summary>
    /// chrome与js交互管理对象，用于将c#方法暴露给js调用
    /// </summary>
    public class JsCallbackManager
    {
        private static ChromiumWebBrowser chromeWb;
        public static void setBrowser(ChromiumWebBrowser wb)
        {
            chromeWb = wb;
        }

        public void test(string a)
        {
            
        }
        /// <summary>
        /// 登录回调
        /// </summary>
        /// <param name="json"></param>
        public void onLogin(string json)
        {
            json = cleanJson(json);
            LoginCallbackObject loginObj = LoginCallbackObject.fromJson(json);
            if (loginObj.error == "success")
            {
                FileWorker.LogHelper.WriteLog("登录成功");
            }
            else
            {
                FileWorker.LogHelper.WriteLog("登录失败" + loginObj.error);
            }
        }

        /// <summary>
        /// 设备列表回调
        /// </summary>
        /// <param name="json"></param>
        public void onDeviceList(string json)
        {
            json = cleanJson(json);
            DeviceListCallbackObject deviceListOjb = DeviceListCallbackObject.fromJson(json);
            if (deviceListOjb.error == "success")
            {
                foreach (var device in deviceListOjb.param.list)
                {
                    //只拿报警设备，不拿联动设备
                    if (device.deviceType == "alarm")
                    {
                        DeviceEntity deviceEntity = MessageTransfor.getDevice(device);
                        KafkaWorker.sendDeviceMessage(deviceEntity.toJson());
                    }
                }
            }
            else
            {
                FileWorker.LogHelper.WriteLog("获取设备列表失败"+deviceListOjb.error);
            }
        }

        /// <summary>
        /// 设备通知回调
        /// </summary>
        /// <param name="json"></param>
        public void onNotify(string json)
        {
            json = cleanJson(json);
            NotifyCallbackObject notifyObj = NotifyCallbackObject.fromJson(json);
            AlarmEntity alarm = MessageTransfor.getAlarm(notifyObj);
            KafkaWorker.sendAlarmMessage(alarm.toJson());
        }

        /// <summary>
        /// 客户端与服务端断开回调
        /// </summary>
        public void onAlarmServerClosed()
        {
            FileWorker.LogHelper.WriteLog("客户端与服务端的连接已断开!!!");
            //页面中js会执行一次重连
            //if(chromeWb!=null)
            //{
            //    FileWorker.LogHelper.WriteLog("正在尝试重新登陆服务端");
            //    string jsStr = "$(\"#loginBtn\").click();";
            //    chromeWb.GetMainFrame().ExecuteJavaScriptAsync(jsStr);
            //}
        }

        /// <summary>
        /// SDK异常回调
        /// </summary>
        /// <param name="json"></param>
        public void onDHAlarmWebError(string json)
        {
            FileWorker.LogHelper.WriteLog($"SDK出现异常，{json}");
        }
        private string cleanJson(string json)
        {
            return json.Replace("params", "param");
        }
    }
}
public class LoginCallbackObject
{
    public string method { get; set; }
    public string error { get; set; }
    public Params param { get; set; }

    public class Params
    {
        public string loginHandle { get; set; }
    }
    public static LoginCallbackObject fromJson(string json)
    {
        return JsonConvert.DeserializeObject<LoginCallbackObject>(json);
    }
}
public class DeviceListCallbackObject
{
    public string method { get; set; }
    public string error { get; set; }
    public Params param { get; set; }

    public class Params
    {
        public Params()
        {
            list = new List<Device>();
        }
        public string loginHandle { get; set; }
        public List<Device> list { get; set; }
        public class Device
        {
            public string deviceId { get; set; }
            public string deviceName { get; set; }
            public string deviceType { get; set; }
            public string action { get; set; }
        }
    }
    public static DeviceListCallbackObject fromJson(string json)
    {
        return JsonConvert.DeserializeObject<DeviceListCallbackObject>(json);
    }
}

public class NotifyCallbackObject
{
    public string method { get; set; }

    public Params param { get; set; }
    public class Params
    {
        public string code { get; set; }
        public string deviceId { get; set; }
        public string action { get; set; }
        public string loginHandle { get; set; }
    }
    public static NotifyCallbackObject fromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotifyCallbackObject>(json);
    }
}

public class DHAlarmWebErrorCallbackObject
{
    public string clientId { get; set; }
    public string error { get; set; }
    public Msg msg { get; set; }
    public class Msg
    {
        public string method { get; set; }
        public string error { get; set; }
    }
    public static DHAlarmWebErrorCallbackObject fromJson(string json)
    {
        return JsonConvert.DeserializeObject<DHAlarmWebErrorCallbackObject>(json);
    }
}