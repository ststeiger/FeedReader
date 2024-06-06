
DECLARE @some_data TABLE 
(
	 ps_id int NOT NULL 
	,ps_first_name nvarchar(255)
	,ps_last_name nvarchar(255)
);


;WITH CTE AS 
(
	SELECT 1 AS i
	UNION ALL 

	SELECT CTE.i +1 AS i 
	FROM CTE 
	WHERE CTE.i < 100 
)

INSERT INTO @some_data(ps_id, ps_first_name, ps_last_name) 
SELECT 
	 i 
	,'Vorname ' + CAST(i AS nvarchar(255)) 
	,'Nachname ' + CAST(i AS nvarchar(255)) 
FROM CTE 

SELECT * FROM @some_data 



SELECT 
	 NEXT VALUE FOR CountBy OVER (ORDER BY ps_last_name) AS ListNumber,
     ps_first_name, ps_last_name
	,ps_id 
-- INTO table_x
FROM @some_data



-- ALTER SEQUENCE dbo.CountBy RESTART WITH 1; 
-- -- ALTER SEQUENCE dbo.CountBy RESTART; -- this is BAD 




SELECT 
     sch.name AS table_schema 
	,t.name AS table_name  
	,c.name AS column_name 
	 
	,dc.definition AS default_definition 

	,sequence_schema.name AS sequence_schema 
	,s.name AS sequence_name 

	,'ALTER SEQUENCE ' 
	+ + sequence_schema.name + '.' + s.name 
	+ ' RESTART WITH ' 
	+ sequence_info.sequence_restart_value 
	+ '; ' 
	AS sql_restart_sequence 
	 
	-- ,sequence_info.largest_value_in_column 
	-- ,sequence_info.sequence_restart_value 
	 
	-- ,sch.name,  t.name, c.name
	-- compatibility.get_highest_value(sch.name,  t.name, ) 

	--,'SELECT MAX('+ QUOTENAME(c.name) + ') AS val FROM ' 
	--+ QUOTENAME(sch.name) + '.' + QUOTENAME(t.name) 
	--+ '; ' 
	--AS sql_max_value  

	,'SELECT NEXT VALUE FOR ' 
	+ sequence_schema.name + '.' + s.name 
	+ ' AS next_value ' 
	AS sql_next_value 
FROM sys.tables AS t 
INNER JOIN sys.schemas AS sch ON sch.schema_id = t.schema_id 
INNER JOIN sys.columns AS c ON t.object_id = c.object_id 
INNER JOIN sys.default_constraints AS dc ON c.default_object_id = dc.object_id 

INNER JOIN sys.sequences AS s 
INNER JOIN sys.schemas AS sequence_schema 
	ON sequence_schema.schema_id = s.schema_id 
	ON dc.definition LIKE '%NEXT%VALUE%FOR%' + s.name + '%' 
	OR dc.definition LIKE '%NEXT%VALUE%FOR%' + sequence_schema.name + '%.%' + s.name + '%' 

OUTER APPLY 
	(
		SELECT 
			 -- from https://github.com/ststeiger/DatabaseVersionControlTrigger 
			 compatibility.get_highest_value(sch.name,  t.name, c.name) AS largest_value_in_column 
			,CAST(1 + ISNULL(compatibility.get_highest_value(sch.name,  t.name, c.name), 0) AS varchar(36)) AS sequence_restart_value 
			-- ,compatibility.get_next_sequence_value(sequence_schema.name,s.name) AS seq_next_value 
	) AS sequence_info 

WHERE (1=1) 
-- AND s.name = 'YourSequenceName' 

-- SELECT MAX(item_id) AS val FROM feed_data.feed_item; 
-- SELECT NEXT VALUE FOR feed_data.seq_feed_item_item_id AS next_value 


-- SELECT MAX(item_id) AS val FROM feed_data.feed_item; 
-- SELECT NEXT VALUE FOR feed_data.seq_feed_item_item_id AS next_value 








-- sequence update 

DECLARE @sequence_restart_sql nvarchar(MAX); 

DECLARE sequence_cursor CURSOR FOR 
(
	SELECT 
		 'ALTER SEQUENCE ' 
		+ QUOTENAME(sequence_schema.name) + '.' + QUOTENAME(s.name) 
		+ ' RESTART WITH ' 
		+ sequence_info.sequence_restart_value 
		+ '; ' 
		AS sql_restart_sequence 
	FROM sys.tables AS t 
	INNER JOIN sys.schemas AS sch ON sch.schema_id = t.schema_id 
	INNER JOIN sys.columns AS c ON t.object_id = c.object_id 
	INNER JOIN sys.default_constraints AS dc ON c.default_object_id = dc.object_id 

	INNER JOIN sys.sequences AS s 
	INNER JOIN sys.schemas AS sequence_schema 
		ON sequence_schema.schema_id = s.schema_id 
		ON dc.definition LIKE '%NEXT%VALUE%FOR%' + s.name + '%' 
		OR dc.definition LIKE '%NEXT%VALUE%FOR%' + sequence_schema.name + '%.%' + s.name + '%' 

	OUTER APPLY 
		(
			SELECT 
				 -- from https://github.com/ststeiger/DatabaseVersionControlTrigger 
				 compatibility.get_highest_value(sch.name,  t.name, c.name) AS largest_value_in_column 
				,CAST(1 + ISNULL(compatibility.get_highest_value(sch.name,  t.name, c.name), 0) AS varchar(36)) AS sequence_restart_value 
				-- ,compatibility.get_next_sequence_value(sequence_schema.name,s.name) AS seq_next_value 
		) AS sequence_info 

	WHERE (1=1) 
)

OPEN sequence_cursor  
FETCH NEXT FROM sequence_cursor INTO @sequence_restart_sql  

WHILE @@FETCH_STATUS = 0  
BEGIN  
	  PRINT @sequence_restart_sql; 
	  EXECUTE(@sequence_restart_sql); 

      FETCH NEXT FROM sequence_cursor INTO @sequence_restart_sql 
END 

CLOSE sequence_cursor  
DEALLOCATE sequence_cursor
