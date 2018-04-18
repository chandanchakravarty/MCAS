CREATE FUNCTION [dbo].[fnSplitString](@string [nvarchar](max), @delimiter [char](1))
RETURNS @output TABLE (
	[splitdata] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) WITH EXECUTE AS CALLER
AS 
BEGIN 
    DECLARE @start INT, @end INT 
    SELECT @start = 1, @end = CHARINDEX(@delimiter, @string) 
    WHILE @start < LEN(@string) + 1 BEGIN 
        IF @end = 0  
            SET @end = LEN(@string) + 1
       
        INSERT INTO @output (splitdata)  
        VALUES(SUBSTRING(@string, @start, @end - @start)) 
        SET @start = @end + 1 
        SET @end = CHARINDEX(@delimiter, @string, @start)
        
    END 
    RETURN 
END


