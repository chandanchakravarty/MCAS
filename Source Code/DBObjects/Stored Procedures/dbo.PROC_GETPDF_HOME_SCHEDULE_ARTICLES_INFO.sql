IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETPDF_HOME_SCHEDULE_ARTICLES_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETPDF_HOME_SCHEDULE_ARTICLES_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop PROCEDURE dbo.PROC_GETPDF_HOME_SCHEDULE_ARTICLES_INFO 1692,80,1,'APPLICATION'     
 CREATE  PROCEDURE dbo.PROC_GETPDF_HOME_SCHEDULE_ARTICLES_INFO      
 @CUSTOMERID   int,      
 @POLID                int,      
 @VERSIONID   int,      
 @CALLEDFROM  VARCHAR(20)      
AS      
BEGIN   
     
 IF @CALLEDFROM='APPLICATION'      
 BEGIN      
 SELECT   
 SIC.ITEM_ID, MC.COV_CODE,MC.COV_DES,  
 ISNULL(CAST(MCR.LIMIT_DEDUC_AMOUNT*1.00 AS VARCHAR),'') LIMIT_DEDUC_AMOUNT,      
 (  
    SELECT ISNULL(CAST(SUM(ITEM_INSURING_VALUE) AS VARCHAR),'') FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS SICD WITH (NOLOCK)      
           WHERE SIC.ITEM_ID = SICD.ITEM_ID AND SIC.CUSTOMER_ID = SICD.CUSTOMER_ID AND SIC.APP_ID = SICD.APP_ID      
    AND SIC.APP_VERSION_ID = SICD.APP_VERSION_ID AND SICD.IS_ACTIVE='Y'   
 ) AS AMOUNT , --Removed *1.00 by Charles on 6-Oct-09 for Itrack 6488      
 MC.FORM_NUMBER AS FORM_NO, MC.COMPONENT_CODE      
 FROM APP_HOME_OWNER_SCH_ITEMS_CVGS SIC WITH (NOLOCK)      
 INNER JOIN MNT_COVERAGE MC WITH (NOLOCK) ON SIC.ITEM_ID = MC.COV_ID AND SIC.IS_ACTIVE = 'Y' --ITrack # 6140 Manoj      
 INNER JOIN MNT_COVERAGE_RANGES MCR WITH (NOLOCK) ON MCR.COV_ID = SIC.ITEM_ID AND      
 MCR.LIMIT_DEDUC_ID = SIC.DEDUCTIBLE      
 WHERE SIC.CUSTOMER_ID = @CUSTOMERID AND APP_ID = @POLID AND APP_VERSION_ID = @VERSIONID      
  
 SELECT CASE WHEN ISNUMERIC(C1.PREMIUM) = 0 THEN C1.PREMIUM ELSE C1.PREMIUM + '.00' END AS COVERAGE_PREMIUM, C1.COMPONENT_CODE, P1.RISK_ID         
 FROM CLT_PREMIUM_SPLIT P1 WITH (NOLOCK)         
 LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS C1 WITH (NOLOCK)         
 ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID                       
 WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.APP_ID=@POLID AND P1.APP_VERSION_ID=@VERSIONID AND P1.RISK_TYPE = 'HOME'      
  
    
 END      
 ELSE IF @CALLEDFROM='POLICY'      
 BEGIN      
 SELECT SIC.ITEM_ID, MC.COV_CODE,MC.COV_DES,  
 ISNULL(CAST(MCR.LIMIT_DEDUC_AMOUNT*1.00 AS VARCHAR),'') LIMIT_DEDUC_AMOUNT,      
 (  
  SELECT ISNULL(CAST(SUM(convert(bigint,ITEM_INSURING_VALUE)) AS VARCHAR),'')      
     
  FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS SICD WITH (NOLOCK)      
  WHERE SIC.ITEM_ID = SICD.ITEM_ID AND SIC.CUSTOMER_ID = SICD.CUSTOMER_ID AND SIC.Policy_ID = SICD.Pol_ID      
  AND SIC.Policy_VERSION_ID = SICD.Pol_VERSION_ID AND SICD.IS_ACTIVE='Y' -- --ITrack # 6140 Manoj   
 ) AS AMOUNT,  --Removed *1.00 by Charles on 6-Oct-09 for Itrack 6488          
 MC.FORM_NUMBER AS FORM_NO, MC.COMPONENT_CODE      
 FROM POL_HOME_OWNER_SCH_ITEMS_CVGS SIC WITH (NOLOCK)      
 INNER JOIN MNT_COVERAGE MC WITH (NOLOCK) ON SIC.ITEM_ID = MC.COV_ID AND SIC.IS_ACTIVE = 'Y'      
 INNER JOIN MNT_COVERAGE_RANGES MCR WITH (NOLOCK) ON MCR.COV_ID = SIC.ITEM_ID AND      
 MCR.LIMIT_DEDUC_ID = SIC.DEDUCTIBLE      
 WHERE SIC.CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLID AND POLICY_VERSION_ID = @VERSIONID      
    
 SELECT CASE WHEN ISNUMERIC(C1.PREMIUM) = 0 THEN C1.PREMIUM ELSE C1.PREMIUM + '.00' END AS COVERAGE_PREMIUM, C1.COMPONENT_CODE, P1.RISK_ID         
 FROM CLT_PREMIUM_SPLIT P1 WITH (NOLOCK)         
 LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS C1 WITH (NOLOCK)         
 ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID                       
 WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.POLICY_ID=@POLID AND P1.POLICY_VERSION_ID=@VERSIONID AND P1.RISK_TYPE = 'HOME'      
   
        
 END       
       
        
END       
      
      
      
      
      
      
      
      
      
    
  
  
  
  
  
  
GO

