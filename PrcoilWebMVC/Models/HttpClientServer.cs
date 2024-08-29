using AzureFantasy_Web.Models.Data;
using Newtonsoft.Json;

public class HttpClientServer
{
    private readonly HttpClient _httpClient;

    public HttpClientServer()
    {
        _httpClient = new HttpClient();
    }

    public double SendPostRequest(string apiUrl, Dictionary<string, string> formData)
    {
        try
        {
            // 使用 FormUrlEncodedContent 来设置请求内容
            var content = new FormUrlEncodedContent(formData);

            // 发送 HTTP POST 请求
            HttpResponseMessage response = _httpClient.PostAsync(apiUrl, content).Result;

            if (response.IsSuccessStatusCode)
            {
                // 如果请求成功，读取响应内容
                string result = response.Content.ReadAsStringAsync().Result;

                // 使用 Json.NET 解析 JSON 数据
                var parsedData = JsonConvert.DeserializeObject<ReCaptchaData>(result);

                var score = parsedData.score;
                // 此时，parsedData 包含了解析后的对象
                // 可以根据实际返回的数据结构定义 MyResponseType 类

                return score;
            }
            else
            {
                // 如果请求失败，处理错误
                Console.WriteLine($"Error: {response.StatusCode}");
                return 999;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            return 999;
        }
    }
}