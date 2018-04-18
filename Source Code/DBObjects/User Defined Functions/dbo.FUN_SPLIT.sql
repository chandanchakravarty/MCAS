IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FUN_SPLIT]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FUN_SPLIT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                      
Function Name :  DBO.FUN_SPLIT                                   
CREATED BY      : PRAVEEN KUMAR         
DATE            : 2nd FEB 2009    
PURPOSE         : TO SPLIT any text                                       
REVISON HISTORY :                                      
USED IN         : WOLVERINE 
*/
--BEGIN TRAN

--GO

CREATE FUNCTION DBO.FUN_SPLIT
(
	@CNUMBER NVARCHAR(2000)
	
)  
RETURNS @RTNVALUE TABLE 
(
	CONTRACTNUMBER NVARCHAR(15)
) 
AS  
BEGIN 
	WHILE (CHARINDEX(',',@CNUMBER)>0)
	BEGIN
		INSERT INTO @RTNVALUE (CONTRACTNUMBER)
		SELECT 
			CONTRACTNUMBER = LTRIM(RTRIM(SUBSTRING(@CNUMBER,1,CHARINDEX(',',@CNUMBER)-1)))

		SET @CNUMBER = SUBSTRING(@CNUMBER,CHARINDEX(',',@CNUMBER)+1,LEN(@CNUMBER))
		
	END
	
	RETURN
END


GO

