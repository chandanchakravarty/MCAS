IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_CLAIM_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_CLAIM_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------                                                                        
Proc Name       : dbo.Proc_UpdateCLM_CLAIM_INFO                                                                  
Created by      : Sumit Chhabra                                                                      
Date            : 27/04/2006                                                                        
Purpose         : Update data in CLM_CLAIM_INFO table for claim notification screen                                                    
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
-- DROP PROC dbo.Proc_UpdateCLM_CLAIM_INFO                                                                    
CREATE PROC [dbo].[Proc_UpdateCLM_CLAIM_INFO]                                                                    
 @CUSTOMER_ID int,                                                    
 @POLICY_ID int,                                                    
 @POLICY_VERSION_ID smallint,                                                    
 @CLAIM_ID int,                                                    
 @LOSS_DATE datetime,                                                    
 @ADJUSTER_CODE varchar(10),                                                    
 @ADJUSTER_ID INT,        
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
 @MODIFIED_BY int,                                                    
 @LAST_UPDATED_DATETIME datetime,                             --@SUB_ADJUSTER varchar(50),         
 --@SUB_ADJUSTER_CONTACT varchar(50),                                                
 @EXTENSION varchar(5),                                              
 @LOSS_TIME_AM_PM smallint,                                          
 @LITIGATION_FILE int,                           
 @STATE int,                                    
 @CLAIMANT_PARTY int,                                
 --@LINKED_TO_CLAIM varchar(500),                                
 @LINKED_CLAIM_ID_LIST VARCHAR(500),                  
 --@ADD_FAULT char(1),                                
 --@TOTAL_LOSS char(1),                                
 @NOTIFY_REINSURER int,                            
 @REPORTED_TO varchar(50),                        
 @FIRST_NOTICE_OF_LOSS datetime,                
 @RECIEVE_PINK_SLIP_USERS_LIST varchar(200),            
 @NEW_RECIEVE_PINK_SLIP_USERS_LIST varchar(200),          
 @PINK_SLIP_TYPE_LIST varchar(200),  
 @CLAIM_STATUS_UNDER int,  
 @AT_FAULT_INDICATOR int,  --Done for Itrack Issue 6620 on 27 Nov 09      
 @LAST_DOC_RECEIVE_DATE datetime,
 @REINSURANCE_TYPE int ,  
 @REIN_CLAIM_NUMBER nvarchar(500), 
 @REIN_LOSS_NOTICE_NUM nvarchar(500)  ,
 @IS_VICTIM_CLAIM INT,
 @POSSIBLE_PAYMENT_DATE datetime
      
AS                                                       
BEGIN                             
                        
--if not exists(SELECT CUSTOMER_ID FROM CLM_CLAIM_INFO WHERE CLAIM_NUMBER LIKE '%'+ RTRIM(LTRIM(@LINKED_TO_CLAIM)) +'%')                        
-- RETURN -2                        
 DECLARE @DUMMY_POLICY_ID INT  
 SELECT @DUMMY_POLICY_ID = ISNULL(DUMMY_POLICY_ID,0) FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID  
 IF(@DUMMY_POLICY_ID<>0)  
   BEGIN  
 SET @POLICY_ID = 0  
   END  
  
 declare @DETAIL_TYPE_ADJUSTER int  
 SET @DETAIL_TYPE_ADJUSTER = 12 
                                          
 UPDATE CLM_CLAIM_INFO                                               
 SET                                                       
 LOSS_DATE =@LOSS_DATE,                                                    
 ADJUSTER_CODE =@ADJUSTER_CODE,         
 ADJUSTER_ID = @ADJUSTER_ID,                                           
 REPORTED_BY =@REPORTED_BY,                                                    
 CATASTROPHE_EVENT_CODE =@CATASTROPHE_EVENT_CODE,                                                
 --  CLAIMANT_INSURED = @CLAIMANT_INSURED,                                                    
 INSURED_RELATIONSHIP =@INSURED_RELATIONSHIP,                                                   
 CLAIMANT_NAME =@CLAIMANT_NAME,                                                    
 COUNTRY =@COUNTRY,                                                    
 ZIP= @ZIP,                                                    
 ADDRESS1=@ADDRESS1,                                                    
 ADDRESS2=@ADDRESS2,                           
 CITY=@CITY,                                                    
 HOME_PHONE=@HOME_PHONE,                                                    
 WORK_PHONE=@WORK_PHONE,                                                    
 MOBILE_PHONE=@MOBILE_PHONE,                                                    
 WHERE_CONTACT=@WHERE_CONTACT,                                                    
 WHEN_CONTACT=@WHEN_CONTACT,                                                    
-- DIARY_DATE=@DIARY_DATE,                                   
 CLAIM_STATUS=@CLAIM_STATUS,                                                    
 OUTSTANDING_RESERVE=@OUTSTANDING_RESERVE,                                                    
 RESINSURANCE_RESERVE=@RESINSURANCE_RESERVE,                                                    
 PAID_LOSS=@PAID_LOSS,                                                    
 PAID_EXPENSE=@PAID_EXPENSE,                                                    
 RECOVERIES=@RECOVERIES,                                                    
 CLAIM_DESCRIPTION=@CLAIM_DESCRIPTION,                                            
 MODIFIED_BY=@MODIFIED_BY,                                                    
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,                                                
 -- SUB_ADJUSTER=@SUB_ADJUSTER,      
 -- SUB_ADJUSTER_CONTACT=@SUB_ADJUSTER_CONTACT,      
 EXTENSION=@EXTENSION,                                              
 LOSS_TIME_AM_PM=@LOSS_TIME_AM_PM,                                          
 LITIGATION_FILE = @LITIGATION_FILE,                              
 STATE = @STATE,                                    
 CLAIMANT_PARTY = @CLAIMANT_PARTY,                                
 -- LINKED_TO_CLAIM = @LINKED_TO_CLAIM,                                 
 -- ADD_FAULT = @ADD_FAULT,                                 
 -- TOTAL_LOSS = @TOTAL_LOSS,                                
 NOTIFY_REINSURER = @NOTIFY_REINSURER,                            
 REPORTED_TO = @REPORTED_TO,                            
 FIRST_NOTICE_OF_LOSS = @FIRST_NOTICE_OF_LOSS,              
 RECIEVE_PINK_SLIP_USERS_LIST = @RECIEVE_PINK_SLIP_USERS_LIST,          
 PINK_SLIP_TYPE_LIST = @PINK_SLIP_TYPE_LIST,  
 CLAIM_STATUS_UNDER =  @CLAIM_STATUS_UNDER,   
 AT_FAULT_INDICATOR = @AT_FAULT_INDICATOR,   --Done for Itrack Issue 6620 on 27 Nov 09       
 LAST_DOC_RECEIVE_DATE= @LAST_DOC_RECEIVE_DATE,
 REINSURANCE_TYPE	  = @REINSURANCE_TYPE,     --Added by santosh kumar gautam on 08 Feb 2011
 REIN_CLAIM_NUMBER	  = @REIN_CLAIM_NUMBER,    --Added by santosh kumar gautam on 08 Feb 2011
 REIN_LOSS_NOTICE_NUM = @REIN_LOSS_NOTICE_NUM,  --Added by santosh kumar gautam on 08 Feb 2011
 IS_VICTIM_CLAIM = @IS_VICTIM_CLAIM	,			--Added by santosh kumar gautam on 08 Feb 2011
 POSSIBLE_PAYMENT_DATE   =@POSSIBLE_PAYMENT_DATE --Added by santosh kumar gautam on 15 Feb 2011
 WHERE                                                    
  CUSTOMER_ID = @CUSTOMER_ID AND                                                     
  POLICY_ID =  @POLICY_ID AND                                                     
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND                                                     
  CLAIM_ID =@CLAIM_ID                                         

                                      
--Check if the adjuster added already exists at claim parties table...                                      
--if the record updated contains new adjuter, then add  the adjuster data at the parties table also            
          
IF NOT EXISTS(SELECT CLAIM_ID FROM CLM_PARTIES WHERE CLAIM_ID=@CLAIM_ID AND ADJUSTER_ID=@ADJUSTER_ID) -- PARTY_CODE=@ADJUSTER_CODE // Commented by Asfa 29/Aug/2007                                        
BEGIN                                                    
--INSERT ADJUSTER DATA AT CLAIM PARTIES TABLE                                          
 DECLARE @PARTY_ID int                                                  
SELECT                                                       
  @PARTY_ID = ISNULL(MAX(PARTY_ID),0)+1                                                         
 FROM                                             
  CLM_PARTIES                                            
 WHERE CLAIM_ID=@CLAIM_ID                                                                        
                                                                        
 --INSERT INTO CLM_PARTIES                                      
 --(                                      
 -- PARTY_ID,                                      
 -- CLAIM_ID,                                      
 -- NAME,                                      
 -- ADDRESS1,           
 -- ADDRESS2,                                      
 -- CITY,                                      
 -- STATE,                                      
 -- ZIP,                                      
 -- CONTACT_PHONE,                                      
 -- CONTACT_EMAIL,                                      
 -- OTHER_DETAILS,                                      
 -- CREATED_BY,                                      
 -- CREATED_DATETIME,                                      
 -- PARTY_TYPE_ID,                                      
 -- COUNTRY,                                      
 -- IS_ACTIVE,                                      
 -- PARTY_CODE,            
 -- ADJUSTER_ID                       -- Added by Asfa 29-Aug-2007            
 --)                                      
 --select                                      
 -- @PARTY_ID,                                 
 -- @CLAIM_ID,                                      
 -- ADJUSTER_NAME,                               
 -- USER_ADD1,    
 -- USER_ADD2,    
 -- USER_CITY,    
 -- USER_STATE,    
 -- USER_ZIP,    
 -- USER_PHONE,    
 -- USER_EMAIL,    
 -- '',                                      
 -- @MODIFIED_BY,                                    
 -- @LAST_UPDATED_DATETIME,                                      
 -- 12, --detail_type_id for adjuster                                      
 -- COUNTRY,    
 -- 'Y',                                      
 -- @ADJUSTER_CODE,            
 -- @ADJUSTER_ID     
 -- FROM  MNT_USER_LIST MUL JOIN CLM_ADJUSTER CA ON MUL.USER_ID= CA.USER_ID    
 -- WHERE ADJUSTER_ID=@ADJUSTER_ID                                    
 ----------------------------------------
--  FOR COPY ADJUSTER DATA 
----------------------------------------                                  
                                          
SELECT                                            
  @PARTY_ID = ISNULL(MAX(PARTY_ID),0)                                           
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
  NUMBER
  
 )                                            
 select                                            
  @PARTY_ID+row_number() OVER(ORDER BY CA.ADJUSTER_ID asc) ,                                           
  @CLAIM_ID,                                            
  ADJUSTER_NAME,                                     
  USER_ADD1,          
  USER_ADD2,          
  USER_CITY,          
  USER_STATE,          
  USER_ZIP, 
  COUNTRY,         
  USER_PHONE,          
  USER_EMAIL,   
  USER_FAX,
  SUBSTRING(ISNULL(USER_EXT,''),1,10) , 
  DATE_OF_BIRTH,     
  @MODIFIED_BY,                                            
  @LAST_UPDATED_DATETIME,                                            
  @DETAIL_TYPE_ADJUSTER,  -- ADJUSTER TYPE CODE                                          
  'Y',                                            
  @ADJUSTER_CODE,   
  @ADJUSTER_ID  ,
  11109,-- PARTY_TYPE(HERE 11109 IS LOOKUP VALUE OF COMMERCIAL),
        -- FOR BRASIL DEPLOYMENT THEY NEED TO CREATE BRANCH AS USER TO CLAIM ADJUSTER, 
        -- SO WE ARE USING DEFAUT VALUE FOR PARTY_TYPE WHICH MEANS CLAIM ADJUSTER IS COMMERCIAL TYPE   
  (CASE WHEN len(USER_ADD1)-len(replace(USER_ADD1,',','')) > 0 THEN 
		CASE WHEN ISNUMERIC(DBO.Piece(USER_ADD1,',',len(USER_ADD1)-len(replace(USER_ADD1,',',''))+1)) =1 
		THEN
		DBO.Piece(USER_ADD1,',',len(USER_ADD1)-len(replace(USER_ADD1,',',''))+1)
		END
	 END )    
  FROM  MNT_USER_LIST MUL  WITH(NOLOCK) 
  JOIN CLM_ADJUSTER CA  WITH(NOLOCK) ON MUL.USER_ID= CA.USER_ID          
  WHERE ADJUSTER_ID=@ADJUSTER_ID                                  
                                 
 --INSERT INTO CLM_PARTIES                                            
 --(                              
 -- PARTY_ID,                                 
 -- CLAIM_ID,                             
 -- NAME,                
 -- ADDRESS1,                
 -- ADDRESS2,                                            
 -- CITY,                                          
 -- STATE,                                    
 -- ZIP,                                            
 -- CONTACT_PHONE,                                            
 -- CONTACT_EMAIL,                                          
 -- OTHER_DETAILS,                                            
 -- CREATED_BY,               
 -- CREATED_DATETIME,                                            
 -- PARTY_TYPE_ID,                                            
 -- COUNTRY,                                            
 -- IS_ACTIVE,                                            
 -- PARTY_CODE,                  
 -- ADJUSTER_ID    ,                   -- Added by Asfa 29-Aug-2007 
 -- PARTY_TYPE,
 -- NUMBER
  
 --)                                            
 -- select                                            
 -- @PARTY_ID+row_number() OVER(ORDER BY CA.ADJUSTER_ID asc) ,                                           
 -- @CLAIM_ID,                                            
 -- ADJUSTER_NAME,                                     
 -- USER_ADD1,          
 -- USER_ADD2,          
 -- USER_CITY,          
 -- USER_STATE,          
 -- USER_ZIP,          
 -- USER_PHONE,          
 -- USER_EMAIL,          
 -- '',                                          
 -- @MODIFIED_BY,                                            
 -- @LAST_UPDATED_DATETIME,                                            
 -- @DETAIL_TYPE_ADJUSTER,                                            
 -- COUNTRY,          
 -- 'Y',                                            
 -- @ADJUSTER_CODE,   
 -- @ADJUSTER_ID  ,
 -- 11109,-- PARTY_TYPE(HERE 11109 IS LOOKUP VALUE OF COMMERCIAL),
 --       -- FOR BRASIL DEPLOYMENT THEY NEED TO CREATE BRANCH AS USER TO CLAIM ADJUSTER, 
 --       -- SO WE ARE USING DEFAUT VALUE FOR PARTY_TYPE WHICH MEANS CLAIM ADJUSTER IS COMMERCIAL TYPE   
 -- (CASE WHEN len(USER_ADD1)-len(replace(USER_ADD1,',','')) > 0 THEN 
	--	CASE WHEN ISNUMERIC(DBO.Piece(USER_ADD1,',',len(USER_ADD1)-len(replace(USER_ADD1,',',''))+1)) =1 
	--	THEN
	--	DBO.Piece(USER_ADD1,',',len(USER_ADD1)-len(replace(USER_ADD1,',',''))+1)
	--	END
	-- END )    
 -- FROM  MNT_USER_LIST MUL  WITH(NOLOCK) 
 -- JOIN CLM_ADJUSTER CA  WITH(NOLOCK) ON MUL.USER_ID= CA.USER_ID          
 -- WHERE ADJUSTER_ID=@ADJUSTER_ID                                                    

END
--Add entries in the linked claim table                  
exec Proc_InsertCLM_LINKED_CLAIMS @CLAIM_ID,@LINKED_CLAIM_ID_LIST                                      
  
-- Asfa (14-May-2008) - iTrack issue #4193, #4175  
/*- Added by Asfa Praveen (06-Feb-2008) - iTrack issue #3545  
11739 - Open  
11740 - Closed  
*/  
  
DECLARE @CLOSED_DATE DATETIME  
  
IF(@CLAIM_STATUS =11739)   
BEGIN  
  IF EXISTS(SELECT LISTID FROM TODOLIST WHERE CLAIMID=@CLAIM_ID)  
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
  IF EXISTS(SELECT LISTID FROM TODOLIST WHERE CLAIMID=@CLAIM_ID)  
   BEGIN  
 UPDATE TODOLIST SET LISTOPEN ='N' WHERE CLAIMID=@CLAIM_ID   
   END  
 -- Added by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008  
 UPDATE CLM_CLAIM_INFO SET CLOSED_DATE=GETDATE() WHERE CLAIM_ID=@CLAIM_ID  
END  
  
    
--Add diary entries for new selected pink slip notify users            
if (@NEW_RECIEVE_PINK_SLIP_USERS_LIST is not null) and (@NEW_RECIEVE_PINK_SLIP_USERS_LIST<>'')          
 exec Proc_Clm_InsertDiaryEntryForPinkSlipUsers @CLAIM_ID,@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@NEW_RECIEVE_PINK_SLIP_USERS_LIST,@MODIFIED_BY                
                             
           
-- Added by Santosh Kumar Gautam on 25 Nov 2010
 -- @LIMIT_OVERRIDE in CLM_Product_Coverage WOULD BE 'N' if litigation flag on Claim is set to ‘Yes’ or 
  -- Claim is Co-Insurance Claim (Co-Insurance Type is Follower on Policy Main Page), 
  -- update override Limit to ‘Y’ to claim Coverages
   DECLARE @LIMIT_OVERRIDE  CHAR(1)='N'
  
 -----------------------------------------------------------------
 --- ADDED BY SANTOSH KR GAUTAM ON 13 JUL 2011 (REF ITRACK :1044)
 -----------------------------------------------------------------
  DECLARE @COI_LITIGATION_FILE  INT
  
  SELECT @COI_LITIGATION_FILE=LITIGATION_FILE  FROM CLM_CO_INSURANCE WHERE CLAIM_ID=@CLAIM_ID
  
  IF(@LITIGATION_FILE=10963 OR @COI_LITIGATION_FILE=10963 )  --FOR 10963= YES, 10964 = No
     SET @LIMIT_OVERRIDE='Y'
  
 -----------------------------------------------------------------
 --- MODIFIED BY SANTOSH KR GAUTAM ON 19 JUL 2011 (REF ITRACK :1044)
 -----------------------------------------------------------------  
   --IF(@LIMIT_OVERRIDE='N')     
   --BEGIN
   --   -- HERE 14549 MEANS CO_INSURANCE IS FOLLOWER TYPE
   --   SELECT @LIMIT_OVERRIDE= CASE WHEN CO_INSURANCE =14549 THEN 'Y' ELSE 'N' END 
   --   FROM POL_CUSTOMER_POLICY_LIST
   --   WHERE (CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND IS_ACTIVE='Y')  
   --END                            
  
  UPDATE CLM_PRODUCT_COVERAGES
  SET  [LIMIT_OVERRIDE] =CASE WHEN @LIMIT_OVERRIDE='Y' THEN 'Y' ELSE 'N' END,
       [MODIFIED_BY] =@MODIFIED_BY,
       [LAST_UPDATED_DATETIME]=@LAST_UPDATED_DATETIME
  WHERE (CLAIM_ID=@CLAIM_ID AND COVERAGE_CODE_ID NOT  IN (50022,50019,50020,50021,50018,50017) AND IS_ACTIVE='Y')        
  
END   
  
  
  
  
  
  




GO

