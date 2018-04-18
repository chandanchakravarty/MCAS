IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_Split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_Split]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
Created by  : Pravesh k Chandel  
Date  : 13 Aug 2007  
Purpose  : to Split a string and return values in a table  
drop FUNCTION dbo.func_Split
*/  
  
CREATE FUNCTION dbo.func_Split(  
    @sInputList VARCHAR(8000) -- List of delimited items  
  , @sDelimiter VARCHAR(8000) = ',' -- delimiter that separates items  
) RETURNS @List TABLE (item VARCHAR(8000))  
  
BEGIN  
DECLARE @sItem VARCHAR(8000)  
WHILE CHARINDEX(@sDelimiter,@sInputList,0) <> 0  
 BEGIN  
 SELECT  
  @sItem=RTRIM(LTRIM(SUBSTRING(@sInputList,1,CHARINDEX(@sDelimiter,@sInputList,0)-1))),  
  @sInputList=RTRIM(LTRIM(SUBSTRING(@sInputList,CHARINDEX(@sDelimiter,@sInputList,0)+LEN(@sDelimiter),LEN(@sInputList))))  
   
 IF LEN(@sItem) > 0  
  INSERT INTO @List SELECT @sItem  
 END  
  
IF LEN(@sInputList) > 0  
 INSERT INTO @List SELECT @sInputList -- Put the last item in  
RETURN  
END  


GO

