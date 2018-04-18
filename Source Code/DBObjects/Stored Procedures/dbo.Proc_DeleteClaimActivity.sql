IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteClaimActivity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteClaimActivity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

                  
 /*----------------------------------------------------------                                                          
Proc Name             : Dbo.Proc_DeleteClaimActivity                                                          
Created by            : Santosh Kumar Gautam                                                         
Date                  : 04 Jan 2011                                                         
Purpose               : To delete activity data                                                     
Revison History       :                                                          
Used In               :                                          
------------------------------------------------------------                                                          
Date     Review By          Comments                             
                    
drop Proc Proc_DeleteClaimActivity                                                 
------   ------------       -------------------------*/                                                          
--                             
                              
--                           
                        
CREATE PROCEDURE [dbo].[Proc_DeleteClaimActivity]                              
                               
 @CLAIM_ID            INT                      
,@ACTIVITY_ID         INT      
  
AS                              
BEGIN                  
   
  
 -------------------------------------------------
 -- DELETE DATA FROM CLM_PAYEE
 -------------------------------------------------        
   DELETE FROM CLM_PAYEE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID    
   
-------------------------------------------------
 -- DELETE DATA FROM CLM_ACTIVITY_CO_RI_BREAKDOWN
 -------------------------------------------------        
   DELETE FROM CLM_ACTIVITY_CO_RI_BREAKDOWN WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID    
   
 -------------------------------------------------
 -- DELETE DATA FROM CLM_ACTIVITY_RESERVE
 -------------------------------------------------        
   DELETE FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID     
    
 -------------------------------------------------
 -- DELETE DATA FROM CLM_ACTIVITY
 -------------------------------------------------        
   DELETE FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID      
   
 
                      
                      
                    
END 
GO

