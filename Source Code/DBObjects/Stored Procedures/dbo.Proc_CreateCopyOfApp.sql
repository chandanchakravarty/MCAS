IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CreateCopyOfApp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CreateCopyOfApp]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                  
Proc Name       : dbo.Proc_CreateCopyOfApp    
Created by      : Ravindra Gupta    
Date            : 06-22-2007    
Purpose         :     
Revison History :  
modified by		: Pravesh
date			: 28 may 2008
purpose		    : Copy Credid info
Used In      : Wolverine                                         
                                      
            
---------------------------------------------------------- */                                                        
-- drop proc   dbo.Proc_CreateCopyOfApp    
CREATE proc  [dbo].[Proc_CreateCopyOfApp]                                             
(                                                                        
 @CUSTOMER_ID int,                                                                              
 @APP_ID  int,                                                                              
 @APP_VERSION_ID smallint,                                                                              
 @CREATED_BY int ,    
 @APP_NUMBER Varchar(20) Out    
)                                                                              
AS                                                                              
BEGIN                               
BEGIN TRAN                                                                
 DECLARE @TEMP_ERROR_CODE INT                                              
    
 Declare @Date varchar(50)                             
 set @Date = convert(varchar(50),getdate(),100)                                                                                                                                    
 Declare @RISKID int ,    
  @AGENCY_ID Int                                                                             
     
 SELECT @RISKID= APP_LOB , @AGENCY_ID = APP_AGENCY_ID    
 FROM APP_LIST     
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID    
    
 --Generate new APP NUMBER    
 CREATE TABLE #APP_NUM ( APP_NUM VARCHAR(20))    
  INSERT INTO #APP_NUM  EXEC Proc_GenerateAppNumber @RISKID,@AGENCY_ID                                                           
 SELECT @APP_NUMBER = APP_NUM FROM #APP_NUM     
 DROP TABLE #APP_NUM     
 -- Get New App ID    
    
 DECLARE @NEW_APP_ID int ,    
  @APP_NEW_VERSION Int    
 SET @APP_NEW_VERSION = 1                                                            
 SELECT  @NEW_APP_ID  = MAX(APP_ID) + 1 FROM APP_LIST      
 WHERE CUSTOMER_ID=@CUSTOMER_ID       
                          
                         
                      
 /* COPY THE COMMON DATA */                      
                                                       
 --1.                                              
 SELECT * INTO #APP_LIST   FROM APP_LIST  (nolock)      
 WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                                                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                     
                                           
 UPDATE #APP_LIST SET APP_VERSION_ID = @APP_NEW_VERSION, APP_ID = @NEW_APP_ID,    
  APP_NUMBER = @APP_NUMBER,    
  APP_VERSION = cast(@APP_NEW_VERSION as varchar)+'.0',CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE, APP_STATUS = 'Incomplete'                                                                    
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
                                                           
 UPDATE #APP_APPLICANT_LIST SET APP_VERSION_ID = @APP_NEW_VERSION,    
 APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                             
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                            
                                                        
 INSERT INTO APP_APPLICANT_LIST SELECT * FROM #APP_APPLICANT_LIST  (nolock)                                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                         
                                                        
 DROP TABLE #APP_APPLICANT_LIST                                                                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM  



 --addded by Pravesh on 28 may 2008          
 --3. Coping Billing Info            
SELECT * INTO #ACT_APP_EFT_CUST_INFO FROM ACT_APP_EFT_CUST_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                           
SELECT @TEMP_ERROR_CODE = @@ERROR             
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                     
        
UPDATE #ACT_APP_EFT_CUST_INFO SET  APP_VERSION_ID = @APP_NEW_VERSION,APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE          
SELECT @TEMP_ERROR_CODE = @@ERROR                     
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
            
INSERT INTO ACT_APP_EFT_CUST_INFO                             
SELECT * FROM #ACT_APP_EFT_CUST_INFO                            
SELECT @TEMP_ERROR_CODE = @@ERROR                               
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM          
    
DROP TABLE #ACT_APP_EFT_CUST_INFO          
SELECT @TEMP_ERROR_CODE = @@ERROR                                       
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
      
--4. Coping Cradit Card Billing Info            
CREATE TABLE #ACT_APP_CREDIT_CARD_DETAILS       
(      
IDEN_ROW_ID   int IDENTITY NOT NULL ,      
APP_ID   int,      
APP_VERSION_ID smallint,      
CUSTOMER_ID  int,      
PAY_PAL_REF_ID	Varchar(200) ,
LAST_UPDATED_DATETIME Datetime ,
CREATED_DATETIME	Datetime
)      
      
INSERT INTO #ACT_APP_CREDIT_CARD_DETAILS (      
APP_ID,APP_VERSION_ID,CUSTOMER_ID,PAY_PAL_REF_ID ,LAST_UPDATED_DATETIME,CREATED_DATETIME)      
      
SELECT APP_ID,APP_VERSION_ID,CUSTOMER_ID,PAY_PAL_REF_ID,LAST_UPDATED_DATETIME,CREATED_DATETIME     
FROM ACT_APP_CREDIT_CARD_DETAILS        
WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                             
      
SELECT @TEMP_ERROR_CODE = @@ERROR             
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                     
              
UPDATE #ACT_APP_CREDIT_CARD_DETAILS SET APP_VERSION_ID = @APP_NEW_VERSION,APP_ID = @NEW_APP_ID   ,
LAST_UPDATED_DATETIME = GETDATE() , CREATED_DATETIME  = GETDATE()                                  
SELECT @TEMP_ERROR_CODE = @@ERROR                     
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
              
      
INSERT INTO ACT_APP_CREDIT_CARD_DETAILS (      
APP_ID,APP_VERSION_ID,CUSTOMER_ID,PAY_PAL_REF_ID ,LAST_UPDATED_DATETIME,CREATED_DATETIME)        
SELECT APP_ID,APP_VERSION_ID,CUSTOMER_ID,PAY_PAL_REF_ID ,LAST_UPDATED_DATETIME,CREATED_DATETIME    
FROM #ACT_APP_CREDIT_CARD_DETAILS                
      
SELECT @TEMP_ERROR_CODE = @@ERROR                               
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                   
              
DROP TABLE #ACT_APP_CREDIT_CARD_DETAILS                          
SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM

                                                 
                      
 /* COPY THE LOB SPECIFIC DATA                                                                                
 
     
     
 RISKIDs                                                                              
 1.  Homeowners                                                                              
 2.  Private Passenger                                                                              
 3.  Motorcycle                                                                              
 4.  Watercraft                                                                       
 5.  Umbrella                                                                              
 6.  Rental Dwelling                                                                              
 7.  General Liability                                
 */                                                                            
                                                                        
 -- Private Passenger or Motorcycle                                                      
 IF  (@RISKID = 2 OR @RISKID =3)                                 
 BEGIN                                                                    
  --1.                                                                    
  SELECT * INTO #APP_VEHICLES FROM APP_VEHICLES  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
      
  UPDATE #APP_VEHICLES SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                       
      
  INSERT INTO APP_VEHICLES SELECT * FROM #APP_VEHICLES (nolock)                                                                   
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                     
      
  DROP TABLE #APP_VEHICLES                                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                     
      
     
  --2.                                                                    
  SELECT * INTO #APP_VEHICLE_COVERAGES FROM APP_VEHICLE_COVERAGES AVC  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                         
  AND VEHICLE_ID IN (SELECT VEHICLE_ID FROM APP_VEHICLES WHERE   CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                      
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_VEHICLE_COVERAGES SET APP_VERSION_ID = @APP_NEW_VERSION ,  APP_ID = @NEW_APP_ID    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
      
  INSERT INTO APP_VEHICLE_COVERAGES               
  SELECT * FROM #APP_VEHICLE_COVERAGES   (nolock)                                            
      
     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_VEHICLE_COVERAGES                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  --3.                                                                    
  SELECT * INTO #APP_VEHICLE_ENDORSEMENTS FROM APP_VEHICLE_ENDORSEMENTS  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                       
  AND VEHICLE_ID IN (SELECT VEHICLE_ID FROM APP_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                       
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
      
  UPDATE #APP_VEHICLE_ENDORSEMENTS SET APP_VERSION_ID = @APP_NEW_VERSION ,APP_ID = @NEW_APP_ID                   
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
      
  INSERT INTO APP_VEHICLE_ENDORSEMENTS SELECT * FROM #APP_VEHICLE_ENDORSEMENTS (nolock)       
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM          
      
  DROP TABLE #APP_VEHICLE_ENDORSEMENTS                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
     
  --4.                                                   
  SELECT * INTO #APP_ADD_OTHER_INT FROM APP_ADD_OTHER_INT  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and isnull(IS_ACTIVE,'Y')='Y'                                                                  
  AND VEHICLE_ID IN (SELECT VEHICLE_ID FROM APP_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                       
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_ADD_OTHER_INT SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_ADD_OTHER_INT SELECT * FROM #APP_ADD_OTHER_INT  (nolock)                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_ADD_OTHER_INT                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  --5.                                                                    
  SELECT * INTO #APP_DRIVER_DETAILS FROM APP_DRIVER_DETAILS  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and isnull(IS_ACTIVE,'Y')='Y'                                                                  
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_DRIVER_DETAILS SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_DRIVER_DETAILS SELECT * FROM #APP_DRIVER_DETAILS   (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_DRIVER_DETAILS                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
 
  --6.                                                                    
  SELECT * INTO #APP_MVR_INFORMATION FROM APP_MVR_INFORMATION  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and isnull(IS_ACTIVE,'Y')='Y'                                                                   
  AND DRIVER_ID IN (SELECT DRIVER_ID FROM APP_DRIVER_DETAILS (nolock)      
  WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                       
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_MVR_INFORMATION SET APP_VERSION_ID = @APP_NEW_VERSION,APP_ID = @NEW_APP_ID    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_MVR_INFORMATION SELECT * FROM #APP_MVR_INFORMATION  (nolock)                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
  DROP TABLE #APP_MVR_INFORMATION                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                  
     
  --7.                                                                    
  SELECT * INTO #APP_AUTO_GEN_INFO FROM APP_AUTO_GEN_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and isnull(IS_ACTIVE,'Y')='Y'                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                               
      
  UPDATE #APP_AUTO_GEN_INFO SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_AUTO_GEN_INFO SELECT * FROM #APP_AUTO_GEN_INFO  (nolock)                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_AUTO_GEN_INFO                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                   
      
   --8.                                                                    
  SELECT * INTO #APP_DRIVER_ASSIGNED_VEHICLE FROM APP_DRIVER_ASSIGNED_VEHICLE (nolock)      
WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID          
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                               
      
  UPDATE #APP_DRIVER_ASSIGNED_VEHICLE  SET APP_VERSION_ID = @APP_NEW_VERSION   ,APP_ID = @NEW_APP_ID     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_DRIVER_ASSIGNED_VEHICLE  SELECT * FROM #APP_DRIVER_ASSIGNED_VEHICLE  (nolock)   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
      
  DROP TABLE #APP_DRIVER_ASSIGNED_VEHICLE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM          
      
      
  IF (@RISKID = 2)  --Insert data at APP_MISCELLANEOUS_EQUIPMENT_VALUES for Automobile                
  begin                 
      
   SELECT * INTO #APP_MISCELLANEOUS_EQUIPMENT_VALUES FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES  (nolock)      
   WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                                    
       
      
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                     
       
   UPDATE #APP_MISCELLANEOUS_EQUIPMENT_VALUES SET APP_VERSION_ID = @APP_NEW_VERSION,    
   APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                     
       
   INSERT INTO APP_MISCELLANEOUS_EQUIPMENT_VALUES SELECT * FROM #APP_MISCELLANEOUS_EQUIPMENT_VALUES                                                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                     
       
   DROP TABLE #APP_MISCELLANEOUS_EQUIPMENT_VALUES                                                      
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
  end                                                                 
 END                          
                                                         
                                                                  
 -- Homeowners or Rental Dwelling                                                                     
 IF  (@RISKID = 1 OR @RISKID =6)          
 BEGIN                                                                    
      
      
  --1.                                                                    
  SELECT * INTO #APP_LOCATIONS FROM APP_LOCATIONS  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                        
  SELECT @TEMP_ERROR_CODE = @@ERROR    
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                                           
  UPDATE #APP_LOCATIONS SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                       
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_LOCATIONS SELECT * FROM #APP_LOCATIONS (nolock)                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                    
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                   
      
  DROP TABLE #APP_LOCATIONS                                                                   
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                            
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
    
  --2.                                                                    
  SELECT * INTO #APP_DWELLINGS_INFO FROM APP_DWELLINGS_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and isnull(IS_ACTIVE,'Y')='Y'      
  AND LOCATION_ID IN (SELECT LOCATION_ID FROM APP_LOCATIONS (nolock) WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                      
      
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_DWELLINGS_INFO SET APP_VERSION_ID = @APP_NEW_VERSION ,APP_ID = @NEW_APP_ID                                                                   
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                            
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                   
      
  INSERT INTO APP_DWELLINGS_INFO SELECT * FROM #APP_DWELLINGS_INFO  (nolock)                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_DWELLINGS_INFO                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                          
   -- APP_OTHER_STRUCTURE_DWELLING                             
                       
  SELECT * INTO #APP_OTHER_STRUCTURE_DWELLING FROM APP_OTHER_STRUCTURE_DWELLING  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and IS_ACTIVE='Y'                                                                  
  AND DWELLING_ID IN (SELECT DWELLING_ID FROM APP_DWELLINGS_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                       
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                   
      
  UPDATE #APP_OTHER_STRUCTURE_DWELLING SET APP_VERSION_ID = @APP_NEW_VERSION ,APP_ID = @NEW_APP_ID                                        
  SELECT @TEMP_ERROR_CODE = @@ERROR                                              
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                
      
  INSERT INTO APP_OTHER_STRUCTURE_DWELLING SELECT * FROM #APP_OTHER_STRUCTURE_DWELLING (nolock)                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
      
  DROP TABLE #APP_OTHER_STRUCTURE_DWELLING                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                               
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                 
                                                                  
  --3.                                 
  SELECT * INTO #APP_HOME_RATING_INFO FROM APP_HOME_RATING_INFO  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                                                    
  AND DWELLING_ID IN (SELECT DWELLING_ID FROM APP_DWELLINGS_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                       
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
      
  UPDATE #APP_HOME_RATING_INFO SET APP_VERSION_ID = @APP_NEW_VERSION ,APP_ID = @NEW_APP_ID                                       
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_HOME_RATING_INFO SELECT * FROM #APP_HOME_RATING_INFO (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_HOME_RATING_INFO                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                               
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
    
  --4.                                                                    
  SELECT * INTO #APP_HOME_OWNER_ADD_INT FROM APP_HOME_OWNER_ADD_INT  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                                                    
  AND DWELLING_ID IN (SELECT DWELLING_ID FROM APP_DWELLINGS_INFO (nolock)     
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                       
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_HOME_OWNER_ADD_INT SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_HOME_OWNER_ADD_INT SELECT * FROM #APP_HOME_OWNER_ADD_INT  (nolock)            
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_HOME_OWNER_ADD_INT                                          
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
    
  --5.                                                                    
  SELECT * INTO #APP_DWELLING_SECTION_COVERAGES FROM APP_DWELLING_SECTION_COVERAGES (nolock)      
  WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                                                    
  AND DWELLING_ID IN (SELECT DWELLING_ID FROM APP_DWELLINGS_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                       
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_DWELLING_SECTION_COVERAGES SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID--,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                  
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
      
  INSERT INTO APP_DWELLING_SECTION_COVERAGES SELECT * FROM #APP_DWELLING_SECTION_COVERAGES (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_DWELLING_SECTION_COVERAGES                                                                   
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                              
                            
  --  Other Locations                            
  SELECT * INTO #APP_OTHER_LOCATIONS  FROM APP_OTHER_LOCATIONS (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and IS_ACTIVE='Y'                                                                  
  AND DWELLING_ID IN (SELECT DWELLING_ID FROM APP_DWELLINGS_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                       
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_OTHER_LOCATIONS SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID--,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                       
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_OTHER_LOCATIONS SELECT * FROM #APP_OTHER_LOCATIONS (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                              
      
  DROP TABLE #APP_OTHER_LOCATIONS                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
    
  --6.                                                
  SELECT * INTO #APP_DWELLING_ENDORSEMENTS FROM APP_DWELLING_ENDORSEMENTS (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                                                    
  AND DWELLING_ID IN (SELECT DWELLING_ID FROM APP_DWELLINGS_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                       
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                          
      
  UPDATE #APP_DWELLING_ENDORSEMENTS SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID--,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                       
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_DWELLING_ENDORSEMENTS SELECT * FROM #APP_DWELLING_ENDORSEMENTS (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_DWELLING_ENDORSEMENTS                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                      
                                                                  
  --7.                                                                    
  SELECT * INTO #APP_HOME_OWNER_RECREATIONAL_VEHICLES FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and isnull(ACTIVE,'Y')='Y'                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_HOME_OWNER_RECREATIONAL_VEHICLES SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_HOME_OWNER_RECREATIONAL_VEHICLES SELECT * FROM #APP_HOME_OWNER_RECREATIONAL_VEHICLES  (nolock)                                          
  SELECT @TEMP_ERROR_CODE = @@ERROR              
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_HOME_OWNER_RECREATIONAL_VEHICLES                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                           
      
  --  Recreational Vehicles ( Additional Interest )                            
  SELECT * INTO #APP_HOMEOWNER_REC_VEH_ADD_INT FROM APP_HOMEOWNER_REC_VEH_ADD_INT  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and IS_ACTIVE = 'Y'                                                            
  AND REC_VEH_ID IN (SELECT REC_VEH_ID FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and ACTIVE = 'Y')                      
      
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                    
      
  UPDATE #APP_HOMEOWNER_REC_VEH_ADD_INT SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                
      
  INSERT INTO APP_HOMEOWNER_REC_VEH_ADD_INT SELECT * FROM #APP_HOMEOWNER_REC_VEH_ADD_INT  (nolock)                                                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_HOMEOWNER_REC_VEH_ADD_INT                                                                   
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
                                                               
  --9.                                                             
  SELECT * INTO #APP_HOME_OWNER_GEN_INFO FROM APP_HOME_OWNER_GEN_INFO  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and isnull(IS_ACTIVE,'Y')='Y'                           
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_HOME_OWNER_GEN_INFO SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                
      
  INSERT INTO APP_HOME_OWNER_GEN_INFO SELECT * FROM #APP_HOME_OWNER_GEN_INFO  (nolock)                                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_HOME_OWNER_GEN_INFO                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                   
 END                                                                       
                                                                      
 --ONLY HomeOwners                              
 IF  (@RISKID = 1)                                                                  
 BEGIN                                                             
      
  --2.                                                                   
  SELECT * INTO #APP_HOME_OWNER_SCH_ITEMS_CVGS FROM APP_HOME_OWNER_SCH_ITEMS_CVGS (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID   and isnull(IS_ACTIVE,'Y')='Y'                    
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                            
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_HOME_OWNER_SCH_ITEMS_CVGS SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE          
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_HOME_OWNER_SCH_ITEMS_CVGS SELECT * FROM #APP_HOME_OWNER_SCH_ITEMS_CVGS  (nolock)                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_HOME_OWNER_SCH_ITEMS_CVGS                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                               
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  -- Details                      
      
  SELECT * INTO #APP_HOME_OWNER_SCH_ITEMS_CVGS_Details FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_Details  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                        
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                            
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                 
      
  UPDATE #APP_HOME_OWNER_SCH_ITEMS_CVGS_Details SET APP_VERSION_ID = convert(int,@APP_NEW_VERSION) ,    
  APP_ID = @NEW_APP_ID                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_HOME_OWNER_SCH_ITEMS_CVGS_Details SELECT * FROM #APP_HOME_OWNER_SCH_ITEMS_CVGS_Details (nolock)                                                              
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_HOME_OWNER_SCH_ITEMS_CVGS_Details                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                              
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                               
                                                                  
  --4.                                                                
  SELECT * INTO #APP_HOME_OWNER_SOLID_FUEL FROM APP_HOME_OWNER_SOLID_FUEL (nolock)     
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and isnull(IS_ACTIVE,'Y')='Y'                                                  
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                   
      
  UPDATE #APP_HOME_OWNER_SOLID_FUEL SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                        
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_HOME_OWNER_SOLID_FUEL SELECT * FROM #APP_HOME_OWNER_SOLID_FUEL (nolock)                                                              
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_HOME_OWNER_SOLID_FUEL                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
                                                                  
      
  --5.                                                                   
  SELECT * INTO #APP_HOME_OWNER_FIRE_PROT_CLEAN FROM APP_HOME_OWNER_FIRE_PROT_CLEAN  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                                           
  and FUEL_ID IN (select FUEL_ID from APP_HOME_OWNER_SOLID_FUEL where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and IS_ACTIVE='Y')                    
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_HOME_OWNER_FIRE_PROT_CLEAN SET APP_VERSION_ID = @APP_NEW_VERSION    
  ,APP_ID = @NEW_APP_ID--,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                   
      
  INSERT INTO APP_HOME_OWNER_FIRE_PROT_CLEAN SELECT * FROM #APP_HOME_OWNER_FIRE_PROT_CLEAN  (nolock)                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                   
      
  DROP TABLE #APP_HOME_OWNER_FIRE_PROT_CLEAN                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
                                                           
      
  --6.                                                                   
  SELECT * INTO #APP_HOME_OWNER_CHIMNEY_STOVE FROM APP_HOME_OWNER_CHIMNEY_STOVE (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and isnull(IS_ACTIVE,'Y')='Y'                                                  
  and FUEL_ID IN (select FUEL_ID from APP_HOME_OWNER_SOLID_FUEL where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and IS_ACTIVE='Y')                    
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_HOME_OWNER_CHIMNEY_STOVE SET APP_VERSION_ID = @APP_NEW_VERSION,APP_ID = @NEW_APP_ID--,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_HOME_OWNER_CHIMNEY_STOVE SELECT * FROM #APP_HOME_OWNER_CHIMNEY_STOVE  (nolock)                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
      
  DROP TABLE #APP_HOME_OWNER_CHIMNEY_STOVE                           
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
 END                                                 
                                                                   
 --FOr HOmeOWner and Watercraft                                                             
 IF  (@RISKID = 1 or @RISKID = 4)                                                                  
 BEGIN                                                      
                           
  --1.                                
  SELECT * INTO #APP_WATERCRAFT_INFO FROM APP_WATERCRAFT_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID   and isnull(IS_ACTIVE,'Y')='Y'                                                                                  
      
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                        
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_WATERCRAFT_INFO SET APP_VERSION_ID = @APP_NEW_VERSION, APP_ID = @NEW_APP_ID                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                   
      
  INSERT INTO APP_WATERCRAFT_INFO SELECT * FROM #APP_WATERCRAFT_INFO  (nolock)                                        
  SELECT @TEMP_ERROR_CODE = @@ERROR                    
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
      
  DROP TABLE #APP_WATERCRAFT_INFO                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                   
                                                                  
  --2.                                                                    
  SELECT * INTO #APP_WATERCRAFT_ENGINE_INFO FROM APP_WATERCRAFT_ENGINE_INFO  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID   and isnull(IS_ACTIVE,'Y')='Y'                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                     
      
  UPDATE #APP_WATERCRAFT_ENGINE_INFO SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                              
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_WATERCRAFT_ENGINE_INFO SELECT * FROM #APP_WATERCRAFT_ENGINE_INFO (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
      
  DROP TABLE #APP_WATERCRAFT_ENGINE_INFO                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
                                                                  
  --3.                                                             
  SELECT * INTO #APP_WATERCRAFT_COVERAGE_INFO FROM APP_WATERCRAFT_COVERAGE_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                  
  AND BOAT_ID IN (SELECT BOAT_ID FROM APP_WATERCRAFT_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                      
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                   
      
  UPDATE #APP_WATERCRAFT_COVERAGE_INFO SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_WATERCRAFT_COVERAGE_INFO SELECT * FROM #APP_WATERCRAFT_COVERAGE_INFO  (nolock)                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_WATERCRAFT_COVERAGE_INFO                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
                                                                         
  --4.                                                                 
  SELECT * INTO #APP_WATERCRAFT_ENDORSEMENTS FROM APP_WATERCRAFT_ENDORSEMENTS (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                               
  AND BOAT_ID IN (SELECT BOAT_ID FROM APP_WATERCRAFT_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM               
      
  UPDATE #APP_WATERCRAFT_ENDORSEMENTS SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID--,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_WATERCRAFT_ENDORSEMENTS SELECT * FROM #APP_WATERCRAFT_ENDORSEMENTS (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM  
      
  DROP TABLE #APP_WATERCRAFT_ENDORSEMENTS                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
    
  --5.                                                             
  SELECT * INTO #APP_WATERCRAFT_COV_ADD_INT FROM APP_WATERCRAFT_COV_ADD_INT (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID    and isnull(IS_ACTIVE,'Y')='Y'                                                                
  AND BOAT_ID IN (SELECT BOAT_ID FROM APP_WATERCRAFT_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                           
  SELECT @TEMP_ERROR_CODE = @@ERROR                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_WATERCRAFT_COV_ADD_INT                                                     
  SET APP_VERSION_ID = @APP_NEW_VERSION,APP_ID = @NEW_APP_ID,    
  CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                            
      
  INSERT INTO APP_WATERCRAFT_COV_ADD_INT SELECT * FROM #APP_WATERCRAFT_COV_ADD_INT (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                             
      
  DROP TABLE #APP_WATERCRAFT_COV_ADD_INT                                                              
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
    
  --6.                                                                  
  SELECT * INTO #APP_WATERCRAFT_GEN_INFO FROM APP_WATERCRAFT_GEN_INFO  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                                                   
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_WATERCRAFT_GEN_INFO SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_WATERCRAFT_GEN_INFO SELECT * FROM #APP_WATERCRAFT_GEN_INFO                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_WATERCRAFT_GEN_INFO                          
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                   
                                                                  
  --7.                                                                    
  SELECT * INTO #APP_WATERCRAFT_DRIVER_DETAILS FROM APP_WATERCRAFT_DRIVER_DETAILS  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                                               
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_WATERCRAFT_DRIVER_DETAILS SET APP_VERSION_ID = @APP_NEW_VERSION,APP_ID = @NEW_APP_ID,    
  CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_WATERCRAFT_DRIVER_DETAILS SELECT * FROM #APP_WATERCRAFT_DRIVER_DETAILS (nolock)                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_WATERCRAFT_DRIVER_DETAILS                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
    
  --8.                                                            
  SELECT * INTO #APP_WATER_MVR_INFORMATION FROM APP_WATER_MVR_INFORMATION  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                                                   
  AND DRIVER_ID IN (SELECT DRIVER_ID FROM APP_WATERCRAFT_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y')                       
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
      
  UPDATE #APP_WATER_MVR_INFORMATION SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                       
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_WATER_MVR_INFORMATION SELECT * FROM #APP_WATER_MVR_INFORMATION  (nolock)                                          
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_WATER_MVR_INFORMATION                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  --9.                                                                   
  SELECT * INTO #APP_WATERCRAFT_TRAILER_INFO FROM APP_WATERCRAFT_TRAILER_INFO  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                    
      
  UPDATE #APP_WATERCRAFT_TRAILER_INFO SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                  
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_WATERCRAFT_TRAILER_INFO SELECT * FROM #APP_WATERCRAFT_TRAILER_INFO (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_WATERCRAFT_TRAILER_INFO            
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                        
                                                                  
  --10.                                                                   
  SELECT * INTO #APP_WATERCRAFT_TRAILER_ADD_INT FROM APP_WATERCRAFT_TRAILER_ADD_INT (nolock)       
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and isnull(IS_ACTIVE,'Y')='Y'                                                              
  AND TRAILER_ID IN (SELECT TRAILER_ID FROM APP_WATERCRAFT_TRAILER_INFO (nolock)       
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and IS_ACTIVE='Y')                      
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_WATERCRAFT_TRAILER_ADD_INT SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                   
      
  INSERT INTO APP_WATERCRAFT_TRAILER_ADD_INT SELECT * FROM #APP_WATERCRAFT_TRAILER_ADD_INT  (nolock)                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_WATERCRAFT_TRAILER_ADD_INT                                                          
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                              
      
      
  --11.                                                               
  SELECT * INTO #APP_WATERCRAFT_EQUIP_DETAILLS FROM APP_WATERCRAFT_EQUIP_DETAILLS (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                                               
      
      
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
      
  UPDATE #APP_WATERCRAFT_EQUIP_DETAILLS SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_WATERCRAFT_EQUIP_DETAILLS SELECT * FROM #APP_WATERCRAFT_EQUIP_DETAILLS  (nolock)                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                              
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_WATERCRAFT_EQUIP_DETAILLS                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                  
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                
 END      
  
--12     
  SELECT * INTO #APP_OPERATOR_ASSIGNED_BOAT FROM APP_OPERATOR_ASSIGNED_BOAT (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID          
  SELECT @TEMP_ERROR_CODE = @@ERROR                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                               
      
  UPDATE #APP_OPERATOR_ASSIGNED_BOAT  SET APP_VERSION_ID = @APP_NEW_VERSION   ,APP_ID = @NEW_APP_ID     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_OPERATOR_ASSIGNED_BOAT  SELECT * FROM #APP_OPERATOR_ASSIGNED_BOAT  (nolock)                                                                   
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
      
  DROP TABLE #APP_OPERATOR_ASSIGNED_BOAT                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                              
  
                                                       
 -- General Liability                                                                    
 IF  (@RISKID =7)                                                                   
 BEGIN                                                                    
  --1.                         
  SELECT * INTO #APP_LOCATIONS_FORUMB FROM APP_LOCATIONS (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                    
      
  UPDATE #APP_LOCATIONS_FORUMB SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_LOCATIONS SELECT * FROM #APP_LOCATIONS_FORUMB (nolock)                       
  SELECT @TEMP_ERROR_CODE = @@ERROR                        
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_LOCATIONS_FORUMB                   
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                    
      
  ---Added By Ravindra (03-31-2006)                                
      
  -------- 2.                                
  SELECT * INTO #APP_GENERAL_LIABILITY_DETAILS FROM APP_GENERAL_LIABILITY_DETAILS (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                                            
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_GENERAL_LIABILITY_DETAILS SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                               
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
      
INSERT INTO APP_GENERAL_LIABILITY_DETAILS SELECT * FROM #APP_GENERAL_LIABILITY_DETAILS (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_GENERAL_LIABILITY_DETAILS                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
      
  ------ 3.                                
  SELECT * INTO #APP_GENERAL_COVERAGE_LIMITS FROM APP_GENERAL_COVERAGE_LIMITS (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_GENERAL_COVERAGE_LIMITS SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
      
  INSERT INTO APP_GENERAL_COVERAGE_LIMITS SELECT * FROM #APP_GENERAL_COVERAGE_LIMITS  (nolock)                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_GENERAL_COVERAGE_LIMITS                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
    
  ----Added By Ravindra Ends here                                                                  
      
  --4.  APP_GENERAL_HOLDER_INTEREST   
      
  SELECT * INTO #APP_GENERAL_HOLDER_INTEREST FROM APP_GENERAL_HOLDER_INTEREST (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_GENERAL_HOLDER_INTEREST SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_GENERAL_HOLDER_INTEREST SELECT * FROM #APP_GENERAL_COVERAGE_LIMITS (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_GENERAL_HOLDER_INTEREST                                                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
      
      
  --- 5.                              
  SELECT * INTO #APP_GENERAL_UNDERWRITING_INFO FROM APP_GENERAL_UNDERWRITING_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID   and isnull(IS_ACTIVE,'Y')='Y'                                                          
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                            
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_GENERAL_UNDERWRITING_INFO SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID--,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_GENERAL_UNDERWRITING_INFO SELECT * FROM #APP_GENERAL_UNDERWRITING_INFO (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                        
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                           
      
  DROP TABLE #APP_GENERAL_UNDERWRITING_INFO                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
 END                                                                  
    
 --Umbrella                                                                  
 IF  (@RISKID = 5)                                                                     
 BEGIN                                                                    
  --1.                                                                    
  SELECT * INTO #APP_PKG_LOB_DETAILS FROM APP_PKG_LOB_DETAILS (nolock)      
  WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                 
      
  UPDATE #APP_PKG_LOB_DETAILS SET APP_VERSION_ID = @APP_NEW_VERSION  ,APP_ID = @NEW_APP_ID                                                            
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_PKG_LOB_DETAILS SELECT * FROM #APP_PKG_LOB_DETAILS (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_PKG_LOB_DETAILS                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                             
  --3.                                                                    
  SELECT * INTO #APP_UMBRELLA_LIMITS  FROM APP_UMBRELLA_LIMITS (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID   and isnull(IS_ACTIVE,'Y')='Y'                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_UMBRELLA_LIMITS SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
      
  INSERT INTO APP_UMBRELLA_LIMITS SELECT * FROM #APP_UMBRELLA_LIMITS (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_UMBRELLA_LIMITS                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
                                                                  
  --4.                                                                    
  SELECT * INTO #APP_UMBRELLA_GEN_INFO FROM APP_UMBRELLA_GEN_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_UMBRELLA_GEN_INFO SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
 INSERT INTO APP_UMBRELLA_GEN_INFO SELECT * FROM #APP_UMBRELLA_GEN_INFO (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_UMBRELLA_GEN_INFO                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
    
              
  --5.                                                                    
  SELECT * INTO #APP_UMBRELLA_REAL_ESTATE_LOCATION FROM APP_UMBRELLA_REAL_ESTATE_LOCATION (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and isnull(IS_ACTIVE,'Y')='Y'                                       
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_UMBRELLA_REAL_ESTATE_LOCATION SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_UMBRELLA_REAL_ESTATE_LOCATION SELECT * FROM #APP_UMBRELLA_REAL_ESTATE_LOCATION (nolock)                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_UMBRELLA_REAL_ESTATE_LOCATION                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                 
      
  --6.                                                                  
      
  SELECT * INTO #APP_UMBRELLA_VEHICLE_INFO FROM APP_UMBRELLA_VEHICLE_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                       
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_UMBRELLA_VEHICLE_INFO SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_UMBRELLA_VEHICLE_INFO SELECT * FROM #APP_UMBRELLA_VEHICLE_INFO  (nolock)                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                   
      
  DROP TABLE #APP_UMBRELLA_VEHICLE_INFO                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR            
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                  
                                                                  
  --9.                                                                  
  SELECT * INTO #APP_UMBRELLA_RECREATIONAL_VEHICLES FROM APP_UMBRELLA_RECREATIONAL_VEHICLES (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(ACTIVE,'Y')='Y'                                                     
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_UMBRELLA_RECREATIONAL_VEHICLES SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                            
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                      
      
  INSERT INTO APP_UMBRELLA_RECREATIONAL_VEHICLES SELECT * FROM #APP_UMBRELLA_RECREATIONAL_VEHICLES (nolock)                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                           
      
  DROP TABLE #APP_UMBRELLA_RECREATIONAL_VEHICLES                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
    
  --11.                                                                    
  SELECT * INTO #APP_UMBRELLA_WATERCRAFT_INFO FROM APP_UMBRELLA_WATERCRAFT_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and isnull(IS_ACTIVE,'Y')='Y'                                                              
      
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_UMBRELLA_WATERCRAFT_INFO SET APP_VERSION_ID = @APP_NEW_VERSION  ,APP_ID = @NEW_APP_ID                                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
      
  INSERT INTO APP_UMBRELLA_WATERCRAFT_INFO SELECT * FROM #APP_UMBRELLA_WATERCRAFT_INFO  (nolock)                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_UMBRELLA_WATERCRAFT_INFO                                       
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                   
                                                 
  --12.                                                                    
  SELECT * INTO #APP_WATERCRAFT_ENGINE_INFO_FORUMB FROM APP_WATERCRAFT_ENGINE_INFO  (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID    and isnull(IS_ACTIVE,'Y')='Y'                            
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_WATERCRAFT_ENGINE_INFO_FORUMB SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                   
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                               
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_WATERCRAFT_ENGINE_INFO SELECT * FROM #APP_WATERCRAFT_ENGINE_INFO_FORUMB (nolock)                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                             
      
  DROP TABLE #APP_WATERCRAFT_ENGINE_INFO_FORUMB                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                             
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
    
                                           
      
  --15.                                                                    
  SELECT * INTO #APP_UMBRELLA_DRIVER_DETAILS FROM APP_UMBRELLA_DRIVER_DETAILS (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                                                 
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_UMBRELLA_DRIVER_DETAILS SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                           
  SELECT @TEMP_ERROR_CODE = @@ERROR                                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_UMBRELLA_DRIVER_DETAILS SELECT * FROM #APP_UMBRELLA_DRIVER_DETAILS  (nolock)                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_UMBRELLA_DRIVER_DETAILS                             
  SELECT @TEMP_ERROR_CODE = @@ERROR                                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                   
      
                                         
  SELECT * INTO #APP_UMBRELLA_MVR_INFORMATION FROM APP_UMBRELLA_MVR_INFORMATION (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                                               
    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                        
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_UMBRELLA_MVR_INFORMATION SET APP_VERSION_ID = @APP_NEW_VERSION,APP_ID = @NEW_APP_ID                            
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_UMBRELLA_MVR_INFORMATION SELECT * FROM #APP_UMBRELLA_MVR_INFORMATION  (nolock)                                                             
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                        
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_UMBRELLA_MVR_INFORMATION                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
    
  SELECT * INTO #APP_UMBRELLA_DWELLINGS_INFO FROM APP_UMBRELLA_DWELLINGS_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                             
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM             
      
  UPDATE #APP_UMBRELLA_DWELLINGS_INFO SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_UMBRELLA_DWELLINGS_INFO SELECT * FROM #APP_UMBRELLA_DWELLINGS_INFO (nolock)                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                              
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
  
  DROP TABLE #APP_UMBRELLA_DWELLINGS_INFO                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  --2                                       
  SELECT * INTO #APP_UMBRELLA_RATING_INFO FROM APP_UMBRELLA_RATING_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                       
      
  UPDATE #APP_UMBRELLA_RATING_INFO SET APP_VERSION_ID = @APP_NEW_VERSION , APP_ID = @NEW_APP_ID                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                            
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_UMBRELLA_RATING_INFO SELECT * FROM #APP_UMBRELLA_RATING_INFO (nolock)                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_UMBRELLA_RATING_INFO                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                
                                      
      
  --3                                      
      
  SELECT * INTO #APP_UMBRELLA_UNDERLYING_POLICIES FROM APP_UMBRELLA_UNDERLYING_POLICIES (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                        
  SELECT @TEMP_ERROR_CODE = @@ERROR             
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_UMBRELLA_UNDERLYING_POLICIES SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_UMBRELLA_UNDERLYING_POLICIES SELECT * FROM #APP_UMBRELLA_UNDERLYING_POLICIES  (nolock)                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                    
      
  DROP TABLE #APP_UMBRELLA_UNDERLYING_POLICIES                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
    
                                   
  --4                                      
      
  SELECT * INTO #APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                                                    
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
  
  UPDATE #APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES SELECT * FROM #APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES (nolock)                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
      
  DROP TABLE #APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                         
    
  --5                                       
      
  SELECT * INTO #APP_UMBRELLA_FARM_INFO FROM APP_UMBRELLA_FARM_INFO (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and isnull(IS_ACTIVE,'Y')='Y'                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                              
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_UMBRELLA_FARM_INFO SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                   
      
  INSERT INTO APP_UMBRELLA_FARM_INFO SELECT * FROM #APP_UMBRELLA_FARM_INFO (nolock)                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
      
  DROP TABLE #APP_UMBRELLA_FARM_INFO                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                         
    
  SELECT * INTO #APP_UMBRELLA_COVERAGES FROM APP_UMBRELLA_COVERAGES AVC  (nolock)WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                     
      
  UPDATE #APP_UMBRELLA_COVERAGES SET APP_VERSION_ID = @APP_NEW_VERSION ,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
  INSERT INTO APP_UMBRELLA_COVERAGES               
  SELECT * FROM #APP_UMBRELLA_COVERAGES (nolock)                                              
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_UMBRELLA_COVERAGES                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  --18.                                                          
      
  SELECT * INTO #APP_WATER_MVR_INFORMATION_FORUMB FROM APP_WATER_MVR_INFORMATION (nolock)      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and isnull(IS_ACTIVE,'Y')='Y'                         
      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  UPDATE #APP_WATER_MVR_INFORMATION_FORUMB SET APP_VERSION_ID = @APP_NEW_VERSION,    
  APP_ID = @NEW_APP_ID,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  INSERT INTO APP_WATER_MVR_INFORMATION SELECT * FROM #APP_WATER_MVR_INFORMATION_FORUMB (nolock)                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
      
  DROP TABLE #APP_WATER_MVR_INFORMATION_FORUMB                                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                       
 END         
                                                                  
COMMIT TRAN                                                                        
RETURN @APP_NEW_VERSION    
PROBLEM:                                                                                          
 ROLLBACK TRAN                                                                                          
 SET @APP_NEW_VERSION = -1                                                                           
 RETURN @APP_NEW_VERSION                                                                                         
END                
              
    
  










GO

