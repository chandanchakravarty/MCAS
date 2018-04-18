
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_REINSURENCE_DATA]    Script Date: 12/02/2011 17:54:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_POLICY_REINSURENCE_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_REINSURENCE_DATA]
GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_REINSURENCE_DATA]    Script Date: 12/02/2011 17:54:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================            
-- Author:  <ATUL KUMAR SINGH>            
-- Create date: <2011-09-19>            
-- Description: <validate file data [file 'RE-INSURENCE']>            
-- =============================================            
--  UPDATE HISTORY            
-- =============================================            
-- Author:              
-- update date:             
-- Description:             
-- =============================================            
-- drop proc [PROC_MIG_IL_VALIDATE_POLICY_REINSURENCE_DATA]       302        
CREATE PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_REINSURENCE_DATA]             
            
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
            
            
              
            
            
DECLARE @LOOP_IMPORT_SERIAL_NO INT                
DECLARE @COUNTER INT  =1              
DECLARE @MAX_RECORD_COUNT INT               
DECLARE @ERROR_NO INT=0              
DECLARE @IMPORT_SERIAL_NO INT             
                  
DECLARE @REIN_COMPANY_ID VARCHAR(10)            
DECLARE @CONTRACT_FACULTATIVE INT             
DECLARE @CONTRACT_NO VARCHAR(50)        
DECLARE @REINSURENCE_TYPE INT        
DECLARE @LAYER INT        
            
          
            
BEGIN TRY            
-------------------------------- CREATE TEMP TABLE FOR THOSE RECORDS NEEDS TO BE PROCESSED            
-----------------------------------------------------------------------------------------            
              
     CREATE TABLE #TEMP_POLICY              
     (              
    ID INT IDENTITY(1,1),              
      IMPORT_SERIAL_NO BIGINT,              
                
     )              
            
            
            
  INSERT INTO #TEMP_POLICY              
     (              
      IMPORT_SERIAL_NO            
     )              
              
      SELECT               
   IMPORT_SERIAL_NO           
                         
      FROM MIG_IL_POLICY_REINSURANCE_DETAILS WITH(NOLOCK)              
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0              
                 
------------------------------------------------------------------------------------------            
            
            
  ------------------------------------                    
   -- GET MAX RECOUNT COUNT              
   ------------------------------------                 
    SELECT @MAX_RECORD_COUNT = COUNT(ID)               
    FROM   #TEMP_POLICY               
     
            
  WHILE(@COUNTER<=@MAX_RECORD_COUNT)              
  BEGIN            
    SET @ERROR_NO=0              
            
      SELECT             
       @IMPORT_SERIAL_NO   = IMPORT_SERIAL_NO             
      FROM   #TEMP_POLICY (NOLOCK) WHERE ID   = @COUNTER                 
            
            
  SELECT              
    @REIN_COMPANY_ID         = REIN_COMPANY_ID,            
    @CONTRACT_FACULTATIVE    = CONTRACT_FACULTATIVE,        
    @CONTRACT_NO             = [CONTRACT]   ,        
    @REINSURENCE_TYPE        = RI_TYPE,        
    @LAYER                   = LAYER        
            
 FROM  MIG_IL_POLICY_REINSURANCE_DETAILS WITH (NOLOCK)            
 WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID             
 AND   IMPORT_SERIAL_NO = @IMPORT_SERIAL_NO             
         
         
     ------------------------------------  VALIDATION OF REINSURENCE COMPANY CODE          
    
     IF NOT EXISTS( SELECT 1 FROM MNT_REIN_COMAPANY_LIST (NOLOCK) WHERE REIN_COMAPANY_ID=CAST(@REIN_COMPANY_ID AS INT) AND IS_ACTIVE='Y')            
     begin    
     
      SET @ERROR_NO=93      --WRONG REINSURENCE COMPANY CODE            
        
    end    
     ELSE            
     BEGIN            
                
     ------------------------------------  VALIDATION OF CONTRACT FACULTATIVE            
    IF (@CONTRACT_FACULTATIVE not in (14627,14628))   -- 14627 Contract, 14628 Facultative            
    SET @ERROR_NO=94  -- WRONG CONTRACT FACULTATIVE    ------------------------------------  VALIDATION OF CONTRACT NUMBER        
     ------------------------------------  VALIDATION OF CONTRACT NUMBER        
    ELSE IF NOT EXISTS (SELECT 1 FROM MNT_REINSURANCE_CONTRACT (NOLOCK) WHERE CONTRACT_NUMBER=@CONTRACT_NO)        
       SET @ERROR_NO=99     -- INVALID REINSURENCE TYPE        
    ELSE IF ( @REINSURENCE_TYPE NOT IN (40,41,43)    )           
    SET @ERROR_NO=100     -- INVALID REINSURENCE TYPE        
    --ELSE IF NOT EXISTS( SELECT 1 FROM MNT_REIN_LOSSLAYER (NOLOCK) WHERE LAYER=@LAYER )           
    --SET @ERROR_NO=101     -- INVALID LAYER        
                 
     END             
            
        
            
                 
            
      IF(@ERROR_NO>0)              
      BEGIN              
                     
      UPDATE MIG_IL_POLICY_REINSURANCE_DETAILS              
      SET    HAS_ERRORS=1                    
      WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND               
       IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO               
            
            
        EXEC  [PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS]                               
        @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,                    
        @IMPORT_SERIAL_NO      = @IMPORT_SERIAL_NO  ,                        
        @ERROR_SOURCE_FILE     = ''    ,                   
        @ERROR_SOURCE_COLUMN   = ''     ,                    
        @ERROR_SOURCE_COLUMN_VALUE= '' ,                    
        @ERROR_ROW_NUMBER      = @IMPORT_SERIAL_NO   ,                      
        @ERROR_TYPES           = @ERROR_NO,                 
        @ACTUAL_RECORD_DATA    = '' ,                    
        @ERROR_MODE            = 'VE',  -- VALIDATION ERROR                    
        @ERROR_SOURCE_TYPE     = 'REIS'                 
    END            
                
            
    SET @COUNTER+=1              
  END            
            
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