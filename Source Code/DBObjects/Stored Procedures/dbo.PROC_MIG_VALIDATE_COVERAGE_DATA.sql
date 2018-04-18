IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_VALIDATE_COVERAGE_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_VALIDATE_COVERAGE_DATA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



 /*----------------------------------------------------------                                                                
Proc Name             : Dbo.PROC_MIG_VALIDATE_COVERAGE_DATA                                                                
Created by            : Santosh Kumar Gautam                                                               
Date                  : 12 May 2011                                                              
Purpose               : TO VALIDATE COVERAGE POLICY DATA      
Revison History       :                                                                
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD                   
------------------------------------------------------------                                                                
Date     Review By          Comments                                   
                          
drop Proc PROC_MIG_VALIDATE_COVERAGE_DATA  180,'6151431',0,0                                                      
------   ------------       -------------------------*/                                                                
--                                   
                                    
--                                 
                              
CREATE PROCEDURE [dbo].[PROC_MIG_VALIDATE_COVERAGE_DATA]        
      
@IMPORT_REQUEST_ID    INT,        
--@IMPORT_SERIAL_NO     INT,                   
@LEADER_POLICY_NUMBER       NVARCHAR(20),      
@LEADER_ENDORSEMENT_NUMBER  INT,      
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
          
    CREATE TABLE #TEMP_COVERAGES      
    (      
      ID INT  IDENTITY,      
      IMPORT_SERIAL_NO INT,      
      COVERAGE_CODE NVARCHAR(5)      
    )      
          
 DECLARE  @CHK_COUNTER    INT =0     
 DECLARE  @CUSTOMER_ID            INT =0     
 DECLARE  @POLICY_ID              INT =0      
 DECLARE  @POLICY_VERSION_ID      INT =0     
 DECLARE  @LOB_ID          INT    
 DECLARE  @SUB_LOB_ID      INT    
 
 DECLARE  @END_EFFECTIVE_DATE   DATETIME  
 DECLARE  @END_EXPIRY_DATE   DATETIME  
 
 --DECLARE  @LEADER_TRANSCTION_ID   NVARCHAR(50) 

 DECLARE @CHK_POLICY_STATUS  NVARCHAR(20)=''  
 DECLARE @CHK_APP_STATUS     NVARCHAR(20)=''  
 DECLARE @CHK_APP_EFFECTIVE_DATE DATETIME  
 DECLARE @CHK_APP_EXPIRY_DATE    DATETIME  
 DECLARE @CHK_END_EFFECTIVE_DATE DATETIME  
 DECLARE @CHK_END_EXPIRY_DATE    DATETIME  
 DECLARE @CHK_ENDORSEMENT_NUMBER INT    
       
         
   SET @HAS_ERRORS=0      
          
          
         
    --=============================================      
    -- FOR APPLICATION       
    --=============================================      
      
   
	--------------------------------------------------      
	-- CHECK WHETHER APPLICATION ALREADY EXISTS      
	-- ERROR TYPE =10      
	--------------------------------------------------      
	IF NOT EXISTS (SELECT LEADER_POLICY_NUMBER FROM POL_CO_INSURANCE WHERE LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER)
	BEGIN
	 SET @HAS_ERRORS =10
	END
   ELSE IF(@LEADER_ENDORSEMENT_NUMBER<=0 )
    BEGIN      
   
		SELECT @CUSTOMER_ID    = COI.CUSTOMER_ID,   
		 @POLICY_ID			   = COI.POLICY_ID,   
		 @POLICY_VERSION_ID    = COI.POLICY_VERSION_ID,  
		 @LOB_ID			   = ISNULL(POLICY_LOB,0),  
		 @SUB_LOB_ID           = ISNULL(POLICY_SUBLOB,0)  
		FROM   POL_CO_INSURANCE AS COI INNER JOIN  
		 POL_CUSTOMER_POLICY_LIST AS POL ON COI.CUSTOMER_ID=POL.CUSTOMER_ID AND COI.POLICY_ID=POL.POLICY_ID AND COI.POLICY_VERSION_ID=POL.POLICY_VERSION_ID                     
		WHERE  COI.LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER   
    END  
   ELSE  
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
             --DECLARE @COINSURENCE_TRANSACTION_ID NVARCHAR(15)
             
             --SELECT @COINSURENCE_TRANSACTION_ID = COINSURENCE_TRANSACTION_ID 
             --FROM   MIG_POLICY_COVERAGES
             --WHERE  IMPORT_REQUEST_ID			= @IMPORT_REQUEST_ID	AND 
             --       LEADER_POLICY_NUMBER		= @LEADER_POLICY_NUMBER AND
             --       LEADER_ENDORSEMENT_NUMBER 	= @LEADER_ENDORSEMENT_NUMBER AND
             --       HAS_ERRORS					= 'N'
                  
                  
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
                   ,@LOB_ID					= ISNULL(POLICY_LOB,0) 
				   ,@SUB_LOB_ID				= ISNULL(POLICY_SUBLOB,0)  
              FROM POL_CO_INSURANCE  C INNER JOIN  
                   POL_CUSTOMER_POLICY_LIST P ON C.CUSTOMER_ID=P.CUSTOMER_ID AND c.POLICY_ID=P.POLICY_ID AND C.POLICY_VERSION_ID=P.POLICY_VERSION_ID  
                   LEFT OUTER JOIN POL_POLICY_PROCESS PP ON PP.CUSTOMER_ID=P.CUSTOMER_ID AND PP.POLICY_ID=P.POLICY_ID AND PP.NEW_POLICY_VERSION_ID=P.POLICY_VERSION_ID AND PP.PROCESS_STATUS<>'ROLLBACK'  
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
				ELSE IF(@CHK_POLICY_STATUS='EXPIRED' OR @CHK_POLICY_STATUS='Suspended')                  BEGIN  
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
    
    --  SELECT  @CUSTOMER_ID			= COI.CUSTOMER_ID,    
			 -- @POLICY_ID			= COI.POLICY_ID,   
    --          @POLICY_VERSION_ID    = COI.POLICY_VERSION_ID,   
			 -- @LOB_ID				= ISNULL(POLICY_LOB,0),  
			 -- @SUB_LOB_ID		    = ISNULL(POLICY_SUBLOB,0)  
    --  FROM    POL_CO_INSURANCE AS COI INNER JOIN  
    --          POL_POLICY_ENDORSEMENTS AS EN ON  COI.CUSTOMER_ID=EN.CUSTOMER_ID AND COI.POLICY_ID=EN.POLICY_ID AND COI.POLICY_VERSION_ID=EN.POLICY_VERSION_ID INNER JOIN  
			 -- POL_CUSTOMER_POLICY_LIST AS POL ON COI.CUSTOMER_ID=POL.CUSTOMER_ID AND COI.POLICY_ID=POL.POLICY_ID AND COI.POLICY_VERSION_ID=POL.POLICY_VERSION_ID                     
    --  WHERE   COI.LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER AND POL.POLICY_STATUS IS NOT NULL AND   
			 ----POL.POLICY_STATUS <>'' AND --POL.POLICY_STATUS NOT IN('UISSUE','REJECT','Suspended','APPLICATION') AND   
			 -- POL.POLICY_NUMBER IS NOT NULL AND POL.POLICY_NUMBER<>'' AND  
			 -- CAST(EN.CO_ENDORSEMENT_NO AS INT) =@LEADER_ENDORSEMENT_NUMBER+1  
    END  
     
   
     
         IF (@HAS_ERRORS>0)        
         BEGIN     
			
		             
			UPDATE MIG_POLICY_COVERAGES      
			SET    HAS_ERRORS='Y'      
			WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND  
			 LEADER_POLICY_NUMBER='00'+@LEADER_POLICY_NUMBER AND   
			 LEADER_ENDORSEMENT_NUMBER=@LEADER_ENDORSEMENT_NUMBER  
		           
         
          INSERT INTO [MIG_IMPORT_ERROR_DETAILS]      
            (  
			[IMPORT_REQUEST_ID]           
			,[IMPORT_SERIAL_NO]                
			,[ERROR_DATETIME]              
			,[ERROR_TYPES]                    
			,[ERROR_MODE]    
			,ERROR_SOURCE_TYPE     
			)               
			(    
			SELECT                        
			@IMPORT_REQUEST_ID           
			,SERIAL_NO    
			,GETDATE()                  
			,@HAS_ERRORS  
			,'VE'       
			,'COV'   
			FROM   MIG_POLICY_COVERAGES     
			WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND  
			LEADER_POLICY_NUMBER='00'+@LEADER_POLICY_NUMBER AND   
			LEADER_ENDORSEMENT_NUMBER=@LEADER_ENDORSEMENT_NUMBER  

            )        
              
         END  -- END OF IF (@HAS_ERRORS>0)         
         ELSE      
         BEGIN      
               
           INSERT INTO #TEMP_COVERAGES      
			(      
			IMPORT_SERIAL_NO,      
			COVERAGE_CODE      
			       
			)      
			(           
			SELECT  C.SERIAL_NO,M.ALBA_COVERAGE_CODE   
			FROM   MIG_POLICY_COVERAGES C INNER JOIN       
			MIG_POLICY_COVERAGE_CODE_MAPPING AS M ON CAST(C.LEADER_COVERAGE_CODE AS INT)=M.LEADER_COVERAGE_CODE      
			WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND  
			   LEADER_POLICY_NUMBER ='00'+@LEADER_POLICY_NUMBER AND  
			   LEADER_ENDORSEMENT_NUMBER=@LEADER_ENDORSEMENT_NUMBER   AND  
			   HAS_ERRORS='N'   
			)      
      
   ------------------------------------      
   -- LOOP FOR VALIDATE COVERAGE CODE      
   ------------------------------------      
         
   DECLARE @LOOP_COVERAGE_CODE NVARCHAR(20)      
   DECLARE @LOOP_IMPORT_SERIAL_NO NVARCHAR(20)      
         
      
      
   SELECT  @CHK_COUNTER= COUNT(ID) FROM #TEMP_COVERAGES      
         
   WHILE(@CHK_COUNTER>0)      
   BEGIN      
              
        SELECT @LOOP_COVERAGE_CODE=COVERAGE_CODE ,@LOOP_IMPORT_SERIAL_NO=IMPORT_SERIAL_NO      
        FROM  #TEMP_COVERAGES      
        WHERE ID=@CHK_COUNTER      
             
             
            
       ------------------------------------      
    -- CHECK COVERAGE CODE      
    -- ERROR TYPE =24      
    ------------------------------------    
    --IF NOT EXISTS(SELECT CARRIER_COV_CODE FROM MNT_COVERAGE WHERE CAST(CARRIER_COV_CODE AS INT) = CAST(@LOOP_COVERAGE_CODE AS INT) AND LOB_ID=@LOB_ID AND SUB_LOB_ID=@SUB_LOB_ID )      
    IF NOT EXISTS(select A.CARRIER_COV_CODE from MNT_COVERAGE A
                  where( CAST(@LOOP_COVERAGE_CODE AS INT)=A.CARRIER_COV_CODE AND A.LOB_ID=@LOB_ID and SUB_LOB_ID=@SUB_LOB_ID)
                  )
     BEGIN      
              
       SET @HAS_ERRORS=24        
               
       UPDATE MIG_POLICY_COVERAGES      
       SET    HAS_ERRORS='Y'      
       WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND SERIAL_NO=@LOOP_IMPORT_SERIAL_NO      
                      
       EXEC PROC_MIG_INSERT_IMPORT_ERROR_DETAILS                   
         @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,      
         @IMPORT_SERIAL_NO      = @LOOP_IMPORT_SERIAL_NO  ,          
         @ERROR_SOURCE_FILE     = ''     ,      
         @ERROR_SOURCE_COLUMN   = ''     ,      
         @ERROR_SOURCE_COLUMN_VALUE= '' ,      
         @ERROR_ROW_NUMBER      = @LOOP_IMPORT_SERIAL_NO   ,        
         @ERROR_TYPES           = @HAS_ERRORS,          
         @ACTUAL_RECORD_DATA    = '' ,      
         @ERROR_MODE            = 'VE',  -- VALIDATION ERROR        
         @ERROR_SOURCE_TYPE     = 'COV'   
     END      
              
        SET @CHK_COUNTER=@CHK_COUNTER-1      
              
   END      
         
        
               
         END       
              
           DROP TABLE #TEMP_COVERAGES       
                             
     
         
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

