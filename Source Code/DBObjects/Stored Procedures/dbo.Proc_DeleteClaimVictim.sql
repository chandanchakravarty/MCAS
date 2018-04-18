IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteClaimVictim]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteClaimVictim]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                                                
Proc Name             : Dbo.Proc_DeleteClaimVictim                                                                
Created by            : Santosh Kumar Gautam                                                               
Date                  : 09 Feb 2011                                                              
Purpose               : To detete vicim from claim            
Revison History       :                                                                
Used In               : claim module                      
------------------------------------------------------------                                                                
Date     Review By          Comments                                   
                          
drop Proc Proc_DeleteClaimVictim                                                       
------   ------------       -------------------------*/                                                                
--                                   
                                    
--                                 
                              
CREATE PROCEDURE [dbo].[Proc_DeleteClaimVictim]    
@VICTIM_ID INT  ,  
@CLAIM_ID INT ,  
@ERROR_CODE INT OUT  
  
AS                                    
BEGIN                         
        
  ------------------------------------------------------
  -- RESERVE IS CREATED SO NO MORE COVERAGE CAN ADDED
  ------------------------------------------------------
  IF(EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY_RESERVE WITH(NOLOCK) WHERE CLAIM_ID= @CLAIM_ID))       
  BEGIN 
   
	  SET @ERROR_CODE=2 
	  RETURN  
	  
  END 
    
  IF NOT EXISTS(SELECT CLAIM_ID FROM CLM_PRODUCT_COVERAGES WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND VICTIM_ID=@VICTIM_ID)  
  BEGIN  
      
    DELETE FROM CLM_VICTIM_INFO
    WHERE  CLAIM_ID=@CLAIM_ID AND VICTIM_ID=@VICTIM_ID
    SET @ERROR_CODE=0  
      
      
  END  
  ELSE  
   SET @ERROR_CODE=1  
    
     
    
END 
GO

