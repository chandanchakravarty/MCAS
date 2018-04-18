IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_POL_DISTINCT_DRIVER_FOR_UMBRELLA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GET_POL_DISTINCT_DRIVER_FOR_UMBRELLA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- DROP PROC  dbo.PROC_GET_POL_DISTINCT_DRIVER_FOR_UMBRELLA                        
-- sp_find 'PROC_GET_POL_DISTINCT_DRIVER_FOR_UMBRELLA',p                  
-- PROC_GET_POL_DISTINCT_DRIVER_FOR_UMBRELLA 936, 279,1                  
CREATE PROC dbo.PROC_GET_POL_DISTINCT_DRIVER_FOR_UMBRELLA                        
 @CUSTOMER_ID INT,              
 @POLICY_ID int,              
 @POLICY_VERSION_ID SMALLINT              
AS                        
BEGIN                        
 -- IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME = 'bbb')                        
   --BEGIN                        
--    DROP TABLE  TEMPCOPYDRIVERSU                        
  -- END   
 declare @MOTOR_LOB varchar(5)
 set @MOTOR_LOB = '3'                     
                          
  SELECT                         
  DRIVER_FNAME, DRIVER_MNAME, DRIVER_LNAME , DRIVER_CODE ,                         
  DRIVER_ADD1 , DRIVER_ADD2 , DRIVER_CITY , DRIVER_STATE , DRIVER_ZIP ,                        
  DRIVER_COUNTRY , DRIVER_HOME_PHONE , DRIVER_BUSINESS_PHONE,                        
  DRIVER_EXT , DRIVER_MOBILE , DRIVER_DOB , DRIVER_SSN , DRIVER_SEX,
  case APP_LIST.APP_LOB 
	WHEN @MOTOR_LOB then ''
	else DRIVER_DRIV_TYPE
  end DRIVER_DRIV_TYPE,
  DRIVER_LIC_STATE,FORM_F95,DATE_LICENSED,DRIVER_DRIV_LIC,DRIVER_MART_STAT  
  INTO #TEMPCOPYDRIVERSU                        
  FROM APP_DRIVER_DETAILS                        
  INNER JOIN APP_LIST ON APP_DRIVER_DETAILS.CUSTOMER_ID = APP_LIST.CUSTOMER_ID                        
  AND APP_DRIVER_DETAILS.APP_ID = APP_LIST.APP_ID                         
  AND APP_DRIVER_DETAILS.APP_VERSION_ID = APP_LIST.APP_VERSION_ID                         
  INNER JOIN POL_UMBRELLA_UNDERLYING_POLICIES ON APP_LIST.CUSTOMER_ID = POL_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID                        
  AND APP_LIST.APP_NUMBER =                       
  LTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,0,                    
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))                          
  AND APP_LIST.APP_VERSION_ID=                      
  SUBSTRING(LTRIM(RTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                    
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@POLICY_VERSION_ID,                      
  LEN(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))),0,                    
  CHARINDEX('.',LTRIM(RTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                    
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@POLICY_VERSION_ID,                    
  LEN(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER))))))                          
  WHERE POL_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID = @CUSTOMER_ID                     
  AND POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_ID = @POLICY_ID                         
  AND POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_VERSION_ID = @POLICY_VERSION_ID                         
  AND UPPER(POLICY_COMPANY)='WOLVERINE' AND ISNULL(IS_POLICY,0)=0                        
                    
  UNION                        
                          
  SELECT                         
  DRIVER_FNAME, DRIVER_MNAME, DRIVER_LNAME , DRIVER_CODE ,                         
  DRIVER_ADD1 , DRIVER_ADD2 , DRIVER_CITY , DRIVER_STATE , DRIVER_ZIP ,                        
  DRIVER_COUNTRY , DRIVER_HOME_PHONE , DRIVER_BUSINESS_PHONE,                        
  DRIVER_EXT , DRIVER_MOBILE , DRIVER_DOB , DRIVER_SSN , DRIVER_SEX,
  case POL_CUSTOMER_POLICY_LIST.POLICY_LOB 
	WHEN @MOTOR_LOB then ''
	else DRIVER_DRIV_TYPE
   end DRIVER_DRIV_TYPE,
  DRIVER_LIC_STATE,FORM_F95,DATE_LICENSED,DRIVER_DRIV_LIC ,DRIVER_MART_STAT   
  FROM POL_DRIVER_DETAILS                        
  INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_DRIVER_DETAILS.CUSTOMER_ID = POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID                        
  AND POL_DRIVER_DETAILS.POLICY_ID = POL_CUSTOMER_POLICY_LIST.POLICY_ID                         
  AND POL_DRIVER_DETAILS.POLICY_VERSION_ID = POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID                         
  INNER JOIN POL_UMBRELLA_UNDERLYING_POLICIES ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID                     
  = POL_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID                        
  AND POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER =                     
  LTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,0,                    
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))                          
  AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=                    
  SUBSTRING(LTRIM(RTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                    
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@POLICY_VERSION_ID,                  
  LEN(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))),0,                    
  CHARINDEX('.',LTRIM(RTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                    
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@POLICY_VERSION_ID,                    
  LEN(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER))))))                          
  WHERE POL_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID = @CUSTOMER_ID AND                     
  POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_ID = @POLICY_ID             AND POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_VERSION_ID = @POLICY_VERSION_ID                         
  AND UPPER(POLICY_COMPANY)='WOLVERINE' AND ISNULL(IS_POLICY,@POLICY_VERSION_ID)=@POLICY_VERSION_ID                        
                          
                          
                          
  UNION                        
                     
                          
  SELECT                         
  DRIVER_FNAME, DRIVER_MNAME, DRIVER_LNAME , DRIVER_CODE ,                         
  DRIVER_ADD1 , DRIVER_ADD2 , DRIVER_CITY , DRIVER_STATE , DRIVER_ZIP ,                        
  DRIVER_COUNTRY , '' DRIVER_HOME_PHONE , '' DRIVER_BUSINESS_PHONE,                        
  '' DRIVER_EXT , '' DRIVER_MOBILE , DRIVER_DOB , DRIVER_SSN , DRIVER_SEX,'' DRIVER_DRIV_TYPE, DRIVER_LIC_STATE,'' FORM_F95,'' DATE_LICENSED,'' DRIVER_DRIV_LIC ,'' DRIVER_MART_STAT   
  FROM APP_WATERCRAFT_DRIVER_DETAILS                        
  INNER JOIN APP_LIST ON APP_WATERCRAFT_DRIVER_DETAILS.CUSTOMER_ID = APP_LIST.CUSTOMER_ID                        
  AND APP_WATERCRAFT_DRIVER_DETAILS.APP_ID = APP_LIST.APP_ID                         
  AND APP_WATERCRAFT_DRIVER_DETAILS.APP_VERSION_ID = APP_LIST.APP_VERSION_ID                         
  INNER JOIN POL_UMBRELLA_UNDERLYING_POLICIES ON APP_LIST.CUSTOMER_ID =                     
  POL_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID                        
  AND APP_LIST.APP_NUMBER = LTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,0,                    
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))                          
  AND APP_LIST.APP_VERSION_ID=                    
  SUBSTRING(LTRIM(RTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                    
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@POLICY_VERSION_ID,                    
  LEN(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))),0,                    
  CHARINDEX('.',LTRIM(RTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                    
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@POLICY_VERSION_ID,                    
  LEN(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER))))))                          
  WHERE POL_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID = @CUSTOMER_ID AND                     
  POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_ID = @POLICY_ID                         
  AND POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_VERSION_ID = @POLICY_VERSION_ID                         
  AND UPPER(POLICY_COMPANY)='WOLVERINE' AND ISNULL(IS_POLICY,0)=0                        
                          
                          
  UNION                        
                          
  SELECT                         
  DRIVER_FNAME, DRIVER_MNAME, DRIVER_LNAME , DRIVER_CODE ,                         
  DRIVER_ADD1 , DRIVER_ADD2 , DRIVER_CITY , DRIVER_STATE , DRIVER_ZIP ,                        
  DRIVER_COUNTRY , '' DRIVER_HOME_PHONE , '' DRIVER_BUSINESS_PHONE,                        
  '' DRIVER_EXT , '' DRIVER_MOBILE , DRIVER_DOB , DRIVER_SSN , DRIVER_SEX,'' DRIVER_DRIV_TYPE, DRIVER_LIC_STATE,'' FORM_F95,'' DATE_LICENSED,'' DRIVER_DRIV_LIC ,'' DRIVER_MART_STAT    
  FROM POL_WATERCRAFT_DRIVER_DETAILS                        
  INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_WATERCRAFT_DRIVER_DETAILS.CUSTOMER_ID =                    
  POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID                        
  AND POL_WATERCRAFT_DRIVER_DETAILS.POLICY_ID = POL_CUSTOMER_POLICY_LIST.POLICY_ID                         
  AND POL_WATERCRAFT_DRIVER_DETAILS.POLICY_VERSION_ID = POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID                         
  INNER JOIN POL_UMBRELLA_UNDERLYING_POLICIES ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID =                     
   POL_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID                        
  AND POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER =                     
  LTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,0,           
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))                          
  AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=                    
  SUBSTRING(LTRIM(RTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                    
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@POLICY_VERSION_ID,                    
  LEN(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))),0,                    
  CHARINDEX('.',LTRIM(RTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                    
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@POLICY_VERSION_ID,                    
  LEN(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER))))))                          
  WHERE POL_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID = @CUSTOMER_ID                     
  AND POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_ID = @POLICY_ID                         
  AND POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_VERSION_ID = @POLICY_VERSION_ID                         
  AND UPPER(POLICY_COMPANY)='WOLVERINE' AND ISNULL(IS_POLICY,@POLICY_VERSION_ID)=@POLICY_VERSION_ID                        
                          
                          
  SELECT                        
  DRIVER_FNAME,                        
  CONVERT(VARCHAR(10),DRIVER_DOB,101) DRIVER_DOB,                
  MAX(DRIVER_MNAME) AS DRIVER_MNAME,                
  MAX(DRIVER_LNAME) AS DRIVER_LNAME,                
  MAX(DRIVER_FNAME + ' ' + DRIVER_MNAME + ' ' + DRIVER_LNAME ) AS DRIVER_NAME,                          
  MAX(DRIVER_ADD1 + ' ' + DRIVER_ADD2  ) AS DRIVER_ADDRESS,                          
  MAX(DRIVER_CODE) AS DRIVER_CODE,                         
  MAX(DRIVER_ADD1) AS DRIVER_ADD1,                        
  MAX( DRIVER_ADD2) as DRIVER_ADD2 ,                         
  MAX(DRIVER_CITY) as DRIVER_CITY,                        
  MAX( DRIVER_STATE) 'DRIVER_STATE' ,                         
  MAX(DRIVER_ZIP) 'DRIVER_ZIP' ,                        
  MAX(DRIVER_COUNTRY ) 'DRIVER_COUNTRY',                         
  MAX(DRIVER_HOME_PHONE) 'DRIVER_HOME_PHONE' ,                        
  MAX(DRIVER_BUSINESS_PHONE) 'DRIVER_BUSINESS_PHONE',                        
  MAX( DRIVER_EXT) 'DRIVER_EXT' ,                         
  MAX(DRIVER_MOBILE) 'DRIVER_MOBILE',                         
  MAX(DRIVER_SSN) 'DRIVER_SSN',                         
  MAX(DRIVER_SEX) 'DRIVER_SEX',      
  MAX(DRIVER_DRIV_TYPE) 'DRIVER_DRIV_TYPE',    
  MAX(DRIVER_LIC_STATE) 'DRIVER_LIC_STATE',    
  MAX(FORM_F95) 'FORM_F95',    
  MAX(DATE_LICENSED) 'DATE_LICENSED',    
  MAX(DRIVER_DRIV_LIC) 'DRIVER_DRIV_LIC',    
  MAX(DRIVER_MART_STAT) 'DRIVER_MART_STAT'    
  FROM #TEMPCOPYDRIVERSU                        
  GROUP BY DRIVER_FNAME, DRIVER_DOB                        
                  
--DROP TABLE  TEMPCOPYDRIVERSU                        
END                        
                    
                
              
            
          
        
      
    
  



GO

