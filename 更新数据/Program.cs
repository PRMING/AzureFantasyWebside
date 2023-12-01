using System;
using System.Globalization;
using 更新数据.JsonModels;

namespace 更新数据
{
    internal class Program
    {
        static async Task Main()
        {
            // 设置中文区域性信息
            CultureInfo chineseCulture = new CultureInfo("zh-CN");

            string dailyPage = "daily_page";

            MySqlService mySqlService = new MySqlService();



            while (true)
            {
                if (mySqlService.MySqlSelect("show_date", "show_date", System.DateTime.Now.ToString("yyyy年MM月dd日", chineseCulture), dailyPage) != System.DateTime.Now.ToString("yyyy年MM月dd日", chineseCulture))
                {
                    GetInfoService bingImageService = new GetInfoService();
                    string bingApiUrl = "https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN"; // 替换成实际的API地址
                    string hitokotoApiUrl = "https://v1.hitokoto.cn";

                    BingImageInfo bingImageInfo = await bingImageService.GetBingImageInfo(bingApiUrl);
                    HitokotoInfo hitokotoInfo = await bingImageService.GetHitokotoInfo(hitokotoApiUrl);

                    //if (bingImageInfo != null)
                    //{
                    //    Console.WriteLine($"Title: {bingImageInfo.Images[0].Title}");
                    //    Console.WriteLine($"https://cn.bing.com/{bingImageInfo.Images[0].Url}");
                    //    Console.WriteLine($"Copyright: {bingImageInfo.Images[0].Copyright}");
                    //    //其他信息...
                    //}

                    //if(hitokotoInfo.from_who==null)
                    //{
                    //    hitokotoInfo.from_who = "无";
                    //}
                    //if (hitokotoInfo.from == null)
                    //{
                    //    hitokotoInfo.from_who = "无";
                    //}

                    //Console.WriteLine(hitokotoInfo.hitokoto);
                    //Console.WriteLine(hitokotoInfo.from);
                    //Console.WriteLine(hitokotoInfo.from_who);

                    mySqlService.MySqlInsertDaily("image_url", "show_date", "from_where", "from_who", "hitokoto", dailyPage, $"https://cn.bing.com{bingImageInfo.Images[0].Url}", System.DateTime.Now.ToString("yyyy年MM月dd日", chineseCulture),hitokotoInfo.from,hitokotoInfo.from_who,hitokotoInfo.hitokoto);

                    Console.WriteLine();
                    Console.WriteLine("------------------------------------------------------------------------------------------------------");
                    Console.WriteLine($"[INFO]{System.DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss", chineseCulture)} 已更新:");
                    Console.WriteLine($"[INFO]{System.DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss", chineseCulture)} 更新日期: {System.DateTime.Now.ToString("yyyy年MM月dd日", chineseCulture)}");
                    Console.WriteLine($"[INFO]{System.DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss", chineseCulture)} 图片地址: https://cn.bing.com{bingImageInfo.Images[0].Url}");
                    Console.WriteLine($"[INFO]{System.DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss", chineseCulture)} 一言: {hitokotoInfo.hitokoto}");
                    Console.WriteLine($"[INFO]{System.DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss", chineseCulture)} 一言From_Where: {hitokotoInfo.from}");
                    Console.WriteLine($"[INFO]{System.DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss", chineseCulture)} 一言From_Who: {hitokotoInfo.from_who}");
                    Console.WriteLine("------------------------------------------------------------------------------------------------------");
                    Console.WriteLine();

                }
                else 
                {
                    int updateTime = 1;
                    Console.WriteLine($"[INFO]{System.DateTime.Now.ToString("yyyy年MM月dd日", chineseCulture)} 无需更新 下次更新检测时间: {Convert.ToString(updateTime)} 分");
                    Thread.Sleep(updateTime*1000*60);
                }
            }
        }
    }
}
