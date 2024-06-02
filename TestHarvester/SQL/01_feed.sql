-- DROP SEQUENCE feed_data.seq_feed_feed_id; 
CREATE SEQUENCE feed_data.seq_feed_feed_id AS INT START WITH 1 INCREMENT BY 1; 



CREATE TABLE feed_data.feed 
(
   feed_id int CONSTRAINT df_feed_feed_id DEFAULT NEXT VALUE FOR feed_data.seq_feed_feed_id 
  ,feed_type varchar(20) -- Type (e.g., Rss 2.0, Atom) 
  ,feed_title nvarchar(255) -- Title with Unicode support 
  ,feed_link nvarchar(255) -- Link with Unicode support 
  ,feed_description nvarchar(MAX) -- Description (large text) 
  ,feed_language varchar(10) -- Language code 
  ,feed_copyright nvarchar(255) -- Copyright information 
  ,feed_last_updated_date datetime2(7) -- Last updated date (nullable) 
  ,feed_image_url nvarchar(255) -- Image URL with Unicode support 
  ,feed_original_document nvarchar(MAX) -- Original feed content (large text) 
  ,PRIMARY KEY(feed_id)
);
