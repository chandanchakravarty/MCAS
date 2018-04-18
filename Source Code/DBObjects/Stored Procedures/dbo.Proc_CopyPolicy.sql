IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CopyPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CopyPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
/*----------------------------------------------------------                                                                  
Proc Name       : dbo.Proc_CopyPolicy                                                               
Created by      : Charles Gomes                                                             
Date            : 6/April/2010  
Purpose         : Create New Version of Application                                                     
    
---------------------------------------------------------- */                                                        
-- drop proc Proc_CopyPolicy                                                                                             
CREATE proc  [dbo].[Proc_CopyPolicy]                                               
(                                                                        
 @CUSTOMER_ID INT,                                                                              
 @APP_ID  INT, --Policy ID                                                                             
 @APP_VERSION_ID SMALLINT, --Policy Version                                                                             
 @CREATED_BY INT,                                                                
 @NEW_VERSION INT OUTPUT                                                                              
)      
AS                                                
BEGIN                               
                                                                    
 BEGIN TRAN                                                                
   DECLARE @TEMP_ERROR_CODE INT, 
   @DATE NVARCHAR(50),    
   @APP_NEW_VERSION SMALLINT,
   @NEW_REMUNERATION_ID INT,
   @NEW_COINSURANCE_ID INT,
   @NEW_LOCATION_ID INT                                        
                                                            
   -- Get NEW Version number                                                                                                                            
   SET @APP_NEW_VERSION = (SELECT ISNULL(MAX(POLICY_VERSION_ID),0)+1 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@APP_ID)                                                                              
                                     
   SELECT @NEW_REMUNERATION_ID = ISNULL(MAX(REMUNERATION_ID),0) + 1 FROM POL_REMUNERATION WITH(NOLOCK)   
   SELECT @NEW_COINSURANCE_ID = ISNULL(MAX(COINSURANCE_ID),0) + 1 FROM POL_CO_INSURANCE WITH(NOLOCK) 
   SELECT @NEW_LOCATION_ID = ISNULL(MAX(LOCATION_ID),0) + 1 FROM POL_LOCATIONS WITH(NOLOCK)                                            
   -- Get Current date and time to update for Created_Datetime field                                                                                   
   SET @DATE = CONVERT(VARCHAR(50),GETDATE(),100)                                                                                                                                                                                                            
                     
 /* COPY THE COMMON DATA */                      
                                                                              
   --1.                         
   /*                     
   SELECT * INTO #APP_LIST   FROM APP_LIST  (nolock)      
   WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                     
                                                                  
   UPDATE #APP_LIST SET APP_VERSION_ID = @APP_NEW_VERSION,APP_VERSION = cast(@APP_NEW_VERSION as varchar)+'.0',CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                     
                                                       
   INSERT INTO APP_LIST SELECT * FROM #APP_LIST (nolock)                                                                   
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                              
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
                                                               
   DROP TABLE #APP_LIST                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
                                                                        
   --2.                                                                    
   SELECT * INTO #APP_APPLICANT_LIST FROM APP_APPLICANT_LIST  (nolock)      
   WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID      
   SELECT @TEMP_ERROR_CODE = @@ERROR       
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                             
                                                      
   UPDATE #APP_APPLICANT_LIST SET APP_VERSION_ID = @APP_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                             
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                            
                                                        
   INSERT INTO APP_APPLICANT_LIST SELECT * FROM #APP_APPLICANT_LIST  (nolock)                                                                  
   SELECT @TEMP_ERROR_CODE = @@ERROR                                          
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                         
                                                        
  DROP TABLE #APP_APPLICANT_LIST                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                                        
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
   */   
  --3.    
   SELECT * INTO #POL_CUSTOMER_POLICY_LIST   FROM POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK) 
   WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@APP_ID and POLICY_VERSION_ID=@APP_VERSION_ID                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                     
                                                                  
   UPDATE #POL_CUSTOMER_POLICY_LIST SET APP_VERSION_ID = @APP_NEW_VERSION, POLICY_VERSION_ID = @APP_NEW_VERSION,  POLICY_DISP_VERSION = cast(@APP_NEW_VERSION as varchar)+'.0',
   APP_VERSION = cast(@APP_NEW_VERSION as varchar)+'.0',CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                     
                                                       
   INSERT INTO POL_CUSTOMER_POLICY_LIST SELECT * FROM #POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                                
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
                                                               
   DROP TABLE #POL_CUSTOMER_POLICY_LIST                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
                                                                        
   --4.                                                                    
   SELECT * INTO #POL_APPLICANT_LIST FROM POL_APPLICANT_LIST  WITH(NOLOCK)    
   WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@APP_ID and POLICY_VERSION_ID=@APP_VERSION_ID      
   SELECT @TEMP_ERROR_CODE = @@ERROR       
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                             
                                                      
   UPDATE #POL_APPLICANT_LIST SET POLICY_VERSION_ID = @APP_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                             
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                            
                                                        
   INSERT INTO POL_APPLICANT_LIST SELECT * FROM #POL_APPLICANT_LIST  WITH(NOLOCK)                                                                 
   SELECT @TEMP_ERROR_CODE = @@ERROR                                          
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                         
                                                        
  DROP TABLE #POL_APPLICANT_LIST                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                                        
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM  
    
    
   --5. 
   SELECT * INTO #POL_REMUNERATION FROM POL_REMUNERATION  WITH(NOLOCK)    
   WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@APP_ID and POLICY_VERSION_ID=@APP_VERSION_ID      
   SELECT @TEMP_ERROR_CODE = @@ERROR       
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                             
                                                      
   UPDATE #POL_REMUNERATION SET REMUNERATION_ID = @NEW_REMUNERATION_ID, POLICY_VERSION_ID = @APP_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                             
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                            
                                                        
   INSERT INTO POL_REMUNERATION SELECT * FROM #POL_REMUNERATION  WITH(NOLOCK)                                                                 
   SELECT @TEMP_ERROR_CODE = @@ERROR                                          
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                         
                                                        
  DROP TABLE #POL_REMUNERATION                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                                        
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM  
    
    --6.
   SELECT * INTO #POL_CO_INSURANCE FROM POL_CO_INSURANCE  WITH(NOLOCK)    
   WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@APP_ID and POLICY_VERSION_ID=@APP_VERSION_ID      
   SELECT @TEMP_ERROR_CODE = @@ERROR       
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                             
                                                      
   UPDATE #POL_CO_INSURANCE SET COINSURANCE_ID = @NEW_COINSURANCE_ID, POLICY_VERSION_ID = @APP_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                             
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                            
                                                        
   INSERT INTO POL_CO_INSURANCE SELECT * FROM #POL_CO_INSURANCE  WITH(NOLOCK)                                                                 
   SELECT @TEMP_ERROR_CODE = @@ERROR                                          
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                         
                                                        
  DROP TABLE #POL_CO_INSURANCE                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                                        
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM    
    
   --7.
   SELECT * INTO #POL_LOCATIONS FROM POL_LOCATIONS  WITH(NOLOCK)    
   WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@APP_ID and POLICY_VERSION_ID=@APP_VERSION_ID      
   SELECT @TEMP_ERROR_CODE = @@ERROR       
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                             
                                                      
   UPDATE #POL_LOCATIONS SET LOCATION_ID = @NEW_LOCATION_ID, POLICY_VERSION_ID = @APP_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                             
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                            
                                                        
   INSERT INTO POL_LOCATIONS SELECT * FROM #POL_LOCATIONS  WITH(NOLOCK)                                                                 
   SELECT @TEMP_ERROR_CODE = @@ERROR                                          
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                         
                                                        
  DROP TABLE #POL_LOCATIONS                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                                        
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
    
--- Billing Info   
 --EFT    
  --SELECT * INTO #ACT_APP_EFT_CUST_INFO FROM ACT_APP_EFT_CUST_INFO AEFT  WITH(NOLOCK) 
  --WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                      
  --SELECT @TEMP_ERROR_CODE = @@ERROR                                                     
  --IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                     
                                                            
  --UPDATE #ACT_APP_EFT_CUST_INFO SET APP_VERSION_ID = @APP_NEW_VERSION ,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                 
  --SELECT @TEMP_ERROR_CODE = @@ERROR                                         
  --IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                   
  --INSERT INTO ACT_APP_EFT_CUST_INFO               
  --SELECT * FROM #ACT_APP_EFT_CUST_INFO (nolock)                                              
                                                                
  --SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  --IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
                                                            
  --DROP TABLE #ACT_APP_EFT_CUST_INFO                                                  
  --SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
  --IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM    
  
  SELECT * INTO #ACT_POL_EFT_CUST_INFO FROM ACT_POL_EFT_CUST_INFO AEFT  WITH(NOLOCK) 
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@APP_ID and POLICY_VERSION_ID=@APP_VERSION_ID                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                     
                                                            
  UPDATE #ACT_POL_EFT_CUST_INFO SET POLICY_VERSION_ID = @APP_NEW_VERSION ,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM  
                                                                   
  INSERT INTO ACT_POL_EFT_CUST_INFO               
  SELECT * FROM #ACT_POL_EFT_CUST_INFO (nolock)                                              
                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
                                                            
  DROP TABLE #ACT_POL_EFT_CUST_INFO                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM  
    
 --CREDIT CARD     
--  SELECT * INTO #ACT_APP_CREDIT_CARD_DETAILS FROM ACT_APP_CREDIT_CARD_DETAILS ACREDIT  WITH(NOLOCK)
--  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                      
--  SELECT @TEMP_ERROR_CODE = @@ERROR                                                     
--  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                     
                                                            
--  UPDATE #ACT_APP_CREDIT_CARD_DETAILS SET APP_VERSION_ID = @APP_NEW_VERSION                                                             
--  SELECT @TEMP_ERROR_CODE = @@ERROR                            
--  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                   
--  INSERT INTO ACT_APP_CREDIT_CARD_DETAILS    
--(APP_ID,APP_VERSION_ID,CUSTOMER_ID,PAY_PAL_REF_ID)    
    
--   SELECT APP_ID,APP_VERSION_ID,CUSTOMER_ID,PAY_PAL_REF_ID    
--  FROM #ACT_APP_CREDIT_CARD_DETAILS (nolock)                                              
                                                                
--  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
--  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
                                                            
--  DROP TABLE #ACT_APP_CREDIT_CARD_DETAILS                                                  
--  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
--  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM     

SELECT * INTO #ACT_POL_CREDIT_CARD_DETAILS FROM ACT_POL_CREDIT_CARD_DETAILS ACREDIT  WITH(NOLOCK)
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@APP_ID and POLICY_VERSION_ID=@APP_VERSION_ID                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                     
                                                            
  UPDATE #ACT_POL_CREDIT_CARD_DETAILS SET POLICY_VERSION_ID = @APP_NEW_VERSION                                                             
  SELECT @TEMP_ERROR_CODE = @@ERROR                            
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
                                                                    
  INSERT INTO ACT_POL_CREDIT_CARD_DETAILS    
(POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID,PAY_PAL_REF_ID)    
    
   SELECT POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID,PAY_PAL_REF_ID    
  FROM #ACT_POL_CREDIT_CARD_DETAILS (nolock)                                              
                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
                                                            
  DROP TABLE #ACT_POL_CREDIT_CARD_DETAILS                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM     

-- End Billing Info    
      
   COMMIT TRAN                                                                        
   SET @NEW_VERSION=@APP_NEW_VERSION                                                                                           
   RETURN @NEW_VERSION                                                                                            
                                         
  PROBLEM:                                                                                          
                                                                                         
   ROLLBACK TRAN                                                                                          
   SET @NEW_VERSION = -1                                                                           
   RETURN @NEW_VERSION                                                                                                                    
                                      
END                
              
GO

