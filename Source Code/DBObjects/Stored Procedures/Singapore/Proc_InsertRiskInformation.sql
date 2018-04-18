  
 /*----------------------------------------------------------                                                                
Proc Name             : Dbo.Proc_InsertRiskInformation                                                                
Created by            : Santosh Kumar Gautam                                                               
Date                  : 11/11/2010                                                               
Purpose               : To insert the risk information details                      
Revison History       :                                                                
Used In               : claim module                      
------------------------------------------------------------                                                                
Date     Review By          Comments                                   
                          
drop Proc Proc_InsertRiskInformation                                                       
------   ------------       -------------------------*/                                                                
--                                   
                                    
--                                 
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertRiskInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertRiskInformation]
GO                             
CREATE PROCEDURE [dbo].[Proc_InsertRiskInformation]                                    
                                     
 @CUSTOMER_ID   int,                      
 @POLICY_ID    int ,                      
 @POLICY_VERSION_ID  smallint,                   
                     
 @INSURED_PRODUCT_ID    int OUTPUT                      
,@CLAIM_ID    int          
,@CREATED_BY            int                           
,@CREATED_DATETIME      datetime                         
,@IS_ACTIVE             CHAR(1)   
,@DAMAGE_DESCRIPTION     nvarchar(150)=NULL      
                
,@POL_RISK_ID   int     =NULL                          
,@YEAR     smallint    =NULL                         
,@VEHICLE_INSURED_PLEADED_GUILTY  char(1)    =NULL                          
,@VEHICLE_MAKER          nvarchar(150)=NULL                          
,@VEHICLE_MODEL          nvarchar(150)=NULL                          
,@VEHICLE_VIN            nvarchar(150)=NULL                         
                          
,@VESSEL_TYPE            nvarchar(70) =NULL                          
,@VESSEL_NAME            nvarchar(70) =NULL                       
,@VESSEL_MANUFACTURER    nvarchar(50) =NULL                       
,@LOCATION_ADDRESS       nvarchar(150)=NULL                       
,@LOCATION_COMPLIMENT    nvarchar(75) =NULL                       
,@LOCATION_DISTRICT      nvarchar(75) =NULL                       
,@LOCATION_ZIPCODE       nvarchar(11) =NULL       
,@CITY1                  nvarchar(250)=NULL     
,@STATE1                 nvarchar(150)=NULL                         
,@COUNTRY1               nvarchar(150)=NULL          
,@CITY2                  nvarchar(250)=NULL       
,@STATE2                 nvarchar(150)=NULL                       
,@COUNTRY2               nvarchar(150)=NULL                      
,@VOYAGE_CONVEYENCE_TYPE nvarchar(150)=NULL                       
,@VOYAGE_DEPARTURE_DATE  datetime=NULL                       
                      
,@INSURED_NAME          nvarchar(150)=NULL                       
,@EFFECTIVE_DATE        datetime    =NULL                       
,@EXPIRE_DATE           datetime    =NULL                       
  
,@LICENCE_PLATE_NUMBER  nvarchar(50)    
,@DAMAGE_TYPE   int    
,@PERSON_DOB   datetime    
,@PERSON_DiSEASE_DATE   datetime    
,@VOYAGE_CERT_NUMBER    nvarchar(50)    
,@VOYAGE_PREFIX   nvarchar(50)    
,@VESSEL_NUMBER   nvarchar(50)    
,@VOYAGE_TRAN_COMPANY   nvarchar(150)    
,@VOYAGE_IO_DESC  nvarchar(256)    
,@VOYAGE_ARRIVAL_DATE   datetime    
,@VOYAGE_SURVEY_DATE    datetime      
    
,@ITEM_NUMBER   int    
    
,@RURAL_INSURED_AREA    int    
,@RURAL_PROPERTY        int    
,@RURAL_CULTIVATION     int    
,@RURAL_FESR_COVERAGE   int    
,@RURAL_MODE   int    
,@RURAL_SUBSIDY_PREMIUM decimal(18,2)    
    
,@PA_NUM_OF_PASS  numeric(18,0)    
,@DP_TICKET_NUMBER  int    
,@DP_CATEGORY   int    
,@ACTUAL_INSURED_OBJECT nvarchar(250)    
,@RISK_CO_APP_ID        int  =0  
          
                                  
    
                 
AS                                    
BEGIN                     
                        
  DECLARE @RISK_ID INT =0   
  DECLARE @PARTY_ID INT =0   
  DECLARE @LOBID INT = 0
  
                       
                        
  SELECT @INSURED_PRODUCT_ID=(ISNULL(MAX([INSURED_PRODUCT_ID]),0)+1)  FROM [dbo].[CLM_INSURED_PRODUCT]  WITH(NOLOCK)                  
  SELECT @LOBID = POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID
                        
  INSERT INTO [dbo].[CLM_INSURED_PRODUCT]                      
           (                       
    [INSURED_PRODUCT_ID]                                                 
   ,[CLAIM_ID]           
   ,[POL_RISK_ID]          
   ,VEHICLE_INSURED_PLEADED_GUILTY               
   ,VEHICLE_MAKER            
   ,VEHICLE_MODEL            
   ,VEHICLE_VIN           
   ,DAMAGE_DESCRIPTION                
   ,VESSEL_TYPE           
   ,VESSEL_NAME           
   ,VESSEL_MANUFACTURER                 
   ,LOCATION_ADDRESS              
   ,LOCATION_COMPLIMENT                 
   ,LOCATION_DISTRICT               
   ,LOCATION_ZIPCODE         
   ,VOYAGE_CONVEYENCE_TYPE                   
   ,VOYAGE_DEPARTURE_DATE                  
   ,INSURED_NAME           
   ,EFFECTIVE_DATE             
   ,EXPIRE_DATE           
   ,IS_ACTIVE           
   ,CREATED_BY           
   ,CREATED_DATETIME         
   ,VESSEL_NUMBER          
   ,PERSON_DISEASE_DATE         
   ,VOYAGE_PREFIX           
   ,VOYAGE_ARRIVAL_DATE         
   ,VOYAGE_SURVEY_DATE          
   ,DAMAGE_TYPE            
   ,PERSON_DOB           
   ,VOYAGE_CERT_NUMBER         
   ,VOYAGE_TRAN_COMPANY          
   ,VOYAGE_IO_DESC          
   ,ITEM_NUMBER         
   ,RURAL_INSURED_AREA         
   ,RURAL_PROPERTY          
   ,RURAL_CULTIVATION         
   ,RURAL_FESR_COVERAGE         
   ,RURAL_MODE           
   ,RURAL_SUBSIDY_PREMIUM        
   ,PA_NUM_OF_PASS          
   ,DP_TICKET_NUMBER         
   ,DP_CATEGORY           
   ,STATE1            
   ,COUNTRY1           
   ,COUNTRY2           
   ,STATE2            
   ,CITY1            
   ,CITY2            
   ,LICENCE_PLATE_NUMBER        
   ,[YEAR]      
   ,ACTUAL_INSURED_OBJECT   
   ,RISK_CO_APP_ID      
      
           )                      
                               
     VALUES                      
           (                      
   @INSURED_PRODUCT_ID                      
   ,@CLAIM_ID                      
   ,@POL_RISK_ID                   
   ,@VEHICLE_INSURED_PLEADED_GUILTY                      
   ,@VEHICLE_MAKER                      
   ,@VEHICLE_MODEL                      
   ,@VEHICLE_VIN                      
   ,@DAMAGE_DESCRIPTION                      
   ,@VESSEL_TYPE                      
   ,@VESSEL_NAME                      
   ,@VESSEL_MANUFACTURER                      
   ,@LOCATION_ADDRESS                      
   ,@LOCATION_COMPLIMENT                      
   ,@LOCATION_DISTRICT                      
   ,@LOCATION_ZIPCODE     
   ,@VOYAGE_CONVEYENCE_TYPE                      
   ,@VOYAGE_DEPARTURE_DATE                      
   ,@INSURED_NAME                      
   ,@EFFECTIVE_DATE                      
   ,@EXPIRE_DATE                      
   ,'Y'                      
   ,@CREATED_BY                      
   ,@CREATED_DATETIME       
   ,@VESSEL_NUMBER        
   ,@PERSON_DISEASE_DATE       
   ,@VOYAGE_PREFIX        
   ,@VOYAGE_ARRIVAL_DATE       
   ,@VOYAGE_SURVEY_DATE       
   ,@DAMAGE_TYPE         
   ,@PERSON_DOB         
   ,@VOYAGE_CERT_NUMBER       
   ,@VOYAGE_TRAN_COMPANY       
   ,@VOYAGE_IO_DESC        
   ,@ITEM_NUMBER       
   ,@RURAL_INSURED_AREA       
   ,@RURAL_PROPERTY        
   ,@RURAL_CULTIVATION       
   ,@RURAL_FESR_COVERAGE       
   ,@RURAL_MODE         
   ,@RURAL_SUBSIDY_PREMIUM      
   ,@PA_NUM_OF_PASS        
   ,@DP_TICKET_NUMBER       
   ,@DP_CATEGORY         
   ,@STATE1          
   ,@COUNTRY1         
   ,@COUNTRY2         
   ,@STATE2          
   ,@CITY1          
   ,@CITY2          
   ,@LICENCE_PLATE_NUMBER      
   ,@YEAR         
   ,@ACTUAL_INSURED_OBJECT   
   ,@RISK_CO_APP_ID  
       
    
         
                         
    )                      
                            
                   
             
   IF(@POL_RISK_ID IS NOT NULL AND @POL_RISK_ID>0)                  
   SET @RISK_ID =@POL_RISK_ID                   
                     
   
 ---------------------------------------------------------------    
 -- ADDED BY SANTOSH KUMAR GAUTAM ON 03 MARCH 2011 (ITRACK:852)    
 -- FOR COPY BENEFICIARY PARTY  
 ---------------------------------------------------------------   
 SELECT @PARTY_ID = ISNULL(MAX(PARTY_ID),0)+1  FROM CLM_PARTIES  WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID                                                
                                  
INSERT INTO CLM_PARTIES                                                
 (                                                
  PARTY_ID,                                                
  CLAIM_ID,    
  PARTY_TYPE_ID,                                            
  NAME,    
  PARTY_PERCENTAGE,  
  OTHER_DETAILS,  
  [STATE],  
  CREATED_BY,                                                
  CREATED_DATETIME,      
  IS_ACTIVE         
 )                              
(       
  SELECT              
  @PARTY_ID+row_number() OVER(ORDER BY CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID,BENEFICIARY_ID  asc) ,                                            
  @CLAIM_ID,       
  157,     --- FOR BENFICIARY      
  BENEFICIARY_NAME,  -- NAME  
  BENEFICIARY_SHARE,  
  BENEFICIARY_RELATION, --OTHER_DETAILS  
  0,     --[STATE]  
  @CREATED_BY,  
  @CREATED_DATETIME,  
  'Y'  
  FROM POL_BENEFICIARY  WITH(NOLOCK)          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID               
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK_ID=@POL_RISK_ID   
      
  )          
       
                    
  -----------------------------------------------------------------------------------------   
  -- @LIMIT_OVERRIDE WOULD BE 'Y' if litigation flag on Claim is set to ‘Yes’ or         
  -- Claim is Co-Insurance Claim (Co-Insurance Type is Follower on Policy Main Page),         
  -- update override Limit to ‘Y’ to claim Coverages        
  -----------------------------------------------------------------------------------------  
   DECLARE @LIMIT_OVERRIDE  CHAR(1)='N'        
   DECLARE @CLAIM_COV_ID  INT  
   DECLARE @XOL_CONTRACT INT=0  
          
   SELECT @LIMIT_OVERRIDE=CASE WHEN LITIGATION_FILE=10963 THEN 'Y' ELSE 'N' END ,  
          @XOL_CONTRACT  =CATASTROPHE_EVENT_CODE  
   FROM CLM_CLAIM_INFO WITH(NOLOCK)         
   WHERE CLAIM_ID=@CLAIM_ID        
              
   --IF(@LIMIT_OVERRIDE='N')             
   --BEGIN        
   --   -- HERE 14549 MEANS CO_INSURANCE IS FOLLOWER TYPE        
   --   SELECT @LIMIT_OVERRIDE= CASE WHEN CO_INSURANCE =14549 THEN 'Y' ELSE 'N' END         
   --   FROM POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)      
   --   WHERE (CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND IS_ACTIVE='Y')          
   --END        
        
    SELECT @CLAIM_COV_ID=(ISNULL(MAX([CLAIM_COV_ID]),0))  FROM [dbo].[CLM_PRODUCT_COVERAGES]  WITH(NOLOCK)                  
                 
   -- COPY COVERAGES OF RESPECTIVE RISK                
    IF(@LOBID = 38)
    BEGIN
		INSERT INTO [dbo].[CLM_PRODUCT_COVERAGES]                      
           (  
            [CLAIM_COV_ID]  
           ,[CLAIM_ID]                      
           ,[RISK_ID]                
           ,[COVERAGE_CODE_ID]                
           ,[RI_APPLIES]                
           ,[LIMIT_OVERRIDE]                
           ,[LIMIT_1]              
           ,[POLICY_LIMIT]          
           ,[DEDUCTIBLE_1]        
           ,[DEDUCTIBLE_1_TYPE]  
           ,[DEDUCTIBLE1_AMOUNT_TEXT]   
           ,[MINIMUM_DEDUCTIBLE]        
           ,[IS_RISK_COVERAGE]                
           ,[IS_ACTIVE]                
           ,[CREATED_BY]                
           ,[CREATED_DATETIME]  
           ,[VICTIM_ID]             
        )                     
           (                
            SELECT          
            @CLAIM_COV_ID+ ROW_NUMBER()OVER(ORDER BY COV_ID ASC)       
           ,@CLAIM_ID          --<CLAIM_ID, int,>                
           ,@INSURED_PRODUCT_ID   --<RISK_ID, int,>                
           ,COVERAGE_CODE_ID  --<COVERAGE_CODE_ID, int,>                
           ,(CASE WHEN REINSURANCE_LOB=10963 THEN 'Y' ELSE 'N' END) AS RI_APPLIES    --<RI_APPLIES, nchar(1),>                
           ,CASE WHEN @LIMIT_OVERRIDE='Y' THEN 'Y' ELSE 'N' END      --<LIMIT_OVERRIDE, nvarchar(5),>                
           ,CASE WHEN LIMIT_1 IS NULL  THEN 0 ELSE LIMIT_1 END     --<LIMIT_1, decimal(18,0),>                
           ,CASE WHEN LIMIT_1 IS NULL  THEN 0 ELSE LIMIT_1 END       --<POLICY_LIMIT, decimal(18,0),>           
           ,CASE WHEN DEDUCTIBLE_1 IS NULL  THEN 0 ELSE DEDUCTIBLE_1 END     --<DEDUCTIBLE_1, decimal(18,0),>                
           ,DEDUCTIBLE_1_TYPE  
           ,[DEDUCTIBLE2_AMOUNT_TEXT]  
           ,ISNULL(P.MINIMUM_DEDUCTIBLE,0)  
           --=================================================  
           -- UPDATED BY SANTOSH KR. GAUTAM ON 28 MARCH 2011  
           -- ITRACK :1014             
           --=================================================  
             
              --      ,CASE --WHEN ([DEDUCTIBLE2_AMOUNT_TEXT] IS NOT NULL )AND ([DEDUCTIBLE2_AMOUNT_TEXT] !='' ) AND ([DEDUCTIBLE2_AMOUNT_TEXT] !='0' )THEN 0   
     ----WHEN ([DEDUCTIBLE_1] IS NOT NULL )AND ([DEDUCTIBLE_1] !=0 )THEN 0              
     --WHEN ([MINIMUM_DEDUCTIBLE] IS NULL )THEN 0              
     --ELSE [MINIMUM_DEDUCTIBLE] END --[MINIMUM_DEDUCTIBLE]     
           ,'Y'      --<IS_RISK_COVERAGE, nchar(1),>                
           ,'Y'      --<IS_ACTIVE, nchar(1),>                
           ,@CREATED_BY                
           ,@CREATED_DATETIME    
           ,0                    
            FROM POL_PRODUCT_COVERAGES P WITH(NOLOCK) INNER JOIN            
            MNT_COVERAGE M WITH(NOLOCK) ON P.COVERAGE_CODE_ID=M.COV_ID            
           WHERE   CUSTOMER_ID=@CUSTOMER_ID         
    AND POLICY_ID=@POLICY_ID         
    AND POLICY_VERSION_ID=@POLICY_VERSION_ID        
    AND RISK_ID=@RISK_ID                 
           ) 
    END
    ELSE
    BEGIN
    INSERT INTO [dbo].[CLM_PRODUCT_COVERAGES]                      
           (  
            [CLAIM_COV_ID]  
           ,[CLAIM_ID]                      
           ,[RISK_ID]                
           ,[COVERAGE_CODE_ID]                
           ,[RI_APPLIES]                
           ,[LIMIT_OVERRIDE]                
           ,[LIMIT_1]              
           ,[POLICY_LIMIT]          
           ,[DEDUCTIBLE_1]        
           ,[DEDUCTIBLE_1_TYPE]  
           ,[DEDUCTIBLE1_AMOUNT_TEXT]   
           ,[MINIMUM_DEDUCTIBLE]        
           ,[IS_RISK_COVERAGE]                
           ,[IS_ACTIVE]                
           ,[CREATED_BY]                
           ,[CREATED_DATETIME]  
           ,[VICTIM_ID]             
        )                     
           (                
            SELECT          
            @CLAIM_COV_ID+ ROW_NUMBER()OVER(ORDER BY COV_ID ASC)       
           ,@CLAIM_ID          --<CLAIM_ID, int,>                
           ,@INSURED_PRODUCT_ID   --<RISK_ID, int,>                
           ,COVERAGE_CODE_ID  --<COVERAGE_CODE_ID, int,>                
           ,(CASE WHEN REINSURANCE_LOB=10963 THEN 'Y' ELSE 'N' END) AS RI_APPLIES    --<RI_APPLIES, nchar(1),>                
           ,CASE WHEN @LIMIT_OVERRIDE='Y' THEN 'Y' ELSE 'N' END      --<LIMIT_OVERRIDE, nvarchar(5),>                
           ,CASE WHEN LIMIT_1 IS NULL  THEN 0 ELSE LIMIT_1 END     --<LIMIT_1, decimal(18,0),>                
           ,CASE WHEN LIMIT_1 IS NULL  THEN 0 ELSE LIMIT_1 END       --<POLICY_LIMIT, decimal(18,0),>           
           ,CASE WHEN DEDUCTIBLE_1 IS NULL  THEN 0 ELSE DEDUCTIBLE_1 END     --<DEDUCTIBLE_1, decimal(18,0),>                
           ,DEDUCTIBLE_1_TYPE  
           ,[DEDUCTIBLE2_AMOUNT_TEXT]  
           ,ISNULL(P.DEDUCTIBLE,0)  
           --=================================================  
           -- UPDATED BY SANTOSH KR. GAUTAM ON 28 MARCH 2011  
           -- ITRACK :1014             
           --=================================================  
             
              --      ,CASE --WHEN ([DEDUCTIBLE2_AMOUNT_TEXT] IS NOT NULL )AND ([DEDUCTIBLE2_AMOUNT_TEXT] !='' ) AND ([DEDUCTIBLE2_AMOUNT_TEXT] !='0' )THEN 0   
     ----WHEN ([DEDUCTIBLE_1] IS NOT NULL )AND ([DEDUCTIBLE_1] !=0 )THEN 0              
     --WHEN ([MINIMUM_DEDUCTIBLE] IS NULL )THEN 0              
     --ELSE [MINIMUM_DEDUCTIBLE] END --[MINIMUM_DEDUCTIBLE]     
           ,'Y'      --<IS_RISK_COVERAGE, nchar(1),>                
           ,'Y'      --<IS_ACTIVE, nchar(1),>                
           ,@CREATED_BY                
           ,@CREATED_DATETIME    
           ,0                    
            FROM POL_DWELLING_SECTION_COVERAGES P WITH(NOLOCK) INNER JOIN            
            MNT_COVERAGE M WITH(NOLOCK) ON P.COVERAGE_CODE_ID=M.COV_ID            
           WHERE   CUSTOMER_ID=@CUSTOMER_ID         
    AND POLICY_ID=@POLICY_ID         
    AND POLICY_VERSION_ID=@POLICY_VERSION_ID        
    AND DWELLING_ID=@RISK_ID                 
           )    
           END
                
       
 SELECT @CLAIM_COV_ID=(ISNULL(MAX([CLAIM_COV_ID]),0))  FROM [dbo].[CLM_PRODUCT_COVERAGES]  WITH(NOLOCK)                  
                      
   -- COPY ADDITION 6 COVERAGES                 
    INSERT INTO [dbo].[CLM_PRODUCT_COVERAGES]                
           (  
           [CLAIM_COV_ID]  
           ,[CLAIM_ID]                
           ,[RISK_ID]                
           ,[COVERAGE_CODE_ID]                
           ,[RI_APPLIES]                
           ,[LIMIT_OVERRIDE]                
           ,[LIMIT_1]                
           ,[POLICY_LIMIT]                   
           ,[DEDUCTIBLE_1]    
           ,[MINIMUM_DEDUCTIBLE]              
           ,[IS_RISK_COVERAGE]                
           ,[IS_ACTIVE]                
           ,[CREATED_BY]                
           ,[CREATED_DATETIME]                
         )                     
         (                
           SELECT    
            @CLAIM_COV_ID+ ROW_NUMBER()OVER(ORDER BY COV_ID ASC)                 
           ,@CLAIM_ID  --<CLAIM_ID, int,>                
           ,@INSURED_PRODUCT_ID  --<RISK_ID, int,>                
           ,COV_ID    --<COVERAGE_CODE_ID, int,>                
           ,'Y'       --<RI_APPLIES, nchar(1),>                
           ,'Y'       --<LIMIT_OVERRIDE, nvarchar(5),>                
           ,0         --<LIMIT_1, decimal(18,0),>         
           ,0         --<POLICY_LIMIT, decimal(18,0),>                
           ,0         --<DEDUCTIBLE_1, decimal(18,0),>   
           ,0         --[MINIMUM_DEDUCTIBLE]               
           ,'N'       --<IS_RISK_COVERAGE, nchar(1),>                
           ,'Y'       --<IS_ACTIVE, nchar(1),>                
           ,@CREATED_BY            
           ,@CREATED_DATETIME                             
           FROM MNT_COVERAGE_EXTRA  WITH(NOLOCK)              
           WHERE (COV_ID IN (50022,50019,50020,50021,50018,50017))                
          )                
            
               
          
-----------------------------------------------------------    
--  FOR COPY REINSURANCE DATA    
--  TO COPY REINURANCE PARTY AS PER RISK SELECTED    
-----------------------------------------------------------       
           
     
  SELECT @PARTY_ID = ISNULL(MAX(PARTY_ID),0)+1  FROM CLM_PARTIES  WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID          
         
 INSERT INTO CLM_PARTIES      
   (      
    CLAIM_ID,                
    PARTY_ID,     
    PARTY_TYPE_ID,     
    PARTY_CODE,    
    PARTY_TYPE,             
    NAME,         
    ADDRESS1,     
    ADDRESS2,        
    CITY,        
    [STATE],      
    ZIP,          
    COUNTRY,      
    CONTACT_PHONE,     
    CONTACT_EMAIL,    
    CONTACT_FAX,      
    SOURCE_PARTY_ID,    
    PARTY_PERCENTAGE,    
    SOURCE_PARTY_TYPE_ID,    
    IS_ACTIVE,     
    CREATED_BY,     
    CREATED_DATETIME  ,    
    PARTY_CPF_CNPJ,    
    NUMBER,    
    CONTACT_PHONE_EXT,    
    ACCOUNT_NUMBER,    
    FEDRERAL_ID,    
    ACCOUNT_TYPE,    
    BANK_BRANCH,    
    BANK_NUMBER    
        
        )       
     (      
        SELECT       
  @CLAIM_ID,      
  @PARTY_ID+row_number() OVER(ORDER BY M.REIN_COMAPANY_ID asc) ,        
  619,--PARTY_TYPE_ID check in CLM_TYPE_DETAIL and Clm_type_master    
  REIN_COMAPANY_CODE,    
  11109,--PARTY_TYPE FOR commercial      
  REIN_COMAPANY_NAME,    
  REIN_COMAPANY_ADD1,    
  REIN_COMAPANY_ADD2,      
  REIN_COMAPANY_CITY,    
  ISNULL(M2.STATE_ID,0) ,--REIN_COMAPANY_STATE,      
  REIN_COMAPANY_ZIP,     
  ISNULL(M1.COUNTRY_ID,0) ,--REIN_COMAPANY_COUNTRY,      
  REIN_COMAPANY_PHONE,    
  REIN_COMAPANY_EMAIL,      
  REIN_COMAPANY_FAX,    
  P.MAJOR_PARTICIPANT ,-- SOURCE_PARTY_ID,    
  SUM(P.COMM_PERCENTAGE),  -- PARTY_PERCENTAGE,    
  NULL,     -- SOURCE_PARTY_TYPE_ID,      
  'Y',    
  @CREATED_BY,    
  @CREATED_DATETIME  ,    
  CARRIER_CNPJ  ,    
   (CASE WHEN len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',','')) > 0 THEN     
    CASE WHEN ISNUMERIC(DBO.Piece(REIN_COMAPANY_ADD1,',',len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',',''))+1)) =1     
    THEN    
    DBO.Piece(REIN_COMAPANY_ADD1,',',len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',',''))+1)    
    END    
    END     
    ),    
  REIN_COMAPANY_EXT ,    
        REIN_COMAPANY_ACC_NUMBER ,    
        FEDERAL_ID,    
        BANK_ACCOUNT_TYPE ,    
        BANK_BRANCH_NUMBER,    
        BANK_NUMBER          
  FROM [MNT_REIN_COMAPANY_LIST] M INNER JOIN      
  POL_REINSURANCE_BREAKDOWN_DETAILS P ON P.MAJOR_PARTICIPANT=M.REIN_COMAPANY_ID  LEFT OUTER JOIN    
  MNT_COUNTRY_LIST M1 ON M1.COUNTRY_NAME=M.REIN_COMAPANY_COUNTRY  LEFT OUTER JOIN    
  MNT_COUNTRY_STATE_LIST M2 ON M2.STATE_CODE=M.REIN_COMAPANY_STATE      
  WHERE (CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
  AND dbo.Instring(CAST(P.RISK_ID AS NVARCHAR(250)),CAST(@POL_RISK_ID AS NVARCHAR(250)))>0  
  AND P.COMM_PERCENTAGE IS NOT NULL )      
  GROUP BY     
  REIN_COMAPANY_CODE,    
  REIN_COMAPANY_NAME,    
  REIN_COMAPANY_ADD1,    
  REIN_COMAPANY_ADD2,      
  REIN_COMAPANY_CITY,    
  ISNULL(M2.STATE_ID,0) ,--REIN_COMAPANY_STATE,      
  REIN_COMAPANY_ZIP,     
  ISNULL(M1.COUNTRY_ID,0) ,--REIN_COMAPANY_COUNTRY,      
  REIN_COMAPANY_PHONE,    
  REIN_COMAPANY_EMAIL,      
  REIN_COMAPANY_FAX,    
  P.MAJOR_PARTICIPANT ,-- SOURCE_PARTY_ID,      
  CARRIER_CNPJ  ,    
   (CASE WHEN len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',','')) > 0 THEN     
    CASE WHEN ISNUMERIC(DBO.Piece(REIN_COMAPANY_ADD1,',',len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',',''))+1)) =1     
    THEN    
    DBO.Piece(REIN_COMAPANY_ADD1,',',len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',',''))+1)    
    END    
    END     
    ),    
  REIN_COMAPANY_EXT ,    
        REIN_COMAPANY_ACC_NUMBER ,    
        FEDERAL_ID,    
        BANK_ACCOUNT_TYPE ,    
        BANK_BRANCH_NUMBER,    
        BANK_NUMBER       ,    
        REIN_COMAPANY_ID,    
        CONTRACT_NUMBER    
      
      )                  
   
   
 ------------------------------------------------------------------------------    
--  FOR COPY REINSURANCE DATA OF XOL CONTRACT    
--  WHEN CLAIM IS XOL TYPE THEN COPY THE REINSURANCE PARTY CORRESPODANT TO XOL   
--  (REINSURANCE PARTY FROM MAJOR PARTICIPATION TAB)  
-------------------------------------------------------------------------------    
IF(@XOL_CONTRACT IS NOT NULL AND @XOL_CONTRACT>0)  
BEGIN  
  
     
     
  SELECT @PARTY_ID = ISNULL(MAX(PARTY_ID),0)+1  FROM CLM_PARTIES  WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID          
         
 INSERT INTO CLM_PARTIES      
   (      
    CLAIM_ID,                
    PARTY_ID,     
    PARTY_TYPE_ID,     
    PARTY_CODE,    
    PARTY_TYPE,             
    NAME,         
    ADDRESS1,     
    ADDRESS2,        
    CITY,        
    [STATE],      
    ZIP,          
    COUNTRY,      
    CONTACT_PHONE,     
    CONTACT_EMAIL,    
    CONTACT_FAX,      
    SOURCE_PARTY_ID,    
    --PARTY_PERCENTAGE,    
    SOURCE_PARTY_TYPE_ID,    
    IS_ACTIVE,     
    CREATED_BY,     
    CREATED_DATETIME  ,    
    PARTY_CPF_CNPJ,    
    NUMBER,    
    CONTACT_PHONE_EXT,    
    ACCOUNT_NUMBER,    
    FEDRERAL_ID,    
    ACCOUNT_TYPE,    
    BANK_BRANCH,    
    BANK_NUMBER    
        
        )       
        (  
   SELECT  
    @CLAIM_ID,      
    @PARTY_ID+row_number() OVER(ORDER BY A.REIN_COMAPANY_ID asc) ,        
    619,--PARTY_TYPE_ID check in CLM_TYPE_DETAIL and Clm_type_master    
    REIN_COMAPANY_CODE,    
    11109,--PARTY_TYPE FOR commercial      
    REIN_COMAPANY_NAME,    
    REIN_COMAPANY_ADD1,    
    REIN_COMAPANY_ADD2,      
    REIN_COMAPANY_CITY,    
    ISNULL(M2.STATE_ID,0) ,--REIN_COMAPANY_STATE,      
    REIN_COMAPANY_ZIP,     
    ISNULL(M1.COUNTRY_ID,0) ,--REIN_COMAPANY_COUNTRY,      
    REIN_COMAPANY_PHONE,    
    REIN_COMAPANY_EMAIL,      
    REIN_COMAPANY_FAX,    
    A.REIN_COMAPANY_ID ,-- SOURCE_PARTY_ID,    
   -- SUM(P.COMM_PERCENTAGE),  -- PARTY_PERCENTAGE,    
    NULL,     -- SOURCE_PARTY_TYPE_ID,      
    'Y',    
    @CREATED_BY,    
    @CREATED_DATETIME  ,    
    CARRIER_CNPJ  ,    
     (CASE WHEN len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',','')) > 0 THEN     
   CASE WHEN ISNUMERIC(DBO.Piece(REIN_COMAPANY_ADD1,',',len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',',''))+1)) =1     
   THEN    
   DBO.Piece(REIN_COMAPANY_ADD1,',',len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',',''))+1)    
   END    
   END     
   ),    
    REIN_COMAPANY_EXT ,    
    REIN_COMAPANY_ACC_NUMBER ,    
    FEDERAL_ID,    
    BANK_ACCOUNT_TYPE ,    
    BANK_BRANCH_NUMBER,    
    BANK_NUMBER          
   FROM [MNT_REIN_COMAPANY_LIST] A INNER JOIN    
     MNT_REINSURANCE_MAJORMINOR_PARTICIPATION B ON B.REINSURANCE_COMPANY=A.REIN_COMAPANY_ID LEFT OUTER JOIN    
     MNT_COUNTRY_LIST M1 ON M1.COUNTRY_NAME=A.REIN_COMAPANY_COUNTRY  LEFT OUTER JOIN    
     MNT_COUNTRY_STATE_LIST M2 ON M2.STATE_CODE=A.REIN_COMAPANY_STATE      
   WHERE CONTRACT_ID =@XOL_CONTRACT AND B.IS_ACTIVE='Y' AND  
     REINSURANCE_COMPANY NOT IN   
     (   
       SELECT REINSURANCE_COMPANY FROM  CLM_PARTIES C INNER JOIN  
       MNT_REINSURANCE_MAJORMINOR_PARTICIPATION M ON M.CONTRACT_ID =@XOL_CONTRACT AND M.REINSURANCE_COMPANY=C.SOURCE_PARTY_ID   
       WHERE CLAIM_ID=@CLAIM_ID AND SOURCE_PARTY_ID IS NOT NULL AND PARTY_TYPE_ID=619 -- FOR REINSURANE PARTY TYPE  
     )  
       )  
      
          
     
END  
  
  
                        
END       
      
  
  
  