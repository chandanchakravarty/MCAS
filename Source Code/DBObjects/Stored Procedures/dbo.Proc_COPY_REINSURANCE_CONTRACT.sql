IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_COPY_REINSURANCE_CONTRACT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_COPY_REINSURANCE_CONTRACT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*          
CREATED BY   : Swarup           
CREATED DATE  : August 31, 2007          
Purpose   : To COPY RECORD OF REINSURANSE CONTRACT          
*/          
--drop proc [dbo].[Proc_COPY_REINSURANCE_CONTRACT] 62        
CREATE PROCEDURE [dbo].[Proc_COPY_REINSURANCE_CONTRACT]          
(         
 @CONTRACT_ID INT,    
 @CREATED_BY INT        
)        
AS       
--BEGIN TRAN         
BEGIN        
   DECLARE @TEMP_ERROR_CODE INT        
   DECLARE @CONTRACT_ID_NEW INT         
   DECLARE @ATTACHMENT_ID_NEW INT        
   DECLARE @PARTICIPATION_ID_NEW INT        
   DECLARE @MAJOR_PARTICIPATION_ID_OLD INT      
   DECLARE @MINOR_PARTICIPATION_ID_NEW INT        
   DECLARE @PREMIUM_BUILDER_ID_NEW INT        
   DECLARE @LOSS_LAYER_ID_NEW INT         
   DECLARE @EFFECTIVE_DATE_NEW DATETIME        
   DECLARE @EXPIRATION_DATE_NEW DATETIME        
   DECLARE @CONTACT_YEAR_NEW NVARCHAR(10)      
                                                           
   SET @CONTRACT_ID_NEW = (SELECT MAX(isnull(CONTRACT_ID,0))+1 FROM MNT_REINSURANCE_CONTRACT)         
   SET @EFFECTIVE_DATE_NEW = (SELECT DATEADD(YEAR,1,(SELECT EFFECTIVE_DATE FROM MNT_REINSURANCE_CONTRACT WHERE CONTRACT_ID = @CONTRACT_ID)))        
   SET @EXPIRATION_DATE_NEW = (SELECT DATEADD(YEAR,1,(SELECT EXPIRATION_DATE FROM MNT_REINSURANCE_CONTRACT WHERE CONTRACT_ID = @CONTRACT_ID)))        
   SET @CONTACT_YEAR_NEW = (SELECT CONVERT(NVARCHAR,CONVERT(INT,CONTACT_YEAR)+1) FROM MNT_REINSURANCE_CONTRACT WHERE  CONTRACT_ID = @CONTRACT_ID)  
--1.Contract Information                                                    
 SELECT * INTO #MNT_REINSURANCE_CONTRACT   FROM MNT_REINSURANCE_CONTRACT  (nolock)            
  WHERE  CONTRACT_ID = @CONTRACT_ID                                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                           
                                                                 
 UPDATE #MNT_REINSURANCE_CONTRACT SET CONTRACT_ID = @CONTRACT_ID_NEW,        
  EFFECTIVE_DATE = @EFFECTIVE_DATE_NEW,        
  EXPIRATION_DATE = @EXPIRATION_DATE_NEW,  
  CONTACT_YEAR  = @CONTACT_YEAR_NEW,  
  CREATED_BY = @CREATED_BY,    
  CREATED_DATETIME = getDate();        
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                           
                                                      
 INSERT INTO MNT_REINSURANCE_CONTRACT SELECT * FROM #MNT_REINSURANCE_CONTRACT (nolock)                                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                          
                                                              
 DROP TABLE #MNT_REINSURANCE_CONTRACT                                                                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM         
      
--1.1 Contract LOB      
      
 SELECT * INTO #MNT_REIN_CONTRACT_LOB   FROM MNT_REIN_CONTRACT_LOB  (nolock)            
  WHERE  CONTRACT_ID = @CONTRACT_ID                                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                           
                                                                 
 UPDATE #MNT_REIN_CONTRACT_LOB SET CONTRACT_ID = @CONTRACT_ID_NEW    
--,  CREATED_BY = @CREATED_BY,    
 -- CREATED_DATETIME = getDate();          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                           
                       
INSERT INTO MNT_REIN_CONTRACT_LOB SELECT * FROM #MNT_REIN_CONTRACT_LOB (nolock)                                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                       
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                          
                                                              
 DROP TABLE #MNT_REIN_CONTRACT_LOB                                                                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
       
--1.2 Risk Exposure      
      
 SELECT * INTO #MNT_REIN_CONTRACT_RISKEXPOSURE   FROM MNT_REIN_CONTRACT_RISKEXPOSURE  (nolock)            
 WHERE  CONTRACT_ID = @CONTRACT_ID                                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                           
                                                                 
 UPDATE #MNT_REIN_CONTRACT_RISKEXPOSURE SET CONTRACT_ID = @CONTRACT_ID_NEW    
--,  CREATED_BY = @CREATED_BY,    
--  CREATED_DATETIME = getDate();          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                           
                                                      
 INSERT INTO MNT_REIN_CONTRACT_RISKEXPOSURE SELECT * FROM #MNT_REIN_CONTRACT_RISKEXPOSURE (nolock)                                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                          
                                                              
 DROP TABLE #MNT_REIN_CONTRACT_RISKEXPOSURE                                                                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
       
--1.3 Contract State      
 SELECT * INTO #MNT_REIN_CONTRACT_STATE   FROM MNT_REIN_CONTRACT_STATE  (nolock)            
 WHERE  CONTRACT_ID = @CONTRACT_ID                                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                           
                                                                 
 UPDATE #MNT_REIN_CONTRACT_STATE SET CONTRACT_ID = @CONTRACT_ID_NEW     
--,  CREATED_BY = @CREATED_BY,    
 -- CREATED_DATETIME = getDate();         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                           
                                                      
 INSERT INTO MNT_REIN_CONTRACT_STATE SELECT * FROM #MNT_REIN_CONTRACT_STATE (nolock)                                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                    
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                          
                                                              
 DROP TABLE #MNT_REIN_CONTRACT_STATE                                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
      
              
              
      
       
        
--2. Attachment        
        
SET @ATTACHMENT_ID_NEW = (SELECT MAX(isnull(ATTACH_ID,0))+1 FROM MNT_ATTACHMENT_LIST (nolock))         
      
      
SELECT (SELECT COUNT(*) FROM MNT_ATTACHMENT_LIST WHERE ATTACH_ENT_ID<=A.ATTACH_ENT_ID      
 AND a.ATTACH_ID=ATTACH_ID) AS ROW_NUMBER_ATTACH,      
 * INTO #MNT_ATTACHMENT_LIST      
 FROM MNT_ATTACHMENT_LIST A           
 WHERE  ATTACH_ENT_ID = @CONTRACT_ID      
 and ATTACH_ENTITY_TYPE = 'REINSURANCE'       
                                                              
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM          
      
 UPDATE #MNT_ATTACHMENT_LIST SET ATTACH_ENT_ID = @CONTRACT_ID_NEW    
    
 -- CREATED_BY = @CREATED_BY,    
--  CREATED_DATETIME = getDate();                                                                           
      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM          
      
 DECLARE  @ROW_NUMBER_ATTACH INT      
 SET    @ROW_NUMBER_ATTACH=1                                                                      
 WHILE 1=1        
      
BEGIN      
--SELECT @ATTACHMENT_ID_NEW      
  IF NOT EXISTS(SELECT * FROM #MNT_ATTACHMENT_LIST WHERE ROW_NUMBER_ATTACH=@ROW_NUMBER_ATTACH )      
    BEGIN      
  BREAK      
    END                                                             
                                                                  
 INSERT INTO MNT_ATTACHMENT_LIST (      
   ATTACH_LOC,ATTACH_ENT_ID,ATTACH_FILE_NAME,ATTACH_DATE_TIME,ATTACH_USER_ID,ATTACH_FILE_DESC,      
   ATTACH_POLICY_ID,ATTACH_POLICY_VER_TRACKING_ID,ATTACH_GEN_FILE_NAME,ATTACH_FILE_TYPE,ATTACH_ENTITY_TYPE,      
   ATTACH_CUSTOMER_ID,ATTACH_APP_ID,ATTACH_APP_VER_ID,SOURCE_ATTACH_ID,IS_ACTIVE,ATTACH_TYPE)      
         
   SELECT ATTACH_LOC,ATTACH_ENT_ID,ATTACH_FILE_NAME,ATTACH_DATE_TIME,ATTACH_USER_ID,ATTACH_FILE_DESC,      
   ATTACH_POLICY_ID,ATTACH_POLICY_VER_TRACKING_ID,ATTACH_GEN_FILE_NAME,ATTACH_FILE_TYPE,ATTACH_ENTITY_TYPE,      
   ATTACH_CUSTOMER_ID,ATTACH_APP_ID,ATTACH_APP_VER_ID,SOURCE_ATTACH_ID,IS_ACTIVE,ATTACH_TYPE    
         
   FROM #MNT_ATTACHMENT_LIST WHERE ROW_NUMBER_ATTACH=@ROW_NUMBER_ATTACH      
         
   SET @ATTACHMENT_ID_NEW=@ATTACHMENT_ID_NEW+1      
   SET @ROW_NUMBER_ATTACH=@ROW_NUMBER_ATTACH+1      
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
      
        IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
                                                                        
  END                                                                   
   DROP TABLE #MNT_ATTACHMENT_LIST                                                                         
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM         
       
--3. Major Participation        
          
SET @PARTICIPATION_ID_NEW = (SELECT MAX(isnull(PARTICIPATION_ID,0))+1 FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION (nolock) )         
--SET @MAJOR_PARTICIPATION_ID_OLD = (SELECT PARTICIPATION_ID FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION (nolock) WHERE CONTRACT_ID = @CONTRACT_ID )         
   SELECT (SELECT COUNT(*) FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION WHERE PARTICIPATION_ID<=A.PARTICIPATION_ID      
     AND a.CONTRACT_ID=CONTRACT_ID      
 ) AS ROW_NUMBER,      
 * INTO #MNT_REINSURANCE_MAJORMINOR_PARTICIPATION      
 FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION A      
 WHERE  CONTRACT_ID = @CONTRACT_ID                                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                           
      
    UPDATE #MNT_REINSURANCE_MAJORMINOR_PARTICIPATION SET CONTRACT_ID = @CONTRACT_ID_NEW,    
  CREATED_BY = @CREATED_BY,    
  CREATED_DATETIME = getDate();           
--Minor Update start      
SET @MINOR_PARTICIPATION_ID_NEW = (SELECT MAX(isnull(MINOR_PARTICIPATION_ID,0))+1 FROM MNT_REIN_MINOR_PARTICIPATION)      
SELECT (SELECT COUNT(*) FROM MNT_REIN_MINOR_PARTICIPATION WHERE MINOR_PARTICIPATION_ID<=A.MINOR_PARTICIPATION_ID      
 AND a.CONTRACT_ID=CONTRACT_ID) AS ROW_NUMBER_MINOR,      
 * INTO #MNT_REIN_MINOR_PARTICIPATION      
 FROM MNT_REIN_MINOR_PARTICIPATION A           
 WHERE  CONTRACT_ID = @CONTRACT_ID       
                                                              
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM          
      
 DECLARE  @ROW_NUMBER INT      
 SET    @ROW_NUMBER=1       
      
  WHILE 1=1      
 BEGIN      
      
 IF NOT EXISTS(SELECT * FROM #MNT_REINSURANCE_MAJORMINOR_PARTICIPATION WHERE ROW_NUMBER=@ROW_NUMBER )      
 BEGIN      
  BREAK      
 END      
        SELECT   @MAJOR_PARTICIPATION_ID_OLD = PARTICIPATION_ID FROM #MNT_REINSURANCE_MAJORMINOR_PARTICIPATION                                                        
   WHERE ROW_NUMBER=@ROW_NUMBER      
 INSERT INTO MNT_REINSURANCE_MAJORMINOR_PARTICIPATION (      
   PARTICIPATION_ID,CONTRACT_ID,PARTICIPATION_TYPE,NET_RETENTION,REINSURANCE_COMPANY,WHOLE_PERCENT,SIGNED_LINE_PERCENT,REINSURANCE_ACC_NUMBER,SEP_ACC,      
   IS_ACTIVE,LAYER,MINOR_PARTICIPANTS,REIN_COMPANY_ID,MAJOR_PARTICIPANTS,    
   CREATED_BY  ,    
   CREATED_DATETIME)      
         
   SELECT @PARTICIPATION_ID_NEW,CONTRACT_ID,PARTICIPATION_TYPE,NET_RETENTION,REINSURANCE_COMPANY,WHOLE_PERCENT,      
   SIGNED_LINE_PERCENT,REINSURANCE_ACC_NUMBER,SEP_ACC,IS_ACTIVE,LAYER,MINOR_PARTICIPANTS,REIN_COMPANY_ID,      
   MAJOR_PARTICIPANTS,    
   CREATED_BY  ,    
   CREATED_DATETIME      
   FROM #MNT_REINSURANCE_MAJORMINOR_PARTICIPATION WHERE ROW_NUMBER=@ROW_NUMBER      
      
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                          
       
 --UPDATING MINOR PARTICIPANTS'S MAJAR PART ID      
   UPDATE #MNT_REIN_MINOR_PARTICIPATION SET MAJOR_PARTICIPANTS =  @PARTICIPATION_ID_NEW,    
  CREATED_BY = @CREATED_BY,    
  CREATED_DATETIME = getDate()           
   WHERE MAJOR_PARTICIPANTS=@MAJOR_PARTICIPATION_ID_OLD      
      
   SET @PARTICIPATION_ID_NEW=@PARTICIPATION_ID_NEW+1      
   SET @ROW_NUMBER=@ROW_NUMBER+1      
       
END      
   DROP TABLE #MNT_REINSURANCE_MAJORMINOR_PARTICIPATION                                                                          
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM          
        
--4. Minor participation        
--         
-- SET @MINOR_PARTICIPATION_ID_NEW = (SELECT MAX(isnull(MINOR_PARTICIPATION_ID,0))+1 FROM MNT_REIN_MINOR_PARTICIPATION)      
-- SELECT (SELECT COUNT(*) FROM MNT_REIN_MINOR_PARTICIPATION WHERE MINOR_PARTICIPATION_ID<=A.MINOR_PARTICIPATION_ID      
--  AND a.CONTRACT_ID=CONTRACT_ID) AS ROW_NUMBER_MINOR,      
--  * INTO #MNT_REIN_MINOR_PARTICIPATION      
--  FROM MNT_REIN_MINOR_PARTICIPATION A           
--  WHERE  CONTRACT_ID = @CONTRACT_ID       
--                                                               
--  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
--   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM          
                                                                        
                                                                        
   UPDATE #MNT_REIN_MINOR_PARTICIPATION SET CONTRACT_ID = @CONTRACT_ID_NEW,        
 MINOR_PARTICIPATION_ID = @MINOR_PARTICIPATION_ID_NEW    
--,  CREATED_BY = @CREATED_BY,    
--  CREATED_DATETIME = getDate();            
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                           
                                                             
      
DECLARE  @ROW_NUMBER_MINOR INT      
 SET    @ROW_NUMBER_MINOR=1                                                                      
 WHILE 1=1      
 BEGIN      
      
 IF NOT EXISTS(SELECT * FROM #MNT_REIN_MINOR_PARTICIPATION WHERE ROW_NUMBER_MINOR=@ROW_NUMBER_MINOR )      
 BEGIN      
  BREAK      
 END                                                             
      
 INSERT INTO MNT_REIN_MINOR_PARTICIPATION (MINOR_PARTICIPATION_ID,      
   MINOR_LAYER,      
   MINOR_PARTICIPANTS,      
   MINOR_WHOLE_PERCENT,      
   MAJOR_PARTICIPANTS,      
   IS_ACTIVE,      
   CONTRACT_ID,    
   CREATED_BY  ,    
   CREATED_DATETIME      
   )      
         
   SELECT @MINOR_PARTICIPATION_ID_NEW,      
   MINOR_LAYER,      
   MINOR_PARTICIPANTS,      
   MINOR_WHOLE_PERCENT,      
   MAJOR_PARTICIPANTS,      
   IS_ACTIVE,      
   CONTRACT_ID,    
   CREATED_BY  ,    
   CREATED_DATETIME      
         
   FROM #MNT_REIN_MINOR_PARTICIPATION WHERE ROW_NUMBER_MINOR=@ROW_NUMBER_MINOR      
       
   SET @MINOR_PARTICIPATION_ID_NEW=@MINOR_PARTICIPATION_ID_NEW+1      
   SET @ROW_NUMBER_MINOR=@ROW_NUMBER_MINOR+1      
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                          
      
END                                                                                     
                                                                     
   DROP TABLE #MNT_REIN_MINOR_PARTICIPATION                          
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM          
--5. Premium Builder        
                
SET @PREMIUM_BUILDER_ID_NEW = (SELECT MAX(isnull(PREMIUM_BUILDER_ID,0))+1 FROM MNT_REIN_PREMIUM_BUILDER)         
--   SELECT * INTO #MNT_REIN_PREMIUM_BUILDER   FROM MNT_REIN_PREMIUM_BUILDER  (nolock)            
--    WHERE  CONTRACT_ID = @CONTRACT_ID                                                                         
--    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
--     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                           
                                                                        
SELECT (SELECT COUNT(*) FROM MNT_REIN_PREMIUM_BUILDER WHERE PREMIUM_BUILDER_ID<=A.PREMIUM_BUILDER_ID      
 AND a.CONTRACT_ID=CONTRACT_ID) AS ROW_NUMBER_PREM,      
 * INTO #MNT_REIN_PREMIUM_BUILDER      
 FROM MNT_REIN_PREMIUM_BUILDER A           
 WHERE  CONTRACT_ID = @CONTRACT_ID       
                                                              
 SELECT @TEMP_ERROR_CODE = @@ERROR                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM        
      
   UPDATE #MNT_REIN_PREMIUM_BUILDER SET CONTRACT_ID = @CONTRACT_ID_NEW,        
 PREMIUM_BUILDER_ID = @PREMIUM_BUILDER_ID_NEW,        
 EFFECTIVE_DATE = @EFFECTIVE_DATE_NEW,        
 EXPIRY_DATE = @EXPIRATION_DATE_NEW ,    
  CREATED_BY = @CREATED_BY,    
  CREATED_DATETIME = getDate();           
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                     
                                                             
--    INSERT INTO MNT_REIN_PREMIUM_BUILDER SELECT * FROM #MNT_REIN_PREMIUM_BUILDER (nolock)                                                                         
--    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
--     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                          
                                                                     
DECLARE  @ROW_NUMBER_PREM INT      
 SET    @ROW_NUMBER_PREM=1                                                                      
 WHILE 1=1      
 BEGIN      
-- SELECT @LOSS_LAYER_ID_NEW      
 IF NOT EXISTS(SELECT * FROM #MNT_REIN_PREMIUM_BUILDER WHERE ROW_NUMBER_PREM=@ROW_NUMBER_PREM )      
 BEGIN      
  BREAK      
 END                                                             
      
 INSERT INTO MNT_REIN_PREMIUM_BUILDER (PREMIUM_BUILDER_ID,      
   CONTRACT_ID,      
   CONTRACT,      
   EFFECTIVE_DATE,      
   EXPIRY_DATE,      
   LAYER,      
   COVERAGE_CATEGORY,      
   CALCULATION_BASE,      
   INSURANCE_VALUE,      
   TOTAL_INSURANCE_FROM,      
   TOTAL_INSURANCE_TO,      
   OTHER_INST,      
   RATE_APPLIED,      
   CONSTRUCTION,      
   PROTECTION,      
   ALARM_CREDIT,      
   ALARM_PERCENTAGE,      
   HOME_CREDIT,      
   HOME_AGE,      
   COMMENTS,
   HOME_PERCENTAGE,      
   IS_ACTIVE,    
   CREATED_BY  ,    
   CREATED_DATETIME)      
         
   SELECT @PREMIUM_BUILDER_ID_NEW,      
   CONTRACT_ID,      
   CONTRACT,      
   EFFECTIVE_DATE,      
   EXPIRY_DATE,      
   LAYER,      
   COVERAGE_CATEGORY,      
   CALCULATION_BASE,      
   INSURANCE_VALUE,      
   TOTAL_INSURANCE_FROM,      
   TOTAL_INSURANCE_TO,      
   OTHER_INST,      
   RATE_APPLIED,      
   CONSTRUCTION,      
   PROTECTION,      
   ALARM_CREDIT,      
   ALARM_PERCENTAGE,      
   HOME_CREDIT,      
   HOME_AGE,      
   COMMENTS,
   HOME_PERCENTAGE,      
   IS_ACTIVE,    
   CREATED_BY  ,    
   CREATED_DATETIME    
         
   FROM #MNT_REIN_PREMIUM_BUILDER WHERE ROW_NUMBER_PREM=@ROW_NUMBER_PREM       
       
   SET @PREMIUM_BUILDER_ID_NEW=@PREMIUM_BUILDER_ID_NEW+1      
   SET @ROW_NUMBER_PREM=@ROW_NUMBER_PREM+1      
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                          
      
END                                
      
   DROP TABLE #MNT_REIN_PREMIUM_BUILDER                                                                         
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                  
--6. Loss Layer        
             
SET @LOSS_LAYER_ID_NEW = (SELECT MAX(isnull(LOSS_LAYER_ID,0))+1 FROM MNT_REIN_LOSSLAYER)       
      
SELECT (SELECT COUNT(*) FROM MNT_REIN_LOSSLAYER WHERE LOSS_LAYER_ID<=A.LOSS_LAYER_ID      
 AND a.CONTRACT_ID=CONTRACT_ID) AS ROW_NUMBER_LOSS,      
 * INTO #MNT_REIN_LOSSLAYER      
 FROM MNT_REIN_LOSSLAYER A           
 WHERE  CONTRACT_ID = @CONTRACT_ID       
                                                              
 SELECT @TEMP_ERROR_CODE = @@ERROR            
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM          
                                                                        
 UPDATE #MNT_REIN_LOSSLAYER SET CONTRACT_ID = @CONTRACT_ID_NEW,    
  CREATED_BY = @CREATED_BY,    
  CREATED_DATETIME = getDate();            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                           
                                                             
   DECLARE  @ROW_NUMBER_LOSS INT      
 SET    @ROW_NUMBER_LOSS=1                                                                      
 WHILE 1=1      
 BEGIN      
-- SELECT @LOSS_LAYER_ID_NEW      
 IF NOT EXISTS(SELECT * FROM #MNT_REIN_LOSSLAYER WHERE ROW_NUMBER_LOSS=@ROW_NUMBER_LOSS )      
 BEGIN      
  BREAK      
 END                                                             
      
 INSERT INTO MNT_REIN_LOSSLAYER (LOSS_LAYER_ID,CONTRACT_ID,      
   LAYER,COMPANY_RETENTION,LAYER_AMOUNT,RETENTION_AMOUNT,RETENTION_PERCENTAGE,      
   REIN_CEDED,IS_ACTIVE,REIN_CEDED_PERCENTAGE,    
   CREATED_BY  ,    
   CREATED_DATETIME)      
         
   SELECT @LOSS_LAYER_ID_NEW,CONTRACT_ID,LAYER,COMPANY_RETENTION,      
   LAYER_AMOUNT,RETENTION_AMOUNT,RETENTION_PERCENTAGE,      
   REIN_CEDED,IS_ACTIVE,REIN_CEDED_PERCENTAGE,    
   CREATED_BY  ,    
   CREATED_DATETIME      
         
   FROM #MNT_REIN_LOSSLAYER WHERE ROW_NUMBER_LOSS=@ROW_NUMBER_LOSS      
       
   SET @LOSS_LAYER_ID_NEW=@LOSS_LAYER_ID_NEW+1      
   SET @ROW_NUMBER_LOSS=@ROW_NUMBER_LOSS+1      
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                          
      
END                                                                       
                                                                     
   DROP TABLE #MNT_REIN_LOSSLAYER                                                                         
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM           
--COMMIT TRAN        
PROBLEM:                                                                                                
  --ROLLBACK TRAN        
                                                                                              
 RETURN @CONTRACT_ID_NEW         
END        
      
      
  




GO

