/*----------------------------------------------------------                                                                    
Proc Name             : Dbo.PROC_MIG_IL_CHECK_POLICY_UPLOADED_DATA
Created by            : ATUL KUMAR SINGH                                           
Date                  : 02-Sept-2011                                                           
Purpose               : TO CHECK UPLOADED DATA        
Revison History       :                                                                    
Used In               : INITIAL LOAD                       
------------------------------------------------------------                                                                    
Date     Review By          Comments                                       
                              
drop Proc [PROC_MIG_IL_CHECK_POLICY_UPLOADED_DATA]    37,'CAPP'                                                    
------   ------------       -------------------------*/                                                                    
--                                       
                                        
--                     
CREATE PROCEDURE [dbo].[PROC_MIG_IL_CHECK_POLICY_UPLOADED_DATA] 
@IMPORT_REQUEST_ID    INT  ,  
@FILE_MODE     CHAR(4)  
AS
BEGIN
--=======================================================================    
 --   FILE MODE MEANS FOR WHICH FILE TYPE THIS PROCESS BEING EXECUTED                        
 --     * POL  - POLICY FILE  
 --     * LOC   - LOCATION FILE                               
 --     * COAP  - COAPPLICANT FILE  
 --     * REMU  - REMUNERATION FILE  
 --=======================================================================   
            
 SET NOCOUNT ON;      
        
 	  ------------------------------------------------------------------  
   -- IF NO RECORD FOUND IT MEANS FILE IS NOT PROCESSED BY PACKAGE   
   -- CREATE A DEFAULT RECORD WITH ERROR  
   -- ERROR TYPE :34 (File could not be processed becuase uploaded file has incorrect data.)  
   ------------------------------------------------------------------  
   IF (@FILE_MODE='POL')  
    BEGIN  
        
         -------------------------------------------------------------  
         -- IF RECORD IS EXISTS THEN COPY ERROR RECORDS TO ERROR TABLE  
         -------------------------------------------------------------  
     IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)  
        BEGIN  
            
           INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]          
            (      
     [IMPORT_REQUEST_ID]                   
     ,[IMPORT_SERIAL_NO]     
     ,[ERROR_TYPES]         
     ,ERROR_SOURCE_COLUMN  
     ,ERROR_SOURCE_COLUMN_VALUE  
     ,ERROR_ROW_NUMBER                 
     ,[ERROR_DATETIME]    
     ,[ERROR_MODE]        
     ,ERROR_SOURCE_TYPE         
            )                   
            (        
              SELECT                            
      @IMPORT_REQUEST_ID               
     ,IMPORT_SERIAL_NO    
     ,ERROR_TYPES              
     ,ERROR_COLUMNS  
     ,ERRORCOLUMNVALUES      
     ,IMPORT_SERIAL_NO  
     ,GETDATE()  
     ,'SE'      -- SSIS ERROOR     
     ,'POL'       
     FROM  MIG_IL_POLICY_DETAILS WITH(NOLOCK)  
     WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND     
           HAS_ERRORS=1  
            )         
            
      
     END    
    ELSE  
     BEGIN  
       ----------------------------------------  
   -- INSERT DEFAULT RECORD  
   ----------------------------------------  
      INSERT INTO MIG_IL_POLICY_DETAILS  
      (  
       IMPORT_REQUEST_ID,  
       IMPORT_SERIAL_NO,  
       HAS_ERRORS,  
       CUSTOMER_CODE,  
       POLICY_NUMBER  
      )  
      VALUES  
      (  
       @IMPORT_REQUEST_ID,  
       1,  
       1,  
       '0',  
       '0'  
      )  
          
	 ----------------------------------------  
   -- INSERT ERROR  
   ----------------------------------------        
   EXEC PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                   
      @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,      
      @IMPORT_SERIAL_NO      = 1  ,          
      @ERROR_SOURCE_FILE     = ''     ,      
      @ERROR_SOURCE_COLUMN   = ''     ,      
      @ERROR_SOURCE_COLUMN_VALUE= '' ,      
      @ERROR_ROW_NUMBER      = 1   ,        
      @ERROR_TYPES           = 34,          
      @ACTUAL_RECORD_DATA    = ''  ,      
      @ERROR_MODE            = 'VE',  -- VALIDATION ERROR       
      @ERROR_SOURCE_TYPE     = 'POL'      
     END  
     END  
     
     
     
     
     
    ELSE IF (@FILE_MODE='LOC')        
    BEGIN   
 --===================================================  
 -- FOR POLICY LOCATION
 --===================================================         
  IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_LOCATION_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)  
        BEGIN  
            
   INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]          
            (      
     [IMPORT_REQUEST_ID]                   
     ,[IMPORT_SERIAL_NO]     
     ,[ERROR_TYPES]         
     ,ERROR_SOURCE_COLUMN  
     ,ERROR_SOURCE_COLUMN_VALUE  
     ,ERROR_ROW_NUMBER                 
     ,[ERROR_DATETIME]    
     ,[ERROR_MODE]        
     ,ERROR_SOURCE_TYPE         
            )                   
            (        
              SELECT                            
      @IMPORT_REQUEST_ID               
     ,IMPORT_SERIAL_NO    
     ,ERROR_TYPES              
     ,ERROR_COLUMNS  
     ,ERRORCOLUMNVALUES      
     ,IMPORT_SERIAL_NO  
     ,GETDATE()  
     ,'SE'      -- SSIS ERROOR     
     ,'POL'       
     FROM  MIG_IL_POLICY_LOCATION_DETAILS WITH(NOLOCK)  
     WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND     
           HAS_ERRORS=1  
            )         
            
      
     END    
     ELSE  
     BEGIN  
       ----------------------------------------  
   -- INSERT DEFAULT RECORD  
   ----------------------------------------  
      INSERT INTO MIG_IL_POLICY_LOCATION_DETAILS  
      (  
       IMPORT_REQUEST_ID,  
       IMPORT_SERIAL_NO,  
       HAS_ERRORS,  
       CUSTOMER_CODE,  
        LOCATION_CODE 
      )  
      VALUES  
      (  
       @IMPORT_REQUEST_ID,  
       1,  
       1,  
       '0',  
       '0'  
      )  
           
   ----------------------------------------  
   -- INSERT ERROR  
   ----------------------------------------        
   EXEC  PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                   
      @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,      
      @IMPORT_SERIAL_NO      = 1  ,          
      @ERROR_SOURCE_FILE     = ''     ,      
      @ERROR_SOURCE_COLUMN   = ''     ,      
      @ERROR_SOURCE_COLUMN_VALUE= '' ,      
      @ERROR_ROW_NUMBER      = 1   ,        
      @ERROR_TYPES           = 34,          
      @ACTUAL_RECORD_DATA    = ''  ,      
      @ERROR_MODE            = 'VE',  -- VALIDATION ERROR       
      @ERROR_SOURCE_TYPE     = 'LOC'      
     END  
       
    END    
END
GO
