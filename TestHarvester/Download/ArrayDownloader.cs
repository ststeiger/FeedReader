
namespace TestHarvester
{


    public static class ArrayDownloader
    {


        internal static async System.Threading.Tasks.Task<byte[]?> Test()
        {
            byte[]? content = null;
            string fileUrl = "https://example.com/largefile.zip"; // Replace with the URL of the file you want to download

            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
            {
                content = await DownloadFile(httpClient, fileUrl);
            }

            return content;
        } // End Task Test 


        public static async System.Threading.Tasks.Task<byte[]?> DownloadFile(string fileUrl)
        {
            byte[]? content= null;

            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
            {
                content = await DownloadFile(httpClient, fileUrl);
            }

            return content;
        } // End Task DownloadFile 


        public static async System.Threading.Tasks.Task<byte[]?> DownloadFile(System.Net.Http.HttpClient httpClient, string fileUrl)
        {
            byte[]? fileBytes = null;

            try
            {
                // Send a GET request to the file URL and get the response as a byte array
                fileBytes = await httpClient.GetByteArrayAsync(fileUrl);
                System.Console.WriteLine("File downloaded successfully.");
            } // End Try 
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"An error occurred while downloading the file: {ex.Message}");
            } // End Catch 

            if (fileBytes != null)
            {
                System.Console.WriteLine($"Downloaded {fileBytes.Length} bytes.");
                // You can save the byte array to a file if needed
                System.IO.File.WriteAllBytes("path_to_save_file", fileBytes);
            } // end if (fileBytes != null)

            return fileBytes; 
        } // End Task Test 


    } // End Class ArrayDownloader 


} // End Namepspace 
