IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Func_IsNumeric]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Func_IsNumeric]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name       : Dbo.Func_IsNumeric            
Created by      : Mohit Agarwal            
Date            :            
Purpose         : Checks a string for non digit character           
Revison History :            
Used In        : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/       
--drop FUNCTION dbo.Func_IsNumeric         
CREATE FUNCTION dbo.Func_IsNumeric   
(  
@HOUSE_NO VARCHAR(100)  
)  
RETURNS INT  
AS  
BEGIN  
DECLARE @INDEX INT  
DECLARE @CHARINDEX VARCHAR(1)  
  
SET @INDEX = 1  
WHILE(@INDEX <= LEN(@HOUSE_NO))  
BEGIN  
   SET @CHARINDEX = SUBSTRING(@HOUSE_NO,@INDEX,1)  
--   print @CHARINDEX  
   IF (@CHARINDEX != '9' AND @CHARINDEX != '8' AND @CHARINDEX != '7' AND @CHARINDEX != '6' AND @CHARINDEX != '5'  
   AND @CHARINDEX != '4' AND @CHARINDEX != '3' AND @CHARINDEX != '2' AND @CHARINDEX != '1' AND @CHARINDEX != '0')  
      RETURN 0  
   SET @INDEX = @INDEX + 1  
END  
RETURN 1      
END  


GO

