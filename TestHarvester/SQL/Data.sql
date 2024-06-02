
INSERT INTO feed_data.feed 
(
	 feed_id 
	,feed_type 
	,feed_title 
	,feed_link 
	,feed_description 
	,feed_language 
	,feed_copyright 
	,feed_last_updated_date 
	,feed_image_url 
	,feed_original_document 
)
SELECT 
	 1 feed_id -- int
	,NULL AS feed_type -- varchar(20)
	,NULL AS feed_title -- nvarchar(255)
	,NULL AS feed_link -- nvarchar(255)
	,NULL AS feed_description -- nvarchar(max)
	,NULL AS feed_language -- varchar(10)
	,NULL AS feed_copyright -- nvarchar(255)
	,NULL AS feed_last_updated_date -- datetime2(7)
	,NULL AS feed_image_url -- nvarchar(255)
	,NULL AS feed_original_document -- nvarchar(max))
; 


INSERT INTO feed_data.feed_item 
(
	 item_id 
	,item_feed_id 
	,item_title 
	,item_link 
	,item_description 
	,item_publishing_date 
	,item_author 
	,item_content 
)
SELECT 
	 1 AS item_id -- int
	,1 AS item_feed_id -- int 
	,NULL AS item_title -- nvarchar(255) 
	,NULL AS item_link -- nvarchar(255) 
	,NULL AS item_description -- nvarchar(max) 
	,NULL AS item_publishing_date -- datetime2(7) 
	,NULL AS item_author -- nvarchar(255) 
	,NULL AS item_content -- nvarchar(max) 
; 


INSERT INTO feed_data.itunes_item 
(
	 itunes_id 
	,itunes_item_id 
	,itunes_author 
	,itunes_block 
	,itunes_image 
	,itunes_duration 
	,itunes_explicit 
	,itunes_is_closed_captioned 
	,itunes_order 
	,itunes_subtitle 
	,itunes_summary 
)
SELECT 
	 1 AS itunes_id -- int 
	,1 AS itunes_item_id -- int 
	,NULL AS itunes_author -- nvarchar(255) 
	,NULL AS itunes_block -- bit 
	,NULL AS itunes_image -- nvarchar(255) 
	,NULL AS itunes_duration -- nvarchar(50) 
	,NULL AS itunes_explicit -- bit 
	,NULL AS itunes_is_closed_captioned -- bit 
	,NULL AS itunes_order -- int 
	,NULL AS itunes_subtitle -- nvarchar(255) 
	,NULL AS itunes_summary -- nvarchar(max) )
; 


