using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

unsafe public delegate void DATACHANGEPROC(uint sHandle, uint gHandle, uint iHandle, object value, long timeStamp, ushort quality);
unsafe public delegate void SHUTDOWNPROC(uint sHandle, string reason);

public class DACLTSDK
{

    private DATACHANGEPROC m_dataChange;
    private SHUTDOWNPROC m_shutDown;

    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_ActiveCode(string userName, string passWord);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_Init();
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_Uninit();
    [DllImport("DACLTSDK.dll")]
    public static extern uint ASDAC_GetServers(string host, uint version, ref object clsIDs, ref object progIDs);
    [DllImport("DACLTSDK.dll")]
    public static extern uint ASDAC_Connect(string host, string classID, uint version);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_GetServerStatus(uint sHandle, ref long startTime, ref long currentTime, ref long updateTime,
        ref ushort state, ref uint bandWidth, ref uint groupCount, ref ushort majorVersion, ref ushort minorVersion, ref ushort buildNum,
        StringBuilder vendor, uint venderSize);

    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_SetDataChangeProc(uint sHandle, DATACHANGEPROC dataChange);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_SetShutdownProc(uint sHandle, SHUTDOWNPROC shutDown);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_Disconnect(uint sHandle);


    [DllImport("DACLTSDK.dll")]
    public static extern uint ASDAC_AddGroup(uint sHandle, string groupName, bool active, uint updateRate, int timeBias, float deadBand, uint LCID);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_RemoveGroup(uint sHandle, uint gHandle);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_SetGroupName(uint sHandle, uint gHandle, string groupName);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_RefreshGroup(uint sHandle, uint gHandle, ushort dataSource);
    //OPC_DS_CACHE=1, OPC_DS_DEVICE=2
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_GetGroupStat(uint sHandle, uint gHandle, ref uint groupRate, ref bool active, ref int timeBias, ref float deadBand, ref uint LCID);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_SetGroupStat(uint sHandle, uint gHandle, uint groupRate, bool active, int timeBias, float deadBand, uint LCID);


    [DllImport("DACLTSDK.dll")]
    public static extern uint ASDAC_AddItem(uint sHandle, uint gHandle, string tagName);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_RemoveItem(uint sHandle, uint gHandle, uint iHandle);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_ReadItem(uint sHandle, uint gHandle, uint iHandle, ref object Value, ref long timeStamp, ref ushort quality);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_ReadItemCache(uint sHandle, uint gHandle, uint iHandle, ref object Value, ref long timeStamp, ref ushort quality);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_WriteItem(uint sHandle, uint gHandle, uint iHandle, object Value, bool Async);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_ActiveItem(uint sHandle, uint gHandle, uint iHandle, bool Active);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_ValidateItem(uint sHandle, string tagName, ushort dataType, ushort accessRights);

    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_GetNameSpace(uint sHandle, ref ushort nameSpace);
    //OPC_NS_HIERARCHIAL=1,OPC_NS_FLAT= 2

    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_ChangeBrowsePosition(uint sHandle, ushort direction, string nodeName);
    //OPC_BROWSE_UP 	= 1  	移动到上级节点，忽略NodeName
    //OPC_BROWSE_DOWN 	= 2	 	移动到下级节点
    //OPC_BROWSE_TO 	= 3		直接移动到某一节点


    [DllImport("DACLTSDK.dll")]
    public static extern uint ASDAC_BrowseItems(uint sHandle, ushort filterNodeType, string filterTagName, ushort filterDataType, ushort filterAccessRights, ref object tagNames);
    //filterNode
    //OPC_BRANCH=1
    //OPC_LEAF=2
    //filterAccessRights  read write
    //0-	不能读写  1-	只读  2-	只写  3-	可读可写
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_GetItemFullName(uint sHandle, string itemName, StringBuilder itemFullName, uint itemFullNameSize);
    [DllImport("DACLTSDK.dll")]
    public static extern uint ASDAC_GetItemProperties(uint sHandle, string itemName, ref object propIDs, ref object propDatatypes, ref object propDescs);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_GetItemPropertyValue(uint sHandle, string itemName, uint propID, ref object propValue);
    //for support 3.0
    [DllImport("DACLTSDK.dll")]
    public static extern uint ASDAC_BrowseItemsEx(uint sHandle, string itemID, ushort browseFilter, string itemNameFilter, ref object tagNames, ref object tagIDs);
    //browseFilter
    //OPC_ALL= 1 //OPC_BRANCH=2 //OPC_LEAVES=3
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_ReadItemEx(uint sHandle, string itemID, ref object Value, ref long timeStamp, ref ushort quality);
    [DllImport("DACLTSDK.dll")]
    public static extern bool ASDAC_WriteItemEx(uint sHandle, string ItemID, object Value);
}
