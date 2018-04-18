IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimCoinsuranceDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimCoinsuranceDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                  
Proc Name             : Dbo.Proc_GetClaimCoinsuranceDetails                                                  
Created by            : Santosh Kumar Gautam                                                 
Date                  : 14 Dec 2010                                             
Purpose               : To get co-insurance details        
Revison History       :                                                  
Used In               : claim module        
------------------------------------------------------------                                                  
Date     Review By          Comments                     
            
drop Proc Proc_GetClaimCoinsuranceDetails   856                                      
------   ------------       -------------------------*/          
        
CREATE PROC [dbo].[Proc_GetClaimCoinsuranceDetails]          
        
@CLAIM_ID   int          
        
AS                                                                                    
BEGIN             
    
  SELECT [COINSURANCE_ID]
      ,[CLAIM_ID]
      ,[LEADER_SUSEP_CODE]
      ,[LEADER_POLICY_NUMBER]
      ,[LEADER_ENDORSEMENT_NUMBER]
      ,[LEADER_CLAIM_NUMBER]
      ,[CLAIM_REGISTRATION_DATE]
      ,[IS_ACTIVE]     
      ,[LITIGATION_FILE] -- ADDED BY SANTOSH KR GAUTAM ON 13 JULY(REF 1044)
  FROM [dbo].[CLM_CO_INSURANCE] WITH(NOLOCK)
  WHERE (CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y') 
  
  
 IF EXISTS( SELECT CLAIM_ID FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID )
  SELECT 'Y' AS IS_RESERVE_CREATED
 ELSE
  SELECT 'N' AS IS_RESERVE_CREATED
      
          
END          
        
        

GO

