IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CopyAppPol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CopyAppPol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                                                                      
Proc Name       : dbo.Proc_CopyAppPol                                                                   
Created by      : Pradeep Kushwaha                                                           
Date            : 21/June/2010      
Purpose         : Create New Version of Application                                                         
        
---------------------------------------------------------- */        
/*    
Declare @PolId int      
Declare @AppID int    
exec Proc_CopyAppPol 2154,41,1,'889982010210115000028',396,@PolId out,@AppID out    
select @PolId    
select @AppID    
  
select * from pol_clauses where customer_id = 2154 and policy_id = 10  
select * from pol_clauses order by pol_clause_id desc  
*/    
---select  *  FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  where CUSTOMER_ID=2126                                                   
--drop proc Proc_CopyAppPol 2126,97,1,'889982010210196000066',396,    
    
CREATE proc  [dbo].[Proc_CopyAppPol]                                                   
(                                                                            
 @CUSTOMER_ID INT,                                                                                  
 @OLD_POL_ID  INT,                               
 @OLD_POL_VERSION_ID SMALLINT,                                                                         
 @NEW_APP_POL_NUMBER NVARCHAR(75),    
 @CREATED_BY INT,    
 @POL_NEW_ID INT OUTPUT,       
 @APP_NEW_ID INT OUTPUT       
)          
AS                                                    
BEGIN                                   
                                                                        
 BEGIN TRAN   TR  --Set begin transction   
     
   DECLARE @TEMP_ERROR_CODE INT,     
   @DATE NVARCHAR(50),      
   @APP_POL_NEW_VERSION SMALLINT ,  
   @POLICY_LOB_ID INT,     
   @MAXCLAUSE_ID INT    
    
       
 SET @APP_POL_NEW_VERSION = 1 --Default App policy version id is 1  
       
    -- Get Current date and time to update for Created_Datetime field                                                                                       
 SET @DATE = CONVERT(VARCHAR(50),GETDATE(),100)                                                               
     
 -- Get NEW App Id and Pol Id                                                                                                                                
    SELECT @APP_NEW_ID = ISNULL(MAX(APP_ID),0)+1, @POL_NEW_ID = ISNULL(MAX(POLICY_ID),0)+1 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID   
   
   
    
    --Get the Risk Id ,which is associated to this paticular Customer ,Policy and policy version id  
    SELECT  @POLICY_LOB_ID = POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST with(nolock)   WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@OLD_POL_ID and POLICY_VERSION_ID=@OLD_POL_VERSION_ID    
    
   --Get the Customer Application/Policy Data for particular customet , policy and policy version id   
 SELECT * INTO #POL_CUSTOMER_POLICY_LIST   FROM POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@OLD_POL_ID and POLICY_VERSION_ID=@OLD_POL_VERSION_ID                                            
      
----Update the  temp table #POL_CUSTOMER_POLICY_LIST                
     
 UPDATE #POL_CUSTOMER_POLICY_LIST SET APP_VERSION_ID = @APP_POL_NEW_VERSION, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,     
 POLICY_DISP_VERSION = cast(@APP_POL_NEW_VERSION as varchar)+'.0', APP_VERSION = cast(@APP_POL_NEW_VERSION as varchar)+'.0',    
 CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE, APP_ID = @APP_NEW_ID, POLICY_ID = @POL_NEW_ID,    
 APP_NUMBER = @NEW_APP_POL_NUMBER, APP_STATUS = 'APPLICATION'  , POLICY_STATUS = NULL , IS_ACTIVE = 'Y',    
 POLICY_NUMBER = NULL, APP_VERIFICATION_XML =NULL, RULE_INPUT_XML = NULL, CURRENT_TERM = 1        
                                                                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                         
     
 ---Insert Updated temp table into POL_CUSTOMER_POLICY_LIST ,Basically it create new application from old Application/Policy  
                                                          
 INSERT INTO POL_CUSTOMER_POLICY_LIST SELECT * FROM #POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                              
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                        
       
    --Drop temp table                                                               
 DROP TABLE #POL_CUSTOMER_POLICY_LIST                                                                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                              
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                        
  
 --Copy the related appliction/policy table data   
   
--1.Copy Co-Applicant Data     
  
 SELECT * INTO #POL_APPLICANT_LIST FROM POL_APPLICANT_LIST  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID                                                   
 SELECT @TEMP_ERROR_CODE = @@ERROR                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                   
 UPDATE #POL_APPLICANT_LIST SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                               
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                   
 INSERT INTO POL_APPLICANT_LIST SELECT * FROM #POL_APPLICANT_LIST                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM     
           
 DROP TABLE #POL_APPLICANT_LIST                                
 SELECT @TEMP_ERROR_CODE = @@ERROR     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
  
--2.-Copy Pol Location Data    
  
 SELECT * INTO #POL_LOCATIONS_POL FROM POL_LOCATIONS  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID   
 SELECT @TEMP_ERROR_CODE = @@ERROR                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
             
 UPDATE #POL_LOCATIONS_POL SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE          
 SELECT @TEMP_ERROR_CODE = @@ERROR                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                 
 INSERT INTO POL_LOCATIONS SELECT * FROM #POL_LOCATIONS_POL                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
         
 DROP TABLE #POL_LOCATIONS_POL              
 SELECT @TEMP_ERROR_CODE = @@ERROR                                           
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
  
--3.--Copy Co-Insurance Table    
  
 SELECT * INTO #POL_CO_INSURANCE FROM POL_CO_INSURANCE  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID                                                 
 SELECT @TEMP_ERROR_CODE = @@ERROR                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
             
 UPDATE #POL_CO_INSURANCE SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE              
 SELECT @TEMP_ERROR_CODE = @@ERROR                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                 
 INSERT INTO POL_CO_INSURANCE                                 
 SELECT * FROM #POL_CO_INSURANCE                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
         
 DROP TABLE #POL_CO_INSURANCE              
 SELECT @TEMP_ERROR_CODE = @@ERROR                                           
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
  
--4.-Copy Remuneration Table   
  
 SELECT * INTO #POL_REMUNERATION FROM POL_REMUNERATION  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID                                                 
 SELECT @TEMP_ERROR_CODE = @@ERROR                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
             
 UPDATE #POL_REMUNERATION SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE              
 SELECT @TEMP_ERROR_CODE = @@ERROR                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                 
 INSERT INTO POL_REMUNERATION                                 
 SELECT * FROM #POL_REMUNERATION                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
         
 DROP TABLE #POL_REMUNERATION              
 SELECT @TEMP_ERROR_CODE = @@ERROR                                           
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM   
  
--5-Copy Policy Clauses Table    
  
 SELECT @MAXCLAUSE_ID= ISNULL(MAX(POL_CLAUSE_ID),0) FROM POL_CLAUSES WITH(NOLOCK)    
     
 SELECT POL_CLAUSE_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAUSE_ID,CLAUSE_TITLE,CLAUSE_DESCRIPTION,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,SUSEP_LOB_ID
 ,CLAUSE_CODE ,ATTACH_FILE_NAME,CLAUSE_TYPE   
    ---itrack # 1344,clause code not copied from cancelled version
   INTO #POL_CLAUSES FROM POL_CLAUSES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID                                             
 SELECT @TEMP_ERROR_CODE = @@ERROR                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
             
 UPDATE #POL_CLAUSES SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE            
 SELECT @TEMP_ERROR_CODE = @@ERROR                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
     
 INSERT INTO POL_CLAUSES(POL_CLAUSE_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAUSE_ID,CLAUSE_TITLE,CLAUSE_DESCRIPTION,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,SUSEP_LOB_ID
 ,CLAUSE_CODE,ATTACH_FILE_NAME,CLAUSE_TYPE   )
 SELECT     
 @MAXCLAUSE_ID + ROW_NUMBER() over(order by CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID),    
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAUSE_ID,CLAUSE_TITLE,CLAUSE_DESCRIPTION,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,NULL,NULL,SUSEP_LOB_ID 
 ,CLAUSE_CODE,ATTACH_FILE_NAME,CLAUSE_TYPE   
  FROM #POL_CLAUSES                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
         
 DROP TABLE #POL_CLAUSES              
 SELECT @TEMP_ERROR_CODE = @@ERROR                                           
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM   
  
--6-Copy Policy Reinsurance Table    
  
 SELECT * INTO #POL_REINSURANCE_INFO FROM POL_REINSURANCE_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID                                                 
 SELECT @TEMP_ERROR_CODE = @@ERROR                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
             
 UPDATE #POL_REINSURANCE_INFO SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE              
 SELECT @TEMP_ERROR_CODE = @@ERROR          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                 
 INSERT INTO POL_REINSURANCE_INFO                                 
 SELECT * FROM #POL_REINSURANCE_INFO                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
         
 DROP TABLE #POL_REINSURANCE_INFO              
 SELECT @TEMP_ERROR_CODE = @@ERROR                                           
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM     
  
--7-Copy Policy Discount/Surcharge Table           
  
 SELECT * INTO #POL_DISCOUNT_SURCHARGE FROM POL_DISCOUNT_SURCHARGE WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
             
 UPDATE #POL_DISCOUNT_SURCHARGE SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE              
 SELECT @TEMP_ERROR_CODE = @@ERROR                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                 
 INSERT INTO POL_DISCOUNT_SURCHARGE                                 
 SELECT * FROM #POL_DISCOUNT_SURCHARGE                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
         
 DROP TABLE #POL_DISCOUNT_SURCHARGE              
 SELECT @TEMP_ERROR_CODE = @@ERROR                                           
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM    
  
--copying protective devices   
  
  SELECT * INTO #POL_PROTECTIVE_DEVICES FROM POL_PROTECTIVE_DEVICES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID    
  SELECT @TEMP_ERROR_CODE = @@ERROR                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
              
  UPDATE #POL_PROTECTIVE_DEVICES SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                
  SELECT @TEMP_ERROR_CODE = @@ERROR                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                  
  INSERT INTO POL_PROTECTIVE_DEVICES                                 
  SELECT * FROM #POL_PROTECTIVE_DEVICES                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
          
  DROP TABLE #POL_PROTECTIVE_DEVICES              
  SELECT @TEMP_ERROR_CODE = @@ERROR                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
  
----Pol Product Coverages  
  
  SELECT * INTO #POL_PRODUCT_COVERAGES  FROM POL_PRODUCT_COVERAGES WHERE   
    CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID    
                   
   SELECT @TEMP_ERROR_CODE = @@ERROR                 
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
                   
   UPDATE #POL_PRODUCT_COVERAGES SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE           
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                   
                
   INSERT INTO POL_PRODUCT_COVERAGES SELECT * FROM #POL_PRODUCT_COVERAGES                     
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
                   
   DROP TABLE #POL_PRODUCT_COVERAGES       
   SELECT @TEMP_ERROR_CODE = @@ERROR            
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM  
-----Copying Risk level Data    
  
-- All Risk and Named Perils Product  
 IF(@POLICY_LOB_ID in (9,26))  
 BEGIN  
   SELECT * INTO #POL_PERILS FROM POL_PERILS WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID                                                                     
   SELECT @TEMP_ERROR_CODE = @@ERROR                 
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
               
   UPDATE #POL_PERILS SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE      
   SELECT @TEMP_ERROR_CODE = @@ERROR                         
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                   
   INSERT INTO POL_PERILS                                 
   SELECT * FROM #POL_PERILS                                
   SELECT @TEMP_ERROR_CODE = @@ERROR                                   
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
           
   DROP TABLE #POL_PERILS              
   SELECT @TEMP_ERROR_CODE = @@ERROR                                           
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM     
 END  
  
----Comprehensive Condominium (10),Comprehensive Company(11),Diversified Risks(14),Robbery(16),Dwelling(19) and General Civil Liability(12)  
   
 ELSE IF( @POLICY_LOB_ID IN(10,11,14,16,19,12,25,27,32))  
    BEGIN  
   SELECT * INTO #POL_PRODUCT_LOCATION_INFO FROM POL_PRODUCT_LOCATION_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID      
   SELECT @TEMP_ERROR_CODE = @@ERROR                 
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
               
   UPDATE #POL_PRODUCT_LOCATION_INFO SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE             
   SELECT @TEMP_ERROR_CODE = @@ERROR                         
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                   
   INSERT INTO POL_PRODUCT_LOCATION_INFO                                 
   SELECT * FROM #POL_PRODUCT_LOCATION_INFO                                
   SELECT @TEMP_ERROR_CODE = @@ERROR                                   
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
           
   DROP TABLE #POL_PRODUCT_LOCATION_INFO              
   SELECT @TEMP_ERROR_CODE = @@ERROR                                           
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
    END   
     
 --Facultative Liability(17)-,-Civil Liability Transportation(18)    
 ELSE IF (@POLICY_LOB_ID IN (17,18,28,29,30,31,36))      
 BEGIN       
  SELECT * INTO #POL_CIVIL_TRANSPORT_VEHICLES FROM POL_CIVIL_TRANSPORT_VEHICLES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID                                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
              
  UPDATE #POL_CIVIL_TRANSPORT_VEHICLES SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE             
  SELECT @TEMP_ERROR_CODE = @@ERROR                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                  
  INSERT INTO POL_CIVIL_TRANSPORT_VEHICLES                                 
  SELECT * FROM #POL_CIVIL_TRANSPORT_VEHICLES                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
          
  DROP TABLE #POL_CIVIL_TRANSPORT_VEHICLES              
  SELECT @TEMP_ERROR_CODE = @@ERROR                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
 END    
   
-- MeriTIme (13)    
  
 ELSE IF (@POLICY_LOB_ID = 13)      
 BEGIN       
  SELECT * INTO #POL_MARITIME FROM POL_MARITIME WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID                            
  SELECT @TEMP_ERROR_CODE = @@ERROR                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
              
  UPDATE #POL_MARITIME SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE             
  SELECT @TEMP_ERROR_CODE = @@ERROR                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                  
  INSERT INTO POL_MARITIME                                 
  SELECT * FROM #POL_MARITIME                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
          
  DROP TABLE #POL_MARITIME              
  SELECT @TEMP_ERROR_CODE = @@ERROR                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
 END     
-- National(20) and International(23) Cargo Transport    
 ELSE IF (@POLICY_LOB_ID in(20,23))      
 BEGIN       
  SELECT * INTO #POL_COMMODITY_INFO FROM POL_COMMODITY_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID                                                   
  SELECT @TEMP_ERROR_CODE = @@ERROR                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
              
  UPDATE #POL_COMMODITY_INFO SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE              
  SELECT @TEMP_ERROR_CODE = @@ERROR                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                  
  INSERT INTO POL_COMMODITY_INFO                                 
  SELECT * FROM #POL_COMMODITY_INFO                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
          
  DROP TABLE #POL_COMMODITY_INFO              
  SELECT @TEMP_ERROR_CODE = @@ERROR                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
 END  
--  Individual Personal Accident (15) , Group Passenger Personal Accident (21)     
 ELSE IF (@POLICY_LOB_ID in (15,21,33,34))      
 BEGIN      
  SELECT * INTO #POL_PERSONAL_ACCIDENT_INFO FROM POL_PERSONAL_ACCIDENT_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID       
  SELECT @TEMP_ERROR_CODE = @@ERROR                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
              
  UPDATE #POL_PERSONAL_ACCIDENT_INFO SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE             
  SELECT @TEMP_ERROR_CODE = @@ERROR                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                  
  INSERT INTO POL_PERSONAL_ACCIDENT_INFO                                 
  SELECT * FROM #POL_PERSONAL_ACCIDENT_INFO                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
          
  DROP TABLE #POL_PERSONAL_ACCIDENT_INFO              
  SELECT @TEMP_ERROR_CODE = @@ERROR        
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
 END   
-- Passenger Personal Accident(22)    
 ELSE IF (@POLICY_LOB_ID = 22)      
 BEGIN      
  SELECT * INTO #POL_PASSENGERS_PERSONAL_ACCIDENT_INFO FROM POL_PASSENGERS_PERSONAL_ACCIDENT_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
              
  UPDATE #POL_PASSENGERS_PERSONAL_ACCIDENT_INFO SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                
  SELECT @TEMP_ERROR_CODE = @@ERROR                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                  
  INSERT INTO POL_PASSENGERS_PERSONAL_ACCIDENT_INFO                                 
  SELECT * FROM #POL_PASSENGERS_PERSONAL_ACCIDENT_INFO                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
          
  DROP TABLE #POL_PASSENGERS_PERSONAL_ACCIDENT_INFO    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
 END  
  ELSE IF (@POLICY_LOB_ID in (35,37))      
 BEGIN      
  SELECT * INTO #POL_PENHOR_RURAL_INFO FROM POL_PENHOR_RURAL_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@OLD_POL_ID AND POLICY_VERSION_ID=@OLD_POL_VERSION_ID                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
              
  UPDATE #POL_PENHOR_RURAL_INFO SET POLICY_ID=@POL_NEW_ID, POLICY_VERSION_ID = @APP_POL_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                
  SELECT @TEMP_ERROR_CODE = @@ERROR                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                  
  INSERT INTO POL_PENHOR_RURAL_INFO                                 
  SELECT * FROM #POL_PENHOR_RURAL_INFO                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
          
  DROP TABLE #POL_PENHOR_RURAL_INFO    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
 END     
    
 COMMIT TRAN   TR                                                                                                                                                                                                                                             
 
    RETURN      
----If there is any error then rollback the transcation                                             
  PROBLEM:         
  BEGIN    
  SET @POL_NEW_ID = -1     
  --IF @@TRANCOUNT > 0     
  ROLLBACK TRAN TR                                                                              
  END     
     
END                    


GO

