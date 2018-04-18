IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_GetClaimLossDescription]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_GetClaimLossDescription]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                      
Function Name	: dbo.fun_GetClaimLossDescription
CREATED BY      : Asfa Praveen        
DATE            : 30 june 2008    
PURPOSE         : TO GET THE Loss Type Description of a Claim                                       
REVISON HISTORY :                                      
USED IN         : WOLVERINE 
------------------------------------------------------------                                      
*/    
--select dbo.fun_GetClaimLossDescription('27,48,49,')                         
-- drop function dbo.fun_GetClaimLossDescription            
CREATE function dbo.fun_GetClaimLossDescription                                
(                               
 @LOSS_TYPE NVARCHAR(300)
)         RETURNS NVARCHAR(1000)    
    
AS            
BEGIN      
DECLARE @DETAIL_TYPE_DESCRIPTION NVARCHAR(100)   
DECLARE @RETURN_STRING NVARCHAR(1000)

SET @DETAIL_TYPE_DESCRIPTION = ''
SET @RETURN_STRING = ''
IF @LOSS_TYPE='' OR @LOSS_TYPE IS NULL
	RETURN ''

DECLARE CUR CURSOR      
FOR SELECT DETAIL_TYPE_DESCRIPTION FROM CLM_TYPE_DETAIL 
WHERE DBO.INSTRING(REPLACE((@LOSS_TYPE),',',' '),DETAIL_TYPE_ID)>0

OPEN CUR      
FETCH NEXT FROM CUR INTO @DETAIL_TYPE_DESCRIPTION
WHILE @@FETCH_STATUS = 0      
  BEGIN  
	SET @RETURN_STRING = @RETURN_STRING + @DETAIL_TYPE_DESCRIPTION + '<br>'
	FETCH NEXT FROM CUR INTO @DETAIL_TYPE_DESCRIPTION
  END      
CLOSE CUR      
DEALLOCATE CUR    

SET @RETURN_STRING = SUBSTRING(@RETURN_STRING,0, LEN(@RETURN_STRING))
RETURN @RETURN_STRING

END 
       


GO

