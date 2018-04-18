IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHomeRentalCoveragesForClaimsReserve]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHomeRentalCoveragesForClaimsReserve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
      
--drop proc Proc_GetHomeRentalCoveragesForClaimsReserve    
/*----------------------------------------------------------                       
Proc Name   : dbo.Proc_GetHomeRentalCoveragesForClaimsReserve                      
Created by  : Sumit Chhabra                                    
Date        : 29 May,2006                                    
Purpose     : Get the coverages for Claims Reserves          
Revison History  :                                           
 ------------------------------------------------------------                                                          
Date     Review By          Comments                                                        
------   ------------       -------------------------*/                   
CREATE PROCEDURE dbo.Proc_GetHomeRentalCoveragesForClaimsReserve          
(                                    
 @CLAIM_ID INT                     
)                                  
AS                                 
BEGIN     
DECLARE @CUSTOMER_ID int    
DECLARE @POLICY_ID int      
DECLARE @POLICY_VERSION_ID int    
DECLARE @LOB_ID INT    
DECLARE @SECTION1_COVERAGES varchar(5)               
DECLARE @SECTION2_COVERAGES varchar(5)               
    
set @SECTION1_COVERAGES = 'S1'    
set @SECTION2_COVERAGES = 'S2'    
    
 SELECT                     
  @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID                    
 FROM                    
  CLM_CLAIM_INFO                    
 WHERE                    
  CLAIM_ID=@CLAIM_ID              
     
 IF(@CUSTOMER_ID=0 OR @POLICY_ID=0 OR @POLICY_VERSION_ID=0)                    
  RETURN -1              
     
 SELECT    
  @LOB_ID=POLICY_LOB               
 FROM              
  POL_CUSTOMER_POLICY_LIST               
 WHERE              
  CUSTOMER_ID=@CUSTOMER_ID AND              
  POLICY_ID = @POLICY_ID AND              
  POLICY_VERSION_ID= @POLICY_VERSION_ID          
     
--SELECT SECTION - I COVERAGES    
 SELECT              
  --Dummy columns only for grid-binding              
  ''  AS PRIMARY_EXCESS,'' AS ATTACHMENT_POINT,'' AS OUTSTANDING,'' AS RI_RESERVE,'' AS RESERVE_ID,          
  '' AS REINSURANCE_CARRIER,'' AS MCCA_ATTACHMENT_POINT,'' AS MCCA_APPLIES,          
  AVC.DWELLING_ID, C.COV_ID, CAST(AVC.DWELLING_ID AS VARCHAR(10)) AS DWELLING,                                          
  C.COV_CODE,                                                    
  COV_DES as COV_DESC ,                             
  CASE C.LIMIT_TYPE                           
  WHEN 2 THEN--CONVERT(VARCHAR(20),AVC.LIMIT_1) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT1_AMOUNT_TEXT,''))                           
  Substring(convert(varchar(30),convert(money,avc.LIMIT_1),1),0,charindex('.',convert(varchar(30),convert(money,avc.LIMIT_1),1),0)) +                        
  CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,''))            + '/' +                         
  Substring(convert(varchar(30),convert(money,avc.LIMIT_2),1),0,charindex('.',convert(varchar(30),convert(money,avc.LIMIT_1),1),0)) +                        
  CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,''))                          
  ELSE Substring(convert(varchar(30),convert(money,avc.LIMIT_1),1),0,charindex('.',convert(varchar(30),convert(money,avc.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT1_AMOUNT_TEXT,''))                          
  END AS LIMIT,                          
  CASE C.DEDUCTIBLE_TYPE           
  WHEN 2 THEN Substring(convert(varchar(30),convert(money,avc.Deductible_1),1),0,charindex('.',convert(varchar(30),convert(money,avc.Deductible_1),1),0)) + ' ' +               
  + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'')) + '/' +                          
  Substring(convert(varchar(30),convert(money,avc.Deductible_2),1),0,charindex('.',convert(varchar(30),convert(money,avc.Deductible_2),1),0))  + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,''))                         
  ELSE  Substring(convert(varchar(30),convert(money,avc.Deductible_1),1),0,charindex('.',convert(varchar(30),convert(money,avc.Deductible_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,''))   
  END AS DEDUCTIBLE,
  ISNULL(CAST(avc.DEDUCTIBLE AS VARCHAR(10)),'') + ISNULL(avc.DEDUCTIBLE_TEXT,'') AS DEDUCTIBLE2                                                                               
 FROM                     
  POL_DWELLING_SECTION_COVERAGES AVC                                          
 INNER JOIN                     
  MNT_COVERAGE C          
 ON                       
  AVC.COVERAGE_CODE_ID = C.COV_ID       
 LEFT JOIN     
  CLM_CLAIM_INFO CCI    
 ON    
  CCI.CUSTOMER_ID = AVC.CUSTOMER_ID AND    
  CCI.POLICY_ID = AVC.POLICY_ID AND    
  CCI.POLICY_VERSION_ID = AVC.POLICY_VERSION_ID                                  
 WHERE               
  CCI.CLAIM_ID = @CLAIM_ID AND    
  C.COVERAGE_TYPE=@SECTION1_COVERAGES AND                                          
  DWELLING_ID IN                     
  (SELECT                    
   PDI.DWELLING_ID                   
  FROM                     
   POL_LOCATIONS PL    
  JOIN    
   POL_DWELLINGS_INFO PDI    
  ON     
   PL.CUSTOMER_ID = PDI.CUSTOMER_ID AND    
   PL.POLICY_ID = PDI.POLICY_ID AND    
   PL.POLICY_VERSION_ID = PDI.POLICY_VERSION_ID AND    
   PL.LOCATION_ID = PDI.LOCATION_ID     
  LEFT JOIN    
   CLM_INSURED_LOCATION CIL    
  ON    
   PL.LOCATION_ID = CIL.POLICY_LOCATION_ID    
  WHERE                    
   PDI.CUSTOMER_ID=@CUSTOMER_ID AND                    
   PDI.POLICY_ID = @POLICY_ID AND                    
   PDI.POLICY_VERSION_ID= @POLICY_VERSION_ID AND    
   PDI.IS_ACTIVE='Y' AND    
   PL.IS_ACTIVE='Y' AND    
   CIL.CLAIM_ID = @CLAIM_ID     
  )          
  ORDER BY AVC.DWELLING_ID,AVC.COVERAGE_CODE_ID    
    
--SELECT SECTION - II COVERAGES    
 SELECT              
  --Dummy columns only for grid-binding              
  ''  AS PRIMARY_EXCESS,'' AS ATTACHMENT_POINT,'' AS OUTSTANDING,'' AS RI_RESERVE,'' AS RESERVE_ID,          
  '' AS REINSURANCE_CARRIER,'' AS MCCA_ATTACHMENT_POINT,'' AS MCCA_APPLIES,          
  AVC.DWELLING_ID, C.COV_ID, CAST(AVC.DWELLING_ID AS VARCHAR(10)) AS DWELLING,                                         
  C.COV_CODE,                                                    
  COV_DES as COV_DESC ,                             
  CASE C.LIMIT_TYPE                           
  WHEN 2 THEN--CONVERT(VARCHAR(20),AVC.LIMIT_1) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT1_AMOUNT_TEXT,''))                           
  Substring(convert(varchar(30),convert(money,avc.LIMIT_1),1),0,charindex('.',convert(varchar(30),convert(money,avc.LIMIT_1),1),0)) +                        
  CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,''))            + '/' +                         
  Substring(convert(varchar(30),convert(money,avc.LIMIT_2),1),0,charindex('.',convert(varchar(30),convert(money,avc.LIMIT_1),1),0)) +                        
  CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,''))                          
  ELSE Substring(convert(varchar(30),convert(money,avc.LIMIT_1),1),0,charindex('.',convert(varchar(30),convert(money,avc.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT1_AMOUNT_TEXT,''))                          
  END AS LIMIT,                          
  CASE C.DEDUCTIBLE_TYPE           
  WHEN 2 THEN Substring(convert(varchar(30),convert(money,avc.Deductible_1),1),0,charindex('.',convert(varchar(30),convert(money,avc.Deductible_1),1),0)) + ' ' +               
  + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'')) + '/' +                          
  Substring(convert(varchar(30),convert(money,avc.Deductible_2),1),0,charindex('.',convert(varchar(30),convert(money,avc.Deductible_2),1),0))  + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,''))                          
  ELSE Substring(convert(varchar(30),convert(money,avc.Deductible_1),1),0,charindex('.',convert(varchar(30),convert(money,avc.Deductible_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,''))                          
  END AS DEDUCTIBLE,
  ISNULL(CAST(avc.DEDUCTIBLE AS VARCHAR(10)),'') + ISNULL(avc.DEDUCTIBLE_TEXT,'') AS DEDUCTIBLE2                                                                                                    
 FROM                     
  POL_DWELLING_SECTION_COVERAGES AVC                                          
 INNER JOIN                     
  MNT_COVERAGE C          
 ON                       
  AVC.COVERAGE_CODE_ID = C.COV_ID      
 LEFT JOIN     
  CLM_CLAIM_INFO CCI    
 ON    
  CCI.CUSTOMER_ID = AVC.CUSTOMER_ID AND    
  CCI.POLICY_ID = AVC.POLICY_ID AND    
  CCI.POLICY_VERSION_ID = AVC.POLICY_VERSION_ID                                  
 WHERE               
  CCI.CLAIM_ID = @CLAIM_ID AND    
  C.COVERAGE_TYPE=@SECTION2_COVERAGES AND                                          
  DWELLING_ID IN                     
  (SELECT                    
   PDI.DWELLING_ID                   
  FROM                     
   POL_LOCATIONS PL    
  JOIN    
   POL_DWELLINGS_INFO PDI    
  ON     
   PL.CUSTOMER_ID = PDI.CUSTOMER_ID AND    
   PL.POLICY_ID = PDI.POLICY_ID AND    
   PL.POLICY_VERSION_ID = PDI.POLICY_VERSION_ID AND    
   PL.LOCATION_ID = PDI.LOCATION_ID     
  LEFT JOIN    
   CLM_INSURED_LOCATION CIL    
  ON    
   PL.LOCATION_ID = CIL.POLICY_LOCATION_ID    
  WHERE                    
   PDI.CUSTOMER_ID=@CUSTOMER_ID AND                    
   PDI.POLICY_ID = @POLICY_ID AND                    
   PDI.POLICY_VERSION_ID= @POLICY_VERSION_ID AND    
   PDI.IS_ACTIVE='Y' AND    
   PL.IS_ACTIVE='Y' AND    
   CIL.CLAIM_ID = @CLAIM_ID    
  )                 
  ORDER BY AVC.DWELLING_ID,AVC.COVERAGE_CODE_ID    
    
END    
    
    
  



GO

