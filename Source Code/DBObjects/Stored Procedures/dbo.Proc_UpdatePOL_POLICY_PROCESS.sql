IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePOL_POLICY_PROCESS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePOL_POLICY_PROCESS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                  
Modified By  : Lalit   Kr Chauhan        
Modified Date : Jan 19 2011        
Purpose  : update Policy Status on commit in case Out of Sequence Endorsement        
------------------------------------------------------------                                  
Date     Review By          Comments                                  
------   ------------       -------------------------            
drop proc dbo.Proc_UpdatePOL_POLICY_PROCESS            
*/                                  
CREATE PROC [dbo].[Proc_UpdatePOL_POLICY_PROCESS]                                  
(                                  
  @CUSTOMER_ID       INT,                                  
  @POLICY_ID        INT,                                  
  @POLICY_VERSION_ID      SMALLINT,                                  
  @ROW_ID        INT,                                  
  @PROCESS_ID       INT,                                  
  @PROCESS_TYPE       NVARCHAR(40),                                  
  @NEW_CUSTOMER_ID      INT,                                  
  @NEW_POLICY_ID       INT,                                  
  @NEW_POLICY_VERSION_ID  SMALLINT,                                  
  @POLICY_PREVIOUS_STATUS NVARCHAR(20),                                  
  @POLICY_CURRENT_STATUS  NVARCHAR(20),                                  
  @PROCESS_STATUS      NVARCHAR(20),                                  
  @COMPLETED_BY       INT,                                  
  @COMPLETED_DATETIME     DATETIME,                                  
  @COMMENTS        NVARCHAR(1000),                                  
  @PRINT_COMMENTS      CHAR(1),                                  
  @REQUESTED_BY       SMALLINT,                                  
  @EFFECTIVE_DATETIME     DATETIME,                                  
  @EXPIRY_DATE       DATETIME,                                  
  @CANCELLATION_OPTION    INT,                                  
  @CANCELLATION_TYPE      INT,                                
  @REASON        INT,                                  
  @OTHER_REASON       NVARCHAR(500),                                  
  @RETURN_PREMIUM      DECIMAL(13),                                  
  @PAST_DUE_PREMIUM      DECIMAL(13),                                  
  @ENDORSEMENT_NO      INT,                                  
  @PROPERTY_INSPECTION_CREDIT     NCHAR(2),                                  
  @POLICY_TERMS       SMALLINT,                                
  @NEW_POLICY_TERM_EFFECTIVE_DATE DATETIME = null,                                 
  @NEW_POLICY_TERM_EXPIRATION_DATE DATETIME = null,                            
  @PRINTING_OPTIONS SMALLINT = null,                            
  @INSURED SMALLINT = null,                            
  @SEND_INSURED_COPY_TO SMALLINT = null,                            
  @AUTO_ID_CARD SMALLINT = null,                          
  @NO_COPIES INT = NULL,                        
  @STD_LETTER_REQD SMALLINT = NULL,                        
  @CUSTOM_LETTER_REQD SMALLINT = NULL,                 
  @ADVERSE_LETTER_REQD SMALLINT =NULL,                 
  @SEND_ALL SMALLINT = NULL,                      
  @ADD_INT SMALLINT = NULL,                      
  @ADD_INT_ID VARCHAR(500)=NULL,                    
  @AGENCY_PRINT SMALLINT = NULL,              
  @OTHER_RES_DATE DATETIME= null,                  
  @OTHER_RES_DATE_CD      CHAR(1)= null     ,            
  @INTERNAL_CHANGE     NCHAR(2)=null ,            
  @APPLY_REINSTATE_FEE   int =null    ,            
  @SAME_AGENCY  INT =NULL,            
  @ANOTHER_AGENCY         INT=NULL,            
  @DUE_DATE        Datetime = null,            
  @CANCELLATION_NOTICE_SENT  char(1)=null,            
  @REVERT_BACK  char(1)=null,             
  @LAST_REVERT_BACK   varchar(20)=null,            
  @INCLUDE_REASON_DESC    CHar(1)=null ,            
  @CFD_AMT numeric(10,2)=0  ,          
  @COINSURANCE_NUMBER nvarchar(25)=null ,          
  @ENDORSEMENT_TYPE INT=NULL  ,        
  @ENDORSEMENT_OPTION INT = NULL      ,  
  @CO_APPLICANT_ID INT = NULL ,  
  @ENDORSEMENT_RE_ISSUE   INT = NULL  ,
  @SOURCE_VERSION_ID INT = NULL
)                                  
AS                              
BEGIN               
 DECLARE @ENDORSMENT_NO INT,@ENDORSEMENT_DETAIL_ID INT = 0  ,@END_OPTION INT,        
 @END_OPTION_OVERRIDE INT = 14835, @END_OPTION_CARRY_FWD INT = 14834        
         
 --if new business commited and effective datetime is null then set it to policy effective date time            
IF (@PROCESS_ID=25 AND @EFFECTIVE_DATETIME IS NULL )            
 SELECT @EFFECTIVE_DATETIME=APP_EFFECTIVE_DATE FROM  POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                              
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID  AND POLICY_VERSION_ID=@NEW_POLICY_VERSION_ID                                  
            
   
 --Comment Endorsement option functionality  
 --SELECT @END_OPTION = ENDORSEMENT_OPTION FROM  POL_POLICY_PROCESS WITH(NOLOCK)                              
 -- WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND NEW_POLICY_VERSION_ID=@NEW_POLICY_VERSION_ID                
                   
 --       IF(@END_OPTION IN (@END_OPTION_OVERRIDE,@END_OPTION_CARRY_FWD))        
 --       BEGIN         
 --            CREATE TABLE #tempPolicyVersion(POLICY_VERSION int)        
 --           INSERT INTO #tempPolicyVersion SELECT NEW_POLICY_VERSION_ID FROM POL_POLICY_PROCESS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND REVERT_BACK  = @NEW_POLICY_VERSION_ID        
 --  SELECT* FROM #tempPolicyVersion        
 --  UPDATE POL_POLICY_PROCESS SET POLICY_PREVIOUS_STATUS ='MENDORSE' ,POLICY_CURRENT_STATUS = 'MENDORSE' ,PROCESS_ID =14 , PROCESS_STATUS = 'COMPLETE'         
 --  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID IN (SELECT POLICY_VERSION FROM  #tempPolicyVersion)        
           
 --  UPDATE POL_CUSTOMER_POLICY_LIST SET POLICY_STATUS = 'MENDORSE'  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID        
 --  AND POLICY_VERSION_ID IN (SELECT POLICY_VERSION FROM  #tempPolicyVersion)        
           
           
 --  DROP TABLE #tempPolicyVersion        
 --       END        
            
   -- ELSE         
   -- BEGIN            
  UPDATE POL_POLICY_PROCESS         
SET PROCESS_ID      =  @PROCESS_ID,                                  
 PROCESS_TYPE      =  @PROCESS_TYPE,                                
 POLICY_CURRENT_STATUS    =  @POLICY_CURRENT_STATUS,                             
 --PROCESS_STATUS     =  @PROCESS_STATUS,                   
 COMPLETED_BY      =  @COMPLETED_BY,                                  
 COMPLETED_DATETIME    =  @COMPLETED_DATETIME,                                  
 COMMENTS       =  @COMMENTS,                                  
 PRINT_COMMENTS     =  @PRINT_COMMENTS,                                  
 REQUESTED_BY      =  @REQUESTED_BY,                                  
 EFFECTIVE_DATETIME    =  @EFFECTIVE_DATETIME,                                  
 EXPIRY_DATE      =  @EXPIRY_DATE,                                  
 CANCELLATION_OPTION    =  @CANCELLATION_OPTION,                                  
 CANCELLATION_TYPE     =  @CANCELLATION_TYPE,                                 
 REASON  =  @REASON,                                  
 OTHER_REASON      =  @OTHER_REASON,                                  
 RETURN_PREMIUM     =  @RETURN_PREMIUM,                                  
 PAST_DUE_PREMIUM     =  @PAST_DUE_PREMIUM,                                  
 PROPERTY_INSPECTION_CREDIT =  @PROPERTY_INSPECTION_CREDIT,                                  
 POLICY_TERMS  =  @POLICY_TERMS,                                
 NEW_POLICY_TERM_EFFECTIVE_DATE =  @NEW_POLICY_TERM_EFFECTIVE_DATE,                                
 NEW_POLICY_TERM_EXPIRATION_DATE = @NEW_POLICY_TERM_EXPIRATION_DATE,                            
 PRINTING_OPTIONS  = @PRINTING_OPTIONS,                            
 INSURED = @INSURED,                            
 SEND_INSURED_COPY_TO = @SEND_INSURED_COPY_TO,                            
 AUTO_ID_CARD = @AUTO_ID_CARD,                        
 NO_COPIES = @NO_COPIES,                        
 STD_LETTER_REQD = @STD_LETTER_REQD,                        
 CUSTOM_LETTER_REQD = @CUSTOM_LETTER_REQD,                    
ADVERSE_LETTER_REQD =@ADVERSE_LETTER_REQD,              
 SEND_ALL = @SEND_ALL,                      
 ADD_INT = @ADD_INT,                      
 ADD_INT_ID = @ADD_INT_ID,                    
 AGENCY_PRINT = @AGENCY_PRINT,              
 OTHER_RES_DATE = @OTHER_RES_DATE,              
 OTHER_RES_DATE_CD = @OTHER_RES_DATE_CD   ,            
 INTERNAL_CHANGE=@INTERNAL_CHANGE  ,            
 APPLY_REINSTATE_FEE=@APPLY_REINSTATE_FEE,            
SAME_AGENCY=@SAME_AGENCY,            
ANOTHER_AGENCY =@ANOTHER_AGENCY    ,            
 DUE_DATE = @DUE_DATE ,            
 REVERT_BACK  = @REVERT_BACK,              
 LAST_REVERT_BACK    = @LAST_REVERT_BACK   ,            
INCLUDE_REASON_DESC =@INCLUDE_REASON_DESC,            
CFD_AMT=@CFD_AMT ,          
 COINSURANCE_NUMBER =@COINSURANCE_NUMBER,           
 ENDORSEMENT_TYPE=@ENDORSEMENT_TYPE  ,         
    ENDORSEMENT_OPTION = @ENDORSEMENT_OPTION    ,    
    ENDORSEMENT_NO  =  --Added By Lalit For insert endorsemnt no on commit(Undo endorsement and cancel)    
    CASE WHEN @PROCESS_ID = 37 OR @PROCESS_ID = 12    
    THEN @ENDORSEMENT_NO ELSE ENDORSEMENT_NO END  ,  
    CO_APPLICANT_ID = @CO_APPLICANT_ID --Added By Lalit April 11,2011.i-track # for master polliy implimentationn  
     ,ENDORSEMENT_RE_ISSUE = @ENDORSEMENT_RE_ISSUE  ,
     SOURCE_VERSION_ID = @SOURCE_VERSION_ID
      
WHERE                                   
 CUSTOMER_ID     =  @CUSTOMER_ID                                  
 AND POLICY_ID       =  @POLICY_ID                                  
 AND POLICY_VERSION_ID     =  @POLICY_VERSION_ID                                  
 AND ROW_ID       =  @ROW_ID                             
    --END        
--Added By Lalit For Activate/deactivate Risk For Master policy Case and maintain Endorsment log  ,oct 28,2010           
 IF(@PROCESS_ID =3 OR @PROCESS_ID =14 )          
 BEGIN          
   Exec Proc_ActivateDeactivate_MasterPolicyRisk @CUSTOMER_ID,@POLICY_ID,@NEW_POLICY_VERSION_ID,@EFFECTIVE_DATETIME,@EXPIRY_DATE,null,null          
 END          
 /* Commented By Lalit On Nov 11,2010 for maintain Endorsment Transaction Log on Endorsement Commit          
IF(@PROCESS_ID =14 )--OR @PROCESS_ID =3          
 BEGIN          
   SELECT @ENDORSMENT_NO = ENDORSEMENT_NO FROM POL_POLICY_ENDORSEMENTS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID          
     AND POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID           
              
    IF NOT EXISTS (SELECT @ENDORSMENT_NO FROM POL_POLICY_ENDORSEMENTS_DETAILS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID           
     AND ENDORSEMENT_NO = @ENDORSMENT_NO)           
     BEGIN          
    EXEC Proc_InsertPOL_POLICY_ENDORSEMENTS_DETAILS @POLICY_ID,@NEW_POLICY_VERSION_ID,@CUSTOMER_ID,          
          @ENDORSMENT_NO,@ENDORSEMENT_DETAIL_ID,@EFFECTIVE_DATETIME,          
          @ENDORSEMENT_TYPE,'Complete','Endorsement complete',@COMPLETED_BY,@COMPLETED_DATETIME,0          
               
   END          
  ELSE          
   BEGIN          
       SELECT  @ENDORSEMENT_DETAIL_ID = ENDORSEMENT_DETAIL_ID FROM POL_POLICY_ENDORSEMENTS_DETAILS WHERE           
     CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID           
     AND ENDORSEMENT_NO = @ENDORSMENT_NO          
                 
    EXEC Proc_UpdatePOL_POLICY_ENDORSEMENTS_DETAILS @POLICY_ID,@NEW_POLICY_VERSION_ID,          
         @CUSTOMER_ID,@ENDORSEMENT_NO,@ENDORSEMENT_DETAIL_ID,@EFFECTIVE_DATETIME          
         ,@ENDORSEMENT_TYPE,'',''          
              
   END          
            
            
     END          
 --    */          
 --IF(@PROCESS_ID = 12) --If Commit Cancellation insert in POL_POLICY_ENDORSEMENTS and POL_POLICY_ENDORSEMENTS_DETAILS          
 --BEGIN          
 --  EXEC Proc_InsertPOL_POLICY_ENDORSEMENTS @POLICY_ID , @NEW_POLICY_VERSION_ID           
 --       ,@CUSTOMER_ID ,  @ENDORSMENT_NO OUT, @EFFECTIVE_DATETIME,@COMPLETED_BY,@COMPLETED_DATETIME,@PROCESS_ID,'COM'          
                  
             
 -- IF(@ENDORSMENT_NO <> 0)          
 --  EXEC Proc_InsertPOL_POLICY_ENDORSEMENTS_DETAILS @POLICY_ID,@NEW_POLICY_VERSION_ID,@CUSTOMER_ID,          
 --         @ENDORSMENT_NO,@ENDORSEMENT_DETAIL_ID,@EFFECTIVE_DATETIME,          
 --         @PROCESS_ID,'Commit Cancellation','Commit Cancellation',@COMPLETED_BY,@COMPLETED_DATETIME,0               
                  
 --END          
           
               
-- if process is Endorsement/REINSTATEMENT/Cancellation Commit/Non Renew Commit then update POL_CUSTOMER_POLICY_LIST                  
--20          Commit Non-Renewal Process                       
--6           Non-Renew            
IF(@PROCESS_ID=3 or @PROCESS_ID=14 OR @PROCESS_ID=4 OR @PROCESS_ID=16 OR @PROCESS_ID=12 or @PROCESS_ID=6 OR @PROCESS_ID=20)                              
BEGIN                               
UPDATE POL_CUSTOMER_POLICY_LIST                              
 SET POL_VER_EFFECTIVE_DATE = @EFFECTIVE_DATETIME,                      POL_VER_EXPIRATION_DATE = isnull(@EXPIRY_DATE, APP_EXPIRATION_DATE)                             
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID  AND POLICY_VERSION_ID=@NEW_POLICY_VERSION_ID                                  
          
END                              
          
  IF( @PROCESS_ID =14 )          
 BEGIN          
   Exec Proc_UpdateTransactionIdCoinsurance @CUSTOMER_ID,@POLICY_ID,@NEW_POLICY_VERSION_ID          
 END          
          
          
          
--Commented by Ravindra(08-05-2008) : Logic need to be moved to form level            
 --added by pravesh on 10 july 2008                                
--if (@PROCESS_ID=2 OR @PROCESS_ID=12 )            
--begin             
--exec Proc_POL_POLICY_PROCESS_DUE_DATE @CUSTOMER_ID,@POLICY_ID,@NEW_POLICY_VERSION_ID,@ROW_ID            
--end         
                           
END 

GO

