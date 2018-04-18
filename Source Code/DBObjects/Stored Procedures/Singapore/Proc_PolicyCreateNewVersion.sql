          
          
                
--begin tran                  
--drop proc dbo.Proc_PolicyCreateNewVersion                       
--go                  
          
/*----------------------------------------------------------                                                              
Proc Name       : dbo.Proc_PolicyCreateNewVersion                                                              
                  
Modified by   : Ravindra Gupta                                           
Modified On   : 05-22-2006                                          
Purpose       : Changes to prevent copying of ineligible coverages while renewal (Done for Home & Rental)                                      
                                  
Modified by   : PRAVESH CHANDEL                                           
Modified On   : 18 OCT-2006                                          
Purpose       : ADD UMBRELLA COVERAGES TO BE COPIED IN NEW VERSION                                  
                                
Modified by   : Ravindra Gupta                                           
Modified On   : 11-02-2006                                
Purpose       : Optimisation of code                                
                                    
Modified by   : Pravesh K. Chandel                                
Modified On   : 7 feb-2007                                
Purpose       : Copy only enabled Endorsement Attachemnt                                
                                
Modified by   : Pravesh K Chandel                              
Modified On   : 20/04/2007                                
Purpose       : copying BIlling Info form ACT_POL_EFT_CUST_INFO                            
Modified by   : Pravesh K Chandel                              
Modified On   : 3rd july 2007                                
Purpose       : copying  POL_OTHER_STRUCTURE_DWELLING                        
                        
Modified by   : Ravindra Gupta                                           
Modified On   : 09-05-2007                        
Purpose       : Change for rewrite process, Current_term will be incremented in Rewrite process                        
                      
Modified by   : Praveen kasana                      
Modified On   : 18-09-2007                        
Purpose       : Added Boat Assgned                       
                      
Modified by   : Pravesh k Chandel                      
Modified On   : 3 oct 2007                      
Purpose       : if Mortgagee bill then bill plan to Full pay Plan                      
                      
Modified by   : Praveen kasana                      
Modified On   : 13 Feb 2008                      
Purpose       : In case of Renewal and Rewrite the Complete App Bonus will not be Copied.                      
                    
Modified by   : Praveen kasana                      
Modified On   : 17 March 2008                   
Purpose       : Insurance score Reasons 1,2,3,4 Copied. (For Renewal = 1)                    
                    
Modified by   : Praveen kasana                      
Modified On   : 17 March 2008                      
Purpose       : Insurance score Reasons 1,2,3,4 Copied. (For Endorsement)                    
                  
Modified by   : Praveen kasana                      
Modified On   : 29 Dec 2009                  
Purpose       : COPYING  Underwriting Tier                       
                
Modified by   : Lalit Chauhan                 
Modified On   : 13 Dec 2010                  
Purpose       : Copy Risk For NewProduct                
---------------------------------------------------------- */                                                    
            /*DECLARE     @A VARCHAR(50)                         
            EXEC Proc_PolicyCreateNewVersion 2156,416,1,398,@A OUT                
                            
            */                
-- drop proc dbo.Proc_PolicyCreateNewVersion     2156,                                       
                        
alter PROC [dbo].[Proc_PolicyCreateNewVersion]           
(                                                                          
 @CUSTOMER_ID int,                            
 @POLICY_ID  int,                                                                          
 @POLICY_VERSION_ID smallint,                                                                
 @CREATED_BY int,                      
 @NEW_VERSION  INT OUTPUT,                                      
 --RPSINGH for Making new version in case of Renewal                                       
 @RENEWAL Int = 0 ,   -- In case  of Renewal 1 will be passed in case of Rewrite 3 will be passed else 0                              
 @NEW_DISP_VERSION  NVARCHAR(50) =null output  ,                                
 @TRAN_DESC nvarchar(2000) =null output,                              
 @INVALID_COVERAGE int = null output ,                        
 @COVERAGE_BASE_CHANGED_BOAT_ID varchar(100) =null out ,                      
 @NEW_DISP_VERSION_REWRITABLE   NVARCHAR(50) =null output ,          
 @CALLED_FROM NVARCHAR(15) = NULL                           
)                                                                   
AS                                                                          
BEGIN                                
                        
BEGIN TRAN                                                       
DECLARE @TEMP_ERROR_CODE INT                                           
                                      
                                
-- Added By Ravindra(05-22-2006)                                          
DECLARE @NEW_APP_EFFECTIVE_DATE DATETIME                                       
DECLARE @HAS_INVALID_COVERAGE INT                          
DECLARE @HAS_INVALID_COVERAGE_LIMIT INT                                  
                
                                
SET @HAS_INVALID_COVERAGE =0                            
SET @HAS_INVALID_COVERAGE_LIMIT =0                                    
set @INVALID_COVERAGE =0                                
SET @TRAN_DESC=''                          
set @COVERAGE_BASE_CHANGED_BOAT_ID=''                              
CREATE TABLE #IN_EFFECTIVE_COVERAGES                                     
(                                                      
 COV_ID INT                                                      
)                                         
                                
CREATE TABLE #IN_EFFECTIVE_COVERAGE_RANGES                                             
(                                         
LIMIT_DEDUC_ID INT                                                      
)                                                         
-- Added By Ravindra(05-25-2006)                                          
CREATE TABLE #IN_EFFECTIVE_ENDORSMENT                                
(                                      
 ENDORSMENT_ID INT                                                      
)                                         
-- Added By Ravindra Ends here                                       
-- Added By PRAVESHR (07 FEB-2007)                                          
CREATE TABLE #IN_EFFECTIVE_ENDORSMENT_ATTACHMENT                                  
(                                   
 ENDORSEMENT_ATTACH_ID INT                                                      
)                                         
-- Added By PRAVESH Ends here                                       
 CREATE TABLE ##TMP_MULTIPOLICY                      
 (                      
 POLICY_NUMBER varChar(15)                      
 )                               
-- Get Policy New Version Number                                               
DECLARE @POLICY_NEW_VERSION smallint                                                  
DECLARE @POLICY_DISP_VERSION smallint                                    
                                
SET @POLICY_NEW_VERSION = (SELECT MAX(ISNULL(POLICY_VERSION_ID,0))+1 FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID)                                                                          
SET @NEW_DISP_VERSION_REWRITABLE =                   
 (SELECT CONVERT(DECIMAL(5,0),ISNULL(POLICY_DISP_VERSION,0))+1                   
 FROM POL_CUSTOMER_POLICY_LIST with(nolock)                   
 WHERE CUSTOMER_ID=@CUSTOMER_ID                   
 AND POLICY_ID=@POLICY_ID                   
 AND POLICY_VERSION_ID = @POLICY_VERSION_ID                  
 )                                 
                  
IF (@RENEWAL = 3 )                              
BEGIN                             
 SET @POLICY_DISP_VERSION = 1          
END                              
ELSE                              
BEGIN                        
                
 SET @POLICY_DISP_VERSION = (SELECT CONVERT(DECIMAL(5,0),ISNULL(POLICY_DISP_VERSION,0)) FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)                        
             
END                              
 -- Get Current date and time to update for Created_Datetime field                                                                 
Declare @Date varchar(50)                                                  
set @Date = CONVERT(VARCHAR(50),GETDATE(),100)                                                                          
                                
/* Get LOB of Current Policy */                                           
Declare @RISKID int                                                                          
declare @BILL_TYPE_ID int                              
declare @NEW_BILL_TYPE_ID int                             
declare @BILL_TYPE char(2)                                
Declare @APP_INCEPTION_DATE DateTime                          
Declare @APP_EFFECTIVE_DATE DateTime                                      
Declare @APP_EXPIRATION_DATE DateTime                                
DECLARE @CURRENT_TERM SmallInt                        
DECLARE @POLICY_TERMS nvarchar(5)                                  
Declare @NEW_APP_EXPIRATION_DATE DateTime                                
DECLARE @NEW_POLICY_TERMS nvarchar(5)                                
declare @INSTALL_PLAN_ID int                                 
declare @DOWN_PAY_MODE int                                
declare @NEW_INSTALL_PLAN_ID int                                 
declare @NEW_DOWN_PAY_MODE int                                
                       
declare @INSTALL_PLAN varchar(50)                                 
declare @DOWN_PAY_MODE_DESC varchar(50)                                  
declare @NEW_INSTALL_PLAN varchar(50)                                  
declare @NEW_DOWN_PAY_MODE_DESC varchar(50)                                 
declare @NO_OF_YEAR_WITH_WOL smallint                      
declare @POLICY_NUMBER varchar(50)                         
declare @AGENCY_ID        int                      
DECLARE @MAXCLAUSE_ID INT                  
                      
DECLARE @MULTI_POLICY_NUMBER VARCHAR(15)                      
DECLARE @MULTI_POLICY_COUNT  INT                      
DECLARE @TRAILBALZER_EXPIRY_DATE DATETIME,          
@PLAN_TYPE NVARCHAR(10)  = ''          
SET @TRAILBALZER_EXPIRY_DATE = '01/01/2010'                       
set @NO_OF_YEAR_WITH_WOL=0                               
SELECT  @RISKID = POLICY_LOB,                                 
 @BILL_TYPE = BILL_TYPE,                                
 @BILL_TYPE_ID=BILL_TYPE_ID ,                                
 @APP_INCEPTION_DATE = APP_INCEPTION_DATE ,                                 
 @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE ,                                 
 @APP_EXPIRATION_DATE = APP_EXPIRATION_DATE,                                
 @CURRENT_TERM  = CURRENT_TERM  ,                                
 @POLICY_TERMS   = APP_TERMS,                                
 @INSTALL_PLAN_ID =INSTALL_PLAN_ID,                                
 @DOWN_PAY_MODE =DOWN_PAY_MODE ,                      
@POLICY_NUMBER=POLICY_NUMBER ,                      
@AGENCY_ID=AGENCY_ID                              
 FROM POL_CUSTOMER_POLICY_LIST with(nolock)                   
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                          
             
/* COPY THE COMMON DATA */                              
--1.                               
SELECT * INTO #POL_CUSTOMER_POLICY_LIST FROM POL_CUSTOMER_POLICY_LIST with(nolock)                   
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                 
                                
SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                         
                                      
                                      
UPDATE #POL_CUSTOMER_POLICY_LIST                                               
SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,                                              
--POLICY_DISP_VERSION = CONVERT(VARCHAR(10), @POLICY_DISP_VERSION) + '.0' ,                                  
CREATED_BY = @CREATED_BY,                                           
CREATED_DATETIME = @DATE,                                
                                
POL_VER_EFFECTIVE_DATE=@APP_EFFECTIVE_DATE ,                                
POL_VER_EXPIRATION_DATE=@APP_EXPIRATION_DATE,                                
IS_YEAR_WITH_WOL_UPDATED=null                  
-- In case of rewrite Policy Number will be generated at the time of commit            
--get plan type from act_install_plan_detail table          
          
SELECT @PLAN_TYPE = ISNULL(PLAN_TYPE,'') FROM ACT_INSTALL_PLAN_DETAIL WITH(NOLOCK) WHERE IDEN_PLAN_ID = @INSTALL_PLAN_ID          
          
          
DECLARE @PRODUCT_SUSEP_NUMBER nvarchar(10)             
                            
IF (@RENEWAL = 3 )                              
BEGIN                       
 SET @CURRENT_TERM = @CURRENT_TERM + 1                               
 UPDATE #POL_CUSTOMER_POLICY_LIST    SET OLD_POLICY_NUMBER = POLICY_NUMBER,POLICY_NUMBER = 'To be generated' ,--modified by Lalit May 5,2011.old policy no is copied in different field for in  re-write case ,i-track# 945          
                    
CURRENT_TERM = @CURRENT_TERM     --Ravindra(09-05-2007)                         
                      
--In case of Rewrite Complete AppBonus not to be Copied -- Added by Praveen (02/13/2008)                       
UPDATE #POL_CUSTOMER_POLICY_LIST SET COMPLETE_APP = NULL                      
                        
END                       
                    
--By praveen                    
DECLARE @APPLY_INSURANCE_SCORE NUMERIC                      
DECLARE @CUSTOMER_REASON_CODE nvarchar(10)                      
DECLARE @CUSTOMER_REASON_CODE2 nvarchar(10)                      
DECLARE @CUSTOMER_REASON_CODE3 nvarchar(10)                      
DECLARE @CUSTOMER_REASON_CODE4 nvarchar(10)                    
                 
                       
--end                               
                                 
if @Renewal = 1                                      
Begin                                
  set @NEW_INSTALL_PLAN_ID=@INSTALL_PLAN_ID                                
  set @NEW_DOWN_PAY_MODE =@DOWN_PAY_MODE                         
  set @NEW_BILL_TYPE_ID  =@BILL_TYPE_ID                        
-- by pravesh                                    
 --Amend the term dates - If less than 1 year make it an annual policy                                
 --in case of LOBs Home,Rental,MotorCycle incase of Rental Change term date if term is 6 months                                
 --1 HOME Homeowners                    
 --2 AUTOP Automobile                                
 --3 CYCL Motorcycle                                
 --4 BOAT Watercraft                                
 --5 UMB Umbrella                               
 --6 REDW Rental                                
--  Set @NEW_APP_EXPIRATION_DATE=DateAdd(day,datediff(day,@APP_EFFECTIVE_DATE,@APP_EXPIRATION_DATE)+1,@APP_EXPIRATION_DATE)                                
if (@POLICY_TERMS=6)                              
  Set @NEW_APP_EXPIRATION_DATE=DateAdd(mm,6,@APP_EXPIRATION_DATE)                                
else if (@POLICY_TERMS=12)                              
  Set @NEW_APP_EXPIRATION_DATE=DateAdd(mm,12,@APP_EXPIRATION_DATE)                                
else                             
  Set @NEW_APP_EXPIRATION_DATE=DateAdd(day,datediff(day,@APP_EFFECTIVE_DATE,@APP_EXPIRATION_DATE),@APP_EXPIRATION_DATE)                                
                              
  SET @NEW_POLICY_TERMS=@POLICY_TERMS                          
  --SET @TRAN_DESC=''                                
 if (@RISKID=6 and @POLICY_TERMS=6)  -- for rental                                
 begin          
  Set @NEW_APP_EXPIRATION_DATE=DateAdd(mm,12,@APP_EXPIRATION_DATE)                                
  SET @NEW_POLICY_TERMS=12  --Anualy/Yearly                                
  SET @TRAN_DESC=@TRAN_DESC + 'Term has been changed from 6 months to Yearly and Term dates also changed according to Yearly mode;'                                
 end                                
 if ((@RISKID=1 or @RISKID=3) and @POLICY_TERMS<12) --for Home and Motor                                
 begin                                            
  Set @NEW_APP_EXPIRATION_DATE=DateAdd(mm,12,@APP_EXPIRATION_DATE)                                
  SET @NEW_POLICY_TERMS=12  --Anualy/Yearly                                
  SET @TRAN_DESC= @TRAN_DESC + 'Term has been changed from ' + convert(varchar,@POLICY_TERMS) +' month(s) to Yearly and Term dates also changed according to Yearly mode;'                                
 end                                
 --by pravesh end here                        
               
 IF  (@BILL_TYPE_ID=11191 or @BILL_TYPE_ID=8459 OR @BILL_TYPE_ID=11277 OR @BILL_TYPE_ID = 11278)       --AB to DB                       
 BEGIN                              
  IF(@BILL_TYPE_ID=11191 or @BILL_TYPE_ID=8459 )-- AGENCY BILL 1ST TERM / INSURED BILL @RENEWAL  or Agency Bill all terms                               
     BEGIN                                 
      if (@RISKID=1 or @RISKID=6)                               
       SET @NEW_BILL_TYPE_ID=11150                               
      else                               
       SET @NEW_BILL_TYPE_ID=8460                            
                               
      SET @BILL_TYPE = (SELECT TYPE FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_UNIQUE_ID=8460)                                
         if(@BILL_TYPE_ID=11191)                              
       set  @TRAN_DESC =@TRAN_DESC + '"Agency Bill 1st term / Insured Bill @renewal" Changed to "Insured Bill" and Bill Type to "'+ @BILL_TYPE + '".;'                                
         else                              
       set  @TRAN_DESC =@TRAN_DESC + '"Agency Bill all terms" Changed to "Insured Bill" and Bill Type to "'+ @BILL_TYPE + '".;'                                  
     END                                  
  IF(@BILL_TYPE_ID=11277 OR @BILL_TYPE_ID = 11278)-- Agency Bill 1st term/Mortgagee @renewal (11277), Insured Bill 1st term/Mortgagee @renewal (11278)                                
     BEGIN                                 
     SET @NEW_BILL_TYPE_ID=11276                                
     SET @BILL_TYPE = (SELECT TYPE FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_UNIQUE_ID=11276)                            
     IF(@BILL_TYPE_ID=11277)                                
      set  @TRAN_DESC =@TRAN_DESC + '"Agency Bill 1st term/Mortgagee @renewal" Changed to "Mortgagee Bill from Inception"  and Bill Type to "' + @BILL_TYPE + '".;'                                
     else                   
      set  @TRAN_DESC =@TRAN_DESC + '"Insured Bill 1st term/Mortgagee @renewal" changed to "Mortgagee Bill from Inception".;'  --and Bill Type to "' + @BILL_TYPE + '".;'                                
 --fetching Full pay Plan                      
  SELECT @NEW_INSTALL_PLAN_ID=IDEN_PLAN_ID,@NEW_DOWN_PAY_MODE = case when isnull(MODE_OF_DOWNPAY_RENEW,0) >0 then MODE_OF_DOWNPAY_RENEW                         
    else case when isnull(MODE_OF_DOWNPAY_RENEW1,0) >0 then MODE_OF_DOWNPAY_RENEW1                         
    else MODE_OF_DOWNPAY_RENEW2 end  end                         
  FROM ACT_INSTALL_PLAN_DETAIL with(nolock) WHERE isnull(SYSTEM_GENERATED_FULL_PAY,0)=1                       
    END                       
   if (@NEW_INSTALL_PLAN_ID is null or @NEW_INSTALL_PLAN_ID=0)                                
     begin                                
     set @NEW_INSTALL_PLAN_ID=@INSTALL_PLAN_ID                                
     set @NEW_DOWN_PAY_MODE =@DOWN_PAY_MODE                       
     end                               
   else                              
     begin                               
    select @DOWN_PAY_MODE_DESC = LOOKUP_VALUE_DESC   FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_UNIQUE_ID=  @DOWN_PAY_MODE                              
    select @NEW_DOWN_PAY_MODE_DESC = LOOKUP_VALUE_DESC   FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_UNIQUE_ID = @NEW_DOWN_PAY_MODE                              
     SELECT   @NEW_INSTALL_PLAN= ISNULL(INST_PLAN.PLAN_DESCRIPTION,' ') + ' (' + ISNULL(INST_PLAN.PLAN_CODE,' ') + ')'                               
       FROM ACT_INSTALL_PLAN_DETAIL INST_PLAN with (nolock)where IDEN_PLAN_ID=@NEW_INSTALL_PLAN_ID                        
 SELECT   @INSTALL_PLAN=ISNULL(INST_PLAN.PLAN_DESCRIPTION,' ') + ' (' + ISNULL(INST_PLAN.PLAN_CODE,' ') + ')'                               
       FROM ACT_INSTALL_PLAN_DETAIL INST_PLAN with (nolock) where IDEN_PLAN_ID=@INSTALL_PLAN_ID                              
        set  @TRAN_DESC = isnull(@TRAN_DESC,'') + ' Billing Plan Changed from "' + isnull(@INSTALL_PLAN,'Not Applicable') + '" to "' + isnull(@NEW_INSTALL_PLAN,'') + '".;'                              
      if (@NEW_DOWN_PAY_MODE !=@DOWN_PAY_MODE)                              
        set  @TRAN_DESC = isnull(@TRAN_DESC,'') + 'Down Payment Mode Changed from "' + isnull(@DOWN_PAY_MODE_DESC,'Not Applicable') + '" to "' + isnull(@NEW_DOWN_PAY_MODE_DESC,'') + '".;'                               
     end                                
END --END FOR BILL TYPE CHANGE                               
ELSE  --if DB to DB                      
BEGIN              
 --fetching defalut plan and default payment mode                      
 SET @NEW_INSTALL_PLAN_ID=0                      
 SET @NEW_DOWN_PAY_MODE=0                      
                    
 if not exists(                    
  SELECT IDEN_PLAN_ID  FROM ACT_INSTALL_PLAN_DETAIL with(nolock)                    
  WHERE (APPLABLE_POLTERM=@NEW_POLICY_TERMS  OR APPLABLE_POLTERM = 0) AND IDEN_PLAN_ID =@INSTALL_PLAN_ID)                      
  SELECT @NEW_INSTALL_PLAN_ID=IDEN_PLAN_ID,@NEW_DOWN_PAY_MODE = case when isnull(MODE_OF_DOWNPAY_RENEW,0) >0 then MODE_OF_DOWNPAY_RENEW                         
  else case when isnull(MODE_OF_DOWNPAY_RENEW1,0) >0 then MODE_OF_DOWNPAY_RENEW1                         
  else isnull(MODE_OF_DOWNPAY_RENEW2,0) end  end                         
  FROM ACT_INSTALL_PLAN_DETAIL with(nolock)WHERE APPLABLE_POLTERM=@NEW_POLICY_TERMS AND DEFAULT_PLAN=1 and isnull(IS_ACTIVE,'Y')='Y'                             
 else                      
  set @NEW_INSTALL_PLAN_ID=@INSTALL_PLAN_ID                      
  if (@NEW_INSTALL_PLAN_ID is null or @NEW_INSTALL_PLAN_ID=0)                                
  SELECT @NEW_INSTALL_PLAN_ID=IDEN_PLAN_ID,@NEW_DOWN_PAY_MODE = case when isnull(MODE_OF_DOWNPAY_RENEW,0) >0 then MODE_OF_DOWNPAY_RENEW                         
  else case when isnull(MODE_OF_DOWNPAY_RENEW1,0) >0 then MODE_OF_DOWNPAY_RENEW1                         
  else MODE_OF_DOWNPAY_RENEW2 end  end                         
  FROM ACT_INSTALL_PLAN_DETAIL with(nolock) WHERE isnull(SYSTEM_GENERATED_FULL_PAY,0)=1                       
  --fetching renewal downpay mode                      
                   
 SELECT  @NEW_DOWN_PAY_MODE = case  when @DOWN_PAY_MODE=isnull(MODE_OF_DOWNPAY_RENEW,0) or @DOWN_PAY_MODE=isnull(MODE_OF_DOWNPAY_RENEW1,0) or @DOWN_PAY_MODE=isnull(MODE_OF_DOWNPAY_RENEW2,0) then @DOWN_PAY_MODE                    
else                   
 case when isnull(MODE_OF_DOWNPAY_RENEW,0) >0 then MODE_OF_DOWNPAY_RENEW                         
 else case when isnull(MODE_OF_DOWNPAY_RENEW1,0) >0 then MODE_OF_DOWNPAY_RENEW1                         
 else isnull(MODE_OF_DOWNPAY_RENEW2,0) end  end                         
 end                      
 FROM ACT_INSTALL_PLAN_DETAIL with(nolock)WHERE IDEN_PLAN_ID =@NEW_INSTALL_PLAN_ID                      
                    
                         
   if (@NEW_INSTALL_PLAN_ID is null or @NEW_INSTALL_PLAN_ID=0)                           
     begin                                
     set @NEW_INSTALL_PLAN_ID=@INSTALL_PLAN_ID                                
     set @NEW_DOWN_PAY_MODE =@DOWN_PAY_MODE                                
     end                               
   else                      
     begin                               
     select @DOWN_PAY_MODE_DESC = LOOKUP_VALUE_DESC   FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_UNIQUE_ID=  @DOWN_PAY_MODE                              
     select @NEW_DOWN_PAY_MODE_DESC = LOOKUP_VALUE_DESC   FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_UNIQUE_ID = @NEW_DOWN_PAY_MODE                              
      SELECT   @NEW_INSTALL_PLAN= ISNULL(INST_PLAN.PLAN_DESCRIPTION,' ') + ' (' + ISNULL(INST_PLAN.PLAN_CODE,' ') + ')'                               
         FROM ACT_INSTALL_PLAN_DETAIL INST_PLAN with (nolock)where IDEN_PLAN_ID=@NEW_INSTALL_PLAN_ID                        
     SELECT   @INSTALL_PLAN=ISNULL(INST_PLAN.PLAN_DESCRIPTION,' ') + ' (' + ISNULL(INST_PLAN.PLAN_CODE,' ') + ')'                               
         FROM ACT_INSTALL_PLAN_DETAIL INST_PLAN with (nolock) where IDEN_PLAN_ID=@INSTALL_PLAN_ID                       
        if (@NEW_INSTALL_PLAN_ID !=@INSTALL_PLAN_ID)                              
  set  @TRAN_DESC = isnull(@TRAN_DESC,'') + ' Billing Plan Changed from "' + isnull(@INSTALL_PLAN,'Not Applicable') + '" to "' + isnull(@NEW_INSTALL_PLAN,'') + '".;'                              
 if (@NEW_DOWN_PAY_MODE !=@DOWN_PAY_MODE)                              
    set  @TRAN_DESC = isnull(@TRAN_DESC,'') + 'Down Payment Mode Changed from "' + isnull(@DOWN_PAY_MODE_DESC,'Not Applicable') + '" to "' + isnull(@NEW_DOWN_PAY_MODE_DESC,'') + '".;'                               
   end                                
 END  --END FOR BILL TYPE CHANGE else part                      
                             
--check whether install Plan deactivated   ADDED BY PRAVESH                            
            
   IF NOT EXISTS(SELECT IDEN_PLAN_ID FROM ACT_INSTALL_PLAN_DETAIL with(nolock) WHERE IDEN_PLAN_ID=@NEW_INSTALL_PLAN_ID AND ISNULL(IS_ACTIVE,'Y')='Y')                      
   BEGIN                      
  --fetching defalut plan and default payment mode                      
 SET @NEW_INSTALL_PLAN_ID=0                      
 SET @NEW_DOWN_PAY_MODE=0                      
   SELECT @NEW_INSTALL_PLAN_ID=ISNULL(IDEN_PLAN_ID,0),@NEW_DOWN_PAY_MODE = case when isnull(MODE_OF_DOWNPAY_RENEW,0) >0 then MODE_OF_DOWNPAY_RENEW                         
     else case when isnull(MODE_OF_DOWNPAY_RENEW1,0) >0 then MODE_OF_DOWNPAY_RENEW1                         
     else isnull(MODE_OF_DOWNPAY_RENEW2,0) end  end                         
    FROM ACT_INSTALL_PLAN_DETAIL with(nolock)WHERE APPLABLE_POLTERM=@NEW_POLICY_TERMS AND DEFAULT_PLAN=1 and isnull(IS_ACTIVE,'Y')='Y'                             
  if (@NEW_INSTALL_PLAN_ID is null or @NEW_INSTALL_PLAN_ID=0)                                
   SELECT @NEW_INSTALL_PLAN_ID=IDEN_PLAN_ID,@NEW_DOWN_PAY_MODE = case when isnull(MODE_OF_DOWNPAY_RENEW,0) >0 then MODE_OF_DOWNPAY_RENEW                         
     else case when isnull(MODE_OF_DOWNPAY_RENEW1,0) >0 then MODE_OF_DOWNPAY_RENEW1                         
     else MODE_OF_DOWNPAY_RENEW2 end  end                         
    FROM ACT_INSTALL_PLAN_DETAIL with(nolock) WHERE isnull(SYSTEM_GENERATED_FULL_PAY,0)=1                       
  select @DOWN_PAY_MODE_DESC = LOOKUP_VALUE_DESC   FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_UNIQUE_ID=  @DOWN_PAY_MODE                              
     select @NEW_DOWN_PAY_MODE_DESC = LOOKUP_VALUE_DESC   FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_UNIQUE_ID = @NEW_DOWN_PAY_MODE                              
      SELECT   @NEW_INSTALL_PLAN= ISNULL(INST_PLAN.PLAN_DESCRIPTION,' ') + ' (' + ISNULL(INST_PLAN.PLAN_CODE,' ') + ')'                               
        FROM ACT_INSTALL_PLAN_DETAIL INST_PLAN with (nolock)where IDEN_PLAN_ID=@NEW_INSTALL_PLAN_ID                              
     SELECT @INSTALL_PLAN=ISNULL(INST_PLAN.PLAN_DESCRIPTION,' ') + ' (' + ISNULL(INST_PLAN.PLAN_CODE,' ') + ')'                   
        FROM ACT_INSTALL_PLAN_DETAIL INST_PLAN with (nolock) where IDEN_PLAN_ID=@INSTALL_PLAN_ID                       
        set  @TRAN_DESC = isnull(@TRAN_DESC,'') + ' Billing Plan Changed from "' + isnull(@INSTALL_PLAN,'Not Applicable') + '" to "' + isnull(@NEW_INSTALL_PLAN,'') + '".;'                              
      if (@NEW_DOWN_PAY_MODE !=@DOWN_PAY_MODE)                              
        set  @TRAN_DESC = isnull(@TRAN_DESC,'') + 'Down Payment Mode Changed from "' + isnull(@DOWN_PAY_MODE_DESC,'Not Applicable') + '" to "' + isnull(@NEW_DOWN_PAY_MODE_DESC,'') + '".;'                               
                      
   END                        
                       
--end here                      
--Modified by praveen K INSURANCE SCORE REASONS                    
                    
                    
--ADDED BY PRAVESH FORM INSURANCE SCORE                                
                              
 SELECT                    
 @APPLY_INSURANCE_SCORE = CUSTOMER_INSURANCE_SCORE,                    
 @CUSTOMER_REASON_CODE = ISNULL(CUSTOMER_REASON_CODE,''),                    
 @CUSTOMER_REASON_CODE2 = ISNULL(CUSTOMER_REASON_CODE2,''),                    
 @CUSTOMER_REASON_CODE3 = ISNULL(CUSTOMER_REASON_CODE3,''),                    
 @CUSTOMER_REASON_CODE4 = ISNULL(CUSTOMER_REASON_CODE4,'')                     
 FROM                     
CLT_CUSTOMER_LIST with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID                                             
 --END HERE                                
 ---Added By Ravindra 12-18-2006                                 
 -- In case of renewal term will be increased By 1 but in case of Endorsement it will be same                                 
 -- as Endorsment means the change in Policy but the term is same                                
 -- Need to be handeled For Re-Instatement                                
 SET @CURRENT_TERM = @CURRENT_TERM + 1                                
                
                              
 UPDATE #POL_CUSTOMER_POLICY_LIST                             
 Set  APP_INCEPTION_DATE = @APP_INCEPTION_DATE,                                      
 APP_EFFECTIVE_DATE = @APP_EXPIRATION_DATE, --DateAdd(day,1,@APP_EXPIRATION_DATE) ,                                      
 APP_EXPIRATION_DATE =@NEW_APP_EXPIRATION_DATE, --DateAdd(day,datediff(day,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE)+1,@APP_EXPIRATION_DATE),                                
 POL_VER_EFFECTIVE_DATE=@APP_EXPIRATION_DATE, --DateAdd(day,1,@APP_EXPIRATION_DATE) ,                                
 POL_VER_EXPIRATION_DATE=@NEW_APP_EXPIRATION_DATE, --DateAdd(day,datediff(day,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE)+1,@APP_EXPIRATION_DATE),                                
 POLICY_EFFECTIVE_DATE =  @APP_EXPIRATION_DATE, --Added By Lalit March 08,2011.if renewal then policy effective date should be changed          
 POLICY_EXPIRATION_DATE = @NEW_APP_EXPIRATION_DATE, --Added By Lalit March 08,2011.if renewal then policy expiry date should be changed          
 BILL_TYPE= @BILL_TYPE,                                 
 BILL_TYPE_ID =@NEW_BILL_TYPE_ID,                                
 --BY PRAVESH                                 
 INSTALL_PLAN_ID=@NEW_INSTALL_PLAN_ID,                         
 DOWN_PAY_MODE=@NEW_DOWN_PAY_MODE,                                 
 APPLY_INSURANCE_SCORE=@APPLY_INSURANCE_SCORE,                                
 --                       
 --Added by Praveen K                    
 CUSTOMER_REASON_CODE = @CUSTOMER_REASON_CODE,                    
 CUSTOMER_REASON_CODE2 = @CUSTOMER_REASON_CODE2,                    
 CUSTOMER_REASON_CODE3 = @CUSTOMER_REASON_CODE3,                    
 CUSTOMER_REASON_CODE4 = @CUSTOMER_REASON_CODE4,                    
 --          
 CURRENT_TERM = @CURRENT_TERM ,                          
 APP_TERMS=@NEW_POLICY_TERMS ,                      
 --Complete AppBonus                       
 COMPLETE_APP = NULL --In Case of renewal :Added by Praveen (02/13/2008)                              
 --Added By Ravindra(05-22-2006)                     
                 
                
                                    
 SELECT @NEW_APP_EFFECTIVE_DATE=@APP_EXPIRATION_DATE --DateAdd(day,1,@APP_EXPIRATION_DATE)       Commented by Pravesh on 14 Sep 09 as new effective date is same as expiration date of old term                  
--set @NO_OF_YEAR_WITH_WOL=datediff(year,@APP_EFFECTIVE_DATE,@NEW_APP_EFFECTIVE_DATE)                     
---CODE ADDED BY pRAvesh on 30 dec 2008 to handle continiously insured with wolverine field at renewal itrack 5089/5090                  
          
--Added by Lalit for itrack # 1493.          
--control product LOB data on Effective and Expire dates          
--as per itrack # 1178 lob 22 and 21 susep code consider same.When renew policy then get new SusepCode           
IF EXISTS(SELECT * FROM MNT_LOB_SUSEPCODE_MASTER WHERE LOB_ID = CASE WHEN @RISKID=22 THEN 21 ELSE @RISKID END  AND EFFECTIVE_FROM <= @NEW_APP_EFFECTIVE_DATE AND  EFFECTIVE_TO >= @NEW_APP_EFFECTIVE_DATE )          
 SELECT  @PRODUCT_SUSEP_NUMBER = SUSEP_LOB_CODE  FROM MNT_LOB_SUSEPCODE_MASTER WHERE LOB_ID = CASE WHEN @RISKID=22 THEN 21 ELSE @RISKID END AND EFFECTIVE_FROM <= @NEW_APP_EFFECTIVE_DATE AND  EFFECTIVE_TO >= @NEW_APP_EFFECTIVE_DATE           
ELSE          
 SELECT  @PRODUCT_SUSEP_NUMBER = SUSEP_LOB_CODE  FROM MNT_LOB_MASTER WHERE LOB_ID = CASE WHEN @RISKID=22 THEN 21 ELSE @RISKID END          
          
UPDATE           
#POL_CUSTOMER_POLICY_LIST           
SET SUSEP_LOB_CODE =  @PRODUCT_SUSEP_NUMBER          
          
          
DECLARE @LAST_YEAR_WITH_WOL_CHANGE_DATE DATETIME                  
SELECT TOP 1 @LAST_YEAR_WITH_WOL_CHANGE_DATE=APP_EFFECTIVE_DATE FROM POL_CUSTOMER_POLICY_LIST  PCPL WITH(NOLOCK)                  
 INNER JOIN POL_POLICY_PROCESS PPP WITH(NOLOCK) ON PPP.CUSTOMER_ID=PCPL.CUSTOMER_ID                   
 AND PPP.POLICY_ID=PCPL.POLICY_ID AND PPP.NEW_POLICY_VERSION_ID=PCPL.POLICY_VERSION_ID                  
 AND PPP.PROCESS_STATUS='COMPLETE' AND ISNULL(REVERT_BACK,'')<>'Y'                  
 AND PROCESS_ID=18                  
 WHERE PCPL.CUSTOMER_ID=@CUSTOMER_ID                   
  AND PCPL.POLICY_ID=@POLICY_ID                   
  AND ISNULL(IS_YEAR_WITH_WOL_UPDATED,'')='Y'                  
 ORDER BY PCPL.POLICY_VERSION_ID DESC                   
if(datediff(dd,isnull(@LAST_YEAR_WITH_WOL_CHANGE_DATE,@APP_INCEPTION_DATE),@NEW_APP_EFFECTIVE_DATE) >=365)                   
 begin                  
  set @NO_OF_YEAR_WITH_WOL=1                  
  UPDATE #POL_CUSTOMER_POLICY_LIST set IS_YEAR_WITH_WOL_UPDATED='Y'                  
 end                  
--end here itrack 5089/5090                   
                  
 INSERT INTO #IN_EFFECTIVE_COVERAGES(COV_ID )           
 SELECT COV_ID FROM MNT_COVERAGE WITH(NOLOCK) WHERE                                 
 @NEW_APP_EFFECTIVE_DATE > ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630')                                  
                      
                                 
 INSERT INTO #IN_EFFECTIVE_COVERAGE_RANGES(LIMIT_DEDUC_ID )                                      
 SELECT LIMIT_DEDUC_ID FROM MNT_COVERAGE_RANGES RANGES                       
 WHERE @NEW_APP_EFFECTIVE_DATE > ISNULL(RANGES.DISABLED_DATE,'3000-03-16 16:59:06.630')                                        
         -- Added By Ravindra(05-25-2006)                                      
 INSERT INTO #IN_EFFECTIVE_ENDORSMENT (ENDORSMENT_ID)                                         
 SELECT ENDORSMENT_ID FROM MNT_ENDORSMENT_DETAILS WITH(NOLOCK) WHERE                                 
 @NEW_APP_EFFECTIVE_DATE > ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630')                          
 -- Added By Ravindra End Here                                      
 -- Added By PRAVESH(7-FEB-2007)                                          
 INSERT INTO #IN_EFFECTIVE_ENDORSMENT_ATTACHMENT (ENDORSEMENT_ATTACH_ID)                                         
 SELECT ENDORSEMENT_ATTACH_ID FROM MNT_ENDORSEMENT_ATTACHMENT WITH(NOLOCK) WHERE                                 
 @NEW_APP_EFFECTIVE_DATE > ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630')                                
 -- Added By PRAVESH End Here                        
                      
                                  
                                      
End                                        
                                      
-- End of Addition by RPSINGH - 8 May 2006                      
                                                    
 --Added by praveen : During Endorsement Process                     
--Get old Inurance score for Policy process :                     
 DECLARE @OLD_APPLY_INSURANCE_SCORE numeric                    
 SELECT                    
  @OLD_APPLY_INSURANCE_SCORE = APPLY_INSURANCE_SCORE                    
  FROM                    
  POL_CUSTOMER_POLICY_LIST with(nolock) WHERE                    
  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                    
                    
  SELECT                    
  @APPLY_INSURANCE_SCORE=CUSTOMER_INSURANCE_SCORE,                    
  @CUSTOMER_REASON_CODE = ISNULL(CUSTOMER_REASON_CODE,''),                    
  @CUSTOMER_REASON_CODE2 = ISNULL(CUSTOMER_REASON_CODE2,''),                    
  @CUSTOMER_REASON_CODE3 = ISNULL(CUSTOMER_REASON_CODE3,''),                    
  @CUSTOMER_REASON_CODE4 = ISNULL(CUSTOMER_REASON_CODE4,'')                     
  FROM                     
  CLT_CUSTOMER_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID                      
                    
  --dont update insurance score for cancellation/rescind/reinstatement. in case of reinstate.score will be updated on commit Itrack 4575                  
 IF(@OLD_APPLY_INSURANCE_SCORE <> @APPLY_INSURANCE_SCORE and @RENEWAL NOT IN(12,29,16) )                    
 BEGIN                    
                    
  UPDATE #POL_CUSTOMER_POLICY_LIST SET                     
   APPLY_INSURANCE_SCORE = @APPLY_INSURANCE_SCORE,                    
   CUSTOMER_REASON_CODE  = @CUSTOMER_REASON_CODE,         
   CUSTOMER_REASON_CODE2 = @CUSTOMER_REASON_CODE2,                    
   CUSTOMER_REASON_CODE3 = @CUSTOMER_REASON_CODE3,                    
   CUSTOMER_REASON_CODE4 = @CUSTOMER_REASON_CODE4                     
  WHERE                     
   CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_NEW_VERSION                    
                             
  set  @TRAN_DESC = isnull(@TRAN_DESC,'') + 'Insurance Score Changed from '+ case when convert(varchar,@OLD_APPLY_INSURANCE_SCORE) ='-2' then 'No Hit/No Score' else convert(varchar,@OLD_APPLY_INSURANCE_SCORE) end                   
  set  @TRAN_DESC = isnull(@TRAN_DESC,'') + ' to '+ case when convert(varchar,@APPLY_INSURANCE_SCORE)='-2' then 'No Hit/No Score' else convert(varchar,@APPLY_INSURANCE_SCORE) end + '.;'                    
                    
 END                    
                    
            
 --End                                                                
  -- Update for POLICY_SUBLOB trailblazer on renewal                  
--IF(@RENEWAL=1 or @RENEWAL=3)                  
-- BEGIN                  
--  IF(@APP_EXPIRATION_DATE>=@TRAILBALZER_EXPIRY_DATE)                  
--   BEGIN               
--    UPDATE #POL_CUSTOMER_POLICY_LIST SET  POLICY_SUBLOB='0'                  
--   END                  
-- END                  
--SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
--IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                 
                                
--SELECT * FROM #POL_CUSTOMER_POLICY_LIST                   
                      
--Added By Lalit Dec 17,2010                
--change to genrate policy display version , base on current term                
--like of policy current term is 1 then policy display version will in sequence 1.1,1.2,1.2 for effective period endorsement only        
--if endorsemnt is out of sequence then display version according to base version                
--SELECT @CURRENT_DISP  = CURRENT_TERM FROM  #POL_CUSTOMER_POLICY_LIST                 
                   
DECLARE @VER NVARCHAR(15),@DISP_PART_2 INT,@CURRENT_DISP NVARCHAR(50),@DISP_PART_1 INT,@DISP_CURRENT_TERM  INT                
,@MAX_VERSION_ID INT          
SELECT @VER   = CAST(POLICY_DISP_VERSION AS NVARCHAR(50)),                
@DISP_CURRENT_TERM =  CAST(CURRENT_TERM AS NVARCHAR(10))  FROM  #POL_CUSTOMER_POLICY_LIST                   
             
       /*          
       Added By Lalit for in case of previos version re-issue endorsement then           
       process launch on last max verion which is not re-issue .but in this case policy display version           
       keep increasing with last max version          
       */          
                  
  SELECT @VER =  CAST(POLICY_DISP_VERSION AS NVARCHAR(50)) FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  AND POLICY_VERSION_ID           
  in(SELECT MAX(POLICY_VERSION_ID) from POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)          
          
       -- SELECT @VER           
                  
SELECT @DISP_PART_1 = CAST(dbo.Piece(@VER,'.',1) AS INT)                 
IF(@DISP_CURRENT_TERM <> @DISP_PART_1)                
 BEGIN                
  SELECT @DISP_PART_1 = @DISP_CURRENT_TERM                
  SELECT @DISP_PART_2 = 0                
 END                
ELSE                
 BEGIN                
  SELECT @DISP_PART_2 = CAST(dbo.Piece(@VER,'.',2) AS INT)                  
  SELECT @DISP_PART_2 = @DISP_PART_2 + 1                  
 END                
                
SET @CURRENT_DISP = CAST(@DISP_PART_1 AS NVARCHAR(10)) +'.'+ CAST(@DISP_PART_2 AS NVARCHAR(50))                  
                
UPDATE #POL_CUSTOMER_POLICY_LIST SET POLICY_DISP_VERSION = @CURRENT_DISP                 
SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                   
                  
                                
INSERT INTO POL_CUSTOMER_POLICY_LIST                     
SELECT * FROM #POL_CUSTOMER_POLICY_LIST                           
SELECT @TEMP_ERROR_CODE = @@ERROR             
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                     
                                
DROP TABLE #POL_CUSTOMER_POLICY_LIST                                                                
SELECT @TEMP_ERROR_CODE = @@ERROR                                          
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                              
--2. Co Applicant Data                   
SELECT * INTO #POL_APPLICANT_LIST FROM POL_APPLICANT_LIST  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
SELECT @TEMP_ERROR_CODE = @@ERROR                               
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                                
UPDATE #POL_APPLICANT_LIST SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                              
SELECT @TEMP_ERROR_CODE = @@ERROR                                                                      
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                                
INSERT INTO POL_APPLICANT_LIST                                       
SELECT * FROM #POL_APPLICANT_LIST                                              
SELECT @TEMP_ERROR_CODE = @@ERROR                                
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                   
                        
DROP TABLE #POL_APPLICANT_LIST                                              
SELECT @TEMP_ERROR_CODE = @@ERROR                   
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                            
                              
 --3. Coping Billing Info                              
SELECT * INTO #ACT_POL_EFT_CUST_INFO FROM ACT_POL_EFT_CUST_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
SELECT @TEMP_ERROR_CODE = @@ERROR                               
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                          
UPDATE #ACT_POL_EFT_CUST_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                       
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                              
INSERT INTO ACT_POL_EFT_CUST_INFO                                               
SELECT * FROM #ACT_POL_EFT_CUST_INFO                                              
SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                      
DROP TABLE #ACT_POL_EFT_CUST_INFO           
SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                        
--4. Coping Credit Card Billing Info                              
CREATE TABLE #ACT_POL_CREDIT_CARD_DETAILS                         
(                        
IDEN_ROW_ID   int IDENTITY NOT NULL ,                        
POLICY_ID   int,                        
POLICY_VERSION_ID smallint,                        
CUSTOMER_ID  int,                     
PAY_PAL_REF_ID varchar(200)                  
                    
                      
)                        
                        
INSERT INTO #ACT_POL_CREDIT_CARD_DETAILS (                        
POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID,PAY_PAL_REF_ID                  
)                        
                        
SELECT POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID,PAY_PAL_REF_ID                  
FROM ACT_POL_CREDIT_CARD_DETAILS                          
WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
                        
SELECT @TEMP_ERROR_CODE = @@ERROR                               
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                                
UPDATE #ACT_POL_CREDIT_CARD_DETAILS SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                                                
SELECT @TEMP_ERROR_CODE = @@ERROR              
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                                
                        
INSERT INTO ACT_POL_CREDIT_CARD_DETAILS (                        
POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID,PAY_PAL_REF_ID)                       
                     
SELECT POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID,PAY_PAL_REF_ID                  
                      
FROM #ACT_POL_CREDIT_CARD_DETAILS                                  
                        
SELECT @TEMP_ERROR_CODE = @@ERROR                                         
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                     
                                
DROP TABLE #ACT_POL_CREDIT_CARD_DETAILS                                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                                                
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
---------Copy Policy Locations Data                  
--SELECT * INTO #POL_LOCATIONS_POL FROM POL_LOCATIONS  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
--SELECT @TEMP_ERROR_CODE = @@ERROR                               
--IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                          
--UPDATE #POL_LOCATIONS_POL SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
--SELECT @TEMP_ERROR_CODE = @@ERROR                                       
--IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                               
--INSERT INTO POL_LOCATIONS                                               
--SELECT * FROM #POL_LOCATIONS_POL                                              
--SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
--IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                      
--DROP TABLE #POL_LOCATIONS_POL                            
--SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
--IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
-----Copy Co-Insurance Table                  
SELECT * INTO #POL_CO_INSURANCE FROM POL_CO_INSURANCE  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
SELECT @TEMP_ERROR_CODE = @@ERROR                               
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                          
UPDATE #POL_CO_INSURANCE SET POLICY_VERSION_ID = @POLICY_NEW_VERSION--,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                       
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                              
INSERT INTO POL_CO_INSURANCE                                               
SELECT * FROM #POL_CO_INSURANCE                                              
SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                      
DROP TABLE #POL_CO_INSURANCE                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                  
-----Copy Remuneration Table                  
SELECT * INTO #POL_REMUNERATION FROM POL_REMUNERATION  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
SELECT @TEMP_ERROR_CODE = @@ERROR                               
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
                          
UPDATE #POL_REMUNERATION SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                       
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                              
INSERT INTO POL_REMUNERATION                                               
SELECT * FROM #POL_REMUNERATION                                              
SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                      
DROP TABLE #POL_REMUNERATION                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
-----Copy Policy Clauses Table                  
SELECT @MAXCLAUSE_ID= ISNULL(MAX(POL_CLAUSE_ID),0) FROM POL_CLAUSES WITH(NOLOCK)  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                
                  
SELECT POL_CLAUSE_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAUSE_ID,CLAUSE_TITLE,CLAUSE_DESCRIPTION,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,SUSEP_LOB_ID,CLAUSE_TYPE, ATTACH_FILE_NAME,CLAUSE_CODE ,PREVIOUS_VERSION_ID    
  
    
      
        
            
 INTO #POL_CLAUSES FROM POL_CLAUSES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID               
 AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND ISNULL(CLAUSE_CODE,'') NOT IN ('7008','7001','7002')          
 -- Modified by praveer 7 july 2011 for itrack no 1355          
 --Modified by Lalit march 03,2011---'9999_0_7008_1_05082010' ---Added By Lalit Jan 37,2011              
SELECT @TEMP_ERROR_CODE = @@ERROR                               
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                          
UPDATE #POL_CLAUSES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                       
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                  
INSERT INTO POL_CLAUSES(POL_CLAUSE_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAUSE_ID,CLAUSE_TITLE,CLAUSE_DESCRIPTION,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,SUSEP_LOB_ID,CLAUSE_TYPE, ATTACH_FILE_NAME,CLAUSE_CODE,PREVIOUS_VERSION_ID)                 
            
                                        
SELECT                   
@MAXCLAUSE_ID + ROW_NUMBER() over(order by CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID),                  
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAUSE_ID,CLAUSE_TITLE,CLAUSE_DESCRIPTION,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,NULL,NULL,SUSEP_LOB_ID ,CLAUSE_TYPE, ATTACH_FILE_NAME,CLAUSE_CODE ,PREVIOUS_VERSION_ID                   
 FROM #POL_CLAUSES                                              
SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                      
DROP TABLE #POL_CLAUSES                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
-----Copy Policy Reinsurance Table                  
SELECT * INTO #POL_REINSURANCE_INFO FROM POL_REINSURANCE_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
SELECT @TEMP_ERROR_CODE = @@ERROR                               
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                          
UPDATE #POL_REINSURANCE_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                       
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                              
INSERT INTO POL_REINSURANCE_INFO                                               
SELECT * FROM #POL_REINSURANCE_INFO                                              
SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                  
                      
DROP TABLE #POL_REINSURANCE_INFO                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                  
-----Copy Policy Discount/Surcharge Table                         
SELECT * INTO #POL_DISCOUNT_SURCHARGE FROM POL_DISCOUNT_SURCHARGE WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                   
SELECT @TEMP_ERROR_CODE = @@ERROR                               
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                          
UPDATE #POL_DISCOUNT_SURCHARGE SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                       
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                     
INSERT INTO POL_DISCOUNT_SURCHARGE                                               
SELECT * FROM #POL_DISCOUNT_SURCHARGE                                              
SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                      
DROP TABLE #POL_DISCOUNT_SURCHARGE                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
---copying risk  level Data                  
-- All Risk and Named Perils  ,26 Engineering Risk               
-- i-track #-600 ,do not copy deactivated risk in to next version            
IF (@RISKID in(9,26))                    
BEGIN                     
 SELECT * INTO #POL_PERILS FROM POL_PERILS WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
 --AND ISNULL(UPPER(IS_ACTIVE),'Y')='Y'            
 SELECT @TEMP_ERROR_CODE = @@ERROR                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                           
 UPDATE #POL_PERILS SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
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
-- MeriTIme                  
IF (@RISKID = 13)                    
BEGIN                     
 SELECT * INTO #POL_MARITIME FROM POL_MARITIME WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
  --AND ISNULL(UPPER(IS_ACTIVE),'Y')='Y'             SELECT @TEMP_ERROR_CODE = @@ERROR                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                           
 UPDATE #POL_MARITIME SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
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
-- 15 Individual Personal Accident,21 Group Passenger Personal Accident   ,Dpvat,Mortgage,Group Life                
IF (@RISKID in (15,21,33,34))                    
BEGIN                    
 SELECT * INTO #POL_PERSONAL_ACCIDENT_INFO FROM POL_PERSONAL_ACCIDENT_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                     
  --AND ISNULL(UPPER(IS_ACTIVE),'Y')='Y'            
 SELECT @TEMP_ERROR_CODE = @@ERROR                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
     
 UPDATE #POL_PERSONAL_ACCIDENT_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
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
                  
--20/23 National/International Cargo Transport                  
IF (@RISKID in(20,23))                    
BEGIN                     
 SELECT * INTO #POL_COMMODITY_INFO FROM POL_COMMODITY_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
  --AND ISNULL(UPPER(IS_ACTIVE),'Y')='Y'            
 SELECT @TEMP_ERROR_CODE = @@ERROR                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                           
 UPDATE #POL_COMMODITY_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
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
--17 FACLIAB Facultative Liability                  
--18 CVLIABTR Civil Liability Transportation , Aeronautic,Motor,cargo transportation civil Liability                
--30 Dpvat(Cat. 3 e 4)              
--36 DPVAT(Cat.1,2,9 e 10)              
IF (@RISKID in (17,18,28,29,31,30,36))                    
BEGIN                     
 SELECT * INTO #POL_CIVIL_TRANSPORT_VEHICLES FROM POL_CIVIL_TRANSPORT_VEHICLES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
  --AND ISNULL(UPPER(IS_ACTIVE),'Y')='Y'            
 SELECT @TEMP_ERROR_CODE = @@ERROR                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                           
 UPDATE #POL_CIVIL_TRANSPORT_VEHICLES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
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
--10 Comprehensive Condominium,11 -Comprehensive Company,14--Diversified Risks,16- Robbery,19-Dwelling,12 --General Civil Liability                  
--25 Traditional fire,Global of bank,Judicial Guarantee                
IF (@RISKID in (10,11,14,16,19,12,25,27,32))                    
BEGIN                     
 SELECT * INTO #POL_PRODUCT_LOCATION_INFO FROM POL_PRODUCT_LOCATION_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
 -- AND ISNULL(UPPER(IS_ACTIVE),'Y')='Y'            
 SELECT @TEMP_ERROR_CODE = @@ERROR                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                           
 UPDATE #POL_PRODUCT_LOCATION_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
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
--22 Passenger Personal Accident                   
IF (@RISKID = 22)                    
BEGIN                   
 SELECT * INTO #POL_PASSENGERS_PERSONAL_ACCIDENT_INFO FROM POL_PASSENGERS_PERSONAL_ACCIDENT_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                            
  
    
  --AND ISNULL(UPPER(IS_ACTIVE),'Y')='Y'            
 SELECT @TEMP_ERROR_CODE = @@ERROR                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                     
                           
 UPDATE #POL_PASSENGERS_PERSONAL_ACCIDENT_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
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
              
IF (@RISKID in (35,37))   --Rural Lien,rental surety               
BEGIN                  
 SELECT * INTO #POL_PENHOR_RURAL_INFO              
 FROM POL_PENHOR_RURAL_INFO              
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                              
  --AND ISNULL(UPPER(IS_ACTIVE),'Y')='Y'            
 SELECT @TEMP_ERROR_CODE = @@ERROR                             
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
                         
 UPDATE #POL_PENHOR_RURAL_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                          
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
              
              
-----copying Coverages and protective devices                  
IF (@RISKID > 8)                    
begin                  
 SELECT * INTO #POL_PROTECTIVE_DEVICES FROM POL_PROTECTIVE_DEVICES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                           
 UPDATE #POL_PROTECTIVE_DEVICES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                       
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                               
 INSERT INTO POL_PROTECTIVE_DEVICES                                               
 SELECT * FROM #POL_PROTECTIVE_DEVICES                                              
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
               
 DROP TABLE #POL_PROTECTIVE_DEVICES                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
     
                   
                   
 IF (@RENEWAL=1)                  
 begin                    SELECT * INTO #POL_PRODUCT_COVERAGES  FROM POL_PRODUCT_COVERAGES                             
  WHERE   CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID            
   AND ISNULL(UPPER(IS_ACTIVE),'Y')='Y'                                                              
  AND COVERAGE_CODE_ID NOT IN (SELECT COV_ID FROM #IN_EFFECTIVE_COVERAGES)                         
                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                               
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                                
  UPDATE #POL_PRODUCT_COVERAGES SET LIMIT_ID=NULL,                                       
  LIMIT_1=NULL,                                      
  LIMIT_2=NULL,                         
  LIMIT1_AMOUNT_TEXT=NULL,                                      
  LIMIT2_AMOUNT_TEXT=NULL                                      
  WHERE                                       
  LIMIT_ID IN (SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                       
  SELECT @TEMP_ERROR_CODE = @@ERROR                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                
  UPDATE #POL_PRODUCT_COVERAGES SET DEDUC_ID=NULL,                                       
  DEDUCTIBLE_1=NULL,                                      
  DEDUCTIBLE_2=NULL,                                      
  DEDUCTIBLE1_AMOUNT_TEXT=NULL,                                      
  DEDUCTIBLE2_AMOUNT_TEXT=NULL                         WHERE  DEDUC_ID IN(SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
                  
  --UPDATE #POL_PRODUCT_COVERAGES SET ADDDEDUCTIBLE_ID=NULL,                            
  --DEDUCTIBLE=NULL,                                      
  --DEDUCTIBLE_TEXT=NULL                                     
  --WHERE  ADDDEDUCTIBLE_ID IN(SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                      
  --SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  --IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                   
                                                         
  /*if version is created from base version in case of undo endorsement then premium should not be blank in new version          
  i-track #1126          
  */          
  --IF(ISNULL(UPPER(RTRIM(LTRIM(@CALLED_FROM))),'')<>'UNDO')           
 --as discussed with mr. pravesh (May 18 ,2011) on endorsemnt cancellation process(undo endorsemnt)          
 --written premium should copied from base version.          
  SELECT * FROM #POL_PRODUCT_COVERAGES          
  UPDATE #POL_PRODUCT_COVERAGES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                         
  ,WRITTEN_PREMIUM = CASE WHEN ISNULL(UPPER(RTRIM(LTRIM(@CALLED_FROM))),'') = 'UNDO' OR UPPER(@PLAN_TYPE) = 'MAUTO'   THEN WRITTEN_PREMIUM --FULL_TERM_PREMIUM =0,          
     ELSE 0 END,   --if called from undo then version created from base version and premium also same as base version ,           
   CHANGE_INFORCE_PREM = CASE WHEN ISNULL(UPPER(RTRIM(LTRIM(@CALLED_FROM))),'')= 'UNDO' OR UPPER(@PLAN_TYPE) = 'MAUTO'   THEN CHANGE_INFORCE_PREM ---Modified by Lalit April 26,2011 ,i-track # 1126/1216 (if billing plan is monthly auto the premium should not be zero at new version )          
      ELSE 0 END                
  --user enter transaction premium in wriiten premium box every time,We calculate full term premium and change inforce premium on save.          
            
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM         
                   
  INSERT INTO POL_PRODUCT_COVERAGES SELECT * FROM #POL_PRODUCT_COVERAGES                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                               
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                
  DROP TABLE #POL_PRODUCT_COVERAGES                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
 end                  
 else                  
 begin                  
  SELECT * INTO #POL_PRODUCT_COVERAGES1  FROM POL_PRODUCT_COVERAGES                             
  WHERE   CUSTOMER_ID=@CUSTOMER_ID                                       
  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                 AND ISNULL(UPPER(IS_ACTIVE),'Y')='Y'             
                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                               
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                                
  UPDATE #POL_PRODUCT_COVERAGES1 SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                         
  ,WRITTEN_PREMIUM = CASE WHEN ISNULL(UPPER(RTRIM(LTRIM(@CALLED_FROM))),'') = 'UNDO' OR UPPER(@PLAN_TYPE) = 'MAUTO'   THEN WRITTEN_PREMIUM --FULL_TERM_PREMIUM =0,          
     ELSE 0 END,   --if called from undo then version created from base version and premium also same as base version ,           
   CHANGE_INFORCE_PREM = CASE WHEN ISNULL(UPPER(RTRIM(LTRIM(@CALLED_FROM))),'')= 'UNDO' OR UPPER(@PLAN_TYPE) = 'MAUTO'   THEN CHANGE_INFORCE_PREM ---Modified by Lalit April 26,2011 ,i-track # 1126/1216 (if billing plan is monthly auto the premium should not be zero at new version )          
      ELSE 0 END            
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM     
                             
  INSERT INTO POL_PRODUCT_COVERAGES SELECT * FROM #POL_PRODUCT_COVERAGES1                                   
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                               
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                
  DROP TABLE #POL_PRODUCT_COVERAGES1                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
 end                  
end                  
                  
-- Aviation                  
IF (@RISKID = 8)                    
BEGIN                                                    
 --1.                                                                
 SELECT * INTO #POL_AVIATION_VEHICLES FROM POL_AVIATION_VEHICLES  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
 -- Is update column is set to 10964 for new policy version itrack(6369)                                
 UPDATE #POL_AVIATION_VEHICLES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                 
                                 
 INSERT INTO POL_AVIATION_VEHICLES SELECT * FROM #POL_AVIATION_VEHICLES                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
                                 
 DROP TABLE #POL_AVIATION_VEHICLES                                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                    
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                     
--2                  
  SELECT * INTO #POL_AVIATION_VEHICLE_COVERAGES FROM POL_AVIATION_VEHICLE_COVERAGES  WHERE  CUSTOMER_ID=@CUSTOMER_ID                                     
  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                      
                                  
  UPDATE #POL_AVIATION_VEHICLE_COVERAGES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                       
  SELECT @TEMP_ERROR_CODE = @@ERROR                              
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  INSERT INTO POL_AVIATION_VEHICLE_COVERAGES SELECT * FROM #POL_AVIATION_VEHICLE_COVERAGES                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  DROP TABLE #POL_AVIATION_VEHICLE_COVERAGES                           
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                       
                   
END                  
--added by praveen kasana                  
-- Private Passenger                          
                    
IF (@RISKID = 2)                                                                  
BEGIN                    
                  
                  
---- UNDERWRITING TIER                              
SELECT * INTO #POL_UNDERWRITING_TIER FROM POL_UNDERWRITING_TIER  WHERE                   
 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                  
                                                            
SELECT @TEMP_ERROR_CODE = @@ERROR                               
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                          
UPDATE #POL_UNDERWRITING_TIER SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                       
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                              
INSERT INTO POL_UNDERWRITING_TIER                                               
SELECT * FROM #POL_UNDERWRITING_TIER                                              
SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                      
DROP TABLE #POL_UNDERWRITING_TIER                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                                
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                  
END                     
--end here                                                                                                      
                     
/* COPY THE LOB SPECIFIC DATA                    
RISKIDs                   
1. Homeowners                                                                          
2.  Private Passenger                                       
3.  Motorcycle                                                     
4.  Watercraft                          
5.  Umbrella                       
6.  Rental Dwelling                                                                          
7.  General Liability                                                                
*/                                                      
                                                                 
-- Private Passenger or Motorcycle                                       
IF (@RISKID = 2 OR @RISKID = 3 OR @RISKID = 38)                                                                 
BEGIN                                                    
 --1.                               
 SELECT * INTO #POL_VEHICLES FROM POL_VEHICLES  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
 -- Is update column is set to 10964 for new policy version itrack(6369)                                
 UPDATE #POL_VEHICLES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE,IS_UPDATED=10964                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                 
                                 
 INSERT INTO POL_VEHICLES SELECT * FROM #POL_VEHICLES                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                              
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
                                 
 DROP TABLE #POL_VEHICLES                                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                    
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
 --2.                                           
 -- Added By Ravindra(05-22-2006)                                                       
 IF @RENEWAL = 1  --In Case of renewal ineligible coverages not to be selected                                      
 BEGIN                                
                    
  --UPDATING TERRITORY AT RENEWAL BY PRAVESH ON 14 MARCH                    
 CREATE TABLE #POL_VEHICLES_TERRITORY                        
  (                         
   TERRITORY VARCHAR(4)                        
  )                        
 CREATE TABLE #POL_VEHICLES_TEMP                        
  (                         
   [IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,                        
   [VEHICLE_ID] INT,                        
   )                        
  INSERT INTO #POL_VEHICLES_TEMP                        
  ([VEHICLE_ID])                        
  SELECT VEHICLE_ID FROM POL_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_NEW_VERSION                        
                    
 DECLARE @VEHICLE_USE SMALLINT,@ZIP_ID VARCHAR(10),@GRG_STATE INT,@OLD_TERRITORY INT,@NEW_TERRITORY INT,@VEHICLE_ID INT                    
 DECLARE @APP_USE_VEHICLE_ID int,@APP_VEHICLE_PERTYPE_ID SMALLINT,@OLD_SYMBOL NVARCHAR(5),@NEW_SYMBOL NVARCHAR(5) ,@VEHICLE_YEAR nvarchar(4),@MAKE nvarchar(75),@MODEL nvarchar(75),@VIN nvarchar(75) ,@BODY_TYPE nvarchar(75)                  
 DECLARE @ID_COL Int                        
 SET @ID_COL = 1                        
 WHILE 1 = 1                                      
 BEGIN                               
  IF NOT EXISTS                                      
 (SELECT IDENT_COL FROM #POL_VEHICLES_TEMP  WHERE IDENT_COL = @ID_COL  )                                      
   BEGIN                                      
  BREAK                                      
   END                      
 SELECT   @VEHICLE_ID=VEHICLE_ID  FROM #POL_VEHICLES_TEMP  WHERE IDENT_COL = @ID_COL                     
 SELECT @APP_USE_VEHICLE_ID=APP_USE_VEHICLE_ID,@VEHICLE_USE=VEHICLE_USE,@ZIP_ID= GRG_ZIP,@OLD_TERRITORY=TERRITORY ,@GRG_STATE=GRG_STATE,                   
@APP_VEHICLE_PERTYPE_ID=APP_VEHICLE_PERTYPE_ID,@OLD_SYMBOL =SYMBOL,@VEHICLE_YEAR =VEHICLE_YEAR,@MAKE =MAKE,@MODEL =MODEL,@VIN =VIN ,@BODY_TYPE =BODY_TYPE                  
   FROM POL_VEHICLES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_NEW_VERSION                     
   AND VEHICLE_ID=@VEHICLE_ID                    
                       
 INSERT INTO #POL_VEHICLES_TERRITORY EXEC Proc_FetchTerritoryForZipStateLob                     
   @ZIP_ID , @RISKID, @GRG_STATE  ,  @CUSTOMER_ID, @POLICY_ID , @POLICY_NEW_VERSION,'POL',@APP_USE_VEHICLE_ID --@VEHICLE_USE                    
 SELECT top 1 @NEW_TERRITORY=TERRITORY FROM #POL_VEHICLES_TERRITORY                    
 IF (@OLD_TERRITORY<>ISNULL(@NEW_TERRITORY,'-1') and ISNULL(@NEW_TERRITORY,'-1') <> '-1')                    
  begin                    
  UPDATE POL_VEHICLES SET TERRITORY=@NEW_TERRITORY  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                     
   AND POLICY_VERSION_ID=@POLICY_NEW_VERSION  AND VEHICLE_ID=@VEHICLE_ID                    
  set  @TRAN_DESC = isnull(@TRAN_DESC,'') + 'Territory of vehicle '+ convert(varchar,@VEHICLE_ID) + ' has been modified from '+ convert(varchar,@OLD_TERRITORY)  + ' to '+ convert(varchar,@NEW_TERRITORY) + '.;'                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                     
  end                    
--added by Pravesh on 25 nov 08 Itrack 5092 Update Vehicle age on renewal @NEW_APP_EFFECTIVE_DATE                  
 declare @MODELMONTH smallint -- if Model Month is greater equal to Oct then Increase age by 1                  
 set @MODELMONTH = month(@NEW_APP_EFFECTIVE_DATE)                  
 declare @OLD_AGE int,@NEW_AGE int                  
  select @OLD_AGE= isnull(VEHICLE_AGE,0) from POL_VEHICLES                  
   WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                     
   AND POLICY_VERSION_ID=@POLICY_NEW_VERSION  AND VEHICLE_ID=@VEHICLE_ID                  
  UPDATE POL_VEHICLES SET VEHICLE_AGE= case when isnull(VEHICLE_YEAR,'')='' then VEHICLE_AGE                   
          when @MODELMONTH >=10 then ((year(@NEW_APP_EFFECTIVE_DATE) - cast(isnull(VEHICLE_YEAR,0)  as smallint)) + 1) + 1                    
          else ((year(@NEW_APP_EFFECTIVE_DATE) - cast(isnull(VEHICLE_YEAR,0)  as smallint)) + 1)                   
          end                   
   WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                     
   AND POLICY_VERSION_ID=@POLICY_NEW_VERSION  AND VEHICLE_ID=@VEHICLE_ID                  
  select @NEW_AGE = isnull(VEHICLE_AGE,0) from POL_VEHICLES                  
   WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                     
   AND POLICY_VERSION_ID=@POLICY_NEW_VERSION  AND VEHICLE_ID=@VEHICLE_ID                  
  if(isnull(@OLD_AGE,0)!=isnull(@NEW_AGE,0))                  
    set  @TRAN_DESC = isnull(@TRAN_DESC,'') + 'Age of ' + case when @RISKID = 2 then 'vehicle ' else 'cycle ' end                   
    +  convert(varchar,@VEHICLE_ID) + ' has been modified from '+ convert(varchar,isnull(@OLD_AGE,0))                   
    + ' to ' +convert(varchar,isnull                  
                  
(@NEW_AGE,0)) + '.;'                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                     
--end here                  
--added by Pravesh on 14 April 09 Itrack 5684 Update Vehicle Symbol on renewal (Note in 5680)                  
DECLARE @MAKE_CODE VARCHAR(75)                  
SELECT @MAKE_CODE =LOOKUP_VALUE_CODE FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_ID=1308 AND LOOKUP_VALUE_DESC=@MAKE                  
SELECT  @NEW_SYMBOL=min(LTRIM(RTRIM(ISNULL(SYMBOL,''))))                   
  FROM MNT_VIN_MASTER WHERE MODEL_YEAR = @VEHICLE_YEAR AND MAKE_CODE=@MAKE_CODE AND SERIES_NAME=@MODEL AND BODY_TYPE=@BODY_TYPE             
  AND LTRIM(RTRIM(VIN))=SUBSTRING(@VIN,1,10)                  
--order by  LTRIM(RTRIM(ISNULL(SYMBOL,'')))                    
--in case of Private Passenger Auto only                  
 IF (@RISKID = 2 AND @APP_USE_VEHICLE_ID='11332' AND @APP_VEHICLE_PERTYPE_ID='11334' AND ISNULL(@NEW_SYMBOL,'')!='' AND ISNULL(@NEW_SYMBOL,'')!=ISNULL(@OLD_SYMBOL,'') AND ISNULL(@NEW_SYMBOL,'0')<ISNULL(@OLD_SYMBOL,'0') )                  
  BEGIN                  
 UPDATE POL_VEHICLES SET SYMBOL=@NEW_SYMBOL                  
   WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                     
   AND POLICY_VERSION_ID=@POLICY_NEW_VERSION  AND VEHICLE_ID=@VEHICLE_ID                  
 set  @TRAN_DESC = isnull(@TRAN_DESC,'') + 'Symbol of vehicle '+ convert(varchar,@VEHICLE_ID) + ' has been modified from '+ @OLD_SYMBOL  + ' to '+ @NEW_SYMBOL + '.;'                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                     
  END                  
--end here                  
                  
 DELETE FROM #POL_VEHICLES_TERRITORY                                            
 set @ID_COL=@ID_COL+1                    
 END  -- end while                  
                  
DROP TABLE #POL_VEHICLES_TERRITORY                                               
DROP TABLE #POL_VEHICLES_TEMP                    
 --END HERE                    
SELECT * INTO #POL_VEHICLE_COVERAGES FROM POL_VEHICLE_COVERAGES  WHERE  CUSTOMER_ID=@CUSTOMER_ID                                       
  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                         
  AND COVERAGE_CODE_ID NOT IN (SELECT COV_ID FROM #IN_EFFECTIVE_COVERAGES)                       
                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                               
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                     
  SELECT @HAS_INVALID_COVERAGE=COUNT(COVERAGE_CODE_ID) FROM POL_VEHICLE_COVERAGES                                        
  WHERE  CUSTOMER_ID=@CUSTOMER_ID                                       
  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                        
  AND                                       
  COVERAGE_CODE_ID IN (SELECT COV_ID FROM #IN_EFFECTIVE_COVERAGES)                             
                                  
  SELECT @HAS_INVALID_COVERAGE_LIMIT=COUNT(COVERAGE_CODE_ID) FROM POL_VEHICLE_COVERAGES                        
  WHERE  CUSTOMER_ID=@CUSTOMER_ID                                       
  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                         
  AND                               
  (                                
  LIMIT_ID IN (SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                       
  OR                                      
  DEDUC_ID IN(SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                 
  )                     
                                  
  UPDATE #POL_VEHICLE_COVERAGES SET LIMIT_ID=NULL,                                  
  LIMIT_1=NULL,                                
  LIMIT_2=NULL,                          LIMIT1_AMOUNT_TEXT=NULL,                                      
  LIMIT2_AMOUNT_TEXT=NULL          
  WHERE                                       
  LIMIT_ID  IN (SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  UPDATE #POL_VEHICLE_COVERAGES SET DEDUC_ID=NULL,                                       
  DEDUCTIBLE_1=NULL,                                      
  DEDUCTIBLE_2=NULL,                    
  DEDUCTIBLE1_AMOUNT_TEXT=NULL,                                      
  DEDUCTIBLE2_AMOUNT_TEXT=NULL                       
  WHERE                            
  DEDUC_ID IN(SELECT LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  UPDATE #POL_VEHICLE_COVERAGES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                         
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                              
             
  INSERT INTO POL_VEHICLE_COVERAGES SELECT * FROM #POL_VEHICLE_COVERAGES                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  DROP TABLE #POL_VEHICLE_COVERAGES                                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                                     
  --3 .Copy Eligible Endorsment                                      
  SELECT * INTO #POL_VEHICLE_ENDORSEMENTS_T FROM POL_VEHICLE_ENDORSEMENTS                                 
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
  AND ENDORSEMENT_ID NOT IN (SELECT ENDORSMENT_ID FROM #IN_EFFECTIVE_ENDORSMENT)                                       
  SELECT @TEMP_ERROR_CODE = @@ERROR                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                     
                   
  UPDATE #POL_VEHICLE_ENDORSEMENTS_T SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                                            
  SELECT @TEMP_ERROR_CODE = @@ERROR                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM   --ADDED BY PRAVESH ON 7 FEB 2007 FOR EDITION DATE                                
  UPDATE #POL_VEHICLE_ENDORSEMENTS_T SET EDITION_DATE = NULL WHERE EDITION_DATE IN                                
   (SELECT ENDORSEMENT_ATTACH_ID FROM  #IN_EFFECTIVE_ENDORSMENT_ATTACHMENT)                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
  --END HERE                                
  INSERT INTO POL_VEHICLE_ENDORSEMENTS SELECT * FROM #POL_VEHICLE_ENDORSEMENTS_T                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                          
                                  
  DROP TABLE #POL_VEHICLE_ENDORSEMENTS_T                                              
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
 END                                      
 ELSE      -- In Case Of other than Renewal copy all coverages                                
BEGIN                                      
 SELECT * INTO #POL_VEHICLE_COVERAGES_T FROM POL_VEHICLE_COVERAGES  WHERE  CUSTOMER_ID=@CUSTOMER_ID                                       
  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                      
                                 
  UPDATE #POL_VEHICLE_COVERAGES_T SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                       
  SELECT @TEMP_ERROR_CODE = @@ERROR                              
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  INSERT INTO POL_VEHICLE_COVERAGES SELECT * FROM #POL_VEHICLE_COVERAGES_T                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  DROP TABLE #POL_VEHICLE_COVERAGES_T                           
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
                               
 --3.                                                  
  SELECT * INTO #POL_VEHICLE_ENDORSEMENTS FROM POL_VEHICLE_ENDORSEMENTS  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                              
  SELECT @TEMP_ERROR_CODE = @@ERROR                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                             
  UPDATE #POL_VEHICLE_ENDORSEMENTS SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  INSERT INTO POL_VEHICLE_ENDORSEMENTS SELECT * FROM #POL_VEHICLE_ENDORSEMENTS                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                               
                                  
  DROP TABLE #POL_VEHICLE_ENDORSEMENTS                                              
  SELECT @TEMP_ERROR_CODE = @@ERROR                                  
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
 END                                      
     --Added By Ravindra Ends here                                      
                        
 --4.                                                                
 SELECT * INTO #POL_ADD_OTHER_INT FROM POL_ADD_OTHER_INT  WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                                 
 UPDATE #POL_ADD_OTHER_INT SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 INSERT INTO POL_ADD_OTHER_INT SELECT * FROM #POL_ADD_OTHER_INT                                                               
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 DROP TABLE #POL_ADD_OTHER_INT                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                            
                                 
 --5.                               
 SELECT * INTO #POL_DRIVER_DETAILS FROM POL_DRIVER_DETAILS  WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                            
                                 
 UPDATE #POL_DRIVER_DETAILS SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
                  
 INSERT INTO POL_DRIVER_DETAILS SELECT * FROM #POL_DRIVER_DETAILS                                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                           
                                 
 DROP TABLE #POL_DRIVER_DETAILS                                                 
 SELECT @TEMP_ERROR_CODE = @@ERROR                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                               
                                 
 --6.                                                                
 SELECT * INTO #POL_MVR_INFORMATION FROM POL_MVR_INFORMATION  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
                          
 UPDATE #POL_MVR_INFORMATION SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                     
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                           
 INSERT INTO POL_MVR_INFORMATION SELECT * FROM #POL_MVR_INFORMATION                                                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                
                                 
 DROP TABLE #POL_MVR_INFORMATION                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                        
                                 
 --7.                                                                
 SELECT * INTO #POL_AUTO_GEN_INFO FROM POL_AUTO_GEN_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
                                                   
 UPDATE #POL_AUTO_GEN_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                                              
 IF (@RENEWAL = 1 and @NO_OF_YEAR_WITH_WOL >0 )                               
begin                  
 UPDATE #POL_AUTO_GEN_INFO SET YEARS_INSU_WOL=isnull(YEARS_INSU_WOL,0) + 1,YEARS_INSU=isnull(YEARS_INSU ,0) +1                      
  --for Premier driver and safe driver discount added on 16 March 09 by Pravesh              
    declare @YEARS_INSU_WOL int                  
 select @YEARS_INSU_WOL = YEARS_INSU_WOL  from  #POL_AUTO_GEN_INFO                   
 if (@YEARS_INSU_WOL>2)                  
  UPDATE POL_DRIVER_DETAILS set SAFE_DRIVER_RENEWAL_DISCOUNT=1 ,DRIVER_PREF_RISK=0                  
   WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_NEW_VERSION                                                                
 else                  
  UPDATE POL_DRIVER_DETAILS set DRIVER_PREF_RISK=case when DRIVER_PREF_RISK=0 then 0 else 1 end                  
  ,SAFE_DRIVER_RENEWAL_DISCOUNT=0                   
  WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_NEW_VERSION                                                              
   --end here on 16 March 09                  
end                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                              
--by pravesh on 7 march for Multipolicy discount                      
 IF (@RENEWAL = 1 )                      
  begin                       
--select MULTI_POLICY_DISC_APPLIED,MULTI_POLICY_DISC_APPLIED_PP_DESC,* from POL_AUTO_GEN_INFO                       
 SET @MULTI_POLICY_COUNT=0                      
                      
 INSERT INTO ##TMP_MULTIPOLICY EXECUTE Proc_GetEligiblePolicies   @CUSTOMER_ID ,  @AGENCY_ID , @RISKID , @POLICY_NUMBER                       
 SELECT @MULTI_POLICY_COUNT=COUNT(*)  FROM ##TMP_MULTIPOLICY                      
 SELECT TOP 1 @MULTI_POLICY_NUMBER=POLICY_NUMBER FROM ##TMP_MULTIPOLICY                       
 --DROP TABLE ##TMP_MULTIPOLICY                      
 IF (ISNULL(@MULTI_POLICY_NUMBER,'')!='N.A.' AND ISNULL(@MULTI_POLICY_NUMBER,'')!='' AND @MULTI_POLICY_COUNT>0)                      
 BEGIN                      
  UPDATE  #POL_AUTO_GEN_INFO SET MULTI_POLICY_DISC_APPLIED=1,MULTI_POLICY_DISC_APPLIED_PP_DESC=@MULTI_POLICY_NUMBER                      
  SET @TRAN_DESC=@TRAN_DESC + 'Under writing Question "Is Multipolicy discount applied?" has been set to "Yes".;'                      
 END                      
  end                                                 
                       
INSERT INTO POL_AUTO_GEN_INFO SELECT * FROM #POL_AUTO_GEN_INFO                                                       
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                              
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 DROP TABLE #POL_AUTO_GEN_INFO                                                 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
                          
                          
 --8.                                     
 SELECT * INTO #POL_DRIVER_ASSIGNED_VEHICLE FROM POL_DRIVER_ASSIGNED_VEHICLE WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                     
 UPDATE #POL_DRIVER_ASSIGNED_VEHICLE SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                           
 -------Added By Pravesh To update  Assigned Driver if driver Age Changes above 25  ,set That driver to Not Rated (5 may 09)                  
 IF (@RENEWAL = 1 and @RISKID = 2 )                           
begin                  
 CREATE TABLE #TEMP_POL_DRIVER_DETAILS                 
  (                         
   [IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,                        
   [DRIVER_ID] INT,                        
   [DRIVER_DOB] DATETIME,                  
   [DRIVER_AGE] INT  ,                  
   [DRIVER_AGE_OLD] INT                  
   )                        
  INSERT INTO #TEMP_POL_DRIVER_DETAILS([DRIVER_ID],[DRIVER_DOB],[DRIVER_AGE],[DRIVER_AGE_OLD])                   
 SELECT DRIVER_ID,DRIVER_DOB,dbo.GetAge (DRIVER_DOB,@NEW_APP_EFFECTIVE_DATE),dbo.GetAge (DRIVER_DOB,@APP_EFFECTIVE_DATE)                  
  FROM POL_DRIVER_DETAILS  WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_NEW_VERSION                  
                  
 DECLARE @IDEN_COL Int ,@DRIVER_AGE  INT,@DRIVER_AGE_OLD  INT,@DRIVER_ID INT                  
 SET @IDEN_COL = 1                        
 WHILE 1 = 1                                  
  BEGIN                               
  IF NOT EXISTS                                      
  (SELECT IDENT_COL FROM #TEMP_POL_DRIVER_DETAILS  WHERE IDENT_COL = @IDEN_COL  )                                      
 BEGIN                                      
  BREAK                                      
 END                      
   SELECT @DRIVER_ID=DRIVER_ID,@DRIVER_AGE=DRIVER_AGE,@DRIVER_AGE_OLD=DRIVER_AGE_OLD FROM #TEMP_POL_DRIVER_DETAILS WHERE IDENT_COL = @IDEN_COL                  
 IF (@DRIVER_AGE>=25 AND @DRIVER_AGE_OLD<25)                  
  BEGIN                  
  UPDATE #POL_DRIVER_ASSIGNED_VEHICLE SET APP_VEHICLE_PRIN_OCC_ID='11931' WHERE  DRIVER_ID= @DRIVER_ID                  
    SET @TRAN_DESC=@TRAN_DESC + 'Age of Driver '+convert(varchar,@DRIVER_ID) + ' changed from ' + convert(varchar,@DRIVER_AGE_OLD) + ' to ' + convert(varchar,@DRIVER_AGE) + ', therefore this driver has been set to "Not Rated";'                      
   UPDATE POL_DRIVER_DETAILS  SET DRIVER_GOOD_STUDENT = CASE WHEN ISNULL(DRIVER_GOOD_STUDENT,'0')=10963 THEN NULL ELSE DRIVER_GOOD_STUDENT END                  
   WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_NEW_VERSION AND DRIVER_ID=@DRIVER_ID                  
 --UPDATING REFER TO UNDER WRITER TO YES                   
   UPDATE POL_CUSTOMER_POLICY_LIST                       
   SET REFER_UNDERWRITER='Y',REFERAL_INSTRUCTIONS = CASE WHEN ISNULL(REFERAL_INSTRUCTIONS,'') =''                   
               THEN ISNULL(REFERAL_INSTRUCTIONS,'') + 'Age of Driver '+ convert(varchar,@DRIVER_ID) + ' changed from ' + convert(varchar,@DRIVER_AGE_OLD) + ' to ' + convert(varchar,@DRIVER_AGE) + ', therefore this driver has been set to "Not Rated"'     
  
    
      
        
          
            
             
               ELSE  ISNULL(REFERAL_INSTRUCTIONS,'') + '; Age of Driver '+ convert(varchar,@DRIVER_ID) + ' changed from ' + convert(varchar,@DRIVER_AGE_OLD) + ' to ' + convert(varchar,@DRIVER_AGE) + ', therefore this driver has been set to "Not Rated"'   
  
    
      
        
          
            
             
                
               END                      
   WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_NEW_VERSION                        
   SET @TRAN_DESC=@TRAN_DESC + '"Refer to Underwriter is Checked off and Referral Instruction has been set.";'                  
  END                  
 SET @IDEN_COL = @IDEN_COL+ 1                        
  END  -- end while                  
DROP TABLE #TEMP_POL_DRIVER_DETAILS                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                               
end -- end renewal=1                  
 --------   END HERE                      
                  
 INSERT INTO POL_DRIVER_ASSIGNED_VEHICLE SELECT * FROM #POL_DRIVER_ASSIGNED_VEHICLE                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                       
                                 
 DROP TABLE #POL_DRIVER_ASSIGNED_VEHICLE                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                       
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                           
                                 
                                   
 if(@RISKID=2) --add data at POL_MISCELLANEOUS_EQUIPMENT_VALUES for Automobile LOB                                  
 begin                                  
  SELECT * INTO #POL_MISCELLANEOUS_EQUIPMENT_VALUES FROM POL_MISCELLANEOUS_EQUIPMENT_VALUES  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                
                                  
  UPDATE #POL_MISCELLANEOUS_EQUIPMENT_VALUES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                           
  SELECT @TEMP_ERROR_CODE = @@ERROR                             
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                                  
  INSERT INTO POL_MISCELLANEOUS_EQUIPMENT_VALUES SELECT * FROM #POL_MISCELLANEOUS_EQUIPMENT_VALUES                                           
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                 
                    
  DROP TABLE #POL_MISCELLANEOUS_EQUIPMENT_VALUES                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
 end                                      
END        
    
                                     
                                
                               
   -- Homeowners or Rental Dwelling                                                       
IF  (@RISKID = 1 OR @RISKID =6)                                                               
BEGIN                                                
                                
 --1.                                                                
 SELECT * INTO #POL_LOCATIONS FROM POL_LOCATIONS  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                         
                                 
                                 
 UPDATE #POL_LOCATIONS SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                   
 SELECT @TEMP_ERROR_CODE = @@ERROR               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
                                 
 INSERT INTO POL_LOCATIONS SELECT * FROM #POL_LOCATIONS                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 DROP TABLE #POL_LOCATIONS                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
 --2.                              
 SELECT * INTO #POL_DWELLINGS_INFO FROM POL_DWELLINGS_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                             
                    
 UPDATE #POL_DWELLINGS_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                              
                                 
 INSERT INTO POL_DWELLINGS_INFO SELECT * FROM #POL_DWELLINGS_INFO                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                        
 DROP TABLE #POL_DWELLINGS_INFO                   
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                            
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                           
 --3.                                                          
SELECT * INTO #POL_HOME_RATING_INFO FROM POL_HOME_RATING_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                   
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                             
                                 
 UPDATE #POL_HOME_RATING_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                                               
 SELECT @TEMP_ERROR_CODE = @@ERROR                           
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                    
                   
 INSERT INTO POL_HOME_RATING_INFO SELECT * FROM #POL_HOME_RATING_INFO                                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                             
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 DROP TABLE #POL_HOME_RATING_INFO                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                       
                                 
 --4.                                     
 SELECT * INTO #POL_HOME_OWNER_ADD_INT FROM POL_HOME_OWNER_ADD_INT WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                       
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 UPDATE #POL_HOME_OWNER_ADD_INT SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                            
                                 
 INSERT INTO POL_HOME_OWNER_ADD_INT SELECT * FROM #POL_HOME_OWNER_ADD_INT                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                        
                                 
 DROP TABLE #POL_HOME_OWNER_ADD_INT                                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
--added by pravesh to copy Other structure details                      
                        
SELECT * INTO #POL_OTHER_STRUCTURE_DWELLING FROM POL_OTHER_STRUCTURE_DWELLING  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                       
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
                               
 UPDATE #POL_OTHER_STRUCTURE_DWELLING SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                                 
 INSERT INTO POL_OTHER_STRUCTURE_DWELLING SELECT * FROM #POL_OTHER_STRUCTURE_DWELLING                                 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                                 
--end here                                  

--added by Abhishek Goel 
--if(@RENEWAL != 1)
--begin
--	SELECT * INTO #POL_BILLING_DETAILS FROM POL_BILLING_DETAILS  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
--	 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                       
--	 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
	                               
--	 UPDATE #POL_BILLING_DETAILS SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                
--	 SELECT @TEMP_ERROR_CODE = @@ERROR                                      
--	 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
	                                 
--	 INSERT INTO POL_BILLING_DETAILS SELECT * FROM #POL_BILLING_DETAILS                                
--	 SELECT @TEMP_ERROR_CODE = @@ERROR                                 
--	 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM  
	 
--	 DROP TABLE #POL_BILLING_DETAILS                     
--	 SELECT @TEMP_ERROR_CODE = @@ERROR                                 
--	 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
-- end
--end here                     
                         
 -- Added By Ravindra(07-21-2006)                                              
 IF @RENEWAL = 1  --In Case of renewal ineligible coverages not to be selected                                      
 BEGIN    
                        
  SELECT * INTO #POL_DWELLING_SECTION_COVERAGES  FROM POL_DWELLING_SECTION_COVERAGES                                      
  WHERE   CUSTOMER_ID=@CUSTOMER_ID                                       
 AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
  AND COVERAGE_CODE_ID NOT IN (SELECT COV_ID FROM #IN_EFFECTIVE_COVERAGES)                         
                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                               
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                                  
  SELECT @HAS_INVALID_COVERAGE=COUNT(COVERAGE_CODE_ID) FROM POL_DWELLING_SECTION_COVERAGES                                        
  WHERE  CUSTOMER_ID=@CUSTOMER_ID                                       
  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                    
  AND                                 
  COVERAGE_CODE_ID IN (SELECT COV_ID FROM #IN_EFFECTIVE_COVERAGES)                               
                             
  SELECT @HAS_INVALID_COVERAGE_LIMIT=COUNT(COVERAGE_CODE_ID) FROM POL_DWELLING_SECTION_COVERAGES                                        
  WHERE  CUSTOMER_ID=@CUSTOMER_ID                             
  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                            
  AND                                       
  (                                    
  LIMIT_ID IN (SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                       
  OR                                      
  DEDUC_ID IN(SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                  
  OR                  
  ADDDEDUCTIBLE_ID IN(SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                       
  )                      
                            
  UPDATE #POL_DWELLING_SECTION_COVERAGES SET LIMIT_ID=NULL,                                       
  LIMIT_1=NULL,                                      
  LIMIT_2=NULL,                         
  LIMIT1_AMOUNT_TEXT=NULL,                             
  LIMIT2_AMOUNT_TEXT=NULL                                      
  WHERE                                       
  LIMIT_ID IN (SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                       
  SELECT @TEMP_ERROR_CODE = @@ERROR                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  UPDATE #POL_DWELLING_SECTION_COVERAGES SET DEDUC_ID=NULL,                                       
  DEDUCTIBLE_1=NULL,                                      
  DEDUCTIBLE_2=NULL,                                      
  DEDUCTIBLE1_AMOUNT_TEXT=NULL,                                      
  DEDUCTIBLE2_AMOUNT_TEXT=NULL                                       
  WHERE  DEDUC_ID IN(SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
                   
  UPDATE #POL_DWELLING_SECTION_COVERAGES SET ADDDEDUCTIBLE_ID=NULL,                                       
  DEDUCTIBLE=NULL,                                      
  DEDUCTIBLE_TEXT=NULL                                     
 WHERE  ADDDEDUCTIBLE_ID IN(SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                   
                                                                                     
  UPDATE #POL_DWELLING_SECTION_COVERAGES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                         
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  INSERT INTO POL_DWELLING_SECTION_COVERAGES SELECT * FROM #POL_DWELLING_SECTION_COVERAGES                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                               
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
  DROP TABLE #POL_DWELLING_SECTION_COVERAGES                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                     
                                  
  -- .Copy Eligible Endorsment                                      
  SELECT * INTO #POL_DWELLING_ENDORSEMENTS FROM POL_DWELLING_ENDORSEMENTS                                   
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                   
  AND ENDORSEMENT_ID NOT IN (SELECT ENDORSMENT_ID FROM #IN_EFFECTIVE_ENDORSMENT)                                       
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                       
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                                  
  UPDATE #POL_DWELLING_ENDORSEMENTS SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                                            
  SELECT @TEMP_ERROR_CODE = @@ERROR                                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                             
  --ADDED BY PRAVESH ON 7 FEB 2007 FOR EDITION DATE                                
  UPDATE #POL_DWELLING_ENDORSEMENTS SET EDITION_DATE = NULL WHERE EDITION_DATE IN                                
             (SELECT ENDORSEMENT_ATTACH_ID FROM  #IN_EFFECTIVE_ENDORSMENT_ATTACHMENT)                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
  --END HERE                            
                                
  INSERT INTO POL_DWELLING_ENDORSEMENTS SELECT * FROM #POL_DWELLING_ENDORSEMENTS                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                    
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                        
                             
  DROP TABLE #POL_DWELLING_ENDORSEMENTS                                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                           
--added by pravesh on 5 july to update  "Is the dwelling under construction?" to NO on renewal if it is yes                        
 declare  @OLD_IS_UNDER_CONSTRUCTION char(1)                       
 select  @OLD_IS_UNDER_CONSTRUCTION=isnull(IS_UNDER_CONSTRUCTION,'0')  from  POL_HOME_RATING_INFO with(nolock)                        
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_NEW_VERSION                        
 if (@OLD_IS_UNDER_CONSTRUCTION='1')                        
  begin                        
     UPDATE POL_HOME_RATING_INFO SET IS_UNDER_CONSTRUCTION = '0'                         
   WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_NEW_VERSION                        
--UPDATING REFER TO UNDER WRITER TO YES AS PER ITRACK 2110 ATTACHMENT BY RAJAN                      
   UPDATE POL_CUSTOMER_POLICY_LIST                       
 SET REFER_UNDERWRITER='Y',REFERAL_INSTRUCTIONS = CASE WHEN ISNULL(REFERAL_INSTRUCTIONS,'') ='' THEN ISNULL(REFERAL_INSTRUCTIONS,'') + 'Previously under construction'  ELSE  ISNULL(REFERAL_INSTRUCTIONS,'') + ', Previously under construction' END         
  
    
      
         
         
            
             
   WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_NEW_VERSION                        
      SET @TRAN_DESC=@TRAN_DESC + '"Is the dwelling under construction?" changed to NO and corresponding Coverages/Endorsements have been re-called.;'                        
      SET @TRAN_DESC=@TRAN_DESC + '"Refer to Underwriter is Checked off and Referral Instruction has been set.";'                        
   end                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                                  
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
-- end here                        
 END                                      
 ELSE      -- Other than Renewal Copy All Coverages                                
 BEGIN                                       
  SELECT * INTO #POL_DWELLING_SECTION_COVERAGES_T FROM POL_DWELLING_SECTION_COVERAGES                                      
  WHERE   CUSTOMER_ID=@CUSTOMER_ID                                       
  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                              
                                  
  UPDATE #POL_DWELLING_SECTION_COVERAGES_T SET POLICY_VERSION_ID = @POLICY_NEW_VERSION             
  SELECT @TEMP_ERROR_CODE = @@ERROR                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  INSERT INTO POL_DWELLING_SECTION_COVERAGES SELECT * FROM #POL_DWELLING_SECTION_COVERAGES_T                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                        
                                  
  DROP TABLE #POL_DWELLING_SECTION_COVERAGES_T                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                               
  -- Endorsment                                                           
  SELECT * INTO #POL_DWELLING_ENDORSEMENTS_T FROM POL_DWELLING_ENDORSEMENTS                                     
  WHERE  CUSTOMER_ID=@CUSTOMER_ID                                     
  AND POLICY_ID=@POLICY_ID                                     
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                               
                                  
  UPDATE #POL_DWELLING_ENDORSEMENTS_T SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                                            
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                     
  INSERT INTO POL_DWELLING_ENDORSEMENTS SELECT * FROM #POL_DWELLING_ENDORSEMENTS_T                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                          
                                  
  DROP TABLE #POL_DWELLING_ENDORSEMENTS_T                   
 SELECT @TEMP_ERROR_CODE = @@ERROR                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                      
 END                                      
 --Added By Ravindra Ends here                                     
                              
--8                      
 SELECT * INTO #POL_OTHER_LOCATIONS FROM POL_OTHER_LOCATIONS WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                              
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
                                 
 UPDATE #POL_OTHER_LOCATIONS SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                              
 SELECT @TEMP_ERROR_CODE = @@ERROR                                     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                      
                                 
 INSERT INTO POL_OTHER_LOCATIONS SELECT * FROM #POL_OTHER_LOCATIONS                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                                 
 DROP TABLE #POL_OTHER_LOCATIONS                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                             
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                      
--10.           --changed order by pravesh on 22 jan 2008                       
 SELECT * INTO #POL_HOME_OWNER_RECREATIONAL_VEHICLES FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                    
                                 
 UPDATE #POL_HOME_OWNER_RECREATIONAL_VEHICLES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                   
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
            
 INSERT INTO POL_HOME_OWNER_RECREATIONAL_VEHICLES SELECT * FROM #POL_HOME_OWNER_RECREATIONAL_VEHICLES                                                 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                                 
 DROP TABLE #POL_HOME_OWNER_RECREATIONAL_VEHICLES                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                           
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
                                  
                         
 --9  ( Home>Recr Veh>Add Int)                      
 SELECT * INTO #POL_HOMEOWNER_REC_VEH_ADD_INT FROM POL_HOMEOWNER_REC_VEH_ADD_INT  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 UPDATE #POL_HOMEOWNER_REC_VEH_ADD_INT SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                     
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 INSERT INTO POL_HOMEOWNER_REC_VEH_ADD_INT SELECT * FROM #POL_HOMEOWNER_REC_VEH_ADD_INT                                                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                         
                                 
 DROP TABLE #POL_HOMEOWNER_REC_VEH_ADD_INT                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
                                                       
                                 
 --11.                                      
 SELECT * INTO #POL_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES                    
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                   
                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                          UPDATE #POL_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                              
 SELECT @TEMP_ERROR_CODE = @@ERROR                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                     
                                 
 INSERT INTO POL_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES SELECT * FROM #POL_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES                                                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
                             
 DROP TABLE #POL_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES                                                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                     
                                 
 --12.                                 
 SELECT * INTO #POL_HOME_OWNER_GEN_INFO FROM POL_HOME_OWNER_GEN_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 UPDATE #POL_HOME_OWNER_GEN_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                              
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                       
                      
 IF (@RENEWAL = 1 and @NO_OF_YEAR_WITH_WOL >0 )                               
  UPDATE #POL_HOME_OWNER_GEN_INFO SET YEARS_INSU_WOL=isnull(YEARS_INSU_WOL,0) + 1,YEARS_INSU= isnull(YEARS_INSU,0) + 1                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                    
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                         
--by pravesh on 7 march for Multipolicy discount                      
 IF (@RENEWAL = 1 )                      
  begin                       
--select MULTI_POLICY_DISC_APPLIED,MULTI_POLICY_DISC_APPLIED_PP_DESC,* from POL_AUTO_GEN_INFO                       
 SET @MULTI_POLICY_COUNT=0                      
                       
 INSERT INTO ##TMP_MULTIPOLICY EXECUTE Proc_GetEligiblePolicies   @CUSTOMER_ID ,  @AGENCY_ID , @RISKID , @POLICY_NUMBER                       
                    
SELECT @MULTI_POLICY_COUNT=COUNT(*)  FROM ##TMP_MULTIPOLICY                       
 SELECT TOP 1 @MULTI_POLICY_NUMBER=POLICY_NUMBER FROM ##TMP_MULTIPOLICY                      
 --DROP TABLE ##TMP_MULTIPOLICY                      
 IF (ISNULL(@MULTI_POLICY_NUMBER,'')!='N.A.' AND ISNULL(@MULTI_POLICY_NUMBER,'')!='' AND @MULTI_POLICY_COUNT>0)                      
 BEGIN                      
  UPDATE  #POL_HOME_OWNER_GEN_INFO SET MULTI_POLICY_DISC_APPLIED=1,DESC_MULTI_POLICY_DISC_APPLIED=@MULTI_POLICY_NUMBER                      
  SET @TRAN_DESC=@TRAN_DESC + 'Under writing Question "Is Multipolicy discount applied?" has been set to "Yes".;'                      
 END                      
  end                           
                                                            
                                 
 INSERT INTO POL_HOME_OWNER_GEN_INFO SELECT * FROM #POL_HOME_OWNER_GEN_INFO                                                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                                 
 DROP TABLE #POL_HOME_OWNER_GEN_INFO                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                
END                                                               
                                   
--ONLY HomeOwners                       
IF  (@RISKID = 1)                                              
BEGIN                                    
 --2.                           
                                  
 SELECT * INTO #POL_HOME_OWNER_SCH_ITEMS_CVGS FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                     
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                           
                                 
 UPDATE #POL_HOME_OWNER_SCH_ITEMS_CVGS SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 INSERT INTO POL_HOME_OWNER_SCH_ITEMS_CVGS SELECT * FROM #POL_HOME_OWNER_SCH_ITEMS_CVGS                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                             
    
 DROP TABLE #POL_HOME_OWNER_SCH_ITEMS_CVGS                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                              
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                           
                                 
 --2A Added by RP - 10 July 2006                                    
 SELECT * INTO #POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POL_ID=@POLICY_ID AND POL_VERSION_ID=@POLICY_VERSION_ID                                  
                                 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
 -- Commented  By Ravindra (10-10-2006) -- Table POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS not having Column                                  
 -- CREATED_BY & CREATED_DATETIME                      
 UPDATE #POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS SET POL_VERSION_ID = @POLICY_NEW_VERSION                           
 --,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                
 --  SELECT @TEMP_ERROR_CODE = @@ERROR                                
 --  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
 --                                      
 INSERT INTO POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS SELECT * FROM #POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 DROP TABLE #POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS                                                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                     
 -- End of additon by RP - 10 July 2006                                  
                                  
 --3.                                                    
 SELECT * INTO #POL_HOME_OWNER_PER_ART_GEN_INFO FROM POL_HOME_OWNER_PER_ART_GEN_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                           
                                 
 UPDATE #POL_HOME_OWNER_PER_ART_GEN_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
                      
 INSERT INTO POL_HOME_OWNER_PER_ART_GEN_INFO SELECT * FROM #POL_HOME_OWNER_PER_ART_GEN_INFO                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                      
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                
 DROP TABLE #POL_HOME_OWNER_PER_ART_GEN_INFO                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                       
                               
                                 
 --4.                                
 SELECT * INTO #POL_HOME_OWNER_SOLID_FUEL FROM POL_HOME_OWNER_SOLID_FUEL  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                               
 SELECT @TEMP_ERROR_CODE = @@ERROR                                           
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 UPDATE #POL_HOME_OWNER_SOLID_FUEL SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                       
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                                 
 INSERT INTO POL_HOME_OWNER_SOLID_FUEL SELECT * FROM #POL_HOME_OWNER_SOLID_FUEL                                               
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 DROP TABLE #POL_HOME_OWNER_SOLID_FUEL                                   
 SELECT @TEMP_ERROR_CODE = @@ERROR                            
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
                                 
 --5.                         
 SELECT * INTO #POL_HOME_OWNER_FIRE_PROT_CLEAN FROM POL_HOME_OWNER_FIRE_PROT_CLEAN  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                        
                       
 UPDATE #POL_HOME_OWNER_FIRE_PROT_CLEAN SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                    
                                
 INSERT INTO POL_HOME_OWNER_FIRE_PROT_CLEAN SELECT * FROM #POL_HOME_OWNER_FIRE_PROT_CLEAN                                              
 SELECT @TEMP_ERROR_CODE = @@ERROR                                       
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                                 
 DROP TABLE #POL_HOME_OWNER_FIRE_PROT_CLEAN                                              
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 --6.                                         
 SELECT * INTO #POL_HOME_OWNER_CHIMNEY_STOVE FROM POL_HOME_OWNER_CHIMNEY_STOVE  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                                 
 UPDATE #POL_HOME_OWNER_CHIMNEY_STOVE SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 INSERT INTO POL_HOME_OWNER_CHIMNEY_STOVE SELECT * FROM #POL_HOME_OWNER_CHIMNEY_STOVE                                                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 DROP TABLE #POL_HOME_OWNER_CHIMNEY_STOVE                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                     
                                  
END                         
                      
--FOr HOmeOWner and Watercraft                                                              
IF  (@RISKID = 1 or @RISKID = 4)                                                              
BEGIN                                      
IF EXISTS (                                
   SELECT CUSTOMER_ID FROM POL_WATERCRAFT_INFO WHERE                        
   CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                   
  )                          
 BEGIN                                
  --1.                         
  SELECT * INTO #POL_WATERCRAFT_INFO FROM POL_WATERCRAFT_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                              
                                  
  UPDATE #POL_WATERCRAFT_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                        
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                              
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                           
                                  
  INSERT INTO POL_WATERCRAFT_INFO SELECT * FROM #POL_WATERCRAFT_INFO                                              
  SELECT @TEMP_ERROR_CODE = @@ERROR           IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                              
                              
 DROP TABLE #POL_WATERCRAFT_INFO                                                              
SELECT @TEMP_ERROR_CODE = @@ERROR                                                          
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                                  
--2.                                                                
  SELECT * INTO #POL_WATERCRAFT_ENGINE_INFO FROM POL_WATERCRAFT_ENGINE_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                  
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                    
                                  
  UPDATE #POL_WATERCRAFT_ENGINE_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  INSERT INTO POL_WATERCRAFT_ENGINE_INFO SELECT * FROM #POL_WATERCRAFT_ENGINE_INFO                                              
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                    
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                         
                                  
  DROP TABLE #POL_WATERCRAFT_ENGINE_INFO                                                          
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                    
                                 
  --3.                                      
 --Added By Ravindra(05-22-2006)                                      
  IF @RENEWAL = 1  --In Case of renewal ineligible coverages not to be selected                                      
  BEGIN              
  --added by pravesh on 30aug 2007  If the boat is now over 20 year of age and had agreed value - on renewal change Agreed value to Actual cash value -                         
 IF EXISTS (                                
    SELECT CUSTOMER_ID FROM POL_WATERCRAFT_INFO WHERE                                 
    CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_NEW_VERSION                        
     )                                
begin                        
 CREATE TABLE #POL_WATERCRAFT_INFO_TEMP                        
 (                         
  [IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,                        
  [BOAT_ID] INT,                        
  [BOAT_NO] int,                        
  [YEAR] Int ,                        
  [COV_TYPE_BASIS] Int                        
 )                        
 INSERT INTO #POL_WATERCRAFT_INFO_TEMP                        
 (                        
  BOAT_ID ,                        
  BOAT_NO,                        
  [YEAR] ,                        
  COV_TYPE_BASIS                        
 )                        
 SELECT BOAT_ID,BOAT_NO,[YEAR],COV_TYPE_BASIS FROM POL_WATERCRAFT_INFO WHERE                                 
    CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_NEW_VERSION                        
 DECLARE @IDENT_COL Int                  
 DECLARE @BOAT_TRANS NVARCHAR(800)                  
 DECLARE @BOAT_TRAN NVARCHAR(300)                  
SET @BOAT_TRANS=''                  
SET @BOAT_TRAN=''                  
 SET @IDENT_COL = 1                        
 WHILE 1 = 1                                  
 BEGIN                          
IF NOT EXISTS                              
  (                                    
 SELECT IDENT_COL FROM #POL_WATERCRAFT_INFO_TEMP                                    
 WHERE IDENT_COL = @IDENT_COL                                      
 )                                      
 BEGIN                                      
  BREAK                   
 END                   
 declare @COVERAGE_BASE INT,@BOAT_AGE INT,@BOAT_ID INT,@BOAT_NO int ,@BOAT_AGE_OLD INT ,@IS_CHANGE SMALLINT                  
 SET @COVERAGE_BASE=0                        
 SET @BOAT_AGE=0                    
 SET @IS_CHANGE=0                      
 SELECT @BOAT_ID=BOAT_ID,@BOAT_NO=BOAT_NO, @BOAT_AGE=YEAR(@NEW_APP_EFFECTIVE_DATE)- [YEAR],@BOAT_AGE_OLD=YEAR(@APP_EFFECTIVE_DATE)- [YEAR],@COVERAGE_BASE=ISNULL(COV_TYPE_BASIS,0) FROM #POL_WATERCRAFT_INFO_TEMP                         
 WHERE  IDENT_COL=@IDENT_COL                        
 IF(@BOAT_AGE>20 AND @COVERAGE_BASE=11759 ) --11759=AGREED VALUE                        
 begin                        
  UPDATE POL_WATERCRAFT_INFO SET COV_TYPE_BASIS=11758 WHERE                                 
  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_NEW_VERSION AND  BOAT_ID=@BOAT_ID                        
  --SET @TRAN_DESC=@TRAN_DESC + 'Boat# ' + convert(varchar,@BOAT_NO) + ': Coverage type basis Changed from Agreed value to Actual Case Value as Age of Boat is over 20 and hence coverages modified.;'                        
  SET @BOAT_TRAN = 'Boat# ' + convert(varchar,@BOAT_NO) + ': Coverage type basis Changed from Agreed value to Actual Case Value as Age of Boat is over 20 and hence coverages modified.;'                        
  SET @BOAT_TRANS =@BOAT_TRANS + @BOAT_TRAN                  
  set @COVERAGE_BASE_CHANGED_BOAT_ID=@COVERAGE_BASE_CHANGED_BOAT_ID + convert(varchar,@BOAT_ID) + ','                        
  SET @IS_CHANGE=1                  
 end                       
 else IF(@BOAT_AGE>5 and @BOAT_AGE_OLD <6)                      
 begin                      
  set @COVERAGE_BASE_CHANGED_BOAT_ID=@COVERAGE_BASE_CHANGED_BOAT_ID + convert(varchar,@BOAT_ID) + ','                    
     --SET @TRAN_DESC=@TRAN_DESC + 'Boat# ' + convert(varchar,@BOAT_NO) + ':Coverages modified for this Boat as Age of Boat changed.;'                   
      SET @BOAT_TRAN = 'Boat# ' + convert(varchar,@BOAT_NO) + ':Coverages/Deductible modified due to change in Boat''''s Age.;'                   
  SET @BOAT_TRANS =@BOAT_TRANS + @BOAT_TRAN                  
  SET @IS_CHANGE=1       
 end                   
 --UPDATING REFER TO UNDER WRITER TO YES AS Coverages Modified                    
 IF (@IS_CHANGE=1)                   
 BEGIN                  
  UPDATE POL_CUSTOMER_POLICY_LIST                       
  SET REFER_UNDERWRITER='Y',                  
  REFERAL_INSTRUCTIONS = CASE WHEN ISNULL(REFERAL_INSTRUCTIONS,'') ='' THEN ISNULL(REFERAL_INSTRUCTIONS,'') + @BOAT_TRAN  ELSE  ISNULL(REFERAL_INSTRUCTIONS,'') + ',' + @BOAT_TRAN END                      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_NEW_VERSION                        
     END                    
 SET @IDENT_COL = @IDENT_COL + 1                      
 END                   
SET @TRAN_DESC=@TRAN_DESC +  @BOAT_TRANS                       
 DROP TABLE #POL_WATERCRAFT_INFO_TEMP                        
end                        
 --added by pravesh end here                    
   SELECT * INTO #POL_WATERCRAFT_COVERAGE_INFO FROM POL_WATERCRAFT_COVERAGE_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID                                       
   AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                      
   AND COVERAGE_CODE_ID NOT IN (SELECT COV_ID FROM #IN_EFFECTIVE_COVERAGES)                                       
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                               
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                     
                         
 SELECT @HAS_INVALID_COVERAGE=COUNT(COVERAGE_CODE_ID) FROM POL_WATERCRAFT_COVERAGE_INFO                                        
  WHERE  CUSTOMER_ID=@CUSTOMER_ID                                       
  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
  AND                                       
  COVERAGE_CODE_ID IN (SELECT COV_ID FROM #IN_EFFECTIVE_COVERAGES)                          
                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                               
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                         
                                
   SELECT @HAS_INVALID_COVERAGE_LIMIT=COUNT(COVERAGE_CODE_ID) FROM POL_WATERCRAFT_COVERAGE_INFO                                        
   WHERE  CUSTOMER_ID=@CUSTOMER_ID                                    
   AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                           
  AND                      
   (                                      
   COVERAGE_CODE_ID IN (SELECT COV_ID FROM #IN_EFFECTIVE_COVERAGES)                      
   OR                                      
   LIMIT_ID  IN (SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                       
   OR                                      
   DEDUC_ID IN(SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                              
   )                             
                                   
   UPDATE #POL_WATERCRAFT_COVERAGE_INFO SET LIMIT_ID=NULL,                                       
   LIMIT_1=NULL,                                   
   LIMIT_2=NULL,                                      
LIMIT1_AMOUNT_TEXT=NULL,                                      
   LIMIT2_AMOUNT_TEXT=NULL                                      
   WHERE                                       
   LIMIT_ID IN (SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                      
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                 
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                   
   UPDATE #POL_WATERCRAFT_COVERAGE_INFO SET DEDUC_ID=NULL,                                       
   DEDUCTIBLE_1=NULL,                                      
   DEDUCTIBLE_2=NULL,                                      
   DEDUCTIBLE1_AMOUNT_TEXT=NULL,                                 
   DEDUCTIBLE2_AMOUNT_TEXT=NULL                              
   WHERE                                
   DEDUC_ID IN(SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                      
   SELECT @TEMP_ERROR_CODE = @@ERROR                                         
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                                   
                  
   UPDATE #POL_WATERCRAFT_COVERAGE_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                  
   SELECT @TEMP_ERROR_CODE = @@ERROR                             
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                         
                            
   INSERT INTO POL_WATERCRAFT_COVERAGE_INFO SELECT * FROM #POL_WATERCRAFT_COVERAGE_INFO                                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                  
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                   
   DROP TABLE #POL_WATERCRAFT_COVERAGE_INFO                                                             
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                    
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
   --added by pravesh on 22 june 2007 to remove grandfathered option in case of trailer deductuble                        
  SELECT * INTO #POL_WATERCRAFT_TRAILER_INFO_T FROM POL_WATERCRAFT_TRAILER_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                              
                           
  SELECT @HAS_INVALID_COVERAGE_LIMIT=COUNT(TRAILER_DED_ID) FROM POL_WATERCRAFT_TRAILER_INFO                                        
   WHERE  CUSTOMER_ID=@CUSTOMER_ID                                       
   AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
   AND                                       
   (                                      
     TRAILER_DED_ID  IN (SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                       
   )                             
                        
 UPDATE #POL_WATERCRAFT_TRAILER_INFO_T SET TRAILER_DED=NULL,                                       
   TRAILER_DED_ID=NULL,                                      
   TRAILER_DED_AMOUNT_TEXT=NULL                                      
   WHERE                                       
   TRAILER_DED_ID IN(SELECT  LIMIT_DEDUC_ID FROM #IN_EFFECTIVE_COVERAGE_RANGES)                                      
   SELECT @TEMP_ERROR_CODE = @@ERROR                                         
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
                                  
  UPDATE #POL_WATERCRAFT_TRAILER_INFO_T SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                      
  SELECT @TEMP_ERROR_CODE = @@ERROR                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                               
                                  
  INSERT INTO POL_WATERCRAFT_TRAILER_INFO SELECT * FROM #POL_WATERCRAFT_TRAILER_INFO_T                                                           
  SELECT @TEMP_ERROR_CODE = @@ERROR                                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                           
                                  
  DROP TABLE #POL_WATERCRAFT_TRAILER_INFO_T                                                             
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
  ---added by pravesh end here                                            -- Copy eligible endorsments                                      
   --4.                    
   SELECT * INTO #POL_WATERCRAFT_ENDORSEMENTS_T FROM POL_WATERCRAFT_ENDORSEMENTS WHERE                                       
   CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
   AND ENDORSEMENT_ID NOT IN (SELECT ENDORSMENT_ID FROM #IN_EFFECTIVE_ENDORSMENT)                       
         
                       
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                   
                           
   UPDATE #POL_WATERCRAFT_ENDORSEMENTS_T SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                              
   SELECT @TEMP_ERROR_CODE = @@ERROR            
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
   --ADDED BY PRAVESH ON 7 FEB 2007 FOR EDITION DATE                                
   UPDATE #POL_WATERCRAFT_ENDORSEMENTS_T SET EDITION_DATE = NULL WHERE EDITION_DATE IN                        
         (SELECT ENDORSEMENT_ATTACH_ID FROM  #IN_EFFECTIVE_ENDORSMENT_ATTACHMENT)                                
   SELECT @TEMP_ERROR_CODE = @@ERROR                     
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
   --END HERE                            
   INSERT INTO POL_WATERCRAFT_ENDORSEMENTS SELECT * FROM #POL_WATERCRAFT_ENDORSEMENTS_T                                                           
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                   
   DROP TABLE #POL_WATERCRAFT_ENDORSEMENTS_T                                      
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                              
                                   
                                   
  END                                     
  ELSE -- In  Case of Endorsment Process Copy All                                       
  BEGIN                                    
   SELECT * INTO #POL_WATERCRAFT_COVERAGE_INFO_T FROM POL_WATERCRAFT_COVERAGE_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID                             
   AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                               
                                   
   UPDATE #POL_WATERCRAFT_COVERAGE_INFO_T SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                           
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                                   
 INSERT INTO POL_WATERCRAFT_COVERAGE_INFO SELECT * FROM #POL_WATERCRAFT_COVERAGE_INFO_T                                                           
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                          
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                       
                                   
   DROP TABLE #POL_WATERCRAFT_COVERAGE_INFO_T                                                             
   SELECT @TEMP_ERROR_CODE = @@ERROR             
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                        
   --4. Copy Endorsments                            
   SELECT * INTO #POL_WATERCRAFT_ENDORSEMENTS FROM POL_WATERCRAFT_ENDORSEMENTS WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                        
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                              
                                   
   UPDATE #POL_WATERCRAFT_ENDORSEMENTS SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                                              
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                          
                                   
   INSERT INTO POL_WATERCRAFT_ENDORSEMENTS SELECT * FROM #POL_WATERCRAFT_ENDORSEMENTS                               
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                          
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                   
   DROP TABLE #POL_WATERCRAFT_ENDORSEMENTS                   
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
  --9. shifted to here by pravesh                        
  SELECT * INTO #POL_WATERCRAFT_TRAILER_INFO FROM POL_WATERCRAFT_TRAILER_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                        
  SELECT @TEMP_ERROR_CODE = @@ERROR                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                           
                                  
  UPDATE #POL_WATERCRAFT_TRAILER_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                               
 SELECT @TEMP_ERROR_CODE = @@ERROR                    
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                            
                                  
  INSERT INTO POL_WATERCRAFT_TRAILER_INFO SELECT * FROM #POL_WATERCRAFT_TRAILER_INFO                                             
  SELECT @TEMP_ERROR_CODE = @@ERROR                                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  DROP TABLE #POL_WATERCRAFT_TRAILER_INFO                                    
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                    
                     
                                           
  END                                      
      --- Added By Ravindra ends here                                      
                              
  --5.                                          
  SELECT * INTO #POL_WATERCRAFT_COV_ADD_INT FROM POL_WATERCRAFT_COV_ADD_INT WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                   
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                           
                                  
  UPDATE #POL_WATERCRAFT_COV_ADD_INT SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                   
  SELECT @TEMP_ERROR_CODE = @@ERROR                             IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                
                          
  INSERT INTO POL_WATERCRAFT_COV_ADD_INT SELECT * FROM #POL_WATERCRAFT_COV_ADD_INT                                                            
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                           
                                  
  DROP TABLE #POL_WATERCRAFT_COV_ADD_INT                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                    
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
  --6.                                                              
  SELECT * INTO #POL_WATERCRAFT_GEN_INFO FROM POL_WATERCRAFT_GEN_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                            
  SELECT @TEMP_ERROR_CODE = @@ERROR                     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                               
                    
  UPDATE #POL_WATERCRAFT_GEN_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                           
  SELECT @TEMP_ERROR_CODE = @@ERROR                                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
 /*                        
 IF (@RENEWAL = 1 and @NO_OF_YEAR_WITH_WOL >0 )                       
  UPDATE #POL_WATERCRAFT_GEN_INFO SET YEARS_INSU_WOL=isnull(YEARS_INSU_WOL,0) + 1                       
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                              
   */                         
--by pravesh on 7 march for Multipolicy discount                      
 IF (@RENEWAL = 1 )                      
  begin                       
--select MULTI_POLICY_DISC_APPLIED,MULTI_POLICY_DISC_APPLIED_PP_DESC,* from POL_AUTO_GEN_INFO                       
 SET @MULTI_POLICY_COUNT=0                      
                      
 INSERT INTO ##TMP_MULTIPOLICY EXECUTE Proc_GetEligiblePolicies   @CUSTOMER_ID ,  @AGENCY_ID , @RISKID , @POLICY_NUMBER                   
 SELECT @MULTI_POLICY_COUNT=COUNT(*)  FROM ##TMP_MULTIPOLICY                       
 SELECT TOP 1 @MULTI_POLICY_NUMBER=POLICY_NUMBER FROM ##TMP_MULTIPOLICY                       
                       
 IF (ISNULL(@MULTI_POLICY_NUMBER,'')!='N.A.' AND ISNULL(@MULTI_POLICY_NUMBER,'')!='' AND @MULTI_POLICY_COUNT>0)                      
 BEGIN                      
  UPDATE  #POL_WATERCRAFT_GEN_INFO SET MULTI_POLICY_DISC_APPLIED=1,MULTI_POLICY_DISC_APPLIED_PP_DESC=@MULTI_POLICY_NUMBER           SET @TRAN_DESC=@TRAN_DESC + 'Under writing Question "Is Multipolicy discount applied?" has been set to "Yes".;'           
  
    
      
        
          
           
 END                      
  end                              
                  
  INSERT INTO POL_WATERCRAFT_GEN_INFO SELECT * FROM #POL_WATERCRAFT_GEN_INFO                                                            
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  DROP TABLE #POL_WATERCRAFT_GEN_INFO                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                            
                                  
  --7.                                                                
  SELECT * INTO #POL_WATERCRAFT_DRIVER_DETAILS FROM POL_WATERCRAFT_DRIVER_DETAILS  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                       
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                                  
UPDATE #POL_WATERCRAFT_DRIVER_DETAILS SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                               
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
  INSERT INTO POL_WATERCRAFT_DRIVER_DETAILS SELECT * FROM #POL_WATERCRAFT_DRIVER_DETAILS                                                            
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                              
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                                  
  DROP TABLE #POL_WATERCRAFT_DRIVER_DETAILS                                                          
  SELECT @TEMP_ERROR_CODE = @@ERROR                               
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
  --8.                        
  SELECT * INTO #POL_WATERCRAFT_MVR_INFORMATION FROM POL_WATERCRAFT_MVR_INFORMATION  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                           
                                  
  UPDATE #POL_WATERCRAFT_MVR_INFORMATION SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                              
                                  
  INSERT INTO POL_WATERCRAFT_MVR_INFORMATION SELECT * FROM #POL_WATERCRAFT_MVR_INFORMATION                        
  SELECT @TEMP_ERROR_CODE = @@ERROR                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                           
                                  
  DROP TABLE #POL_WATERCRAFT_MVR_INFORMATION                                           
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                           
                      
 --9 Assign boats Praveen k                      
 SELECT * INTO #POL_OPERATOR_ASSIGNED_BOAT FROM POL_OPERATOR_ASSIGNED_BOAT WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                       
 UPDATE #POL_OPERATOR_ASSIGNED_BOAT SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                            
                         
 INSERT INTO POL_OPERATOR_ASSIGNED_BOAT SELECT * FROM #POL_OPERATOR_ASSIGNED_BOAT                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                                 
 DROP TABLE #POL_OPERATOR_ASSIGNED_BOAT                            
 SELECT @TEMP_ERROR_CODE = @@ERROR         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                            
                        
  --9.                        
/*  this code shifted to else part of renewal above  by pravesh                                                         
  SELECT * INTO #POL_WATERCRAFT_TRAILER_INFO FROM POL_WATERCRAFT_TRAILER_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                           
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
                                  
  UPDATE #POL_WATERCRAFT_TRAILER_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                             
  SELECT @TEMP_ERROR_CODE = @@ERROR                            
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                             
                                  
  INSERT INTO POL_WATERCRAFT_TRAILER_INFO SELECT * FROM #POL_WATERCRAFT_TRAILER_INFO                      
  SELECT @TEMP_ERROR_CODE = @@ERROR           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                              
  DROP TABLE #POL_WATERCRAFT_TRAILER_INFO                                                             
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                         
                                  
  */                                
  --10.                                                
  SELECT * INTO #POL_WATERCRAFT_TRAILER_ADD_INT FROM POL_WATERCRAFT_TRAILER_ADD_INT WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  UPDATE #POL_WATERCRAFT_TRAILER_ADD_INT SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                            
                                
  INSERT INTO POL_WATERCRAFT_TRAILER_ADD_INT SELECT * FROM #POL_WATERCRAFT_TRAILER_ADD_INT                                                            
  SELECT @TEMP_ERROR_CODE = @@ERROR                        
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                         
                                  
  DROP TABLE #POL_WATERCRAFT_TRAILER_ADD_INT                                          
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                                  
  --11.                   
  SELECT * INTO #POL_WATERCRAFT_EQUIP_DETAILLS FROM POL_WATERCRAFT_EQUIP_DETAILLS  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR           IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                  
  UPDATE #POL_WATERCRAFT_EQUIP_DETAILLS SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,CREATED_DATETIME = @DATE                                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                                  
  INSERT INTO POL_WATERCRAFT_EQUIP_DETAILLS SELECT * FROM #POL_WATERCRAFT_EQUIP_DETAILLS                                         
  SELECT @TEMP_ERROR_CODE = @@ERROR                       
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                              
                   
  DROP TABLE #POL_WATERCRAFT_EQUIP_DETAILLS                                                           
  SELECT @TEMP_ERROR_CODE = @@ERROR                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                   
END                                          
END                               
          
-- Added By Ravindra Gupta (03-17-2006)                                          
-- To create new version for Umbrella Policy                                          
                               
IF(@RISKID = 5)                                                              
BEGIN                                                
                                         
 --1. POL_UMBRELLA_LIMITS                                          
 SELECT * INTO #POL_UMBRELLA_LIMITS FROM POL_UMBRELLA_LIMITS WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                                           
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                          
                                 
 UPDATE #POL_UMBRELLA_LIMITS SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,                             
 CREATED_DATETIME = @DATE                                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                              
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
                       
 INSERT INTO POL_UMBRELLA_LIMITS SELECT * FROM #POL_UMBRELLA_LIMITS                                                 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                      
                                 
 DROP TABLE #POL_UMBRELLA_LIMITS                                                             
 SELECT @TEMP_ERROR_CODE = @@ERROR                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                                 
 -- 2.  POL_UMBRELLA_REAL_ESTATE_LOCATION                                       
                                 
 SELECT * INTO #POL_UMBRELLA_REAL_ESTATE_LOCATION FROM POL_UMBRELLA_REAL_ESTATE_LOCATION WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                                           
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 UPDATE #POL_UMBRELLA_REAL_ESTATE_LOCATION SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,                    
 CREATED_DATETIME = @DATE                             
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                 
                                 
 INSERT INTO POL_UMBRELLA_REAL_ESTATE_LOCATION SELECT * FROM #POL_UMBRELLA_REAL_ESTATE_LOCATION                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                 
 DROP TABLE #POL_UMBRELLA_REAL_ESTATE_LOCATION                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                           
                                 
 -- 3. POL_UMBRELLA_DWELLINGS_INFO                     
                                 
 SELECT * INTO #POL_UMBRELLA_DWELLINGS_INFO FROM POL_UMBRELLA_DWELLINGS_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                                           
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                     
SELECT @TEMP_ERROR_CODE = @@ERROR                                                                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                       
                                
 UPDATE #POL_UMBRELLA_DWELLINGS_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,                      
 CREATED_DATETIME = @DATE                                     
 SELECT @TEMP_ERROR_CODE = @@ERROR                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                            
                                 
 INSERT INTO POL_UMBRELLA_DWELLINGS_INFO SELECT * FROM #POL_UMBRELLA_DWELLINGS_INFO                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                                 
 DROP TABLE #POL_UMBRELLA_DWELLINGS_INFO                                                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                              
                           
                        
 -- 4.  POL_UMBRELLA_RATING_INFO                                          
                             
 SELECT * INTO #POL_UMBRELLA_RATING_INFO FROM POL_UMBRELLA_RATING_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                                           
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                            
                              
 UPDATE #POL_UMBRELLA_RATING_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION                          
   ---,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                   
 SELECT @TEMP_ERROR_CODE = @@ERROR                          
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
                                 
 INSERT INTO POL_UMBRELLA_RATING_INFO SELECT * FROM #POL_UMBRELLA_RATING_INFO                                                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 DROP TABLE #POL_UMBRELLA_RATING_INFO                                                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                                 
 -- 5. POL_UMBRELLA_VEHICLE_INFO                                     
                                 
 SELECT * INTO #POL_UMBRELLA_VEHICLE_INFO FROM POL_UMBRELLA_VEHICLE_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                                           
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                       
                                 
 UPDATE #POL_UMBRELLA_VEHICLE_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,                                          
 CREATED_DATETIME = @DATE                                       
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                    
                                 
 INSERT INTO POL_UMBRELLA_VEHICLE_INFO SELECT * FROM #POL_UMBRELLA_VEHICLE_INFO                                                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                       
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                                 
 DROP TABLE #POL_UMBRELLA_VEHICLE_INFO                     
 SELECT @TEMP_ERROR_CODE = @@ERROR                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                                 
 -- 6. POL_UMBRELLA_RECREATIONAL_VEHICLES                                
                                 
 SELECT * INTO #POL_UMBRELLA_RECREATIONAL_VEHICLES FROM POL_UMBRELLA_RECREATIONAL_VEHICLES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                             
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                             
 SELECT @TEMP_ERROR_CODE = @@ERROR                            
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                              
                  
 UPDATE #POL_UMBRELLA_RECREATIONAL_VEHICLES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,                                          
 CREATED_DATETIME = @DATE                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                        
 INSERT INTO POL_UMBRELLA_RECREATIONAL_VEHICLES SELECT * FROM #POL_UMBRELLA_RECREATIONAL_VEHICLES                                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 DROP TABLE #POL_UMBRELLA_RECREATIONAL_VEHICLES                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                        
                                 
 -- 7. POL_UMBRELLA_WATERCRAFT_INFO                                          
                                 
 SELECT * INTO #POL_UMBRELLA_WATERCRAFT_INFO FROM POL_UMBRELLA_WATERCRAFT_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                                           
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                       
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 UPDATE #POL_UMBRELLA_WATERCRAFT_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,                                          
 CREATED_DATETIME = @DATE                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
                                 
 INSERT INTO POL_UMBRELLA_WATERCRAFT_INFO SELECT * FROM #POL_UMBRELLA_WATERCRAFT_INFO                                                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM             
                                 
 DROP TABLE #POL_UMBRELLA_WATERCRAFT_INFO                                                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                                 
                                 
 --8.  POL_UMBRELLA_WATERCRAFT_ENGINE_INFO                                          
                        
 SELECT * INTO #POL_UMBRELLA_WATERCRAFT_ENGINE_INFO FROM POL_UMBRELLA_WATERCRAFT_ENGINE_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                               
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                     
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                    
                                 
 UPDATE #POL_UMBRELLA_WATERCRAFT_ENGINE_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,                      
CREATED_DATETIME = @DATE                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
                                 
 INSERT INTO POL_UMBRELLA_WATERCRAFT_ENGINE_INFO SELECT * FROM #POL_UMBRELLA_WATERCRAFT_ENGINE_INFO                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                                 
 DROP TABLE #POL_UMBRELLA_WATERCRAFT_ENGINE_INFO                                                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                       
                              
                         
 -- 9. POL_UMBRELLA_UNDERLYING_POLICIES                          
           
 SELECT * INTO #POL_UMBRELLA_UNDERLYING_POLICIES FROM POL_UMBRELLA_UNDERLYING_POLICIES WHERE CUSTOMER_ID=@CUSTOMER_ID AND                                           
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                       
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                     
                                 
 UPDATE #POL_UMBRELLA_UNDERLYING_POLICIES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,                                          
 CREATED_DATETIME = @DATE                            
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                       
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                         
                                 
 INSERT INTO POL_UMBRELLA_UNDERLYING_POLICIES SELECT * FROM #POL_UMBRELLA_UNDERLYING_POLICIES                                                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                             
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                                 
 DROP TABLE #POL_UMBRELLA_UNDERLYING_POLICIES                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                                 
                                 
 --  10.  POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES                                          
                                 
 SELECT * INTO #POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                                           
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                            
                                 
 UPDATE #POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,                                  
 CREATED_DATETIME = @DATE                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 INSERT INTO POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES SELECT * FROM #POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES                                       
 SELECT @TEMP_ERROR_CODE = @@ERROR                             
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 DROP TABLE #POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES     
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                                 
 -- 11.  POL_UMBRELLA_DRIVER_DETAILS                                          
                                 
 SELECT * INTO #POL_UMBRELLA_DRIVER_DETAILS FROM POL_UMBRELLA_DRIVER_DETAILS WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                                           
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                   
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                      
 UPDATE #POL_UMBRELLA_DRIVER_DETAILS SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,                                          
 CREATED_DATETIME = @DATE                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                 
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                           
                                 
 INSERT INTO POL_UMBRELLA_DRIVER_DETAILS SELECT * FROM #POL_UMBRELLA_DRIVER_DETAILS                                                       
 SELECT @TEMP_ERROR_CODE = @@ERROR                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                  
 DROP TABLE #POL_UMBRELLA_DRIVER_DETAILS                   
 SELECT @TEMP_ERROR_CODE = @@ERROR                                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                        
                     
 -- Added By Ravindra(03-24-2006) for POL_UMBRELLA_MVR_INFORMATION                                      
 -- 12.  POL_UMBRELLA_MVR_INFORMATION                                         
                            
 SELECT * INTO #POL_UMBRELLA_MVR_INFORMATION FROM POL_UMBRELLA_MVR_INFORMATION WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                                 
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                     
                                 
 UPDATE #POL_UMBRELLA_MVR_INFORMATION SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,                                          
 CREATED_DATETIME = @DATE                                                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 INSERT INTO POL_UMBRELLA_MVR_INFORMATION SELECT * FROM #POL_UMBRELLA_MVR_INFORMATION                          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 DROP TABLE #POL_UMBRELLA_MVR_INFORMATION                                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                                       
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                       
                                 
                                 
 -- 13.   POL_UMBRELLA_FARM_INFO                                          
                                 
 SELECT * INTO #POL_UMBRELLA_FARM_INFO FROM POL_UMBRELLA_FARM_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                                           
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                 
 SELECT @TEMP_ERROR_CODE = @@ERROR                             
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                        
                                 
 UPDATE #POL_UMBRELLA_FARM_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,                              
 CREATED_DATETIME = @DATE                                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 INSERT INTO POL_UMBRELLA_FARM_INFO SELECT * FROM #POL_UMBRELLA_FARM_INFO                                                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                     
                      
 DROP TABLE #POL_UMBRELLA_FARM_INFO                                                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                    
                                 
 --1.     POL_UMBRELLA_GEN_INFO                                     
                                 
 SELECT * INTO #POL_UMBRELLA_GEN_INFO FROM POL_UMBRELLA_GEN_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                                           
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                         
                           
 UPDATE #POL_UMBRELLA_GEN_INFO SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY,                                          
 CREATED_DATETIME = @DATE                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                             
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
                                 
 INSERT INTO POL_UMBRELLA_GEN_INFO SELECT * FROM #POL_UMBRELLA_GEN_INFO                                                   
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                            
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                         
 DROP TABLE #POL_UMBRELLA_GEN_INFO                                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                          
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
 --added by pravesh for umbrella coverages                                  
 SELECT * INTO #POL_UMBRELLA_COVERAGES_T FROM POL_UMBRELLA_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID                                       
 AND POL_ID=@POLICY_ID AND POL_VERSION_ID=@POLICY_VERSION_ID                                                              
     
 UPDATE #POL_UMBRELLA_COVERAGES_T SET POL_VERSION_ID = @POLICY_NEW_VERSION, CREATED_BY = @CREATED_BY,                                 
 CREATED_DATETIME = @DATE                                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                               
                                 
 INSERT INTO POL_UMBRELLA_COVERAGES SELECT * FROM #POL_UMBRELLA_COVERAGES_T                                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                              
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                 
 DROP TABLE #POL_UMBRELLA_COVERAGES_T                                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                      
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                              
 -------end  HERE                           
                  
END                                          
-- Added By Ravindra Ends Here                                                                           
                              
--Added By Ravindra (05-22-2006)                                      
--Drop temporary tables used for Coverage Filteration                    
--Added By Lalit for copy Benificiary Tab data          
                  
-----Copy Policy Discount/Surcharge Table                         
SELECT * INTO #POL_BENEFICIARY FROM POL_BENEFICIARY WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                   
SELECT @TEMP_ERROR_CODE = @@ERROR                               
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                          
UPDATE #POL_BENEFICIARY SET POLICY_VERSION_ID = @POLICY_NEW_VERSION,CREATED_BY = @CREATED_BY, CREATED_DATETIME = @DATE                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                       
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                        
                              
INSERT INTO POL_BENEFICIARY                                               
SELECT * FROM #POL_BENEFICIARY                                              
SELECT @TEMP_ERROR_CODE = @@ERROR                                                 
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                      
DROP TABLE #POL_BENEFICIARY                            
SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
          
--End Benificiary          
          
          
          
                             
DROP TABLE #IN_EFFECTIVE_COVERAGES                                      
SELECT @TEMP_ERROR_CODE = @@ERROR                                                   
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
                                
DROP TABLE #IN_EFFECTIVE_COVERAGE_RANGES                              
SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                        
                                
                                
DROP TABLE #IN_EFFECTIVE_ENDORSMENT                                    
DROP TABLE #IN_EFFECTIVE_ENDORSMENT_ATTACHMENT                         
DROP TABLE ##TMP_MULTIPOLICY                      
                    
SELECT @TEMP_ERROR_CODE = @@ERROR                                         
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                        
-- If some Invalid Coverages has been copied in the Policy New Version create a diary entry for same                                
IF (@HAS_INVALID_COVERAGE >0  or @HAS_INVALID_COVERAGE_LIMIT >0 )                                   
BEGIN                                      
 UPDATE POL_CUSTOMER_POLICY_LIST SET ALL_DATA_VALID =2                                      
WHERE CUSTOMER_ID=@CUSTOMER_ID                                       
 AND POLICY_ID=@POLICY_ID                                    
 AND POLICY_VERSION_ID=@POLICY_NEW_VERSION                                        
                           
SET @INVALID_COVERAGE = @HAS_INVALID_COVERAGE                   
SET  @TRAN_DESC=isnull(@TRAN_DESC,'') + 'Some Invalid Coverages/Limits/Deductibles/Endorsements are not copied to Renewal.;'                              
 -- Insert Diary entry                                            
 -- Commented by anurag verma on 21/03/2007 as diary entry will fire from code file                                
 /*                             
 DECLARE @UNDERWRITER INT                                             
                                 
 SELECT @UNDERWRITER=ISNULL(UNDERWRITER,0)                                           
 FROM POL_CUSTOMER_POLICY_LIST                            
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID                        
 AND POLICY_VERSION_ID=@POLICY_NEW_VERSION                                    
                                 
                                 
 INSERT into TODOLIST                                   
 (                                                                                       
 RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,POLICYCLIENTID,POLICYID,                                            
 POLICYVERSION,POLICYCARRIERID,POLICYBROKERID,SUBJECTLINE,LISTOPEN,                                  
 SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,FROMUSERID,STARTTIME,ENDTIME,NOTE,                                 
 PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,FROMENTITYID,                           
CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID                            
 )                   
 values                                        
 (                                     
 null,getdate(),getdate(),16,@CUSTOMER_ID,@POLICY_ID,                                                                            
 @POLICY_NEW_VERSION,null, null,'Ineligible Coverages Not Copied in Renewal','Y',                            
 null,'M',@CREATED_BY,@CREATED_BY,null,null,null,                                               
 null,null,null,null,null,null,                             
 @CUSTOMER_ID, null,null,@POLICY_ID,@POLICY_NEW_VERSION                                         
 )*/                                                         
                                 
                                         
 select @TEMP_ERROR_CODE = @@ERROR                                                              
   if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                         
END                            
                                      
   -- Added By Ravindra Ends Here                      
   --ROLLBACK TRAN                                                                                                   
   COMMIT TRAN                                      
   SET @NEW_VERSION=@POLICY_NEW_VERSION                                                                                   
   SET  @NEW_DISP_VERSION =@CURRENT_DISP --@POLICY_DISP_VERSION  --Changed by Lalit Dec 17,2010                              
                   
   RETURN @NEW_VERSION                                               
                                                          
  PROBLEM:                                                              
   ROLLBACK TRAN                                                            
   SET @NEW_VERSION = -1                                                                       
   RETURN @NEW_VERSION                                                  
                                    
END                        
                        
                        
-- go                        
-- declare  @NEW_VERSION  INT   ,                        
--  @NEW_DISP_VERSION  nvarchar(5),                        
--  @INVALID_COVERAGE int ,           
--  @NEW_DISP_VERSION_REWRITABLE NVARCHAR(50) = NULL          
                         
-- exec dbo.Proc_PolicyCreateNewVersion 2156,738,1,398, @NEW_VERSION out ,null, @NEW_DISP_VERSION out,  null,   @INVALID_COVERAGE out  ,null   , @NEW_DISP_VERSION_REWRITABLE OUT,NULL          
----SELECT * FROM  POL_BENEFICIARY WHERE CUSTOMER_ID = 28169 and POLICY_ID = 47          
--SELECT @NEW_VERSION     ,@NEW_DISP_VERSION                                                                   
-- --select  @NEW_VERSION,@NEW_DISP_VERSION,@TRAN_DESC   ,@INVALID_COVERAGE ,@COVERAGE_BASE_CHANGED_BOAT_ID                       
-- --select POLICY_LOB,POLICY_SUBLOB,* from POL_CUSTOMER_POLICY_LIST where CUSTOMER_ID = 2156 and POLICY_ID = 661             
-- select WRITTEN_PREMIUM,* from POL_PRODUCT_COVERAGES WITH(NOLOCK) where CUSTOMER_ID = 2156 and POLICY_ID = 738             
                                                                   
-- rollback tran 