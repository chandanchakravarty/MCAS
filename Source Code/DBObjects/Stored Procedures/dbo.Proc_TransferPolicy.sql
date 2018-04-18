IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_TransferPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_TransferPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 --BEGIN TRAN        
 --DROP PROc dbo.Proc_TransferPolicy        
 --GO        
/*        
Proc  :Proc_TransferPolicy        
Created by : Pravesh K Chandel        
Created Date :25 feb 2008        
Purpose  : Transfer a policy to new Customer       
    
Modified by KASANA     
Modified Date :April 10 2008       
: Itrack 4039 -@OLD_POLICY_NUMBER    

Modified by Pradeep Kushwaha
Modified Date :April 27 2011
Purpose  : Copy a policy to Customer the same customer         
used in  : Ebx advantage brazil 
: Itrack 1094 -
        
used in  : Wolverine        
*/      
--DROP PROc dbo.Proc_TransferPolicy        
create proc [dbo].[Proc_TransferPolicy]        
(        
@CUSTOMER_ID INT,        
@POLICY_ID INT,        
@POLICY_VERSION_ID SMALLINT,        
@NEW_CUSTOMER_ID INT,        
@CREATED_BY INT =NULL,        
@NEW_POLICY_ID int outPUT,        
@NEW_POLICY_VERSION_ID int output,        
@TRAN_DESC VARCHAR(1000)='' output,                    
@NEW_CSR int,        
@NEW_PRODUCER int,      
@LANG_ID INT=1  
)        
as        
BEGIN        
declare @RISKID INT        
DECLARE @TEMP_ERROR_CODE INT        
DECLARE @NEW_VERSION_ID SMALLINT        
        
SET @TEMP_ERROR_CODE=0        
SET @NEW_VERSION_ID=@POLICY_VERSION_ID        
        
BEGIN TRAN        
        
        
SELECT @RISKID = POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)        
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID         
    AND POLICY_VERSION_ID=@POLICY_VERSION_ID        
        
DECLARE @NEW_DISP_VERSION    VARCHAR(10) ,                  
 @INVALID_COVERAGE    INT,          
 @COVERAGE_BASE_CHANGED_BOAT_ID  VARCHAR(100) ,        
 @NEW_DISP_VERSION_REWRITABLE   INT,        
 @AGENCY_ID   INT,         
 @NEW_AGENCY_ID   INT,        
 @AGENCY_DESC   varchar(50),        
 @NEW_AGENCY_DESC  varchar(50),        
 @CUSTOMER_NAME   varchar(50),        
 @NEW_CUSTOMER_NAME  varchar(50),        
 @CLIENT_CODE   VARCHAR(10),        
 @NEW_CLIENT_CODE  VARCHAR(10),        
 @OLD_POLICY_NUMBER  VARCHAR(75),    
 @NEW_AGENCY_CODE   varchar(10) ,    
 @NEW_AGENCY_BILL_TYPE    Nvarchar(10),    
 @NEW_BILL_TYPE  varchar(2) ,    
 @LOB_ID int    

DECLARE @NEW_APPLICANT_ID INT    
DECLARE @APPLICANT_ID INT    
DECLARE @NEW_APP_ID INT  
 
DECLARE @APP_NEW_ID INT   

DECLARE @NEW_APP_NUMBER  VARCHAR(75)   
DECLARE @MAXCLAUSE_ID INT      


 
    
--CREATE NEW VERSION OF CANCELED POLICY AND UPDATE IT TO SUSPENED        
EXECUTE         
 Proc_PolicyCreateNewVersion        
 @CUSTOMER_ID ,        
 @POLICY_ID ,                                                            
 @NEW_VERSION_ID,                                                            
 @CREATED_BY,                                                            
 @NEW_VERSION_ID OUTPUT,                        
0, -- @RENEWAL Int = 0 ,   -- In case  of Renewal 1 will be passed in case of Rewrite 3 will be passed else 0                
 @NEW_DISP_VERSION  output  ,                  
 @TRAN_DESC output,                    
 @INVALID_COVERAGE  ,          
 @COVERAGE_BASE_CHANGED_BOAT_ID out ,        
 @NEW_DISP_VERSION_REWRITABLE  output        
    
set @NEW_POLICY_VERSION_ID = 1 --@NEW_VERSION_ID      
set @TRAN_DESC  =''  
--1.          
  
SELECT * INTO #POL_CUSTOMER_POLICY_LIST FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@NEW_VERSION_ID                                   
        
--CHANGE AGENCY        
SELECT @AGENCY_ID=AGENCY_ID,@OLD_POLICY_NUMBER=POLICY_NUMBER , @LOB_ID=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                  
  
        
SELECT @APP_NEW_ID = ISNULL(MAX(APP_ID),0)+1, @NEW_POLICY_ID = ISNULL(MAX(POLICY_ID),0) + 1 FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID=@NEW_CUSTOMER_ID        



SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                           
        
--customer name and agency        
SELECT @CUSTOMER_NAME = isnull(CUSTOMER_FIRST_NAME,'') + ' ' + isnull(CUSTOMER_MIDDLE_NAME,'')+ ' ' + isnull(CUSTOMER_LAST_NAME,''),        
 @CLIENT_CODE=ISNULL(CUSTOMER_CODE,'')        
 FROM CLT_CUSTOMER_LIST (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID        
        
SELECT @NEW_AGENCY_ID=CUSTOMER_AGENCY_ID ,       
 @NEW_CUSTOMER_NAME = isnull(CUSTOMER_FIRST_NAME,'') + ' ' +isnull(CUSTOMER_MIDDLE_NAME,'')+ ' ' + isnull(CUSTOMER_LAST_NAME,''),        
 @NEW_CLIENT_CODE=ISNULL(CUSTOMER_CODE,'')        
 FROM CLT_CUSTOMER_LIST (NOLOCK) WHERE CUSTOMER_ID=@NEW_CUSTOMER_ID        
        
SELECT @AGENCY_DESC=AGENCY_DISPLAY_NAME FROM MNT_AGENCY_LIST with(nolock) WHERE AGENCY_ID=@AGENCY_ID        
SELECT @NEW_AGENCY_DESC=AGENCY_DISPLAY_NAME,@NEW_AGENCY_CODE=AGENCY_CODE,@NEW_AGENCY_BILL_TYPE=AGENCY_BILL_TYPE FROM MNT_AGENCY_LIST with(nolock) WHERE AGENCY_ID=@NEW_AGENCY_ID        
    
SELECT @NEW_BILL_TYPE=[TYPE] FROM MNT_LOOKUP_VALUES with(nolock) WHERE convert(varchar,LOOKUP_UNIQUE_ID)=@NEW_AGENCY_BILL_TYPE                 
      
UPDATE #POL_CUSTOMER_POLICY_LIST                                 
 SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID --,  AGENCY_ID=@NEW_AGENCY_ID --Comment for itrack- 1496       
 
 --If new agency is wolverine then update bill type,bill type id,and set install plan and down apy mode to null    
 if (upper(@NEW_AGENCY_CODE)='W001')    
 BEGIN    
 
 DECLARE @NEW_INSTALL_PLAN_ID INT    
 SELECT @NEW_INSTALL_PLAN_ID=IDEN_PLAN_ID FROM ACT_INSTALL_PLAN_DETAIL WHERE ISNULL(SYSTEM_GENERATED_FULL_PAY,0)=1    
 
 UPDATE #POL_CUSTOMER_POLICY_LIST                               
 --Case Added FOR iTrack Issue #5607     
    set BILL_TYPE_ID = case when @LOB_ID in (1,6) then @NEW_AGENCY_BILL_TYPE else 8460 end,    
   BILL_TYPE  = @NEW_BILL_TYPE,    
   INSTALL_PLAN_ID = @NEW_INSTALL_PLAN_ID,    
   DOWN_PAY_MODE = NULL    
 END    
    
--if new agency is Not Wolverine then HOME Discount will not be given.    
IF (UPPER(@NEW_AGENCY_CODE)<>'W001')        
BEGIN            
  UPDATE #POL_CUSTOMER_POLICY_LIST SET IS_HOME_EMP = 0     
END    
--if new agency is Wolverine then HOME Discount will be given    
IF (UPPER(@NEW_AGENCY_CODE)='W001')        
BEGIN            
  UPDATE #POL_CUSTOMER_POLICY_LIST SET IS_HOME_EMP = 1     
END    
    
if (@NEW_CSR!=0)        
UPDATE #POL_CUSTOMER_POLICY_LIST                                 
 SET CSR = @NEW_CSR        
if (@NEW_PRODUCER!=0)        
UPDATE #POL_CUSTOMER_POLICY_LIST                                 
 SET PRODUCER = @NEW_PRODUCER        
--if agency change then update underwriter to null    
if(@NEW_AGENCY_ID !=@AGENCY_ID)    
UPDATE #POL_CUSTOMER_POLICY_LIST                                 
 SET UNDERWRITER = null    
    
SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                   
                 
INSERT INTO POL_CUSTOMER_POLICY_LIST                                
 SELECT * FROM #POL_CUSTOMER_POLICY_LIST                            
 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                       
DROP TABLE #POL_CUSTOMER_POLICY_LIST                                                  
SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      


/*--Commneted by Pradeep on 27-April-2011 itrack- 1094
--UPDATING APLICANT INFO    
SELECT @NEW_APPLICANT_ID=APPLICANT_ID FROM CLT_APPLICANT_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@NEW_CUSTOMER_ID   
AND IS_PRIMARY_APPLICANT =1  --Added condition for 5655 on 29 April 2009    

SELECT @APPLICANT_ID=APPLICANT_ID FROM CLT_APPLICANT_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID   
AND IS_PRIMARY_APPLICANT =1 --Added condition for 5655 on 29 April 2009     


if (@NEW_APPLICANT_ID=@APPLICANT_ID)    
 UPDATE POL_APPLICANT_LIST SET     
 POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID,CUSTOMER_ID=@NEW_CUSTOMER_ID,    
 -- APPLICANT_ID=@NEW_APPLICANT_ID,    
 CREATED_BY=@CREATED_BY,CREATED_DATETIME=GETDATE()     
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@NEW_VERSION_ID     
 AND IS_PRIMARY_APPLICANT =1    
ELSE    
BEGIN    
 UPDATE POL_APPLICANT_LIST SET     
 POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID,CUSTOMER_ID=@NEW_CUSTOMER_ID,    
   APPLICANT_ID=@NEW_APPLICANT_ID,    
  CREATED_BY=@CREATED_BY,CREATED_DATETIME=GETDATE()     
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@NEW_VERSION_ID     
  AND IS_PRIMARY_APPLICANT =1    
      
  DELETE FROM POL_APPLICANT_LIST      
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@NEW_VERSION_ID     
  AND IS_PRIMARY_APPLICANT <> 1    
END    
      
       
--Creating a entry in APP_list against this new policy and this will be replica of app which is mapped to Old policy    

SELECT AP.* INTO #APP_LIST    
FROM APP_LIST AP WITH(NOLOCK)    
INNER JOIN POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK) ON AP.CUSTOMER_ID=POL.CUSTOMER_ID AND AP.APP_ID=POL.APP_ID     
AND AP.APP_VERSION_ID=POL.APP_VERSION_ID    
WHERE POL.CUSTOMER_ID=@CUSTOMER_ID AND POL.POLICY_ID=@POLICY_ID AND POL.POLICY_VERSION_ID=@POLICY_VERSION_ID    
    
SELECT @NEW_APP_ID=ISNULL(MAX(APP_ID) ,0) +1 FROM APP_LIST AP WITH(NOLOCK) WHERE CUSTOMER_ID=@NEW_CUSTOMER_ID    

UPDATE  #APP_LIST SET CUSTOMER_ID=@NEW_CUSTOMER_ID,APP_ID=@NEW_APP_ID,APP_VERSION_ID=1,CREATED_BY=@CREATED_BY,CREATED_DATETIME=GETDATE(),MODIFIED_BY=NULL,LAST_UPDATED_DATETIME=NULL    
INSERT INTO APP_LIST  SELECT * FROM #APP_LIST    
DROP TABLE #APP_LIST    

 */

--Updating new Policy number        

CREATE TABLE #POLICY_TABLE    
(
  NEW_APP_NUMBER NVARCHAR(75)
)
INSERT INTO #POLICY_TABLE ( NEW_APP_NUMBER ) EXEC Proc_GenerateAppNumber @LOB_ID   
SELECT @NEW_APP_NUMBER=NEW_APP_NUMBER FROM #POLICY_TABLE

DROP TABLE #POLICY_TABLE

--execute Proc_RewritePolicyNumber @NEW_CUSTOMER_ID,@NEW_POLICY_ID,@NEW_POLICY_VERSION_ID, @NEW_POLICY_NUMBER out        
      
----Transaction log discription    
    --
SET @TRAN_DESC=ISNULL(@TRAN_DESC,'')+ '<br>'+CASE WHEN @LANG_ID=2 THEN 'Política copiado –' ELSE 'Policy copied –' END + CONVERT(VARCHAR,GETDATE(),101) + '<br>'        
    
SET @TRAN_DESC=@TRAN_DESC +CASE WHEN @LANG_ID=2 THEN  'Copiado do Cliente - ' ELSE 'Copied from Client - ' END +   @CUSTOMER_NAME + ' – ' +  @AGENCY_DESC + ' – ' + @OLD_POLICY_NUMBER + ' – '  + @CLIENT_CODE + '<br>'        
set @TRAN_DESC = isnull(@TRAN_DESC,'') -- + 'Insurance score of copied policy has been updated to Insurance Score of new client<br>'  
    
--- On old account – put the following details         
--– Policy copied – the date the policy was copied         
--– Copied to  Client -  New Client Name – New Agency Name – New Policy Number  - New Client Code         
SET @TRAN_DESC=ISNULL(@TRAN_DESC,'') + '~' + '<br>'+CASE WHEN @LANG_ID=2 THEN 'Política copiado –' ELSE 'Policy copied –' END +  CONVERT(VARCHAR,GETDATE(),101) + '<br>'        
    
SET @TRAN_DESC=@TRAN_DESC + CASE WHEN @LANG_ID=2 THEN  'Copiado do Cliente - ' ELSE 'Copied from Client - ' END +   @NEW_CUSTOMER_NAME + ' – ' +  @NEW_AGENCY_DESC + ' – ' + @NEW_APP_NUMBER + ' – '  + @NEW_CLIENT_CODE + ''        

--/*Commneted by Pradeep on 27-April-2011 itrack- 1094
-- Private Passenger or Motorcycle      
 --COPYING EFT AND CREDIT INFO TABLE    
UPDATE ACT_POL_CREDIT_CARD_DETAILS SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@NEW_VERSION_ID                                   
UPDATE  ACT_POL_EFT_CUST_INFO  SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@NEW_VERSION_ID                                   
                           
IF (@RISKID = 2 OR @RISKID = 3)                                                   
BEGIN                                      
 --1.                                                  
 SELECT * INTO #POL_VEHICLES FROM POL_VEHICLES WITH(NOLOCK)WHERE          
  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                                  
 UPDATE #POL_VEHICLES SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
 INSERT INTO POL_VEHICLES SELECT * FROM #POL_VEHICLES         
 DROP TABLE  #POL_VEHICLES         
 --2.                             
 UPDATE POL_VEHICLE_COVERAGES SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID WHERE  CUSTOMER_ID=@CUSTOMER_ID            
 AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                   
 --3.                              
 UPDATE POL_VEHICLE_ENDORSEMENTS SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@NEW_VERSION_ID            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                              
 --4.                                                  
 UPDATE POL_ADD_OTHER_INT SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID  WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@NEW_VERSION_ID                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
 --5.                         
 UPDATE POL_DRIVER_DETAILS SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@NEW_VERSION_ID                                     
                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
 --6.            
 UPDATE POL_MVR_INFORMATION  SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                              
 SELECT @TEMP_ERROR_CODE = @@ERROR          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                    
 --7.                                                  
 UPDATE POL_AUTO_GEN_INFO  SET CUSTOMER_ID=@NEW_CUSTOMER_ID,        POLICY_ID=@NEW_POLICY_ID,        POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID       WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID       
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM            
 --8.                                                  
 UPDATE POL_DRIVER_ASSIGNED_VEHICLE SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                            
           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
         
 if(@RISKID=2)         
 begin                    
   UPDATE POL_MISCELLANEOUS_EQUIPMENT_VALUES  SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID      
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                 
                                    
   SELECT @TEMP_ERROR_CODE = @@ERROR                     
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
 end                               
         
 DELETE FROM POL_VEHICLES WHERE          
  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                                  
        
END                                                  
   -- Homeowners or Rental Dwelling                                         
IF  (@RISKID = 1 OR @RISKID =6)                             
BEGIN        
 --1        
 SELECT * INTO #POL_LOCATIONS FROM POL_LOCATIONS WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                                  
 UPDATE #POL_LOCATIONS SET  CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID        
 INSERT INTO POL_LOCATIONS SELECT * FROM #POL_LOCATIONS        
 DROP TABLE #POL_LOCATIONS         
 SELECT @TEMP_ERROR_CODE = @@ERROR               
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
 --2        
 SELECT * INTO #POL_DWELLINGS_INFO FROM POL_DWELLINGS_INFO        
 WHERE   CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                                
        
 UPDATE #POL_DWELLINGS_INFO SET  INFLATION_FACTOR=NULL,CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID       
 INSERT INTO POL_DWELLINGS_INFO SELECT * FROM #POL_DWELLINGS_INFO        
 DROP TABLE #POL_DWELLINGS_INFO         
 SELECT @TEMP_ERROR_CODE = @@ERROR                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM            
 --3        
 UPDATE POL_DWELLING_SECTION_COVERAGES   SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                     
 WHERE   CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM            
        
        --4                              
 UPDATE POL_HOME_RATING_INFO  SET CUSTOMER_ID=@NEW_CUSTOMER_ID,        POLICY_ID=@NEW_POLICY_ID,        POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID         
  
                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM     
    
 --5.                                  
 UPDATE POL_HOME_OWNER_ADD_INT   SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                               
                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
 --6                   
 UPDATE POL_OTHER_STRUCTURE_DWELLING SET  CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                          
                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM            
 --7        
 UPDATE POL_DWELLING_ENDORSEMENTS    SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
         
 --8            
 UPDATE POL_OTHER_LOCATIONS  SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID    
    WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                               
                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                           
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                  
     
 --9.             
 SELECT * INTO #POL_HOME_OWNER_RECREATIONAL_VEHICLES FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@NEW_VERSION_ID                                               
 UPDATE #POL_HOME_OWNER_RECREATIONAL_VEHICLES  SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID          
 INSERT INTO POL_HOME_OWNER_RECREATIONAL_VEHICLES SELECT * FROM #POL_HOME_OWNER_RECREATIONAL_VEHICLES        
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
 DROP TABLE #POL_HOME_OWNER_RECREATIONAL_VEHICLES        
 --10  ( Home>Recr Veh>Add Int)              
 UPDATE POL_HOMEOWNER_REC_VEH_ADD_INT  SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                         
                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
     
 --11.                                     
 UPDATE POL_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES   SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID       
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
 --12.                   
 UPDATE POL_HOME_OWNER_GEN_INFO   SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID      
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                             
                       
 SELECT @TEMP_ERROR_CODE = @@ERROR                       
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM        
-------        
 DELETE FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@NEW_VERSION_ID                                                
 DELETE FROM POL_DWELLINGS_INFO WHERE   CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID           
 DELETE FROM POL_LOCATIONS WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                                  
                   
END                                                     
                                         
--ONLY HomeOwners              
IF  (@RISKID = 1)                                         
BEGIN                                                  
 --1.                                                 
 UPDATE POL_HOME_OWNER_SCH_ITEMS_CVGS  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID       WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID              
  
        
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                             
 --2        
 UPDATE POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS  SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POL_ID=@NEW_POLICY_ID,POL_VERSION_ID = @NEW_POLICY_VERSION_ID WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POL_ID=@POLICY_ID AND POL_VERSION_ID=@NEW_VERSION_ID                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                             
 --3.                         
 UPDATE POL_HOME_OWNER_PER_ART_GEN_INFO   SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                 
    
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM          
            
 --4.                                                 
 SELECT * INTO #POL_HOME_OWNER_SOLID_FUEL FROM POL_HOME_OWNER_SOLID_FUEL WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                 
 UPDATE #POL_HOME_OWNER_SOLID_FUEL  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID        
 INSERT INTO POL_HOME_OWNER_SOLID_FUEL SELECT * FROM #POL_HOME_OWNER_SOLID_FUEL        
 SELECT @TEMP_ERROR_CODE = @@ERROR                               
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
 DROP TABLE #POL_HOME_OWNER_SOLID_FUEL        
 --5.           
 UPDATE POL_HOME_OWNER_FIRE_PROT_CLEAN   SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                   
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
            
 --6.         
 UPDATE POL_HOME_OWNER_CHIMNEY_STOVE   SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID ,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM             
 DELETE FROM POL_HOME_OWNER_SOLID_FUEL WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                        
             
END                         
                  
--FOr HOmeOWner and Watercraft                                                
IF  (@RISKID = 1 or @RISKID = 4)     
BEGIN            
 IF EXISTS (                  
     SELECT CUSTOMER_ID FROM POL_WATERCRAFT_INFO WHERE          
     CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID               
   )            
  BEGIN                  
  --1.         
   SELECT * INTO #POL_WATERCRAFT_INFO FROM POL_WATERCRAFT_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                                  
UPDATE #POL_WATERCRAFT_INFO SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID        
  INSERT INTO POL_WATERCRAFT_INFO SELECT * FROM #POL_WATERCRAFT_INFO        
        
  DROP TABLE #POL_WATERCRAFT_INFO        
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                  
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                
        
  --2.                                                  
  UPDATE POL_WATERCRAFT_ENGINE_INFO   SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                         
                          
  SELECT @TEMP_ERROR_CODE = @@ERROR                                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
              
  --3.                        
UPDATE POL_WATERCRAFT_COVERAGE_INFO  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID WHERE  CUSTOMER_ID=@CUSTOMER_ID                         
  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
  --4.         
  UPDATE POL_WATERCRAFT_ENDORSEMENTS  SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID      
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                         
                         
  SELECT @TEMP_ERROR_CODE = @@ERROR           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
  --5.         
  SELECT * INTO #POL_WATERCRAFT_TRAILER_INFO FROM POL_WATERCRAFT_TRAILER_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                           
  UPDATE #POL_WATERCRAFT_TRAILER_INFO SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID         
  INSERT INTO POL_WATERCRAFT_TRAILER_INFO SELECT * FROM #POL_WATERCRAFT_TRAILER_INFO        
        
 SELECT @TEMP_ERROR_CODE = @@ERROR           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
  DROP TABLE #POL_WATERCRAFT_TRAILER_INFO        
  --6.                            
  UPDATE POL_WATERCRAFT_COV_ADD_INT SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                     
  SELECT @TEMP_ERROR_CODE = @@ERROR             
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
          
  --7.                                                
  UPDATE POL_WATERCRAFT_GEN_INFO SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID              
  SELECT @TEMP_ERROR_CODE = @@ERROR                             
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
              
  --8.                                   
  SELECT * INTO #POL_WATERCRAFT_DRIVER_DETAILS FROM POL_WATERCRAFT_DRIVER_DETAILS  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                              
  UPDATE #POL_WATERCRAFT_DRIVER_DETAILS  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID        
  INSERT INTO POL_WATERCRAFT_DRIVER_DETAILS SELECT * FROM #POL_WATERCRAFT_DRIVER_DETAILS        
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                              
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                
  DROP TABLE #POL_WATERCRAFT_DRIVER_DETAILS        
  --9.            
  UPDATE POL_WATERCRAFT_MVR_INFORMATION SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID               
 
                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
        
  UPDATE POL_MVR_INFORMATION SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                              
  SELECT @TEMP_ERROR_CODE = @@ERROR          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                    
    
  --10         
  UPDATE POL_OPERATOR_ASSIGNED_BOAT SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                    
  
    
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM          
  --11.          
        
  UPDATE POL_WATERCRAFT_TRAILER_ADD_INT SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID ,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
             
  --12.                                                 
  UPDATE POL_WATERCRAFT_EQUIP_DETAILLS SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID      
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                        
                             
  SELECT @TEMP_ERROR_CODE = @@ERROR          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM            
  --        
  DELETE FROM POL_WATERCRAFT_TRAILER_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                 
  DELETE FROM POL_WATERCRAFT_DRIVER_DETAILS  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                  
  DELETE FROM POL_WATERCRAFT_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                         
           
 END                                          
END                            
                  
IF(@RISKID = 5)                                      
BEGIN                                  
                    
 --1. POL_UMBRELLA_LIMITS                            
 UPDATE POL_UMBRELLA_LIMITS SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                             
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM           
     
 -- 2.  POL_UMBRELLA_REAL_ESTATE_LOCATION                          
            
 UPDATE POL_UMBRELLA_REAL_ESTATE_LOCATION SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
 -- 3. POL_UMBRELLA_DWELLINGS_INFO                            
            
 UPDATE POL_UMBRELLA_DWELLINGS_INFO SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                             
  POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                       
 SELECT @TEMP_ERROR_CODE = @@ERROR                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                         
 -- 4.  POL_UMBRELLA_RATING_INFO                            
         
 UPDATE POL_UMBRELLA_RATING_INFO SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                          
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                            
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                              
         
 -- 5. POL_UMBRELLA_VEHICLE_INFO                            
 UPDATE POL_UMBRELLA_VEHICLE_INFO SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                          
  POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                       
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
 -- 6. POL_UMBRELLA_RECREATIONAL_VEHICLES                            
 UPDATE POL_UMBRELLA_RECREATIONAL_VEHICLES  SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID WHERE  CUSTOMER_ID=@CUSTOMER_ID AND               
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                           
 SELECT @TEMP_ERROR_CODE = @@ERROR              
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
           
 -- 7. POL_UMBRELLA_WATERCRAFT_INFO                            
            
 UPDATE  POL_UMBRELLA_WATERCRAFT_INFO SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                             
  POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                   
 SELECT @TEMP_ERROR_CODE = @@ERROR                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
            
 --8.  POL_UMBRELLA_WATERCRAFT_ENGINE_INFO                            
         
 UPDATE POL_UMBRELLA_WATERCRAFT_ENGINE_INFO SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                 
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                       
 SELECT @TEMP_ERROR_CODE = @@ERROR                                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
       -- 9. POL_UMBRELLA_UNDERLYING_POLICIES                            
            
 UPDATE POL_UMBRELLA_UNDERLYING_POLICIES SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID WHERE CUSTOMER_ID=@CUSTOMER_ID AND                             
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID       
 SELECT @TEMP_ERROR_CODE = @@ERROR                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM         
            
 -- 10.  POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES                            
            
 UPDATE POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                             
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                    
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                    
            
 -- 11.  POL_UMBRELLA_DRIVER_DETAILS                            
            
 UPDATE POL_UMBRELLA_DRIVER_DETAILS SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                             
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
         
 -- 12.  POL_UMBRELLA_MVR_INFORMATION                            
            
 UPDATE POL_UMBRELLA_MVR_INFORMATION SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                             
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
            
 -- 13.   POL_UMBRELLA_FARM_INFO                            
             UPDATE POL_UMBRELLA_FARM_INFO SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                             
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                                   
 SELECT @TEMP_ERROR_CODE = @@ERROR               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
         
 --14.     POL_UMBRELLA_GEN_INFO                       
 UPDATE POL_UMBRELLA_GEN_INFO SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                             
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                           
            
 --15        
 UPDATE POL_UMBRELLA_COVERAGES SET CUSTOMER_ID=@NEW_CUSTOMER_ID,POL_ID=@NEW_POLICY_ID,POL_VERSION_ID = @NEW_POLICY_VERSION_ID WHERE  CUSTOMER_ID=@CUSTOMER_ID             
 AND POL_ID=@POLICY_ID AND POL_VERSION_ID=@NEW_VERSION_ID                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
        
                  
END               
        
--updating prior loss information        
--Select * from CLM_CLAIM_INFO  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                   
--SELECT * FROM PRIOR_LOSS_HOME        
--SELECT * FROM APP_PRIOR_LOSS_INFO        
    
declare @LOSSID int     
declare  @LOSS_DATE datetime,@CLAIM_DATE datetime,@LOSS_TYPE int,@AMOUNT_PAID numeric,@AMOUNT_RESERVED numeric,    
 @CLAIM_STATUS nvarchar(10),@LOSS_DESC nvarchar(50),    
    @REMARKS nvarchar(250),  @MOD nvarchar(50), @LOSS_RUN nchar(2), @CAT_NO nvarchar(20), @CLAIM_ID nvarchar(20) ,    
 @IS_ACTIVE char(1),@CREATED_DATETIME datetime,@APLUS_REPORT_ORDERED int , @DRIVER_ID smallint , @DRIVER_NAME nvarchar(100),  @RELATIONSHIP int,     
 @CLAIMS_TYPE int, @AT_FAULT int,    @CHARGEABLE int, @LOSS_LOCATION varchar(70), @CAUSE_OF_LOSS varchar(50),  @LOSS_CARRIER  varchar(50)        
--if (@RISKID=1 or @RISKID=6)      
    
if (1=1)        
 begin         
 declare @tempPRIOR_LOSS_ID int ,@HOMELOSSID int       
 DECLARE CR_LOSS CURSOR FOR    
   Select LOSS_DATE, null CLAIM_DATE,CLMD.REF_LOOKUP_UNIQUEID LOSS_TYPE ,  0 AMOUNT_PAID,  0 AMOUNT_RESERVED, CLAIM_STATUS,   OC.LOSS_DESCRIPTION LOSS_DESC,  OC.OTHER_DESCRIPTION AS REMARKS ,      
  0 MOD, null LOSS_RUN, null CAT_NO, CLM.CLAIM_ID, CLM.IS_ACTIVE,    
  getdate() CREATED_DATETIME    ,  0 APLUS_REPORT_ORDERED , null DRIVER_ID , null DRIVER_NAME,  null RELATIONSHIP,  null CLAIMS_TYPE, null AT_FAULT,    null CHARGEABLE, OC.LOSS_LOCATION AS LOSS_LOCATION, null CAUSE_OF_LOSS,  null LOSS_CARRIER          
    from CLM_CLAIM_INFO  CLM    
  INNER JOIN CLM_OCCURRENCE_DETAIL OC ON CLM.CLAIM_ID=OC.CLAIM_ID    
  left outer join CLM_TYPE_DETAIL CLMD ON  dbo.instring(OC.LOSS_TYPE,CLMD.DETAIL_TYPE_ID )>0     
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID        
 OPEN CR_LOSS    
 FETCH NEXT  FROM CR_LOSS into @LOSS_DATE,@CLAIM_DATE ,@LOSS_TYPE ,@AMOUNT_PAID ,@AMOUNT_RESERVED , @CLAIM_STATUS,     
  @LOSS_DESC , @REMARKS ,  @MOD , @LOSS_RUN , @CAT_NO , @CLAIM_ID  ,@IS_ACTIVE,    
  @CREATED_DATETIME ,@APLUS_REPORT_ORDERED  , @DRIVER_ID  , @DRIVER_NAME ,  @RELATIONSHIP ,     
  @CLAIMS_TYPE , @AT_FAULT ,    @CHARGEABLE , @LOSS_LOCATION , @CAUSE_OF_LOSS ,  @LOSS_CARRIER          
    
 WHILE @@FETCH_STATUS = 0    
  BEGIN    
  select @tempPRIOR_LOSS_ID=isnull(max(PRIOR_LOSS_ID),0)+1 from PRIOR_LOSS_HOME        
  select @HOMELOSSID=isnull(max(LOSS_ID),0)+1 from PRIOR_LOSS_HOME where CUSTOMER_ID=@NEW_CUSTOMER_ID     
  select @LOSSID=ISNULL(max(LOSS_id),0)+1 from APP_PRIOR_LOSS_INFO where CUSTOMER_ID=@NEW_CUSTOMER_ID           
   INSERT INTO APP_PRIOR_LOSS_INFO            
     ( LOSS_ID, CUSTOMER_ID, OCCURENCE_DATE, CLAIM_DATE,    LOB,    LOSS_TYPE,    AMOUNT_PAID,    AMOUNT_RESERVED,    CLAIM_STATUS,    LOSS_DESC,    REMARKS,    MOD,    LOSS_RUN,    CAT_NO,    CLAIMID,    IS_ACTIVE,    CREATED_BY,     
     CREATED_DATETIME,   APLUS_REPORT_ORDERED,  DRIVER_ID,  DRIVER_NAME,  RELATIONSHIP,  CLAIMS_TYPE,  AT_FAULT,  CHARGEABLE,LOSS_LOCATION,  CAUSE_OF_LOSS,  POLICY_NUM,  LOSS_CARRIER )            
   Select @LOSSID,@NEW_CUSTOMER_ID,@LOSS_DATE, @CLAIM_DATE,  @RISKID,@LOSS_TYPE ,  @AMOUNT_PAID,  @AMOUNT_RESERVED, @CLAIM_STATUS, @LOSS_DESC,  @REMARKS ,  @MOD,  @LOSS_RUN, @CAT_NO, @CLAIM_ID, @IS_ACTIVE, @CREATED_BY ,    
   @CREATED_DATETIME    ,  @APLUS_REPORT_ORDERED  , @DRIVER_ID , @DRIVER_NAME, @RELATIONSHIP,@CLAIMS_TYPE, @AT_FAULT , @CHARGEABLE,@LOSS_LOCATION ,@CAUSE_OF_LOSS, @OLD_POLICY_NUMBER, @LOSS_CARRIER           
   /*insert into PRIOR_LOSS_HOME        
   (PRIOR_LOSS_ID  ,LOSS_ID ,CUSTOMER_ID  ,LOCATION_ID ,LOSS_ADD1    ,LOSS_ADD2   ,LOSS_CITY   ,LOSS_STATE  ,LOSS_ZIP    , CURRENT_ADD1,CURRENT_ADD2,CURRENT_CITY, CURRENT_STATE ,CURRENT_ZIP  , POLICY_TYPE  , POLICY_NUMBER  )        
   SELECT @tempPRIOR_LOSS_ID,@HOMELOSSID,@NEW_CUSTOMER_ID,0  ,ADDRESS1 ,ADDRESS2    ,CITY ,  null , ZIP     , '' , '' , ''     ,  null     , null , '' , @NEW_POLICY_NUMBER        
   from CLM_CLAIM_INFO  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID        
   */    
  FETCH NEXT  FROM CR_LOSS into @LOSS_DATE,@CLAIM_DATE ,@LOSS_TYPE ,@AMOUNT_PAID ,@AMOUNT_RESERVED , @CLAIM_STATUS,     
  @LOSS_DESC , @REMARKS ,  @MOD , @LOSS_RUN , @CAT_NO , @CLAIM_ID  ,@IS_ACTIVE,    
  @CREATED_DATETIME ,@APLUS_REPORT_ORDERED  , @DRIVER_ID  , @DRIVER_NAME ,  @RELATIONSHIP ,     
  @CLAIMS_TYPE , @AT_FAULT ,    @CHARGEABLE , @LOSS_LOCATION , @CAUSE_OF_LOSS ,  @LOSS_CARRIER      
 END --end while    
 CLOSE CR_LOSS    
 DEALLOCATE CR_LOSS    
 end   


  
-- FINALY UPDATE POLICY MAIN TABLE        
-- */
  
--Update data - add by Pradeep kushwaha on 06-july-2011
 
 --1.-Update POL_APPLICANT_LIST Data with New policy and policy version id
 UPDATE POL_APPLICANT_LIST  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID
 ,CREATED_BY = @CREATED_BY,CREATED_DATETIME = GETDATE()                        
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM   
 --2.-Update POL_LOCATIONS Data with New policy and policy version id
 UPDATE POL_LOCATIONS  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM   
 --3.-Update POL_CO_INSURANCE Data with New policy and policy version id
 UPDATE POL_CO_INSURANCE  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
 --4.-Update Remuneration Table  Data with New policy and policy version id
 UPDATE POL_REMUNERATION  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
 --5.-Update POL_CLAUSES Table  Data with New policy and policy version id
 UPDATE POL_CLAUSES  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
 --6.-Update POL_REINSURANCE_INFO Table  Data with New policy and policy version id
 UPDATE POL_REINSURANCE_INFO  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
 --7.-Update POL_DISCOUNT_SURCHARGE Table  Data with New policy and policy version id   
 UPDATE POL_DISCOUNT_SURCHARGE  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
 --8.-Update POL_PROTECTIVE_DEVICES Table  Data with New policy and policy version id   
 UPDATE POL_PROTECTIVE_DEVICES  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
 --8.-Update POL_PRODUCT_COVERAGES Table  Data with New policy and policy version id   
 UPDATE POL_PRODUCT_COVERAGES  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
 --9.-Update POL_BENEFICIARY Table  Data with New policy and policy version id   
 UPDATE POL_BENEFICIARY  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
------------------------------------
-------------------------------------
-----Copying Risk level Data      
    
-- All Risk and Named Perils Product,Engineering Risks    
 IF(@LOB_ID in (9,26))    
	 BEGIN    
		  UPDATE POL_PERILS  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
		  WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
		  SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
		  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
	 END   
----Comprehensive Condominium (10),Comprehensive Company(11),Diversified Risks(14),Robbery(16),Dwelling(19) and General Civil Liability(12)    
     
 ELSE IF( @LOB_ID IN(10,11,14,16,19,12,25,27,32))    
    BEGIN   
		 UPDATE POL_PRODUCT_LOCATION_INFO  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
		 WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
		 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
		 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
    END     
 --Facultative Liability(17)-,-Civil Liability Transportation(18)      
 ELSE IF (@LOB_ID IN (17,18,28,29,30,31,36))        
	BEGIN     
		UPDATE POL_CIVIL_TRANSPORT_VEHICLES  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
		WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
		SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
		IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
	END      
 -- MeriTIme (13)      
 ELSE IF (@LOB_ID = 13)        
	BEGIN
		UPDATE POL_MARITIME  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
		WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
		SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
		IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM
	END
-- National(20) and International(23) Cargo Transport      
 ELSE IF (@LOB_ID in(20,23))        
    BEGIN         
       UPDATE POL_COMMODITY_INFO  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
	   WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
	   SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
	   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM
	END    
-- Individual Personal Accident (15) , Group Passenger Personal Accident (21)       
 ELSE IF (@LOB_ID in (15,21,33,34))        
	BEGIN   
	  UPDATE POL_PERSONAL_ACCIDENT_INFO  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
	  WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
	  SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
	  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM
	END     
-- Passenger Personal Accident(22)
 ELSE IF (@LOB_ID = 22)        
	BEGIN        
	  UPDATE POL_PASSENGERS_PERSONAL_ACCIDENT_INFO  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
	  WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
	  SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
	  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM
	END 
 -- PENHOR_RURAL ,RENTAL SURETY
 ELSE IF (@LOB_ID in (35,37))        
    BEGIN 
	  UPDATE POL_PENHOR_RURAL_INFO  SET CUSTOMER_ID=@NEW_CUSTOMER_ID ,POLICY_ID=@NEW_POLICY_ID,POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID                       
	  WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_VERSION_ID 
	  SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
	  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM
    END
--Update Risk tables till  here 
 DELETE FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@NEW_VERSION_ID 
 
 UPDATE POL_CUSTOMER_POLICY_LIST     
 SET POLICY_STATUS  = null,    
 APP_STATUS='APPLICATION',
 POLICY_NUMBER   = NULL, 
 APP_NUMBER=@NEW_APP_NUMBER,
 POLICY_ID=@NEW_POLICY_ID,
 POLICY_VERSION_ID=@NEW_POLICY_VERSION_ID,
 POLICY_DISP_VERSION='1.0',   
 APP_VERIFICATION_XML = null,    
 APP_ID     = @NEW_POLICY_ID,    
 APP_VERSION_ID   = @NEW_POLICY_VERSION_ID,    
 --IS_REWRITE_POLICY  = 'Y' ,    
 CURRENT_TERM   =1  -- same as in case of new businesss    
 ,IS_ACTIVE = 'Y' --Added by Lalit For Copy Policy as Active.itrack # 1496.
 WHERE CUSTOMER_ID=@NEW_CUSTOMER_ID AND POLICY_ID=@NEW_POLICY_ID and POLICY_VERSION_ID=@NEW_POLICY_VERSION_ID  
 
 /* NBS Launch process if needed 
 --Insert into POL_POLICY_PROCESS on NBS Launch
INSERT INTO POL_POLICY_PROCESS(CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,ROW_ID,PROCESS_ID,
	PROCESS_TYPE,NEW_CUSTOMER_ID,NEW_POLICY_ID,NEW_POLICY_VERSION_ID,POLICY_PREVIOUS_STATUS,
	POLICY_CURRENT_STATUS,PROCESS_STATUS,CREATED_BY,CREATED_DATETIME,COMPLETED_BY,	
	COMPLETED_DATETIME,COMMENTS,PRINT_COMMENTS,REQUESTED_BY,EFFECTIVE_DATETIME,	
	EXPIRY_DATE,CANCELLATION_OPTION,CANCELLATION_TYPE,REASON,OTHER_REASON,
	RETURN_PREMIUM,PAST_DUE_PREMIUM,ENDORSEMENT_NO,PROPERTY_INSPECTION_CREDIT,POLICY_TERMS,	
	RETURN_OTHER_FEE_AMOUNT,PRINTING_OPTIONS,INSURED,SEND_INSURED_COPY_TO,AUTO_ID_CARD,	
	NO_COPIES,STD_LETTER_REQD,CUSTOM_LETTER_REQD,ADD_INT,ADD_INT_ID,
	SEND_ALL,AGENCY_PRINT,OTHER_RES_DATE_CD,OTHER_RES_DATE,INTERNAL_CHANGE,
	WRITTEN_OFF_PREMIUM,COINSURANCE_NUMBER,ENDORSEMENT_TYPE,ENDORSEMENT_OPTION,SOURCE_VERSION_ID,CO_APPLICANT_ID)
VALUES(@CUSTOMER_ID,@NEW_POLICY_ID,@NEW_POLICY_VERSION_ID,1,24,
	   NULL,@CUSTOMER_ID,@NEW_POLICY_ID,@NEW_POLICY_VERSION_ID,'Suspended',
	   NULL,'PENDING',@CREATED_BY,GETDATE(),0,
	   NULL,NULL,NULL,0,GETDATE(),
	   NULL,0,0,0,NULL,
	   0.00,0.00,0,NULL,0,
	   NULL,0,11980,0,11980,
	   0,0,0,11980,NULL,
	   0,11980,NULL,NULL,NULL,
       NULL,NULL,0,0,0,NULL)
 
*/
  



 /*   
----------------
--------------

DELETE FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@NEW_VERSION_ID                                   
--UPDATING APP LIST     
 UPDATE APP_LIST   SET APP_NUMBER='',APP_VERIFICATION_XML=null--,APP_AGENCY_ID= @NEW_AGENCY_ID     
 WHERE   CUSTOMER_ID=@NEW_CUSTOMER_ID AND APP_ID=@NEW_APP_ID and APP_VERSION_ID=1    
   
--UPDATE STATUS AND INSURANCE AND OYHER INFO OF NEW VERSION TO SUSPENDED aNd new policy number        
DECLARE  @CUSTOMER_INSURANCE_SCORE INT,    
   @CUSTOMER_REASON_CODE     NVARCHAR(10),    
   @CUSTOMER_REASON_CODE2  NVARCHAR(10),    
   @CUSTOMER_REASON_CODE3  NVARCHAR(10),    
   @CUSTOMER_REASON_CODE4  NVARCHAR(10)    
    
SELECT     
@CUSTOMER_INSURANCE_SCORE = CUSTOMER_INSURANCE_SCORE,    
@CUSTOMER_REASON_CODE  = CUSTOMER_REASON_CODE,    
@CUSTOMER_REASON_CODE2  = CUSTOMER_REASON_CODE2,    
@CUSTOMER_REASON_CODE3  = CUSTOMER_REASON_CODE3,    
@CUSTOMER_REASON_CODE4  = CUSTOMER_REASON_CODE4    
 FROM CLT_CUSTOMER_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@NEW_CUSTOMER_ID     
  
UPDATE POL_CUSTOMER_POLICY_LIST     
 SET POLICY_STATUS  = 'SUSPENDED',    
 POLICY_NUMBER   = @NEW_POLICY_NUMBER, 
 APP_NUMBER=@NEW_APP_NUMBER,
 POLICY_ID=@NEW_POLICY_ID,
 POLICY_VERSION_ID=@NEW_POLICY_VERSION_ID,
    
 APP_VERIFICATION_XML = null,    
 APP_ID     = @NEW_POLICY_ID,    
 APP_VERSION_ID   = @NEW_POLICY_VERSION_ID,    
 IS_REWRITE_POLICY  = 'Y' ,    
 POL_VER_EFFECTIVE_DATE = APP_EFFECTIVE_DATE,    
 POL_VER_EXPIRATION_DATE = APP_EXPIRATION_DATE ,    
 APPLY_INSURANCE_SCORE = @CUSTOMER_INSURANCE_SCORE,    
 CUSTOMER_REASON_CODE = @CUSTOMER_REASON_CODE,    
 CUSTOMER_REASON_CODE2 = @CUSTOMER_REASON_CODE2,    
 CUSTOMER_REASON_CODE3 = @CUSTOMER_REASON_CODE3,    
 CUSTOMER_REASON_CODE4 = @CUSTOMER_REASON_CODE4,    
 CURRENT_TERM   =1  -- same as in case of new businesss    
    
 WHERE CUSTOMER_ID=@NEW_CUSTOMER_ID AND POLICY_ID=@NEW_POLICY_ID and POLICY_VERSION_ID=@NEW_POLICY_VERSION_ID                                   
  
    */
SELECT @TEMP_ERROR_CODE = @@ERROR               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM    
   COMMIT TRAN                        
 --  SET @NEW_VERSION=@POLICY_NEW_VERSION                                                                     
 --  SET  @NEW_DISP_VERSION =@POLICY_DISP_VERSION                  
   RETURN @NEW_VERSION_ID                             
                                            
  PROBLEM:                                                
   ROLLBACK TRAN                                              
   SET @NEW_VERSION_ID = -1                                                         
   RETURN @NEW_VERSION_ID                                         
        
        
END        
        
-- go       
--declare        
-- @NEW_POLICY_VERSION_ID int ,        
-- @TRAN_DESC VARCHAR(1000),        
-- @NEW_POLICY_ID INT        
-- EXEC Proc_TransferPolicy 28241,194,2,28241,396,@NEW_POLICY_ID OUT ,@NEW_POLICY_VERSION_ID out,@TRAN_DESC out ,3,3,2     
-- SELECT POLICY_DISP_VERSION,POLICY_STATUS,APP_STATUS,* FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = 28241 and POLICY_ID = 194 
-- SELECT POLICY_DISP_VERSION,POLICY_STATUS,APP_STATUS,* FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = 28241 and POLICY_ID = 195 
 
-- ROLLBACK TRAN      
 

GO

