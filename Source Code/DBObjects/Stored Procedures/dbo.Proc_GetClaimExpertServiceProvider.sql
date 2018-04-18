IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimExpertServiceProvider]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimExpertServiceProvider]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                                      
Proc Name             : Dbo.Proc_GetClaimExpertServiceProvider                                                 
Created by            : Santosh Kumar Gautam                                                     
Date                  : 23/11/2010                                                     
Purpose               : SELECT SERVICE PROVIDER FOR EXPERT TYPE IS Insured’s Attorney OR Claimant’s Attorney         
Revison History       :                                                      
Used In               : claim module            
------------------------------------------------------------                                                      
Date     Review By          Comments                         
                
drop Proc_GetClaimExpertServiceProvider                                             
------   ------------       -------------------------*/                                                      
--                         
                          
--                       
                    
CREATE PROCEDURE [dbo].[Proc_GetClaimExpertServiceProvider]   
AS                          
BEGIN               
              
 -- SELECT SERVICE PROVIDER FOR EXPERT TYPE IS Insured’s Attorney OR Claimant’s Attorney
 -- EXPERT TYPE DEFINED IN CLM_TYPE_DETAIL TABLE
 SELECT EXPERT_SERVICE_ID,EXPERT_SERVICE_NAME 
 FROM CLM_EXPERT_SERVICE_PROVIDERS 
 WHERE EXPERT_SERVICE_TYPE IN(160,161) AND IS_ACTIVE='Y'
   
              
END 
GO

