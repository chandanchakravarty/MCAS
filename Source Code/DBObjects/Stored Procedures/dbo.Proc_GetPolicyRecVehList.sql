IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyRecVehList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyRecVehList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_GetPolicyRecVehList                       
Created by      : Sumit Chhabra            
Date            : 11/08/2008                        
Purpose       : Get List of all recreational vehicles under the current policy    
Revison History :                        
Used In   : Wolverine                        
                      
            
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                        
--drop proc Proc_GetPolicyRecVehList                       
                      
CREATE PROC dbo.Proc_GetPolicyRecVehList                     
(                        
 @CLAIM_ID int,
 @REC_VEH_ID smallint = null                       
                                      
)                        
AS                      
BEGIN     
 if (@REC_VEH_ID is null or @REC_VEH_ID=0)
 begin                   
  SELECT     
  P.REC_VEH_ID AS POL_REC_VEH_ID,(ISNULL(CAST(P.YEAR AS VARCHAR),'') + '-' + ISNULL(P.MAKE,'') + '-' +     
  ISNULL(P.MODEL,'') + '-' + ISNULL(P.SERIAL,'')) AS REC_VEH     
 FROM      
  CLM_CLAIM_INFO CCI    
 LEFT OUTER JOIN    
  POL_HOME_OWNER_RECREATIONAL_VEHICLES P    
 ON    
  CCI.CUSTOMER_ID = P.CUSTOMER_ID AND    
  CCI.POLICY_ID = P.POLICY_ID AND    
  CCI.POLICY_VERSION_ID = P.POLICY_VERSION_ID    
 WHERE     
  ISNULL(CCI.IS_ACTIVE,'Y')='Y' AND    
  ISNULL(P.ACTIVE,'Y')='Y' AND    
  CCI.CLAIM_ID = @CLAIM_ID    
 end
 else
 begin
SELECT     
 P.REC_VEH_ID,P.COMPANY_ID_NUMBER,P.YEAR,P.MAKE,P.MODEL,P.SERIAL,P.STATE_REGISTERED,P.HORSE_POWER,
 P.REMARKS,P.VEHICLE_TYPE
 FROM      
  CLM_CLAIM_INFO CCI    
 LEFT OUTER JOIN    
  POL_HOME_OWNER_RECREATIONAL_VEHICLES P    
 ON    
  CCI.CUSTOMER_ID = P.CUSTOMER_ID AND    
  CCI.POLICY_ID = P.POLICY_ID AND    
  CCI.POLICY_VERSION_ID = P.POLICY_VERSION_ID    
 WHERE     
  ISNULL(CCI.IS_ACTIVE,'Y')='Y' AND    
  ISNULL(P.ACTIVE,'Y')='Y' AND    
  CCI.CLAIM_ID = @CLAIM_ID AND
  P.REC_VEH_ID = @REC_VEH_ID

 end
END  



GO

