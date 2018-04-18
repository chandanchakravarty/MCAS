IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ACTIVITY_STATUS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ACTIVITY_STATUS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       :  dbo.Proc_GetCLM_ACTIVITY_STATUS                
Created by      :  Asfa Praveen    
Date            :  18/Oct/2007                
Purpose         :  To fetch Activity Status
Revison History :                
Used In         :   Wolverine                
-------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- DROP PROC dbo.Proc_GetCLM_ACTIVITY_STATUS             
CREATE proc dbo.Proc_GetCLM_ACTIVITY_STATUS                
@CLAIM_ID INT,                
@ACTIVITY_ID INT  
AS                
BEGIN 
  IF(@ACTIVITY_ID = 0)  
    BEGIN
      SET @ACTIVITY_ID = 1
    END

SELECT ACTIVITY_STATUS FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID               

SELECT CLAIM_STATUS FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID

END         
        
      
    
  







GO

