IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_VALIDATE_POLICY_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_VALIDATE_POLICY_DATA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



 /*----------------------------------------------------------                                                            
Proc Name             : Dbo.PROC_MIG_VALIDATE_POLICY_DATA                                                            
Created by            : Santosh Kumar Gautam                                                           
Date                  : 11 May 2011                                                          
Purpose               : TO VALIDATE POLICY/ENDORSEMENT DATA  
Revison History       :                                                            
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc PROC_MIG_VALIDATE_POLICY_DATA                                                   
------   ------------       -------------------------*/                                                            
--                               
                                
--                             
                          
CREATE PROCEDURE [dbo].[PROC_MIG_VALIDATE_POLICY_DATA]    
  
@IMPORT_REQUEST_ID    INT,                     
@IMPORT_SERIAL_NO     INT,  
@HAS_ERRORS     INT OUT  
  
                               
AS                                
BEGIN                         
    
  -- VARIABLES TO HOLD THE EXCEPTION GENERATED VALUES    
    
DECLARE @ERROR_NUMBER    INT    
DECLARE @ERROR_SEVERITY  INT    
DECLARE @ERROR_STATE     INT    
DECLARE @ERROR_PROCEDURE VARCHAR(512)    
DECLARE @ERROR_LINE    INT    
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)    
  
  
 BEGIN TRY      
         
      
 DECLARE @CHK_COUNTER        INT =0  
 DECLARE @CHK_POLICY_STATUS  NVARCHAR(20)=''  
 DECLARE @CHK_APP_STATUS     NVARCHAR(20)=''  
 DECLARE @CHK_APP_EFFECTIVE_DATE DATETIME  
 DECLARE @CHK_APP_EXPIRY_DATE    DATETIME  
 DECLARE @CHK_END_EFFECTIVE_DATE DATETIME  
 DECLARE @CHK_END_EXPIRY_DATE    DATETIME  
 DECLARE @CHK_ENDORSEMENT_NUMBER INT  
   
 DECLARE  @POLICY_LOB           INT    
 DECLARE  @POLICY_SUBLOB        INT  
 DECLARE  @SUSEP_LEADER_CODE    INT  
 DECLARE  @CPF_CNPJ             NVARCHAR(50)  
 DECLARE  @LEADER_POLICY_NUMBER       NVARCHAR(50)  
 DECLARE  @END_EFFECTIVE_DATE   DATETIME  
 DECLARE  @END_EXPIRY_DATE   DATETIME  
 DECLARE  @LEADER_ENDORSEMENT_NUMBER  INT  
 DECLARE  @POLICY_TRANSCTION_ID   NVARCHAR(50)    
   SET @HAS_ERRORS=0  
   
   --------------------------------------------------  
   -- INVALID LOB FOR ACCEPTED COINSURANE LOAD
   --------------------------------------------------  
   IF EXISTS( SELECT * 
                  FROM  MIG_CUSTOMER_POLICY_LIST P LEFT OUTER JOIN
                        MIG_POLICY_LOB_MAPPING  M ON P.POLICY_LOB=M.LEADER_SUSEP_LOB_CODE
                  WHERE P.IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND IMPORT_SERIAL_NO=@IMPORT_SERIAL_NO AND
                        M.ALBA_SUSEP_LOB_CODE NOT IN(111,114,115,116,118,167,171 ,173,196,351,433,435,621,622,654,993 )

                 )
     BEGIN
     
     SET @HAS_ERRORS=32  
     
      
             UPDATE MIG_CUSTOMER_POLICY_LIST   
             SET    HAS_ERRORS='Y'  
             WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND IMPORT_SERIAL_NO=@IMPORT_SERIAL_NO  
               
             EXEC  PROC_MIG_INSERT_IMPORT_ERROR_DETAILS               
				  @IMPORT_REQUEST_ID		 = @IMPORT_REQUEST_ID,  
				  @IMPORT_SERIAL_NO			 = @IMPORT_SERIAL_NO  ,      
				  @ERROR_SOURCE_FILE		 = ''     ,  
				  @ERROR_SOURCE_COLUMN		 = ''     ,  
				  @ERROR_SOURCE_COLUMN_VALUE = '' ,  
				  @ERROR_ROW_NUMBER			 = @IMPORT_SERIAL_NO   ,    
				  @ERROR_TYPES				 = @HAS_ERRORS,      
				  @ACTUAL_RECORD_DATA		 = ''  ,  
				  @ERROR_MODE				 = 'VE',  -- VALIDATION ERROR   
				  @ERROR_SOURCE_TYPE         = 'APP'    
				  
     RETURN
     
     END
                                   
      
   SELECT      
		  @POLICY_LOB          = POLICY_LOB ,            
		  @POLICY_SUBLOB       = POLICY_SUBLOB,          
		  @SUSEP_LEADER_CODE   = SUSEP_LEADER_CODE ,     
		  @CPF_CNPJ            = CPF_CNPJ  ,            
		  @LEADER_POLICY_NUMBER      = LEADER_POLICY_NUMBER  ,       
		  @LEADER_ENDORSEMENT_NUMBER = LEADER_ENDORSEMENT_NUMBER ,  
		  @END_EFFECTIVE_DATE        = ENDORSEMENT_EFFECTIVE_DATE,  
		  @END_EXPIRY_DATE           = ENDORSEMENT_EXPIRE_DATE  
   FROM   MIG_CUSTOMER_POLICY_LIST  
   WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND IMPORT_SERIAL_NO=@IMPORT_SERIAL_NO  
     
     
    --=============================================  
    -- FOR APPLICATION   
    --=============================================  
     IF(@LEADER_ENDORSEMENT_NUMBER<=0)  
       BEGIN  
           
           
         --------------------------------------------------  
         -- CHECK WHETHER APPLICATION ALREADY EXISTS  
         -- ERROR TYPE =6  
         --------------------------------------------------  
         IF EXISTS(SELECT LEADER_POLICY_NUMBER FROM POL_CO_INSURANCE WHERE LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER)    
         BEGIN                 
           SET @HAS_ERRORS=6    
         END  
             
        
        IF(@HAS_ERRORS=0)  
              BEGIN  
                
                  SELECT @CHK_COUNTER= COUNT(LEADER_SUSEP_LOB_CODE) FROM MIG_POLICY_LOB_MAPPING WHERE LEADER_SUSEP_LOB_CODE=@POLICY_LOB AND LEADER_SUSEP_SUB_LOB_CODE=@POLICY_SUBLOB  
                   
				 --------------------------------------------------  
				 -- CHECK DISCREPANCY IN LOB/SUB LOB MAPPING   
				 -- ERROR TYPE =7  
				 --------------------------------------------------  
                  IF(@CHK_COUNTER>1)  
                  BEGIN  
                      SET @HAS_ERRORS=7    
                  END  
                 --------------------------------------------------  
				 -- IF LOB/SUB LOB CODE DOES NOT EXISTS IN MAPPING TABLE  
				 -- ERROR TYPE =8  
				 --------------------------------------------------  
                  ELSE IF(@CHK_COUNTER=0)  -- LOB/SUB LOB DOEST NOT EXISTS ,  -- ERROR TYPE =8  
                      SET @HAS_ERRORS=8     
                      
                 --------------------------------------------------  
				 -- IF LOB/SUB LOB CODE DOES NOT MATCH FROM EBIX MASTER TABLE  
				 -- ERROR TYPE =9  
				 --------------------------------------------------  
                  ELSE IF NOT EXISTS(
                      SELECT L.LOB_ID 
                      FROM MNT_LOB_MASTER  AS L LEFT OUTER JOIN  
					       MNT_SUB_LOB_MASTER AS S  ON L.LOB_ID=S.LOB_ID    LEFT OUTER JOIN
					       MIG_POLICY_LOB_MAPPING M ON M.ALBA_SUSEP_LOB_CODE=L.SUSEP_LOB_CODE 
					 WHERE L.IS_ACTIVE='Y' AND M.LEADER_SUSEP_LOB_CODE=@POLICY_LOB AND CAST(ISNULL(M.LEADER_SUSEP_SUB_LOB_CODE,0) AS INT)=@POLICY_SUBLOB
					 )  
                  BEGIN  
                      SET @HAS_ERRORS=9                          
                  END  
              END  
                
      END  
      ELSE  
  --=============================================  
  -- FOR POLICY ENDORSEMENT  
  --=============================================  
    BEGIN  
            
          --------------------------------------------------  
          -- IF POLICY DOES NOT EXISTS  
          -- ERROR TYPE =10  
          --------------------------------------------------  
          IF NOT EXISTS(SELECT LEADER_POLICY_NUMBER FROM POL_CO_INSURANCE WHERE LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER)             
            SET @HAS_ERRORS=10   
          
          IF(@HAS_ERRORS=0)  
           BEGIN  
             
             DECLARE @MAX_VERSION_ID INT =0  
             DECLARE @LEADER_TRANSACTION_ID NVARCHAR(15)
             
             SELECT @LEADER_TRANSACTION_ID      = COINSURENCE_TRANSACTION_ID 
             FROM   MIG_CUSTOMER_POLICY_LIST 
             WHERE  IMPORT_REQUEST_ID			= @IMPORT_REQUEST_ID	AND 
                    LEADER_POLICY_NUMBER		= @LEADER_POLICY_NUMBER AND
                    LEADER_ENDORSEMENT_NUMBER 	= @LEADER_ENDORSEMENT_NUMBER AND
                    HAS_ERRORS					= 'N'
                  
                  
             DECLARE @CHK_PROCESS_ID     INT =0  
          
                
             SELECT @MAX_VERSION_ID= MAX( P.POLICY_VERSION_ID)  
             FROM  POL_CO_INSURANCE  C INNER JOIN  
                   POL_CUSTOMER_POLICY_LIST P ON C.CUSTOMER_ID=P.CUSTOMER_ID AND C.POLICY_ID=P.POLICY_ID AND C.POLICY_VERSION_ID=P.POLICY_VERSION_ID  
             WHERE LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER AND C.LEADER_FOLLOWER=14548 -- ( LEADER =14548, FOLLOWER=14549)  
          
          
                        
              SELECT TOP 1 
                    @CHK_APP_EFFECTIVE_DATE = P.APP_EFFECTIVE_DATE  
                   ,@CHK_APP_EXPIRY_DATE    = P.APP_EXPIRATION_DATE  
                   ,@CHK_APP_STATUS         = P.APP_STATUS  
                   ,@CHK_POLICY_STATUS      = P.POLICY_STATUS   
                   ,@CHK_END_EFFECTIVE_DATE = PP.EFFECTIVE_DATETIME  
                   ,@CHK_END_EXPIRY_DATE    = PP.[EXPIRY_DATE]  
                   ,@CHK_PROCESS_ID         = PP.PROCESS_ID  
                   ,@CHK_ENDORSEMENT_NUMBER = ISNULL(E.CO_ENDORSEMENT_NO,0)
                   ,@POLICY_TRANSCTION_ID   = C.TRANSACTION_ID
              FROM POL_CO_INSURANCE  C INNER JOIN  
                   POL_CUSTOMER_POLICY_LIST P ON C.CUSTOMER_ID=P.CUSTOMER_ID AND c.POLICY_ID=P.POLICY_ID AND C.POLICY_VERSION_ID=P.POLICY_VERSION_ID  
                   INNER JOIN POL_POLICY_PROCESS PP ON PP.CUSTOMER_ID=P.CUSTOMER_ID AND PP.POLICY_ID=P.POLICY_ID AND PP.NEW_POLICY_VERSION_ID=P.POLICY_VERSION_ID AND PP.PROCESS_STATUS<>'ROLLBACK'  
                   LEFT OUTER JOIN  
                   POL_POLICY_ENDORSEMENTS E ON E.CUSTOMER_ID=P.CUSTOMER_ID AND E.POLICY_ID=P.POLICY_ID AND E.POLICY_VERSION_ID=P.POLICY_VERSION_ID                    
              WHERE LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER AND C.POLICY_VERSION_ID=@MAX_VERSION_ID AND C.LEADER_FOLLOWER=14548 -- ( LEADER =14548, FOLLOWER=14549)  
              ORDER BY E.ENDORSEMENT_NO DESC
               
     --------------------------------------------------  
     -- IF POLICY EXISTS BUT NBS IS NOT COMMIT  
     -- ERROR TYPE =11  
     --------------------------------------------------  
     IF(@CHK_POLICY_STATUS='UISSUE' OR @CHK_POLICY_STATUS IS NULL OR @CHK_POLICY_STATUS ='')  
      BEGIN  
        SET @HAS_ERRORS=11  
      END   
     
        --------------------------------------------------  
     -- IF POLICY HAS EXPIRED OR SUSPENDED  
     -- ERROR TYPE =12  
     --------------------------------------------------  
        ELSE IF(@CHK_POLICY_STATUS='EXPIRED' OR @CHK_POLICY_STATUS='Suspended')               
        BEGIN  
          SET @HAS_ERRORS=12  
        END         
       
          
     --------------------------------------------------  
     -- IF ENDORSEMENT IS ALREADY RUNNING  
     -- ERROR TYPE =13  
     --------------------------------------------------  
        ELSE IF(@CHK_PROCESS_ID=14)         
        BEGIN  
          SET @HAS_ERRORS=44  
        END    
          
     --------------------------------------------------  
     -- IF ENDORSEMENT NUMBER AND TRANSACTION ID ALREADY EXISTS
     -- ERROR TYPE =39  
     --------------------------------------------------  
        ELSE IF(@CHK_ENDORSEMENT_NUMBER=@LEADER_ENDORSEMENT_NUMBER AND @POLICY_TRANSCTION_ID=@LEADER_TRANSACTION_ID)         
        BEGIN  
          SET @HAS_ERRORS=39  
        END    
     --------------------------------------------------  
     -- IF TRANSACTION ID IS ALREADY EXISTS IN POLICY OR ENDORSEMENT
     -- ERROR TYPE =41
     --------------------------------------------------  
        ELSE IF EXISTS(SELECT CUSTOMER_ID FROM POL_CO_INSURANCE WHERE LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER AND TRANSACTION_ID=@LEADER_TRANSACTION_ID)         
        BEGIN  
          SET @HAS_ERRORS=41 
        END            
     --------------------------------------------------  
     -- IF GIVEN ENDORSEMENT NUMBER IS LESS THEN ALREADY CREATED ENDORSEMENT NUMBER  
     -- ERROR TYPE =40  
     --------------------------------------------------  
        ELSE IF(@CHK_ENDORSEMENT_NUMBER>0 AND @CHK_ENDORSEMENT_NUMBER<@LEADER_ENDORSEMENT_NUMBER)          
        BEGIN  
          SET @HAS_ERRORS=40 
        END  
          
        --------------------------------------------------  
     -- IF GIVEN ENDORSEMENT NUMBER IS HIGHER THEN ALREADY CREATED ENDORSEMENT NUMBER  
     -- EXAMPLE WE ARE TRYING TO CREATE ENDORSEMENT 5 WHILE SYSTEM LAST ENDORSEMENT IS 2 SO 3 ,4 ENDORSEMENT IS MISSING  
     -- ERROR TYPE =15  
     --------------------------------------------------  
        ELSE IF(@CHK_ENDORSEMENT_NUMBER>0 AND (@LEADER_ENDORSEMENT_NUMBER-@CHK_ENDORSEMENT_NUMBER)>1)          
        BEGIN  
          SET @HAS_ERRORS=15  
        END  
          
          
          
           END  
           
    END  
        
     
                        
      IF(@HAS_ERRORS<>0)  
          BEGIN  
            
             UPDATE MIG_CUSTOMER_POLICY_LIST   
             SET    HAS_ERRORS='Y'  
             WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND IMPORT_SERIAL_NO=@IMPORT_SERIAL_NO  
               
             EXEC  PROC_MIG_INSERT_IMPORT_ERROR_DETAILS               
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,  
				  @IMPORT_SERIAL_NO      = @IMPORT_SERIAL_NO  ,      
				  @ERROR_SOURCE_FILE     = ''     ,  
				  @ERROR_SOURCE_COLUMN   = ''     ,  
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,  
				  @ERROR_ROW_NUMBER      = @IMPORT_SERIAL_NO ,    
				  @ERROR_TYPES           = @HAS_ERRORS,      
				  @ACTUAL_RECORD_DATA    = ''  ,  
				  @ERROR_MODE               = 'VE',  -- VALIDATION ERROR   
				  @ERROR_SOURCE_TYPE        = 'APP'    
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
      
     
         
     
 END CATCH           
END   
  









GO

