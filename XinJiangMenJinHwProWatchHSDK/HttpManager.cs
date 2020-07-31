using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoneywellAccess.HSDK.oBIX;
using HoneywellAccess.HSDK.oBIX.IO;
using HoneywellAccess.SmartPlus.IntegrationServer.Helpers;
using HoneywellAccess.SmartPlus.IntegrationServer.Logging;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

public class HttpManager
{
    public LogHandler logMessage;
    //public StatusBarHandler ShowStatusBar;
    public HttpManager(LogHandler logMessage)//, StatusBarHandler ShowStatusBar)
    {
        this.logMessage = logMessage;
        //this.ShowStatusBar = ShowStatusBar;
    }
    public Obj SendRequest(string strURL, string strReqData, MethodType type)
    {
        if (HSDKConfiguration.SoapRequest)
        {
            strReqData = this.GetSoapMessage(strURL, strReqData, type);
            type = MethodType.POST;
            strURL = HSDKConfiguration.LobbyUrl + "/soap";
        }
        this.logMessage("Request XML from TestClient to HSDK Server (" + type.ToString() + ") to ...", SmartPlus_LOG_TYPE.TRACE);
        this.logMessage(strURL, SmartPlus_LOG_TYPE.TRACE);
        this.logMessage(strReqData, SmartPlus_LOG_TYPE.REQUEST);
        string text;
        if (HSDKConfiguration.CertFilePath == "")
        {
            text = HTTPStub.PostRequestToURL(strURL, strReqData, type.ToString(), HSDKConfiguration.Username, HSDKConfiguration.Password);
        }
        else
        {
            text = HTTPStub.PostRequestToURL(strURL, strReqData, type.ToString(), HSDKConfiguration.Username, HSDKConfiguration.Password, X509Certificate.CreateFromCertFile(HSDKConfiguration.CertFilePath));
        }
        if (text == "")
        {
            throw new Exception("HSDK Server responded with empty XML.");
        }
        if (HSDKConfiguration.SoapRequest)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            xmlNamespaceManager.AddNamespace("ha", "http://schemas.xmlsoap.org/soap/envelope/");
            xmlDocument.LoadXml(text);
            text = xmlDocument.SelectSingleNode("/ha:Envelope/ha:Body/*", xmlNamespaceManager).OuterXml;
        }
        this.logMessage("Response XML from HSDK Server to TestClient...", SmartPlus_LOG_TYPE.TRACE);
        this.logMessage(text, SmartPlus_LOG_TYPE.RESPONSE);
        Obj obj = oBIXDecoder.fromString(text);
        if (obj.isErr())
        {
            if (!obj.Display.Equals("No License to access WatchService") && !obj.Display.Equals("No License to access AlarmService"))
            {
                throw new Exception(obj.Display);
            }
            //this.ShowStatusBar(obj.Display, MessageType.Error);
        }
        return obj;
    }
    private string GetSoapMessage(string strURL, string strReqData, MethodType type)
    {
        string arg = "";
        switch (type)
        {
            case MethodType.GET:
                arg = "read";
                break;
            case MethodType.POST:
                arg = "invoke";
                break;
            case MethodType.PUT:
                arg = "write";
                break;
        }
        return string.Format("<env:Envelope xmlns:env='http://schemas.xmlsoap.org/soap/envelope/'>\r\n                                <env:Body>\r\n                                <{0} xmlns='http://obix.org/ns/wsdl/1.0'\r\n                                href='{1}' >{2}</{0}>\r\n                                </env:Body>\r\n                                </env:Envelope>\r\n                            ", arg, strURL, strReqData);
    }
    public string SendRequest(string strURL, string strReqData, string methodType)
    {
        if (HSDKConfiguration.CertFilePath == "")
        {
            return HTTPStub.PostRequestToURL(strURL, strReqData, methodType, HSDKConfiguration.Username, HSDKConfiguration.Password);
        }
        return HTTPStub.PostRequestToURL(strURL, strReqData, methodType, HSDKConfiguration.Username, HSDKConfiguration.Password, X509Certificate.CreateFromCertFile(HSDKConfiguration.CertFilePath));
    }
}


public delegate void LogHandler(string message, SmartPlus_LOG_TYPE logType);
public enum MethodType
{
    GET,
    POST,
    PUT
}
public enum MessageType
{
    Information,
    Error,
    Success,
    Fail
}
public enum WatchType
{
    Object,
    Alarm
}
