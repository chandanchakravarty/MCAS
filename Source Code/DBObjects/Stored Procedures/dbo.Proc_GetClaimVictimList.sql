IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimVictimList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimVictimList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


      
 /*----------------------------------------------------------                                                              
Proc Name             : Dbo.Proc_GetClaimVictimList                                                              
Created by            : Santosh Kumar Gautam                                                             
Date                  : 09 Feb 2011                                                            
Purpose               : To fetch the claim Victim list                
Revison History       :                                                              
Used In               : claim module                    
------------------------------------------------------------                                                              
Date     Review By          Comments                                 
                        
drop Proc Proc_GetClaimVictimList                                                     
------   ------------       -------------------------*/                                                              
--                                 
                                  
--                               
                            
CREATE PROCEDURE [dbo].[Proc_GetClaimVictimList]                                  
                                 
                 
 @CLAIM_ID        int                               
                                  
AS                                  
BEGIN                       
      
 SELECT VICTIM_ID,NAME
 FROM CLM_VICTIM_INFO
 WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y'       
      
   
END 

GO

