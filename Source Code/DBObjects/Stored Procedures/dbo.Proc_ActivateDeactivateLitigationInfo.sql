IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateLitigationInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateLitigationInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

            
 /*----------------------------------------------------------                                                          
Proc Name             : Dbo.Proc_ActivateDeactivateLitigationInfo                                                         
Created by            : Santosh Kumar Gautam                                                         
Date                  : 18 March 2011                                                    
Purpose               : To activate/deactivate the Litigation information                
Revison History       :                                                          
Used In               : CLAIM module                
------------------------------------------------------------                                                          
Date     Review By          Comments                             
                    
drop Proc Proc_ActivateDeactivateLitigationInfo                                                 
------   ------------       -------------------------*/        
CREATE PROC [dbo].[Proc_ActivateDeactivateLitigationInfo]        
(        
 @LITIGATION_ID     int                     
,@CLAIM_ID        int
,@IS_ACTIVE    char(1)    
)        
AS        
BEGIN    
  
 UPDATE CLM_LITIGATION_INFORMATION   
 SET IS_ACTIVE=@IS_ACTIVE  
 WHERE CLAIM_ID=@CLAIM_ID AND LITIGATION_ID=@LITIGATION_ID
  
  
END      
    
    
    
GO

