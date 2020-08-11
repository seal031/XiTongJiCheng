
using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

public enum ListWatchesType
{
    Add,
    Refresh,
    Stop,
    Resume,
    Delete
}

public enum MessageType
{
    Information,
    Error,
    Success,
    Fail
}

public enum MethodType
{
    GET,
    POST,
    PUT
}

public enum WatchType
{
    Object,
    Alarm
}