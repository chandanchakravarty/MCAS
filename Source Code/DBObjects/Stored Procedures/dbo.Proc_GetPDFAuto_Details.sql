IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFAuto_Details]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFAuto_Details]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                        
Proc Name          : Dbo.Proc_GetPDFAuto_Details                        
Created by         : Anurag verma                        
Date               : 26-June-2006                        
Purpose            :                         
Revison History    :                        
Used In            : Wolverine                          
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                        
--DROP PROCEDURE dbo.Proc_GetPDFAuto_Details  2126,438,3,'POLICY'                      
CREATE      PROCEDURE [dbo].[Proc_GetPDFAuto_Details]                  
(                        
 @CUSTOMERID   int,                        
 @POLID                int,                        
 @VERSIONID   int,                        
 @CALLEDFROM  VARCHAR(20)                        
)                        
AS                        
BEGIN                        
-- IF (@CALLEDFROM='APPLICATION')                        
-- BEGIN                        
--  SELECT DISTINCT                         
--  CCL.CUSTOMER_INSURANCE_SCORE,AV.SYMBOL,MLV_MOTOTYPE.LOOKUP_VALUE_DESC,AV.VEHICLE_AGE,--ADDINT.HOLDER_NAME+', '+ADDINT.HOLDER_ADD1+', '+ADDINT.HOLDER_ADD2+', '+ADDINT.HOLDER_CITY+', '+ADDINT.HOLDER_STATE+', '+ADDINT.HOLDER_ZIP                     
-- '' AS LOSS_PAYEE_TO,AV.VEHICLE_ID                      
-- AS VEHICLE_ID,VEHICLE_CC,ISNULL(MOTORCYCLE_TYPE,'')MOTORCYCLE_TYPE,CASE WHEN LOWER(AAGI.CURR_RES_TYPE)='OWNED' THEN 1 ELSE 0 END CURRENTADDRESS,                        
--  AV.INSURED_VEH_NUMBER,AV.VEHICLE_YEAR,                        
--  AV.MAKE , AV.MODEL ,AV.BODY_TYPE ,(CASE WHEN AV.IS_OWN_LEASE='1' THEN 'PURCHASED' WHEN AV.IS_OWN_LEASE='0' THEN 'LEASED' ELSE '' END) LEASED   
--  ,isnull(case AV.GRG_ADD1 when '' then isnull(AV.GRG_ADD1,'')  else ltrim(rtrim(AV.GRG_ADD1)) end,'')   
--  + CASE WHEN ISNULL(AV.GRG_ADD2,'') != '' THEN ', ' + ltrim(rtrim(AV.GRG_ADD2)) ELSE '' END                  
--  GRG_ADD,                       
--  isnull( case AV.GRG_CITY when '' then isnull(AV.GRG_CITY,'') else ltrim(rtrim(AV.GRG_CITY)) + ',' end,'')                    
--  +' '  + isnull(case MCSL1.STATE_CODE  when '' then isnull(MCSL1.STATE_CODE,'') else ltrim(rtrim(MCSL1.STATE_CODE))  + '' end,'')                    
--  +' '  + isnull(case AV.GRG_ZIP  when '' then isnull(AV.GRG_ZIP,'') else ltrim(rtrim(AV.GRG_ZIP))    end,'') GRG_CITYSTZIP,                    
--  cast(AV.VIN as varchar(100)) VIN, MCSL.STATE_NAME                        
--  ,CASE WHEN USE_VEHICLE=11332 THEN                         
--   case                         
--        when AV.VEHICLE_TYPE_PER=11334 then 'PP'                        
--        when AV.VEHICLE_TYPE_PER=11335 then 'CV'                        
--        when AV.VEHICLE_TYPE_PER=11336 then 'MV'   
--  when AV.VEHICLE_TYPE_PER=11868 then 'CLC'                       
--        when AV.VEHICLE_TYPE_PER=11869 then 'AC'                        
--        when AV.VEHICLE_TYPE_PER=11870 then 'TT'                        
--        when AV.VEHICLE_TYPE_PER=11337 then 'UT'                        
--        when AV.VEHICLE_TYPE_PER=11618 then 'SC'                        
--   end                        
--  ELSE                         
--   case                         
--        when AV.VEHICLE_TYPE_COM=11339 then 'LH'    
--  when AV.VEHICLE_TYPE_COM=11871 then 'LHA'                      
--        when AV.VEHICLE_TYPE_COM=11338 then 'LHI'                        
--        when AV.VEHICLE_TYPE_COM=11341 then 'TR'                        
--   end                            
--   END HPCC,                        
--CASE WHEN USE_VEHICLE=11332 THEN                         
--   case                         
--        when AV.VEHICLE_TYPE_PER=11334 then 'PP'                        
--        when AV.VEHICLE_TYPE_PER=11335 then 'CV'                        
--        when AV.VEHICLE_TYPE_PER=11336 then 'MH'  
--  when AV.VEHICLE_TYPE_PER=11868 then 'CLC'                         
--        when AV.VEHICLE_TYPE_PER=11869 then 'ANC'    
--       when AV.VEHICLE_TYPE_PER=11870 then 'CTT'                        
--        when AV.VEHICLE_TYPE_PER=11337 then 'TR'                        
--        when AV.VEHICLE_TYPE_PER=11618 then 'SCO'                        
--   end                        
--  ELSE                         
--   case                         
--        when AV.VEHICLE_TYPE_COM=11339 then 'LH'    
--  when AV.VEHICLE_TYPE_COM=11871 then 'LHA'                      
--        when AV.VEHICLE_TYPE_COM=11338 then 'LHI'                        
--        when AV.VEHICLE_TYPE_COM=11341 then 'TR'                        
--   end                            
--   END RISKTYPE,  
-- CASE   
--  WHEN MOTORCYCLE_TYPE=11421 THEN 'A'  
--  WHEN MOTORCYCLE_TYPE=11422 THEN 'E'  
--  WHEN MOTORCYCLE_TYPE=11423 THEN 'G'  
--  WHEN MOTORCYCLE_TYPE=11424 THEN 'H'  
--  WHEN MOTORCYCLE_TYPE=11425 THEN 'T'  
--  WHEN MOTORCYCLE_TYPE=11426 THEN 'X'  
-- ELSE 'A'  
-- END MOTORCYCLETYPE,  
   
-- CASE AV.IS_SUSPENDED   
--  WHEN 10963  
--   THEN 'TRUE'  
--  WHEN 10964  
--   THEN 'FALSE'  
--  ELSE   
--   ''  
-- END SUSPENDEDCOMP_ONLY,  
-- convert(varchar(15),av.purchase_date,1) DATEPURCHASED,                        
--  CASE WHEN AV.IS_NEW_USED='1' THEN 'U' WHEN AV.IS_NEW_USED='0' THEN 'N' ELSE '' END NEWUSED,AV.AMOUNT,cast(AV.SYMBOL as varchar(100)) SYMBOL ,cast(AV.VEHICLE_AGE as varchar(100)) AGEGROUP,                        
--  AV.TERRITORY,AV.MILES_TO_WORK,      
--  CASE WHEN AV.USE_VEHICLE = 11333 THEN ''              
--       WHEN AV.VEHICLE_USE=6906 THEN 'B'                       
--       WHEN AV.VEHICLE_USE=11273 THEN 'CB'                         
--       WHEN AV.VEHICLE_USE=11275 THEN 'CW'                        
--       WHEN AV.VEHICLE_USE=11274 THEN 'CP'                        
--       WHEN AV.VEHICLE_USE=11270 THEN 'W'                        
--       WHEN AV.VEHICLE_USE=11269 THEN 'P'                        
--       WHEN AV.VEHICLE_USE=11272 THEN 'S' END USAGE,                        
--  CASE WHEN CCL.CUSTOMER_INSURANCE_SCORE BETWEEN 0 AND 599 THEN 'Base Rate'                      WHEN CCL.CUSTOMER_INSURANCE_SCORE BETWEEN 600 AND 700 THEN 'Middle Rate'                        
--       WHEN CCL.CUSTOMER_INSURANCE_SCORE > 701 THEN 'Best Rate' END INSURANCE_TYPE,                        
    
--  CASE WHEN   
--  (SELECT COUNT(VEHICLE_ID) FROM APP_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID   
--  and IS_ACTIVE = 'Y' and VEHICLE_TYPE_PER!=11618   
--  and VEHICLE_TYPE_PER !=11870 and VEHICLE_TYPE_PER !=11337)> 1  
--         THEN 'Y'  
--  ELSE  'N'  
--       END MULTI_CAR,  
----CASE WHEN AV.MULTI_CAR IS NULL THEN 'N' WHEN AV.MULTI_CAR=11918 THEN 'N' ELSE 'Y' END MULTI_CAR,  
--       AV.ANNUAL_MILEAGE,  
-- CASE WHEN ISNULL(AV.USE_VEHICLE,0)=11332  
-- THEN   
--  CASE WHEN ISNULL(AV.VEHICLE_TYPE_PER,0)=11618  
--   THEN  ''  
--   WHEN ISNULL(AV.VEHICLE_TYPE_PER,0)=11870  
--   THEN  ''  
--  ELSE  
--   MLV_CLASS1.LOOKUP_VALUE_DESC   
--  END    
--      WHEN ISNULL(AV.USE_VEHICLE,0)=0   
-- THEN   
--  MLV_CLASS2.LOOKUP_VALUE_DESC  
-- ELSE   
--  MLV_CLASS.LOOKUP_VALUE_DESC   
-- END CLASS,   
-- CASE WHEN ISNULL(AV.APP_VEHICLE_CLASS,0) =0  
--  THEN ''  
-- ELSE  MLV_CLASS2.LOOKUP_VALUE_DESC  
-- END MOTORCYCLE_CLASS,  
--  case when AV.PASSIVE_SEAT_BELT=10963 then 'Yes' else 'No' end PASSIVE_SEAT_BELT,CASE WHEN (AV.AIR_BAG=9 OR AIR_BAG=11280) THEN 'Both' WHEN AV.AIR_BAG=8 THEN 'Driver' else '' END AIRBAG,                        
--  MLV_ANTILOCKBRAKES.LOOKUP_VALUE_DESC ANTI_LOCK_BRAKES,AV.TERRITORY,MLV_USE.LOOKUP_VALUE_DESC VEHICLE_USE,                        
--CASE  WHEN ISNULL(AV.USE_VEHICLE,0)=11332  
--THEN    
--  MLV_VEHICLEUSE.LOOKUP_VALUE_DESC +  
--    CASE ISNULL(AV.SNOWPLOW_CONDS,0)  
--   WHEN 0  
--    THEN ''  
--    ELSE  
--    +', '+MLV_SNOWPLOW.LOOKUP_VALUE_DESC  
--    END  
--ELSE  
-- ''  
--END  
--USE_VEHICLE,CASE WHEN (VEHICLE_TYPE_PER IS NOT NULL and VEHICLE_TYPE_PER <>0) THEN MLV_VEHICLETYPEPER.LOOKUP_VALUE_DESC ELSE MLV_VEHICLETYPECOM.LOOKUP_VALUE_DESC END +  
-- CASE AV.VEHICLE_TYPE_PER   
-- WHEN 11869  
--  THEN +' ($'+SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(AV.AMOUNT,0)),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(AV.AMOUNT,0)),1),0))+')'  
-- WHEN 11868  
--  THEN +' ($'+SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(AV.AMOUNT,0)),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(AV.AMOUNT,0)),1),0))+')'  
-- ELSE  ''  
-- END  
-- VEHICLE_TYPE       
--  FROM APP_VEHICLES AV   WITH (NOLOCK)          
--  LEFT JOIN MNT_COUNTRY_STATE_LIST MCSL1  WITH (NOLOCK) ON AV.GRG_STATE=MCSL1.STATE_ID                                
--  LEFT OUTER JOIN APP_AUTO_GEN_INFO AAGI  WITH (NOLOCK) ON AV.CUSTOMER_ID=AAGI.CUSTOMER_ID AND AV.APP_ID=AAGI.APP_ID AND AV.APP_VERSION_ID=AAGI.APP_VERSION_ID                        
--  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL  WITH (NOLOCK) ON MCSL.STATE_ID=AV.REGISTERED_STATE             
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_VEHICLETYPEPER  WITH (NOLOCK) ON MLV_VEHICLETYPEPER.LOOKUP_UNIQUE_ID=AV.VEHICLE_TYPE_PER AND (MLV_VEHICLETYPEPER.LOOKUP_ID=1209) AND MLV_VEHICLETYPEPER.IS_ACTIVE='Y'            
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_VEHICLETYPECOM  WITH (NOLOCK) ON MLV_VEHICLETYPECOM.LOOKUP_UNIQUE_ID=AV.VEHICLE_TYPE_COM AND (MLV_VEHICLETYPECOM.LOOKUP_ID=1210) AND MLV_VEHICLETYPECOM.IS_ACTIVE='Y'                  
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_VEHICLEUSE  WITH (NOLOCK) ON MLV_VEHICLEUSE.LOOKUP_UNIQUE_ID=AV.VEHICLE_USE AND (MLV_VEHICLEUSE.LOOKUP_ID=907) AND MLV_VEHICLEUSE.IS_ACTIVE='Y'                        
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_ANTILOCKBRAKES  WITH (NOLOCK) ON MLV_ANTILOCKBRAKES.LOOKUP_UNIQUE_ID=AV.ANTI_LOCK_BRAKES AND (MLV_ANTILOCKBRAKES.LOOKUP_ID=1126) AND MLV_ANTILOCKBRAKES.IS_ACTIVE='Y'                        
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_CLASS  WITH (NOLOCK) ON MLV_CLASS.LOOKUP_UNIQUE_ID=AV.CLASS_COM AND (MLV_CLASS.LOOKUP_ID=1212) AND MLV_CLASS.IS_ACTIVE='Y'                        
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_CLASS1  WITH (NOLOCK) ON MLV_CLASS1.LOOKUP_UNIQUE_ID=AV.CLASS_PER AND (MLV_CLASS1.LOOKUP_ID=1211) AND MLV_CLASS1.IS_ACTIVE='Y'                        
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_CLASS2  WITH (NOLOCK) ON MLV_CLASS2.LOOKUP_UNIQUE_ID=AV.APP_VEHICLE_CLASS AND MLV_CLASS2.IS_ACTIVE='Y'                        
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_MOTOTYPE  WITH (NOLOCK) ON MLV_MOTOTYPE.LOOKUP_UNIQUE_ID = aV.MOTORCYCLE_TYPE AND MLV_MOTOTYPE.LOOKUP_ID=1188                        
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_USE  WITH (NOLOCK) ON MLV_USE.LOOKUP_UNIQUE_ID=AV.USE_VEHICLE AND (MLV_USE.LOOKUP_ID=1208) AND MLV_USE.IS_ACTIVE='Y'  
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_SNOWPLOW  WITH (NOLOCK) ON MLV_SNOWPLOW.LOOKUP_UNIQUE_ID=AV.SNOWPLOW_CONDS AND  (MLV_SNOWPLOW.LOOKUP_ID=1287) AND MLV_SNOWPLOW.IS_ACTIVE='Y'                         
----  LEFT OUTER JOIN APP_ADD_OTHER_INT ADDINT ON ADDINT.VEHICLE_ID=AV.VEHICLE_ID AND ADDINT.CUSTOMER_ID=AV.CUSTOMER_ID AND ADDINT.APP_ID=AV.APP_ID AND ADDINT.APP_VERSION_ID=AV.APP_VERSION_ID                        
--  LEFT OUTER JOIN CLT_CUSTOMER_LIST CCL  WITH (NOLOCK) ON CCL.CUSTOMER_ID=AV.CUSTOMER_ID                        
--  WHERE AV.CUSTOMER_ID=@CUSTOMERID AND AV.APP_ID=@POLID AND AV.APP_VERSION_ID=@VERSIONID and av.is_active='Y'                   
--  ORDER BY AV.VEHICLE_ID asc                      
-- END                      
-- ELSE IF (@CALLEDFROM='POLICY') 
 IF (@CALLEDFROM='POLICY')                        
 BEGIN                          
  SELECT DISTINCT                         
  CCL.CUSTOMER_INSURANCE_SCORE,PV.SYMBOL,MLV_MOTOTYPE.LOOKUP_VALUE_DESC,PV.VEHICLE_AGE,--POLINT.HOLDER_NAME+', '+POLINT.HOLDER_ADD1+', '+POLINT.HOLDER_ADD2+', '+POLINT.HOLDER_CITY+', '+POLINT.HOLDER_STATE+', '+POLINT.HOLDER_ZIP                     
 '' AS LOSS_PAYEE_TO,PV.VEHICLE_ID               
 AS VEHICLE_ID ,VEHICLE_CC,ISNULL(MOTORCYCLE_TYPE,'') MOTORCYCLE_TYPE,CASE WHEN LOWER(PAGI.CURR_RES_TYPE)='OWNED'                       
THEN 1 ELSE 0 END CURRENTADDRESS,    PV.INSURED_VEH_NUMBER,PV.VEHICLE_YEAR,                        
  PV.MAKE , PV.MODEL ,PV.BODY_TYPE ,(CASE WHEN PV.IS_OWN_LEASE='1' THEN 'PURCHASED' WHEN PV.IS_OWN_LEASE='0' THEN 'LEASED' ELSE '' END) LEASED                        
  ,isnull(case PV.GRG_ADD1 when '' then isnull(PV.GRG_ADD1,'')  else ltrim(rtrim(PV.GRG_ADD1)) end,'')    
   + CASE WHEN ISNULL(PV.GRG_ADD2,'') != '' THEN ', ' + ltrim(rtrim(PV.GRG_ADD2)) ELSE '' END   
--   +  isnull( case PV.GRG_ADD2 when '' then isnull(PV.GRG_ADD2,'')  else ', ' + ltrim(rtrim(PV.GRG_ADD2)) end,'')                     
  GRG_ADD,                       
  isnull( case PV.GRG_CITY when '' then isnull(PV.GRG_CITY,'') else ltrim(rtrim(PV.GRG_CITY)) + ',' end,'')                    
  +' '  + isnull(case MCSL1.STATE_CODE  when '' then isnull(MCSL1.STATE_CODE,'') else ltrim(rtrim(MCSL1.STATE_CODE)) end,'')                    
  +' '  + isnull(case PV.GRG_ZIP  when '' then isnull(PV.GRG_ZIP,'') else ltrim(rtrim(PV.GRG_ZIP))    end,'') GRG_CITYSTZIP,                   
   CAST(PV.VIN AS VARCHAR(100)) VIN, MCSL.STATE_NAME                   
  ,CASE WHEN APP_USE_VEHICLE_ID=11332 THEN                         
   case                         
      when PV.APP_VEHICLE_PERTYPE_ID=11334 then 'PP'                        
        when PV.APP_VEHICLE_PERTYPE_ID=11335 then 'CV'                        
        when PV.APP_VEHICLE_PERTYPE_ID=11336 then 'MV'  
  when PV.APP_VEHICLE_PERTYPE_ID=11868 then 'CLC'             
        when PV.APP_VEHICLE_PERTYPE_ID=11869 then 'AC'                       
        when PV.APP_VEHICLE_PERTYPE_ID=11870 then 'TT'                        
        when PV.APP_VEHICLE_PERTYPE_ID=11337 then 'UT'                  
        when PV.APP_VEHICLE_PERTYPE_ID=11618 then 'SC'                        
   end                        
  ELSE                         
   case              
        when PV.APP_VEHICLE_COMTYPE_ID=11339 then 'LH'                        
        when PV.APP_VEHICLE_COMTYPE_ID=11338 then 'LHI'                        
        when PV.APP_VEHICLE_COMTYPE_ID=11341 then 'TR'                        
   end                            
   END HPCC,  
CASE WHEN APP_USE_VEHICLE_ID=11332 THEN                         
   case        
      when PV.APP_VEHICLE_PERTYPE_ID=11334 then 'PP'                        
        when PV.APP_VEHICLE_PERTYPE_ID=11335 then 'CV'                        
        when PV.APP_VEHICLE_PERTYPE_ID=11336 then 'MH'  
  when PV.APP_VEHICLE_PERTYPE_ID=11868 then 'CLC'                        
        when PV.APP_VEHICLE_PERTYPE_ID=11869 then 'ANC'                        
        when PV.APP_VEHICLE_PERTYPE_ID=11870 then 'CTT'                        
        when PV.APP_VEHICLE_PERTYPE_ID=11337 then 'TR'                  
        when PV.APP_VEHICLE_PERTYPE_ID=11618 then 'SCO'                        
   end                        
  ELSE                         
   case                         
        when PV.APP_VEHICLE_COMTYPE_ID=11339 then 'LH'    
  when PV.APP_VEHICLE_COMTYPE_ID=11871 then 'LHA'                                            
        when PV.APP_VEHICLE_COMTYPE_ID=11338 then 'LHI'                        
        when PV.APP_VEHICLE_COMTYPE_ID=11341 then 'TR'                       
   end                            
   END RISKTYPE,  
 CASE   
  WHEN MOTORCYCLE_TYPE=11421 THEN 'A'  
  WHEN MOTORCYCLE_TYPE=11422 THEN 'E'  
  WHEN MOTORCYCLE_TYPE=11423 THEN 'G'  
  WHEN MOTORCYCLE_TYPE=11424 THEN 'H'  
  WHEN MOTORCYCLE_TYPE=11425 THEN 'T'  
  WHEN MOTORCYCLE_TYPE=11426 THEN 'X'  
 ELSE 'A'  
 END MOTORCYCLETYPE,  
   
 CASE PV.IS_SUSPENDED   
  WHEN 10963  
   THEN 'TRUE'  
  WHEN 10964  
   THEN 'FALSE'  
  ELSE   
   ''  
 END SUSPENDEDCOMP_ONLY,  
CONVERT(VARCHAR(15),PV.PURCHASE_DATE,1) DATEPURCHASED,                        
  CASE WHEN PV.IS_NEW_USED='1' THEN 'U' WHEN PV.IS_NEW_USED='0' THEN 'N' ELSE '' END NEWUSED,PV.AMOUNT,PV.SYMBOL,PV.VEHICLE_AGE AGEGROUP,                        
  PV.TERRITORY,PV.MILES_TO_WORK  ,                        
  CASE WHEN PV.APP_USE_VEHICLE_ID = 11333 THEN ''              
       WHEN PV.VEHICLE_USE=6906 THEN 'B'                        
       WHEN PV.VEHICLE_USE=11273 THEN 'CB'                         
       WHEN PV.VEHICLE_USE=11275 THEN 'CW'                        
       WHEN PV.VEHICLE_USE=11274 THEN 'CP'                        
       WHEN PV.VEHICLE_USE=11270 THEN 'W'                      
       WHEN PV.VEHICLE_USE=11269 THEN 'P'                        
       WHEN PV.VEHICLE_USE=11272 THEN 'S' END USAGE,                        
  CASE WHEN CCL.CUSTOMER_INSURANCE_SCORE BETWEEN 0 AND 599 THEN 'Base Rate'                         
       WHEN CCL.CUSTOMER_INSURANCE_SCORE BETWEEN 600 AND 700 THEN 'Middle Rate'                
       WHEN CCL.CUSTOMER_INSURANCE_SCORE > 701 THEN 'Best Rate' END INSURANCE_TYPE,  
  CASE WHEN   
  (SELECT COUNT(VEHICLE_ID)   
  FROM POL_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID   
  AND POLICY_VERSION_ID=@VERSIONID AND IS_ACTIVE='Y' AND APP_VEHICLE_PERTYPE_ID !=11618   
  AND APP_VEHICLE_PERTYPE_ID !=11870 AND APP_VEHICLE_PERTYPE_ID !=11337) > 1 THEN 'Y'  
  ELSE  'N'  
       END MULTI_CAR,  
  --CASE WHEN PV.MULTI_CAR IS NULL THEN 'N' WHEN PV.MULTI_CAR=11918 THEN 'N' ELSE 'Y' END MULTI_CAR,  
     PV.ANNUAL_MILEAGE,  
  CASE WHEN ISNULL(PV.APP_USE_VEHICLE_ID,0)=11332  
 THEN   
  CASE WHEN ISNULL(PV.APP_VEHICLE_PERTYPE_ID,0)=11618  
   THEN  ''  
    WHEN ISNULL(PV.APP_VEHICLE_PERTYPE_ID,0)=11870  
   THEN ''  
  ELSE  
   MLV_CLASS1.LOOKUP_VALUE_DESC   
  END  
      WHEN ISNULL(PV.APP_USE_VEHICLE_ID,0)=0   
 THEN   
  MLV_CLASS2.LOOKUP_VALUE_DESC  
 ELSE   
  MLV_CLASS.LOOKUP_VALUE_DESC   
 END CLASS,  
 CASE WHEN ISNULL(PV.APP_VEHICLE_CLASS,0) =0  
  THEN ''  
 ELSE  MLV_CLASS2.LOOKUP_VALUE_DESC  
 END MOTORCYCLE_CLASS,                        
  case when PV.PASSIVE_SEAT_BELT=10963 then 'Yes' else 'No' end PASSIVE_SEAT_BELT,CASE WHEN (PV.AIR_BAG=9 OR AIR_BAG=11280) THEN 'Both' WHEN PV.AIR_BAG=8 THEN 'Driver' else '' END AIRBAG,                        
  MLV_ANTILOCKBRAKES.LOOKUP_VALUE_DESC ANTI_LOCK_BRAKES,PV.TERRITORY,MLV_USE.LOOKUP_VALUE_DESC VEHICLE_USE,                        
CASE WHEN ISNULL(PV.APP_USE_VEHICLE_ID,0)=11332  
THEN    
   MLV_VEHICLEUSE.LOOKUP_VALUE_DESC +  
   CASE  ISNULL(PV.SNOWPLOW_CONDS,0)  
   WHEN 0  
    THEN ''  
    ELSE  
    +', '+MLV_SNOWPLOW.LOOKUP_VALUE_DESC  
    END  
ELSE  
 ''  
END  
USE_VEHICLE,CASE WHEN (APP_VEHICLE_PERTYPE_ID IS NOT NULL and APP_VEHICLE_PERTYPE_ID <>0) THEN MLV_VEHICLETYPEPER.LOOKUP_VALUE_DESC ELSE MLV_VEHICLETYPECOM.LOOKUP_VALUE_DESC END +   
CASE PV.APP_VEHICLE_PERTYPE_ID   
 WHEN 11869  
  THEN +' ($'+SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(PV.AMOUNT,0)),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(PV.AMOUNT,0)),1),0))+')'  
 WHEN 11868  
  THEN +' ($'+SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(PV.AMOUNT,0)),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(PV.AMOUNT,0)),1),0))+')'  
 ELSE  ''  
 END  
VEHICLE_TYPE                        
  FROM POL_VEHICLES PV    WITH (NOLOCK)                      
  LEFT JOIN MNT_COUNTRY_STATE_LIST MCSL1  WITH (NOLOCK) ON PV.GRG_STATE=MCSL1.STATE_ID                                
  LEFT OUTER JOIN POL_AUTO_GEN_INFO PAGI  WITH (NOLOCK) ON PV.CUSTOMER_ID=PAGI.CUSTOMER_ID AND PV.POLICY_ID=PAGI.POLICY_ID AND PV.POLICY_VERSION_ID=PAGI.POLICY_VERSION_ID                        
  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL  WITH (NOLOCK) ON MCSL.STATE_ID=PV.REGISTERED_STATE                        
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_VEHICLETYPEPER  WITH (NOLOCK) ON MLV_VEHICLETYPEPER.LOOKUP_UNIQUE_ID=PV.APP_VEHICLE_PERTYPE_ID AND (MLV_VEHICLETYPEPER.LOOKUP_ID=1209) AND MLV_VEHICLETYPEPER.IS_ACTIVE='Y'                        
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_VEHICLETYPECOM  WITH (NOLOCK) ON MLV_VEHICLETYPECOM.LOOKUP_UNIQUE_ID=PV.APP_VEHICLE_COMTYPE_ID AND (MLV_VEHICLETYPECOM.LOOKUP_ID=1210) AND MLV_VEHICLETYPECOM.IS_ACTIVE='Y'                        
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_VEHICLEUSE  WITH (NOLOCK) ON MLV_VEHICLEUSE.LOOKUP_UNIQUE_ID=PV.VEHICLE_USE AND (MLV_VEHICLEUSE.LOOKUP_ID=907) AND MLV_VEHICLEUSE.IS_ACTIVE='Y'                        
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_ANTILOCKBRAKES  WITH (NOLOCK) ON MLV_ANTILOCKBRAKES.LOOKUP_UNIQUE_ID=PV.ANTI_LOCK_BRAKES AND (MLV_ANTILOCKBRAKES.LOOKUP_ID=1126) AND MLV_ANTILOCKBRAKES.IS_ACTIVE='Y'                        
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_CLASS  WITH (NOLOCK) ON MLV_CLASS.LOOKUP_UNIQUE_ID=PV.APP_VEHICLE_COMCLASS_ID AND (MLV_CLASS.LOOKUP_ID=1212) AND MLV_CLASS.IS_ACTIVE='Y'                        
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_CLASS1  WITH (NOLOCK) ON MLV_CLASS1.LOOKUP_UNIQUE_ID=PV.APP_VEHICLE_PERCLASS_ID AND (MLV_CLASS1.LOOKUP_ID=1211) AND MLV_CLASS1.IS_ACTIVE='Y'                        
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_CLASS2  WITH (NOLOCK) ON MLV_CLASS2.LOOKUP_UNIQUE_ID=PV.APP_VEHICLE_CLASS AND MLV_CLASS2.IS_ACTIVE='Y'                        
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_USE  WITH (NOLOCK) ON MLV_USE.LOOKUP_UNIQUE_ID=PV.APP_USE_VEHICLE_ID AND (MLV_USE.LOOKUP_ID=1208) AND MLV_USE.IS_ACTIVE='Y'                        
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_MOTOTYPE  WITH (NOLOCK) ON MLV_MOTOTYPE.LOOKUP_UNIQUE_ID = PV.MOTORCYCLE_TYPE AND MLV_MOTOTYPE.LOOKUP_ID=1188    
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_SNOWPLOW  WITH (NOLOCK) ON MLV_SNOWPLOW.LOOKUP_UNIQUE_ID=PV.SNOWPLOW_CONDS AND  (MLV_SNOWPLOW.LOOKUP_ID=1287) AND MLV_SNOWPLOW.IS_ACTIVE='Y'                         
--  LEFT OUTER JOIN POL_ADD_OTHER_INT POLINT ON POLINT.VEHICLE_ID=PV.VEHICLE_ID AND POLINT.CUSTOMER_ID=PV.CUSTOMER_ID AND POLINT.POLICY_ID=PV.POLICY_ID AND POLINT.POLICY_VERSION_ID=PV.POLICY_VERSION_ID                        
  LEFT OUTER JOIN CLT_CUSTOMER_LIST CCL  WITH (NOLOCK) ON CCL.CUSTOMER_ID=PV.CUSTOMER_ID                        
  WHERE PV.CUSTOMER_ID=@CUSTOMERID AND PV.POLICY_ID=@POLID AND PV.POLICY_VERSION_ID=@VERSIONID and pv.is_active='Y'                    
  ORDER BY PV.VEHICLE_ID asc                      
 END                        
END                        
  
  
GO

