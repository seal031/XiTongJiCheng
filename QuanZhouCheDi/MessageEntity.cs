using Newtonsoft.Json;

public class MessCommand
{
    public string inspectionTime { get; set; }//车底检查时间
    public string electronCarlicense { get; set; }//电子车牌号RFID
    public string roadNumber { get; set; }//道口编号
    public string carButtomPicPath { get; set; }//汽车底部图像路径
    public string liceNumberPicPath { get; set; }//车牌图像路径
    public string liceNumber { get; set; }//车牌号
    public string drivDirection { get; set; }//车行方向 0表示"出",1表示"进"
    public string carFacadePicPath { get; set; }//汽车外观图像路径
    public int carButtomStartLoca { get; set; }//车底图像的起始位置
    public int carButtonEndLoca { get; set; }//车底图像的结束位置
    public string driverId { get; set; }//驾驶员ID
    public string currentCode { get; set; }//当前段编号
    public int totalCodeNum { get; set; }//总段数
    public string toJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}
