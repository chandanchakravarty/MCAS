IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFHomeowner_Dwelling_Details]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFHomeowner_Dwelling_Details]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                              
Proc Name          : Dbo.Proc_GetPDFHomeowner_Dwelling_Details                              
Created by         : Anurag verma                              
Date               : 26-June-2006                              
Purpose            :                               
Revison History    :                              
Used In            : Wolverine                                
                        
Modified By    : Anurag Verma                        
Modified On    : 28/03/2007                        
Purpose     : making address field as comma seperated                             
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
--DROP PROCEDURE Proc_GetPDFHomeowner_Dwelling_Details  681,18,1,'POLICY'                            
CREATE  PROCEDURE [dbo].[Proc_GetPDFHomeowner_Dwelling_Details]                    
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
--  ALC.LOC_ADD1,ALC.LOC_ADD2,ALC.LOC_CITY,SL.STATE_CODE,ALC.LOC_ZIP,                              
----  MV1.LOOKUP_VALUE_DESC OCCUPANCY,  
--   MV1.LOOKUP_VALUE_DESC+' / '+cast(NO_OF_FAMILIES as varchar)+' Family'++' / '+   
--  case when ALC.LOCATION_TYPE=11848 then 'Rented'   
--   when ALC.LOCATION_TYPE=11812 then 'Primary'  
--   when ALC.LOCATION_TYPE=11813 then 'Seasonal'  
--   when ALC.LOCATION_TYPE=11814 then 'Secondary'  
--   when ALC.LOCATION_TYPE=11849 then 'Seasonal Rented'   
--End  OCCUPANCY,                        
--  isnull(case isnull(ALC.LOC_ADD1,'') when '' then '' else ALC.LOC_ADD1  end,'') +                        
--  isnull(case isnull(ALC.LOC_ADD2,'') when '' then '' else ', ' + ALC.LOC_ADD2 end,'')                          
--  LOC_ADDRESS,                              
                        
----  ALC.LOC_CITY + ', ' + SL.STATE_CODE + ', ' + ALC.LOC_ZIP LOC_CITYSTATEZIP,                               
                        
-- isnull(case isnull(ALC.LOC_CITY,'') when '' then '' else ALC.LOC_CITY  + ',' end,'') + ' ' +                        
--  isnull(case isnull(SL.STATE_CODE,'') when '' then '' else SL.STATE_CODE end,'') + ' ' +                        
--  isnull(case isnull(ALC.LOC_ZIP,'') when '' then '' else ALC.LOC_ZIP end,'')                         
--  LOC_CITYSTATEZIP,                         
                        
--  convert(varchar(10),LAST_INSPECTED_DATE,101) LAST_INSPECTED_DATE,                              
--    CONVERT(VARCHAR,CONVERT(MONEY,ISNULL(RECEIVED_PRMIUM,'0')) ,1) RECEIVED_PRMIUM,1 BALANCE                              
--  ,CASE WHEN BILL_TYPE ='DM' OR BILL_TYPE ='IM' OR BILL_TYPE ='MB' THEN 1                              
--          WHEN BILL_TYPE ='DI' OR BILL_TYPE ='AM' OR BILL_TYPE ='AB' THEN 0 END BILLING                              
--  ,CASE WHEN BILL_TYPE ='DM' OR BILL_TYPE ='IM' THEN 1                               
--        WHEN BILL_TYPE ='MB' THEN 2                              
--        ELSE 3 END DIRECT_BILL                              
--  ,CASE WHEN INSTALL_PLAN_ID IS NULL THEN 0                              
--        ELSE 1 END APPLICANT_BILL                              
--  ,CASE WHEN EXTERIOR_CONSTRUCTION = 8934 OR EXTERIOR_CONSTRUCTION = 8935 OR EXTERIOR_CONSTRUCTION = 11809 OR EXTERIOR_CONSTRUCTION = 11851                               
--   OR EXTERIOR_CONSTRUCTION = 11850 OR EXTERIOR_CONSTRUCTION = 11288 THEN 1                              
--        WHEN EXTERIOR_CONSTRUCTION = 11301 OR EXTERIOR_CONSTRUCTION = 11302 OR EXTERIOR_CONSTRUCTION = 8936 THEN 2                              
--        WHEN EXTERIOR_CONSTRUCTION = 11298 OR EXTERIOR_CONSTRUCTION = 11299 OR EXTERIOR_CONSTRUCTION = 8937 THEN 3                              
--        WHEN EXTERIOR_CONSTRUCTION = 11810 THEN 4                              
--        WHEN EXTERIOR_CONSTRUCTION = 11853 THEN 6                              
--        WHEN EXTERIOR_CONSTRUCTION = 11852 THEN 7                               
--      WHEN EXTERIOR_CONSTRUCTION = 11300 OR EXTERIOR_CONSTRUCTION = 11368 THEN 8 END EXTERIOR_CONSTRUCTION                              
--  ,ADI.YEAR_BUILT,                
--  convert(varchar,convert(money,ADI.MARKET_VALUE),1) MARKET_VALUE,                
--  AHRI.NEED_OF_UNITS,                
----ADI.REPLACEMENT_COST                               
--convert(varchar,convert(money,ADI.REPLACEMENT_COST),1) REPLACEMENT_COST,                 
--  ISNULL(CASE WHEN BUILDING_TYPE = 8955 THEN 1                             WHEN BUILDING_TYPE = 8956 THEN 2                              
--   WHEN BUILDING_TYPE = 8957 THEN 3                              
--   WHEN BUILDING_TYPE = 8958 THEN 4                              
--   WHEN BUILDING_TYPE = 8959 THEN 5                              
--   WHEN BUILDING_TYPE = 8960 THEN 6 END,0) STRUCTURE_TYPE                              
--  ,CASE WHEN LOCATION_TYPE=11812 or LOCATION_TYPE=11848 THEN 1                              
--  WHEN LOCATION_TYPE=11813  THEN 2                              
--  WHEN LOCATION_TYPE=11814 or LOCATION_TYPE=11849 THEN 3 END USAGE_TYPE                              
--  ,ANY_FORMING,IS_UNDER_CONSTRUCTION COC,convert(varchar(10),DWELLING_CONST_DATE,101) COMP_DATE ,ISNULL(NO_OF_FAMILIES,'') NO_OF_FAMILIES,                              
--   CASE WHEN PURCHASE_YEAR IS NOT NULL                 
--   THEN CAST(PURCHASE_YEAR AS VARCHAR) + '/' ELSE '' END +                
--   CASE WHEN PURCHASE_PRICE IS NOT NULL                 
--   THEN '$ ' + convert(varchar,convert(money,PURCHASE_PRICE),1 ) ELSE '' END  PUR_YR_PRICE                              
--  ,ISNULL(MLV3.LOOKUP_VALUE_DESC,'') PROT_CLASS,HYDRANT_DIST,FIRE_STATION_DIST                              
--  ,CENT_ST_BURG_FIRE,CENT_ST_FIRE,CENT_ST_BURG,DIR_FIRE_AND_POLICE,DIR_FIRE,DIR_POLICE,LOC_FIRE_GAS,                              
--   TWO_MORE_FIRE,PRIMARY_HEAT_TYPE,SECONDARY_HEAT_TYPE                              
--  ,isnull(PRIMARY_HEAT_OTHER_DESC,'') PRIMARY_HEAT_OTHER_DESC,isnull(SECONDARY_HEAT_OTHER_DESC,'') SECONDARY_HEAT_OTHER_DESC                              
--  ,CASE WHEN WIRING_RENOVATION=8922 THEN 1                              
--        WHEN WIRING_RENOVATION=8923 THEN 0 END WIRING,WIRING_UPDATE_YEAR                              
--  ,CASE WHEN PLUMBING_RENOVATION=8922 THEN 1                              
--        WHEN PLUMBING_RENOVATION=8923 THEN 0 END PLUMBING,PLUMBING_UPDATE_YEAR                              
--  ,CASE WHEN HEATING_RENOVATION=8922 THEN 1                              
--        WHEN HEATING_RENOVATION=8923 THEN 0 END HEATING,HEATING_UPDATE_YEAR                              
--  ,CASE WHEN ROOFING_RENOVATION=8922 THEN 1                              
--        WHEN ROOFING_RENOVATION=8923 THEN 0 END ROOFING,ROOFING_UPDATE_YEAR                              
--  ,case when NO_OF_AMPS = 14121 then 100 when NO_OF_AMPS = 14122 then 200 else isnull(NO_OF_AMPS,0) end NO_OF_AMPS ,ISNULL(CASE WHEN CIRCUIT_BREAKERS=10963 THEN 1 WHEN CIRCUIT_BREAKERS=10964 THEN 0 END,0) CIRCUIT_BREAKERS                              
--  ,CASE WHEN FOUNDATION=3708 OR FOUNDATION=3710 THEN 1                              
--      WHEN FOUNDATION=3709 OR FOUNDATION=11694 THEN 2                              
--        WHEN FOUNDATION=3707  THEN 3 END FOUNDATION                              
--  ,ISNULL(CASE WHEN OCCUPANCY=8962 THEN 1                               
--  WHEN OCCUPANCY=8963 THEN 2                              
--  WHEN OCCUPANCY=5625 THEN 3                              
--  WHEN OCCUPANCY=8964 THEN 4                        
--  else 1    
-- END,0) OCCUPANCY_CODE                   
--  ,CASE WHEN NEIGHBOURS_VISIBLE='Y' THEN 1 ELSE 0 END NEIGHBOURS_VISIBLE,AHOGI.SWIMMING_POOL,                              
                              
                              
--  CASE WHEN AHOGI.SWIMMING_POOL=1 THEN AHOGI.SWIMMING_POOL_TYPE ELSE 2 END   SWIMMING_POOL_TYPE,     
                              
                              
--  APPROVED_FENCE,DIVING_BOARD,SLIDE                              
--  ,CASE WHEN LAST_INSPECTED_DATE IS NOT NULL THEN 1 ELSE 0 END INSPECTED                          
--  ,ISNULL(CASE WHEN OCCUPIED_DAILY='Y' THEN 1 WHEN OCCUPIED_DAILY='N' THEN 0 ELSE -1 END,-1) OCCUPIED_DAILY,NO_WEEKS_RENTED,isnull(MV.LOOKUP_VALUE_DESC,'') LOOKUP_VALUE_DESC,isnull(ROOF_OTHER_DESC,'') ROOF_OTHER_DESC                              
--  ,ISNULL(CASE WHEN SPRINKER=11695 THEN 1                              
--  WHEN SPRINKER=11696 THEN 2 END,0) SPRINKER,AHRI.EXTERIOR_CONSTRUCTION,ALC.LOC_COUNTY COUNTY,ADI.DWELLING_ID                                  
--  ,CASE WHEN (AL.POLICY_TYPE=11402 OR AL.POLICY_TYPE=11403) OR (AL.POLICY_TYPE=11193 OR AL.POLICY_TYPE=11192) THEN 'HO-2'                              
--        WHEN (AL.POLICY_TYPE=11409 OR AL.POLICY_TYPE=11400 OR AL.POLICY_TYPE=11404) OR  (AL.POLICY_TYPE=11194 OR AL.POLICY_TYPE=11148) THEN 'HO-3'                              
--        WHEN (AL.POLICY_TYPE=11405) OR (AL.POLICY_TYPE=11195) THEN 'HO-4'                              
--        WHEN (AL.POLICY_TYPE=11410 OR AL.POLICY_TYPE=11401) OR (AL.POLICY_TYPE=11149) THEN 'HO-5'                              
--        WHEN AL.POLICY_TYPE=11406 OR (AL.POLICY_TYPE=11196) THEN 'HO-6'                               
                              
--        WHEN (AL.POLICY_TYPE=11479 OR AL.POLICY_TYPE=11480) OR (AL.POLICY_TYPE=11289 OR AL.POLICY_TYPE=11290)  THEN 'DP-2'                               
--        WHEN (AL.POLICY_TYPE=11481 OR AL.POLICY_TYPE=11482) OR (AL.POLICY_TYPE=11291 OR AL.POLICY_TYPE=11292 OR AL.POLICY_TYPE=11458) THEN 'DP-3'                             
--   END HO_FORM                              
--        ,CCL.CUSTOMER_INSURANCE_SCORE,                               
--   CASE WHEN CCL.CUSTOMER_INSURANCE_SCORE < 600  THEN 'Base'                              
--        WHEN (CCL.CUSTOMER_INSURANCE_SCORE >= 600 and CCL.CUSTOMER_INSURANCE_SCORE < 700) OR (CCL.CUSTOMER_INSURANCE_SCORE IS NULL )  THEN 'Middle'                              
--           WHEN CCL.CUSTOMER_INSURANCE_SCORE >= 700 then 'Best'                              
--        end CUSTOMER_INSURANCE_SCORE_TYPE                                
--   ,case when isnull(AHOGI.IS_SWIMPOLL_HOTTUB,0)=1 then 'Yes'                               
--         when isnull(AHOGI.IS_SWIMPOLL_HOTTUB,0)=0 then 'No'                               
--    end IS_SWIMPOLL_HOTTUB                           
--   ,AHRI.ALARM_CERT_ATTACHED                              
--        ,ISNULL(MV2.LOOKUP_VALUE_DESC,'') CONSTRUCTION_TYPE,                              
--   CASE WHEN HYDRANT_DIST = 11556 THEN '&gt; 1000' ELSE '&lt; 1000' END HYDRANTDEC,                              
--  MLV1.LOOKUP_VALUE_DESC PHEAT_TYPE,  MLV2.LOOKUP_VALUE_DESC SHEAT_TYPE,                              
--  CASE WHEN (AL.POLICY_TYPE=11479 OR AL.POLICY_TYPE=11480) OR (AL.POLICY_TYPE=11289 OR AL.POLICY_TYPE=11290) THEN 1 END BROAD,                              
--  CASE WHEN (AL.POLICY_TYPE=11481 OR AL.POLICY_TYPE=11482) OR (AL.POLICY_TYPE=11291 OR AL.POLICY_TYPE=11292 OR AL.POLICY_TYPE=11458) THEN 1 END SPECIAL,                              
--  case when isnull(AHOGI.IS_RENTED_IN_PART,0)=1 then 'Yes'             
-- when isnull(AHOGI.IS_RENTED_IN_PART,0)=0 then 'No'                               
--    end IS_RENTED_IN_PART,          
-- case when isnull(AHOGI.IS_DWELLING_OWNED_BY_OTHER,0)=1 then 'Yes'             
-- when isnull(AHOGI.IS_DWELLING_OWNED_BY_OTHER,0)=0 then 'No'                               
--    end IS_DWELLING_OWNED_BY_OTHER,                              
--  AHOGI.DESC_RENTED_IN_PART,AHOGI.DESC_DWELLING_OWNED_BY_OTHER,AHOGI.DESC_IS_SWIMPOLL_HOTTUB                              
--  ,CASE WHEN EXTERIOR_CONSTRUCTION = 11300 OR EXTERIOR_CONSTRUCTION = 11368                               
--        THEN MV2.LOOKUP_VALUE_DESC ELSE '' END EXTERIOR_CONSTRUCTION_DESC,                              
--  AHRI.NEED_OF_UNITS 'RATE_NO_APTS',AHOGI.DESC_MULTI_POLICY_DISC_APPLIED, convert(varchar(10),AHRI.DWELLING_CONST_DATE,101) DWELLING_CONST_DATE                              
--  ,MLV4.LOOKUP_VALUE_DESC DWELLING_TYPE,                            
--  '' INFLATION_PRECENT, MTC.TERR                       
--  FROM APP_LIST AL WITH(NOLOCK)                    
--  LEFT OUTER JOIN APP_HOME_OWNER_GEN_INFO AHOGI WITH(NOLOCK) ON AL.CUSTOMER_ID=AHOGI.CUSTOMER_ID AND AL.APP_ID=AHOGI.APP_ID                              
-- AND AL.APP_VERSION_ID=AHOGI.APP_VERSION_ID                              
--  inner JOIN APP_DWELLINGS_INFO ADI WITH(NOLOCK) ON AL.CUSTOMER_ID=ADI.CUSTOMER_ID AND AL.APP_ID=ADI.APP_ID AND                               
--    AL.APP_VERSION_ID=ADI.APP_VERSION_ID                              
--  INNER JOIN APP_LOCATIONS ALC WITH(NOLOCK) ON AL.CUSTOMER_ID=ALC.CUSTOMER_ID AND AL.APP_ID=ALC.APP_ID AND                               
--    AL.APP_VERSION_ID=ALC.APP_VERSION_ID AND ALC.LOCATION_ID=ADI.LOCATION_ID       
--  LEFT OUTER JOIN MNT_TERRITORY_CODES MTC WITH(NOLOCK) ON CONVERT(NVARCHAR,MTC.LOBID) = AL.APP_LOB   
-- AND AL.APP_EFFECTIVE_DATE <= MTC.EFFECTIVE_TO_DATE AND  AL.APP_EFFECTIVE_DATE >= MTC.EFFECTIVE_FROM_DATE                         
--    AND CONVERT(NVARCHAR,MTC.STATE) = ALC.LOC_STATE AND MTC.ZIP = ALC.LOC_ZIP      
--  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL WITH(NOLOCK) ON ALC.LOC_STATE=SL.STATE_ID                              
----  LEFT OUTER JOIN INFLATION_COST_FACTORS ICF ON ICF.STATE_ID = ALC.LOC_STATE AND ALC.LOC_ZIP LIKE CONVERT(VARCHAR(10), ICF.ZIP_CODE) + '%'                            
--  LEFT OUTER JOIN APP_HOME_RATING_INFO AHRI WITH(NOLOCK) ON ADI.CUSTOMER_ID=AHRI.CUSTOMER_ID AND ADI.APP_ID=AHRI.APP_ID AND                               
--    ADI.APP_VERSION_ID=AHRI.APP_VERSION_ID AND ADI.DWELLING_ID=AHRI.DWELLING_ID                              
                                
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MV WITH(NOLOCK) ON MV.LOOKUP_ID=821 AND MV.LOOKUP_UNIQUE_ID = ROOF_TYPE                                
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MV1 WITH(NOLOCK) ON MV1.LOOKUP_ID=1006 AND MV1.LOOKUP_UNIQUE_ID = ADI.OCCUPANCY                              
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MV2 WITH(NOLOCK) ON MV2.LOOKUP_ID=1002 AND MV2.LOOKUP_UNIQUE_ID = AHRI.EXTERIOR_CONSTRUCTION                              
--  INNER JOIN CLT_CUSTOMER_LIST CCL WITH(NOLOCK) ON CCL.CUSTOMER_ID=AL.CUSTOMER_ID                              
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV WITH(NOLOCK) ON MLV.LOOKUP_ID=1221 AND MLV.LOOKUP_UNIQUE_ID=AHRI.CONSTRUCTION_CODE                              
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV1 WITH(NOLOCK) ON MLV1.LOOKUP_ID=764 AND MLV1.LOOKUP_UNIQUE_ID=AHRI.PRIMARY_HEAT_TYPE                              
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV2 WITH(NOLOCK) ON MLV2.LOOKUP_ID=764 AND MLV2.LOOKUP_UNIQUE_ID=AHRI.SECONDARY_HEAT_TYPE                              
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV3 WITH(NOLOCK) ON MLV3.LOOKUP_ID=1285 AND MLV3.LOOKUP_UNIQUE_ID=AHRI.PROT_CLASS                              
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV4 WITH(NOLOCK) ON MLV4.LOOKUP_ID=1005 AND MLV4.LOOKUP_UNIQUE_ID=BUILDING_TYPE                              
                                
--  WHERE AL.CUSTOMER_ID=@CUSTOMERID AND AL.APP_ID=@POLID AND AL.APP_VERSION_ID=@VERSIONID                                
-- and  ADI.is_active='Y'                        
--  ORDER BY ADI.DWELLING_ID   
----SELECT CONVERT(VARCHAR,CONVERT(MONEY,RECEIPT_AMOUNT),1) RECEIVED_PRMIUM  
---- FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID                          
----   SELECT '' AS TOTAL_DUE                          
-- END                              
-- ELSE IF (@CALLEDFROM='POLICY') 
IF (@CALLEDFROM='POLICY')   
 BEGIN                               
  SELECT DISTINCT PLC.LOC_ADD1,PLC.LOC_ADD2,PLC.LOC_CITY,SL.STATE_CODE,PLC.LOC_ZIP,                              
--  MV1.LOOKUP_VALUE_DESC OCCUPANCY,   
  MV1.LOOKUP_VALUE_DESC+' / '+cast(NO_OF_FAMILIES as varchar)+' Family'++'  / '+   
   case when LOCATION_TYPE=11848 then 'Rented'   
   when LOCATION_TYPE=11812 then 'Primary'  
   when LOCATION_TYPE=11813 then 'Seasonal'  
   when LOCATION_TYPE=11814 then 'Secondary'  
   when LOCATION_TYPE=11849 then 'Seasonal Rented'  
 -- case when LOCATION_TYPE=11812 or LOCATION_TYPE=11848 then 'Rented'   
 --  when LOCATION_TYPE=11813 then ''   
 --  when LOCATION_TYPE=11814 or LOCATION_TYPE=11849 then 'Seasonal Rented'   
End  OCCUPANCY,                 
                        
  isnull(case isnull(PLC.LOC_ADD1,'') when '' then '' else PLC.LOC_ADD1 end,'') +                    
  isnull(case isnull(PLC.LOC_ADD2,'') when '' then '' else ', ' + PLC.LOC_ADD2 end,'')                          
  LOC_ADDRESS,                              
       
  --PLC.LOC_ADD1 + ', ' + PLC.LOC_ADD2 LOC_ADDRESS,                        
                        
  isnull(case isnull(PLC.LOC_CITY,'') when '' then '' else PLC.LOC_CITY  + ',' end,'') + ' ' +                        
  isnull(case isnull(SL.STATE_CODE,'') when '' then '' else SL.STATE_CODE end,'') + ' ' +                        
  isnull(case isnull(PLC.LOC_ZIP,'') when '' then '' else PLC.LOC_ZIP end,'')                         
  LOC_CITYSTATEZIP,                         
                        
--  PLC.LOC_CITY + ', ' + SL.STATE_CODE + ', ' + PLC.LOC_ZIP LOC_CITYSTATEZIP,                              
  convert(varchar(10),LAST_INSPECTED_DATE,101) LAST_INSPECTED_DATE,                  
   CONVERT(VARCHAR,CONVERT(MONEY,ISNULL(RECEIVED_PRMIUM,'0')) ,1) RECEIVED_PRMIUM,  
1 BALANCE                              
  ,CASE WHEN BILL_TYPE ='DM' OR BILL_TYPE ='IM' OR BILL_TYPE ='MB' THEN 1                              
          WHEN BILL_TYPE ='DI' OR BILL_TYPE ='AM' OR BILL_TYPE ='AB' THEN 0 END BILLING                              
  ,CASE WHEN BILL_TYPE ='DM' OR BILL_TYPE ='IM' THEN 1                               
        WHEN BILL_TYPE ='MB' THEN 2                              
        ELSE 3 END DIRECT_BILL                              
  ,CASE WHEN INSTALL_PLAN_ID IS NULL THEN 0                              
        ELSE 1 END APPLICANT_BILL                              
  ,CASE WHEN EXTERIOR_CONSTRUCTION = 8934 OR EXTERIOR_CONSTRUCTION = 8935 OR EXTERIOR_CONSTRUCTION = 11809 OR EXTERIOR_CONSTRUCTION = 11851                               
   OR EXTERIOR_CONSTRUCTION = 11850 OR EXTERIOR_CONSTRUCTION = 11288 THEN 1                              
        WHEN EXTERIOR_CONSTRUCTION = 11301 OR EXTERIOR_CONSTRUCTION = 11302 OR EXTERIOR_CONSTRUCTION = 8936 THEN 2                              
        WHEN EXTERIOR_CONSTRUCTION = 11298 OR EXTERIOR_CONSTRUCTION = 11299 OR EXTERIOR_CONSTRUCTION = 8937 THEN 3                              
        WHEN EXTERIOR_CONSTRUCTION = 11810 THEN 4                              
        WHEN EXTERIOR_CONSTRUCTION = 11853 THEN 6                              
        WHEN EXTERIOR_CONSTRUCTION = 11852 THEN 7                               
        WHEN EXTERIOR_CONSTRUCTION = 11300 OR EXTERIOR_CONSTRUCTION = 11368 THEN 8 END EXTERIOR_CONSTRUCTION                                 
  ,PDI.YEAR_BUILT,                
convert(varchar,convert(money,PDI.MARKET_VALUE),1) MARKET_VALUE,                
PDI.NEED_OF_UNITS,                
convert(varchar,convert(money,PDI.REPLACEMENT_COST),1) REPLACEMENT_COST   ,                            
ISNULL(CASE WHEN BUILDING_TYPE = 8955 THEN 1                             
        WHEN BUILDING_TYPE = 8956 THEN 2                              
   WHEN BUILDING_TYPE = 8957 THEN 3                              
   WHEN BUILDING_TYPE = 8958 THEN 4                              
   WHEN BUILDING_TYPE = 8955 THEN 5                              
   WHEN BUILDING_TYPE = 8960 THEN 6 END,0) STRUCTURE_TYPE          
,CASE WHEN LOCATION_TYPE=11812 or LOCATION_TYPE=11848 THEN 1                              
  WHEN LOCATION_TYPE=11813  THEN 2                              
  WHEN LOCATION_TYPE=11814 or LOCATION_TYPE=11849  THEN 3 END USAGE_TYPE                              
  ,ANY_FORMING,IS_UNDER_CONSTRUCTION COC,convert(varchar(10),DWELLING_CONST_DATE,101) COMP_DATE,ISNULL(NO_OF_FAMILIES,'') NO_OF_FAMILIES,                      
   (CASE WHEN PURCHASE_YEAR IS NOT NULL THEN CAST(PURCHASE_YEAR AS VARCHAR) + '/' ELSE '' END) + (CASE WHEN PURCHASE_PRICE IS NOT NULL THEN                 
 '$ '+ convert(varchar,convert(money,PURCHASE_PRICE),1 )  ELSE '' END)  PUR_YR_PRICE                              
  ,ISNULL(MLV3.LOOKUP_VALUE_DESC,'') PROT_CLASS,HYDRANT_DIST,FIRE_STATION_DIST                              
  ,CENT_ST_BURG_FIRE,CENT_ST_FIRE,CENT_ST_BURG,DIR_FIRE_AND_POLICE,DIR_FIRE,DIR_POLICE,LOC_FIRE_GAS,TWO_MORE_FIRE,PRIMARY_HEAT_TYPE,SECONDARY_HEAT_TYPE                              
  ,isnull(PRIMARY_HEAT_OTHER_DESC,'') PRIMARY_HEAT_OTHER_DESC,isnull(SECONDARY_HEAT_OTHER_DESC,'') SECONDARY_HEAT_OTHER_DESC                              
  ,CASE WHEN WIRING_RENOVATION=8922 THEN 1                              
        WHEN WIRING_RENOVATION=8923 THEN 0 END WIRING,WIRING_UPDATE_YEAR                              
  ,CASE WHEN PLUMBING_RENOVATION=8922 THEN 1                              
        WHEN PLUMBING_RENOVATION=8923 THEN 0 END PLUMBING,PLUMBING_UPDATE_YEAR                              
  ,CASE WHEN HEATING_RENOVATION=8922 THEN 1                              
      WHEN HEATING_RENOVATION=8923 THEN 0 END HEATING,HEATING_UPDATE_YEAR                              
  ,CASE WHEN ROOFING_RENOVATION=8922 THEN 1                              
        WHEN ROOFING_RENOVATION=8923 THEN 0 END ROOFING,ROOFING_UPDATE_YEAR                              
                              
  ,case when NO_OF_AMPS = 14121 then 100 when NO_OF_AMPS = 14122 then 200 else isnull(NO_OF_AMPS,0) end NO_OF_AMPS ,ISNULL(CASE WHEN CIRCUIT_BREAKERS=10963 THEN 1 WHEN CIRCUIT_BREAKERS=10964 THEN 0 END,0) CIRCUIT_BREAKERS                              
  ,CASE WHEN FOUNDATION=3708 OR FOUNDATION=3710 THEN 1                              
        WHEN FOUNDATION=3709 OR FOUNDATION=11694 THEN 2                              
        WHEN FOUNDATION=3707  THEN 3 END FOUNDATION                              
  ,ISNULL(CASE WHEN OCCUPANCY=8962 THEN 1                               
  WHEN OCCUPANCY=8963 THEN 2                              
  WHEN OCCUPANCY=5625 THEN 3                              
  WHEN OCCUPANCY=8964 THEN 4                         
  else 1                         
END,0) OCCUPANCY_CODE                              
  ,CASE WHEN NEIGHBOURS_VISIBLE='Y' THEN 1 ELSE 0 END NEIGHBOURS_VISIBLE,PHOGI.SWIMMING_POOL,                              
                              
  CASE WHEN PHOGI.SWIMMING_POOL=1 THEN PHOGI.SWIMMING_POOL_TYPE ELSE 2 END   SWIMMING_POOL_TYPE,                              
                              
APPROVED_FENCE,DIVING_BOARD,SLIDE                              
  ,CASE WHEN LAST_INSPECTED_DATE IS NOT NULL THEN 1 ELSE 0 END INSPECTED                              
  ,ISNULL(CASE WHEN OCCUPIED_DAILY='Y' THEN 1 WHEN OCCUPIED_DAILY='N' THEN 0 ELSE -1 END,-1) OCCUPIED_DAILY,NO_WEEKS_RENTED,isnull(MV.LOOKUP_VALUE_DESC,'') LOOKUP_VALUE_DESC,isnull(ROOF_OTHER_DESC,'') ROOF_OTHER_DESC                              
  ,ISNULL(CASE WHEN SPRINKER=11695 THEN 1                              
  WHEN SPRINKER=11696 THEN 2 END,0) SPRINKER,PHRI.EXTERIOR_CONSTRUCTION,PLC.LOC_COUNTY COUNTY,PDI.DWELLING_ID                                  
  ,CASE WHEN (PL.POLICY_TYPE=11402 OR PL.POLICY_TYPE=11403) OR (PL.POLICY_TYPE=11193 OR PL.POLICY_TYPE=11192) THEN 'HO-2'                              
        WHEN (PL.POLICY_TYPE=11409 OR PL.POLICY_TYPE=11400 OR PL.POLICY_TYPE=11404) OR  (PL.POLICY_TYPE=11194 OR PL.POLICY_TYPE=11148) THEN 'HO-3'                              
        WHEN (PL.POLICY_TYPE=11405) OR (PL.POLICY_TYPE=11195) THEN 'HO-4'                              
        WHEN (PL.POLICY_TYPE=11410 OR PL.POLICY_TYPE=11401) OR (PL.POLICY_TYPE=11149) THEN 'HO-5'                
        WHEN PL.POLICY_TYPE=11406 OR (PL.POLICY_TYPE=11196) THEN 'HO-6'                               
                              
        WHEN (PL.POLICY_TYPE=11479 OR PL.POLICY_TYPE=11480) OR (PL.POLICY_TYPE=11289 OR PL.POLICY_TYPE=11290)  THEN 'DP-2'                               
        WHEN (PL.POLICY_TYPE=11481 OR PL.POLICY_TYPE=11482) OR (PL.POLICY_TYPE=11291 OR PL.POLICY_TYPE=11292 OR PL.POLICY_TYPE=11458) THEN 'DP-3'                               
   END HO_FORM                              
   ,CCL.CUSTOMER_INSURANCE_SCORE,                               
      CASE WHEN CCL.CUSTOMER_INSURANCE_SCORE < 600  THEN 'Base'                     
      WHEN (CCL.CUSTOMER_INSURANCE_SCORE >= 600 and CCL.CUSTOMER_INSURANCE_SCORE < 700) OR (CCL.CUSTOMER_INSURANCE_SCORE IS NULL )  THEN 'Middle'                              
    WHEN CCL.CUSTOMER_INSURANCE_SCORE >= 700 then 'Best'                              
      end CUSTOMER_INSURANCE_SCORE_TYPE                                
      ,case when isnull(PHOGI.IS_SWIMPOLL_HOTTUB,0)=1 then 'Yes'                               
       when isnull(PHOGI.IS_SWIMPOLL_HOTTUB,0)=0 then 'No'                               
    end IS_SWIMPOLL_HOTTUB                              
                              
                              
,PHRI.ALARM_CERT_ATTACHED                              
  ,ISNULL(MV2.LOOKUP_VALUE_DESC,'') CONSTRUCTION_TYPE,                              
   CASE WHEN HYDRANT_DIST = 11556 THEN '&gt; 1000' ELSE '&lt; 1000' END HYDRANTDEC,                              
  MLV1.LOOKUP_VALUE_DESC PHEAT_TYPE,MLV2.LOOKUP_VALUE_DESC SHEAT_TYPE,                              
  CASE WHEN (PL.POLICY_TYPE=11479 OR PL.POLICY_TYPE=11480) OR (PL.POLICY_TYPE=11289 OR PL.POLICY_TYPE=11290) THEN 1 END BROAD,                              
  CASE WHEN (PL.POLICY_TYPE=11481 OR PL.POLICY_TYPE=11482) OR (PL.POLICY_TYPE=11291 OR PL.POLICY_TYPE=11292 OR PL.POLICY_TYPE=11458) THEN 1 END SPECIAL,                              
  case when isnull(PHOGI.IS_RENTED_IN_PART,0)=1 then 'Yes'             
 when isnull(PHOGI.IS_RENTED_IN_PART,0)=0 then 'No'                               
    end IS_RENTED_IN_PART,          
 case when isnull(PHOGI.IS_DWELLING_OWNED_BY_OTHER,0)=1 then 'Yes'             
 when isnull(PHOGI.IS_DWELLING_OWNED_BY_OTHER,0)=0 then 'No'                               
    end IS_DWELLING_OWNED_BY_OTHER,                              
  PHOGI.DESC_RENTED_IN_PART,PHOGI.DESC_DWELLING_OWNED_BY_OTHER,PHOGI.DESC_IS_SWIMPOLL_HOTTUB                              
  ,CASE WHEN EXTERIOR_CONSTRUCTION = 11300 OR EXTERIOR_CONSTRUCTION = 11368                               
        THEN MV2.LOOKUP_VALUE_DESC ELSE '' END EXTERIOR_CONSTRUCTION_DESC                               
  ,PHRI.NEED_OF_UNITS 'RATE_NO_APTS',PHOGI.DESC_MULTI_POLICY_DISC_APPLIED,  convert(varchar(10),PHRI.DWELLING_CONST_DATE,101) DWELLING_CONST_DATE                                
  ,MLV4.LOOKUP_VALUE_DESC DWELLING_TYPE,                            
  CASE WHEN PDI.INFLATION_FACTOR IS NOT NULL THEN ', Inflation %: ' + CONVERT(VARCHAR(10),PDI.INFLATION_FACTOR) + ' (Minimum 1000)' ELSE '' END AS INFLATION_PRECENT                         
  , MTC.TERR      
  FROM POL_CUSTOMER_POLICY_LIST PL WITH(NOLOCK)                             
  LEFT OUTER JOIN POL_HOME_OWNER_GEN_INFO PHOGI WITH(NOLOCK) ON PL.CUSTOMER_ID=PHOGI.CUSTOMER_ID AND PL.POLICY_ID=PHOGI.POLICY_ID             
    AND PL.POLICY_VERSION_ID=PHOGI.POLICY_VERSION_ID                              
  INNER JOIN POL_DWELLINGS_INFO PDI WITH(NOLOCK) ON PL.CUSTOMER_ID=PDI.CUSTOMER_ID AND PL.POLICY_ID=PDI.POLICY_ID AND                               
    PL.POLICY_VERSION_ID=PDI.POLICY_VERSION_ID                              
  INNER JOIN POL_LOCATIONS PLC WITH(NOLOCK) ON PL.CUSTOMER_ID=PLC.CUSTOMER_ID AND PL.POLICY_ID=PLC.POLICY_ID AND   
    PL.POLICY_VERSION_ID=PLC.POLICY_VERSION_ID AND PLC.LOCATION_ID=PDI.LOCATION_ID                              
  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL WITH(NOLOCK) ON PLC.LOC_STATE=SL.STATE_ID                              
--  LEFT OUTER JOIN INFLATION_COST_FACTORS ICF ON ICF.STATE_ID = PLC.LOC_STATE AND PLC.LOC_ZIP LIKE CONVERT(VARCHAR(10), ICF.ZIP_CODE) + '%'                            
  LEFT OUTER JOIN MNT_TERRITORY_CODES MTC WITH(NOLOCK) ON CONVERT(NVARCHAR,MTC.LOBID) = PL.POLICY_LOB                             
    AND CONVERT(NVARCHAR,MTC.STATE) = PLC.LOC_STATE AND MTC.ZIP = PLC.LOC_ZIP      
 AND PL.APP_EFFECTIVE_DATE <= MTC.EFFECTIVE_TO_DATE AND  PL.APP_EFFECTIVE_DATE >= MTC.EFFECTIVE_FROM_DATE                         
   LEFT OUTER JOIN POL_HOME_RATING_INFO PHRI WITH(NOLOCK) ON PDI.CUSTOMER_ID=PHRI.CUSTOMER_ID AND PDI.POLICY_ID=PHRI.POLICY_ID AND                               
    PDI.POLICY_VERSION_ID=PHRI.POLICY_VERSION_ID AND PDI.DWELLING_ID=PHRI.DWELLING_ID                              
                              
                              
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MV WITH(NOLOCK) ON MV.LOOKUP_ID=821 AND MV.LOOKUP_UNIQUE_ID = ROOF_TYPE                              
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MV1 WITH(NOLOCK) ON MV1.LOOKUP_ID=1006 AND MV1.LOOKUP_UNIQUE_ID = PDI.OCCUPANCY                              
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MV2 WITH(NOLOCK) ON MV2.LOOKUP_ID=1002 AND MV2.LOOKUP_UNIQUE_ID = PHRI.EXTERIOR_CONSTRUCTION                              
  INNER JOIN CLT_CUSTOMER_LIST CCL WITH(NOLOCK) ON CCL.CUSTOMER_ID=PL.CUSTOMER_ID                              
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV WITH(NOLOCK) ON MLV.LOOKUP_ID=1221 AND MLV.LOOKUP_UNIQUE_ID=PHRI.CONSTRUCTION_CODE                              
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV1 WITH(NOLOCK) ON MLV1.LOOKUP_ID=764 AND MLV1.LOOKUP_UNIQUE_ID=PHRI.PRIMARY_HEAT_TYPE                              
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV2 WITH(NOLOCK) ON MLV2.LOOKUP_ID=764 AND MLV2.LOOKUP_UNIQUE_ID=PHRI.SECONDARY_HEAT_TYPE                              
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV3 WITH(NOLOCK) ON MLV3.LOOKUP_ID=1285 AND MLV3.LOOKUP_UNIQUE_ID=PHRI.PROT_CLASS                              
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV4 WITH(NOLOCK) ON MLV4.LOOKUP_ID=1005 AND MLV4.LOOKUP_UNIQUE_ID=BUILDING_TYPE                              
                              
  WHERE PL.CUSTOMER_ID=@CUSTOMERID AND PL.POLICY_ID=@POLID AND PL.POLICY_VERSION_ID=@VERSIONID                               
  and  PDI.is_active='Y'                        
  ORDER BY PDI.DWELLING_ID      
--SELECT CONVERT(VARCHAR,CONVERT(MONEY,RECEIPT_AMOUNT),1) RECEIVED_PRMIUM  
-- FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID     
--              
----Use Accounting SP for Total and Minimum Due 6-Nov-2007              
--CREATE TABLE #AMOUNT_DUE   
--(  
-- MINIMUM_DUE  DECIMAL(18,2),                                              
-- TOTAL_DUE  DECIMAL(18,2),                                              
-- AGENCY_ID  INT,              
-- AGENCYCODE  VARCHAR(20),  
-- PREM   Decimal(18,2) ,   
-- FEE    Decimal(18,2),              
-- FIRST_INS_FEE Decimal(18,2)  
--)            
--INSERT INTO #AMOUNT_DUE exec Proc_GetTotalAndMinimumDue @CUSTOMERID,@POLID,@VERSIONID , null              
--              
--SELECT TOTAL_DUE as TOTAL_DUE FROM #AMOUNT_DUE WITH(NOLOCK)              
--DROP TABLE #AMOUNT_DUE              
 END   
ELSE IF(@CALLEDFROM='NOTICE')  
 BEGIN  
  DECLARE @MAXVERSION NVARCHAR(20)  
  DECLARE @CURRENT_TERM INT  
  SELECT @CURRENT_TERM=CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID  
  SELECT  @MAXVERSION=MAX(POLICY_VERSION_ID) FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND CURRENT_TERM=@CURRENT_TERM  
  SELECT DISTINCT PLC.LOC_ADD1,PLC.LOC_ADD2,PLC.LOC_CITY,SL.STATE_CODE,PLC.LOC_ZIP,                              
  --  MV1.LOOKUP_VALUE_DESC OCCUPANCY,   
    MV1.LOOKUP_VALUE_DESC+' / '+cast(NO_OF_FAMILIES as varchar)+' Family'++'  / '+   
     case when LOCATION_TYPE=11848 then 'Rented'   
     when LOCATION_TYPE=11812 then 'Primary'  
     when LOCATION_TYPE=11813 then 'Seasonal'  
     when LOCATION_TYPE=11814 then 'Secondary'  
     when LOCATION_TYPE=11849 then 'Seasonal Rented'  
   -- case when LOCATION_TYPE=11812 or LOCATION_TYPE=11848 then 'Rented'   
   --  when LOCATION_TYPE=11813 then ''   
   --  when LOCATION_TYPE=11814 or LOCATION_TYPE=11849 then 'Seasonal Rented'   
  End  OCCUPANCY,                 
                          
    isnull(case isnull(PLC.LOC_ADD1,'') when '' then '' else PLC.LOC_ADD1 end,'') +                    
    isnull(case isnull(PLC.LOC_ADD2,'') when '' then '' else ', ' + PLC.LOC_ADD2 end,'')                          
    LOC_ADDRESS,                              
         
    --PLC.LOC_ADD1 + ', ' + PLC.LOC_ADD2 LOC_ADDRESS,                        
                          
    isnull(case isnull(PLC.LOC_CITY,'') when '' then '' else PLC.LOC_CITY  + ',' end,'') + ' ' +                        
    isnull(case isnull(SL.STATE_CODE,'') when '' then '' else SL.STATE_CODE end,'') + ' ' +                        
    isnull(case isnull(PLC.LOC_ZIP,'') when '' then '' else PLC.LOC_ZIP end,'')                         
    LOC_CITYSTATEZIP,                         
                          
  --  PLC.LOC_CITY + ', ' + SL.STATE_CODE + ', ' + PLC.LOC_ZIP LOC_CITYSTATEZIP,                              
    convert(varchar(10),LAST_INSPECTED_DATE,101) LAST_INSPECTED_DATE,                  
     CONVERT(VARCHAR,CONVERT(MONEY,ISNULL(RECEIVED_PRMIUM,'0')) ,1) RECEIVED_PRMIUM,  
  1 BALANCE                              
    ,CASE WHEN BILL_TYPE ='DM' OR BILL_TYPE ='IM' OR BILL_TYPE ='MB' THEN 1                              
      WHEN BILL_TYPE ='DI' OR BILL_TYPE ='AM' OR BILL_TYPE ='AB' THEN 0 END BILLING                              
    ,CASE WHEN BILL_TYPE ='DM' OR BILL_TYPE ='IM' THEN 1                               
    WHEN BILL_TYPE ='MB' THEN 2                              
    ELSE 3 END DIRECT_BILL                              
    ,CASE WHEN INSTALL_PLAN_ID IS NULL THEN 0                              
    ELSE 1 END APPLICANT_BILL                              
    ,CASE WHEN EXTERIOR_CONSTRUCTION = 8934 OR EXTERIOR_CONSTRUCTION = 8935 OR EXTERIOR_CONSTRUCTION = 11809 OR EXTERIOR_CONSTRUCTION = 11851                               
     OR EXTERIOR_CONSTRUCTION = 11850 OR EXTERIOR_CONSTRUCTION = 11288 THEN 1                              
    WHEN EXTERIOR_CONSTRUCTION = 11301 OR EXTERIOR_CONSTRUCTION = 11302 OR EXTERIOR_CONSTRUCTION = 8936 THEN 2                              
    WHEN EXTERIOR_CONSTRUCTION = 11298 OR EXTERIOR_CONSTRUCTION = 11299 OR EXTERIOR_CONSTRUCTION = 8937 THEN 3                              
    WHEN EXTERIOR_CONSTRUCTION = 11810 THEN 4                              
    WHEN EXTERIOR_CONSTRUCTION = 11853 THEN 6                              
    WHEN EXTERIOR_CONSTRUCTION = 11852 THEN 7                               
    WHEN EXTERIOR_CONSTRUCTION = 11300 OR EXTERIOR_CONSTRUCTION = 11368 THEN 8 END EXTERIOR_CONSTRUCTION                                 
    ,PDI.YEAR_BUILT,                
  convert(varchar,convert(money,PDI.MARKET_VALUE),1) MARKET_VALUE,                
  PDI.NEED_OF_UNITS,                
  convert(varchar,convert(money,PDI.REPLACEMENT_COST),1) REPLACEMENT_COST   ,                            
  ISNULL(CASE WHEN BUILDING_TYPE = 8955 THEN 1                             
    WHEN BUILDING_TYPE = 8956 THEN 2                              
     WHEN BUILDING_TYPE = 8957 THEN 3                              
     WHEN BUILDING_TYPE = 8958 THEN 4                              
     WHEN BUILDING_TYPE = 8955 THEN 5                              
     WHEN BUILDING_TYPE = 8960 THEN 6 END,0) STRUCTURE_TYPE          
  ,CASE WHEN LOCATION_TYPE=11812 or LOCATION_TYPE=11848 THEN 1                              
    WHEN LOCATION_TYPE=11813  THEN 2          
    WHEN LOCATION_TYPE=11814 or LOCATION_TYPE=11849  THEN 3 END USAGE_TYPE                              
    ,ANY_FORMING,IS_UNDER_CONSTRUCTION COC,convert(varchar(10),DWELLING_CONST_DATE,101) COMP_DATE,ISNULL(NO_OF_FAMILIES,'') NO_OF_FAMILIES,                      
     (CASE WHEN PURCHASE_YEAR IS NOT NULL THEN CAST(PURCHASE_YEAR AS VARCHAR) + '/' ELSE '' END) + (CASE WHEN PURCHASE_PRICE IS NOT NULL THEN                 
   '$ '+ convert(varchar,convert(money,PURCHASE_PRICE),1 )  ELSE '' END)  PUR_YR_PRICE                              
    ,ISNULL(MLV3.LOOKUP_VALUE_DESC,'') PROT_CLASS,HYDRANT_DIST,FIRE_STATION_DIST                              
    ,CENT_ST_BURG_FIRE,CENT_ST_FIRE,CENT_ST_BURG,DIR_FIRE_AND_POLICE,DIR_FIRE,DIR_POLICE,LOC_FIRE_GAS,TWO_MORE_FIRE,PRIMARY_HEAT_TYPE,SECONDARY_HEAT_TYPE                              
    ,isnull(PRIMARY_HEAT_OTHER_DESC,'') PRIMARY_HEAT_OTHER_DESC,isnull(SECONDARY_HEAT_OTHER_DESC,'') SECONDARY_HEAT_OTHER_DESC                              
    ,CASE WHEN WIRING_RENOVATION=8922 THEN 1                              
    WHEN WIRING_RENOVATION=8923 THEN 0 END WIRING,WIRING_UPDATE_YEAR                              
    ,CASE WHEN PLUMBING_RENOVATION=8922 THEN 1                              
    WHEN PLUMBING_RENOVATION=8923 THEN 0 END PLUMBING,PLUMBING_UPDATE_YEAR                              
    ,CASE WHEN HEATING_RENOVATION=8922 THEN 1                              
     WHEN HEATING_RENOVATION=8923 THEN 0 END HEATING,HEATING_UPDATE_YEAR                              
    ,CASE WHEN ROOFING_RENOVATION=8922 THEN 1                              
    WHEN ROOFING_RENOVATION=8923 THEN 0 END ROOFING,ROOFING_UPDATE_YEAR                              
                                
    ,case when NO_OF_AMPS = 14121 then 100 when NO_OF_AMPS = 14122 then 200 else isnull(NO_OF_AMPS,0) end NO_OF_AMPS ,ISNULL(CASE WHEN CIRCUIT_BREAKERS=10963 THEN 1 WHEN CIRCUIT_BREAKERS=10964 THEN 0 END,0) CIRCUIT_BREAKERS                              
    ,CASE WHEN FOUNDATION=3708 OR FOUNDATION=3710 THEN 1                              
    WHEN FOUNDATION=3709 OR FOUNDATION=11694 THEN 2                              
    WHEN FOUNDATION=3707  THEN 3 END FOUNDATION                              
    ,ISNULL(CASE WHEN OCCUPANCY=8962 THEN 1                               
    WHEN OCCUPANCY=8963 THEN 2                              
    WHEN OCCUPANCY=5625 THEN 3                              
    WHEN OCCUPANCY=8964 THEN 4                         
    else 1                         
  END,0) OCCUPANCY_CODE                              
    ,CASE WHEN NEIGHBOURS_VISIBLE='Y' THEN 1 ELSE 0 END NEIGHBOURS_VISIBLE,PHOGI.SWIMMING_POOL,                              
                                
    CASE WHEN PHOGI.SWIMMING_POOL=1 THEN PHOGI.SWIMMING_POOL_TYPE ELSE 2 END   SWIMMING_POOL_TYPE,                              
                                
  APPROVED_FENCE,DIVING_BOARD,SLIDE                              
    ,CASE WHEN LAST_INSPECTED_DATE IS NOT NULL THEN 1 ELSE 0 END INSPECTED                              
    ,ISNULL(CASE WHEN OCCUPIED_DAILY='Y' THEN 1 WHEN OCCUPIED_DAILY='N' THEN 0 ELSE -1 END,-1) OCCUPIED_DAILY,NO_WEEKS_RENTED,isnull(MV.LOOKUP_VALUE_DESC,'') LOOKUP_VALUE_DESC,isnull(ROOF_OTHER_DESC,'') ROOF_OTHER_DESC                              
    ,ISNULL(CASE WHEN SPRINKER=11695 THEN 1                              
    WHEN SPRINKER=11696 THEN 2 END,0) SPRINKER,PHRI.EXTERIOR_CONSTRUCTION,PLC.LOC_COUNTY COUNTY,PDI.DWELLING_ID                                  
    ,CASE WHEN (PL.POLICY_TYPE=11402 OR PL.POLICY_TYPE=11403) OR (PL.POLICY_TYPE=11193 OR PL.POLICY_TYPE=11192) THEN 'HO-2'                              
    WHEN (PL.POLICY_TYPE=11409 OR PL.POLICY_TYPE=11400 OR PL.POLICY_TYPE=11404) OR  (PL.POLICY_TYPE=11194 OR PL.POLICY_TYPE=11148) THEN 'HO-3'                              
    WHEN (PL.POLICY_TYPE=11405) OR (PL.POLICY_TYPE=11195) THEN 'HO-4'                              
    WHEN (PL.POLICY_TYPE=11410 OR PL.POLICY_TYPE=11401) OR (PL.POLICY_TYPE=11149) THEN 'HO-5'                
    WHEN PL.POLICY_TYPE=11406 OR (PL.POLICY_TYPE=11196) THEN 'HO-6'                               
                                
    WHEN (PL.POLICY_TYPE=11479 OR PL.POLICY_TYPE=11480) OR (PL.POLICY_TYPE=11289 OR PL.POLICY_TYPE=11290)  THEN 'DP-2'                               
    WHEN (PL.POLICY_TYPE=11481 OR PL.POLICY_TYPE=11482) OR (PL.POLICY_TYPE=11291 OR PL.POLICY_TYPE=11292 OR PL.POLICY_TYPE=11458) THEN 'DP-3'                               
     END HO_FORM                              
     ,CCL.CUSTOMER_INSURANCE_SCORE,                               
     CASE WHEN CCL.CUSTOMER_INSURANCE_SCORE < 600  THEN 'Base'                     
     WHEN (CCL.CUSTOMER_INSURANCE_SCORE >= 600 and CCL.CUSTOMER_INSURANCE_SCORE < 700) OR (CCL.CUSTOMER_INSURANCE_SCORE IS NULL )  THEN 'Middle'                              
   WHEN CCL.CUSTOMER_INSURANCE_SCORE >= 700 then 'Best'                              
     end CUSTOMER_INSURANCE_SCORE_TYPE                                
     ,case when isnull(PHOGI.IS_SWIMPOLL_HOTTUB,0)=1 then 'Yes'                               
      when isnull(PHOGI.IS_SWIMPOLL_HOTTUB,0)=0 then 'No'                               
   end IS_SWIMPOLL_HOTTUB                              
                                
                                
  ,PHRI.ALARM_CERT_ATTACHED                              
    ,ISNULL(MV2.LOOKUP_VALUE_DESC,'') CONSTRUCTION_TYPE,                              
     CASE WHEN HYDRANT_DIST = 11556 THEN '&gt; 1000' ELSE '&lt; 1000' END HYDRANTDEC,                              
    MLV1.LOOKUP_VALUE_DESC PHEAT_TYPE,MLV2.LOOKUP_VALUE_DESC SHEAT_TYPE,                              
    CASE WHEN (PL.POLICY_TYPE=11479 OR PL.POLICY_TYPE=11480) OR (PL.POLICY_TYPE=11289 OR PL.POLICY_TYPE=11290) THEN 1 END BROAD,                              
    CASE WHEN (PL.POLICY_TYPE=11481 OR PL.POLICY_TYPE=11482) OR (PL.POLICY_TYPE=11291 OR PL.POLICY_TYPE=11292 OR PL.POLICY_TYPE=11458) THEN 1 END SPECIAL,                              
    case when isnull(PHOGI.IS_RENTED_IN_PART,0)=1 then 'Yes'             
   when isnull(PHOGI.IS_RENTED_IN_PART,0)=0 then 'No'                               
   end IS_RENTED_IN_PART,          
   case when isnull(PHOGI.IS_DWELLING_OWNED_BY_OTHER,0)=1 then 'Yes'             
   when isnull(PHOGI.IS_DWELLING_OWNED_BY_OTHER,0)=0 then 'No'                               
   end IS_DWELLING_OWNED_BY_OTHER,                              
    PHOGI.DESC_RENTED_IN_PART,PHOGI.DESC_DWELLING_OWNED_BY_OTHER,PHOGI.DESC_IS_SWIMPOLL_HOTTUB                              
    ,CASE WHEN EXTERIOR_CONSTRUCTION = 11300 OR EXTERIOR_CONSTRUCTION = 11368                               
    THEN MV2.LOOKUP_VALUE_DESC ELSE '' END EXTERIOR_CONSTRUCTION_DESC                               
    ,PHRI.NEED_OF_UNITS 'RATE_NO_APTS',PHOGI.DESC_MULTI_POLICY_DISC_APPLIED,  convert(varchar(10),PHRI.DWELLING_CONST_DATE,101) DWELLING_CONST_DATE                                
    ,MLV4.LOOKUP_VALUE_DESC DWELLING_TYPE,                            
    CASE WHEN PDI.INFLATION_FACTOR IS NOT NULL THEN ', Inflation %: ' + CONVERT(VARCHAR(10),PDI.INFLATION_FACTOR) + ' (Minimum 1000)' ELSE '' END AS INFLATION_PRECENT                         
    , MTC.TERR      
    FROM POL_CUSTOMER_POLICY_LIST PL WITH(NOLOCK)                             
    LEFT OUTER JOIN POL_HOME_OWNER_GEN_INFO PHOGI WITH(NOLOCK) ON PL.CUSTOMER_ID=PHOGI.CUSTOMER_ID AND PL.POLICY_ID=PHOGI.POLICY_ID             
   AND PL.POLICY_VERSION_ID=PHOGI.POLICY_VERSION_ID                              
    INNER JOIN POL_DWELLINGS_INFO PDI WITH(NOLOCK) ON PL.CUSTOMER_ID=PDI.CUSTOMER_ID AND PL.POLICY_ID=PDI.POLICY_ID AND                               
   PL.POLICY_VERSION_ID=PDI.POLICY_VERSION_ID                              
    INNER JOIN POL_LOCATIONS PLC WITH(NOLOCK) ON PL.CUSTOMER_ID=PLC.CUSTOMER_ID AND PL.POLICY_ID=PLC.POLICY_ID AND   
   PL.POLICY_VERSION_ID=PLC.POLICY_VERSION_ID AND PLC.LOCATION_ID=PDI.LOCATION_ID          
    LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL WITH(NOLOCK) ON PLC.LOC_STATE=SL.STATE_ID                              
  --  LEFT OUTER JOIN INFLATION_COST_FACTORS ICF ON ICF.STATE_ID = PLC.LOC_STATE AND PLC.LOC_ZIP LIKE CONVERT(VARCHAR(10), ICF.ZIP_CODE) + '%'                            
    LEFT OUTER JOIN MNT_TERRITORY_CODES MTC WITH(NOLOCK) ON CONVERT(NVARCHAR,MTC.LOBID) = PL.POLICY_LOB                             
   AND CONVERT(NVARCHAR,MTC.STATE) = PLC.LOC_STATE AND MTC.ZIP = PLC.LOC_ZIP      
   AND PL.APP_EFFECTIVE_DATE <= MTC.EFFECTIVE_TO_DATE AND  PL.APP_EFFECTIVE_DATE >= MTC.EFFECTIVE_FROM_DATE                         
     LEFT OUTER JOIN POL_HOME_RATING_INFO PHRI WITH(NOLOCK) ON PDI.CUSTOMER_ID=PHRI.CUSTOMER_ID AND PDI.POLICY_ID=PHRI.POLICY_ID AND                               
   PDI.POLICY_VERSION_ID=PHRI.POLICY_VERSION_ID AND PDI.DWELLING_ID=PHRI.DWELLING_ID                              
                                
                                
    LEFT OUTER JOIN MNT_LOOKUP_VALUES MV WITH(NOLOCK) ON MV.LOOKUP_ID=821 AND MV.LOOKUP_UNIQUE_ID = ROOF_TYPE                              
    LEFT OUTER JOIN MNT_LOOKUP_VALUES MV1 WITH(NOLOCK) ON MV1.LOOKUP_ID=1006 AND MV1.LOOKUP_UNIQUE_ID = PDI.OCCUPANCY                              
    LEFT OUTER JOIN MNT_LOOKUP_VALUES MV2 WITH(NOLOCK) ON MV2.LOOKUP_ID=1002 AND MV2.LOOKUP_UNIQUE_ID = PHRI.EXTERIOR_CONSTRUCTION                              
    INNER JOIN CLT_CUSTOMER_LIST CCL WITH(NOLOCK) ON CCL.CUSTOMER_ID=PL.CUSTOMER_ID                              
    LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV WITH(NOLOCK) ON MLV.LOOKUP_ID=1221 AND MLV.LOOKUP_UNIQUE_ID=PHRI.CONSTRUCTION_CODE                              
    LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV1 WITH(NOLOCK) ON MLV1.LOOKUP_ID=764 AND MLV1.LOOKUP_UNIQUE_ID=PHRI.PRIMARY_HEAT_TYPE                              
    LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV2 WITH(NOLOCK) ON MLV2.LOOKUP_ID=764 AND MLV2.LOOKUP_UNIQUE_ID=PHRI.SECONDARY_HEAT_TYPE                              
    LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV3 WITH(NOLOCK) ON MLV3.LOOKUP_ID=1285 AND MLV3.LOOKUP_UNIQUE_ID=PHRI.PROT_CLASS                              
    LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV4 WITH(NOLOCK) ON MLV4.LOOKUP_ID=1005 AND MLV4.LOOKUP_UNIQUE_ID=BUILDING_TYPE                              
                                
    WHERE PL.CUSTOMER_ID=@CUSTOMERID AND PL.POLICY_ID=@POLID AND PL.POLICY_VERSION_ID=@MAXVERSION                               
    and  PDI.is_active='Y'                        
    ORDER BY PDI.DWELLING_ID      
  
 END                             
END  
  
GO

