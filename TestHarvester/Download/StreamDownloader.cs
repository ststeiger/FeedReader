
using Azure;
using Azure.Core;
using System.Net.Http;
using System.Threading;

namespace TestHarvester
{


    public static class StreamDownloader
    {


        internal static async System.Threading.Tasks.Task Test()
        {
            string url = "https://your-large-file-url.com/file.zip"; // Replace with your download URL
            string filePath = "C:\\Downloads"; // Replace with your desired download folder
            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");

                await DownloadFileAsync(httpClient, url, filePath); // Wait for the async task to complete (consider non-blocking alternatives)
            } // End Using httpClient 

            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        } // End Sub Test 


        public static async System.Threading.Tasks.Task DownloadFileAsync(string url, string filePath)
        {

            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");

                await DownloadFileAsync(httpClient, url, filePath); // Wait for the async task to complete (consider non-blocking alternatives)
            } // End Using httpClient 

        } // End TaskDownloadFileAsync 


        public static async System.Threading.Tasks.Task DownloadFileAsync(System.Net.Http.HttpClient httpClient, string url, string filePath)
        {
            using (System.Net.Http.HttpResponseMessage response = await httpClient.GetAsync(url, System.Net.Http.HttpCompletionOption.ResponseHeadersRead))
            {





                response.EnsureSuccessStatusCode(); // Check for successful response

                using (System.IO.FileStream fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.CreateNew))
                {
                    long? contentLength = response.Content.Headers.ContentLength;
                    long totalBytesDownloaded = 0;

                    using (System.IO.Stream contentStream = await response.Content.ReadAsStreamAsync())
                    {
                        byte[] buffer = new byte[8192]; // Adjust buffer size as needed
                        int bytesRead;
                        while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead);
                            totalBytesDownloaded += bytesRead;

                            // Optional: Report download progress (consider thread safety for UI updates)
                            System.Console.WriteLine($"Downloaded {totalBytesDownloaded} out of {contentLength} bytes");
                        }
                    } // End Using contentStream 

                } // End Using fileStream 

                System.Console.WriteLine($"Download complete: {url}");
            } // End Using response 


        } // End Task DownloadFileAsync 


    } // End Class StreamDownloader 


} // End Namespace 