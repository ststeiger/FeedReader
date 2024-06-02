
-- DROP SEQUENCE feed_data.seq_feed_item_item_id; 
CREATE SEQUENCE feed_data.seq_feed_item_item_id AS INT START WITH 1 INCREMENT BY 1;

CREATE TABLE feed_data.feed_item 
(
   item_id int NOT NULL CONSTRAINT df_FeedItem_item_id DEFAULT NEXT VALUE FOR feed_data.seq_feed_item_item_id 
  ,item_feed_id int 
  ,item_title nvarchar(255) 
  ,item_link nvarchar(255) 
  ,item_description nvarchar(MAX) 
  ,item_publishing_date datetime2(7) 
  ,item_author nvarchar(255) 
  
  ,item_content nvarchar(MAX) 
  ,PRIMARY KEY(item_id) 
);

ALTER TABLE feed_data.feed_item WITH CHECK ADD CONSTRAINT fk_feed_item_feed 
FOREIGN KEY(item_feed_id) REFERENCES feed_data.feed (feed_id)
ON DELETE CASCADE 
ON UPDATE CASCADE 
; 
GO

ALTER TABLE feed_data.feed_item CHECK CONSTRAINT fk_feed_item_feed; 
GO
