
SELECT 
	 feed_id
	,feed_title
	,COUNT(*) AS cnt   
FROM feed_data.feed_item
LEFT JOIN feed_data.feed ON feed_id= item_feed_id
WHERE (1=1) 
-- AND item_feed_id = 10002


GROUP BY feed_id, feed_title 
ORDER BY cnt DESC 
