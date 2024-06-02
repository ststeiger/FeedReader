
SELECT 
	 item_id 
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
FROM feed_data.feed_item 

LEFT JOIN feed_data.itunes_item 
	ON itunes_item.itunes_item_id = feed_item.item_id
