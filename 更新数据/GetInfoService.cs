using Newtonsoft.Json;
using 更新数据.JsonModels;

namespace 更新数据
{
    public class GetInfoService
    {
        private readonly HttpClient _httpClient;

        public GetInfoService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<BingImageInfo> GetBingImageInfo(string apiUrl)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    BingImageInfo bingImageInfo = JsonConvert.DeserializeObject<BingImageInfo>(jsonContent);
                    return bingImageInfo;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<HitokotoInfo> GetHitokotoInfo(string apiUrl)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    HitokotoInfo hitokotoInfo = JsonConvert.DeserializeObject<HitokotoInfo>(jsonContent);
                    return hitokotoInfo;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
    }
}
