IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_CLAIM_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_CLAIM_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


    
--begin tran      
--DROP PROC dbo.Proc_InsertCLM_CLAIM_INFO      
--go      
/*----------------------------------------------------------                                             
Proc Name       : dbo.Proc_InsertCLM_CLAIM_INFO                                            
Created by      : Sumit Chhabra                                            
Date            : 27/04/2006                                            
Purpose         : Insert data in CLM_CLAIM_INFO table for claim notification screen         
Created by      : Sumit Chhabra                                            
Revison History :                                            
Used In        : Wolverine                                            
------------------------------------------------------------                                            
Modified By  : Asfa Praveen                  
Date   : 29/Aug/2007                  
Purpose  : To add Adjuster_ID column                  
------------------------------------------------------------                                                                                  
Date     Review By          Comments                                            
------   ------------       -------------------------*/                                            
-- DROP PROC dbo.Proc_InsertCLM_CLAIM_INFO                                            
CREATE PROC [dbo].[Proc_InsertCLM_CLAIM_INFO]                                            
 @CUSTOMER_ID int,                                            
 @POLICY_ID int,                                            
 @POLICY_VERSION_ID smallint,                                            
 @CLAIM_ID int output,                                            
 @LOSS_DATE datetime,                                            
 @ADJUSTER_CODE varchar(10),                  
 @ADJUSTER_ID int,                        
 @REPORTED_BY varchar(50),                                            
 @CATASTROPHE_EVENT_CODE int,                                            
 --@CLAIMANT_INSURED bit,                                            
 @INSURED_RELATIONSHIP varchar(50),                                            
 @CLAIMANT_NAME varchar(50),                                            
 @COUNTRY int,                                            
 @ZIP varchar(11),                                            
 @ADDRESS1 varchar(50),                                            
 @ADDRESS2 varchar(50),                                            
 @CITY varchar(50),                                            
 @HOME_PHONE varchar(15),                                            
 @WORK_PHONE varchar(15),                                            
 @MOBILE_PHONE varchar(15),                                            
 @WHERE_CONTACT varchar(50),                                            
 @WHEN_CONTACT varchar(10),                                            
 @DIARY_DATE datetime,                                            
 @CLAIM_STATUS int,                                            
 @OUTSTANDING_RESERVE decimal(20,2),                                            
 @RESINSURANCE_RESERVE decimal(20,2),                                            
 @PAID_LOSS decimal(20,2),                                            
 @PAID_EXPENSE decimal(20,2),                                            
 @RECOVERIES decimal(20,2),                                            
 @CLAIM_DESCRIPTION varchar(1000),                                            
 @CREATED_BY int,                                            
 @CREATED_DATETIME datetime,                                            
 --@SUB_ADJUSTER varchar(50),                                            
 --@SUB_ADJUSTER_CONTACT varchar(50),                                 
 @EXTENSION varchar(5),                                            
 @LOSS_TIME_AM_PM smallint,                                            
 @LITIGATION_FILE INT,                                            
 @HOMEOWNER char(1),                                            
 @RECR_VEH char(1),                                            
 @IN_MARINE char(1),                                            
 @STATE int,                                            
 @CLAIMANT_PARTY int, 
 @CLAIMANT_TYPE  int,                                          
 --@LINKED_TO_CLAIM varchar(500),                                            
 --@ADD_FAULT char(1),                                            
 --@TOTAL_LOSS char(1),                                            
 @NOTIFY_REINSURER INT,              
 @LOB_ID nvarchar(10),                                      
 @REPORTED_TO varchar(50),                                          
 @FIRST_NOTICE_OF_LOSS datetime,     
 @LAST_DOC_RECEIVE_DATE datetime,                  
 @LINKED_CLAIM_ID_LIST varchar(500),                            
 @RECIEVE_PINK_SLIP_USERS_LIST varchar(200),                          
 @NEW_RECIEVE_PINK_SLIP_USERS_LIST varchar(200),                        
 @PINK_SLIP_TYPE_LIST varchar(200),          
 @CLAIM_STATUS_UNDER int=null,    
 @AT_FAULT_INDICATOR int , --Done for Itrack Issue 6620 on 27 Nov 09   
 @REINSURANCE_TYPE int ,  
 @REIN_CLAIM_NUMBER nvarchar(500), 
 @REIN_LOSS_NOTICE_NUM nvarchar(500),
 @IS_VICTIM_CLAIM INT,
 @POSSIBLE_PAYMENT_DATE datetime                 
 
AS                              
BEGIN                       
                                          
--if not exists(SELECT CUSTOMER_ID FROM CLM_CLAIM_INFO WHERE CLAIM_NUMBER LIKE '%'+ RTRIM(LTRIM(@LINKED_TO_CLAIM)) +'%')                                        
-- RETURN -2                                          
                                          
 SELECT @CLAIM_ID = ISNULL(MAX(CLAIM_ID),0)+1 FROM CLM_CLAIM_INFO WITH(NOLOCK)                                           
                                            
                                            
/*Generate the new Claim Number of the form XX-YY-ZZZZ                                            
 XX is the adjuster code                                            
 YY is the year                                   
 ZZZZ is the ascending claim number for a particular adjuster                                            
*/                                            
                                            
                                            
declare @ADJUSTER_CLAIM_NUMBER int                                            
declare @LENTH_ADJUSTER_CLAIM_NUMBER int --lenght of the claim adjuster claim number                                            
declare @MAXIMUM_ADJUSTER_CHARACTERS int --ZZZZ maximum characters to be inserted by us                                            
declare @YEAR_PART varchar(2)---variable to store year part ie yy of yyyy                                            
declare @CLAIM_NUMBER varchar(10)--final claim number that will be generated                                            
declare @COMPLETE int                                            
declare @SUB_ADJUSTER_PARTY_ID int                                            
                                            
declare @DETAIL_TYPE_CLAIMANT int                                            
declare @DETAIL_TYPE_ADDITIONAL_INSURED int                                            
declare @DETAIL_TYPE_INSURED int                                            
declare @DETAIL_TYPE_ADJUSTER int           
declare @DETAIL_TYPE_AGENCY INT                                           
declare @PARTY_ID int                                         
declare @TEMP_ADJUSTER_CODE varchar(2)                                            
          
DECLARE @CO_APPLICANT_NAME VARCHAR(60)          
DECLARE @AI_ADDRESS1  VARCHAR(150)          
DECLARE @AI_ADDRESS2  VARCHAR(150)          
DECLARE @AI_CITY   VARCHAR(70)          
DECLARE @AI_STATE   INT          
DECLARE @AI_ZIP_CODE  VARCHAR(20)          
DECLARE @AI_PHONE VARCHAR(15)          
DECLARE @AI_EMAIL VARCHAR(50)          
DECLARE @AI_COUNTRY   INT          
--Done for Itrack Issue 6258 on 17 Aug 2009         
DECLARE @YEAR INT          
                                          
-- MODIFIED BY SANTOSH KR. GAUTAM ON 07 JUL 2011 (REF ITRACK:1348)                                           
SET @DETAIL_TYPE_ADDITIONAL_INSURED = 114 --239          
SET @DETAIL_TYPE_AGENCY = 208          
SET @DETAIL_TYPE_CLAIMANT = 9                                            
SET @DETAIL_TYPE_INSURED = 10  -- FOR INSURED                                          
SET @DETAIL_TYPE_ADJUSTER = 12                                  
set @SUB_ADJUSTER_PARTY_ID = 112     

--Added by Santosh Kumar Gautam on 13 Dec 2010
--DECLARE @ADJUSTER_BRANCH NVARCHAR(2)     
    
                                            
set @COMPLETE = 11801            
          
-- Added by Asfa (08-Jan-2008) - iTrack issue #3334          
-- Block begins by Asfa           
        
--Done for Itrack Issue 6258 on 17 Aug 2009      
        

--SELECT @YEAR=  YEAR(MAX(CREATED_DATETIME)) FROM CLM_CLAIM_INFO       
--WHERE ADJUSTER_ID = @ADJUSTER_ID     
    
--IF(@YEAR = YEAR(GETDATE()))      
--BEGIN      
-- UPDATE CLM_ADJUSTER SET CLAIM_COUNTER = @ADJUSTER_CLAIM_NUMBER       
-- WHERE ADJUSTER_ID = @ADJUSTER_ID      
--END      
--ELSE      
--BEGIN     

-- UPDATE CLM_ADJUSTER SET CLAIM_COUNTER = 1      
-- WHERE ADJUSTER_ID = @ADJUSTER_ID     
      
-- SELECT @ADJUSTER_CLAIM_NUMBER = isnull(CLAIM_COUNTER,0) FROM CLM_ADJUSTER with(nolock)      
-- WHERE ADJUSTER_ID = @ADJUSTER_ID       
      
--END       

SET @TEMP_ADJUSTER_CODE=@ADJUSTER_CODE       
       
SELECT @ADJUSTER_CLAIM_NUMBER = ISNULL(CLAIM_COUNTER,0) + 1 FROM CLM_ADJUSTER with(nolock)      
WHERE ADJUSTER_ID = @ADJUSTER_ID     
        
UPDATE CLM_ADJUSTER      
SET CLAIM_COUNTER=@ADJUSTER_CLAIM_NUMBER
WHERE ADJUSTER_ID = @ADJUSTER_ID     
 
        
--SELECT @ADJUSTER_BRANCH=D.BRANCH_CODE 
--FROM   CLM_ADJUSTER C INNER JOIN
--	   MNT_USER_LIST M ON  C.[USER_ID] =M.[USER_ID] INNER JOIN
--       MNT_DIV_LIST D ON D.DIV_ID= M.USER_DEF_DIV_ID
--WHERE (ADJUSTER_ID = @ADJUSTER_ID )




IF (LEN(@TEMP_ADJUSTER_CODE)>2)
    SET @TEMP_ADJUSTER_CODE =SUBSTRING(@TEMP_ADJUSTER_CODE,0,2) 
ELSE
BEGIN
   SET @TEMP_ADJUSTER_CODE=ISNULL(@TEMP_ADJUSTER_CODE,'00')
END

SET @TEMP_ADJUSTER_CODE =REPLICATE('0',2-LEN(@TEMP_ADJUSTER_CODE))+@TEMP_ADJUSTER_CODE


IF (LEN(@ADJUSTER_CLAIM_NUMBER)>6)
    SET @ADJUSTER_CLAIM_NUMBER =SUBSTRING(CAST(@ADJUSTER_CLAIM_NUMBER AS NVARCHAR(6)),0,6) 
ELSE
BEGIN
   SET @ADJUSTER_CLAIM_NUMBER=ISNULL(@ADJUSTER_CLAIM_NUMBER,'0')
   
END

SET @CLAIM_NUMBER =CAST( REPLICATE('0',6-LEN(@ADJUSTER_CLAIM_NUMBER)) AS NVARCHAR(6))+CAST(@ADJUSTER_CLAIM_NUMBER AS NVARCHAR(6))


-------------------------------------        
/*SELECT @ADJUSTER_CLAIM_NUMBER = COUNT(CLAIM_ID)+1 FROM CLM_CLAIM_INFO with (nolock)          
WHERE ADJUSTER_ID = @ADJUSTER_ID AND year(CREATED_DATETIME)= year(getdate())  --get adjuster claim number */        
        
-- Block ends by Asfa           
          
---- SELECT @ADJUSTER_CLAIM_NUMBER = COUNT(CLAIM_ID)+1 FROM CLM_CLAIM_INFO WHERE ADJUSTER_ID = @ADJUSTER_ID--get adjuster claim number   --ADJUSTER_CODE=@ADJUSTER_CODE  // Commented by Asfa 29-Aug-2007                  
--set @MAXIMUM_ADJUSTER_CHARACTERS = 4 --ZZZZ                                            
--set @LENTH_ADJUSTER_CLAIM_NUMBER = len(@ADJUSTER_CLAIM_NUMBER)                                            
--set @YEAR_PART = substring(cast(year(getdate())  as varchar(4)),3,2)                                            
              
--IF (LEN(@ADJUSTER_CODE) = 1)                                            
--    SET @TEMP_ADJUSTER_CODE = '0' + cast (@ADJUSTER_CODE as varchar)                                            
--ELSE                                            
--   SET @TEMP_ADJUSTER_CODE = cast (@ADJUSTER_CODE as varchar)                                            
                                            
--SET @CLAIM_NUMBER = substring(cast(@TEMP_ADJUSTER_CODE as varchar),1,2) + '-' + @YEAR_PART + '-'+ replicate('0',@MAXIMUM_ADJUSTER_CHARACTERS-@LENTH_ADJUSTER_CLAIM_NUMBER) + cast(@ADJUSTER_CLAIM_NUMBER as varchar)                                        
   
    
--- Added by Santosh Kumar Gautam 15 Dec 2010
--- COPY POLICY CO_INSURANCE TYPE TO REMOVE THE DEPEDENCY FROM POLICY TABLES

DECLARE  @CO_INSURANCE_TYPE INT=0
DECLARE  @CLAIM_CURRENCY_ID INT=0
DECLARE  @POLICY_AGENCY_ID INT=0

SELECT @CO_INSURANCE_TYPE=CO_INSURANCE ,@CLAIM_CURRENCY_ID=POLICY_CURRENCY,@POLICY_AGENCY_ID=AGENCY_ID
FROM POL_CUSTOMER_POLICY_LIST
WHERE (CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  )
     
     
                                            
                                            
--Format changed from 00-00-0000 to 0000-0000             
--SET @CLAIM_NUMBER = substring(cast(@TEMP_ADJUSTER_CODE as varchar),1,2) + @YEAR_PART + '-'+ replicate('0',@MAXIMUM_ADJUSTER_CHARACTERS-@LENTH_ADJUSTER_CLAIM_NUMBER) + cast(@ADJUSTER_CLAIM_NUMBER as varchar)                                            
SET @CLAIM_NUMBER =  @TEMP_ADJUSTER_CODE+@CLAIM_NUMBER


             
 INSERT INTO CLM_CLAIM_INFO                                            
 (                                            
  CUSTOMER_ID,                                            
  POLICY_ID,                                            
  POLICY_VERSION_ID,                                            
  CLAIM_ID,                                            
  CLAIM_NUMBER,                                            
  LOSS_DATE,                                            
  ADJUSTER_CODE,                   
  ADJUSTER_ID,                                           
  REPORTED_BY,                                            
  CATASTROPHE_EVENT_CODE,       
--  CLAIMANT_INSURED,                                            
  INSURED_RELATIONSHIP,                                            
  CLAIMANT_NAME,                                            
  COUNTRY,                                            
  ZIP,                                            
  ADDRESS1,                                            
  ADDRESS2,     
  CITY,                     
  HOME_PHONE,                                            
  WORK_PHONE,                                            
  MOBILE_PHONE,                                            
  WHERE_CONTACT,                                            
  WHEN_CONTACT,                                
  DIARY_DATE,                                            
  CLAIM_STATUS,                                            
  OUTSTANDING_RESERVE,                                            
  RESINSURANCE_RESERVE,                                            
  PAID_LOSS,                                            
  PAID_EXPENSE,                                            
  RECOVERIES,                            
  CLAIM_DESCRIPTION,                                            
  CREATED_BY,                                            
  CREATED_DATETIME,                                            
  IS_ACTIVE,                                            
-- SUB_ADJUSTER,                                            
-- SUB_ADJUSTER_CONTACT,                                            
 EXTENSION,                                 
 LOSS_TIME_AM_PM,                         
 LITIGATION_FILE,                                            
 HOMEOWNER,                                            
 RECR_VEH,                                            
 IN_MARINE,                                            
 STATE,                                            
CLAIMANT_PARTY,                                            
-- LINKED_TO_CLAIM,                         
-- ADD_FAULT,                          
-- TOTAL_LOSS,                                            
 NOTIFY_REINSURER,                                            
 LOB_ID,                                          
 REPORTED_TO,                                          
 FIRST_NOTICE_OF_LOSS,                            
 RECIEVE_PINK_SLIP_USERS_LIST,                        
 PINK_SLIP_TYPE_LIST,          
 CLAIM_STATUS_UNDER,    
 AT_FAULT_INDICATOR, --Done for Itrack Issue 6620 on 27 Nov 09                       
 CO_INSURANCE_TYPE,-- Added by santosh kumar gautam on 15 dec 2010
 CLAIM_CURRENCY_ID, -- Added by santosh kumar gautam on 15 dec 2010,
 LAST_DOC_RECEIVE_DATE, -- Added by santosh kumar gautam on 17 Jan 2011
 REINSURANCE_TYPE,
 REIN_CLAIM_NUMBER,
 REIN_LOSS_NOTICE_NUM,
 IS_VICTIM_CLAIM,
 POSSIBLE_PAYMENT_DATE
 

 )                                            
 VALUES                                            
 (                                            
  @CUSTOMER_ID,                                         
  @POLICY_ID,                                            
  @POLICY_VERSION_ID,                                            
  @CLAIM_ID,                                            
  @CLAIM_NUMBER,                  
  @LOSS_DATE,                                
  @ADJUSTER_CODE,                                            
  @ADJUSTER_ID,                  
  @REPORTED_BY,                                            
  @CATASTROPHE_EVENT_CODE,                                            
--  @CLAIMANT_INSURED,                                            
  @INSURED_RELATIONSHIP,            
  @CLAIMANT_NAME,                                          
 @COUNTRY,            
 @ZIP,                                            
  @ADDRESS1,                                            
  @ADDRESS2,                                            
  @CITY,                                            
  @HOME_PHONE,                
  @WORK_PHONE,                                            
  @MOBILE_PHONE,                    
  @WHERE_CONTACT,                                            
  @WHEN_CONTACT,                                            
  @DIARY_DATE,                                            
  @CLAIM_STATUS,                                            
  @OUTSTANDING_RESERVE,                                            
  @RESINSURANCE_RESERVE,                                            
  @PAID_LOSS,             
  @PAID_EXPENSE,                                            
  @RECOVERIES,                                            
  @CLAIM_DESCRIPTION,                                            
  @CREATED_BY,                                            
  @CREATED_DATETIME,                                            
  'Y',                                            
-- @SUB_ADJUSTER,                                            
-- @SUB_ADJUSTER_CONTACT,            
 @EXTENSION,                              
 @LOSS_TIME_AM_PM,                                            
 @LITIGATION_FILE,                                            
 @HOMEOWNER,                                            
@RECR_VEH,                                            
 @IN_MARINE,                                       
 CASE WHEN @STATE IS NULL THEN 0 ELSE @STATE END ,                       
 @CLAIMANT_PARTY,                                            
-- @LINKED_TO_CLAIM,                                            
-- @ADD_FAULT,                                            
-- @TOTAL_LOSS,                                            
 @NOTIFY_REINSURER,                                            
 @LOB_ID,                                          
 @REPORTED_TO,                                          
 @FIRST_NOTICE_OF_LOSS,                            
 @RECIEVE_PINK_SLIP_USERS_LIST,                        
 @PINK_SLIP_TYPE_LIST,          
 @CLAIM_STATUS_UNDER,    
 @AT_FAULT_INDICATOR, --Done for Itrack Issue 6620 on 27 Nov 09   
 @CO_INSURANCE_TYPE,-- Added by santosh kumar gautam on 15 dec 2010
 @CLAIM_CURRENCY_ID, -- Added by santosh kumar gautam on 15 dec 2010
 @LAST_DOC_RECEIVE_DATE, -- Added by santosh kumar gautam on 17 Jan 2011
 @REINSURANCE_TYPE,
 @REIN_CLAIM_NUMBER,
 @REIN_LOSS_NOTICE_NUM,
 @IS_VICTIM_CLAIM,
 @POSSIBLE_PAYMENT_DATE
 
                      
 )                                            
                                            
/*Now the diary entry will go from the business layer itself                        
SEND THE DIARY ENTRY TO ADJUSTER OF THE CLAIM                          
IF (@ADJUSTER_CODE IS NOT NULL)                                            
BEGIN                                            
 DECLARE @T_ADJUSTER_CODE INT                                         
 SELECT @T_ADJUSTER_CODE=ADJUSTER_CODE FROM CLM_ADJUSTER WHERE ADJUSTER_ID = @ADJUSTER_CODE                                            
                                            
--Add diary entry                                            
 INSERT into TODOLIST                                            
 (                                            
  RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,POLICYCLIENTID,POLICYID,POLICYVERSION,                                           
  POLICYCARRIERID,POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,                                            
  FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,                                            
  FROMENTITYID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID                            
 )                                            
values                                            
 (                                            
  null,@DIARY_DATE,@DIARY_DATE,11,@CUSTOMER_ID,@POLICY_ID,                                            
 @POLICY_VERSION_ID,null,null,'New Claim Added','Y',                                            
  null,'M',@T_ADJUSTER_CODE,@CREATED_BY,null,null,null,null,null,@CLAIM_ID,null,null,null,                                            
  @CUSTOMER_ID,null,null,@POLICY_ID,@POLICY_VERSION_ID                                            
 )                                
END                           
*/              
             
                             
/*If both the Add Fault and Total Loss is selected "Yes" then a Diary Entry should go to the                                            
Policy Underwriter and the message should be "There has been a total loss for this policy" + Policy Number*/                
--Since both these fields have been removed from the page, following code too needs to be commented                           
/*IF (@ADD_FAULT='Y' AND @TOTAL_LOSS='Y')                                            
BEGIN                          
            
DECLARE @TEMP_POLICY_NUMBER char(10)                                            
DECLARE @TEMP_UNDERWRITER int                                            
                                            
SELECT @TEMP_POLICY_NUMBER = POLICY_NUMBER, @TEMP_UNDERWRITER = UNDERWRITER                                            
FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                            
                                            
 INSERT into TODOLIST                                         
 (                                            
  RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,POLICYCLIENTID,POLICYID,POLICYVERSION,                                            
  POLICYCARRIERID,POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,                                            
  FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,                                            
  FROMENTITYID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID                                            
 )                                            
values                                            
 (                                            
  null,@DIARY_DATE,@DIARY_DATE,11,@CUSTOMER_ID,@POLICY_ID,                                            
  @POLICY_VERSION_ID,null,null,'There has been a total loss for policy number : ' + @TEMP_POLICY_NUMBER,'Y',                                            
  null,'M',@TEMP_UNDERWRITER,@CREATED_BY,null,null,null,null,null,null,null,null,null,                                            
  @CUSTOMER_ID,null,null,@POLICY_ID,@POLICY_VERSION_ID                                            
 )                                  
END                                   
*/                                     
                                            
                                            
-- Add data in the CLM_PARTIES TABLE FOR THE CLAIMANT                                            
--declare @party_id int                                            
--exec Proc_InsertCLM_PARTIES  @party_id out,@CLAIM_ID,@CLAIMANT_NAME,@ADDRESS1,@ADDRESS2,@CITY,0,@ZIP,@HOME_PHONE,'','',@CREATED_BY,@CREATED_DATETIME,@DETAIL_TYPE_CLAIMANT,@COUNTRY,'',NULL,'','','','',''                                            
                                            
                                            
--Add data in the CLM_PARTIES TABLE FOR THE CLAIMANT                                            
--exec Proc_InsertCLM_PARTIES  @party_id out,@CLAIM_ID,@CLAIMANT_NAME,@ADDRESS1,@ADDRESS2,@CITY,0,@ZIP,@HOME_PHONE,'','',@CREATED_BY,@CREATED_DATETIME,10,@COUNTRY,'',NULL,'',''                                            
--Commented as the field is removed                            
/*if @CLAIMANT_INSURED=1                                            
begin*/                                            
--SELECT                                            
--  @PARTY_ID = ISNULL(MAX(PARTY_ID),0)+1                                            
-- FROM                                            
--  CLM_PARTIES  WITH(NOLOCK)                                           
-- WHERE CLAIM_ID=@CLAIM_ID                                            
                                         
-- INSERT INTO CLM_PARTIES                                            
-- (                                            
--  PARTY_ID,                                            
--  CLAIM_ID,                                        
--  NAME,                                            
--  ADDRESS1,                                            
--  ADDRESS2,                                            
--  CITY,                  
--  STATE,                                            
--  ZIP,                                        
--  CONTACT_PHONE,                                
--  CONTACT_EMAIL,                                   
--  OTHER_DETAILS,      
--  CREATED_BY,                                            
--  CREATED_DATETIME,                                            
--  PARTY_TYPE_ID,              
--  COUNTRY,                         
--  IS_ACTIVE    ,
--  PARTY_TYPE,
--  NUMBER         
-- )                          
--values(             
--  @PARTY_ID,                                            
--  @CLAIM_ID,            
--  @CLAIMANT_NAME,                              
--  @ADDRESS1,                                            
--  @ADDRESS2,                                       
--  @CITY,                                            
--  @STATE,                                            
--  @ZIP,                                            
--  @HOME_PHONE,                                            
--  '',                                            
--  '',                          
--  @CREATED_BY,                                            
--  @CREATED_DATETIME,                                            
--  @DETAIL_TYPE_INSURED,                                   
--  @COUNTRY,                                            
--  'Y' ,
--  @CLAIMANT_TYPE  -- FOR COMMERCIAL, PERSONAL,GOVERMENT
--   ,
--(CASE WHEN len(@ADDRESS1)-len(replace(@ADDRESS1,',','')) > 0 THEN 
--		CASE WHEN ISNUMERIC(DBO.Piece(@ADDRESS1,',',len(@ADDRESS1)-len(replace(@ADDRESS1,',',''))+1)) =1 
--		THEN
--		DBO.Piece(@ADDRESS1,',',len(@ADDRESS1)-len(replace(@ADDRESS1,',',''))+1)
--		END
--	 END )                                        
-- )         
--END                                     
----------------------------------------
--- ADDED BY SANTOSH KUMAR GAUTAM ON 21 JAN 2011 (Actual code commented above)
--  FOR COPY INSURED PERSON (CLAIMANT)
----------------------------------------
SELECT                                            
  @PARTY_ID = ISNULL(MAX(PARTY_ID),0)+1                                            
 FROM                                            
  CLM_PARTIES  WITH(NOLOCK)                                           
 WHERE CLAIM_ID=@CLAIM_ID                                            
                              
INSERT INTO CLM_PARTIES                                            
 (                                            
  PARTY_ID,                                            
  CLAIM_ID,
  PARTY_TYPE_ID,                                        
  NAME,                                            
  ADDRESS1,                                            
  ADDRESS2,                                            
  CITY,                  
  STATE,                                            
  ZIP,  
  COUNTRY,                                         
  CONTACT_PHONE,                                
  CONTACT_EMAIL,                                   
  CONTACT_PHONE_EXT,                                                
  PARTY_TYPE,           
  MARITAL_STATUS,
  DATE_OF_BIRTH,
  NUMBER   ,
  DISTRICT,
  REGIONAL_ID,
  REGIONAL_ID_ISSUANCE,
  REGIONAL_ID_ISSUE_DATE,
  CONTACT_FAX,
  PARTY_CPF_CNPJ,
  BANK_NAME,
  BANK_NUMBER,
  BANK_BRANCH,
  ACCOUNT_NUMBER,
  ACCOUNT_TYPE,  
  CREATED_BY,                                            
  CREATED_DATETIME,  
  IS_ACTIVE     
 )                          
(   
  SELECT  TOP 1        
  @PARTY_ID,                                            
  @CLAIM_ID,   
  @DETAIL_TYPE_INSURED,
   ISNULL(CAL.FIRST_NAME,'') + ' '+ CASE ISNULL(CAL.MIDDLE_NAME,'') WHEN '' THEN '' ELSE ISNULL(CAL.MIDDLE_NAME,'') END + ' ' + ISNULL(CAL.LAST_NAME,'') ,       
  CAL.ADDRESS1 ,     
  CAL.ADDRESS2 ,    
  CAL.CITY ,
  CASE WHEN [STATE] IS NULL THEN 0 ELSE [STATE] END ,
  CAL.ZIP_CODE ,
  CAL.COUNTRY  ,     
  CAL.PHONE , 
  CAL.EMAIL,   
  CAL.EXT, 
  CAL.APPLICANT_TYPE,  -- FOR COMMERCIAL, PERSONAL,GOVERMENT
  CASE CAL.CO_APPL_MARITAL_STATUS  --  CAL.CO_APPL_MARITAL_STATUS,
       WHEN 'D' THEN 5932 
       WHEN 'M' THEN 5933 
       WHEN 'P' THEN 5934 
       WHEN 'S' THEN 5935
       WHEN 'W' THEN 5936
       ELSE NULL
  END,       
  CAL.CO_APPL_DOB,
  CAL.NUMBER,
  CAL.DISTRICT,
  CAL.REGIONAL_IDENTIFICATION,
  CAL.ORIGINAL_ISSUE,
  CAL.REG_ID_ISSUE,
  CAL.FAX,
  CAL.CPF_CNPJ,
  CAL.BANK_NAME,
  CAL.BANK_NUMBER,
  CAL.BANK_BRANCH,
  CAL.ACCOUNT_NUMBER,
  CAL.ACCOUNT_TYPE,
  @CREATED_BY,
  @CREATED_DATETIME,
  'Y'
  FROM CLT_APPLICANT_LIST CAL INNER JOIN POL_APPLICANT_LIST PAL          
  ON CAL.CUSTOMER_ID = PAL.CUSTOMER_ID AND CAL.APPLICANT_ID= PAL.APPLICANT_ID          
  WHERE PAL.CUSTOMER_ID = @CUSTOMER_ID AND PAL.POLICY_ID=@POLICY_ID           
  AND PAL.POLICY_VERSION_ID=@POLICY_VERSION_ID AND PAL.IS_PRIMARY_APPLICANT = 1  
  
  )                                     
                                            
----------------------------------------
--  FOR COPY ADJUSTER DATA 
----------------------------------------                                  
                                          
SELECT                                            
  @PARTY_ID = ISNULL(MAX(PARTY_ID),0)+1                                            
 FROM                           
  CLM_PARTIES WITH(NOLOCK)                               
 WHERE CLAIM_ID=@CLAIM_ID                               
                                 
 INSERT INTO CLM_PARTIES                                            
 (                              
  PARTY_ID,                                     
  CLAIM_ID,                             
  NAME,                
  ADDRESS1,                
  ADDRESS2,                                            
  CITY,                                          
  STATE,                                    
  ZIP,       
  COUNTRY ,                                    
  CONTACT_PHONE,                                            
  CONTACT_EMAIL,  
  CONTACT_FAX,
  CONTACT_PHONE_EXT,       
  DATE_OF_BIRTH,                           
  CREATED_BY,               
  CREATED_DATETIME,                                            
  PARTY_TYPE_ID,                                            
  IS_ACTIVE,                                            
  PARTY_CODE,                  
  ADJUSTER_ID    ,                   -- Added by Asfa 29-Aug-2007   
  PARTY_TYPE,
  PARTY_CPF_CNPJ,
  NUMBER
  
 )                                            
 select                                            
  @PARTY_ID+row_number() OVER(ORDER BY CA.ADJUSTER_ID asc) ,                                           
  @CLAIM_ID,                                            
  ADJUSTER_NAME,                                     
  CA.SA_ADDRESS1,          
  CA.SA_ADDRESS2,          
  CA.SA_CITY,          
  CASE WHEN CA.SA_STATE IS NULL THEN 0 ELSE USER_STATE END ,          
  CA.SA_ZIPCODE, 
  CA.SA_COUNTRY,         
  CA.SA_PHONE,          
  USER_EMAIL,   
  CA.SA_FAX,
  SUBSTRING(ISNULL(USER_EXT,''),1,10) , 
  DATE_OF_BIRTH,     
  @CREATED_BY,                                            
  @CREATED_DATETIME,                                            
  @DETAIL_TYPE_ADJUSTER,  -- ADJUSTER TYPE CODE                                          
  'Y',                                            
  @ADJUSTER_CODE,   
  @ADJUSTER_ID  ,
  11109,-- PARTY_TYPE(HERE 11109 IS LOOKUP VALUE OF COMMERCIAL),
        -- FOR BRASIL DEPLOYMENT THEY NEED TO CREATE BRANCH AS USER TO CLAIM ADJUSTER, 
        -- SO WE ARE USING DEFAUT VALUE FOR PARTY_TYPE WHICH MEANS CLAIM ADJUSTER IS COMMERCIAL TYPE   
  MUL.CPF,
  (CASE WHEN len(USER_ADD1)-len(replace(USER_ADD1,',','')) > 0 THEN 
		CASE WHEN ISNUMERIC(DBO.Piece(USER_ADD1,',',len(USER_ADD1)-len(replace(USER_ADD1,',',''))+1)) =1 
		THEN
		DBO.Piece(USER_ADD1,',',len(USER_ADD1)-len(replace(USER_ADD1,',',''))+1)
		END
	 END )    
  FROM  MNT_USER_LIST MUL  WITH(NOLOCK) 
  JOIN CLM_ADJUSTER CA  WITH(NOLOCK) ON MUL.USER_ID= CA.USER_ID          
  WHERE ADJUSTER_ID=@ADJUSTER_ID                                          
                    
                                         

----------------------------------------
--  FOR COPY AGENCY DATA 
----------------------------------------                                            
SELECT                                            
  @PARTY_ID = ISNULL(MAX(PARTY_ID),0)+1                                            
 FROM                                            
  CLM_PARTIES  WITH(NOLOCK)                                           
 WHERE CLAIM_ID=@CLAIM_ID                                            
                                            
 INSERT INTO CLM_PARTIES                                            
 (                                            
  PARTY_ID,                                            
  CLAIM_ID,                                            
  NAME,                                      
  ADDRESS1,                 
  ADDRESS2,                                            
  CITY,                                          
  STATE,                                            
  ZIP,                                            
  CONTACT_PHONE,                                            
  CONTACT_EMAIL,                                            
  OTHER_DETAILS,                                            
  CREATED_BY,                                            
  CREATED_DATETIME,                                            
  PARTY_TYPE_ID,                                            
  COUNTRY,                           
  IS_ACTIVE ,
  SOURCE_PARTY_ID,-- Added by santosh kumar gautam on 15 Dec 2010
  SOURCE_PARTY_TYPE_ID, ---- Added by santosh kumar gautam on 15 Dec 2010    
  NUMBER,
  PARTY_CPF_CNPJ,
  PARTY_TYPE ,                          
  PARTY_PERCENTAGE, ---- Added by santosh kumar gautam on 15 Dec 2010 ,
  CONTACT_PHONE_EXT,
  CONTACT_FAX,
  FEDRERAL_ID,
  ACCOUNT_NUMBER,
  BANK_BRANCH,
  ACCOUNT_TYPE,
  DATE_OF_BIRTH,
  REGIONAL_ID,
  REGIONAL_ID_ISSUANCE,
  REGIONAL_ID_ISSUE_DATE,
  MARITAL_STATUS,
  GENDER
  
 )                                            
 select                                            
  @PARTY_ID+row_number() OVER(ORDER BY AGENCY.AGENCY_ID asc) ,                                          
  @CLAIM_ID,                                            
  AGENCY_DISPLAY_NAME,                                     
  M_AGENCY_ADD_1,          
  M_AGENCY_ADD_2,          
  M_AGENCY_CITY,          
   CASE WHEN M_AGENCY_STATE IS NULL THEN 0 ELSE M_AGENCY_STATE END ,         
  M_AGENCY_ZIP,          
  M_AGENCY_PHONE,          
  AGENCY_EMAIL,          
  '',                                            
  @CREATED_BY,                                            
  @CREATED_DATETIME,                                            
  @DETAIL_TYPE_AGENCY,                                            
  M_AGENCY_COUNTRY, 
  'Y',
  AGENCY.AGENCY_ID,  --SOURCE_PARTY_ID 
  AGENCY.AGENCY_TYPE_ID,  --SOURCE_PARTY_TYPE_ID 
  AGENCY.NUMBER,
  AGENCY.BROKER_CPF_CNPJ,
  AGENCY.BROKER_TYPE ,-- COMMERCIAL,PERSONAL,GOVERMENT
  POLICY.COMMISSION_PERCENT,
  SUBSTRING(AGENCY.AGENCY_EXT,1,10),
  AGENCY.AGENCY_FAX,
  AGENCY.FEDERAL_ID,
  AGENCY.BANK_ACCOUNT_NUMBER,
  AGENCY.BANK_BRANCH,
  AGENCY.ACCOUNT_TYPE,
  AGENCY.BROKER_DATE_OF_BIRTH,
  AGENCY.BROKER_REGIONAL_ID,
  AGENCY.REGIONAL_ID_ISSUANCE,
  AGENCY.REGIONAL_ID_ISSUE_DATE,
  AGENCY.MARITAL_STATUS,
  AGENCY.GENDER
  FROM POL_REMUNERATION POLICY  WITH(NOLOCK) LEFT OUTER JOIN 
  MNT_AGENCY_LIST AGENCY  WITH(NOLOCK)          
  ON POLICY.BROKER_ID = AGENCY.AGENCY_ID                  
  WHERE POLICY.CUSTOMER_ID=@CUSTOMER_ID AND POLICY.POLICY_ID=@POLICY_ID AND POLICY.POLICY_VERSION_ID=@POLICY_VERSION_ID                                      
                    
-------------------------------------------------------------------------          
          
--Added by Asfa (22-Apr-2008) - iTrack issue #3807          
--INSERT ADDITION NAME INSURED                                   
          
DECLARE CUR CURSOR            
 FOR SELECT ISNULL(CAL.FIRST_NAME,'') + ' ' + CASE ISNULL(CAL.MIDDLE_NAME,'') WHEN '' THEN '' ELSE ISNULL(CAL.MIDDLE_NAME,'') END + ' ' +           
 ISNULL(CAL.LAST_NAME,'') AS CO_APPLICANT_NAME, CAL.ADDRESS1, CAL.ADDRESS2, CAL.CITY, CAL.STATE,          
 CAL.ZIP_CODE, CAL.PHONE, CAL.EMAIL, CAL.COUNTRY           
 FROM CLT_APPLICANT_LIST CAL INNER JOIN POL_APPLICANT_LIST PAL          
 ON CAL.CUSTOMER_ID = PAL.CUSTOMER_ID AND CAL.APPLICANT_ID= PAL.APPLICANT_ID          
 WHERE PAL.CUSTOMER_ID = @CUSTOMER_ID AND PAL.POLICY_ID=@POLICY_ID           
 AND PAL.POLICY_VERSION_ID=@POLICY_VERSION_ID AND PAL.IS_PRIMARY_APPLICANT != 1          
           
OPEN CUR            
FETCH NEXT FROM CUR             
INTO @CO_APPLICANT_NAME, @AI_ADDRESS1, @AI_ADDRESS2, @AI_CITY, @AI_STATE, @AI_ZIP_CODE, @AI_PHONE, @AI_EMAIL, @AI_COUNTRY          
             
              
  WHILE @@FETCH_STATUS = 0            
  BEGIN          
          
SELECT                                            
  @PARTY_ID = ISNULL(MAX(PARTY_ID),0)+1                                            
 FROM                                            
  CLM_PARTIES  WITH(NOLOCK)                                           
 WHERE CLAIM_ID=@CLAIM_ID                                            
            
INSERT INTO CLM_PARTIES( PARTY_ID, CLAIM_ID, NAME, ADDRESS1, ADDRESS2, CITY, STATE,                                            
  ZIP, CONTACT_PHONE, CONTACT_EMAIL, OTHER_DETAILS, CREATED_BY, CREATED_DATETIME,                                            
  PARTY_TYPE_ID, COUNTRY, IS_ACTIVE )                                            
 VALUES( @PARTY_ID, @CLAIM_ID, @CO_APPLICANT_NAME, @AI_ADDRESS1, @AI_ADDRESS2, @AI_CITY, @AI_STATE,          
  @AI_ZIP_CODE, @AI_PHONE, @AI_EMAIL, '', @CREATED_BY, @CREATED_DATETIME, @DETAIL_TYPE_ADDITIONAL_INSURED,                                            
  @AI_COUNTRY, 'Y')          
               
              
FETCH NEXT FROM CUR             
INTO @CO_APPLICANT_NAME, @AI_ADDRESS1, @AI_ADDRESS2, @AI_CITY, @AI_STATE, @AI_ZIP_CODE, @AI_PHONE, @AI_EMAIL, @AI_COUNTRY          
              
END            
CLOSE CUR            
DEALLOCATE CUR             
            
          
-----------------------------          
          
                                            
--INSERT SUB- ADJUSTER DATA AT CLAIM PARTIES TABLE                                
--commented the following code as field has been removed from the screen                            
/*if (@SUB_ADJUSTER is not null and @SUB_ADJUSTER<>'') --add data only sub-adjuster is chosen                        
begin             
SELECT                                            
  @PARTY_ID = ISNULL(MAX(PARTY_ID),0)+1                                  
 FROM                                            
  CLM_PARTIES                                        
 WHERE CLAIM_ID=@CLAIM_ID                                   
                                
 INSERT INTO CLM_PARTIES                                            
 (                                            
  PARTY_ID,                                         
  CLAIM_ID,                                            
  NAME,                                            
  ADDRESS1,                                            
  ADDRESS2,                                            
  CITY,                                            
  STATE,                                            
  ZIP,                                            
  CONTACT_PHONE,                                            
  CREATED_BY,                                            
  CREATED_DATETIME,                                            
  PARTY_TYPE_ID,                                            
  COUNTRY,                                            
  IS_ACTIVE,                                            PARTY_CODE                                
 )                                            
 select                                            
  @PARTY_ID,                                            
  @CLAIM_ID,            
  SUB_ADJUSTER,                                            
  SA_ADDRESS1,                 
  SA_ADDRESS2,                                            
  SA_CITY,                                            
  SA_STATE,                            
  SA_ZIPCODE,                                            
 SA_PHONE,                                            
  @CREATED_BY,                      
  @CREATED_DATETIME,                                            
  @SUB_ADJUSTER_PARTY_ID,                           
  SA_COUNTRY,                        
  'Y',                                            
 @ADJUSTER_CODE                                            
 from CLM_ADJUSTER                                            
 where                                            
  ADJUSTER_ID=@ADJUSTER_CODE                                            
end                                  
*/                          
                                            
--Make an activity entry at the clm_activity table with the status as Authorised and Activity Reason - First Notification                                            
 --11805--First Notification (Lookup Unique ID)                                            
--Initially when the claim is added, we will set its status to complete                                            
                
/*Commented by Asfa 03-Sept-2007 in Reference to iTrack Issue #2424 Problem No- 3*/                
--exec Proc_InsertCLM_ACTIVITY @CLAIM_ID,0,11805,'',@CREATED_BY,@COMPLETE,'',192                                            
                              
--Add entries in the linked claim table                              
exec Proc_InsertCLM_LINKED_CLAIMS @CLAIM_ID,@LINKED_CLAIM_ID_LIST                              
                          
--Add diary entry for new selected pink slip notify users                          
if (@NEW_RECIEVE_PINK_SLIP_USERS_LIST is not null) and (@NEW_RECIEVE_PINK_SLIP_USERS_LIST<>'')                        
 exec Proc_Clm_InsertDiaryEntryForPinkSlipUsers @CLAIM_ID,@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@NEW_RECIEVE_PINK_SLIP_USERS_LIST,@CREATED_BY                          
          
          
/*- Added by Asfa Praveen (06-Feb-2008) - iTrack issue #3545          
11739 - Open          
11740 - Closed          
*/          
          
DECLARE @CLOSED_DATE DATETIME          
          
IF(@CLAIM_STATUS =11739)           
BEGIN          
  IF EXISTS(SELECT LISTID FROM TODOLIST  WITH(NOLOCK) WHERE CLAIMID=@CLAIM_ID)          
   BEGIN          
 UPDATE TODOLIST SET LISTOPEN ='Y' WHERE CLAIMID=@CLAIM_ID           
   END          
 -- Added by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008          
 SELECT @CLOSED_DATE=CLOSED_DATE FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID          
 IF(@CLOSED_DATE IS NOT NULL AND @CLOSED_DATE!='')          
  UPDATE CLM_CLAIM_INFO SET REOPENED_DATE=GETDATE() WHERE CLAIM_ID=@CLAIM_ID          
          
END          
ELSE IF(@CLAIM_STATUS =11740)           
BEGIN          
  IF EXISTS(SELECT LISTID FROM TODOLIST WITH(NOLOCK) WHERE CLAIMID=@CLAIM_ID)          
   BEGIN          
 UPDATE TODOLIST SET LISTOPEN ='N' WHERE CLAIMID=@CLAIM_ID           
  END          
 -- Added by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008          
 UPDATE CLM_CLAIM_INFO SET CLOSED_DATE=GETDATE() WHERE CLAIM_ID=@CLAIM_ID          
END          
 
-------------------------------------------------------------------------------------------         
  -- Added by Santosh Kumar Gautam 
  
----------------------------------------
--  FOR COPY COINSURANCE DATA 
----------------------------------------    
 
  DECLARE @PartyID INT=0  
  
  SELECT @PartyID=(ISNULL(MAX([PARTY_ID]),0)) FROM CLM_PARTIES  
  
  INSERT INTO CLM_PARTIES  
	  (  
		  CLAIM_ID,                 
		  PARTY_ID, 
		  PARTY_TYPE_ID, 
		  PARTY_CODE,
		  PARTY_TYPE,         
		  NAME,     
		  ADDRESS1, 
		  ADDRESS2,     
		  CITY,     
		  [STATE],  
		  ZIP,      
		  COUNTRY,  
		  CONTACT_PHONE, 
		  CONTACT_EMAIL,
		  CONTACT_FAX,  
		  SOURCE_PARTY_ID,
		  PARTY_PERCENTAGE,
		  SOURCE_PARTY_TYPE_ID,
		  IS_ACTIVE, 
		  CREATED_BY, 
		  CREATED_DATETIME  ,
		  PARTY_CPF_CNPJ,
		  NUMBER,
		  CONTACT_PHONE_EXT,
		  ACCOUNT_NUMBER,
		  FEDRERAL_ID,
		  ACCOUNT_TYPE,
		  BANK_BRANCH,
		  BANK_NUMBER
        )   
     (  
       SELECT   
		@CLAIM_ID, 
		@PartyID+row_number() OVER(ORDER BY M.REIN_COMAPANY_ID asc) ,     
		618,--PARTY_TYPE_ID check in CLM_TYPE_DETAIL and Clm_type_master
		REIN_COMAPANY_CODE,
		11109,--PARTY_TYPE FOR commercial  
		REIN_COMAPANY_NAME,
		REIN_COMAPANY_ADD1,
		REIN_COMAPANY_ADD2,  
		REIN_COMAPANY_CITY, 
		ISNULL(M2.STATE_ID,0) ,--REIN_COMAPANY_STATE,   
		REIN_COMAPANY_ZIP, 
		ISNULL(M1.COUNTRY_ID,0) ,--REIN_COMAPANY_COUNTRY,  
		REIN_COMAPANY_PHONE,
		REIN_COMAPANY_EMAIL,  
		REIN_COMAPANY_FAX,
		P.COMPANY_ID ,		   -- SOURCE_PARTY_ID,
		P.COINSURANCE_PERCENT, -- PARTY_PERCENTAGE,
		P.LEADER_FOLLOWER,     -- SOURCE_PARTY_TYPE_ID,		
		'Y',
		@CREATED_BY,
		@CREATED_DATETIME  ,
		CARRIER_CNPJ  ,
		 SUBSTRING((CASE WHEN len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',','')) > 0 THEN 
				CASE WHEN ISNUMERIC(DBO.Piece(REIN_COMAPANY_ADD1,',',len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',',''))+1)) =1 
				THEN
				DBO.Piece(REIN_COMAPANY_ADD1,',',len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',',''))+1)
				END
			 END 
		 ),0,20) ,
		SUBSTRING(REIN_COMAPANY_EXT,0,10) ,
        REIN_COMAPANY_ACC_NUMBER ,
        FEDERAL_ID,
        BANK_ACCOUNT_TYPE ,
        BANK_BRANCH_NUMBER,
        BANK_NUMBER       
	  FROM [MNT_REIN_COMAPANY_LIST] M INNER JOIN  
		    POL_CO_INSURANCE P ON P.COMPANY_ID=M.REIN_COMAPANY_ID  LEFT OUTER JOIN
		    MNT_COUNTRY_LIST M1 ON M1.COUNTRY_NAME=M.REIN_COMAPANY_COUNTRY  LEFT OUTER JOIN
		    MNT_COUNTRY_STATE_LIST M2 ON M2.STATE_CODE=M.REIN_COMAPANY_STATE	
      WHERE (CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
           -- COPY ALBA AS COINSURER PARTY REFER ITRACK:1263
            -- AND M.REIN_COMAPANY_ID NOT IN (SELECT SYS_CARRIER_ID FROM MNT_SYSTEM_PARAMS)
              )--AND P.IS_ACTIVE<>'N')  
      )  
      
      
         
    
  
-- Added by Santosh Kumar Gautam 

--==================================================================================
--  FOR ALBA AS A COISURANCE PARTY( REFER ITRACK 1263 NOTES ADDED ON 16 JUNE 2011)
--==================================================================================

IF NOT EXISTS(SELECT 1 FROM CLM_PARTIES WHERE CLAIM_ID=@CLAIM_ID AND PARTY_TYPE_ID=618 AND SOURCE_PARTY_ID IN (SELECT SYS_CARRIER_ID FROM MNT_SYSTEM_PARAMS))
 BEGIN
 
   SELECT @PartyID=(ISNULL(MAX([PARTY_ID]),0)) FROM CLM_PARTIES  
   
  INSERT INTO CLM_PARTIES  
	  (  
		  CLAIM_ID,                 
		  PARTY_ID, 
		  PARTY_TYPE_ID, 
		  PARTY_CODE,
		  PARTY_TYPE,         
		  NAME,     
		  ADDRESS1, 
		  ADDRESS2,     
		  CITY,     
		  [STATE],  
		  ZIP,      
		  COUNTRY,  
		  CONTACT_PHONE, 
		  CONTACT_EMAIL,
		  CONTACT_FAX,  
		  SOURCE_PARTY_ID,
		  PARTY_PERCENTAGE,
		  SOURCE_PARTY_TYPE_ID,
		  IS_ACTIVE, 
		  CREATED_BY, 
		  CREATED_DATETIME  ,
		  PARTY_CPF_CNPJ,
		  NUMBER,
		  CONTACT_PHONE_EXT,
		  ACCOUNT_NUMBER,
		  FEDRERAL_ID,
		  ACCOUNT_TYPE,
		  BANK_BRANCH,
		  BANK_NUMBER
        )   
     (  
       SELECT   
		@CLAIM_ID, 
		@PartyID+row_number() OVER(ORDER BY M.REIN_COMAPANY_ID asc) ,     
		618,--PARTY_TYPE_ID check in CLM_TYPE_DETAIL and Clm_type_master
		REIN_COMAPANY_CODE,
		11109,--PARTY_TYPE FOR commercial  
		REIN_COMAPANY_NAME,
		REIN_COMAPANY_ADD1,
		REIN_COMAPANY_ADD2,  
		REIN_COMAPANY_CITY, 
		CS.STATE_ID ,--REIN_COMAPANY_STATE,  -- Modified by Santosh Kr Gautam on 08 Aug 2011 for itrack 1263 
		REIN_COMAPANY_ZIP, 
		5 ,--REIN_COMAPANY_COUNTRY,  
		REIN_COMAPANY_PHONE,
		REIN_COMAPANY_EMAIL,  
		REIN_COMAPANY_FAX,
		M.REIN_COMAPANY_ID ,    -- SOURCE_PARTY_ID,
		100,						-- PARTY_PERCENTAGE, -- MODIFIED BY SANTOSH KR GAUTAM ON 18 AUG 2011 FOR ITRACK 1063
		14549,      -- SOURCE_PARTY_TYPE_ID, --( 14548 :LEADER ,14549 :FOLLOWER)		
		'Y',
		@CREATED_BY,
		@CREATED_DATETIME  ,
		CARRIER_CNPJ  ,
		 SUBSTRING((CASE WHEN len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',','')) > 0 THEN 
				CASE WHEN ISNUMERIC(DBO.Piece(REIN_COMAPANY_ADD1,',',len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',',''))+1)) =1 
				THEN
				DBO.Piece(REIN_COMAPANY_ADD1,',',len(REIN_COMAPANY_ADD1)-len(replace(REIN_COMAPANY_ADD1,',',''))+1)
				END
			 END 
		 ),0,20) ,
		SUBSTRING(REIN_COMAPANY_EXT,0,10) ,
        REIN_COMAPANY_ACC_NUMBER ,
        FEDERAL_ID,
        BANK_ACCOUNT_TYPE ,
        BANK_BRANCH_NUMBER,
        BANK_NUMBER       
       
	  FROM [MNT_REIN_COMAPANY_LIST] M WITH (NOLOCK) INNER JOIN  
		    MNT_SYSTEM_PARAMS  P WITH (NOLOCK) ON P.SYS_CARRIER_ID=M.REIN_COMAPANY_ID LEFT OUTER JOIN			
			MNT_COUNTRY_STATE_LIST CS WITH (NOLOCK) ON CS.STATE_CODE=M.REIN_COMAPANY_STATE
    
		 )
 END



----------------------------------------  
       
 
 
		

----------------------------------------------------------------------------------------------


--==========================================================================
-- ADDED BY SANTOSH KUMAR GAUTAM ON 30 MARCH 2011 FOR ITRACK :1026
-- TO COPY BROKER FROM POLICY SCREEN 
-- IF BROKER IS ALREADY COPED FROM REMUNARATION TAB THEN DONT COPY FURTHER
--==========================================================================
 
 IF(NOT EXISTS(SELECT CLAIM_ID FROM  CLM_PARTIES WHERE CLAIM_ID=@CLAIM_ID AND PARTY_TYPE_ID=208 AND SOURCE_PARTY_ID=@POLICY_AGENCY_ID))-- 208 CODE FOR AGENCY
  BEGIN
  
	 SELECT                                            
	  @PARTY_ID = ISNULL(MAX(PARTY_ID),0)+1                                            
	 FROM                                            
	  CLM_PARTIES  WITH(NOLOCK)                                           
	 WHERE CLAIM_ID=@CLAIM_ID                                            
	                                            
	 INSERT INTO CLM_PARTIES                                            
	 (                                            
	  PARTY_ID,                                            
	  CLAIM_ID,                                            
	  NAME,                                      
	  ADDRESS1,                 
	  ADDRESS2,                                            
	  CITY,                                          
	  STATE,                                            
	  ZIP,                                            
	  CONTACT_PHONE,                                            
	  CONTACT_EMAIL,                                            
	  OTHER_DETAILS,                                            
	  CREATED_BY,                                            
	  CREATED_DATETIME,                                            
	  PARTY_TYPE_ID,                                            
	  COUNTRY,                           
	  IS_ACTIVE ,
	  SOURCE_PARTY_ID,-- Added by santosh kumar gautam on 15 Dec 2010
	  SOURCE_PARTY_TYPE_ID, ---- Added by santosh kumar gautam on 15 Dec 2010    
	  NUMBER,
	  PARTY_CPF_CNPJ,
	  PARTY_TYPE ,                          
	  PARTY_PERCENTAGE, ---- Added by santosh kumar gautam on 15 Dec 2010 ,
	  CONTACT_PHONE_EXT,
	  CONTACT_FAX,
	  FEDRERAL_ID,
	  ACCOUNT_NUMBER,
	  BANK_BRANCH,
	  ACCOUNT_TYPE,
	  DATE_OF_BIRTH,
	  REGIONAL_ID,
	  REGIONAL_ID_ISSUANCE,
	  REGIONAL_ID_ISSUE_DATE,
	  MARITAL_STATUS,
	  GENDER
	  
	 )                                            
	 select                                            
	  @PARTY_ID+row_number() OVER(ORDER BY AGENCY.AGENCY_ID asc) ,                                          
	  @CLAIM_ID,                                            
	  AGENCY_DISPLAY_NAME,                                     
	  M_AGENCY_ADD_1,          
	  M_AGENCY_ADD_2,          
	  M_AGENCY_CITY,          
	   CASE WHEN M_AGENCY_STATE IS NULL THEN 0 ELSE M_AGENCY_STATE END ,         
	  M_AGENCY_ZIP,          
	  M_AGENCY_PHONE,          
	  AGENCY_EMAIL,          
	  '',                                            
	  @CREATED_BY,                                            
	  @CREATED_DATETIME,                                            
	  @DETAIL_TYPE_AGENCY,                                            
	  M_AGENCY_COUNTRY, 
	  'Y',
	  AGENCY.AGENCY_ID,  --SOURCE_PARTY_ID 
	  AGENCY.AGENCY_TYPE_ID,  --SOURCE_PARTY_TYPE_ID 
	  AGENCY.NUMBER,
	  AGENCY.BROKER_CPF_CNPJ,
	  AGENCY.BROKER_TYPE ,-- COMMERCIAL,PERSONAL,GOVERMENT
	  AGENCY.AGENCY_COMMISSION,
	  AGENCY.AGENCY_EXT,
	  AGENCY.AGENCY_FAX,
	  AGENCY.FEDERAL_ID,
	  AGENCY.BANK_ACCOUNT_NUMBER,
	  AGENCY.BANK_BRANCH,
	  AGENCY.ACCOUNT_TYPE,
	  AGENCY.BROKER_DATE_OF_BIRTH,
	  AGENCY.BROKER_REGIONAL_ID,
	  AGENCY.REGIONAL_ID_ISSUANCE,
	  AGENCY.REGIONAL_ID_ISSUE_DATE,
	  AGENCY.MARITAL_STATUS,
	  AGENCY.GENDER
	  FROM MNT_AGENCY_LIST AGENCY  WITH(NOLOCK)                        
	  WHERE AGENCY_ID=@POLICY_AGENCY_ID
   
  END       
       
----------------------------------------------------------------------------------------------
 DECLARE @COINSURANCE_ID INT=0     
 
 SELECT @COINSURANCE_ID=(ISNULL(MAX([COINSURANCE_ID]),0))+1 FROM CLM_CO_INSURANCE  WITH(NOLOCK) 
       

-- Added by Santosh Kumar Gautam on 14 Dec 2010              
-- Add default record in coinsurance when claim is added(value of SUSPE code, leader policy no., leader endorsement no. in Coinsurance tab copied when claim is added).              
--------------------------------------------------
 -- MODIFIED BY SANTOSH KR. GAUTAM ON 15 JUL 2011 (ITRACK:1021 AS PER NOTES ON 14 JUL 2011)   
 -- TO COPY LEADER ENDORSEMENT NUMBER 
 -------------------------------------------------
 DECLARE @LEADER_POLICY_NUM NVARCHAR(25)
 DECLARE @LEADER_ENDS_NUM NVARCHAR(25)
 
 SELECT @LEADER_POLICY_NUM =P.LEADER_POLICY_NUMBER,
        @LEADER_ENDS_NUM   =P.ENDORSEMENT_POLICY_NUMBER
 FROM  POL_CO_INSURANCE P WITH(NOLOCK) 
 WHERE P.LEADER_FOLLOWER      = 14549				AND  -- COPY LEADER POLICY NUMBER OF FOLLOWER
       P.CUSTOMER_ID          = @CUSTOMER_ID		AND 
       P.POLICY_ID		      = @POLICY_ID			AND 
       P.POLICY_VERSION_ID    = @POLICY_VERSION_ID  AND
       ISNULL(P.IS_ACTIVE,'Y')='Y'
 
INSERT INTO CLM_CO_INSURANCE
 (
    [COINSURANCE_ID],                 
	[CLAIM_ID] ,
	[LEADER_SUSEP_CODE] ,
	[LEADER_POLICY_NUMBER]  ,
	[LEADER_ENDORSEMENT_NUMBER] ,	
	--[LEADER_CLAIM_NUMBER] ,	
	--[CLAIM_REGISTRATION_DATE] ,	
	[IS_ACTIVE] ,
	[CREATED_BY] ,
	[CREATED_DATETIME] 
   )
   (
	 SELECT   
	@COINSURANCE_ID,   
	@CLAIM_ID ,  
	M.SUSEP_NUM ,  
	@LEADER_POLICY_NUM,  
	@LEADER_ENDS_NUM,  --NULL ,  --[LEADER_ENDORSEMENT_NUMBER]
	'Y',    --[IS_ACTIVE]
	@CREATED_BY,  
	@CREATED_DATETIME 
	FROM [MNT_REIN_COMAPANY_LIST] M  WITH(NOLOCK) INNER JOIN  
	POL_CO_INSURANCE P WITH(NOLOCK)  ON P.COMPANY_ID=M.REIN_COMAPANY_ID  --LEFT OUTER JOIN
	--POL_POLICY_ENDORSEMENTS EN WITH(NOLOCK) ON  P.CUSTOMER_ID=EN.CUSTOMER_ID AND P.POLICY_ID=EN.POLICY_ID AND P.POLICY_VERSION_ID=EN.POLICY_VERSION_ID 
	WHERE (P.LEADER_FOLLOWER=14548 AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID=@POLICY_VERSION_ID 
	AND ISNULL(P.IS_ACTIVE,'Y')='Y')   
	-- HERE LEADER_FOLLOWER=14548 MEANS LEADER
	)          
              

           
END                                            
--go      
--exec Proc_InsertCLM_CLAIM_INFO 1898,4,3,1,'2009-08-08','06',19,'',0,null,'Rojan Thomas Joseph',1,'46001','34 Lion Strret','','Wales Street','','','','','','2009-08-08',11739,0,'0.0','0.0','0.0','0.0','0.0','',334,'2009-08-08','',0,0,0,0,0,14,14133,'N',
  
    
    
    
    
--2,'',2009,'',null,null,null      
--rollback tran          
--          
--          
          
          
    
    
    


GO

