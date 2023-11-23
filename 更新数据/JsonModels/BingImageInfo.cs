namespace 更新数据.JsonModels;
// 定义用于反序列化JSON的类
public class BingImageInfo
{
    public List<ImageInfo> Images { get; set; }
    public Tooltips Tooltips { get; set; }
}

public class ImageInfo
{
    // 定义与JSON数据结构相匹配的属性
    public string StartDate { get; set; }
    public string FullStartDate { get; set; }
    public string EndDate { get; set; }
    public string Url { get; set; }
    public string UrlBase { get; set; }
    public string Copyright { get; set; }
    public string CopyrightLink { get; set; }
    public string Title { get; set; }
    public string Quiz { get; set; }
    public bool Wp { get; set; }
    public string Hsh { get; set; }
    public int Drk { get; set; }
    public int Top { get; set; }
    public int Bot { get; set; }
    public List<object> Hs { get; set; }
}

public class Tooltips
{
    public string Loading { get; set; }
    public string Previous { get; set; }
    public string Next { get; set; }
    public string Walle { get; set; }
    public string Walls { get; set; }
}
