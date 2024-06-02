namespace CodeHollow.FeedReader
{
    using Feeds;


    /// <summary>
    /// Generic Feed object that contains some basic properties. If a property is not available
    /// for a specific feed type (e.g. Rss 1.0), then the property is empty.
    /// If a feed has more properties, like the Generator property for Rss 2.0, then you can use
    /// the <see cref="SpecificFeed"/> property.
    /// </summary>
    public class Feed
    {
        /// <summary>
        /// The Type of the feed - Rss 2.0, 1.0, 0.92, Atom or others
        /// </summary>
        public FeedType Type { get; set; }

        /// <summary>
        /// The title of the field
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The link (url) to the feed
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// The description of the feed
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The language of the feed
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// The copyright of the feed
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// The last updated date as string. This is filled, if a last updated
        /// date is set - independent if it is a correct date or not
        /// </summary>
        public string LastUpdatedDateString { get; set; }

        /// <summary>
        /// The last updated date as datetime. Null if parsing failed or if
        /// no last updated date is set. If null, please check <see cref="LastUpdatedDateString"/> property.
        /// </summary>
        public System.DateTime? LastUpdatedDate { get; set; }

        /// <summary>
        /// The url of the image
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// List of items
        /// </summary>
        public System.Collections.Generic.IList<FeedItem> Items { get; set; }

        /// <summary>
        /// Gets the whole, original feed as string
        /// </summary>
        public string OriginalDocument
        {
            get { return SpecificFeed.OriginalDocument; }
        }

        /// <summary>
        /// The parsed feed element - e.g. of type <see cref="Rss20Feed"/> which contains
        /// e.g. the Generator property which does not exist in others.
        /// </summary>
        public BaseFeed SpecificFeed { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Feed"/> class.
        /// Default constructor, just there for serialization.
        /// </summary>
        public Feed()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Feed"/> class.
        /// Creates the generic feed object based on a parsed BaseFeed
        /// </summary>
        /// <param name="feed">BaseFeed which is a <see cref="Rss20Feed"/> , <see cref="Rss10Feed"/>, or another.</param>
        public Feed(BaseFeed feed)
        {
            SpecificFeed = feed;

            Title = feed.Title;
            Link = feed.Link;

            Items = System.Linq.Enumerable.ToList( System.Linq.Enumerable.Select(feed.Items, x => x.ToFeedItem()));
        }


        private static System.Text.Encoding GetXmlEncoding(string xmlString)
        {
            if (string.IsNullOrEmpty(xmlString))
            {
                throw new System.ArgumentException("The XML string cannot be null or empty.");
            }

            using (System.IO.StringReader stringReader = new System.IO.StringReader(xmlString))
            {
                System.Xml.XmlReaderSettings settings = new System.Xml.XmlReaderSettings();
                settings.DtdProcessing = System.Xml.DtdProcessing.Parse;

                using (System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(stringReader, settings))
                {
                    if (xmlReader.Read())
                    {
                        // Check if the current node is an XML declaration
                        if (xmlReader.NodeType == System.Xml.XmlNodeType.XmlDeclaration)
                        {
                            string encoding = xmlReader.GetAttribute("encoding");
                            if (!string.IsNullOrEmpty(encoding))
                            {
                                return System.Text.Encoding.GetEncoding(encoding);
                            }
                        }
                    }

                }
            }

            // Default to UTF-8 if encoding is not specified in the XML declaration
            return System.Text.Encoding.UTF8;
        }


        /// <summary>
        /// Save the feed as file 
        /// </summary>
        /// <param name="path"></param>
        public void SaveAs(string path)
        {
            System.Text.Encoding enc = GetXmlEncoding(this.OriginalDocument);
            System.IO.File.WriteAllText(path, this.OriginalDocument, enc);
        }


    }
}
