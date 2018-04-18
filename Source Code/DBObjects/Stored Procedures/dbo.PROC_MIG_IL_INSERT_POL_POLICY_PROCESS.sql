/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POL_POLICY_PROCESS]    Script Date: 12/02/2011 15:59:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_POL_POLICY_PROCESS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POL_POLICY_PROCESS]
GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POL_POLICY_PROCESS]    Script Date: 12/02/2011 15:59:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
/*----------------------------------------------------------                                  
Proc Name       : Dbo.Proc_MIG_IL_InsertPOL_POLICY_PROCESS                                  
Created by      : Lalit Chauhan   
Date            : Oct 4, 2011  
Purpose   : Insert record in to POL_POLICY_PROCESS table                                  
*/  
                          
CREATE PROC [dbo].[PROC_MIG_IL_INSERT_POL_POLICY_PROCESS]  
(                                  
 @CUSTOMER_ID       INT,                                  
 @POLICY_ID        INT,                                  
 @POLICY_VERSION_ID      SMALLINT,                                  
--- @ROW_ID        INT  OUTPUT,                                  
 @PROCESS_ID       INT,                                  
 --@PROCESS_TYPE       NVARCHAR(40),                                  
 --@NEW_CUSTOMER_ID      INT,                                  
 --@NEW_POLICY_ID       INT,                                  
 --@NEW_POLICY_VERSION_ID  SMALLINT,                                  
 --@POLICY_PREVIOUS_STATUS NVARCHAR(20),                                  
 --@POLICY_CURRENT_STATUS  NVARCHAR(20),                                  
 --@PROCESS_STATUS      NVARCHAR(20),                                  
 @CREATED_BY       INT,                                  
 --@CREATED_DATETIME      DATETIME,                                  
 --@COMPLETED_BY       INT,                                  
 @COMPLETED_DATETIME     DATETIME,                                  
 --@COMMENTS        NVARCHAR(1000),                                  
 --@PRINT_COMMENTS      CHAR(1),                                  
 --@REQUESTED_BY       SMALLINT,                                  
 @EFFECTIVE_DATETIME     DATETIME,                                  
 @EXPIRY_DATE       DATETIME,  
 --@CANCELLATION_OPTION    INT,                                  
 --@CANCELLATION_TYPE      INT,                          
 --@REASON        INT,                                  
 --@OTHER_REASON       NVARCHAR(500),                                  
 --@RETURN_PREMIUM      DECIMAL(13),                                  
 --@PAST_DUE_PREMIUM      DECIMAL(13),                                  
 @ENDORSEMENT_NO      INT,                                  
 --@PROPERTY_INSPECTION_CREDIT     NCHAR(2),                                  
 --@POLICY_TERMS       SMALLINT,                                
 --@NEW_POLICY_TERM_EFFECTIVE_DATE DATETIME = null,                                
 --@NEW_POLICY_TERM_EXPIRATION_DATE DATETIME = null,                              
 --@DIARY_LIST_ID INT = null,                        
 --@PRINTING_OPTIONS SMALLINT = null,                        
 --@INSURED SMALLINT = null,                        
 --@SEND_INSURED_COPY_TO SMALLINT = null,                        
 --@AUTO_ID_CARD SMALLINT = null,                      
 --@NO_COPIES INT = NULL,                    
 --@STD_LETTER_REQD SMALLINT = NULL,                    
 --@CUSTOM_LETTER_REQD SMALLINT = NULL,                  
 --@SEND_ALL SMALLINT = NULL,                  
 --@ADD_INT SMALLINT = NULL,         
 --@ADD_INT_ID VARCHAR(500)=NULL,                
 --@AGENCY_PRINT SMALLINT = NULL,          
 --@OTHER_RES_DATE DATETIME= null,            
 --@OTHER_RES_DATE_CD      CHAR(1)= null,        
 --@DUE_DATE Datetime = null   ,        
 --@INCLUDE_REASON_DESC    CHar(1)=null,      
 @ENDORSEMENT_TYPE  INT=  NULL,    
 --@ENDORSEMENT_OPTION  INT = NULL,    
 --@SOURCE_VERSION_ID  INT = NULL,    
 --@ENDORSEMENT_RE_ISSUE INT  = NULL,    
 @CO_APPLICANT_ID INT = NULL    
)                                  
AS                                  
BEGIN       
  
--internal   
DECLARE @ROW_ID  INT  
DECLARE @POLICY_PREVIOUS_STATUS NVARCHAR(50)  
--in case of new business                               
if ( @EFFECTIVE_DATETIME is null and @PROCESS_ID=24)        
 SELECT  @EFFECTIVE_DATETIME = APP_EFFECTIVE_DATE FROM POL_CUSTOMER_POLICY_LIST  with(nolock)        
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID        
                      
IF (@PROCESS_ID in (24,25))  
SET @POLICY_PREVIOUS_STATUS =  'UISSUE'  
  
                      
                                  
 SELECT @ROW_ID = ISNULL(MAX(ROW_ID),0)+1                                   
 FROM POL_POLICY_PROCESS  with(nolock)                                 
 WHERE POLICY_ID = @POLICY_ID                                  
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID                                  
  AND CUSTOMER_ID = @CUSTOMER_ID         
        
 /*if(@CUSTOMER_ID=0)         
  set @CUSTOMER_ID =@CUSTOMER_ID                             
 if (@POLICY_ID =0)         
 set @POLICY_ID = @POLICY_ID        
 if (@POLICY_VERSION_ID =0)         
 set @POLICY_VERSION_ID = @POLICY_VERSION_ID       
   */  
         
 INSERT INTO POL_POLICY_PROCESS                                  
 (                                  
 CUSTOMER_ID,  
 POLICY_ID,   
 POLICY_VERSION_ID,   
 ROW_ID,                                  
 PROCESS_ID,   
 PROCESS_TYPE,   
 NEW_CUSTOMER_ID,   
 NEW_POLICY_ID,                   
 NEW_POLICY_VERSION_ID,   
 POLICY_PREVIOUS_STATUS,   
 POLICY_CURRENT_STATUS,                                  
 PROCESS_STATUS,   
 CREATED_BY,   
 CREATED_DATETIME,   
 COMPLETED_BY,      
 COMPLETED_DATETIME,   
 COMMENTS,   
 PRINT_COMMENTS,   
 REQUESTED_BY,                                  
 EFFECTIVE_DATETIME,   
 EXPIRY_DATE,   
 CANCELLATION_OPTION,   
 CANCELLATION_TYPE,                              
 REASON,   
 OTHER_REASON,   
 RETURN_PREMIUM,   
 PAST_DUE_PREMIUM,                                  
 ENDORSEMENT_NO,   
 PROPERTY_INSPECTION_CREDIT,   
 POLICY_TERMS,   
 NEW_POLICY_TERM_EFFECTIVE_DATE,                              
 NEW_POLICY_TERM_EXPIRATION_DATE,  
 DIARY_LIST_ID,   
 PRINTING_OPTIONS,  
 INSURED,  
 SEND_INSURED_COPY_TO,  
 AUTO_ID_CARD,  
 NO_COPIES,                     
 STD_LETTER_REQD,  
 CUSTOM_LETTER_REQD,  
 SEND_ALL,  
 ADD_INT,  
 ADD_INT_ID,  
 AGENCY_PRINT ,        
 OTHER_RES_DATE ,  
 OTHER_RES_DATE_CD ,   
 DUE_DATE  ,  
 INCLUDE_REASON_DESC    ,    
 ENDORSEMENT_OPTION  ,  
 SOURCE_VERSION_ID ,  
 ENDORSEMENT_TYPE   ,  
 ENDORSEMENT_RE_ISSUE,  
 CO_APPLICANT_ID    
        
 )                                  
 VALUES                                  
 (                                  
  @CUSTOMER_ID,  
  @POLICY_ID,  
  @POLICY_VERSION_ID,  
  @ROW_ID,  
  @PROCESS_ID,  
  '',  
  @CUSTOMER_ID,  
  @POLICY_ID,  
  @POLICY_VERSION_ID,  
  @POLICY_PREVIOUS_STATUS,  
  'NORMAL',  
  'COMPLETE',  
  @CREATED_BY,  
  GETDATE(),  
  @CREATED_BY,  
  @COMPLETED_DATETIME,  
  '',  
  '',  
  0,  
  @EFFECTIVE_DATETIME,  
  @EXPIRY_DATE,  
  0,  
  0,  
  0,  
  '',  
  0,  
  0,  
  @ENDORSEMENT_NO,  
  '',  
  0,  
  null,  
  null,  
  null,  
  null,  
  null,  
  null,  
  0,  
  11981,  
  11983,  
  0,  
  0,  
  0,  
  0,  
  11981,  
  '',  
  0,  
  11984,  
  '',  
  null,  
  0,  
  0,  
  0,  
  @CO_APPLICANT_ID  
  
 )                               
           
END                  
       
GO
