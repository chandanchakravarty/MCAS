
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_RISK_DISCOUNT_DETAILS]    Script Date: 12/02/2011 16:17:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_POLICY_RISK_DISCOUNT_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_RISK_DISCOUNT_DETAILS]
GO




/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_RISK_DISCOUNT_DETAILS]    Script Date: 12/02/2011 16:17:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                
Proc Name             : Dbo.[[PROC_MIG_IL_INSERT_POLICY_RISK_DISCOUNT_DETAILS]]                                                            
Created by            : Puneet Kumar    
Date                  : 04 OCT 2011                                                              
Purpose               : Insert Risk Info    
Modified by           : Pradeep Kushwaha
Date                  : 10 Nov 2011   
Revison History       :                                                                
Used In               : INITIAL LOAD                   
------------------------------------------------------------                                                                
Date     Review By          Comments                                   
                          
drop Proc [PROC_MIG_IL_INSERT_POLICY_RISK_DISCOUNT_DETAILS]   671                                                  
------   ------------       -------------------------*/        
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_RISK_DISCOUNT_DETAILS]       
      
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
      
      
DECLARE @CUSTOMER_CODE VARCHAR(20)      
DECLARE @CUSTOMER_ID INT      
DECLARE @POLICY_ID INT      
DECLARE @POLICY_VERSION_ID INT      
DECLARE @POLICY_NUMBER NVARCHAR(21)      
DECLARE @ENDORSEMENT_NUMBER INT     
DECLARE @TYPE INT    
DECLARE @PERC DECIMAL(18,2)    
      
      
           
DECLARE @LOOP_POLICY_SEQUANCE_NO INT      
           
DECLARE @LOOP_DISCOUNT_SEQUANCE_NO INT        
DECLARE @LOOP_END_SEQUANCE_NO INT          
DECLARE @LOOP_RISK_SEQUANCE_NO INT        

DECLARE @LOOP_IMPORT_SERIAL_NO INT          
DECLARE @COUNTER INT  =1        
DECLARE @MAX_RECORD_COUNT INT         
DECLARE @ERROR_NO INT=0        
DECLARE @IMPORT_SERIAL_NO INT    
DECLARE @DISOUNT_ROW_ID   INT    
DECLARE @RISK_ID   INT =0 -- FOR POLICY DISCOUNT   
  
DECLARE @LOB_ID INT  
DECLARE @IMPORT_FILE_NAME NVARCHAR(50)       
DECLARE @IMPORT_POLICY_FILE_TYPE INT = 14939 -- FOR POLICY FILE TYPE    
DECLARE @IMPORT_RISK_DISCOUNT_FILE_TYPE INT = 14966 -- FOR POLICY DISCOUNT    
DECLARE @IMPORT_RISK_FILE_TYPE INT = 15008 -- FOR POLICY RISK FILE TYPE    
DECLARE @PROCESS_TYPE INT    --- Change   
DECLARE @IS_DEACTIVATE NCHAR(1)     
      
BEGIN TRY      
      
        
     CREATE TABLE #TEMP_POLICY        
     (        
		  ID INT IDENTITY(1,1) ,        
		  POLICY_SEQUENTIAL INT NULL,        
		  ENDORSEMENT_SEQUENTIAL INT NULL,       
		  DISCOUNT_SEQUENTIAL INT NULL,     
		  RISK_SEQUENTIAL INT NULL,     
		  IMPORT_SERIAL_NO BIGINT NULL,        
		  [TYPE] INT NULL,    
		  [PERCENT] DECIMAL(18,2)  NULL      ,
		  IS_DEACTIVATE NCHAR(1)     
     )        
      
      
      
  INSERT INTO #TEMP_POLICY        
     (        
		  POLICY_SEQUENTIAL ,        
		  ENDORSEMENT_SEQUENTIAL ,  
		  DISCOUNT_SEQUENTIAL     ,  
		  RISK_SEQUENTIAL,
		  IMPORT_SERIAL_NO,    
		  [TYPE],    
		  [PERCENT]       ,
		  IS_DEACTIVATE 
     )        
     (        
      SELECT  POLICY_SEQUENTIAL ,        
			  ENDORSEMENT_SEQUENTIAL ,   
			  DISCOUNT_SURCHARGE_RISK_SEQUENTIAL,   
			  RISK_LOCATION_SEQUENTIAL, 
			  IMPORT_SERIAL_NO,        
			  [TYPE],    
			  [PERCENT],
			  IS_DEACTIVATE             
	  FROM MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS WITH(NOLOCK)        
	  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0        
     )        
    
    --select * from #TEMP_POLICY
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
          IMPORT_FILE_TYPE   = @IMPORT_RISK_DISCOUNT_FILE_TYPE
       
   END    
    
  WHILE(@COUNTER<=@MAX_RECORD_COUNT)        
  BEGIN      
      
     SET @ERROR_NO=0        
     SET @DISOUNT_ROW_ID=0    
     SET @CUSTOMER_ID=0    
     SET @POLICY_ID=0    
     SET @POLICY_VERSION_ID=0    
     SET @PROCESS_TYPE=0    
     SET @IS_DEACTIVATE=''
     
      SELECT     
       @IMPORT_SERIAL_NO        = IMPORT_SERIAL_NO ,        
       @LOOP_POLICY_SEQUANCE_NO = POLICY_SEQUENTIAL  ,      
       @LOOP_END_SEQUANCE_NO    = ENDORSEMENT_SEQUENTIAL, 
       @LOOP_RISK_SEQUANCE_NO   = RISK_SEQUENTIAL,       
       @TYPE				    = [TYPE],    
       @PERC				    = [PERCENT]  ,  
     @LOOP_DISCOUNT_SEQUANCE_NO = DISCOUNT_SEQUENTIAL , 
     @IS_DEACTIVATE				= IS_DEACTIVATE
     FROM   #TEMP_POLICY (NOLOCK) WHERE ID   = @COUNTER           
      
        
   ----------------------------------------------------------------          
   -- GET RISK ID FROM PROCESSED RISK RECORDS
   ----------------------------------------------------------------     
   SELECT @CUSTOMER_ID		  = CUSTOMER_ID ,    
          @POLICY_ID		  = POLICY_ID,    
          @POLICY_VERSION_ID  = POLICY_VERSION_ID ,
          @LOB_ID             = LOB_ID ,
          @RISK_ID            = IMPORTED_RECORD_ID,
          @PROCESS_TYPE		  = PROCESS_TYPE	
   FROM   MIG_IL_IMPORT_SUMMARY    
   WHERE  POLICY_SEQUENTIAL			= @LOOP_POLICY_SEQUANCE_NO AND    
		  ENDORSEMENT_SEQUENTIAL    = @LOOP_END_SEQUANCE_NO    AND        
		  IMPORT_SEQUENTIAL         = @LOOP_RISK_SEQUANCE_NO   AND
          FILE_TYPE					= @IMPORT_RISK_FILE_TYPE   AND    
          [FILE_NAME]				= @IMPORT_FILE_NAME        AND    
          IS_ACTIVE					= 'Y'    
   
              
   -------------------------------------------------------          
   -- CHECK WHETHER APPLICATION/POLICY EXISTS OR NOT    
   -------------------------------------------------------     
    IF(NOT EXISTS( SELECT 1 FROM   MIG_IL_IMPORT_SUMMARY WHERE  
										 POLICY_SEQUENTIAL          = @LOOP_POLICY_SEQUANCE_NO AND    
										  ENDORSEMENT_SEQUENTIAL    = @LOOP_END_SEQUANCE_NO    AND    
										  FILE_TYPE          		 = @IMPORT_POLICY_FILE_TYPE AND    
										  [FILE_NAME]        		 = @IMPORT_FILE_NAME        AND    
										  IS_ACTIVE     = 'Y'    
				)  
	    )               
        SET @ERROR_NO =53 -- Application/Policy does not exists  
    ELSE IF(@RISK_ID IS NULL OR @RISK_ID=0)
       SET @ERROR_NO =20 
    ELSE IF NOT EXISTS (SELECT * FROM MNT_DISCOUNT_SURCHARGE WHERE DISCOUNT_ID=@TYPE AND [LEVEL]=14705 AND LOB_ID=@LOB_ID)
      SET @ERROR_NO =104 
    ELSE IF(@RISK_ID IS NULL OR @RISK_ID=0)
      SET @ERROR_NO =20
    ELSE IF EXISTS (SELECT * FROM POL_DISCOUNT_SURCHARGE nolock WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and RISK_ID=@RISK_ID and DISCOUNT_ID=@TYPE )
      SET @ERROR_NO =133 
      
       
       select @ERROR_NO
    IF(@ERROR_NO>0)    
       BEGIN    
           
           
        -----------------------------------------------------------          
  -- INSERT ERROR DETAILS    
  -----------------------------------------------------------     
     UPDATE MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS
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
     @ERROR_SOURCE_TYPE     = 'RDSC'       
         
    
       END     
       ELSE    
       BEGIN    
 --===========================================================
 -----FOR NBS ADDED BY PADEEP
 --==========================================================
      
 IF(@PROCESS_TYPE=1)
 BEGIN
   -----------------------------------------------------------          
   -- GET DISCOUNT ID    
   -----------------------------------------------------------     
       
        SELECT @DISOUNT_ROW_ID= ISNULL(MAX(DISCOUNT_ROW_ID),0) +1    
        FROM POL_DISCOUNT_SURCHARGE     
        WHERE CUSTOMER_ID       = @CUSTOMER_ID AND     
              POLICY_ID         = @POLICY_ID   --AND    
              --POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
              --RISK_ID           = @RISK_ID -- FOR POLICY DISCOUNT IT SHOULD BE ZERO    
                  
    INSERT INTO [dbo].POL_DISCOUNT_SURCHARGE    
     ([CUSTOMER_ID]    
     ,[POLICY_ID]    
     ,[POLICY_VERSION_ID]    
     ,RISK_ID    
     ,DISCOUNT_ROW_ID    
     ,DISCOUNT_ID    
     ,PERCENTAGE    
     ,IS_ACTIVE    
     ,CREATED_BY    
     ,CREATED_DATETIME    
     )    
     (    
     SELECT     
      @CUSTOMER_ID    
     ,@POLICY_ID    
     ,@POLICY_VERSION_ID    
     ,@RISK_ID    
     ,@DISOUNT_ROW_ID    
     ,@TYPE    
     ,@PERC    
     ,CASE WHEN @IS_DEACTIVATE ='Y' THEN 'N' ELSE 'Y' END 
     ,dbo.fun_GetDefaultUserID()    
     ,GETDATE()    
            
     )    
      
END
 --========================================================
 -- FOR POLICY ENDORSEMENT PROCESS ADDED BY PRADEEP
 --========================================================
ELSE IF(@PROCESS_TYPE =3)
BEGIN
	 ----------------------------------------------------------------          
   -- GET @DISOUNT_ROW_ID
   ----------------------------------------------------------------     
   SELECT @DISOUNT_ROW_ID			=  IMPORTED_RECORD_ID 
   FROM   MIG_IL_IMPORT_SUMMARY    
   WHERE  POLICY_SEQUENTIAL			= @LOOP_POLICY_SEQUANCE_NO			AND    
		  ENDORSEMENT_SEQUENTIAL    = @LOOP_END_SEQUANCE_NO				AND        
		  IMPORT_SEQUENTIAL         = @LOOP_RISK_SEQUANCE_NO			AND
          FILE_TYPE					= @IMPORT_RISK_DISCOUNT_FILE_TYPE   AND    
          [FILE_NAME]				= @IMPORT_FILE_NAME					AND    
          IS_ACTIVE					= 'Y'    
   -- IF RISK DISCOUNT IS EXISTS THEN UPDATE EXISTING RISK DISCOUNT DETAILS
   IF(@DISOUNT_ROW_ID>0)
   BEGIN
	   ------------------------------------          
	   -- UPDATE IMPORT DETAILS    
	   ------------------------------------             
		UPDATE MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS
		SET    CUSTOMER_ID    = @CUSTOMER_ID,    
		 POLICY_ID         = @POLICY_ID,    
		 POLICY_VERSION_ID = @POLICY_VERSION_ID,    
		 IS_PROCESSED      = 'Y'    
		WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND    
		 IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO    
		 
        -- UPDATE EXISTING DETAILS
       UPDATE POL_DISCOUNT_SURCHARGE               
       SET	    
					 DISCOUNT_ID    		=   CASE WHEN T.DISCOUNT_ID IS NULL OR T.DISCOUNT_ID=0 THEN PD.DISCOUNT_ID ELSE T.DISCOUNT_ID END 
					,PERCENTAGE     		=   CASE WHEN T.PERCENTAGE IS NULL OR T.PERCENTAGE=0 THEN PD.PERCENTAGE ELSE T.PERCENTAGE END  
					,IS_ACTIVE      		=   T.IS_DEACTIVATE
					,MODIFIED_BY			=   T.MODIFIED_BY
					,LAST_UPDATED_DATETIME	=	T.LAST_UPDATED_DATETIME
					
	     FROM POL_DISCOUNT_SURCHARGE PD INNER JOIN
	     (
	     
	        SELECT  CUSTOMER_ID
	               ,POLICY_ID
	               ,POLICY_VERSION_ID
	               ,[TYPE] AS DISCOUNT_ID    
				   ,[PERCENT]  AS PERCENTAGE   
				   ,CASE WHEN IS_DEACTIVATE ='Y' THEN 'N' ELSE 'Y' END   AS IS_DEACTIVATE  
				   ,dbo.fun_GetDefaultUserID()  AS MODIFIED_BY  
				   ,GETDATE() AS LAST_UPDATED_DATETIME
      
		    FROM   MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS 
		    WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND 
		           IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
			   
	     )T ON T.CUSTOMER_ID= PD.CUSTOMER_ID AND T.POLICY_ID=PD.POLICY_ID AND T.POLICY_VERSION_ID=PD.POLICY_VERSION_ID
	     
	     WHERE PD.CUSTOMER_ID =@CUSTOMER_ID AND PD.POLICY_ID=@POLICY_ID AND PD.POLICY_VERSION_ID=@POLICY_VERSION_ID AND PD.DISCOUNT_ROW_ID=@DISOUNT_ROW_ID   AND PD.RISK_ID=@RISK_ID
   END
   ELSE
   BEGIN
   
	   -----------------------------------------------------------          
	   -- GET DISCOUNT ID    
	   -----------------------------------------------------------     
	       
			SELECT @DISOUNT_ROW_ID= ISNULL(MAX(DISCOUNT_ROW_ID),0) +1    
			FROM POL_DISCOUNT_SURCHARGE     
			WHERE CUSTOMER_ID       = @CUSTOMER_ID AND     
				  POLICY_ID         = @POLICY_ID   
	                  
		INSERT INTO [dbo].POL_DISCOUNT_SURCHARGE    
		 ([CUSTOMER_ID]    
		 ,[POLICY_ID]    
		 ,[POLICY_VERSION_ID]    
		 ,RISK_ID    
		 ,DISCOUNT_ROW_ID    
		 ,DISCOUNT_ID    
		 ,PERCENTAGE    
		 ,IS_ACTIVE    
		 ,CREATED_BY    
		 ,CREATED_DATETIME    
		 )    
		 (    
		 SELECT     
		  @CUSTOMER_ID    
		 ,@POLICY_ID    
		 ,@POLICY_VERSION_ID    
		 ,@RISK_ID    
		 ,@DISOUNT_ROW_ID    
		 ,@TYPE    
		 ,@PERC    
		 ,CASE WHEN @IS_DEACTIVATE ='Y' THEN 'N' ELSE 'Y' END 
		 ,dbo.fun_GetDefaultUserID()    
		 ,GETDATE()    
	            
		 )  
   END
END
    
   ------------------------------------          
   -- UPDATE IMPORT DETAILS    
   ------------------------------------             
    UPDATE MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS
    SET    CUSTOMER_ID    = @CUSTOMER_ID,    
     POLICY_ID         = @POLICY_ID,    
     POLICY_VERSION_ID = @POLICY_VERSION_ID,    
     IS_PROCESSED      = 'Y'    
    WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND    
     IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO    
       
    ------------------------------------          
    -- INSERT IMPORT SUMMARY  
    ------------------------------------         
    EXEC [PROC_MIG_IL_INSERT_IMPORT_SUMMARY]       
    @IMPORT_REQUEST_ID      = @IMPORT_REQUEST_ID,  
    @IMPORT_SERIAL_NO       = @IMPORT_SERIAL_NO,   
    @CUSTOMER_ID            = @CUSTOMER_ID ,  
    @POLICY_ID              = @POLICY_ID,  
    @POLICY_VERSION_ID      = @POLICY_VERSION_ID,  
    @IS_ACTIVE              = 'Y',  
    @IS_PROCESSED           = 'Y',  
    @FILE_TYPE				= @IMPORT_RISK_DISCOUNT_FILE_TYPE,  
    @FILE_NAME              = @IMPORT_FILE_NAME,  
    @CUSTOMER_SEQUENTIAL    = NULL,  
    @POLICY_SEQUENTIAL      = @LOOP_POLICY_SEQUANCE_NO,   
    @ENDORSEMENT_SEQUENTIAL = @LOOP_END_SEQUANCE_NO,  
    @IMPORT_SEQUENTIAL      = @LOOP_RISK_SEQUANCE_NO,  
    @IMPORT_SEQUENTIAL2     = @LOOP_DISCOUNT_SEQUANCE_NO,  
    @LOB_ID					= @LOB_ID ,   
    @IMPORTED_RECORD_ID     = @DISOUNT_ROW_ID,            
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


