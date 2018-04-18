IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_POLICY_PROCESS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_POLICY_PROCESS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                              
Proc Name       : Dbo.Proc_InsertPOL_POLICY_PROCESS                              
Created by      : Vijay Joshi                              
Date            : 12/20/2005                              
Purpose        : Insert record in to POL_POLICY_PROCESS table                              
Revison History :                              
Used In      : Wolverine                              
                            
Modified By  : Vijay Arora                            
Modified Date : 21-12-2005                            
Purpose  : Add the two fields specific for Reinstatement Process.                            
                          
Modified By  : Vijay Arora                            
Modified Date : 23-12-2005                          
Purpose  : Add the one field for Diary Entry List.                          
                        
Modified By  : Pradeep                        
Modified Date : 20-3-2006                        
Purpose  : Added code to update coverage log table        
      
Modified By  : kranti                        
Modified Date : 30-1-2007                        
Purpose  : Added code to enter other Rescission  date                        
    
Modified By  : Pravesh    
Modified Date : 31-Jan-2008                        
Purpose  :  in new business if effictive date is nul then set it to policy effective date    
    
                        
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
--drop proc dbo.Proc_InsertPOL_POLICY_PROCESS                        
CREATE PROC dbo.Proc_InsertPOL_POLICY_PROCESS                              
(                              
 @CUSTOMER_ID       INT,                              
 @POLICY_ID        INT,                              
 @POLICY_VERSION_ID      SMALLINT,                              
 @ROW_ID        INT  OUTPUT,                              
 @PROCESS_ID       INT,                              
 @PROCESS_TYPE       NVARCHAR(40),                              
 @NEW_CUSTOMER_ID      INT,                              
 @NEW_POLICY_ID       INT,                              
 @NEW_POLICY_VERSION_ID  SMALLINT,                              
 @POLICY_PREVIOUS_STATUS NVARCHAR(20),                              
 @POLICY_CURRENT_STATUS  NVARCHAR(20),                              
 @PROCESS_STATUS      NVARCHAR(20),                              
 @CREATED_BY       INT,                              
 @CREATED_DATETIME      DATETIME,                              
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
 @DIARY_LIST_ID INT = null,                    
 @PRINTING_OPTIONS SMALLINT = null,                    
 @INSURED SMALLINT = null,                    
 @SEND_INSURED_COPY_TO SMALLINT = null,                    
 @AUTO_ID_CARD SMALLINT = null,                  
 @NO_COPIES INT = NULL,                
 @STD_LETTER_REQD SMALLINT = NULL,                
 @CUSTOM_LETTER_REQD SMALLINT = NULL,              
 @SEND_ALL SMALLINT = NULL,              
 @ADD_INT SMALLINT = NULL,     
 @ADD_INT_ID VARCHAR(500)=NULL,            
 @AGENCY_PRINT SMALLINT = NULL,      
 @OTHER_RES_DATE DATETIME= null,        
 @OTHER_RES_DATE_CD      CHAR(1)= null,    
 @DUE_DATE Datetime = null   ,    
 @INCLUDE_REASON_DESC    CHar(1)=null,  
 @ENDORSEMENT_TYPE  INT=  NULL,
 @ENDORSEMENT_OPTION  INT = NULL,
 @SOURCE_VERSION_ID  INT = NULL,
 @ENDORSEMENT_RE_ISSUE INT  = NULL,
 @CO_APPLICANT_ID INT = NULL
)                              
AS                              
BEGIN                              
--in case of new business                           
if ( @EFFECTIVE_DATETIME is null and @PROCESS_ID=24)    
 SELECT  @EFFECTIVE_DATETIME = APP_EFFECTIVE_DATE FROM POL_CUSTOMER_POLICY_LIST  with(nolock)    
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@NEW_POLICY_VERSION_ID    
                              
 SELECT @ROW_ID = ISNULL(MAX(ROW_ID),0)+1                               
 FROM POL_POLICY_PROCESS  with(nolock)                             
 WHERE POLICY_ID = @POLICY_ID                              
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID                              
  AND CUSTOMER_ID = @CUSTOMER_ID     
    
 if(@NEW_CUSTOMER_ID=0)     
  set @NEW_CUSTOMER_ID =@CUSTOMER_ID                         
 if (@NEW_POLICY_ID =0)     
 set @NEW_POLICY_ID = @POLICY_ID    
 if (@NEW_POLICY_VERSION_ID =0)     
 set @NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID   
 
     
 INSERT INTO POL_POLICY_PROCESS                              
 (                              
 CUSTOMER_ID,POLICY_ID, POLICY_VERSION_ID, ROW_ID,                              
  PROCESS_ID, PROCESS_TYPE, NEW_CUSTOMER_ID, NEW_POLICY_ID,               
  NEW_POLICY_VERSION_ID, POLICY_PREVIOUS_STATUS, POLICY_CURRENT_STATUS,                              
  PROCESS_STATUS, CREATED_BY, CREATED_DATETIME, COMPLETED_BY,  
  COMPLETED_DATETIME, COMMENTS, PRINT_COMMENTS, REQUESTED_BY,                              
  EFFECTIVE_DATETIME, EXPIRY_DATE, CANCELLATION_OPTION, CANCELLATION_TYPE,                          
  REASON, OTHER_REASON, RETURN_PREMIUM, PAST_DUE_PREMIUM,                              
  ENDORSEMENT_NO, PROPERTY_INSPECTION_CREDIT, POLICY_TERMS, NEW_POLICY_TERM_EFFECTIVE_DATE,                          
  NEW_POLICY_TERM_EXPIRATION_DATE,DIARY_LIST_ID, PRINTING_OPTIONS,INSURED,SEND_INSURED_COPY_TO,AUTO_ID_CARD,NO_COPIES,                 
  STD_LETTER_REQD,CUSTOM_LETTER_REQD,SEND_ALL,ADD_INT,ADD_INT_ID,AGENCY_PRINT ,    
  OTHER_RES_DATE ,OTHER_RES_DATE_CD , DUE_DATE  ,INCLUDE_REASON_DESC    ,  ENDORSEMENT_OPTION
  ,SOURCE_VERSION_ID ,ENDORSEMENT_TYPE   ,ENDORSEMENT_RE_ISSUE,CO_APPLICANT_ID
    
 )                              
 VALUES                              
 (                              
  @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @ROW_ID,                              
  @PROCESS_ID, @PROCESS_TYPE, @NEW_CUSTOMER_ID, @NEW_POLICY_ID,                              
  @NEW_POLICY_VERSION_ID, @POLICY_PREVIOUS_STATUS, @POLICY_CURRENT_STATUS,                            
  @PROCESS_STATUS, @CREATED_BY, @CREATED_DATETIME, @COMPLETED_BY,    
  @COMPLETED_DATETIME, @COMMENTS, @PRINT_COMMENTS, @REQUESTED_BY,                              
  @EFFECTIVE_DATETIME,  @EXPIRY_DATE, @CANCELLATION_OPTION, @CANCELLATION_TYPE,                              
  @REASON, @OTHER_REASON, @RETURN_PREMIUM, @PAST_DUE_PREMIUM,        
  @ENDORSEMENT_NO, @PROPERTY_INSPECTION_CREDIT, @POLICY_TERMS,@NEW_POLICY_TERM_EFFECTIVE_DATE,                            
  @NEW_POLICY_TERM_EXPIRATION_DATE,@DIARY_LIST_ID,@PRINTING_OPTIONS,@INSURED,@SEND_INSURED_COPY_TO,@AUTO_ID_CARD,@NO_COPIES,                
  @STD_LETTER_REQD,@CUSTOM_LETTER_REQD,@SEND_ALL,@ADD_INT,@ADD_INT_ID,@AGENCY_PRINT,    
  @OTHER_RES_DATE, @OTHER_RES_DATE_CD , @DUE_DATE  ,@INCLUDE_REASON_DESC ,@ENDORSEMENT_OPTION ,
  @SOURCE_VERSION_ID,@ENDORSEMENT_TYPE,@ENDORSEMENT_RE_ISSUE,@CO_APPLICANT_ID
 )                           
      
-- Commented By Ravindra  (07-02-2007) while reviewing the SP , obsolete code used for previous implementation of     
-- grand fathered implementation    
/*    
--Insert into coverage log table                        
 EXEC Proc_Insert_COVERAGE_LOG                            
  @CUSTOMER_ID,                 
  @POLICY_ID,                         
  @POLICY_VERSION_ID,                        
  @PROCESS_ID,                      
  @ROW_ID                        
                         
 IF ( @@ERROR <> 0 )      
 BEGIN                        
 RAISERROR                        
 ('Unable to update Coverage log',                        
 16, 1)                        
 END    */                    
                        
END              
    
    
    
    
    
    
    
    
    
    
    

GO

