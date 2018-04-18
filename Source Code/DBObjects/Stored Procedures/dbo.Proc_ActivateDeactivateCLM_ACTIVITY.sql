IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateCLM_ACTIVITY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateCLM_ACTIVITY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
Proc Name       : dbo.Proc_ActivateDeactivateCLM_ACTIVITY                                
Created by      : Sumit Chhabra                          
Date            : 06/07/2006                                
Purpose       : Performs various activites related to completing a claim activity                          
Revison History :                                
Used In  : Wolverine                                
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                                
--DROP PROC dbo.Proc_ActivateDeactivateCLM_ACTIVITY                
CREATE PROC [dbo].[Proc_ActivateDeactivateCLM_ACTIVITY]                                
(                                
 @CLAIM_ID     int,                                
 @ACTIVITY_ID  int,                        
 @ACTIVITY_STATUS  int,  
 @IS_ACTIVE char(1)  
)                                
AS                                
BEGIN    

-- ===============================================================================
-- ADDED BY SANTOSH KUMAR GAUTAM ON 28 APR 2011 FOR ITRACK 1158
-- ===============================================================================
-- IF USER WANT TO ACTIVE ANT ACTIVITY AND OTHER ACTIVITY IS CREATED AFTER THIS 
-- ACTIVITY THEN DO NOT ALLOW TO ACTIVE
-- ===============================================================================

IF (@IS_ACTIVE='Y' AND EXISTS( SELECT ACTIVITY_ID FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID>@ACTIVITY_ID))  
BEGIN
   RETURN -2
END
  
 UPDATE CLM_ACTIVITY_PAYMENT SET IS_ACTIVE=@IS_ACTIVE  
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID    
  
 UPDATE CLM_ACTIVITY_RESERVE SET IS_ACTIVE=@IS_ACTIVE  
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID    
  
 UPDATE CLM_ACTIVITY_EXPENSE SET IS_ACTIVE=@IS_ACTIVE  
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID    
  
 UPDATE CLM_ACTIVITY_RECOVERY SET IS_ACTIVE=@IS_ACTIVE  
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID    
  
 UPDATE CLM_ACTIVITY SET IS_ACTIVE=@IS_ACTIVE,ACTIVITY_STATUS = @ACTIVITY_STATUS   
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID    
  
  -- Added by Santosh Kumar Gautam on 08 Dec 2010 
  UPDATE CLM_ACTIVITY_CO_RI_BREAKDOWN SET IS_ACTIVE=@IS_ACTIVE
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID    
  
    -- Added by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008    
 IF (@IS_ACTIVE='N')  
 BEGIN  
  UPDATE CLM_ACTIVITY SET DEACTIVATED_DATE=GETDATE()  
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID    
 END  
END     
  
  
  
  
GO

