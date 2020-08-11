using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HoneywellAccess.SmartPlus.IntegrationServer.Helpers;
using HoneywellAccess.SmartPlus.IntegrationServer.Logging;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections;
using HoneywellAccess.HSDK.oBIX;
using HoneywellAccess.HSDK.oBIX.IO;
using System.Security.Cryptography.X509Certificates;

    public delegate void LogHandler(string message, SmartPlus_LOG_TYPE logType);
    public delegate void StatusBarHandler(string message, MessageType messageType);

public class HttpManager
{
    public LogHandler logMessage;
    public StatusBarHandler ShowStatusBar;

    public HttpManager(LogHandler logMessage, StatusBarHandler ShowStatusBar)
    {
        this.logMessage = logMessage;
        this.ShowStatusBar = ShowStatusBar;
    }

    public Obj SendRequest(string strURL, string strReqData, MethodType type)
    {
        if (HSDKConfiguration.SoapRequest)
        {
            strReqData = GetSoapMessage(strURL, strReqData, type);
            type = MethodType.POST;
            strURL = HSDKConfiguration.LobbyUrl + "/soap";
        }

        logMessage("Request XML from TestClient to HSDK Server (" + type.ToString() + ") to ...", SmartPlus_LOG_TYPE.TRACE);
        logMessage(strURL, SmartPlus_LOG_TYPE.TRACE);
        logMessage(strReqData, SmartPlus_LOG_TYPE.REQUEST);

        string response;
        if (HSDKConfiguration.CertFilePath == "")
            response = HTTPStub.PostRequestToURL(strURL, strReqData, type.ToString(), HSDKConfiguration.Username, HSDKConfiguration.Password);
        else
            response = HTTPStub.PostRequestToURL(strURL, strReqData, type.ToString(), HSDKConfiguration.Username, HSDKConfiguration.Password, X509Certificate.CreateFromCertFile(HSDKConfiguration.CertFilePath));

        if (response == "")
            throw new Exception("HSDK Server responded with empty XML.");

        if (HSDKConfiguration.SoapRequest)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlNamespaceManager xmlNSMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            xmlNSMgr.AddNamespace("ha", "http://schemas.xmlsoap.org/soap/envelope/");
            xmlDoc.LoadXml(response);
            response = xmlDoc.SelectSingleNode("/ha:Envelope/ha:Body/*", xmlNSMgr).OuterXml;
        }

        logMessage("Response XML from HSDK Server to TestClient...", SmartPlus_LOG_TYPE.TRACE);
        logMessage(response, SmartPlus_LOG_TYPE.RESPONSE);

        Obj obj = oBIXDecoder.fromString(response);
        if (obj.isErr())
        {
            if (obj.Display.Equals("No License to access WatchService") ||
                obj.Display.Equals("No License to access AlarmService"))
            {
                //Let the oBIX tree discovery continue, even if no license for WatchService.
                ShowStatusBar(obj.Display, MessageType.Error);
            }
            else
                throw new Exception(obj.Display);
        }

        return obj;
    }

    private string GetSoapMessage(string strURL, string strReqData, MethodType type)
    {
        string httpMethodToSoapName = "";
        switch (type)
        {
            case MethodType.GET:
                httpMethodToSoapName = "read";
                break;
            case MethodType.POST:
                httpMethodToSoapName = "invoke";
                break;
            case MethodType.PUT:
                httpMethodToSoapName = "write";
                break;
        }

        return string.Format(@"<env:Envelope xmlns:env='http://schemas.xmlsoap.org/soap/envelope/'>
                                <env:Body>
                                <{0} xmlns='http://obix.org/ns/wsdl/1.0'
                                href='{1}' >{2}</{0}>
                                </env:Body>
                                </env:Envelope>
                            ", httpMethodToSoapName, strURL, strReqData);

    }

    public string SendRequest(string strURL, string strReqData, string methodType)
    {
        if (HSDKConfiguration.CertFilePath == "")
            return HTTPStub.PostRequestToURL(strURL, strReqData, methodType, HSDKConfiguration.Username, HSDKConfiguration.Password);
        else
            return HTTPStub.PostRequestToURL(strURL, strReqData, methodType, HSDKConfiguration.Username, HSDKConfiguration.Password, X509Certificate.CreateFromCertFile(HSDKConfiguration.CertFilePath));
    }
}
