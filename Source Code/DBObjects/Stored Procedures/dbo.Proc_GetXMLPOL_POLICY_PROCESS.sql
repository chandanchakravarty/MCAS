IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLPOL_POLICY_PROCESS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLPOL_POLICY_PROCESS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                  
Proc Name       : Dbo.Proc_GetPOL_POLICY_PROCESS_XML                                  
Created by      : Vijay Joshi                                  
Date            : 12/20/2005                                  
Purpose        : To retreive the process information                                   
Revison History :                                  
Used In      : Wolverine                        
                    
Modified By  : kranti                                      
Modified Date : 30-1-2007                                      
Purpose  : Added code to enter other Rescission  date                      
            
Reviewed By : Anurag Verma            
Reviewed On : 25-06-2007                              
------------------------------------------------------------                                  
Date     Review By          Comments                                  
------   ------------       -------------------------                  
drop proc Proc_GetXMLPOL_POLICY_PROCESS   28323,10,2,1                
*/                                  
                  
CREATE PROC [dbo].[Proc_GetXMLPOL_POLICY_PROCESS]                    
(                                  
  @CUSTOMER_ID      INT,                                  
  @POLICY_ID   INT,                                  
  @POLICY_VERSION_ID INT,                                  
  @ROW_ID    INT                                  
)                                  
AS                                  
BEGIN                                  
 SELECT                                   
-- convert(varchar,PROCESS.CUSTOMER_ID) + '^' +  convert(varchar,PROCESS.POLICY_ID)+ '^' +  convert(varchar,PROCESS.POLICY_VERSION_ID) + '^' +  convert(varchar,PROCESS.PROCESS_ID) + '^' +  convert(varchar, ROW_ID) as LAST_REVERT_BACK_ID,                  
  
    
        
              
 convert(varchar,PROCESS.CUSTOMER_ID) + '^' +    
 convert(varchar,PROCESS.POLICY_ID)+ '^' +  convert(varchar,PROCESS.NEW_POLICY_VERSION_ID) + '^' +    
 convert(varchar,PROCESS.PROCESS_ID) + '^' +  convert(varchar, ROW_ID) + '^' +    
 convert(varchar,PROCESS.POLICY_VERSION_ID) as LAST_REVERT_BACK_ID,                                
            
  PROCESS.CUSTOMER_ID, PROCESS.POLICY_ID, PROCESS.POLICY_VERSION_ID, ROW_ID,                                
  PROCESS_ID, PROCESS_TYPE, NEW_CUSTOMER_ID, NEW_POLICY_ID,                                
  NEW_POLICY_VERSION_ID, POLICY_PREVIOUS_STATUS, POLICY_CURRENT_STATUS,                                
  PROCESS.PROCESS_STATUS, COMPLETED_BY, COMPLETED_DATETIME, COMMENTS, PRINT_COMMENTS,                                
  REQUESTED_BY,COINSURANCE_NUMBER,ENDORSEMENT_TYPE,                
  CONVERT(VARCHAR(20), EFFECTIVE_DATETIME, 101) EFFECTIVE_DATETIME,                                  
  EFFECTIVE_DATETIME AS EFFECTIVETIME,          
  dbo.FormatDateTime (EFFECTIVE_DATETIME, 'HH:MM:SS 12') as EFFECTIVE_TIME,               
  --convert(varchar,EFFECTIVE_DATETIME,108) as EFFECTIVE_TIME,                
      
  case when (charindex('PM', convert(varchar,cast(EFFECTIVE_DATETIME as datetime),100))>0) then '1' else '0' end as MERIDIEM , --Added by Charles on 31-Aug-09 for Itrack 6323       
                               
  CONVERT(VARCHAR(20), EXPIRY_DATE, 101) EXPIRY_DATE, CANCELLATION_OPTION, CANCELLATION_TYPE,                                
  REASON, OTHER_REASON, RETURN_PREMIUM, PAST_DUE_PREMIUM, ENDORSEMENT_NO, PROPERTY_INSPECTION_CREDIT,                                
  POLICY.APP_TERMS POLICY_TERMS, CONVERT(VARCHAR(20), NEW_POLICY_TERM_EFFECTIVE_DATE, 101) NEW_POLICY_TERM_EFFECTIVE_DATE,                                
  CONVERT(VARCHAR(20), NEW_POLICY_TERM_EXPIRATION_DATE, 101) NEW_POLICY_TERM_EXPIRATION_DATE,                              
  PRINTING_OPTIONS,INSURED,SEND_INSURED_COPY_TO,AUTO_ID_CARD,NO_COPIES,STD_LETTER_REQD,CUSTOM_LETTER_REQD, ADVERSE_LETTER_REQD,                       
  SEND_ALL,PROCESS.ADD_INT,PROCESS.ADD_INT_ID,AGENCY_PRINT ,convert(varchar,OTHER_RES_DATE,101) as OTHER_RES_DATE ,OTHER_RES_DATE_CD, PROCESS.INTERNAL_CHANGE,                  
  ISNULL(POLICY.UNDERWRITER,0) AS UNDERWRITER , ISNULL(POLICY.APP_TERMS,0) AS APP_TERMS,        
  APPLY_REINSTATE_FEE  ,                
  LAST_REVERT_BACK,                
  INCLUDE_REASON_DESC    ,    
  ENDORSEMENT_OPTION  ,  
  CO_APPLICANT_ID, --Added By Lalit April 11 ,2011          
  ENDORSEMENT_RE_ISSUE ,--Added By Lalit May 1 ,2011      -endorsement re-issue
  SOURCE_VERSION_ID      --Added By Lalit May 1 ,2011             -endorsement re-issue
 FROM                                    
   POL_POLICY_PROCESS PROCESS (NOLOCK)           
 LEFT OUTER JOIN                    
 POL_CUSTOMER_POLICY_LIST POLICY (NOLOCK)                    
 ON                    
 PROCESS.CUSTOMER_ID = POLICY.CUSTOMER_ID AND            
 PROCESS.POLICY_ID = POLICY.POLICY_ID AND                    
-- PROCESS.POLICY_VERSION_ID = POLICY.POLICY_VERSION_ID AND                     
 PROCESS.NEW_POLICY_VERSION_ID = POLICY.POLICY_VERSION_ID                 
--AND  PROCESS.POLICY_CURRENT_STATUS = POLICY.POLICY_STATUS                    
 WHERE                                    
   PROCESS.CUSTOMER_ID = @CUSTOMER_ID                                  
  AND PROCESS.POLICY_ID = @POLICY_ID                                  
  AND PROCESS.POLICY_VERSION_ID = @POLICY_VERSION_ID                                  
  AND ROW_ID = @ROW_ID                          
END 
GO

