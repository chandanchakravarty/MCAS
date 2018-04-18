IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUmbCoveragesForClaimsReserve]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUmbCoveragesForClaimsReserve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                            
Proc Name       : dbo.Proc_GetUmbCoveragesForClaimsReserve                            
Created by      : Sumit Chhabra                            
Date            : 20/11/2006                            
Purpose         : Get coverages for umbrella LOB for claims      
Created by      : Sumit Chhabra                            
Revison History :                            
Used In        : Wolverine                            
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
-- DROP PROC dbo.Proc_GetUmbCoveragesForClaimsReserve                            
CREATE PROC dbo.Proc_GetUmbCoveragesForClaimsReserve                            
@CLAIM_ID int      
AS      
BEGIN      
      
 SELECT     
 --Dummy Columns only for data-grid    
  '' AS PRIMARY_EXCESS,'' AS ATTACHMENT_POINT,'' AS OUTSTANDING, '' AS RI_RESERVE,'' AS RESERVE_ID,    
  '' AS REINSURANCE_CARRIER,'' AS MCCA_ATTACHMENT_POINT,'' AS MCCA_APPLIES,     
 MC.COV_DES AS COV_DESC,PUC.COVERAGE_CODE_ID AS COV_ID    
 FROM       
  CLM_CLAIM_INFO CCI      
 LEFT OUTER JOIN
  POL_UMBRELLA_COVERAGES PUC
 ON
  CCI.CUSTOMER_ID=PUC.CUSTOMER_ID AND
  CCI.POLICY_ID=PUC.POL_ID AND
  CCI.POLICY_VERSION_ID=PUC.POL_VERSION_ID
 LEFT OUTER JOIN      
  MNT_COVERAGE MC      
 ON      
  PUC.COVERAGE_CODE_ID = MC.COV_ID      
 WHERE      
  CCI.IS_ACTIVE='Y' AND      
  MC.IS_ACTIVE='Y' AND      
  CCI.CLAIM_ID=@CLAIM_ID      
 ORDER BY    
  MC.RANK
      
END  



GO

