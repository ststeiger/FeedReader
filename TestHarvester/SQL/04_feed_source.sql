
-- DROP SEQUENCE feed_data.seq_feed_source_fs_id; 
CREATE SEQUENCE feed_data.seq_feed_source_fs_id AS INT START WITH 1 INCREMENT BY 1; 


-- DROP TABLE feed_data.feed_source; 


CREATE TABLE feed_data.feed_source 
( 
	 fs_id int NOT NULL CONSTRAINT df_feed_source_fs_id DEFAULT NEXT VALUE FOR feed_data.seq_feed_source_fs_id 
	,fs_feed_id int 
	,fs_name national character varying(100) NULL 
	,fs_url character varying(2048) NULL 
	,fs_url_info_source character varying(2048) NULL 
	,CONSTRAINT pk_feed_source PRIMARY KEY(fs_id) 
); 


ALTER TABLE feed_data.feed_source WITH CHECK ADD CONSTRAINT fk_feed_source_feed 
	FOREIGN KEY(fs_feed_id) REFERENCES feed_data.feed( feed_id ) 
	ON UPDATE CASCADE
	ON DELETE CASCADE
GO

ALTER TABLE feed_data.feed_source CHECK CONSTRAINT fk_feed_source_feed
GO
