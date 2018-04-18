        
 /*----------------------------------------------------------                                                  
Proc Name             : Dbo.Proc_GetRiskTypes                                                  
Created by            : Santosh Kumar Gautam                                                 
Date                  : 10/11/2010                                                 
Purpose               : To retrieve the Risk types based on the lob id        
Revison History       :                                                  
Used In               : To fill dropdown at risk information page.(CLAIM module)                                                  
------------------------------------------------------------                                                  
Date     Review By          Comments                     
            
drop Proc Proc_GetRiskTypes                                         
------   ------------       -------------------------*/                                                  
--                     
                      
--                   
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRiskTypes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRiskTypes]
GO                     
CREATE PROCEDURE [dbo].[Proc_GetRiskTypes]      
                       
@CUSTOMER_ID         INT,                                                                                  
@POLICTY_ID          INT,                                                                                  
@POLICY_VERSION_ID   INT,          
@CLAIM_ID            INT=0,        
@LOB_ID              INT,                                                                                
@LANG_ID    INT  
                                                                            
                      
AS                      
BEGIN           
          
          
 DECLARE @TRANSACTION_TYPE INT=0  
 DECLARE @CLM_LOSS_DATE DATETIME  
 DECLARE @ENDO_EFF_DATE DATETIME  
 DECLARE @ENDO_EXR_DATE DATETIME  
  
  
 -----------------------------------------------------------------------------  
 --- FETCH TRANSACTION_TYPE TO CHECK WETHERE CURRENCT POLICY IS MASTER OR NOT  
 ----------------------------------------------------------------------------  
 SELECT @TRANSACTION_TYPE=ISNULL(TRANSACTION_TYPE,0)   
 FROM   POL_CUSTOMER_POLICY_LIST   
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND  
        POLICY_ID=@POLICTY_ID AND  
        POLICY_VERSION_ID=@POLICY_VERSION_ID  
          
 --------------------------------------------------------  
 --- FETCH CLAIM LOSS DATE IF CURRENT POLICY IS MASTER POLICY  
 --------------------------------------------------------       
 IF (@TRANSACTION_TYPE= 14560  ) -- IF CURRENT POLICY IS MASTER POLICY  
 BEGIN     
  SELECT @CLM_LOSS_DATE=LOSS_DATE  
  FROM   CLM_CLAIM_INFO   
  WHERE  CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y'  
 END    
   
   
   
---------------------------------------------------------  
 -- 9  : All Risks and Named Perils     
 -- 26 : Engeneering Risks   
 ---------------------------------------------------------    
 IF(@LOB_ID IN(9,26))  
 BEGIN        
          
   SELECT  P.PERIL_ID AS ITEM_ID ,          
         (ISNULL(L.NAME,'')+'-'+ISNULL(L.LOC_ADD1,'')+'-'+          
          ISNULL(CONVERT(VARCHAR(10), L.LOC_NUM),'')+'-' +          
          ISNULL(L.LOC_ADD2,'')+'-'+ISNULL(L.DISTRICT,'')+'-'+           
          ISNULL(L.LOC_CITY,'')+'-'+ISNULL(L.NUMBER,''))           
          AS ITEM_VALUE          
  FROM    POL_LOCATIONS L WITH(NOLOCK) INNER JOIN          
          POL_PERILS    P WITH(NOLOCK) ON  P.LOCATION>0 AND P.LOCATION=L.LOCATION_ID AND L.CUSTOMER_ID=P.CUSTOMER_ID AND L.POLICY_ID=P.POLICY_ID AND L.POLICY_VERSION_ID=P.POLICY_VERSION_ID           
  WHERE (P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
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
          
  SELECT P.PRODUCT_RISK_ID AS ITEM_ID ,          
         (ISNULL(L.NAME,'')+'-'+ISNULL(L.LOC_ADD1,'')+'-'+          
         ISNULL(CONVERT(VARCHAR(10), L.LOC_NUM),'')+'-' +          
         ISNULL(L.LOC_ADD2,'')+'-'+ISNULL(L.DISTRICT,'')+'-'+           
         ISNULL(L.LOC_CITY,'')+'-'+ISNULL(L.NUMBER,''))           
         AS ITEM_VALUE          
  FROM  POL_LOCATIONS L WITH(NOLOCK) INNER JOIN          
  POL_PRODUCT_LOCATION_INFO  P WITH(NOLOCK) ON  P.LOCATION>0 AND P.LOCATION=L.LOCATION_ID AND L.CUSTOMER_ID=P.CUSTOMER_ID AND L.POLICY_ID=P.POLICY_ID AND L.POLICY_VERSION_ID=P.POLICY_VERSION_ID      
  WHERE ( P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
 END       
 
 ---------------------------------------------------------  
 --1 : Fire  
 ---------------------------------------------------------      
 ELSE IF(@LOB_ID IN (1))    
 BEGIN        
          
  SELECT P.DWELLING_ID  AS ITEM_ID ,          
         (ISNULL(L.NAME,'')+'-'+ISNULL(L.LOC_ADD1,'')+'-'+          
         ISNULL(CONVERT(VARCHAR(10), L.LOC_NUM),'')+'-' +          
         ISNULL(L.LOC_ADD2,'')+'-'+ISNULL(L.DISTRICT,'')+'-'+           
         ISNULL(L.LOC_CITY,'')+'-'+ISNULL(L.NUMBER,''))           
         AS ITEM_VALUE          
  FROM  POL_LOCATIONS L WITH(NOLOCK) INNER JOIN          
  POL_DWELLINGS_INFO  P WITH(NOLOCK) ON  P.LOCATION_ID>0 AND P.LOCATION_ID=L.LOCATION_ID AND L.CUSTOMER_ID=P.CUSTOMER_ID AND L.POLICY_ID=P.POLICY_ID AND L.POLICY_VERSION_ID=P.POLICY_VERSION_ID      
  WHERE ( P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
 END   
 
       
 ---------------------------------------------------------                         
  -- 13 : MARITIME  
 ---------------------------------------------------------             
  ELSE IF(@LOB_ID=13)   
  BEGIN        
          
    SELECT  M.MARITIME_ID AS ITEM_ID,          
   (ISNULL(CAST(M.VESSEL_NUMBER AS NVARCHAR(10)),'')+'-'+ISNULL(M.NAME_OF_VESSEL,'')+'-'+ISNULL(M.TYPE_OF_VESSEL,'')+'-'+          
   ISNULL(CAST(M.MANUFACTURE_YEAR AS NVARCHAR(4)),'')+'-'+          
   ISNULL(M.MANUFACTURER,''))          
   AS ITEM_VALUE          
    FROM    POL_MARITIME M WITH(NOLOCK)   
    WHERE  ( M.CUSTOMER_ID=@CUSTOMER_ID AND M.POLICY_ID=@POLICTY_ID AND M.POLICY_VERSION_ID= @POLICY_VERSION_ID AND M.IS_ACTIVE='Y')             
              
  END             
 ---------------------------------------------------------  
 -- 21 : Group Personal Accident for Passenger   
 -- 34 : Group Life   
 -- 15 : Individual Personal Accident  
 -- 33 : Mortgage  
 ---------------------------------------------------------      
  ELSE IF(@LOB_ID IN (21,34,15,33))  
   BEGIN        
        
       IF (@TRANSACTION_TYPE= 14560 ) -- IF CURRENT POLICY IS MASTER POLICY  
        BEGIN  
   
       
     -- IF CURRENT POLICY IS MASTER POLICY THEN FETCH ONLY RISK RECORDS WHERE   
  -- CLAIM LOSS DATE MUST BE IN BETWEEN ENDORSEMENT EFFECTIVE AND EXPIRE DATE  
    
   SELECT @ENDO_EFF_DATE=EFFECTIVE_DATETIME , @ENDO_EXR_DATE=[EXPIRY_DATE]  
   FROM   POL_POLICY_PROCESS   
   WHERE  CUSTOMER_ID=@CUSTOMER_ID AND  
       POLICY_ID=@POLICTY_ID AND  
       NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID AND  
       PROCESS_STATUS='COMPLETE' AND   
       PROCESS_ID=14  -- WHEN THE PROCESS TYPE IS ENDORSEMENT COMMIT  
       
     IF (@ENDO_EFF_DATE IS NOT NULL )  
      BEGIN  
    SELECT P.PERSONAL_INFO_ID AS ITEM_ID,         
        ISNULL(P.INDIVIDUAL_NAME,'')+'-'+P.CPF_NUM        
        AS ITEM_VALUE           
    FROM   POL_PERSONAL_ACCIDENT_INFO P WITH(NOLOCK)  
       -- INNER JOIN  CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID                   
        --INNER JOIN CLT_APPLICANT_LIST Q ON Q.APPLICANT_ID=P.APPLICANT_ID AND Q.CUSTOMER_ID =P.CUSTOMER_ID  
    WHERE (  P.CUSTOMER_ID=@CUSTOMER_ID AND   
      P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y' AND  
      (@ENDO_EFF_DATE <= @CLM_LOSS_DATE AND @CLM_LOSS_DATE<= @ENDO_EXR_DATE)  
      )      
      END   
      ELSE  
       BEGIN  
         
     SELECT P.PERSONAL_INFO_ID AS ITEM_ID,         
        ISNULL(P.INDIVIDUAL_NAME,'')+'-'+P.CPF_NUM        
        AS ITEM_VALUE       
    FROM   POL_PERSONAL_ACCIDENT_INFO P WITH(NOLOCK)  
       --INNER JOIN  CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID                   
       --INNER JOIN CLT_APPLICANT_LIST Q ON Q.APPLICANT_ID=P.APPLICANT_ID AND Q.CUSTOMER_ID =P.CUSTOMER_ID  
    WHERE (  P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
    END       
                   
                 
       
     
        END  
       ELSE  
        BEGIN    
          
            SELECT P.PERSONAL_INFO_ID AS ITEM_ID,         
        ISNULL(P.INDIVIDUAL_NAME,'')+'-'+P.CPF_NUM        
        AS ITEM_VALUE              
      FROM   POL_PERSONAL_ACCIDENT_INFO P WITH(NOLOCK)  
      -- INNER JOIN  CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID                   
      -- INNER JOIN CLT_APPLICANT_LIST Q ON Q.APPLICANT_ID=P.APPLICANT_ID AND Q.CUSTOMER_ID =P.CUSTOMER_ID  
      WHERE ( P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
                   
     
     
    END  
  END      
    
 ---------------------------------------------------------   
  -- 22 : Personal Accident for Passengers   
 ---------------------------------------------------------      
  ELSE IF(@LOB_ID =22)      
   BEGIN        
        
       IF (@TRANSACTION_TYPE= 14560 ) -- IF CURRENT POLICY IS MASTER POLICY  
        BEGIN  
   
     -- IF CURRENT POLICY IS MASTER POLICY THEN FETCH ONLY RISK RECORDS WHERE   
  -- CLAIM LOSS DATE MUST BE IN BETWEEN RISK EFFECTIVE AND EXPIRE DATE  
    
      SELECT P.PERSONAL_ACCIDENT_ID AS ITEM_ID,         
      ISNULL(Q.FIRST_NAME,'')+' '+ISNULL(Q.MIDDLE_NAME,'')+' '+ISNULL(Q.LAST_NAME,'')+'-'+ISNULL(CAST(P.NUMBER_OF_PASSENGERS AS VARCHAR(20))  ,'')      
      AS ITEM_VALUE        
  FROM          
     POL_PASSENGERS_PERSONAL_ACCIDENT_INFO P WITH(NOLOCK)           
    -- INNER JOIN CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID    
     INNER JOIN CLT_APPLICANT_LIST Q ON Q.APPLICANT_ID=P.CO_APPLICANT_ID AND Q.CUSTOMER_ID =P.CUSTOMER_ID  
      WHERE (  P.CUSTOMER_ID=@CUSTOMER_ID AND   
              P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y' AND  
              (P.[START_DATE] <= @CLM_LOSS_DATE AND @CLM_LOSS_DATE<= P.END_DATE)  
              )                  
       
        END  
       ELSE  
        BEGIN    
        
          
  SELECT P.PERSONAL_ACCIDENT_ID AS ITEM_ID,         
      ISNULL(Q.FIRST_NAME,'')+' '+ISNULL(Q.MIDDLE_NAME,'')+' '+ISNULL(Q.LAST_NAME,'') +'-'+ISNULL(CAST(P.NUMBER_OF_PASSENGERS AS VARCHAR(20))  ,'')   
      AS ITEM_VALUE        
  FROM          
     POL_PASSENGERS_PERSONAL_ACCIDENT_INFO P WITH(NOLOCK)           
    -- INNER JOIN CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID    
     INNER JOIN CLT_APPLICANT_LIST Q ON Q.APPLICANT_ID=P.CO_APPLICANT_ID AND Q.CUSTOMER_ID =P.CUSTOMER_ID  
  WHERE ( P.CUSTOMER_ID=@CUSTOMER_ID AND   
          P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y'  
        )       
     
    END  
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
      
  IF (@TRANSACTION_TYPE= 14560 ) -- IF CURRENT POLICY IS MASTER POLICY  
   BEGIN  
   
 -- IF CURRENT POLICY IS MASTER POLICY THEN FETCH ONLY RISK RECORDS WHERE   
 -- CLAIM LOSS DATE MUST BE IN BETWEEN RISK EFFECTIVE AND EXPIRE DATE  
       
  SELECT P.VEHICLE_ID AS ITEM_ID ,          
            (ISNULL(P.CHASSIS,'')+'-'+ISNULL(CAST(P.MANUFACTURED_YEAR AS NVARCHAR(4)),'')+'-'+          
      ISNULL( P.VEHICLE_MAKE,'')+'-' +          
            iSNULL(P.MAKE_MODEL,''))         
      AS ITEM_VALUE          
     FROM   POL_CIVIL_TRANSPORT_VEHICLES  P WITH(NOLOCK) --INNER JOIN          
           -- CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID                   
           
     WHERE  (  P.CUSTOMER_ID=@CUSTOMER_ID AND   
              P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y' AND  
             (P.RISK_EFFECTIVE_DATE <= @CLM_LOSS_DATE AND @CLM_LOSS_DATE<= P.RISK_EXPIRE_DATE)  
            )      
          
   END  
  ELSE    
   BEGIN  
     
   SELECT P.VEHICLE_ID AS ITEM_ID ,          
           (ISNULL(P.CHASSIS,'')+'-'+ISNULL(CAST(P.MANUFACTURED_YEAR AS NVARCHAR(4)),'')+'-'+          
     ISNULL( P.VEHICLE_MAKE,'')+'-' +          
           iSNULL(P.MAKE_MODEL,''))         
     AS ITEM_VALUE          
   FROM   POL_CIVIL_TRANSPORT_VEHICLES  P WITH(NOLOCK) --INNER JOIN          
          --CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID                   
   WHERE  ( P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
     
   END  
          
    
  END   
  
  
  -- 38 : Motor For SIN
 ELSE IF(@LOB_ID IN (38)) 
  BEGIN        
      
  IF (@TRANSACTION_TYPE= 14560 ) -- IF CURRENT POLICY IS MASTER POLICY  
   BEGIN  
   
 -- IF CURRENT POLICY IS MASTER POLICY THEN FETCH ONLY RISK RECORDS WHERE   
 -- CLAIM LOSS DATE MUST BE IN BETWEEN RISK EFFECTIVE AND EXPIRE DATE  
       
  SELECT P.VEHICLE_ID AS ITEM_ID ,          
            (ISNULL(P.CHASIS_NUMBER,'')+'-'+ISNULL(CAST(P.VEHICLE_YEAR AS NVARCHAR(4)),'')+'-'+          
      ISNULL(MLV.LOOKUP_VALUE_DESC,'')+'-' +          
            iSNULL(MNT_VEHICLE_MODEL_LIST.MODEL,''))         
      AS ITEM_VALUE          
     FROM   POL_VEHICLES  P WITH(NOLOCK) --INNER JOIN          
           -- CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID    
            LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV        
  ON P.MAKE = MLV.LOOKUP_UNIQUE_ID        
  AND MLV.LOOKUP_ID = '1308'         
  INNER JOIN MNT_VEHICLE_MODEL_LIST ON      
  MNT_VEHICLE_MODEL_LIST.ID  = P.MODEL      
     WHERE  (  P.CUSTOMER_ID=@CUSTOMER_ID AND   
              P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y' --AND  
             --(P.RISK_EFFECTIVE_DATE <= @CLM_LOSS_DATE AND @CLM_LOSS_DATE<= P.RISK_EXPIRE_DATE)  
            )      
          
   END  
  ELSE    
   BEGIN  
     
   SELECT P.VEHICLE_ID AS ITEM_ID ,          
           (ISNULL(P.CHASIS_NUMBER,'')+'-'+ISNULL(CAST(P.VEHICLE_YEAR AS NVARCHAR(4)),'')+'-'+          
     ISNULL(MLV.LOOKUP_VALUE_DESC,'')+'-' +          
           iSNULL(MNT_VEHICLE_MODEL_LIST.MODEL,''))         
     AS ITEM_VALUE          
   FROM   POL_VEHICLES  P WITH(NOLOCK) --INNER JOIN          
          --CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID      
           LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV        
  ON P.MAKE = MLV.LOOKUP_UNIQUE_ID        
  AND MLV.LOOKUP_ID = '1308'   
  INNER JOIN MNT_VEHICLE_MODEL_LIST ON      
  MNT_VEHICLE_MODEL_LIST.ID  = P.MODEL          
   WHERE  ( P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
     
   END  
          
    
  END
          
          
          
 ---------------------------------------------------------   
  --  20 : National Cargo Transport  
  --  23 : International Cargo Transport  
 ---------------------------------------------------------                 
ELSE IF(@LOB_ID IN (20,23))      
   BEGIN        
          
  SELECT  P.COMMODITY_ID AS ITEM_ID ,          
          ( ISNULL(CAST(P.COMMODITY_NUMBER AS NVARCHAR(10)),'')+  
           '-'+CASE WHEN @LANG_ID=2 THEN ISNULL(Convert(NVARCHAR(11),DEPARTING_DATE,103),'')   
     ELSE ISNULL(Convert(NVARCHAR(11),DEPARTING_DATE,101),'') END+  
           '-'+ISNULL(P.ORIGIN_CITY ,'')+  
           '-'+ISNULL(P.DESTINATION_CITY ,'')            
          )        
          AS ITEM_VALUE          
  FROM    POL_COMMODITY_INFO  P WITH(NOLOCK)--INNER JOIN          
          --CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID                   
  WHERE  ( P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
 END         
  
  
 ---------------------------------------------------------   
  -- 30 : Dpvat(Cat. 3 e 4)  
  -- 36 : DPVAT(Cat.1,2,9 e 10)  
 ---------------------------------------------------------       
  ELSE IF(@LOB_ID IN (30,36))  
   BEGIN        
            
  SELECT P.VEHICLE_ID AS ITEM_ID ,          
           (ISNULL(CAST(P.TICKET_NUMBER AS NVARCHAR(100)),'')+'-'+          
     ISNULL( M.STATE_NAME,'')  )  
     AS ITEM_VALUE          
   FROM   POL_CIVIL_TRANSPORT_VEHICLES  P WITH(NOLOCK) LEFT OUTER JOIN       
         -- CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID LEFT OUTER JOIN  
          MNT_COUNTRY_STATE_LIST M ON M.STATE_ID=P.STATE_ID  
   WHERE  (  P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
     
  END         
    
 ---------------------------------------------------------   
  -- 35 : Rural Lien  
 ---------------------------------------------------------       
  ELSE IF(@LOB_ID IN (35))  
   BEGIN        
            
  SELECT P.PENHOR_RURAL_ID AS ITEM_ID ,          
           (ISNULL(CAST(P.ITEM_NUMBER AS NVARCHAR(50)),'')+'-'+                
      ISNULL(M.STATE_NAME,'')+'-'+    
      ISNULL(P.CITY,'') +'-'+   
      ISNULL(CAST(P.INSURED_AREA AS NVARCHAR(50)),'')   
     )  
     AS ITEM_VALUE          
   FROM   POL_PENHOR_RURAL_INFO  P WITH(NOLOCK) LEFT OUTER JOIN   
          --CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID LEFT OUTER JOIN  
          MNT_COUNTRY_STATE_LIST M ON M.STATE_ID=P.STATE_ID  
   WHERE  (  P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
     
  END         
    
  ---------------------------------------------------------   
  -- 37 : Rental Security  
 ---------------------------------------------------------       
  ELSE IF(@LOB_ID IN (37))  
   BEGIN        
            
  SELECT P.PENHOR_RURAL_ID AS ITEM_ID ,          
           (ISNULL(CAST(P.ITEM_NUMBER AS NVARCHAR(50)),'')   
     )  
     AS ITEM_VALUE          
   FROM   POL_PENHOR_RURAL_INFO  P WITH(NOLOCK) --INNER JOIN          
         -- CLM_CLAIM_INFO I WITH(NOLOCK) ON I.CUSTOMER_ID=P.CUSTOMER_ID AND I.POLICY_ID=P.POLICY_ID AND I.POLICY_VERSION_ID=P.POLICY_VERSION_ID   
   WHERE  ( P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
     
  END         
          
END   