
using TestHarvester.Db;

namespace TestHarvester
{
    

    internal class Program
    {


        public static string ProjectDirectory
        {
            get {
                string bd = System.AppDomain.CurrentDomain.BaseDirectory;
                bd = System.IO.Path.Combine(bd, "..", "..", "..");
                bd = System.IO.Path.GetFullPath(bd);

                return bd;
            }
        }


        public static async System.Threading.Tasks.Task<int> Main(string[] args)
        {
            ConnectionFactory factory = DevHelper.GetTestFactory();
            DatabaseManager dbm = new DatabaseManager(factory);

            string harvestDirectory = System.IO.Path.Combine(ProjectDirectory, "Harvested");
            
            string feedFile = System.IO.Path.Combine(harvestDirectory, "myfeed.rss");
            CodeHollow.FeedReader.Feed? feed = null; // CodeHollow.FeedReader.FeedReader.ReadFromString("xml");

            if (System.IO.File.Exists(feedFile))
                feed = await CodeHollow.FeedReader.FeedReader.ReadFromFileAsync(feedFile);
            
            if (feed == null)
            {
                feed = await CodeHollow.FeedReader.FeedReader.ReadAsync("https://arminreiter.com/feed");
                feed.SaveAs(feedFile);
            } // End if (feed == null) 



            string imagePath = System.IO.Path.Combine(harvestDirectory, System.IO.Path.GetFileName(feed.ImageUrl));
            string extension = System.IO.Path.GetFileName(feed.ImageUrl);
            if(!System.IO.File.Exists(imagePath))
                await StreamDownloader.DownloadFileAsync(feed.ImageUrl, imagePath);


            System.Console.WriteLine("Feed Link: " + feed.Link);
            System.Console.WriteLine("Feed Type: " + feed.Type);
            System.Console.WriteLine("Feed Title: " + feed.Title);
            System.Console.WriteLine("Feed Description: " + feed.Description);
            System.Console.WriteLine("Feed Image: " + feed.ImageUrl);
            System.Console.WriteLine("Feed LastUpdatedDate: " + feed.LastUpdatedDate);
            System.Console.WriteLine("Feed Copyright: " + feed.Copyright);
            System.Console.WriteLine("Feed Language: " + feed.Language);

            // System.Console.WriteLine("Feed OriginalDocument: " + feed.OriginalDocument);



#if false
            if (feed.Type == CodeHollow.FeedReader.FeedType.Atom)
            {

                CodeHollow.FeedReader.Feeds.AtomFeed atomFeed = (CodeHollow.FeedReader.Feeds.AtomFeed)feed.SpecificFeed;
                foreach (CodeHollow.FeedReader.Feeds.BaseFeedItem? item in atomFeed.Items)
                {
                    System.Console.WriteLine(item.Title + " - " + item.Link);
                    System.Console.WriteLine(item.Element);
                }

            }
            else if (feed.Type == CodeHollow.FeedReader.FeedType.Rss_2_0)
            {
                CodeHollow.FeedReader.Feeds.Rss20Feed rss20feed = (CodeHollow.FeedReader.Feeds.Rss20Feed)feed.SpecificFeed;
                System.Console.WriteLine("Generator: " + rss20feed.Generator);

                foreach (CodeHollow.FeedReader.Feeds.BaseFeedItem? item in rss20feed.Items)
                {
                    System.Console.WriteLine(item.Title + " - " + item.Link);
                    System.Console.WriteLine(item.Element);
                }

            }
            else
#endif
            {

                int feedId = dbm.InsertFeed(feed);
                

                foreach (CodeHollow.FeedReader.FeedItem? item in feed.Items)
                {
                    int feedItem = dbm.InsertFeedItem(item, feedId);

                    System.Console.WriteLine(item.Title + " - " + item.Link);
                    System.Console.WriteLine(item.Id);
                    System.Console.WriteLine(item.Description);
                    System.Console.WriteLine(item.Content);
                    System.Console.WriteLine(item.Author);
                    System.Console.WriteLine(item.Categories);
                    System.Console.WriteLine(item.PublishingDate);
                    System.Console.WriteLine(item.SpecificItem);
                    
                    CodeHollow.FeedReader.Feeds.Itunes.ItunesItem itu = CodeHollow.FeedReader.Feeds.Itunes.ItunesExtensions.GetItunesItem(item);
                    if (itu != null)
                    {
                        dbm.InsertItunesItem(itu, feedItem);

                        System.Console.WriteLine(itu.Subtitle);
                        System.Console.WriteLine(itu.Summary);
                        System.Console.WriteLine(itu.Duration);
                        System.Console.WriteLine(itu.Image);
                        System.Console.WriteLine(itu.Order);
                        System.Console.WriteLine(itu.Block);
                        System.Console.WriteLine(itu.Author);
                        System.Console.WriteLine(itu.Explicit);
                        System.Console.WriteLine(itu.IsClosedCaptioned);
                    } // End if (itu != null)

                } // Next item 

            } // End Block  or Else 

            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
            return await System.Threading.Tasks.Task.FromResult(0);
        } // End Sub Main 


    } // End Class Program 


} // End Namespace 
