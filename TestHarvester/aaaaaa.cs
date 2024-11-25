
namespace TestHarvester
{

    
    


    class dasfasdf
    {
        

        public static async System.Threading.Tasks.Task<string> SendRequest()
        {
            long millisecondsSinceEpoch = System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            string url = "https://data.services.jetbrains.com/products?code=RD&release.type=eap%2Crc%2Crelease&fields=distributions%2Clink%2Cname%2Creleases&_=" + millisecondsSinceEpoch.ToString(System.Globalization.CultureInfo.InvariantCulture);

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                // Set request headers
                client.DefaultRequestHeaders.Add("accept", "application/json, text/javascript, */*; q=0.01");

                // client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/javascript"));
                // client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*", 0.01));


                // client.DefaultRequestHeaders.Add("accept-language", "en-US,en;q=0.9,de-CH;q=0.8,de;q=0.7");
                client.DefaultRequestHeaders.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("en-US"));
                client.DefaultRequestHeaders.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("en", 0.9));
                client.DefaultRequestHeaders.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("de-CH", 0.8));
                client.DefaultRequestHeaders.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("de", 0.7));

                client.DefaultRequestHeaders.Add("dnt", "1");
                client.DefaultRequestHeaders.Add("origin", "https://www.jetbrains.com");
                client.DefaultRequestHeaders.Add("priority", "u=1, i");
                client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Google Chrome\";v=\"126\"");
                client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                client.DefaultRequestHeaders.Add("sec-fetch-site", "same-site");
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");


                // client.DefaultRequestHeaders.Dnt = true;
                // client.DefaultRequestHeaders.Origin = "https://www.jetbrains.com";
                // client.DefaultRequestHeaders.Priority = new System.Net.Http.Headers.PriorityHeaderValue(1);
                // client.DefaultRequestHeaders.SecFetchDest = HttpCompletionOption.Empty;
                // client.DefaultRequestHeaders.SecFetchMode = HttpFetchMode.Cors;
                // client.DefaultRequestHeaders.SecFetchSite = SameSiteMode.SameSite;

                System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                System.IO.File.WriteAllText(@"D:\riderVersions.json", content, System.Text.Encoding.UTF8);


                System.Collections.Generic.List<Root>? myDeserializedClass = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<Root>>(content);
                System.Console.WriteLine(myDeserializedClass);

                return content;
            }
        }




    public static async System.Threading.Tasks.Task Test()
        {
            var client = new System.Net.Http.HttpClient();

            // First request
            var request1 = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, "https://blog.jetbrains.com/dotnet/feed/");

            // request1.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            // request1.Headers.Add("accept-language", "en-US,en;q=0.9,de-CH;q=0.8,de;q=0.7");
            // request1.Headers.Add("cache-control", "max-age=0");

            // request1.Headers.Add("referer", "https://example.com");
            request1.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");

            var response1 = await client.SendAsync(request1);
            var content1 = await response1.Content.ReadAsStringAsync();
            System.Console.WriteLine(content1);
        }
    }



}
