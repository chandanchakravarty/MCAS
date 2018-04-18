/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_LOCATION_DETAILS]    Script Date: 12/02/2011 16:07:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_POLICY_LOCATION_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_LOCATION_DETAILS]
GO


/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_LOCATION_DETAILS]    Script Date: 12/02/2011 16:07:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                            
Proc Name             : Dbo.[PROC_MIG_IL_INSERT_POLICY_LOCATION_DETAILS]                                                        
Created by            : Santosh Kumar Gautam                                                           
Date                  : 22 Sept 2011                                                          
Purpose               : Insert Policy Location
Modified by           : Pradeep Kushwaha
Date                  : 10 Nov 2011         
Revison History       :                                                            
Used In               : INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc [PROC_MIG_IL_INSERT_POLICY_LOCATION_DETAILS]   1                                               
------   ------------       -------------------------*/    
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_LOCATION_DETAILS]   
  
--------------------------------- INPUT PARAMETER  
@IMPORT_REQUEST_ID  INT  
-------------------------------------------------  
    
AS  
BEGIN  
   
-------------------------------- DECLARATION PART  
----------------------------------------------------------------------------------------  
DECLARE @ERROR_NUMBER    INT    
DECLARE @ERROR_SEVERITY  INT    
DECLARE @ERROR_STATE     INT    
DECLARE @ERROR_PROCEDURE VARCHAR(512)    
DECLARE @ERROR_LINE    INT    
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)    
DECLARE @LOOP_LOCATION_SEQUENCE_NO INT   
  
DECLARE @CUSTOMER_CODE VARCHAR(20)  
DECLARE @CUSTOMER_ID INT  
DECLARE @POLICY_ID INT  
DECLARE @POLICY_VERSION_ID INT  
DECLARE @POLICY_NUMBER NVARCHAR(21)  
DECLARE @ENDORSEMENT_NUMBER INT  
  
  
DECLARE @CREATED_BY INT=3 -- ADMINSTRATOR  

  
  
DECLARE @LOOP_LOCATION BIGINT        
DECLARE @LOOP_POLICY_SEQUANCE_NO INT      
DECLARE @LOOP_END_SEQUANCE_NO INT      
DECLARE @LOOP_IMPORT_SERIAL_NO INT      
DECLARE @COUNTER INT  =1    
DECLARE @MAX_RECORD_COUNT INT     
DECLARE @ERROR_NO INT=0    
DECLARE @IMPORT_SERIAL_NO INT
DECLARE @LOCATION_ID   INT

DECLARE @LOB_ID   INT

DECLARE @IMPORT_FILE_NAME NVARCHAR(50)   
DECLARE @IMPORT_POLICY_FILE_TYPE INT = 14939 -- FOR POLICY FILE TYPE
DECLARE @IMPORT_LOCATION_FILE_TYPE INT = 14960 -- FOR LOCATION FILE TYPE
DECLARE @IS_BILLING_ADDRESS NVARCHAR(5)
DECLARE @PROCESS_TYPE INT    --- Change    
BEGIN TRY  
  
    
     CREATE TABLE #TEMP_POLICY    
     (    
	   ID INT IDENTITY(1,1),    
	   POLICY_SEQUANCE_NO INT,    
	   END_SEQUANCE_NO INT,   
	   IMPORT_SERIAL_NO BIGINT,    
	   LOCATION_SEQUESNCE_NO BIGINT,
	   LOCATION BIGINT NULL, 
	   IS_BILLING_ADDRESS  NVARCHAR(20)
     )    
  
  
  
  INSERT INTO #TEMP_POLICY    
     (    
		 POLICY_SEQUANCE_NO ,    
		 END_SEQUANCE_NO ,   
         IMPORT_SERIAL_NO,    
		 LOCATION_SEQUESNCE_NO,
         LOCATION ,
         IS_BILLING_ADDRESS   
     )    
     (    
      SELECT POLICY_SEQUENCE_NO,  
			 END_SEQUENCE_NO,  
			 IMPORT_SERIAL_NO,  
			 LOCATION_SEQUENCE_NO,
			 LOCATION_CODE,
			 IS_BILLING_ADDRESS                
      FROM MIG_IL_POLICY_LOCATION_DETAILS WITH(NOLOCK)    
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0    
     )    

  
   ------------------------------------          
   -- GET MAX RECOUNT COUNT    
   ------------------------------------       
    SELECT @MAX_RECORD_COUNT = COUNT(ID)     
    FROM   #TEMP_POLICY     
  
   ------------------------------------      
   -- GET FILE NAME
   ------------------------------------   
   IF(@MAX_RECORD_COUNT>0)
   BEGIN
   
    SELECT @IMPORT_FILE_NAME = SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9) 
    FROM  MIG_IL_IMPORT_REQUEST_FILES
    WHERE IMPORT_REQUEST_ID  = @IMPORT_REQUEST_ID AND
          IMPORT_FILE_TYPE   = @IMPORT_LOCATION_FILE_TYPE  
   
  END
   
  WHILE(@COUNTER<=@MAX_RECORD_COUNT)    
  BEGIN  
  
     SET @ERROR_NO=0    
     SET @LOCATION_ID=0
     SET @CUSTOMER_ID=0
     SET @POLICY_ID=0
     SET @POLICY_VERSION_ID=0
     SET @IS_BILLING_ADDRESS=NULL
     SET @PROCESS_TYPE=0
      SELECT 
       @IMPORT_SERIAL_NO   = IMPORT_SERIAL_NO ,    
       @LOOP_POLICY_SEQUANCE_NO = POLICY_SEQUANCE_NO  ,  
       @LOOP_END_SEQUANCE_NO = END_SEQUANCE_NO,    
       @LOOP_LOCATION_SEQUENCE_NO = LOCATION_SEQUESNCE_NO,    
	   @LOOP_LOCATION		= LOCATION,
	   @IS_BILLING_ADDRESS  =IS_BILLING_ADDRESS
     FROM   #TEMP_POLICY (NOLOCK) WHERE ID   = @COUNTER       
  
   -------------------------------------------------------      
   -- GET CUSTOMER ID, POLICY ID AND POLICY VERSION ID
   ------------------------------------------------------- 
   SELECT @CUSTOMER_ID		 = CUSTOMER_ID ,
          @POLICY_ID		 = POLICY_ID,
          @POLICY_VERSION_ID = POLICY_VERSION_ID,
          @LOB_ID            = LOB_ID,
          @PROCESS_TYPE		 = PROCESS_TYPE
   FROM   MIG_IL_IMPORT_SUMMARY
   WHERE  POLICY_SEQUENTIAL       = @LOOP_POLICY_SEQUANCE_NO AND
		  ENDORSEMENT_SEQUENTIAL  = @LOOP_END_SEQUANCE_NO    AND
          FILE_TYPE           = @IMPORT_POLICY_FILE_TYPE AND
          [FILE_NAME]         = @IMPORT_FILE_NAME        AND
          IS_ACTIVE			  = 'Y'
          
 
   -------------------------------------------------------      
   -- CHECK WHETHER APPLICATION/POLICY EXISTS OR NOT
   ------------------------------------------------------- 
    IF(@CUSTOMER_ID IS NULL OR @CUSTOMER_ID='' OR @CUSTOMER_ID=0)
        SET @ERROR_NO =53 -- Application/Policy does not exists
   -----------------------------------------------------------      
   -- CHECK WHETHER LOCATION IS ALREADY EXISTS FOR CUSTOMER
   ----------------------------------------------------------- 
     ELSE IF EXISTS(SELECT * FROM POL_LOCATIONS WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID AND LOC_NUM=@LOOP_LOCATION)
        SET @ERROR_NO =54 --  Location number is already exists for given customer 
	/*CHECKING THE DUPLICAY FOR IS BILLING TO YES*/        
     ELSE IF(EXISTS(SELECT LOC_NUM FROM POL_LOCATIONS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND ISNULL(IS_BILLING,'N')='Y') AND @IS_BILLING_ADDRESS ='Y' )            
		SET @ERROR_NO=132
      

    IF(@ERROR_NO>0)
       BEGIN
       
       
        -----------------------------------------------------------      
		-- INSERT ERROR DETAILS
		----------------------------------------------------------- 
		   UPDATE MIG_IL_POLICY_LOCATION_DETAILS
		   SET    HAS_ERRORS=1      
		   WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND 
		          IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO      
	     
		  EXEC  [PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS]                 
				 @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,      
				 @IMPORT_SERIAL_NO      = @IMPORT_SERIAL_NO  ,          
				 @ERROR_SOURCE_FILE     = ''     ,      
				 @ERROR_SOURCE_COLUMN   = ''     ,      
				 @ERROR_SOURCE_COLUMN_VALUE= '' ,      
				 @ERROR_ROW_NUMBER      = @IMPORT_SERIAL_NO   ,        
				 @ERROR_TYPES           = @ERROR_NO,   
				 @ACTUAL_RECORD_DATA    = '' ,      
				 @ERROR_MODE            = 'VE',  -- VALIDATION ERROR      
				 @ERROR_SOURCE_TYPE     = 'LOCA'   
				 

       END 
       ELSE
       BEGIN
  
  
  IF(@PROCESS_TYPE=1)
  BEGIN
		-----------------------------------------------------------      
		-- GET LOCATION ID
		----------------------------------------------------------- 
  
        SELECT @LOCATION_ID= ISNULL(MAX(LOCATION_ID),0) +1
        FROM [POL_LOCATIONS] 
        WHERE CUSTOMER_ID		= @CUSTOMER_ID AND 
              POLICY_ID		    = @POLICY_ID   AND
              POLICY_VERSION_ID = @POLICY_VERSION_ID 
              
		INSERT INTO [dbo].[POL_LOCATIONS]
			   ([CUSTOMER_ID]
			  ,[POLICY_ID]
			   ,[POLICY_VERSION_ID]
			   ,[LOCATION_ID]
			   ,[LOC_NUM]
			   ,[IS_PRIMARY]
			   ,[LOC_ADD1]
			   ,[LOC_ADD2]
			   ,[LOC_CITY]
			   ,[LOC_COUNTY]
			   ,[LOC_STATE]
			   ,[LOC_ZIP]
			   ,[LOC_COUNTRY]
			   ,[PHONE_NUMBER]
			   ,[FAX_NUMBER]
			   ,[DEDUCTIBLE]
			   ,[NAMED_PERILL]
			   ,[DESCRIPTION]
			   ,[IS_ACTIVE]
			   ,[CREATED_BY]
			   ,[CREATED_DATETIME]			  
			   ,[LOC_TERRITORY]
			   ,[LOCATION_TYPE]
			   ,[RENTED_WEEKLY]
			   ,[WEEKS_RENTED]
			   ,[LOSSREPORT_ORDER]
			   ,[LOSSREPORT_DATETIME]
			   ,[REPORT_STATUS]
			   ,[CAL_NUM]
			   ,[NAME]
			   ,[NUMBER]
			   ,[DISTRICT]
			   ,[OCCUPIED]
			   ,[EXT]
			   ,[CATEGORY]
			   ,[ACTIVITY_TYPE]
			   ,[CONSTRUCTION]
			   ,[SOURCE_LOCATION_ID]
			   ,[IS_BILLING]
			   ,[CO_RISK_ID])		 
			   (
			   SELECT 
			    @CUSTOMER_ID
			   ,@POLICY_ID
			   ,@POLICY_VERSION_ID
			   ,@LOCATION_ID
			   ,LOCATION_CODE
			   ,NULL      		 --<IS_PRIMARY, nchar(1),>
			   ,[ADDRESS] 		 ---<LOC_ADD1, nvarchar(75),>
			   ,COMPLIMENT      		 --<LOC_ADD2, nvarchar(75),>
			   ,CITY
			   ,NULL			 ---<LOC_COUNTY, nvarchar(75),>
			   ,[STATE]
			   ,[ZIP CODE]
			   ,COUNTRY
			   ,dbo.fun_FormatPhoneText(PHONE)
			   ,dbo.fun_FormatPhoneText(FAX)
			   ,NULL    		-- [DEDUCTIBLE]
			   ,NULL    		--<NAMED_PERILL, int,>
			   ,REMARKS 		--<DESCRIPTION, nvarchar(1000),>
			   ,CASE WHEN IS_DEACTIVATE ='Y' THEN 'N' ELSE 'Y' END -- 'Y'
			   ,dbo.fun_GetDefaultUserID() --<CREATED_BY, int,>
			   ,GETDATE()		-- <CREATED_DATETIME, datetime,>			  
			   ,NULL     		--- <LOC_TERRITORY, nvarchar(5),>
			   ,0		 		---<LOCATION_TYPE, int,>
			   ,NULL     		---<RENTED_WEEKLY, nvarchar(10),>
			   ,NULL     		--<WEEKS_RENTED, nvarchar(10),>
			   ,NULL     		---<LOSSREPORT_ORDER, int,>
			   ,NULL     		---<LOSSREPORT_DATETIME, datetime,>
			   ,NULL     		---<REPORT_STATUS, char(1),>
			   ,NULL     		---<CAL_NUM, nvarchar(20),>
			   ,BUILDING_NAME
			   ,NUMBER
			   ,DISTRICT
			   ,OCCUPIED_AS
			   ,EXT
			   ,RUBRICA
			   ,ACTIVITY_TYPES
			   ,CONSTRUCTION
			   ,NULL			 ---<SOURCE_LOCATION_ID, int,>
			   ,IS_BILLING_ADDRESS 
			   ,NULL			 ---<CO_RISK_ID, int,>
			   FROM MIG_IL_POLICY_LOCATION_DETAILS
			   WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
			         IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
			   
			   )

 END
 --========================================================
 -- FOR POLICY ENDORSEMENT PROCESS
 --========================================================
 ELSE IF(@PROCESS_TYPE=3)
 BEGIN
 
   -------------------------------------------------------      
   -- GET LOCATION DETAILS
   ------------------------------------------------------- 
   SELECT @LOCATION_ID		      = IMPORTED_RECORD_ID 
   FROM   MIG_IL_IMPORT_SUMMARY
   WHERE  POLICY_SEQUENTIAL       = @LOOP_POLICY_SEQUANCE_NO   AND
		  ENDORSEMENT_SEQUENTIAL  = @LOOP_END_SEQUANCE_NO      AND
          FILE_TYPE               = @IMPORT_LOCATION_FILE_TYPE AND
          [FILE_NAME]             = @IMPORT_FILE_NAME          AND
          IS_ACTIVE			      = 'Y'
          
    -- IF LOCATION IS EXISTS THEN UPDATE EXISTING LOCATION DETAILS
   IF(@LOCATION_ID>0)
   BEGIN
   
        			 
		 UPDATE MIG_IL_POLICY_LOCATION_DETAILS
		 SET    CUSTOMER_ID		  = @CUSTOMER_ID,
		        POLICY_ID         = @POLICY_ID,
		        POLICY_VERSION_ID = @POLICY_VERSION_ID,
		        IS_PROCESSED      = 'Y'
		 WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
		        IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
   
		-- UPDATE EXISTING DETAILS
       UPDATE [POL_LOCATIONS]               
       SET	    [LOC_NUM]           =   CASE WHEN T.LOCATION_CODE IS NULL OR T.LOCATION_CODE='' THEN [LOC_NUM] ELSE T.LOCATION_CODE END		
			   ,[LOC_ADD1]			=   CASE WHEN T.[ADDRESS] IS NULL OR  T.[ADDRESS]='' THEN [LOC_ADD1] ELSE  T.[ADDRESS] END				  
			   ,[LOC_CITY]          =   CASE WHEN T.CITY IS NULL OR  T.CITY='' THEN [LOC_CITY] ELSE  T.CITY END 			 
			   ,[LOC_STATE]		    =   CASE WHEN T.STATE IS NULL OR T.STATE='' THEN [LOC_STATE] ELSE T.STATE END 			 
			   ,[LOC_ZIP]			=   CASE WHEN T.[ZIP CODE] IS NULL OR T.[ZIP CODE]='' OR T.[ZIP CODE]='0' THEN [LOC_ZIP] ELSE T.[ZIP CODE] END 			 
			   ,[LOC_COUNTRY]       =   CASE WHEN T.COUNTRY IS NULL OR T.COUNTRY=0 THEN [LOC_COUNTRY] ELSE T.COUNTRY END 			 
			   ,[PHONE_NUMBER]      =   CASE WHEN T.PHONE IS NULL OR T.PHONE='' OR T.PHONE='0' THEN [PHONE_NUMBER] ELSE T.PHONE END 			 
			   ,[FAX_NUMBER]		=	CASE WHEN T.FAX	IS NULL OR T.FAX='' OR T.FAX='0' THEN [FAX_NUMBER] ELSE T.FAX END
			   ,[DESCRIPTION]       =   CASE WHEN T.REMARKS IS NULL OR T.REMARKS='' THEN [DESCRIPTION] ELSE T.REMARKS END 			  
			   ,[IS_ACTIVE]         =   CASE WHEN T.IS_DEACTIVATE ='Y' THEN 'N' ELSE 'Y' END
			   ,[NAME]              =   CASE WHEN T.BUILDING_NAME IS NULL OR T.BUILDING_NAME='' THEN [NAME] ELSE T.BUILDING_NAME END  
			   ,[NUMBER]            =   CASE WHEN T.NUMBER IS NULL OR T.NUMBER='' THEN LOC.[NUMBER] ELSE T.NUMBER END  
			   ,[DISTRICT]			=   CASE WHEN T.DISTRICT IS NULL OR T.DISTRICT='' THEN LOC.[DISTRICT] ELSE T.DISTRICT END  
			   ,[OCCUPIED]			=   CASE WHEN T.OCCUPIED_AS IS NULL OR T.OCCUPIED_AS=0 THEN [OCCUPIED] ELSE T.OCCUPIED_AS END 
			   ,[EXT]               =   CASE WHEN T.EXT IS NULL OR T.EXT='' THEN LOC.[EXT] ELSE T.EXT END 
			   ,[CATEGORY]			=   CASE WHEN T.RUBRICA IS NULL OR T.RUBRICA='' THEN [CATEGORY] ELSE T.RUBRICA END
			   ,[ACTIVITY_TYPE]		=   CASE WHEN T.ACTIVITY_TYPES IS NULL OR T.ACTIVITY_TYPES=0 THEN [ACTIVITY_TYPE] ELSE T.ACTIVITY_TYPES END
			   ,[CONSTRUCTION]		=   CASE WHEN T.CONSTRUCTION IS NULL OR T.CONSTRUCTION=0 THEN LOC.[CONSTRUCTION] ELSE T.CONSTRUCTION END 			  
			   ,[IS_BILLING]        =   CASE WHEN T.IS_BILLING_ADDRESS IS NULL OR T.IS_BILLING_ADDRESS='' THEN [IS_BILLING] ELSE T.IS_BILLING_ADDRESS END  			  
	     FROM [POL_LOCATIONS] LOC INNER JOIN
	     (
	        SELECT  CUSTOMER_ID
	               ,POLICY_ID
	               ,POLICY_VERSION_ID
	               ,LOCATION_CODE
				   ,[ADDRESS] 		 
				   ,COMPLIMENT      		
				   ,CITY			   
				   ,[STATE]
				   ,[ZIP CODE]
				   ,COUNTRY
				   ,DBO.FUN_FORMATPHONETEXT(PHONE) AS PHONE
				   ,DBO.FUN_FORMATPHONETEXT(FAX)	AS FAX		   
				   ,REMARKS 
				   ,IS_DEACTIVATE			
				   ,BUILDING_NAME
				   ,NUMBER
				   ,DISTRICT
				   ,OCCUPIED_AS
				   ,EXT
				   ,RUBRICA
				   ,ACTIVITY_TYPES
				   ,CONSTRUCTION			
				   ,IS_BILLING_ADDRESS 
				   
		    FROM   MIG_IL_POLICY_LOCATION_DETAILS 
		    WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND 
		           IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
			   
	     )T ON T.CUSTOMER_ID= LOC.CUSTOMER_ID AND T.POLICY_ID=LOC.POLICY_ID AND T.POLICY_VERSION_ID=LOC.POLICY_VERSION_ID
	     
	     WHERE LOC.CUSTOMER_ID =@CUSTOMER_ID AND LOC.POLICY_ID=@POLICY_ID AND LOC.POLICY_VERSION_ID=@POLICY_VERSION_ID AND LOC.LOCATION_ID=@LOCATION_ID
   
   END
	-----------------------------------------------------------------------------      
    -- IF LOCATION IS NOT EXISTS THEN ADD NEW LOCATION IN POLICY ENDORSEMENT
    -----------------------------------------------------------------------------      
   ELSE   
   BEGIN
		-----------------------------------------------------------      
		-- GET LOCATION ID
		----------------------------------------------------------- 
  
        SELECT @LOCATION_ID= ISNULL(MAX(LOCATION_ID),0) +1
        FROM [POL_LOCATIONS] 
        WHERE CUSTOMER_ID		= @CUSTOMER_ID AND 
              POLICY_ID		    = @POLICY_ID   AND
              POLICY_VERSION_ID = @POLICY_VERSION_ID 
              
		INSERT INTO [dbo].[POL_LOCATIONS]
			   ([CUSTOMER_ID]
			   ,[POLICY_ID]
			   ,[POLICY_VERSION_ID]
			   ,[LOCATION_ID]
			   ,[LOC_NUM]
			   ,[IS_PRIMARY]
			   ,[LOC_ADD1]
			   ,[LOC_ADD2]
			  ,[LOC_CITY]
			   ,[LOC_COUNTY]
			   ,[LOC_STATE]
			   ,[LOC_ZIP]
			   ,[LOC_COUNTRY]
			   ,[PHONE_NUMBER]
			   ,[FAX_NUMBER]
			   ,[DEDUCTIBLE]
			   ,[NAMED_PERILL]
			   ,[DESCRIPTION]
			   ,[IS_ACTIVE]
			   ,[CREATED_BY]
			   ,[CREATED_DATETIME]			  
			   ,[LOC_TERRITORY]
			   ,[LOCATION_TYPE]
			   ,[RENTED_WEEKLY]
			   ,[WEEKS_RENTED]
			   ,[LOSSREPORT_ORDER]
			   ,[LOSSREPORT_DATETIME]
			   ,[REPORT_STATUS]
			   ,[CAL_NUM]
			   ,[NAME]
			   ,[NUMBER]
			   ,[DISTRICT]
			   ,[OCCUPIED]
			   ,[EXT]
			   ,[CATEGORY]
			   ,[ACTIVITY_TYPE]
			   ,[CONSTRUCTION]
			   ,[SOURCE_LOCATION_ID]
			   ,[IS_BILLING]
			   ,[CO_RISK_ID])		 
			   (
			   SELECT 
			    @CUSTOMER_ID
			   ,@POLICY_ID
			   ,@POLICY_VERSION_ID
			   ,@LOCATION_ID
			   ,LOCATION_CODE
			   ,NULL      		 --<IS_PRIMARY, nchar(1),>
			   ,[ADDRESS] 		 ---<LOC_ADD1, nvarchar(75),>
			   ,COMPLIMENT      		 --<LOC_ADD2, nvarchar(75),>
			   ,CITY
			   ,NULL			 ---<LOC_COUNTY, nvarchar(75),>
			   ,[STATE]
			   ,[ZIP CODE]
			   ,COUNTRY
			   ,dbo.fun_FormatPhoneText(PHONE)
			   ,dbo.fun_FormatPhoneText(FAX)
			   ,NULL    		-- [DEDUCTIBLE]
			   ,NULL    		--<NAMED_PERILL, int,>
			   ,REMARKS 		--<DESCRIPTION, nvarchar(1000),>
			   ,CASE WHEN IS_DEACTIVATE ='Y' THEN 'N' ELSE 'Y' END -- 'Y'
			   ,dbo.fun_GetDefaultUserID() --<CREATED_BY, int,>
			   ,GETDATE()		-- <CREATED_DATETIME, datetime,>			  
			   ,NULL     		--- <LOC_TERRITORY, nvarchar(5),>
			   ,0		 		---<LOCATION_TYPE, int,>
			   ,NULL     		---<RENTED_WEEKLY, nvarchar(10),>
			   ,NULL     		--<WEEKS_RENTED, nvarchar(10),>
			   ,NULL     		---<LOSSREPORT_ORDER, int,>
			   ,NULL     		---<LOSSREPORT_DATETIME, datetime,>
			   ,NULL     		---<REPORT_STATUS, char(1),>
			   ,NULL     		---<CAL_NUM, nvarchar(20),>
			   ,BUILDING_NAME
			   ,NUMBER
			   ,DISTRICT
			   ,OCCUPIED_AS
			   ,EXT
			   ,RUBRICA
			   ,ACTIVITY_TYPES
			   ,CONSTRUCTION
			   ,NULL			 ---<SOURCE_LOCATION_ID, int,>
			   ,IS_BILLING_ADDRESS 
			   ,NULL			 ---<CO_RISK_ID, int,>
			   FROM MIG_IL_POLICY_LOCATION_DETAILS
			   WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
			         IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
			   
			   )
   END
 
 END
		
		
		------------------------------------      
	    -- UPDATE IMPORT DETAILS
	    ------------------------------------  		 			 
		 UPDATE MIG_IL_POLICY_LOCATION_DETAILS
		 SET    CUSTOMER_ID		  = @CUSTOMER_ID,
		        POLICY_ID         = @POLICY_ID,
		        POLICY_VERSION_ID = @POLICY_VERSION_ID,
		        IS_PROCESSED      = 'Y'
		 WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
		        IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
		        
		 ------------------------------------          
    -- INSERT IMPORT SUMMARY  
    ------------------------------------         
       EXEC [PROC_MIG_IL_INSERT_IMPORT_SUMMARY]       
		@IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID,  
		@IMPORT_SERIAL_NO    = @IMPORT_SERIAL_NO,   
		@CUSTOMER_ID         = @CUSTOMER_ID ,  
		@POLICY_ID           = @POLICY_ID,  
		@POLICY_VERSION_ID   = @POLICY_VERSION_ID,  
		@IS_ACTIVE           = 'Y',  
		@IS_PROCESSED        = 'Y',  
		@FILE_TYPE           = @IMPORT_LOCATION_FILE_TYPE,  
		@FILE_NAME              = @IMPORT_FILE_NAME,  
		@CUSTOMER_SEQUENTIAL    = NULL,  
		@POLICY_SEQUENTIAL      = @LOOP_POLICY_SEQUANCE_NO,   
		@ENDORSEMENT_SEQUENTIAL = @LOOP_END_SEQUANCE_NO,  
		@IMPORT_SEQUENTIAL      = @LOOP_LOCATION_SEQUENCE_NO,  
		@IMPORT_SEQUENTIAL2     = NULL,  
		@LOB_ID			        = @LOB_ID ,   
		@IMPORTED_RECORD_ID     = @LOCATION_ID,
		@PROCESS_TYPE			= @PROCESS_TYPE    

    END
    SET @COUNTER+=1 
       
  END  -- END OF WHILE LOOP
  
END TRY  
BEGIN CATCH    
     
 SELECT     
    @ERROR_NUMBER    = ERROR_NUMBER(),    
    @ERROR_SEVERITY  = ERROR_SEVERITY(),    
    @ERROR_STATE     = ERROR_STATE(),    
    @ERROR_PROCEDURE = ERROR_PROCEDURE(),    
    @ERROR_LINE   = ERROR_LINE(),    
  @ERROR_MESSAGE   = ERROR_MESSAGE()    
         
  -- CREATING LOG OF EXCEPTION     
  EXEC [PROC_MIG_INSERT_ERROR_LOG]      
  @IMPORT_REQUEST_ID    = @IMPORT_REQUEST_ID    
 ,@IMPORT_SERIAL_NO  = 0    
 ,@ERROR_NUMBER      = @ERROR_NUMBER    
 ,@ERROR_SEVERITY    = @ERROR_SEVERITY    
 ,@ERROR_STATE          = @ERROR_STATE    
 ,@ERROR_PROCEDURE   = @ERROR_PROCEDURE    
 ,@ERROR_LINE        = @ERROR_LINE    
 ,@ERROR_MESSAGE        = @ERROR_MESSAGE    
 ,@INITIAL_LOAD_FLAG    = 'Y'    
      
     
 END CATCH    
        
  
  
   
END 


GO
