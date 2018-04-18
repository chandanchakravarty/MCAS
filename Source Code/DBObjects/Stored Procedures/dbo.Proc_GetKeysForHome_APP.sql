IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetKeysForHome_APP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetKeysForHome_APP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran          
--drop proc  dbo.Proc_GetKeysForHome_APP              
--go          
/*                                                    
----------------------------------------------------------                                                        
Proc Name       : dbo.Proc_GetKeysForHome_APP                                                    
Created by      : Ravindra                                                      
Date            : 06-14-2006                        
Purpose         :                         
Revison History :                                                        
Modified by     : Pravesh Chandel                        
Date            : 24-NOv-2006                        
Purpose         : Add more Fetch Coulun for Rules          
          
Modified by     : Pravesh Chandel          
Date            : 24-April-2007          
Purpose         : Round off COVERAGE_AMOUNT to Lower Thousand          
                        
Reviewed By : Anurag verma              
Reviewed On : 25-06-2007              
          
Modified by     : Praveen Kasana          
Date            : 16 Oct 2009          
Purpose         : HO216 : Itrack 6114          
    Premises Alarm or Fire Protection System (HO-216)           
    For Michigan Regular/Premier & Indiana           
                       
------------------------------------------------------------                                                        
Date     Review By          Comments                                                        
------   ------------       -------------------------                                                       
Proc_GetKeysForHome_APP 1692,252,1,1              
*/                         
--drop proc dbo.Proc_GetKeysForHome_APP                    
CREATE proc dbo.Proc_GetKeysForHome_APP                  
(                        
 @CUSTOMER_ID INT,                        
 @APP_ID INT,                        
 @APP_VERSION_ID INT,                        
 @BOAT_ID INT                        
)                        
                        
AS                        
BEGIN                        
  DECLARE @CENT_ST_BURG_FIRE Char(1)                          
  DECLARE @DIR_FIRE_AND_POLICE Char(1)                         
  DECLARE @NUM_LOC_ALARMS_APPLIES int                             
  DECLARE @DIR_FIRE Char(1)                        
  DECLARE @DIR_POLICE Char(1)                          
  DECLARE @CENT_ST_FIRE varchar(1)                        
  DECLARE @CENT_ST_BURG varchar(1)                        
  DECLARE @ALARM_CERT_ATTACHED varchar(10)                    
   DECLARE @LOC_FIRE_GAS varchar(1)                  
  DECLARE @TWO_MORE_FIRE varchar(1)                      
 SELECT                              
  @NUM_LOC_ALARMS_APPLIES = NUM_LOC_ALARMS_APPLIES,                          
  @CENT_ST_BURG_FIRE = CENT_ST_BURG_FIRE ,                          
  @DIR_FIRE_AND_POLICE = DIR_FIRE_AND_POLICE ,                        
  @DIR_FIRE =DIR_FIRE,                        
  @DIR_POLICE=DIR_POLICE ,                        
  @CENT_ST_FIRE =CENT_ST_FIRE,                        
  @CENT_ST_BURG =CENT_ST_BURG,                        
  @ALARM_CERT_ATTACHED =ALARM_CERT_ATTACHED ,                       
  @LOC_FIRE_GAS    =LOC_FIRE_GAS,                  
  @TWO_MORE_FIRE=TWO_MORE_FIRE                  
                      
 FROM APP_HOME_RATING_INFO    WITH(NOLOCK)                           
 WHERE                                       
  CUSTOMER_ID=@CUSTOMER_ID AND                                      
  APP_ID=@APP_ID AND                                      
  APP_VERSION_ID=@APP_VERSION_ID AND                                      
  DWELLING_ID=@BOAT_ID                            
         
 DECLARE @HO216 INT                        
/* commented as per Itrack Issue 1914 as no Certificate of alarm is required                  
 IF ( (@CENT_ST_BURG = 'Y' AND @ALARM_CERT_ATTACHED='10963') OR (@CENT_ST_BURG_FIRE ='Y' AND @ALARM_CERT_ATTACHED='10963' )  OR                    ((@DIR_FIRE_AND_POLICE = 'Y' OR @DIR_FIRE= 'Y' OR @DIR_POLICE='Y' OR @CENT_ST_FIRE='Y' or @LOC_FIRE_GAS='Y' 
or @TWO_MORE_FIRE='Y') and @ALARM_CERT_ATTACHED='10963')                  
  OR  (@NUM_LOC_ALARMS_APPLIES >0 and @ALARM_CERT_ATTACHED='10963' ))                          
*/         
        
 /* Itrack 6586        
 If any Protection Devices are checked off         
  o Then check off Endorsement HO-216         
  o Apply Credit        
 If any Protection Devices are checked off and If No to Alarm Certificate Attached         
  o Refer to Underwriters – Message         
 */                 
 IF           
(          
 ( @CENT_ST_BURG = 'Y'  OR @CENT_ST_BURG_FIRE ='Y'  OR                   
 (@DIR_FIRE_AND_POLICE = 'Y' OR @DIR_FIRE= 'Y' OR @DIR_POLICE='Y' OR @CENT_ST_FIRE='Y' or @LOC_FIRE_GAS='Y' or @TWO_MORE_FIRE='Y')                  
 OR  @NUM_LOC_ALARMS_APPLIES >0)                          
 --AND (@ALARM_CERT_ATTACHED='10963') --Commented by Charles on 20-Oct-09 for Itrack 6586        
)          
                  
 BEGIN                          
  SET @HO216 =1                                                
 END                          
 ELSE                          
 BEGIN                          
  SET @HO216 =0                                                 
 END                          
                        
                        
SELECT                         
APP_INFO.APP_EFFECTIVE_DATE AS APP_EFFECTIVE_DATE,                        
APP_INFO.APP_LOB AS LOB_ID,                        
APP_INFO.STATE_ID as STATE_ID,              
YEAR(CONVERT(VARCHAR(20),APP_INFO.APP_EFFECTIVE_DATE,109)) AS APP_YEAR,                        
ISNULL(DWL_INFO.REPLACEMENT_COST,0) AS REP_COST,                        
ISNULL(DWL_INFO.MARKET_VALUE,0) AS MKT_VALUE,                        
APP_INFO.POLICY_TYPE AS PKG_TYPE,                        
ISNULL(COV_INFO.COVERAGE_ID,0) AS HO_34,                        
ISNULL(COV_INFO2.LIMIT_1,0) AS OLD_COV_A,                        
ISNULL(COV_INFO3.LIMIT_1,0) AS OLD_COV_C,                        
isnull(COV_INFO2.DEDUCTIBLE_1,0) AS INCLUDED_A,                        
ISNULL(COV_INFO4.DEDUCTIBLE_1,0) AS INCLUDED_B,                        
ISNULL(COV_INFO4.LIMIT_1,0) AS OLD_COV_B,                        
ISNULL(LOC_INFO.LOCATION_TYPE,0) AS LOC_TYPE,                       
ISNULL(LOC_INFO.IS_PRIMARY,'U') AS IS_PRIMARY,                        
ISNULL(RAT_INFO.IS_UNDER_CONSTRUCTION,0) AS UND_CONS,                        
ISNULL(APP_GEN.NO_HORSES,0) AS NO_HORSES,                        
ISNULL(APP_GEN.PREMISES,0)  AS PREMISES_DETAIL,                        
ISNULL(APP_GEN.LOCATION,0) AS LOCATIONS,                      
ISNULL(APP_GEN.NON_SMOKER_CREDIT,0)  AS NON_SMOKER_CREDIT,                        
ISNULL(RAT_INFO.IS_SUPERVISED,0) AS IS_SUPER,                        
(                        
 SELECT COUNT(*) FROM  APP_HOME_OWNER_ADD_INT INT_INFO WITH(NOLOCK)                        
 WHERE                        
     INT_INFO.CUSTOMER_ID = @CUSTOMER_ID                        
 AND INT_INFO.APP_ID=@APP_ID                        
 AND INT_INFO.APP_VERSION_ID  = @APP_VERSION_ID                        
 AND INT_INFO.DWELLING_ID = @BOAT_ID                        
 AND INT_INFO.NATURE_OF_INTEREST  = 11815 -- 11816                        
    AND  INT_INFO.IS_ACTIVE='Y'                        
) AS NAT_INS,                        
(                        
 SELECT COUNT(LOC_COUNTY) FROM APP_LOCATIONS LOC WITH(NOLOCK)                        
 WHERE LOC.LOC_COUNTY IN                         
 (                        
  'CLAY','CRAWFORD','DAVIESS','DUBOIS','FOUNTAIN','GIBSON','GREENE','KNOX','LAWRENCE','MARTIN','MONROE',                        
  'MONTGOMERY','ORANGE','OWEN','PARKE','PERRY','PIKE','POSEY','PUTNAM','SPENCER','SULLIVAN','VANDERBURGH',                        
  'VERMILLION','VIGO','WARREN','WARRICK'                        
 )                        
 AND LOC.CUSTOMER_ID = @CUSTOMER_ID                        
 AND LOC.APP_ID = @APP_ID                
 AND LOC.APP_VERSION_ID=@APP_VERSION_ID                        
 AND LOC.LOCATION_ID = DWL_INFO.LOCATION_ID                         
                         
)AS VALID_COUNTY,                        
                        
(                        
 SELECT COUNT(COVERAGE_BASIS)             
 FROM APP_OTHER_STRUCTURE_DWELLING STR1 WITH(NOLOCK)                        
 WHERE STR1.PREMISES_LOCATION = '11841'  -- Added by Charles on 4-Dec-09 for Itrack 6405       
 AND STR1.COVERAGE_BASIS='11846'  ---REPAIR                        
 AND STR1.CUSTOMER_ID = @CUSTOMER_ID                        
 AND STR1.APP_ID = @APP_ID                        
 AND STR1.APP_VERSION_ID=@APP_VERSION_ID                        
 AND STR1.DWELLING_ID = @BOAT_ID                        
 AND ISNULL(STR1.IS_ACTIVE,'') = 'Y'       
 AND STR1.SATELLITE_EQUIPMENT<>'10963'  -- Added by Charles on 2-Dec-09 for Itrack 6405                                  
) AS COV_BASE,                        
(                        
 SELECT COUNT(COVERAGE_BASIS)                        
 FROM APP_OTHER_STRUCTURE_DWELLING STR1 WITH(NOLOCK)                        
 WHERE  STR1.COVERAGE_BASIS='11847'  --REPLACEMENT                        
 AND STR1.CUSTOMER_ID = @CUSTOMER_ID                        
 AND STR1.APP_ID = @APP_ID                        
 AND STR1.APP_VERSION_ID=@APP_VERSION_ID                        
 AND STR1.DWELLING_ID = @BOAT_ID                        
 AND ISNULL(STR1.IS_ACTIVE,'') = 'Y'                      
) AS COV_BASE_REPL,                        
                        
(                        
 SELECT ISNULL(SUM(INSURING_VALUE),0)  -- Changed by Charles on 3-Dec-09 for Itrack 6405                  
 --SELECT CASE (FLOOR(ISNULL(SUM(INSURING_VALUE),0)/1000))*1000 WHEN 0 THEN 1000 ELSE (FLOOR(ISNULL(SUM(INSURING_VALUE),0)/1000))*1000 END    --Changed by Charles on 14-Oct-09 for Itrack 6091             
 FROM APP_OTHER_STRUCTURE_DWELLING STR2 WITH(NOLOCK)                        
 WHERE STR2.PREMISES_LOCATION = '11841'  -- Added by Charles on 4-Dec-09 for Itrack 6405        
 AND STR2.COVERAGE_BASIS='11846'                        
 AND STR2.CUSTOMER_ID = @CUSTOMER_ID                        
 AND STR2.APP_ID = @APP_ID                        
 AND STR2.APP_VERSION_ID=@APP_VERSION_ID           
 AND STR2.DWELLING_ID = @BOAT_ID                        
 AND ISNULL(STR2.IS_ACTIVE,'') = 'Y'      
 AND STR2.SATELLITE_EQUIPMENT<>'10963'  -- Added by Charles on 2-Dec-09 for Itrack 6405                                    
) AS INS_VAL,                        
                        
(                        
 SELECT COUNT(PREMISES_LOCATION)                         
 FROM APP_OTHER_STRUCTURE_DWELLING STR3 WITH(NOLOCK)                        
 WHERE                         
     STR3.PREMISES_LOCATION = '11840' -- Off Primises                        
 AND STR3.CUSTOMER_ID = @CUSTOMER_ID                        
 AND STR3.APP_ID = @APP_ID                        
 AND STR3.APP_VERSION_ID=@APP_VERSION_ID                        
 AND STR3.DWELLING_ID = @BOAT_ID                        
 AND ISNULL(STR3.IS_ACTIVE,'') = 'Y'       
 AND (STR3.COVERAGE_BASIS='11846' OR STR3.COVERAGE_BASIS='11847')  -- Added by Charles on 4-Dec-09 for Itrack 6405                                   
) AS PRIM_LOC,                        
                        
(                        
 SELECT ISNULL(SUM(INSURING_VALUE_OFF_PREMISES),0)                        
 FROM APP_OTHER_STRUCTURE_DWELLING STR4 WITH(NOLOCK)                        
 WHERE                         
     STR4.PREMISES_LOCATION = '11840' -- Off Primises                  
 AND STR4.CUSTOMER_ID = @CUSTOMER_ID                        
 AND STR4.APP_ID = @APP_ID                        
 AND STR4.APP_VERSION_ID=@APP_VERSION_ID                        
 AND STR4.DWELLING_ID = @BOAT_ID                        
 AND ISNULL(STR4.IS_ACTIVE,'') = 'Y'       
 AND (STR4.COVERAGE_BASIS='11846' OR STR4.COVERAGE_BASIS='11847')  -- Added by Charles on 4-Dec-09 for Itrack 6405                          
) AS INS_VAL_OFF ,                        
--added by pravesh 4 On Premises/Rented to Others                        
(                        
 SELECT COUNT(PREMISES_LOCATION)                        
 FROM APP_OTHER_STRUCTURE_DWELLING STR5 WITH(NOLOCK)                        
 WHERE                   
   STR5.PREMISES_LOCATION = '11968' -- On Premises/Rented to Others                        
 AND STR5.CUSTOMER_ID = @CUSTOMER_ID                        
 AND STR5.APP_ID = @APP_ID                        
 AND STR5.APP_VERSION_ID=@APP_VERSION_ID                        
 AND STR5.DWELLING_ID = @BOAT_ID                        
 AND ISNULL(STR5.IS_ACTIVE,'') = 'Y'       
 AND ((STR5.COVERAGE_BASIS='11847' AND STR5.SATELLITE_EQUIPMENT<>'10963') OR STR5.COVERAGE_BASIS='11846')  -- Added by Charles on 4-Dec-09 for Itrack 6405                      
) AS RENT_OTHER_LOC ,                                      
(                        
 SELECT ISNULL(SUM(ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED),0) --Changed by Charles on 4-Dec-09 for Itrack 6405                       
--SELECT case (floor(ISNULL(SUM(COVERAGE_AMOUNT),0)/1000))*1000 when 0 then 1000 else (floor(ISNULL(SUM(COVERAGE_AMOUNT),0)/1000))*1000 end                       
 FROM APP_OTHER_STRUCTURE_DWELLING STR6 WITH(NOLOCK)                        
 WHERE                         
     STR6.PREMISES_LOCATION = '11968' -- On Premises/Rented to Others                        
 AND STR6.CUSTOMER_ID = @CUSTOMER_ID                        
 AND STR6.APP_ID = @APP_ID                        
 AND STR6.APP_VERSION_ID=@APP_VERSION_ID                        
 AND STR6.DWELLING_ID = @BOAT_ID                        
 AND ISNULL(STR6.IS_ACTIVE,'') = 'Y'      
 AND ((STR6.COVERAGE_BASIS='11847' AND STR6.SATELLITE_EQUIPMENT<>'10963') OR STR6.COVERAGE_BASIS='11846')  -- Added by Charles on 4-Dec-09 for Itrack 6405                      
) AS RENT_OTHER_AMT ,                        
--SATELLITE_EQUIPMENT                        
(                        
 SELECT count(SATELLITE_EQUIPMENT)                        
 FROM APP_OTHER_STRUCTURE_DWELLING STR7 WITH(NOLOCK)                        
 WHERE STR7.SATELLITE_EQUIPMENT='10963'              
  AND   STR7.PREMISES_LOCATION = 11841 -- On Premises is setelite is yes                        
 AND STR7.CUSTOMER_ID = @CUSTOMER_ID                        
 AND STR7.APP_ID = @APP_ID                        
 AND STR7.APP_VERSION_ID=@APP_VERSION_ID                        
 AND STR7.DWELLING_ID = @BOAT_ID                        
 AND ISNULL(STR7.IS_ACTIVE,'') = 'Y'                      
) AS SETELITE ,                        
(                        
 SELECT ISNULL(SUM(ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED),0)                        
 FROM APP_OTHER_STRUCTURE_DWELLING STR8 WITH(NOLOCK)                        
 WHERE STR8.SATELLITE_EQUIPMENT='10963'                       
 AND STR8.PREMISES_LOCATION = 11841 -- On Premises and is setelite is yes                        
 AND STR8.CUSTOMER_ID = @CUSTOMER_ID                        
 AND STR8.APP_ID = @APP_ID                        
 AND STR8.APP_VERSION_ID=@APP_VERSION_ID                        
 AND STR8.DWELLING_ID = @BOAT_ID                        
 AND ISNULL(STR8.IS_ACTIVE,'') = 'Y'                      
) AS SETELITE_DED ,                        
(                        
 SELECT count(SATELLITE_EQUIPMENT)                        
 FROM APP_OTHER_STRUCTURE_DWELLING STR7 WITH(NOLOCK)                        
 WHERE STR7.SATELLITE_EQUIPMENT<>'10963'                        
 AND   STR7.PREMISES_LOCATION = '11841' -- On Premises is setelite is no       
 AND STR7.COVERAGE_BASIS='11847' -- Added by Charles on 9-Dec-09 for Itrack 6405                                        
 AND STR7.CUSTOMER_ID = @CUSTOMER_ID                        
 AND STR7.APP_ID = @APP_ID                        
 AND STR7.APP_VERSION_ID=@APP_VERSION_ID                        
 AND STR7.DWELLING_ID = @BOAT_ID                        
 AND ISNULL(STR7.IS_ACTIVE,'') = 'Y'                      
) AS SETELITE_NO ,                        
(                        
 SELECT ISNULL(SUM(ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED),0)                        
 FROM APP_OTHER_STRUCTURE_DWELLING STR8 WITH(NOLOCK)                        
 WHERE STR8.SATELLITE_EQUIPMENT<>'10963'                       
 AND STR8.PREMISES_LOCATION = '11841' -- On Premises and is setelite is no       
 AND STR8.COVERAGE_BASIS='11847'  -- Added by Charles on 4-Dec-09 for Itrack 6405                      
 AND STR8.CUSTOMER_ID = @CUSTOMER_ID                        
 AND STR8.APP_ID = @APP_ID                        
 AND STR8.APP_VERSION_ID=@APP_VERSION_ID                        
 AND STR8.DWELLING_ID = @BOAT_ID                        
 AND ISNULL(STR8.IS_ACTIVE,'') = 'Y'                      
) AS SETELITE_DED_NO ,                                        
--end here                        
(                        
   SELECT COUNT(CUSTOMER_ID) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS APP_SCH WITH(NOLOCK) INNER JOIN MNT_COVERAGE MNT WITH(NOLOCK)                        
   ON APP_SCH.ITEM_ID=MNT.COV_ID                        
    WHERE                         
  APP_SCH.CUSTOMER_ID = @CUSTOMER_ID                        
  AND APP_SCH.APP_ID = @APP_ID        
AND APP_SCH.APP_VERSION_ID=@APP_VERSION_ID                        
        AND MNT.COV_REF_CODE IN ('928','929')                        
                              
) AS SCH_ITEM,                        
(                        
   SELECT COUNT(CUSTOMER_ID) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS APP_SCH WITH(NOLOCK) INNER JOIN MNT_COVERAGE MNT WITH(NOLOCK)                        
   ON APP_SCH.ITEM_ID=MNT.COV_ID                        
    WHERE                         
  APP_SCH.CUSTOMER_ID = @CUSTOMER_ID                        
  AND APP_SCH.APP_ID = @APP_ID                        
  AND APP_SCH.APP_VERSION_ID=@APP_VERSION_ID                        
        AND MNT.COV_REF_CODE IN ('981','982')                        
                              
) AS SCH_ITEM_61,                        
(                        
   SELECT COUNT(CUSTOMER_ID) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS APP_SCH WITH(NOLOCK) INNER JOIN MNT_COVERAGE MNT WITH(NOLOCK)                        
   ON APP_SCH.ITEM_ID=MNT.COV_ID                        
    WHERE                         
  APP_SCH.CUSTOMER_ID = @CUSTOMER_ID                        
  AND APP_SCH.APP_ID = @APP_ID                        
  AND APP_SCH.APP_VERSION_ID=@APP_VERSION_ID                        
        AND MNT.COV_REF_CODE IN ('983','984')                        
                              
) AS SCH_ITEM_214,                        
(                        
 SELECT COUNT(CUSTOMER_ID)                         
 FROM APP_WATERCRAFT_INFO WITH(NOLOCK)                        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                     
 APP_ID = @APP_ID AND                                    
 APP_VERSION_ID = @APP_VERSION_ID  and                        
    IS_ACTIVE='Y'                        
                        
) AS COUNT_WATERCRAFTS,                        
                        
(                        
 SELECT  COUNT(CUSTOMER_ID) FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES WITH(NOLOCK)                        
 WHERE   CUSTOMER_ID = @CUSTOMER_ID                        
 AND APP_ID = @APP_ID                        
 AND APP_VERSION_ID = @APP_VERSION_ID                        
 AND ACTIVE   = 'Y'                        
) AS RVCOUNT,                        
@HO216 AS HO216 ,                
1 AS PROCESS ,              
(                        
 SELECT  COUNT(COVERAGE_CODE_ID) FROM APP_DWELLING_SECTION_COVERAGES WITH(NOLOCK)                        
 WHERE   CUSTOMER_ID = @CUSTOMER_ID                        
 AND APP_ID = @APP_ID                        
 AND APP_VERSION_ID = @APP_VERSION_ID                        
 AND DWELLING_ID  =@BOAT_ID               
AND  COVERAGE_CODE_ID IN (93,166)                 
) AS HO66 ,                       
                      
(            
 SELECT CASE ISNULL(REPLACEMENTCOST_COVA  ,'0')            
               
 WHEN 10964              
 THEN '0'              
 WHEN 10963              
 THEN '1'            
 ELSE            
      '0'             
end            
 FROM APP_DWELLINGS_INFO WITH(NOLOCK)            
  WHERE   CUSTOMER_ID = @CUSTOMER_ID                        
 AND APP_ID = @APP_ID                        
 AND APP_VERSION_ID = @APP_VERSION_ID                        
 AND DWELLING_ID  =@BOAT_ID             
              
) AS SET_TO_EQUAL,            
dbo.Fun_GetMAX_MINValueForHome (APP_INFO.CUSTOMER_ID,APP_INFO.APP_ID,APP_INFO.APP_VERSION_ID,'APP') as VERIFY_RULE,  
(   --Added by Charles on 10-Dec-09 for Itrack 6840                     
 SELECT COUNT(CUSTOMER_ID)                        
 FROM APP_OTHER_STRUCTURE_DWELLING STR9 WITH(NOLOCK)                        
 WHERE                         
 STR9.PREMISES_LOCATION = '11840' -- Off Primises                  
 AND STR9.CUSTOMER_ID = @CUSTOMER_ID                        
 AND STR9.APP_ID = @APP_ID                        
 AND STR9.APP_VERSION_ID=@APP_VERSION_ID                        
 AND STR9.DWELLING_ID = @BOAT_ID                        
 AND ISNULL(STR9.IS_ACTIVE,'') = 'Y'   
 AND ISNULL(STR9.LIABILITY_EXTENDED,10964)=10963  
) AS OFF_LIABILITY_EXTENDED, --Added till here  
1 AS HO_100 --Added by Charles on 11-Jan-2010 for Itrack 6655
                        
FROM APP_DWELLINGS_INFO DWL_INFO WITH(NOLOCK)                        
                        
INNER JOIN APP_LIST APP_INFO WITH(NOLOCK)                        
 ON APP_INFO.CUSTOMER_ID = DWL_INFO.CUSTOMER_ID                        
AND APP_INFO.APP_ID  = DWL_INFO.APP_ID                        
AND APP_INFO.APP_VERSION_ID  = DWL_INFO.APP_VERSION_ID                        
                        
LEFT OUTER JOIN APP_DWELLING_SECTION_COVERAGES COV_INFO WITH(NOLOCK)                        
 ON COV_INFO.CUSTOMER_ID = DWL_INFO.CUSTOMER_ID                        
AND COV_INFO.APP_ID  = DWL_INFO.APP_ID                        
AND COV_INFO.APP_VERSION_ID  = DWL_INFO.APP_VERSION_ID                        
AND COV_INFO.DWELLING_ID = @BOAT_ID                        
AND COV_INFO.COVERAGE_CODE_ID  in (33,140) -- HO-34                        
                        
LEFT OUTER JOIN APP_DWELLING_SECTION_COVERAGES COV_INFO2 WITH(NOLOCK)                        
 ON COV_INFO2.CUSTOMER_ID = DWL_INFO.CUSTOMER_ID                       
AND COV_INFO2.APP_ID  = DWL_INFO.APP_ID                        
AND COV_INFO2.APP_VERSION_ID  = DWL_INFO.APP_VERSION_ID                        
AND COV_INFO2.DWELLING_ID = @BOAT_ID                        
AND COV_INFO2.COVERAGE_CODE_ID  in (3,134) -- Coverage A                        
                        
LEFT OUTER JOIN APP_DWELLING_SECTION_COVERAGES COV_INFO3 WITH(NOLOCK)                        
 ON COV_INFO3.CUSTOMER_ID = DWL_INFO.CUSTOMER_ID                        
AND COV_INFO3.APP_ID  = DWL_INFO.APP_ID                        
AND COV_INFO3.APP_VERSION_ID  = DWL_INFO.APP_VERSION_ID                        
AND COV_INFO3.DWELLING_ID = @BOAT_ID                        
AND COV_INFO3.COVERAGE_CODE_ID  in (7,136) -- Coverage C                        
                        
LEFT OUTER JOIN APP_DWELLING_SECTION_COVERAGES COV_INFO4 WITH(NOLOCK)                        
ON COV_INFO4.CUSTOMER_ID = DWL_INFO.CUSTOMER_ID                        
AND COV_INFO4.APP_ID  = DWL_INFO.APP_ID                        
AND COV_INFO4.APP_VERSION_ID  = DWL_INFO.APP_VERSION_ID                        
AND COV_INFO4.DWELLING_ID = @BOAT_ID                        
AND COV_INFO4.COVERAGE_CODE_ID  in (5,135) -- Coverage B                        
                        
LEFT OUTER JOIN APP_LOCATIONS LOC_INFO WITH(NOLOCK)               
 ON LOC_INFO.CUSTOMER_ID = DWL_INFO.CUSTOMER_ID                        
AND LOC_INFO.APP_ID  = DWL_INFO.APP_ID                        
AND LOC_INFO.APP_VERSION_ID  = DWL_INFO.APP_VERSION_ID                        
AND LOC_INFO.LOCATION_ID = DWL_INFO.LOCATION_ID                        
                        
LEFT OUTER JOIN APP_HOME_RATING_INFO RAT_INFO  WITH(NOLOCK)                        
 ON RAT_INFO.CUSTOMER_ID = DWL_INFO.CUSTOMER_ID                        
AND RAT_INFO.APP_ID  = DWL_INFO.APP_ID                        
AND RAT_INFO.APP_VERSION_ID  = DWL_INFO.APP_VERSION_ID                        
AND RAT_INFO.DWELLING_ID = @BOAT_ID                        
                   
LEFT OUTER JOIN APP_HOME_OWNER_GEN_INFO APP_GEN WITH(NOLOCK)                        
  ON APP_GEN.CUSTOMER_ID = DWL_INFO.CUSTOMER_ID                        
AND APP_GEN.APP_ID  = DWL_INFO.APP_ID                        
AND APP_GEN.APP_VERSION_ID  = DWL_INFO.APP_VERSION_ID                        
                        
WHERE                        
    DWL_INFO.CUSTOMER_ID = @CUSTOMER_ID                        
AND DWL_INFO.APP_ID=@APP_ID                        
AND DWL_INFO.APP_VERSION_ID  = @APP_VERSION_ID                        
AND DWl_INFO.DWELLING_ID = @BOAT_ID                        
                      
                      
                      
END                        
            
--            
--          
--          
--          
--go          
--exec  dbo.Proc_GetKeysForHome_APP   1068,8,1,1           
--rollback tran 
GO

