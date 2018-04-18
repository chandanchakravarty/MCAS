IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ACTIVITY_RESERVE_UMB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ACTIVITY_RESERVE_UMB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                  
Proc Name       : dbo.Proc_GetCLM_ACTIVITY_RESERVE_UMB                                  
Created by      : Sumit Chhabra                                  
Date            : 20/11/2006                                  
Purpose         : Get coverages for umbrella LOB for claims            
Created by      : Sumit Chhabra                                  
Revison History :                                  
Used In        : Wolverine                                  
------------------------------------------------------------                                  
Date     Review By          Comments                                  
------   ------------       -------------------------*/                                  
-- DROP PROC dbo.Proc_GetCLM_ACTIVITY_RESERVE_UMB                                  
CREATE PROC dbo.Proc_GetCLM_ACTIVITY_RESERVE_UMB                                  
@CLAIM_ID int            
AS            
BEGIN            
            
 SELECT         
 --Dummy Columns only for data-grid        
  CAR.PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING, CAR.RI_RESERVE, CAR.RESERVE_ID,        
  CAR.REINSURANCE_CARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,
 MC.COV_DES AS COV_DESC,CAR.COVERAGE_ID AS COV_ID,CAR.POLICY_LIMITS,CAR.RETENTION_LIMITS        
 FROM           
  CLM_ACTIVITY_RESERVE CAR    
 LEFT OUTER JOIN    
  CLM_CLAIM_INFO CCI          
 ON    
  CAR.CLAIM_ID=CCI.CLAIM_ID     
 LEFT OUTER JOIN          
  MNT_COVERAGE MC          
 ON          
  CAR.COVERAGE_ID = MC.COV_ID
 LEFT OUTER JOIN
  MNT_LOOKUP_VALUES MLV 
 ON
  MLV.LOOKUP_UNIQUE_ID =  CAR.REINSURANCE_CARRIER         
 WHERE          
  CCI.IS_ACTIVE='Y' AND     
  CAR.IS_ACTIVE='Y' AND          
  MC.IS_ACTIVE='Y' AND          
  MLV.IS_ACTIVE='Y' AND
  CAR.CLAIM_ID=@CLAIM_ID          
 ORDER BY        
  CAR.RESERVE_ID     
            
END        
      
    
  



GO

