-- DROP SEQUENCE feed_data.seq_feed_types_ft_id; 
CREATE SEQUENCE feed_data.seq_feed_types_ft_id AS INT START WITH 1 INCREMENT BY 1; 


CREATE TABLE feed_data.feed_types
(
	 ft_id int NOT NULL CONSTRAINT df_feed_feed_id DEFAULT NEXT VALUE FOR feed_data.seq_feed_feed_id 
	,ft_name national character varying(50) NULL 
	,ft_version int NULL 
	,ft_subversion int NULL 
	,CONSTRAINT pk_feed_types PRIMARY KEY(ft_id) 
);
