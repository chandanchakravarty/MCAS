IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_GetClaimDriverName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_GetClaimDriverName]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                        
Function Name : dbo.fun_GetClaimDriverName  
CREATED BY      : Asfa Praveen          
DATE            : 30 june 2008      
PURPOSE         : TO GET THE Name of Driver added at Claim  
REVISON HISTORY :                                        
USED IN         : WOLVERINE   
------------------------------------------------------------                                        
*/      
--select dbo.fun_GetClaimDriverName(748)                           
-- drop function dbo.fun_GetClaimDriverName              
CREATE function dbo.fun_GetClaimDriverName                                  
(                                 
 @CLAIM_ID INT  
)         RETURNS VARCHAR(8000)      
      
AS              
BEGIN        
DECLARE @DRIVER_NAME VARCHAR(8000)     
DECLARE @RETURN_STRING VARCHAR(8000)  
  
SET @DRIVER_NAME = ''  
SET @RETURN_STRING = ''  
  
DECLARE CUR CURSOR        
FOR SELECT NAME AS DRIVER_NAME  
FROM CLM_CLAIM_INFO CCI  
LEFT JOIN CLM_DRIVER_INFORMATION CDI ON CDI.CLAIM_ID=CCI.CLAIM_ID AND CDI.IS_ACTIVE='Y'   
WHERE CCI.CLAIM_ID = @CLAIM_ID  
  
OPEN CUR        
FETCH NEXT FROM CUR INTO @DRIVER_NAME  
WHILE @@FETCH_STATUS = 0        
  BEGIN    
 SET @RETURN_STRING = @RETURN_STRING + @DRIVER_NAME + ';<br>'  
 FETCH NEXT FROM CUR INTO @DRIVER_NAME  
  END        
CLOSE CUR        
DEALLOCATE CUR      
  
SET @RETURN_STRING = SUBSTRING(@RETURN_STRING,0, LEN(@RETURN_STRING)-4)  
RETURN @RETURN_STRING  
  
END   
GO

