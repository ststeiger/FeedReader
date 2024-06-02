
SELECT 
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

	,item_id 
	,item_feed_id 
	,item_title 
	,item_link 
	,item_description 
	,item_publishing_date 
	,item_author 
	,item_content 

	,itunes_id 
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
FROM feed_data.feed 
LEFT JOIN feed_data.feed_item ON feed_item.item_feed_id = feed.feed_id 
LEFT JOIN feed_data.itunes_item ON itunes_item.itunes_item_id = feed_item.item_id 
