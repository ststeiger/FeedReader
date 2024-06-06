
-- DROP SEQUENCE feed_data.seq_itunes_item_itunes_id; 
CREATE SEQUENCE feed_data.seq_itunes_item_itunes_id AS INT START WITH 1 INCREMENT BY 1;

CREATE TABLE feed_data.itunes_item 
(
	 itunes_id int NOT NULL CONSTRAINT df_itunes_item_itunes_id DEFAULT NEXT VALUE FOR feed_data.seq_itunes_item_itunes_id  -- Use sequence for ID  -- Regular int for ID
	,itunes_item_id int NOT NULL 
	,itunes_author national character varying(255) 
	,itunes_block bit 
	,itunes_image national character varying(255) 
	,itunes_duration national character varying(50) -- Duration as string (consider a dedicated time datatype if needed) 
	,itunes_explicit bit 
	,itunes_is_closed_captioned bit 
	,itunes_order int 
	,itunes_subtitle national character varying(255) 
	,itunes_summary national character varying(MAX) 
	,CONSTRAINT pk_itunes_item PRIMARY KEY(itunes_id) 
); 

ALTER TABLE feed_data.itunes_item WITH CHECK ADD CONSTRAINT fk_itunes_item_feed_item 
FOREIGN KEY(itunes_item_id) REFERENCES feed_data.feed_item (item_id) 
ON DELETE CASCADE 
ON UPDATE CASCADE 
GO

ALTER TABLE feed_data.itunes_item CHECK CONSTRAINT fk_itunes_item_feed_item; 
GO
