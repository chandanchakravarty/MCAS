IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFRecreationalVehiclesDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFRecreationalVehiclesDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROCEDURE dbo.Proc_GetPDFRecreationalVehiclesDetails      
CREATE  PROCEDURE dbo.Proc_GetPDFRecreationalVehiclesDetails      
(      
 @CUSTOMERID   int,      
 @POLID                int,      
 @VERSIONID   int,      
 @CALLEDFROM  VARCHAR(20)      
)      
AS      
BEGIN      
 IF (@CALLEDFROM='APPLICATION')      
 BEGIN      
  SELECT       
   COMPANY_ID_NUMBER,LOOKUP_VALUE_DESC VEHICLE_DESC,VEHICLE_TYPE,      
   LTRIM(RTRIM(ISNULL(STR(YEAR),'')))+ '/'+ ISNULL(MAKE,'') + '/' + ISNULL(MODEL,'') YEARMAKEMODEL,      
   LTRIM(RTRIM(ISNULL(STR(YEAR),''))) YEAR, ISNULL(MAKE,'') MAKE,ISNULL(MODEL,'') MODEL,      
   HORSE_POWER,SERIAL,ISNULL(CAST(CAST(INSURING_VALUE AS DECIMAL(20,2)) AS VARCHAR),'') INSURING_VALUE,ISNULL(CAST(CAST(DEDUCTIBLE AS DECIMAL(20,2)) AS VARCHAR),'') DEDUCTIBLE,REMARKS,REC_VEH_ID,      
   USED_IN_RACE_SPEED,PRIOR_LOSSES,IS_UNIT_REG_IN_OTHER_STATE,RISK_DECL_BY_OTHER_COMP,DESC_RISK_DECL_BY_OTHER_COMP      
  FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES WITH(NOLOCK)     
   INNER JOIN MNT_LOOKUP_VALUES WITH(NOLOCK) ON LOOKUP_ID=1219 AND LOOKUP_UNIQUE_ID=VEHICLE_TYPE       
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID     
    
 SELECT CASE WHEN ISNUMERIC(C1.PREMIUM) = 0 THEN C1.PREMIUM ELSE C1.PREMIUM + '.00' END AS COVERAGE_PREMIUM, C1.COMPONENT_CODE, P1.RISK_ID       
 FROM CLT_PREMIUM_SPLIT P1 WITH(NOLOCK)       
 LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS C1 WITH(NOLOCK)       
 ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID                     
  WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.APP_ID=@POLID AND P1.APP_VERSION_ID=@VERSIONID AND P1.RISK_TYPE = 'RV'    
     
 END      
 ELSE IF (@CALLEDFROM='POLICY')      
 BEGIN      
  SELECT       
   COMPANY_ID_NUMBER,LOOKUP_VALUE_DESC VEHICLE_DESC,VEHICLE_TYPE,      
   LTRIM(RTRIM(ISNULL(STR(YEAR),'')))+ ' / '+ ISNULL(MAKE,'') + ISNULL(MODEL,'') YEARMAKEMODEL,      
   LTRIM(RTRIM(ISNULL(STR(YEAR),''))) YEAR, ISNULL(MAKE,'') MAKE,ISNULL(MODEL,'') MODEL,      
   HORSE_POWER,SERIAL,ISNULL(CAST(CAST(INSURING_VALUE AS DECIMAL(20,2)) AS VARCHAR),'') INSURING_VALUE,ISNULL(CAST(CAST(DEDUCTIBLE AS DECIMAL(20,2)) AS VARCHAR),'') DEDUCTIBLE,REMARKS,REC_VEH_ID,      
   USED_IN_RACE_SPEED,PRIOR_LOSSES,IS_UNIT_REG_IN_OTHER_STATE,RISK_DECL_BY_OTHER_COMP,DESC_RISK_DECL_BY_OTHER_COMP      
  FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES WITH(NOLOCK)      
   INNER JOIN MNT_LOOKUP_VALUES WITH(NOLOCK) ON LOOKUP_ID=1219 AND LOOKUP_UNIQUE_ID=VEHICLE_TYPE       
  WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID     
    
 SELECT CASE WHEN ISNUMERIC(C1.PREMIUM) = 0 THEN C1.PREMIUM ELSE C1.PREMIUM + '.00' END AS COVERAGE_PREMIUM, C1.COMPONENT_CODE, P1.RISK_ID       
 FROM CLT_PREMIUM_SPLIT P1 WITH(NOLOCK)       
 LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS C1 WITH(NOLOCK)       
 ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID                     
  WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.POLICY_ID=@POLID AND P1.POLICY_VERSION_ID=@VERSIONID AND P1.RISK_TYPE = 'RV'    
     
 END      
END      
      
      
      
      
      
      
      
      
    
    
  


GO

