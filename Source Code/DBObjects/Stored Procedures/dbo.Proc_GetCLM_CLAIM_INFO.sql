IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_CLAIM_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_CLAIM_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                
Proc Name       : dbo.Proc_GetCLM_CLAIM_INFO                                                          
Created by      : Sumit Chhabra                                                              
Date            : 27/04/2006                                                                
Purpose         : Fetch data from CLM_CLAIM_INFO table for claim notification screen                                            
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
--DROP PROC dbo.Proc_GetCLM_CLAIM_INFO   841                                                                              
CREATE PROC [dbo].[Proc_GetCLM_CLAIM_INFO]                                                                                   
 @CLAIM_ID int                                          
AS                                                                
BEGIN                                                                
  
DECLARE @DIARY_DATE DATETIME  
  
SELECT    
 CUSTOMER_ID,  
 POLICY_ID,  
 POLICY_VERSION_ID,                                               
 CLAIM_NUMBER,                                      
 CONVERT(CHAR,LOSS_DATE,101) LOSS_DATE,                                              
 LOSS_DATE AS LOSS_TIME,  
 -- Done for Itrack Issue 6823 on 10 Dec 09   
 -- MUL.ADJUSTER_CODE                                              
 CA.ADJUSTER_CODE,      
 CCI.ADJUSTER_ID,                                            
 REPORTED_BY,                                            
 CATASTROPHE_EVENT_CODE,                                            
 --  CLAIMANT_INSURED,                                            
 INSURED_RELATIONSHIP,                                            
 CLAIMANT_NAME,                                            
 CCI.COUNTRY,                                            
 ZIP,                                            
 ADDRESS1,                                            
 ADDRESS2,                                            
 CITY,                                            
 HOME_PHONE,                                            
 WORK_PHONE,                                            
 MOBILE_PHONE,                                            
 WHERE_CONTACT,                                            
 WHEN_CONTACT,                                            
 CONVERT(CHAR,DIARY_DATE,101)   DIARY_DATE,                                                
 CLAIM_STATUS,                                            
 OUTSTANDING_RESERVE,                                            
 RESINSURANCE_RESERVE,                                            
 PAID_LOSS,                                            
 PAID_EXPENSE,                                            
 RECOVERIES,                                            
 CLAIM_DESCRIPTION,                                            
 CCI.IS_ACTIVE,                                      
 -- SUB_ADJUSTER,                                      
 -- SUB_ADJUSTER_CONTACT,                                      
 EXTENSION,                               
 ISNULL(DUMMY_POLICY_ID,0) AS DUMMY_POLICY_ID,                            
 LOSS_TIME_AM_PM,         
 LITIGATION_FILE,                      
 RECOVERY,                      
 ISNULL(RECOVERY_OUTSTANDING,0) AS RECOVERY_OUTSTANDING,                
 STATE,                
 CLAIMANT_PARTY,              
 LINKED_TO_CLAIM,              
 -- ADD_FAULT,              
 -- TOTAL_LOSS,              
 NOTIFY_REINSURER,            
 REPORTED_TO,            
 CONVERT(VARCHAR(10),FIRST_NOTICE_OF_LOSS,101) AS  FIRST_NOTICE_OF_LOSS,        
 RECIEVE_PINK_SLIP_USERS_LIST,        
 ISNULL(PINK_SLIP_TYPE_LIST,'') AS PINK_SLIP_TYPE_LIST,  
 CLAIM_STATUS_UNDER,  
 AT_FAULT_INDICATOR, --Done for Itrack Issue 6620 on 27 Nov 09  
 CCI.LOB_ID,--Added for Itrack Issue 6932 on 3 June 2010  
 CCI.OFFCIAL_CLAIM_NUMBER,
 CLAIM_CURRENCY_ID,-- Added by santosh kumar gautam on 15 dec 2010
  CONVERT(VARCHAR(10),LAST_DOC_RECEIVE_DATE,101) AS  LAST_DOC_RECEIVE_DATE ,-- Added by santosh kumar gautam on 17 Jan 2011
  REINSURANCE_TYPE,-- Added by santosh kumar gautam on 08 Feb 2011
  REIN_CLAIM_NUMBER,-- Added by santosh kumar gautam on 08 Feb 2011
  REIN_LOSS_NOTICE_NUM,-- Added by santosh kumar gautam on 08 Feb 2011
  IS_VICTIM_CLAIM,-- Added by santosh kumar gautam on 08 Feb 2011
  CONVERT(VARCHAR(10),POSSIBLE_PAYMENT_DATE,101) AS  POSSIBLE_PAYMENT_DATE,-- Added by santosh kumar gautam on 17 Jan 2011
   -- Added by santosh kumar gautam on 15 Feb 2011

  CCI.CO_INSURANCE_TYPE AS CO_INSURANCE_TYPE  --Added by Sneha for iTrack-1238 on 05-08-2011
FROM                                       
  CLM_CLAIM_INFO CCI LEFT OUTER JOIN CLM_ADJUSTER CA ON CCI.ADJUSTER_ID= CA.ADJUSTER_ID left outer JOIN MNT_USER_LIST MUL ON CA.USER_ID = MUL.USER_ID  
 WHERE   CLAIM_ID = @CLAIM_ID                                            
          
 EXEC PROC_GETCLM_LINKED_CLAIMS @CLAIM_ID    
  
SELECT @DIARY_DATE=DIARY_DATE FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID  
  
SELECT TOP 1 LISTID,TOUSERID FROM TODOLIST WHERE CLAIMID=@CLAIM_ID AND MODULE_ID=5 AND FOLLOWUPDATE=@DIARY_DATE  
 
--Added by Santosh Kumar Gautam Nov 2010
-- HAS_LITIGATION is used to disable or enable litigation dropdown   
SELECT CASE WHEN COUNT(LITIGATION_ID)>0 THEN '1' ELSE '0' END AS HAS_LITIGATION FROM CLM_LITIGATION_INFORMATION WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y' 
     
--Added by Santosh Kumar Gautam 10 Dec 2010
  
 SELECT TOP 1 ACTIVITY_ID            
    FROM  CLM_ACTIVITY  WITH(NOLOCK)                        
    WHERE ( ACTIVITY_STATUS=11801 -- COMPLETED ACTIVITY                        
   AND CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y'             
     )                        
    ORDER BY ACTIVITY_ID DESC    
     
END  


GO

