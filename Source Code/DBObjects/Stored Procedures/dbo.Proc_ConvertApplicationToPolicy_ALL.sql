IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ConvertApplicationToPolicy_ALL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ConvertApplicationToPolicy_ALL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/********************************************************************************       
Created By  :  Pravesh Chandel        
Dated       :  19 Oct 2006         
Purpose     :  To insert Comman Data while Coverting app to Pol        
Modified by : Pawan    (Added APPLY_INSURANCE_SCORE column at the end of app_list insert)

Modified by :Praveen (Added Customer Info in ACT_APP_CREDIT_CARD_DETAILS)

modified by : Pravesh k Chandel
modified date	:6 aug 2007
purpose		: to update App_status in Pol_customer_policy_list to 'Complete' while converting App to policy as per Itrack Issue 2278

modified by : praveen k
modified date : 18 march 2008
purpose    : Added Insurance Scroe REASONS CODE (1,2,3,4) 

modified by : praveen k
modified date : 10 june 2008
purpose       : Added DFI No and Trans ID check to Import REVERIFIED Account

modified by : praveen k
modified date : 20 june 2008
purpose       : REmoved Fildes from ACT_APP_CREDIT_CARD_DETAILS (PAY_PAL_REF_ID)

modified by : praveen k
modified date : 24 Sep 2009
purpose       : Copy DATE fields

    
*********************************************************************************/        
        
--DROP PROC dbo.Proc_ConvertApplicationToPolicy_ALL        
CREATE proc [dbo].[Proc_ConvertApplicationToPolicy_ALL]                                                                                               
@CUSTOMER_ID int,                                                                                              
@APP_ID int,                                                                                              
@APP_VERSION_ID smallint,                                                                                              
@CREATED_BY int,                                                                                              
@PARAM1 int = NULL,                                                                      
@PARAM2 int = NULL,                                                                  
@PARAM3 int = NULL,                                   
@CALLED_FROM NVARCHAR(30),                                                                   
@RESULT int output        
AS             
BEGIN         
    
IF EXISTS (SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST                                           
WHERE CUSTOMER_ID = @CUSTOMER_ID  AND APP_ID = @APP_ID)                                          
BEGIN                                          
 SET @RESULT = -1                                    
 RETURN @RESULT                                         
END         
ELSE                                          
BEGIN                                                                                          
 declare @TEMP_ERROR_CODE int                                                                                             
 declare @TEMP_POLICY_ID int                                                                                               
 declare @TEMP_POLICY_VERSION_ID int                                                                                          
                                                                           
 select @TEMP_POLICY_ID = MAX(ISNULL(POLICY_ID,0))+1  from POL_CUSTOMER_POLICY_LIST                                                       
 where CUSTOMER_ID = @CUSTOMER_ID                                                                                                     
    
 if @TEMP_POLICY_ID IS NULL OR @TEMP_POLICY_ID = ''                                                                                              
 begin                                                                                              
  set @TEMP_POLICY_ID = 1                                                                         
 end                                                                                             
 set @TEMP_POLICY_VERSION_ID = 1                                  
     
 if @CALLED_FROM = 'ANYWAY'                                                               
  insert into POL_CUSTOMER_POLICY_LIST        
  (              
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,APP_ID,APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,APP_NUMBER,APP_VERSION,        
  APP_TERMS,APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,POLICY_LOB,POLICY_SUBLOB,CSR, UNDERWRITER,IS_UNDER_REVIEW,        
  AGENCY_ID,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,COUNTRY_ID,STATE_ID,DIV_ID,        
  DEPT_ID,PC_ID,BILL_TYPE,BILL_TYPE_ID,COMPLETE_APP,PROPRTY_INSP_CREDIT,INSTALL_PLAN_ID,CHARGE_OFF_PRMIUM,        
  RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED,POLICY_TYPE,SHOW_QUOTE,APP_VERifICATION_XML,YEAR_AT_CURR_RESI,YEARS_AT_PREV_ADD,        
  POLICY_STATUS,POLICY_NUMBER,POLICY_DISP_VERSION,POLICY_EFFECTIVE_DATE,PIC_OF_LOC,IS_HOME_EMP      
  ,POL_VER_EFFECTIVE_DATE,POL_VER_EXPIRATION_DATE,DOWN_PAY_MODE,DWELLING_ID,ADD_INT_ID,PRODUCER,    
  CURRENT_TERM,
  APPLY_INSURANCE_SCORE,CUSTOMER_REASON_CODE,CUSTOMER_REASON_CODE2,CUSTOMER_REASON_CODE3,CUSTOMER_REASON_CODE4                                                    
  )                                                       
  select                                            
  @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,@APP_ID,@APP_VERSION_ID,PARENT_APP_VERSION_ID,'Complete',APP_NUMBER,APP_VERSION,        
  APP_TERMS,APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,APP_LOB,APP_SUBLOB,CSR,UNDERWRITER,IS_UNDER_REVIEW,        
  APP_AGENCY_ID,IS_ACTIVE,@CREATED_BY,GETDATE(),null,null,COUNTRY_ID,STATE_ID,DIV_ID,        
  DEPT_ID,PC_ID,BILL_TYPE,BILL_TYPE_ID,COMPLETE_APP,PROPRTY_INSP_CREDIT,INSTALL_PLAN_ID,CHARGE_OFF_PRMIUM,        
  RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED,POLICY_TYPE,SHOW_QUOTE,APP_VERifICATION_XML,YEAR_AT_CURR_RESI,YEARS_AT_PREV_ADD,        
  'Suspended',SUBSTRING(APP_NUMBER,1,(LEN(APP_NUMBER)-3)),CONVERT(VARCHAR,@TEMP_POLICY_VERSION_ID) + '.0',APP_EFFECTIVE_DATE,PIC_OF_LOC,IS_HOME_EMP                                                                                                        
  ,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,DOWN_PAY_MODE,DWELLING_ID,ADD_INT_ID,PRODUCER,    
  1 -- In Case Of New Business Current Term will be 1 as it is the first term of policy 
  ,APPLY_INSURANCE_SCORE,CUSTOMER_REASON_CODE,CUSTOMER_REASON_CODE2,CUSTOMER_REASON_CODE3,CUSTOMER_REASON_CODE4
     
  from APP_LIST                                                 
  where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                         
 else        
  insert into POL_CUSTOMER_POLICY_LIST                                                                                     
  (                                                                                    
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,APP_ID,APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,APP_NUMBER,APP_VERSION,        
  APP_TERMS,APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,POLICY_LOB,POLICY_SUBLOB,CSR, UNDERWRITER,IS_UNDER_REVIEW,        
  AGENCY_ID,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODifIED_BY,LasT_UPDATED_DATETIME,COUNTRY_ID,STATE_ID,DIV_ID,        
  DEPT_ID,PC_ID,BILL_TYPE,BILL_TYPE_ID,COMPLETE_APP,PROPRTY_INSP_CREDIT,INSTALL_PLAN_ID,CHARGE_OFF_PRMIUM,        
  RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED,POLICY_TYPE,SHOW_QUOTE,APP_VERifICATION_XML,YEAR_AT_CURR_RESI,YEARS_AT_PREV_ADD,        
  POLICY_STATUS,POLICY_NUMBER,POLICY_DISP_VERSION,POLICY_EFFECTIVE_DATE,PIC_OF_LOC,IS_HOME_EMP                                                                                    
  ,POL_VER_EFFECTIVE_DATE,POL_VER_EXPIRATION_DATE,DOWN_PAY_MODE,CURRENT_TERM ,DWELLING_ID,ADD_INT_ID,PRODUCER,APPLY_INSURANCE_SCORE,
  CUSTOMER_REASON_CODE,CUSTOMER_REASON_CODE2,CUSTOMER_REASON_CODE3,CUSTOMER_REASON_CODE4
     
  )                                                       
  select  
  @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,@APP_ID,@APP_VERSION_ID,PARENT_APP_VERSION_ID,'Complete',APP_NUMBER,APP_VERSION,        
  APP_TERMS,APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,APP_LOB,APP_SUBLOB,CSR,UNDERWRITER,IS_UNDER_REVIEW,        
  APP_AGENCY_ID,IS_ACTIVE,@CREATED_BY,GETDATE(),null,null,COUNTRY_ID,STATE_ID,DIV_ID,        
  DEPT_ID,PC_ID,BILL_TYPE,BILL_TYPE_ID,COMPLETE_APP,PROPRTY_INSP_CREDIT,INSTALL_PLAN_ID,CHARGE_OFF_PRMIUM,        
  RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED,POLICY_TYPE,SHOW_QUOTE,APP_VERifICATION_XML,YEAR_AT_CURR_RESI,YEARS_AT_PREV_ADD,        
  'Suspended',SUBSTRING(APP_NUMBER,1,(LEN(APP_NUMBER)-3)),CONVERT(VARCHAR,@TEMP_POLICY_VERSION_ID) + '.0' ,APP_EFFECTIVE_DATE,PIC_OF_LOC,IS_HOME_EMP                                                                       
  ,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,DOWN_PAY_MODE,1,DWELLING_ID,ADD_INT_ID,PRODUCER,APPLY_INSURANCE_SCORE,
  CUSTOMER_REASON_CODE,CUSTOMER_REASON_CODE2,CUSTOMER_REASON_CODE3,CUSTOMER_REASON_CODE4
    
  from APP_LIST                                           
  where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                   
  select @TEMP_ERROR_CODE = @@ERROR                                                                          
   if (@TEMP_ERROR_CODE <> 0) goto PROBLEM           
        
      
  IF EXISTS ( SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST                                             
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @TEMP_POLICY_ID                                                          
  AND POLICY_VERSION_ID = @TEMP_POLICY_VERSION_ID                                            
  )                                                          
  BEGIN                                                                                
  -- 2. POL_APPLICANT_LIST                                                                 
   insert into POL_APPLICANT_LIST                                                                         
   (                                                                                                    
   POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID, APPLICANT_ID,CREATED_BY,CREATED_DATETIME,IS_PRIMARY_APPLICANT          
   )                                                                                    
   SELECT                     
   @TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,@CUSTOMER_ID,A.APPLICANT_ID,@CREATED_BY,GETDATE(),        
   A.IS_PRIMARY_APPLICANT                                                                                            
   FROM APP_APPLICANT_LIST A                                                                                    
   ,clt_APPLICANT_LIST  B,                                                                       
   APP_LIST C                                                                                         
   WHERE A.CUSTOMER_ID=@CUSTOMER_ID AND A.APP_ID=@APP_ID AND A.APP_VERSION_ID=@APP_VERSION_ID                                                                                                     
                                    
   and A.APPLICANT_ID = B.APPLICANT_ID and B.Is_Active='Y'             
   AND C.IS_ACTIVE = 'Y'             
   --BY PRAVESH         
   AND A.CUSTOMER_ID=C.CUSTOMER_ID        
   AND A.APP_ID =C.APP_ID        
   AND A.APP_VERSION_ID=  C.APP_VERSION_ID                                                                                   
       
       
   select @TEMP_ERROR_CODE = @@ERROR                  
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                                      
  END                       
 
 -- Added By Ravindra To Copy EFT Details 

 IF EXISTS ( SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @TEMP_POLICY_ID                                                          
  AND POLICY_VERSION_ID = @TEMP_POLICY_VERSION_ID                                            
  )                                                          
  BEGIN                                                                                
	  -- 2. ACT_POL_EFT_CUST_INFO   
	   	                                                              
	   insert into ACT_POL_EFT_CUST_INFO                                                                         
	   (           
	   POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID, 
	   FEDERAL_ID,DFI_ACC_NO,TRANSIT_ROUTING_NO,
	   CREATED_BY,CREATED_DATETIME,MODIFIED_BY, LAST_UPDATED_DATETIME,
	   IS_VERIFIED,VERIFIED_DATE,REASON,ACCOUNT_TYPE,EFT_TENTATIVE_DATE,REVERIFIED_AC
	   )                                                                                    
	   SELECT                     
	   @TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,@CUSTOMER_ID,
	   FEDERAL_ID,DFI_ACC_NO,TRANSIT_ROUTING_NO,
	   @CREATED_BY,GETDATE(),null,null,
	   IS_VERIFIED,VERIFIED_DATE,REASON,ACCOUNT_TYPE,EFT_TENTATIVE_DATE,
	   CASE WHEN ISNULL(DFI_ACC_NO,'') <> '' AND ISNULL(TRANSIT_ROUTING_NO,'') <> '' THEN 10963 ELSE 10964 END 
	   FROM ACT_APP_EFT_CUST_INFO A                                                                                    
	   WHERE A.CUSTOMER_ID=@CUSTOMER_ID 
		AND A.APP_ID=@APP_ID 
		AND A.APP_VERSION_ID=@APP_VERSION_ID                                                                                                     
	                                    
	   SELECT @TEMP_ERROR_CODE = @@ERROR                  
	   	IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                      
  END
	
 -- Added By Swastika To Copy Credit Card Details 

 IF EXISTS ( SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST                                             
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @TEMP_POLICY_ID                                                          
  AND POLICY_VERSION_ID = @TEMP_POLICY_VERSION_ID                                            
  )                                                          
  BEGIN                                                                                
	  -- ACT_POL_CREDIT_CARD_DETAILS                                                                 
	   insert into ACT_POL_CREDIT_CARD_DETAILS                                                                         
	   (
		POLICY_ID,
	  	POLICY_VERSION_ID,
		CUSTOMER_ID,
		PAY_PAL_REF_ID,
		LAST_UPDATED_DATETIME,
		CREATED_DATETIME
		
	   )                                                                                    
	   SELECT                     
	  	@TEMP_POLICY_ID,
		@TEMP_POLICY_VERSION_ID,
		@CUSTOMER_ID,
		PAY_PAL_REF_ID,
		LAST_UPDATED_DATETIME,
		CREATED_DATETIME
	  
	   FROM ACT_APP_CREDIT_CARD_DETAILS A                                                                                    
	   WHERE A.CUSTOMER_ID=@CUSTOMER_ID 
		AND A.APP_ID=@APP_ID 
		AND A.APP_VERSION_ID=@APP_VERSION_ID                                                                                                     
	                                    
	   SELECT @TEMP_ERROR_CODE = @@ERROR        
	   	IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                      
  END
       
  -- diary                  
      
  /*
	--Diary entry will be done at page level itself to provide transaction log information of diary addition
 declare @LISTTYPEID int--New Business =7                                                                                
  declare @UNDERWRITER int -- 0                                                                                
  declare @SubjectLine nvarchar(100)      
  declare @appStatus nvarchar(100)     
  -----added by pravesh on 29 nov 2006      
  select @SubjectLine='New Application Submitted'     
  if @CALLED_FROM = 'ANYWAY'          
   set      @SubjectLine=@SubjectLine+ ' Type - Submit Anyways'       
  else       
  begin      
      
   DECLARE @IDOC INT         
   DECLARE @RULE_XML varchar(8000)        
   SELECT @RULE_XML = isnull(APP_VERIFICATION_XML,'') FROM APP_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID            
   if (@RULE_XML!='')      
   begin      
    SELECT @RULE_XML=REPLACE(@RULE_XML,'stylesheet">','stylesheet"></LINK>')      
    SELECT @RULE_XML=REPLACE(@RULE_XML,'charset=utf-16">','charset=utf-16"></META>')      
    SELECT @RULE_XML='<ROOT>' + upper(@RULE_XML) + '</ROOT>'      
    SELECT @RULE_XML=replace(@RULE_XML,'XMLNS','xmlns')      
        
    EXEC SP_XML_PREPAREDOCUMENT @IDOC OUTPUT, @RULE_XML          
      DECLARE @RULE_DESC VARCHAR(200)          
           
      SELECT   @RULE_DESC = REFEREDSTATUS        
      FROM    OPENXML (@IDOC, '/ROOT/SPAN',2)    --2 for Node value      
               WITH( REFEREDSTATUS  VARCHAR(5) )        
    if (@RULE_DESC  ='0')      
    set @SubjectLine=isnull(@SubjectLine,'') + ' Type - Refer to Underwriter'      
    else      
    set @SubjectLine= isnull(@SubjectLine,'') + ' Type - Meets Requirements'      
    
   end        
  end      
-----end here      
      
 select @UNDERWRITER=isnull(UNDERWRITER,0)                                                                                
 from Pol_customer_policy_list                                                                                
 where Customer_ID=@CUSTOMER_ID and  POLICY_ID=@TEMP_POLICY_ID and   POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID                                                                                                            
 INSERT into TODOLIST                                                    
 (                        
 RECBYSYSTEM,    RECDATE,    FOLLOWUPDATE,    LISTTYPEID,    POLICYCLIENTID,    POLICYID,    POLICYVERSION,                                                             
 POLICYCARRIERID,    POLICYBROKERID,    SUBJECTLINE,    LISTOPEN,    SYSTEMFOLLOWUPID,    PRIORITY,                                                                       
 TOUSERID,    FROMUSERID,    STARTTIME,    ENDTIME,    NOTE,    PROPOSALVERSION, QUOTEID,    CLAIMID,                                                                                  
 CLAIMMOVEMENTID,    TOENTITYID,    FROMENTITYID,    CUSTOMER_ID,    APP_ID,    APP_VERSION_ID,    POLICY_ID,                                                                                  
 POLICY_VERSION_ID                                             
 )                                               
 values                                                                                  
 (                  
 null,    getdate(),    getdate(),    7,    @CUSTOMER_ID,    @TEMP_POLICY_ID,                                                                                  
 @TEMP_POLICY_VERSION_ID,    null,    null,@SubjectLine,    'Y',                                                                                  
 null,    'M',   @UNDERWRITER,    @CREATED_BY,    null,   null,    null,                                                         
 null,    null,    null,    null,    null,    null,                                               
 @CUSTOMER_ID,    @APP_ID,    @APP_VERSION_ID,    @TEMP_POLICY_ID,    @TEMP_POLICY_VERSION_ID                                                
 )   */                                                 
                                                                                
 select @TEMP_ERROR_CODE = @@ERROR                                                                                          
 if (@TEMP_ERROR_CODE <> 0) goto PROBLEM           
     
 --commit tran         
 set @RESULT = @TEMP_POLICY_ID                                                          
 return @RESULT                                                             
END                                                                          
                                                                   
PROBLEM:                                              
                                            
if (@TEMP_ERROR_CODE <> 0)             
begin                                    
 --   rollback tran                  
 set @RESULT = -1                                                                                                        
 return @RESULT                                 
end                                                                          
else                                                  
begin                                               
 set @RESULT = @TEMP_POLICY_ID                                                             
 return @RESULT                                                     
end                                  
        
        
end        
























GO

