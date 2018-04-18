IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_RESERVE_TRANSACTION_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_RESERVE_TRANSACTION_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       :  dbo.Proc_GetCLM_RESERVE_TRANSACTION_ID                
Created by      :  Asfa Praveen    
Date            :  12/Oct/2007                
Purpose         :  To generate Transaction ID corresponding to claim_id, activity_id    
Revison History :                
Used In         :   Wolverine                
-------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- DROP PROC dbo.Proc_GetCLM_RESERVE_TRANSACTION_ID             
CREATE proc dbo.Proc_GetCLM_RESERVE_TRANSACTION_ID                
@CLAIM_ID INT,                
@ACTIVITY_ID INT  
AS                
BEGIN 
IF(@ACTIVITY_ID = 1)  
BEGIN
 SET @ACTIVITY_ID = 0
END
IF EXISTS(SELECT TRANSACTION_ID FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID)               
BEGIN  
 SELECT TOP 1 ISNULL(TRANSACTION_ID,0) AS TRANSACTION_ID FROM CLM_ACTIVITY_RESERVE     
 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  
END  
ELSE  
BEGIN  
 SELECT ISNULL(MAX(TRANSACTION_ID),0)+1 AS TRANSACTION_ID FROM CLM_ACTIVITY_RESERVE     
 WHERE CLAIM_ID=@CLAIM_ID   
END            
-- RETURN  @TRANSACTION_ID           
            
end         
        
      
    
  




GO

