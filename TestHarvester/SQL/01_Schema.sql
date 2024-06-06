
-- CREATE SCHEMA IF NOT EXISTS FOO; 
IF NOT EXISTS (SELECT * FROM information_schema.schemata WHERE schema_name = N'feed_data')
BEGIN
  CREATE SCHEMA feed_data AUTHORIZATION [dbo];  -- Replace 'dbo' with your desired owner
END;
GO
