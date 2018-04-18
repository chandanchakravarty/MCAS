
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_CUSTOMER_DETAIL]    Script Date: 12/02/2011 17:08:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_CUSTOMER_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_CUSTOMER_DETAIL]
GO
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_CUSTOMER_DETAIL]    Script Date: 12/02/2011 17:08:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                            
Proc Name             : Dbo.[PROC_MIG_IL_VALIDATE_CUSTOMER_DETAIL]                                                        
Created by            : Santosh Kumar Gautam                                                           
Date                  : 15 Sept 2011                                                          
Purpose               : VALIDATE CUSTOMER
Revison History       :                                                            
Used In               : INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc [PROC_MIG_IL_VALIDATE_CUSTOMER_DETAIL]    1                                               
------   ------------       -------------------------*/                                                             
--                               
       
CREATE PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_CUSTOMER_DETAIL]   
      
 @IMPORT_REQUEST_ID INT      
   
AS                                
BEGIN                         
    
 SET NOCOUNT ON;    
 
 -- VARIABLES TO HOLD THE EXCEPTION GENERATED VALUES

DECLARE @ERROR_NUMBER    INT
DECLARE @ERROR_SEVERITY  INT
DECLARE @ERROR_STATE     INT
DECLARE @ERROR_PROCEDURE VARCHAR(512)
DECLARE @ERROR_LINE  	 INT
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)

 
 BEGIN TRY
    

    
     
     DECLARE @CUSTOMER_ID INT
     DECLARE @APPLICANT_ID INT
     DECLARE @IMPORT_SERIAL_NO INT   
     DECLARE @MAX_RECORD_COUNT INT     
     DECLARE @LOOP_CUST_CODE NVARCHAR(50) 
     DECLARE @LOOP_CUST_STATUS NVARCHAR(2) 
     DECLARE @LOOP_BROKER_CODE NVARCHAR(50) 
     DECLARE @LOOP_CUST_STATE INT
     DECLARE @LOOP_CUST_STATE1 INT
     DECLARE @LOOP_CUST_TYPE INT
   
      
     DECLARE @COUNTER INT  =1
     DECLARE @ERROR_NO   INT =0

	DECLARE @IMPORT_FILE_NAME NVARCHAR(50)           
	DECLARE @IMPORT_FILE_TYPE INT = 14936 -- FOR CUSTOMER FILE TYPE 
	DECLARE @LOOP_CUST_SEQUANCE_NO   INT  
	DECLARE @LOOP_IMPORT_SEQUANCE_NO INT                         
	                       
    

     
     
     CREATE TABLE #TEMP_CUST
     (
	   ID INT IDENTITY(1,1),
       IMPORT_SERIAL_NO BIGINT,
       CUST_CNPJ NVARCHAR(50) NULL,
       CUSTOMER_CODE NVARCHAR(50) NULL,
       CUST_STATUS NVARCHAR(2) NULL,
       BROKER_CODE NVARCHAR(50) NULL,
       CUST_STATE INT NULL,
       CUST_STATE1 INT NULL,
       CUST_TYPE INT NULL,
       CUST_SEQUANCE_NO INT NULL
     )
     
   ------------------------------------      
   -- INSERT RECORD IN TEMP TABLE
   ------------------------------------   
     INSERT INTO #TEMP_CUST
     (
      IMPORT_SERIAL_NO,
      CUST_CNPJ,
      CUSTOMER_CODE,
      CUST_STATUS,
      CUST_SEQUANCE_NO,
      BROKER_CODE,
      CUST_STATE,
      CUST_STATE1,
      CUST_TYPE
     )
     (
      SELECT IMPORT_SERIAL_NO,
             [CPF_CNPJ],
             CUSTOMER_CODE,
             [STATUS],
             CUSTOMER_SEQUENTIAL,
             [BROKER],
             [STATE],
             [STATE1],
             CUST_TYPE
      FROM MIG_IL_CUSTOMER_DETAILS WITH(NOLOCK)
      WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND HAS_ERRORS=0
     )

   ------------------------------------      
   -- GET MAX RECOUNT COUNT
   ------------------------------------   
    SELECT @MAX_RECORD_COUNT=COUNT(ID) 
    FROM   #TEMP_CUST WITH(NOLOCK)

    ------------------------------------              
   -- GET IMPORT FILE NAME        
   ------------------------------------           
    IF(@MAX_RECORD_COUNT>0)        
    BEGIN        
            
      SELECT  @IMPORT_FILE_NAME=SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9)        
      FROM MIG_IL_IMPORT_REQUEST_FILES WITH(NOLOCK)        
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND         
            IMPORT_FILE_TYPE  = @IMPORT_FILE_TYPE        
              
    END        
    
    WHILE(@COUNTER<=@MAX_RECORD_COUNT)
    BEGIN
       
       SET @ERROR_NO=0
      
       SELECT @LOOP_CUST_CODE   		= CUSTOMER_CODE,
              @IMPORT_SERIAL_NO 		= IMPORT_SERIAL_NO ,
              @LOOP_CUST_STATUS 		= CUST_STATUS,
              @LOOP_CUST_SEQUANCE_NO    = CUST_SEQUANCE_NO,
              @LOOP_BROKER_CODE			= BROKER_CODE,
              @LOOP_CUST_STATE			= CUST_STATE,
              @LOOP_CUST_STATE1			= CUST_STATE1,
              @LOOP_CUST_TYPE			= CUST_TYPE
       FROM #TEMP_CUST WITH(NOLOCK)
       WHERE ID=@COUNTER
       
       
        SELECT  CAST(AGENCY_CODE AS INT)AS AGENCY_CODE
				 INTO #TempAgencyList
				 FROM MNT_AGENCY_LIST 
				 WHERE AGENCY_CODE NOT IN (SELECT REIN_COMAPANY_CODE FROM MNT_REIN_COMAPANY_LIST WITH(NOLOCK)) AND
				       IS_ACTIVE='Y'


       -------------------------------------------      
	   -- CHECK THAT RECORD ALREADY PROCESSED  
	   -------------------------------------------      
       IF EXISTS (SELECT 1 FROM MIG_IL_IMPORT_SUMMARY WITH(NOLOCK) WHERE [FILE_NAME]=@IMPORT_FILE_NAME AND FILE_TYPE=@IMPORT_FILE_TYPE 
                        AND CUSTOMER_SEQUENTIAL= @LOOP_CUST_SEQUANCE_NO AND IS_ACTIVE='Y'
                     )          
      SET @ERROR_NO=66      -- THIS RECORD ALREADY PROCESSED
      
       ----------------------------------------      
	   -- INACTIVE CUSTOMER CANNOT BE IMPORTED -- 
	   ----------------------------------------  
       --ELSE IF(@LOOP_CUST_STATUS!='Y')
       --SET @ERROR_NO=56  --Commented as per the sheet given by ALBA on 24-Nov-2011
       
       --ELSE IF NOT EXISTS(SELECT 1 FROM MNT_AGENCY_LIST  WITH(NOLOCK) WHERE AGENCY_CODE=@LOOP_BROKER_CODE)
       --SET @ERROR_NO=89  
       
       ELSE IF NOT EXISTS(SELECT 1 FROM MNT_COUNTRY_STATE_LIST  WITH(NOLOCK) WHERE COUNTRY_ID=5 AND STATE_ID=@LOOP_CUST_STATE)
       SET @ERROR_NO=92  
       
       ELSE IF (@LOOP_CUST_STATE1 IS NOT NULL AND @LOOP_CUST_STATE1!=0) AND NOT EXISTS(SELECT 1 FROM MNT_COUNTRY_STATE_LIST  WITH(NOLOCK) WHERE COUNTRY_ID=5 AND STATE_ID=@LOOP_CUST_STATE)
       SET @ERROR_NO=92  
       ------------------------------------      
	   -- IF CUSTOMER ALREADY EXISTS	   
	   ------------------------------------  
       ELSE IF EXISTS(SELECT * FROM CLT_CUSTOMER_LIST  WITH(NOLOCK) WHERE CUSTOMER_CODE=@LOOP_CUST_CODE)
       SET @ERROR_NO=48  
       
       ELSE IF NOT EXISTS(SELECT 1 FROM #TempAgencyList WHERE AGENCY_CODE =LOOP_BROKER_CODE)
	   BEGIN
				   SET @ERROR_NO=89      -- WRONG BROKER CODE     
			
	   END   
       
       IF(@ERROR_NO>0)
       BEGIN
       
		   UPDATE MIG_IL_CUSTOMER_DETAILS
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
				 @ERROR_SOURCE_TYPE     = 'CUST'   

       END 
              
       
         SET @COUNTER+=1
    
    END -- END OF WHILE LOOP
  
 
   
		
	     
					   
	END TRY
	BEGIN CATCH
	
	SELECT 
			 @ERROR_NUMBER    = ERROR_NUMBER(),
			 @ERROR_SEVERITY  = ERROR_SEVERITY(),
			 @ERROR_STATE     = ERROR_STATE(),
			 @ERROR_PROCEDURE = ERROR_PROCEDURE(),
			 @ERROR_LINE	  = ERROR_LINE(),
			 @ERROR_MESSAGE   = ERROR_MESSAGE()
     
  -- CREATING LOG OF EXCEPTION 
  EXEC [PROC_MIG_INSERT_ERROR_LOG]  
  @IMPORT_REQUEST_ID    = @IMPORT_REQUEST_ID
 ,@IMPORT_SERIAL_NO		= 0
 ,@ERROR_NUMBER    		= @ERROR_NUMBER
 ,@ERROR_SEVERITY  		= @ERROR_SEVERITY
 ,@ERROR_STATE     	    = @ERROR_STATE
 ,@ERROR_PROCEDURE 		= @ERROR_PROCEDURE
 ,@ERROR_LINE  	   		= @ERROR_LINE
 ,@ERROR_MESSAGE        = @ERROR_MESSAGE
 ,@INITIAL_LOAD_FLAG    = 'Y'
  
	
     
	
	END CATCH
		  
  
  
            
END 
 
GO
