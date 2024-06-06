-- DROP SEQUENCE feed_data.seq_feed_feed_id; 
CREATE SEQUENCE feed_data.seq_feed_feed_id AS INT START WITH 1 INCREMENT BY 1; 



CREATE TABLE feed_data.feed 
(
   feed_id int CONSTRAINT df_feed_feed_id DEFAULT NEXT VALUE FOR feed_data.seq_feed_feed_id 
  ,feed_type character varying(20) -- Type (e.g., Rss 2.0, Atom) 
  ,feed_title national character varying(255) -- Title with Unicode support 
  ,feed_link national character varying(255) -- Link with Unicode support 
  ,feed_description national character varying(MAX) -- Description (large text) 
  ,feed_language character varying(10) -- Language code 
  ,feed_copyright national character varying(255) -- Copyright information 
  ,feed_last_updated_date datetime -- Last updated date (nullable) 
  ,feed_image_url national character varying(255) -- Image URL with Unicode support 
  ,feed_original_document national character varying(MAX) -- Original feed content (large text) 
  ,feed_image binary varying(MAX) -- feed image 
  ,CONSTRAINT pk_feed PRIMARY KEY(feed_id) 
);
