
namespace TestHarvester
{
    
    
    using Dapper;


    public class DatabaseManager
    {

        private readonly TestHarvester.Db.ConnectionFactory m_factory;


        public DatabaseManager(TestHarvester.Db.ConnectionFactory factory)
        {
            this.m_factory = factory;
        } // End Constructo 


        public int InsertFeed(CodeHollow.FeedReader.Feed feed)
        {

            using (System.Data.Common.DbConnection connection = this.m_factory.Connection)
            {
                // Use parameter names matching class properties for clarity and SQL injection prevention
                string sql = @"
INSERT INTO feed_data.feed 
(
    feed_type, feed_title, feed_link, feed_description 
   ,feed_language, feed_copyright, feed_last_updated_date, feed_image_url, feed_original_document 
) 
OUTPUT INSERTED.feed_id 
VALUES (@Type, @Title, @Link, @Description, @Language, @Copyright, @LastUpdatedDate, @ImageUrl, @OriginalDocument); 
-- SELECT CAST(SCOPE_IDENTITY() AS int); 
";

                // Convert LastUpdatedDate to UTC for database storage (optional)
                var parameters = new
                {
                    Type = feed.Type,
                    Title = feed.Title,
                    Link = feed.Link,
                    Description = feed.Description,
                    Language = feed.Language,
                    Copyright = feed.Copyright,
                    LastUpdatedDate = feed.LastUpdatedDate?.ToUniversalTime(), // Convert to UTC if needed
                    ImageUrl = feed.ImageUrl,
                    OriginalDocument = feed.OriginalDocument
                };

                // Execute query, returning the inserted feed ID
                return connection.QuerySingle<int>(sql, parameters);
            } // End Using connection 

        } // End Function InsertFeed 

        public int InsertFeedItem(CodeHollow.FeedReader.FeedItem item, int feedId)
        {
            using (System.Data.Common.DbConnection connection = this.m_factory.Connection)
            {
                string sql = @"
INSERT INTO feed_data.feed_item (item_feed_id, item_title, item_link, item_description, 
item_publishing_date, item_author, item_content)
OUTPUT INSERTED.item_id 
VALUES (@FeedId, @Title, @Link, @Description, @PublishingDate, @Author, @Content); 
-- SELECT CAST(SCOPE_IDENTITY() AS int); 
";

                // Convert PublishingDate to UTC for database storage (optional)
                var parameters = new
                {
                    FeedId = feedId,
                    Title = item.Title,
                    Link = item.Link,
                    Description = item.Description,
                    PublishingDate = item.PublishingDate?.ToUniversalTime(), // Convert to UTC if needed
                    Author = item.Author,
                    Content = item.Content
                };

                // connection.Execute(sql, parameters);
                return connection.QuerySingle<int>(sql, parameters);
            }
        } // End Function 


        public int InsertItunesItem(CodeHollow.FeedReader.Feeds.Itunes.ItunesItem itunesItem, int feedItemId)
        {
            using (System.Data.Common.DbConnection connection = this.m_factory.Connection)
            {
                string sql = @"
INSERT INTO feed_data.itunes_item 
(
     itunes_item_id, itunes_author, itunes_block, itunes_image
    ,itunes_duration, itunes_explicit, itunes_is_closed_captioned
    ,itunes_order, itunes_subtitle, itunes_summary
)
OUTPUT INSERTED.itunes_id 
VALUES (@FeedItemId, @Author, @Block, @Image, @Duration, @Explicit, @IsClosedCaptioned, @Order, @Subtitle, @Summary); 
-- SELECT CAST(SCOPE_IDENTITY() AS int); 
";

                // Convert Duration to a string compatible with your database (optional)
                var durationString = itunesItem.Duration?.ToString("hh\\:mm\\:ss"); // Adjust format if needed

                var parameters = new
                {
                    FeedItemId = feedItemId,
                    Author = itunesItem.Author,
                    Block = itunesItem.Block,
                    Image = itunesItem.Image?.Href, 
                    Duration = durationString,
                    Explicit = itunesItem.Explicit,
                    IsClosedCaptioned = itunesItem.IsClosedCaptioned,
                    Order = itunesItem.Order,
                    Subtitle = itunesItem.Subtitle,
                    Summary = itunesItem.Summary
                };

                // connection.Execute(sql, parameters);
                return connection.QuerySingle<int>(sql, parameters);
            }
        } // End Function InsertItunesItem 


    } // End Class DatabaseManager

}