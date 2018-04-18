IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_CANCEL_INSTALLMENT_BOLETO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_CANCEL_INSTALLMENT_BOLETO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--BEGIN TRAN  
--DROP PROC   [dbo].[PROC_MIG_CANCEL_INSTALLMENT_BOLETO]         
--GO  
 /*----------------------------------------------------------                                                                      
Proc Name             : Dbo.PROC_MIG_CANCEL_INSTALLMENT_BOLETO                                                                      
Created by            : Lalit Kumar Chauhan  
Date                  : 29 May 2011                                                                    
Purpose               : TO CANCEL COI INSTALLMENT  
Revison History       :                                                                      
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD                         
------------------------------------------------------------                                                                      
Date     Review By          Comments                                         
                                
drop Proc [dbo].[PROC_MIG_CANCEL_INSTALLMENT_BOLETO]   136  
------   ------------       -------------------------*/  
CREATE PROCEDURE [dbo].[PROC_MIG_CANCEL_INSTALLMENT_BOLETO]              
@IMPORT_REQUEST_ID    INT            
AS                                          
BEGIN                                   
              
  DECLARE @ERROR_NUMBER    INT  
  DECLARE @ERROR_SEVERITY  INT  
  DECLARE @ERROR_STATE     INT  
  DECLARE @ERROR_PROCEDURE VARCHAR(512)  
  DECLARE @ERROR_LINE    INT  
  DECLARE @ERROR_MESSAGE   NVARCHAR(MAX) 
              
              
  BEGIN TRY            
               
                 
	DECLARE @COUNTER     INT =1            
	DECLARE @TOTAL_RECORD_COUNT         INT =0            
	DECLARE @LEADER_POLICY_NUMBER        NVARCHAR(20) =0            
	DECLARE @HAS_ERROR                  INT =0      
	DECLARE @CUSTOMER_ID   INT  
	DECLARE @POLICY_ID    INT  
	DECLARE @POLICY_VERSION_ID  INT   
	DECLARE @FOLLOWER INT  =  14549  
	DECLARE @END_COUNT INT , @END_COUNTER INT = 1   
	DECLARE @CO_ENDORSEMENT_NO NVARCHAR(6)  
	DECLARE @ALBA_POLICY_NO NVARCHAR(50)   
	DECLARE @ALBA_END_NO INT  

  
     CREATE TABLE #TEMP_POLICY_INSTALLMENT_CANCEL  
     (            
	  ID INT  IDENTITY(1,1),                
	  LEADER_POLICY_NUMBER NVARCHAR(20)      
     )            
     
     CREATE TABLE #TEMP_POLICY_ENDORSEMENT  
     (            
	  ID INT  IDENTITY(1,1),                
	  LEADER_POLICY_NUMBER NVARCHAR(20)   ,  
	  ENDORSEMENT_NO NVARCHAR(20)     
     )              
       
        --SELECT @IMPORT_REQUEST_ID        
    INSERT INTO #TEMP_POLICY_INSTALLMENT_CANCEL          
     (                      
       LEADER_POLICY_NUMBER      
     )            
     (            
      SELECT DISTINCT(LEADER_POLICY_NUMBER)      
      FROM   MIG_POLICY_INSTALLMENT_CANCEL            
      WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND   
			 HAS_ERRORS		   = 'N' AND  
			 IS_DELETED		   = 'N'  
     )                      
      
     --SELECT * FROM #TEMP_POLICY_INSTALLMENT_CANCEL
     SELECT @TOTAL_RECORD_COUNT =COUNT(ID) FROM #TEMP_POLICY_INSTALLMENT_CANCEL            
  
     ---------------------------------------            
     --- LOOP TO GET POLICY DETAILS  
     ---------------------------------------            
     WHILE(@COUNTER <=@TOTAL_RECORD_COUNT)            
     BEGIN            
                 
            
                    
        SELECT @LEADER_POLICY_NUMBER = LEADER_POLICY_NUMBER      
        FROM   #TEMP_POLICY_INSTALLMENT_CANCEL            
        WHERE  ID =@COUNTER            
       
     
	SELECT @CUSTOMER_ID = 0, @POLICY_ID=0  
	
    SELECT TOP 1 @CUSTOMER_ID = CUSTOMER_ID ,  
				 @POLICY_ID = POLICY_ID  
    FROM POL_CO_INSURANCE 
    WHERE LEADER_POLICY_NUMBER = @LEADER_POLICY_NUMBER --AND LEADER_FOLLOWER = @FOLLOWER  
        	
	
    ---------------------------------------------  
    -- if policy Not found then update error details  
    -----------------------------------------------*/  
   IF (@CUSTOMER_ID IS NULL OR @CUSTOMER_ID = 0)  
    BEGIN   
      
    --update MIG table with error flag = yes  
    UPDATE MIG_POLICY_INSTALLMENT_CANCEL       
    SET    HAS_ERRORS='Y'      
    WHERE  LEADER_POLICY_NUMBER = @LEADER_POLICY_NUMBER      
       AND IMPORT_REQUEST_ID    = @IMPORT_REQUEST_ID 
       AND HAS_ERRORS			= 'N' 
      
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
      ,IMPORT_SERIAL_NO        
      ,GETDATE()  
      ,10  
      ,'VE'           
      ,'ISTC'       
      FROM  MIG_POLICY_INSTALLMENT_CANCEL  
      WHERE IMPORT_REQUEST_ID      = @IMPORT_REQUEST_ID AND      
            LEADER_POLICY_NUMBER   = @LEADER_POLICY_NUMBER  
       )                     
    END  
   ELSE   
    BEGIN   
     
   --SELECT * FROM @LEADER_POLICY_NUMBER  
   DELETE FROM #TEMP_POLICY_ENDORSEMENT  
   -------------------------------------------  
   -----Get Policy Distinct Endorsemnts-------  
   -------------------------------------------  
   INSERT INTO #TEMP_POLICY_ENDORSEMENT          
   (               
   ENDORSEMENT_NO,  
   LEADER_POLICY_NUMBER    
   )            
   (            
     SELECT DISTINCT(LEADER_ENDORSEMENT_NUMBER),@LEADER_POLICY_NUMBER  
     FROM   MIG_POLICY_INSTALLMENT_CANCEL            
     WHERE  IMPORT_REQUEST_ID	 = @IMPORT_REQUEST_ID AND  
			LEADER_POLICY_NUMBER = @LEADER_POLICY_NUMBER AND   
		    HAS_ERRORS		     = 'N' AND  
		    IS_DELETED		     = 'N'  
   )          
      
   SET @END_COUNTER= 1
  -- SELECT * FROM #TEMP_POLICY_ENDORSEMENT     
   SELECT @END_COUNT = COUNT(ENDORSEMENT_NO) FROM #TEMP_POLICY_ENDORSEMENT  
  
   ---------------------------------------------------------------  
   -----Get Endorsemnts Policy version Id from Policy Table-------  
   ---------------------------------------------------------------  
   WHILE(@END_COUNTER <=@END_COUNT)  
   BEGIN  
        SET  @POLICY_VERSION_ID = 0  
        SET  @ALBA_END_NO = 0  
       
       
     SELECT @CO_ENDORSEMENT_NO=ENDORSEMENT_NO FROM #TEMP_POLICY_ENDORSEMENT WHERE ID =@END_COUNTER      
      
     IF(@CO_ENDORSEMENT_NO <> 0)  
      BEGIN
        
      SELECT @POLICY_VERSION_ID  =  POLICY_VERSION_ID,@ALBA_END_NO=ENDORSEMENT_NO 
      FROM POL_POLICY_ENDORSEMENTS WITH(NOLOCK) 
      WHERE	@CUSTOMER_ID 	   = @CUSTOMER_ID AND   
		    POLICY_ID    	   = @POLICY_ID AND 
		    CO_ENDORSEMENT_NO  = @CO_ENDORSEMENT_NO  
      END  
       
     ELSE ---in case of NBS Commit  no endorseemnt  
      SET  @POLICY_VERSION_ID = 1  
       
      -- SELECT * FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID
      -- SELECT @POLICY_VERSION_ID
     ---------------------------------------  
     --update installment boletos  ---------  
     ---------------------------------------  
     --UPDATE ACT_POLICY_INSTALLMENT_DETAILS SET RELEASED_STATUS = 'C'  WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
     --AND INSTALLMENT_NO IN (  
     --      SELECT INSTALLMENT_NUMBER   
     --      FROM   MIG_POLICY_INSTALLMENT_CANCEL WITH(NOLOCK)   
     --      WHERE  IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID    AND  
     --             LEADER_POLICY_NUMBER      = @LEADER_POLICY_NUMBER AND   
     --             LEADER_ENDORSEMENT_NUMBER = @CO_ENDORSEMENT_NO)   
     --             AND ISNULL(RELEASED_STATUS,'N')='N'  
                  
     --           ---cancel Boleto   
			  --UPDATE POL_INSTALLMENT_BOLETO SET IS_ACTIVE = 'N' WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID 
			  --AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND INSTALLEMT_ID IN (SELECT ACT_POLICY_INSTALLMENT_DETAILS.POLICY_VERSION_ID FROM ACT_POLICY_INSTALLMENT_DETAILS
			  --WITH(NOLOCK) WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND RELEASED_STATUS = 'C'
			  --)
      
     SET @END_COUNTER = @END_COUNTER + 1  
       
       
    IF(@POLICY_VERSION_ID<>0)  
    BEGIN  
     
      
    /*  
    -------------------------------------------------------  
     Update MIG TABLE IF POLICY DOES NOT HAVE ANY ERROR  
    ------------------------------------------------------  
    */  
    SELECT @ALBA_POLICY_NO = POLICY_NUMBER 
    FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID  
   
    UPDATE MIG_POLICY_INSTALLMENT_CANCEL   
		SET CUSTOMER_ID   				= @CUSTOMER_ID ,  
			POLICY_ID     				= @POLICY_ID ,  
		    POLICY_VERSION_ID   		= @POLICY_VERSION_ID,  
		    ALBA_POLICY_NUMBER  		= @ALBA_POLICY_NO,  
		    ALBA_ENDORSEMENT_NO  		= @ALBA_END_NO  
    WHERE   IMPORT_REQUEST_ID			= @IMPORT_REQUEST_ID    AND  
            LEADER_POLICY_NUMBER		= @LEADER_POLICY_NUMBER AND   
            LEADER_ENDORSEMENT_NUMBER   = @CO_ENDORSEMENT_NO   
 
    
    END   
    ELSE --if policy endorsemnt not found  
    BEGIN  
    
     UPDATE MIG_POLICY_INSTALLMENT_CANCEL SET HAS_ERRORS = 'Y'  
     WHERE LEADER_POLICY_NUMBER = @LEADER_POLICY_NUMBER   
     AND LEADER_ENDORSEMENT_NUMBER = @CO_ENDORSEMENT_NO   
      
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
      ,IMPORT_SERIAL_NO        
      ,GETDATE()  
      ,10  
      ,'VE'           
      ,'ISTC'       
      FROM  MIG_POLICY_INSTALLMENT_CANCEL  
       WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND      
      LEADER_POLICY_NUMBER   = @LEADER_POLICY_NUMBER AND  
      LEADER_ENDORSEMENT_NUMBER = @CO_ENDORSEMENT_NO   
        
       )         
      
      
    END  
      
   END  
    
     
      
        -- END  
             
          
       END         
             SET @COUNTER+=1            
     END  
                 
     DROP TABLE #TEMP_POLICY_INSTALLMENT_CANCEL          
     DROP TABLE #TEMP_POLICY_ENDORSEMENT  
                 
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
  
--GO  
--EXEC   [dbo].[PROC_MIG_CANCEL_INSTALLMENT_BOLETO]  119  
----UPDATE MIG_POLICY_INSTALLMENT_CANCEL SET HAS_ERRORS = 'N', IS_DELETED = 'N'   
------SET ALBA_ENDORSEMENT_NO = null,CUSTOMER_ID = null ,POLICY_ID =null ,POLICY_VERSION_ID = null,ALBA_POLICY_NUMBER = NULL  
----WHERE IMPORT_REQUEST_ID = 119
--------SELECT * FROM ACT_POLICY_INSTALLMENT_DETAILS  
--------wHERE CUSTOMER_ID =28070 and POLICY_ID = 419 --AND ENDORSEMENT_NO  =2  
  
--SELECT HAS_ERRORS,* FROm   
--MIG_POLICY_INSTALLMENT_CANCEL WHERE   IMPORT_REQUEST_ID = 119 AND HAS_ERRORS = 'N'
----IMPORT_REQUEST_ID   = 136  
--  --select * From ACT_POLICY_INSTALLMENT_DETAILS wHERE CUSTOMER_ID = 2727 and POLICY_ID = 118
--  -- select * From POL_INSTALLMENT_BOLETO wHERE CUSTOMER_ID = 2727 and POLICY_ID = 118
--ROLLBACK TRAN


GO

