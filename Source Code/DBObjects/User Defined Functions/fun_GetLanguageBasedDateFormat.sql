GO

/****** Object:  UserDefinedFunction [dbo].[fun_GetLanguageBasedDateFormat]    Script Date: 08/17/2011 12:29:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_GetLanguageBasedDateFormat]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_GetLanguageBasedDateFormat]
GO

/****** Object:  UserDefinedFunction [dbo].[fun_GetLanguageBasedDateFormat]    Script Date: 08/17/2011 12:29:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE function [dbo].[fun_GetLanguageBasedDateFormat]                            
(                                        
                                  
 @DATE DATE,
 @LANG_ID smallint =1
) returns NVARCHAR(50)      
      
AS              
BEGIN    
declare @FORMATEDDATE  NVARCHAR(50)
	IF(@LANG_ID = 1)
		BEGIN 
			SET @FORMATEDDATE = convert(NVARCHAR(50),@DATE,101)		--CAST(convert(NVARCHAR(50),@DATE,101) AS DATE)
		END
	ELSE IF(@LANG_ID = 2)
		BEGIN
			SET @FORMATEDDATE = convert(NVARCHAR(50),@DATE,103)		--CAST(convert(NVARCHAR(50),@DATE,103) AS DATE)
		END  
RETURN @FORMATEDDATE
END  

GO

