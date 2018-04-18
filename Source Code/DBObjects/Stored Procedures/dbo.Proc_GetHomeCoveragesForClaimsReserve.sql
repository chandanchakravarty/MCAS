IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHomeCoveragesForClaimsReserve]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHomeCoveragesForClaimsReserve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN
--drop proc Proc_GetHomeCoveragesForClaimsReserve
--GO
-----------------------------------------------------------                
--drop proc Proc_GetHomeCoveragesForClaimsReserve                      
/*----------------------------------------------------------                                         
Proc Name   : dbo.Proc_GetHomeCoveragesForClaimsReserve                                        
Created by  : Sumit Chhabra                                                      
Date        : 27 July,2006                                                      
Purpose     : Get the Home coverages for Claims Reserves                            
Revison History  :                                                             
 ------------------------------------------------------------                                                                            
Date     Review By          Comments                                                                          
------   ------------       -------------------------*/     
CREATE proc dbo.Proc_GetHomeCoveragesForClaimsReserve                                     
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
  @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID,@LOB_ID=LOB_ID                
 FROM                                                
  CLM_CLAIM_INFO                                                
 WHERE                                                
  CLAIM_ID=@CLAIM_ID                                          
                                 
 IF(@CUSTOMER_ID=0 OR @POLICY_ID=0 OR @POLICY_VERSION_ID=0)                                                
  RETURN -1                                          
                            
--SELECT SECTION - I COVERAGES                                
 SELECT                                          
  --Dummy columns only for grid-binding                                          
  ''  AS PRIMARY_EXCESS,'' AS ATTACHMENT_POINT,'' AS OUTSTANDING,'' AS RI_RESERVE,'' AS RESERVE_ID,                                      
  '' AS REINSURANCE_CARRIER,'' AS MCCA_ATTACHMENT_POINT,'' AS MCCA_APPLIES,                                      
  AVC.DWELLING_ID, C.COV_ID,                 
 --CAST(AVC.DWELLING_ID AS VARCHAR(10)) AS DWELLING,                                                                      
 ISNULL(CAST(PDI.DWELLING_NUMBER AS VARCHAR),'') + '-' + ISNULL(PL.LOC_ADD1,'')  + '-' +  ISNULL(PL.LOC_ADD2,'')                    
 + '-' +  ISNULL(PL.LOC_CITY,'')  + '-' +  ISNULL(MCSL.STATE_NAME,'') + '-' + ISNULL(PL.LOC_ZIP,'') AS DWELLING,                
  C.COV_CODE,                                                                  
  COV_DES as COV_DESC ,                           
  CASE C.LIMIT_TYPE                                                       
  WHEN 2 THEN--CONVERT(VARCHAR(20),AVC.LIMIT_1) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT1_AMOUNT_TEXT,''))                                                       
  Substring(convert(varchar(30),convert(money,AVC.LIMIT_1),1),0,charindex('.',convert(varchar(30),convert(money,AVC.LIMIT_1),1),0)) +                                                    
  CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,''))            + '/' +                                                     
  Substring(convert(varchar(30),convert(money,AVC.LIMIT_2),1),0,charindex('.',convert(varchar(30),convert(money,AVC.LIMIT_1),1),0)) +                                                    
  CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,''))                                                      
  ELSE Substring(convert(varchar(30),convert(money,AVC.LIMIT_1),1),0,charindex('.',convert(varchar(30),convert(money,AVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT1_AMOUNT_TEXT,''))                                                      
  END AS LIMIT,                                                      
  CASE C.DEDUCTIBLE_TYPE                                       
  WHEN 2 THEN Substring(convert(varchar(30),convert(money,AVC.Deductible_1),1),0,charindex('.',convert(varchar(30),convert(money,AVC.Deductible_1),1),0)) + ' ' +                                           
  + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'')) + '/' +                                     
  Substring(convert(varchar(30),convert(money,AVC.Deductible_2),1),0,charindex('.',convert(varchar(30),convert(money,AVC.Deductible_2),1),0))  + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,''))                                                   


 
   
  ELSE  Substring(convert(varchar(30),convert(money,AVC.Deductible_1),1),0,charindex('.',convert(varchar(30),convert(money,AVC.Deductible_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,''))                               
  END AS DEDUCTIBLE,                            
  ISNULL(CAST(AVC.DEDUCTIBLE AS VARCHAR(10)),'') + ISNULL(AVC.DEDUCTIBLE_TEXT,'') AS DEDUCTIBLE2                                                                                                           
                
 FROM                                                 
  CLM_CLAIM_INFO CCI                 
 JOIN                 
  CLM_INSURED_LOCATION CIL                 
 ON                
  CCI.CLAIM_ID=CIL.CLAIM_ID                
 JOIN                 
  POL_DWELLINGS_INFO PDI                 
 ON                
  PDI.CUSTOMER_ID=CCI.CUSTOMER_ID AND                 
  PDI.POLICY_ID=CCI.POLICY_ID AND                 
  PDI.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND                 
  PDI.LOCATION_ID=CIL.POLICY_LOCATION_ID                
 JOIN                 
  POL_DWELLING_SECTION_COVERAGES AVC                
 ON                 
  AVC.CUSTOMER_ID=CCI.CUSTOMER_ID AND                 
  AVC.POLICY_ID=CCI.POLICY_ID AND                 
  AVC.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND                
  AVC.DWELLING_ID=PDI.DWELLING_ID                
 JOIN                 
  POL_LOCATIONS PL                
 ON                 
  PL.CUSTOMER_ID=CCI.CUSTOMER_ID AND                 
  PL.POLICY_ID=CCI.POLICY_ID AND                 
  PL.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND                
  CIL.POLICY_LOCATION_ID=PL.LOCATION_ID                
 JOIN                 
  MNT_COVERAGE C                
 ON                 
  C.COV_ID=AVC.COVERAGE_CODE_ID                
 JOIN                 
  MNT_COUNTRY_STATE_LIST MCSL                 
 ON                 
  MCSL.STATE_ID=PL.LOC_STATE                     
 WHERE                 
  CCI.CLAIM_ID=@CLAIM_ID AND                 
  C.COVERAGE_TYPE = @SECTION1_COVERAGES AND
  --Added for Itrack Issue 6635 on 27 Oct 08
  C.COV_ID NOT IN (928,981,983)                          
 ORDER BY                 
  AVC.DWELLING_ID,AVC.COVERAGE_CODE_ID                                
                
                                
--SELECT SECTION - II COVERAGES                                
 SELECT 
  --Dummy columns only for grid-binding                                          
  ''  AS PRIMARY_EXCESS,'' AS ATTACHMENT_POINT,'' AS OUTSTANDING,'' AS RI_RESERVE,'' AS RESERVE_ID,                                      
  '' AS REINSURANCE_CARRIER,'' AS MCCA_ATTACHMENT_POINT,'' AS MCCA_APPLIES,                                      
  AVC.DWELLING_ID, C.COV_ID, --CAST(AVC.DWELLING_ID AS VARCHAR(10)) AS DWELLING,                                                                     
  ISNULL(CAST(PDI.DWELLING_NUMBER AS VARCHAR),'') + '-' + ISNULL(PL.LOC_ADD1,'')  + '-' +  ISNULL(PL.LOC_ADD2,'')                    
  + '-' +  ISNULL(PL.LOC_CITY,'')  + '-' +  ISNULL(MCSL.STATE_NAME,'') + '-' + ISNULL(PL.LOC_ZIP,'') AS DWELLING,                
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
    
  CLM_CLAIM_INFO CCI                 
 JOIN                 
  CLM_INSURED_LOCATION CIL                 
 ON                
  CCI.CLAIM_ID=CIL.CLAIM_ID                
 JOIN                 
  POL_DWELLINGS_INFO PDI                 
 ON                
  PDI.CUSTOMER_ID=CCI.CUSTOMER_ID AND                 
  PDI.POLICY_ID=CCI.POLICY_ID AND                 
  PDI.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND                 
  PDI.LOCATION_ID=CIL.POLICY_LOCATION_ID                
 JOIN                 
  POL_DWELLING_SECTION_COVERAGES AVC                
 ON                 
  AVC.CUSTOMER_ID=CCI.CUSTOMER_ID AND           
  AVC.POLICY_ID=CCI.POLICY_ID AND                 
  AVC.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND                
  AVC.DWELLING_ID=PDI.DWELLING_ID                
 JOIN                 
  POL_LOCATIONS PL                
 ON                 
  PL.CUSTOMER_ID=CCI.CUSTOMER_ID AND                 
  PL.POLICY_ID=CCI.POLICY_ID AND                 
  PL.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND                
  CIL.POLICY_LOCATION_ID=PL.LOCATION_ID                
 JOIN                 
  MNT_COVERAGE C                
 ON                 
  C.COV_ID=AVC.COVERAGE_CODE_ID                
 JOIN                 
  MNT_COUNTRY_STATE_LIST MCSL                 
 ON                 
  MCSL.STATE_ID=PL.LOC_STATE                     
 WHERE                 
  CCI.CLAIM_ID=@CLAIM_ID AND                 
  C.COVERAGE_TYPE = @SECTION2_COVERAGES                  
 ORDER BY                 
  AVC.DWELLING_ID,AVC.COVERAGE_CODE_ID               
  
  
           

--Home Watercraft Coverages                      
 SELECT                       
  ''  AS PRIMARY_EXCESS,'' AS ATTACHMENT_POINT,'' AS OUTSTANDING,'' AS RI_RESERVE,'' AS RESERVE_ID,                       
  '' AS REINSURANCE_CARRIER,'' AS MCCA_ATTACHMENT_POINT,'' AS MCCA_APPLIES,                       
  AVC.BOAT_ID AS DWELLING_ID,C.COV_ID,C.COV_CODE,COV_DES AS COV_DESC,                       
  (CAST(CIV.YEAR AS VARCHAR)+'-'+CAST(CIV.MAKE AS VARCHAR)+'-'+CAST(CIV.MODEL AS VARCHAR)+'-'+CAST(CIV.SERIAL_NUMBER AS VARCHAR)) AS DWELLING,                        
  CASE C.LIMIT_TYPE  WHEN 2 THEN  SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),0,  CHARINDEX('.',CONVERT(VARCHAR(30), CONVERT(MONEY,AVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'')) + '/' +     
SUBSTRING(CONVERT(VARCHAR(30), CONVERT(MONEY,AVC.LIMIT_2),1),0,CHARINDEX('.',CONVERT(VARCHAR(30), CONVERT(MONEY,AVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,''))                        
  ELSE SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),0,CHARINDEX('.',  CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT1_AMOUNT_TEXT,''))                        
  END AS LIMIT,                        
  CASE C.DEDUCTIBLE_TYPE  WHEN 2 THEN SUBSTRING(CONVERT(VARCHAR(30), CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0,CHARINDEX('.',  CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0))+ ' ' + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'')) + '/' 
+

  SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),0))  + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,''))  ELSE SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,


   AVC.DEDUCTIBLE_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0)) + ' ' + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,''))                        
  END AS DEDUCTIBLE,                        
  COVERAGE_TYPE                        
 FROM                       
  POL_WATERCRAFT_COVERAGE_INFO  AVC                        
 INNER JOIN       
  MNT_COVERAGE C                        
 ON                         
  AVC.COVERAGE_CODE_ID = C.COV_ID                        
 LEFT OUTER JOIN                       
  CLM_INSURED_BOAT CIV                       
 ON                       
  AVC.BOAT_ID = CIV.POLICY_BOAT_ID                       
 WHERE                        
  CUSTOMER_ID = @CUSTOMER_ID AND                       
  POLICY_ID = @POLICY_ID AND                       
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND                       
  CIV.CLAIM_ID=@CLAIM_ID   


--Scheduled Item Coverages Grid   
/*  
SELECT P.ITEM_ID,    
SUM(ISNULL(D.ITEM_INSURING_VALUE,0)) AS SCHEDULED_ITEM_COVERAGE_AMOUNT,    
R.LIMIT_DEDUC_AMOUNT AS DEDUCTIBLE,    
C.COV_DES AS COV_DESC    
INTO #TEMP_SCH_COVG    
 FROM 
  POL_HOME_OWNER_SCH_ITEMS_CVGS P               
 LEFT JOIN               
  POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS D              
 ON   
  P.CUSTOMER_ID = D.CUSTOMER_ID AND               
  P.POLICY_ID = D.POL_ID AND               
  P.POLICY_VERSION_ID=D.POL_VERSION_ID AND               
  P.ITEM_ID=D.ITEM_ID              
LEFT JOIN               
  MNT_COVERAGE C               
 ON               
  P.ITEM_ID = C.COV_ID               
 LEFT JOIN               
  MNT_COVERAGE_RANGES R               
 ON               
  R.COV_ID = C.COV_ID AND               
  P.DEDUCTIBLE = R.LIMIT_DEDUC_ID         
 LEFT OUTER JOIN CLM_CLAIM_INFO CCI    
 ON      
  CCI.CUSTOMER_ID = P.CUSTOMER_ID AND               
  CCI.POLICY_ID = P.POLICY_ID AND               
  CCI.POLICY_VERSION_ID=P.POLICY_VERSION_ID        
 WHERE               
 CCI.CLAIM_ID=@CLAIM_ID AND     
  ISNULL(D.IS_ACTIVE,'Y')='Y'              
GROUP BY     
P.ITEM_ID,R.LIMIT_DEDUC_AMOUNT,C.COV_DES    
   
 
    
    
    
    
    
 SELECT              
  ''  AS PRIMARY_EXCESS,'' AS ATTACHMENT_POINT,'' AS OUTSTANDING,'' AS RI_RESERVE,'' AS RESERVE_ID,'' AS LIMIT,              
  '' AS DEDUCTIBLE2,              
  '' AS REINSURANCE_CARRIER,'' AS MCCA_ATTACHMENT_POINT,'' AS MCCA_APPLIES,'' AS DWELLING_ID,    
 #TEMP_SCH_COVG.ITEM_ID AS COV_ID,#TEMP_SCH_COVG.COV_DESC as COV_DESC,  
--C.COV_DES AS COV_DESC,    
--R.LIMIT_DEDUC_AMOUNT AS DEDUCTIBLE,    
--ISNULL(SUM(D.ITEM_INSURING_VALUE),0) AS SCHEDULED_ITEM_COVERAGE_AMOUNT     
    
D.ITEM_DESCRIPTION AS ITEM_DESCRIPTION,    
--#TEMP_SCH_COVG.COV_DESC,    
#TEMP_SCH_COVG.DEDUCTIBLE,#TEMP_SCH_COVG.SCHEDULED_ITEM_COVERAGE_AMOUNT              
  --C.COV_DES AS COV_DESC,R.LIMIT_DEDUC_AMOUNT AS DEDUCTIBLE,    
 -- ISNULL(SUM(D.ITEM_INSURING_VALUE),0) AS SCHEDULED_ITEM_COVERAGE_AMOUNT              
 FROM               
  POL_HOME_OWNER_SCH_ITEMS_CVGS P               
 LEFT JOIN               
  CLM_CLAIM_INFO CCI               
 ON              
  CCI.CUSTOMER_ID = P.CUSTOMER_ID AND               
  CCI.POLICY_ID = P.POLICY_ID AND               
  CCI.POLICY_VERSION_ID=P.POLICY_VERSION_ID              
 LEFT JOIN               
  POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS D              
 ON               
  P.CUSTOMER_ID = D.CUSTOMER_ID AND               
  P.POLICY_ID = D.POL_ID AND               
  P.POLICY_VERSION_ID=D.POL_VERSION_ID AND               
  P.ITEM_ID=D.ITEM_ID         
  LEFT JOIN #TEMP_SCH_COVG    
 ON     
  P.ITEM_ID = #TEMP_SCH_COVG.ITEM_ID    
 WHERE               
  CCI.CLAIM_ID=@CLAIM_ID     
order by cov_id  
 */
--Scheduled Item Coverages Grid  
exec Proc_GetExistingSchItemCovgForClaims @CLAIM_ID
    
      
/********************************************************************************************                     
--Scheduled Item Coverages Grid                        
                        
 SELECT                         
   --Dummy columns only for grid-binding                                          
  ''  AS PRIMARY_EXCESS,'' AS ATTACHMENT_POINT,'' AS OUTSTANDING,'' AS RI_RESERVE,'' AS RESERVE_ID,'' AS LIMIT,                        
  '' AS DEDUCTIBLE2,                        
  '' AS REINSURANCE_CARRIER,'' AS MCCA_ATTACHMENT_POINT,'' AS MCCA_APPLIES,'' AS DWELLING_ID,P.ITEM_ID AS COV_ID,                            
D.ITEM_DESCRIPTION,        
P.ITEM_ID AS COV_ID,                            
  C.COV_DES  AS COV_DESC,R.LIMIT_DEDUC_AMOUNT AS DEDUCTIBLE,D.ITEM_INSURING_VALUE AS SCHEDULED_ITEM_COVERAGE_AMOUNT        
--ISNULL(SUM(D.ITEM_INSURING_VALUE),0) AS SCHEDULED_ITEM_COVERAGE_AMOUNT                        
 FROM                         
  POL_HOME_OWNER_SCH_ITEMS_CVGS P                         
 LEFT JOIN                         
  CLM_CLAIM_INFO CCI                         
 ON                        
  CCI.CUSTOMER_ID = P.CUSTOMER_ID AND                         
  CCI.POLICY_ID = P.POLICY_ID AND                       
  CCI.POLICY_VERSION_ID=P.POLICY_VERSION_ID                        
 LEFT JOIN                         
  MNT_COVERAGE C                         
 ON                         
  P.ITEM_ID = C.COV_ID                         
 LEFT JOIN                         
  MNT_COVERAGE_RANGES R                         
 ON                         
  R.COV_ID = C.COV_ID AND                         
  P.DEDUCTIBLE = R.LIMIT_DEDUC_ID                         
 LEFT JOIN                         
  POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS D                        
 ON                         
  P.CUSTOMER_ID = D.CUSTOMER_ID AND                         
  P.POLICY_ID = D.POL_ID AND                         
  P.POLICY_VERSION_ID=D.POL_VERSION_ID AND                         
  P.ITEM_ID=D.ITEM_ID                        
 WHERE                         
  CCI.CLAIM_ID=573 AND                         
  ISNULL(D.IS_ACTIVE,'Y')='Y'                        
 GROUP BY                         
  C.COV_DES,R.LIMIT_DEDUC_AMOUNT,P.ITEM_ID,D.IS_ACTIVE,D.ITEM_DESCRIPTION,D.ITEM_INSURING_VALUE        
 ORDER BY                         
  C.COV_DES         
      
*/             
                    
END                        

--GO
--EXEC Proc_GetHomeCoveragesForClaimsReserve 2286
--ROLLBACK TRAN



GO

