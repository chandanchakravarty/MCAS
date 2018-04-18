--- FINAL QUERY POLICY_RISK AND COVERAGES FOR SHEET HOSPITAL_05_SEM_IOF
--BEGIN TRAN

--- START INSERT INTO RISK LEVEL
--DROP TABLE HOSPITAL_05_SEM_IOF
--------------------------------------------------------------------------------------------------------------------------------
GO
IF NOT EXISTS(SELECT  COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'HOSPITAL_05_SEM_IOF' 
                        AND COLUMN_NAME = 'NEW_CODE') 
                  BEGIN
                        ALTER TABLE HOSPITAL_05_SEM_IOF
                        ADD  NEW_CODE INT NULL                        
                        
                   END
  GO
 -------------------------------------------------------------------------------------------------------------------------------------     
 IF EXISTS( SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS
				WHERE TABLE_NAME ='HOSPITAL_05_SEM_IOF'
				      AND COLUMN_NAME = 'CODE' )				     
				BEGIN 				
					UPDATE HOSPITAL_05_SEM_IOF SET NEW_CODE=CODE 
				
				END     
GO
------------------------------------------------------------------------------------------------------------------------------------------				
				             
IF EXISTS(SELECT  COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_PERSONAL_ACCIDENT_INFO' 
                        AND COLUMN_NAME = 'INDIVIDUAL_NAME') 
                  BEGIN
                        ALTER TABLE POL_PERSONAL_ACCIDENT_INFO
                        ALTER COLUMN INDIVIDUAL_NAME NVARCHAR(100) NULL
                        
                   END
  GO
-------------------------------------------------------------------------------------------------------------------------------------             
                   
    
     SELECT APPLICATION_NO,COAPP,SUBLOB,COAPP_CNPJ,INDIVIDUAL_NAME,IS_SPOUSE_OR_CHILD,GENDER,CPF_CNPJ,STATE,
                 DATE_OF_BIRTH,REGIONAL_IDENTIFICATION ,REGIONAL_ID,REGIONAL_ID_ISSUES,POSITION,CITY_OF_BIRTH ,NEW_CODE INTO #TEMP_HOSPITAL_05_SEM_IOF FROM HOSPITAL_05_SEM_IOF             
------------------------------------------------------------------------------------------------------------------------------------                             
DECLARE @PERSONAL_INFO_ID INT,
	@POLICY_ID INT=4,
	@POLICY_VERSION_ID INT=1 ,
	@CUSTOMER_ID INT=3804,
	@COVERAGE_ID_MAX INT,
	@COUNT INT=0,
	@COUNTER INT=1


	SELECT  @PERSONAL_INFO_ID=ISNULL(MAX(PERSONAL_INFO_ID),0) 
	FROM POL_PERSONAL_ACCIDENT_INFO WITH(NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID    
------------------------------------------------------------------------------------------------------------------------------------------------------
	INSERT INTO POL_PERSONAL_ACCIDENT_INFO (
	
PERSONAL_INFO_ID,    
POLICY_ID ,     
POLICY_VERSION_ID ,     
CUSTOMER_ID ,    
INDIVIDUAL_NAME,     
CODE,    
POSITION_ID,    
CPF_NUM ,    
STATE_ID ,    
DATE_OF_BIRTH ,    
GENDER ,    
REG_IDEN ,    
REG_ID_ISSUES,      
REG_ID_ORG,    
APPLICANT_ID ,  
IS_SPOUSE_OR_CHILD,  
CITY_OF_BIRTH  ,
COUNTRY_ID,
IS_ACTIVE,
CREATED_BY,
CREATED_DATETIME,
MODIFIED_BY,
LAST_UPDATED_DATETIME,
ORIGINAL_VERSION_ID

 )

SELECT
 (ROW_NUMBER()OVER(ORDER BY NEW_CODE ASC)+ @PERSONAL_INFO_ID) AS PERSONAL_INFO_ID,
 
@POLICY_ID AS POLICY_ID,
@POLICY_VERSION_ID AS POLICY_VERSION_ID,
@CUSTOMER_ID AS CUSTOMER_ID,
INDIVIDUAL_NAME ,     
cast(NEW_CODE as varchar(20)) as CODE,    
21000 AS POSITION_ID,    
CPF_CNPJ ,    
64 AS STATE_ID,    
DATE_OF_BIRTH ,    
GENDER ,    
REGIONAL_IDENTIFICATION ,    
REGIONAL_ID_ISSUES,    
REGIONAL_ID,    
1406 AS APPLICANT_ID ,  
IS_SPOUSE_OR_CHILD,  
CITY_OF_BIRTH ,
5 AS COUNTRY_ID,
'Y' AS IS_ACTIVE,
248 AS CREATED_BY,
GETDATE() AS CREATED_DATETIME,
248 AS MODIFIED_BY,
GETDATE() AS LAST_UPDATED_DATETIME,
0 AS ORIGINAL_VERSION_ID
 from #TEMP_HOSPITAL_05_SEM_IOF with(nolock)
                 GROUP BY APPLICATION_NO,COAPP,SUBLOB,COAPP_CNPJ,INDIVIDUAL_NAME,IS_SPOUSE_OR_CHILD,GENDER,CPF_CNPJ,STATE,
                 DATE_OF_BIRTH,REGIONAL_IDENTIFICATION ,REGIONAL_ID,REGIONAL_ID_ISSUES,POSITION,CITY_OF_BIRTH ,NEW_CODE
                  ORDER BY NEW_CODE
  
    
  -------------------------------------------------------------------------------------------------------------------------------------------
  --SELECT * FROM POL_PERSONAL_ACCIDENT_INFO WITH(NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID --and CODE=8   -- order by PERSONAL_INFO_ID asc
 -- SELECT * FROM POL_PRODUCT_COVERAGES WITH(NOLOCK) WHERE   CUSTOMER_ID=6157 AND POLICY_ID=167
  -- DROP TABLE #TEMP_POL_PERSONAL_ACCIDENT_INFO
  -------------------------------------------------------------------------------------------------------------------------------------------
 CREATE TABLE #TEMP_POL_PERSONAL_ACCIDENT_INFO
 (ID INT IDENTITY(1,1),
 PERSONAL_INFO_ID INT ,
  COVERAGE_CODE float,                                             
      DEDUCTIBLE_TYPE float,                                
      SUM_INSURED float,                                   
      PREMIUM float,                                        
      MINIMUM_DEDUCTIBLE float ,
      NEW_CODE FLOAT
 )
  INSERT  INTO #TEMP_POL_PERSONAL_ACCIDENT_INFO
  ( 
	  PERSONAL_INFO_ID,
	  COVERAGE_CODE,                                         
      DEDUCTIBLE_TYPE,                                
      SUM_INSURED,                                   
      PREMIUM,                                        
      MINIMUM_DEDUCTIBLE ,
      NEW_CODE
       )SELECT 
       PERSONAL_INFO_ID,
       COVERAGE_CODE,                                         
      DEDUCTIBLE_TYPE,                                
      SUM_INSURED,                                   
      PREMIUM,                                        
      MINIMUM_DEDUCTIBLE ,
      H.NEW_CODE
 
   FROM POL_PERSONAL_ACCIDENT_INFO PPAF WITH(NOLOCK)
                  LEFT OUTER JOIN HOSPITAL_05_SEM_IOF H WITH(NOLOCK) ON PPAF.CODE=H.NEW_CODE
                  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
   
    SELECT @COUNT=COUNT(*) FROM #TEMP_POL_PERSONAL_ACCIDENT_INFO WITH(NOLOCK)
 
    
    WHILE(@COUNT>0)
    BEGIN
    SELECT @PERSONAL_INFO_ID = PERSONAL_INFO_ID FROM #TEMP_POL_PERSONAL_ACCIDENT_INFO WITH(NOLOCK)  WHERE ID=@COUNTER
    SELECT  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)   FROM POL_PRODUCT_COVERAGES WITH(NOLOCK)         
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND  POLICY_VERSION_ID = @POLICY_VERSION_ID  AND RISK_ID = @PERSONAL_INFO_ID  
    
     INSERT INTO POL_PRODUCT_COVERAGES                                        
    (                                        
      CUSTOMER_ID,                                        
      POLICY_ID,                                        
      POLICY_VERSION_ID,                                        
      RISK_ID,                                        
      COVERAGE_ID,                                        
      COVERAGE_CODE_ID,    
      DEDUCTIBLE_1_TYPE,                            
      LIMIT_1,                                  
      WRITTEN_PREMIUM ,                                       
      MINIMUM_DEDUCTIBLE,           
      IS_ACTIVE  ,
      CREATED_BY,
	  CREATED_DATETIME,
	  MODIFIED_BY,
	  LAST_UPDATED_DATETIME      
    )                                        
 SELECT                                      
                                           
      @CUSTOMER_ID,                                        
      @POLICY_ID,                                        
      @POLICY_VERSION_ID,                                        
      @PERSONAL_INFO_ID AS RISK_ID,                                         
      (ROW_NUMBER()OVER(ORDER BY NEW_CODE ASC)+ @COVERAGE_ID_MAX) AS  COVERAGE_ID_MAX ,                                       
      COVERAGE_CODE,                                             
      DEDUCTIBLE_TYPE,                                
      SUM_INSURED,                                   
      PREMIUM,                                        
      MINIMUM_DEDUCTIBLE ,      
      'Y' AS IS_ACTIVE   ,
      248 AS CREATED_BY,
      GETDATE() AS CREATED_DATETIME,
      248 AS MODIFIED_BY,
      GETDATE() AS LAST_UPDATED_DATETIME                      
   FROM  #TEMP_POL_PERSONAL_ACCIDENT_INFO WITH(NOLOCK) WHERE ID=@COUNTER
    
    SET @COUNT=@COUNT-1
    SET @COUNTER=@COUNTER+1
    
   
    END
                  
  --SELECT * FROM POL_PRODUCT_COVERAGES WITH(NOLOCK) WHERE CUSTOMER_ID= @CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID
 
 
   --ROLLBACK TRAN
  
