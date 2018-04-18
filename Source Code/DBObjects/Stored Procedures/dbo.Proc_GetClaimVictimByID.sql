IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimVictimByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimVictimByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

        
 /*----------------------------------------------------------                                                                
Proc Name             : Dbo.Proc_GetClaimVictimByID                                                                
Created by            : Santosh Kumar Gautam                                                               
Date                  : 09/02/2011                                                               
Purpose               : To fetch the claim victim details                  
Revison History       :                                                                
Used In               : claim module                      
------------------------------------------------------------                                                                
Date     Review By          Comments                                   
                          
drop Proc Proc_GetClaimVictimByID                                                      
------   ------------       -------------------------*/                                                                
--                                   
                         
                              
CREATE PROCEDURE [dbo].[Proc_GetClaimVictimByID]                                    
                                   
                   
 @CLAIM_ID        int                      
,@VICTIM_ID       int             
                                    
AS                                    
BEGIN                         
      
SELECT 
		VICTIM_ID,
		CLAIM_ID,
		NAME,
		[STATUS],
		INJURY_TYPE,
		IS_ACTIVE,  
		CREATED_BY,  
		CREATED_DATETIME,  
		MODIFIED_BY,  
		LAST_UPDATED_DATETIME,
		'' AS PAGE_MODE		         
  FROM CLM_VICTIM_INFO
  WHERE (CLAIM_ID=@CLAIM_ID AND VICTIM_ID=@VICTIM_ID AND  IS_ACTIVE= 'Y' )       
    
        
     
END 
GO

