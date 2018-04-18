         
 /*----------------------------------------------------------                                                  
Proc Name             : Dbo.Proc_GetRiskTypeDetails                                                  
Created by            : Santosh Kumar Gautam                                                 
Date                  : 18/11/2010                                                 
Purpose               : To retrieve the Risk types based on the lob id        
Revison History       :                                                  
Used In               : Claim module                                              
------------------------------------------------------------                                                  
Date     Review By          Comments                     
            
drop Proc Proc_GetRiskTypeDetails                                         
------   ------------       -------------------------*/                                                  
--                     
                      
--                   
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRiskTypeDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRiskTypeDetails]
GO                
CREATE PROCEDURE [dbo].[Proc_GetRiskTypeDetails]                      
                       
@CUSTOMER_ID         INT,                                                                                  
@POLICTY_ID          INT,                                                                                  
@POLICY_VERSION_ID   INT,          
@RISK_ID             INT,        
@LOB_ID              INT,  
@CLAIM_ID            INT                                                                                      
                                                                                      
                                                                            
                      
AS                      
BEGIN           
          
          
 ---------------------------------------------------------  
 -- 9  : All Risks and Named Perils     
 -- 26 : Engeneering Risks   
 ---------------------------------------------------------           
 IF(@LOB_ID IN (9,26))  
 BEGIN        
          
  SELECT  P.PERIL_ID AS RISK_ID ,    
          L.LOC_ADD1 + ' - ' +  ISNULL(L.NUMBER,'') AS [ADDRESS],          
          L.LOC_ADD2 AS COMPLIMENT,    
          L.DISTRICT ,    
          L.LOC_ZIP  AS ZIP_CODE,    
          L.LOC_STATE AS [STATE] ,    
          L.LOC_COUNTRY AS COUNTRY ,  
          P.ITEM_NUMBER AS LOCATION_ITEM_NUMBER,  
          P.ACTUAL_INSURED_OBJECT ,  
          P.CO_APPLICANT_ID                   
  FROM    POL_LOCATIONS L WITH(NOLOCK) INNER JOIN          
          POL_PERILS    P WITH(NOLOCK) ON  P.LOCATION>0 AND P.LOCATION=L.LOCATION_ID AND L.CUSTOMER_ID=P.CUSTOMER_ID AND L.POLICY_ID=P.POLICY_ID AND L.POLICY_VERSION_ID=P.POLICY_VERSION_ID             
         -- INNER JOIN CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID                                             
  WHERE ( P.PERIL_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
  END           
   
 ---------------------------------------------------------  
 -- 10 : Comprehensive Condominium  
 -- 11 : DWELLING  
 -- 12 : GENERAL CIVIL LIABILITY  
 -- 14 : Diversified Risks  
 -- 16 : RObbery  
 -- 19 : Comprehensive Company    
 -- 25 : Traditional Fire  
 -- 27 : Global of Bank  
 -- 32 : Judicial Guarantee  
 ---------------------------------------------------------            
ELSE IF(@LOB_ID IN (10,11,12,14,16,19,25,27,32))    
 BEGIN        
          
  SELECT P.PRODUCT_RISK_ID AS RISK_ID ,          
          L.LOC_ADD1 + ' - ' +  ISNULL(L.NUMBER,'')   AS [ADDRESS],          
          L.LOC_ADD2    AS COMPLIMENT,    
          L.DISTRICT,    
          L.LOC_ZIP  AS ZIP_CODE,    
          L.LOC_STATE   AS [STATE] ,    
          L.LOC_COUNTRY AS COUNTRY,  
          P.ITEM_NUMBER AS LOCATION_ITEM_NUMBER,  
          P.ACTUAL_INSURED_OBJECT ,  
     P.CO_APPLICANT_ID         
  FROM  POL_LOCATIONS L WITH(NOLOCK) INNER JOIN          
        POL_PRODUCT_LOCATION_INFO  P WITH(NOLOCK)  ON  P.LOCATION>0 AND P.LOCATION=L.LOCATION_ID AND L.CUSTOMER_ID=P.CUSTOMER_ID AND L.POLICY_ID=P.POLICY_ID AND L.POLICY_VERSION_ID=P.POLICY_VERSION_ID             
       -- INNER JOIN  CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID                   
  WHERE ( P.PRODUCT_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
 END   
 
  ---------------------------------------------------------  
 -- 1 : Fire  
 ---------------------------------------------------------            
ELSE IF(@LOB_ID IN (1))    
 BEGIN        
          
  SELECT P.DWELLING_ID AS RISK_ID ,          
          L.LOC_ADD1 + ' - ' +  ISNULL(L.NUMBER,'')   AS [ADDRESS],          
          L.LOC_ADD2    AS COMPLIMENT,    
          L.DISTRICT,    
          L.LOC_ZIP  AS ZIP_CODE,    
          L.LOC_STATE   AS [STATE] ,    
          L.LOC_COUNTRY AS COUNTRY,  
          P.DWELLING_NUMBER AS LOCATION_ITEM_NUMBER, 
          '' as ACTUAL_INSURED_OBJECT ,  
          '' as CO_APPLICANT_ID         
  FROM  POL_LOCATIONS L WITH(NOLOCK) INNER JOIN          
        POL_DWELLINGS_INFO  P WITH(NOLOCK)  ON  P.LOCATION_ID>0 AND P.LOCATION_ID=L.LOCATION_ID AND L.CUSTOMER_ID=P.CUSTOMER_ID AND L.POLICY_ID=P.POLICY_ID AND L.POLICY_VERSION_ID=P.POLICY_VERSION_ID             
       -- INNER JOIN  CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID                   
  WHERE ( P.DWELLING_ID =@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
 END            
               
 ---------------------------------------------------------                         
  -- 13 : MARITIME  
 ---------------------------------------------------------                    
ELSE IF(@LOB_ID=13)      
 BEGIN        
          
    SELECT  M.MARITIME_ID AS RISK_ID,    
            M.NAME_OF_VESSEL,    
            M.TYPE_OF_VESSEL,    
            M.MANUFACTURE_YEAR,    
            M.MANUFACTURER ,  
            VESSEL_NUMBER  ,  
            M.CO_APPLICANT_ID       
    FROM POL_MARITIME M WITH(NOLOCK)   
     --INNER JOIN  CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=M.CUSTOMER_ID AND I.POLICY_ID=M.POLICY_ID AND I.POLICY_VERSION_ID=M.POLICY_VERSION_ID                   
    WHERE  ( M.MARITIME_ID=@RISK_ID AND M.CUSTOMER_ID=@CUSTOMER_ID AND M.POLICY_ID=@POLICTY_ID AND M.POLICY_VERSION_ID= @POLICY_VERSION_ID AND M.IS_ACTIVE='Y')             
              
  END             
   
 ---------------------------------------------------------  
 -- 21 : Group Personal Accident for Passenger   
 -- 34 : Group Life   
 -- 15 : Individual Personal Accident  
 -- 33 : Mortgage  
 ---------------------------------------------------------            
ELSE IF(@LOB_ID IN (21,34,15,33))   
 BEGIN        
            
 SELECT  P.PERSONAL_INFO_ID AS RISK_ID,         
         ISNULL(P.INDIVIDUAL_NAME,'') AS INSURED_NAME ,  
         DATE_OF_BIRTH,  
         ISNULL(PR.EFFECTIVE_DATETIME,CP.POLICY_EFFECTIVE_DATE) AS EFFECTIVE_DATETIME,  
         ISNULL(PR.[EXPIRY_DATE],CP.POLICY_EXPIRATION_DATE) AS EXPIRY_DATE,  
         P.APPLICANT_ID   AS CO_APPLICANT_ID  
  FROM   POL_PERSONAL_ACCIDENT_INFO P WITH(NOLOCK)  
         --INNER JOIN  CLM_CLAIM_INFO I WITH(NOLOCK)  ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID     
         LEFT OUTER JOIN  POL_POLICY_PROCESS PR WITH(NOLOCK) ON PR.CUSTOMER_ID=P.CUSTOMER_ID AND PR.POLICY_ID=P.POLICY_ID AND PR.NEW_POLICY_VERSION_ID=P.POLICY_VERSION_ID AND  PR.PROCESS_STATUS='COMPLETE' -- AND PR.PROCESS_ID=14  
         LEFT OUTER JOIN  POL_CUSTOMER_POLICY_LIST CP WITH(NOLOCK) ON CP.CUSTOMER_ID=P.CUSTOMER_ID AND CP.POLICY_ID=P.POLICY_ID AND CP.POLICY_VERSION_ID=P.POLICY_VERSION_ID --AND  CP.PROCESS_STATUS='COMPLETE'  -- AND PR.PROCESS_ID=14  
           
         --INNER JOIN  CLT_APPLICANT_LIST Q ON Q.APPLICANT_ID=P.APPLICANT_ID AND Q.CUSTOMER_ID =P.CUSTOMER_ID  
  WHERE (  P.PERSONAL_INFO_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
    
  END             
 ---------------------------------------------------------  
 -- 17 : Facultative Liability   (THIS IS MASTER POLICY)      
 -- 18 : Civil Liability Transportation   (THIS IS MASTER POLICY)      
 -- 28 : Aeronautic  
 -- 29 : Motor  
 -- 31 : Cargo Transportation Civil Liability  
 ---------------------------------------------------------                  
ELSE IF(@LOB_ID IN(17,18,28,29,31))  
 BEGIN        
          
  SELECT VEHICLE_ID  AS RISK_ID ,    
         CHASSIS  AS VEHICLE_VIN,       
         MANUFACTURED_YEAR ,    
         VEHICLE_MAKE AS MANUFACTURER,    
         MAKE_MODEL  AS MODEL  ,  
         LICENSE_PLATE      ,  
         P.RISK_EFFECTIVE_DATE,  
         P.RISK_EXPIRE_DATE   ,  
         P.CO_APPLICANT_ID     
  FROM   POL_CIVIL_TRANSPORT_VEHICLES  P WITH(NOLOCK)    
   --INNER JOIN  CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID                    
  WHERE (  VEHICLE_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
  END       
  
  ELSE IF(@LOB_ID IN(38))  
 BEGIN        
          
  SELECT VEHICLE_ID  AS RISK_ID ,    
         P.CHASIS_NUMBER  AS VEHICLE_VIN,       
         P.VEHICLE_YEAR as  MANUFACTURED_YEAR ,    
         P.MAKE AS MANUFACTURER,    
         iSNULL(MNT_VEHICLE_MODEL_LIST.MODEL,'') AS MODEL  ,  
         '' as  LICENSE_PLATE      ,  
         '' as RISK_EFFECTIVE_DATE,  
         '' as RISK_EXPIRE_DATE   ,  
         '' AS CO_APPLICANT_ID     
  FROM   POL_VEHICLES  P WITH(NOLOCK)    
      
  INNER JOIN MNT_VEHICLE_MODEL_LIST ON      
  MNT_VEHICLE_MODEL_LIST.ID  = P.MODEL
   --INNER JOIN  CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID                    
  WHERE (  VEHICLE_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
  END       
          
          
---------------------------------------------------------   
  --  20 : National Cargo Transport  
  --  23 : International Cargo Transport  
 ---------------------------------------------------------                     
ELSE IF(@LOB_ID IN (20,23))   
   BEGIN        
          
  SELECT  P.COMMODITY_ID AS ITEM_ID ,                    
          P.DEPARTING_DATE,   
          P.ARRIVAL_DATE,   
          P.CONVEYANCE_TYPE,    
          P.DESTINATION_CITY,    
          P.DEST_COUNTRY AS DESTINATION_COUNTRY,    
          P.DEST_STATE AS DESTINATION_STATE,    
          P.ORIGIN_CITY,    
          P.ORIGN_COUNTRY AS ORIGIN_COUNTRY,    
          P.ORIGN_STATE AS ORIGIN_STATE  ,  
          P.CO_APPLICANT_ID              
  FROM    POL_COMMODITY_INFO  P WITH(NOLOCK) --INNER JOIN    
         -- CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID            
  WHERE  ( COMMODITY_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
 END      
   
  ---------------------------------------------------------   
  -- 30 : Dpvat(Cat. 3 e 4)  
  -- 36 : DPVAT(Cat.1,2,9 e 10)  
  ---------------------------------------------------------        
 ELSE IF(@LOB_ID IN (30,36))      
  BEGIN        
     
   SELECT P.VEHICLE_ID    AS ITEM_ID,  
    P.TICKET_NUMBER AS DP_TICKET_NUMBER,  
    P.STATE_ID   AS DP_STATE_ID,    
    P.CATEGORY   AS DP_CATEGORY,  
    P.CO_APPLICANT_ID          
   FROM   POL_CIVIL_TRANSPORT_VEHICLES  P WITH(NOLOCK) --INNER JOIN          
       --CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID                      
   WHERE  ( P.VEHICLE_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
  END     
 ---------------------------------------------------------   
  -- 22 : Personal Accident for Passengers   
 ---------------------------------------------------------      
  ELSE IF(@LOB_ID =22)      
   BEGIN        
     
   SELECT P.PERSONAL_ACCIDENT_ID AS ITEM_ID ,          
       P.[START_DATE]         AS PA_START_DATE,          
    P.END_DATE    AS PA_END_DATE,            
    P.NUMBER_OF_PASSENGERS AS PA_NUM_OF_PASS ,  
    P.CO_APPLICANT_ID            
   FROM   POL_PASSENGERS_PERSONAL_ACCIDENT_INFO  P WITH(NOLOCK) --INNER JOIN          
     --  CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID                      
   WHERE  ( P.PERSONAL_ACCIDENT_ID=@RISK_ID  AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
     
   END  
     
---------------------------------------------------------   
  -- 35 : Rural Lien  
---------------------------------------------------------       
  ELSE IF(@LOB_ID = 35)  
   BEGIN        
            
  SELECT   P.PENHOR_RURAL_ID AS ITEM_ID ,          
           P.ITEM_NUMBER     AS RURAL_ITEM_NUMBER,                
     P.STATE_ID        AS RURAL_STATE_ID,  
     P.CITY   AS RURAL_CITY,  
     P.INSURED_AREA AS RURAL_INSURED_AREA,  
     P.PROPERTY  AS RURAL_PROPERTY,  
     P.CULTIVATION  AS RURAL_CULTIVATION,  
     P.FESR_COVERAGE AS RURAL_FESR_COVERAGE,  
     P.MODE   AS RURAL_MODE,  
     P.SUBSIDY_PREMIUM AS RURAL_SUBSIDY_PREMIUM,  
     P.SUBSIDY_STATE AS RURAL_SUBSIDY_STATE,  
     P.CO_APPLICANT_ID      
   FROM    POL_PENHOR_RURAL_INFO  P WITH(NOLOCK) --INNER JOIN          
          -- CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID  
      WHERE  ( P.PENHOR_RURAL_ID=@RISK_ID  AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
     
  END         
    
 ---------------------------------------------------------   
  -- 37 : Rental Security  
 ---------------------------------------------------------       
  ELSE IF(@LOB_ID IN (37))  
   BEGIN        
            
  SELECT P.PENHOR_RURAL_ID AS ITEM_ID ,          
         P.ITEM_NUMBER     AS ITEM_NUMBER,  
         P.REMARKS         AS ACTUAL_INSURED_OBJECT ,  
         P.CO_APPLICANT_ID         
   FROM   POL_PENHOR_RURAL_INFO  P WITH(NOLOCK) --INNER JOIN          
        --  CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID   
   WHERE  ( P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
     
  END              
              
          
END   