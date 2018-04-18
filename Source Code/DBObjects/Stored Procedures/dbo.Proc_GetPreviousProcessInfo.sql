IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPreviousProcessInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPreviousProcessInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Proc_GetPreviousProcessInfo  1338,72,4,35,1,'ALLCOMMIT'    
/*----------------------------------------------------------          
Proc Name          : Dbo.Proc_GetProcessInfo          
Created by         : Pravesh k. Chandel        
Date               : 11/03/2006          
Purpose            : To get Transaction Information  from POL_Policy_PROCESS        
Revison History    :          
Used In            :   Wolverine          
Modified By        :          
Reason             :           
    
Reviewed By : Anurag Verma    
Reviewed On : 06-07-2007    
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------        
drop proc  dbo.Proc_GetPreviousProcessInfo        
Proc_GetPreviousProcessInfo  2885,282,8,0,0,'ALLCOMMIT',2
*/          
CREATE PROC [dbo].[Proc_GetPreviousProcessInfo]          
(        
@CUSTOMER_ID int,        
@POLICY_ID int,        
@POLICY_VERSION_ID smallint,        
@PROCESS_ID int,        
@ROW_ID int,        
@CALLEDFROM  varchar(20)=null  ,  
@LANG_ID INT = 1        
)        
AS          
        
BEGIN         
if  (@CALLEDFROM='ALLCOMMIT')        
begin     
     
 declare @LastCancelVersion int      
 declare @LastRenewVersion int       
 select @LastCancelVersion = isnull(max(policy_version_id),0) from POL_POLICY_PROCESS with(nolock) WHERE         
   CUSTOMER_ID=@CUSTOMER_ID        
   and POLICY_ID=@POLICY_ID and process_id=12      
     
 SELECT @LastRenewVersion= isnull(max(policy_version_id),0) FROM POL_POLICY_PROCESS WITH(NOLOCK)     
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND PROCESS_ID=18   --and isnull(REVERT_BACK,'N')<>'Y'    
 --SELECT @LastRenewVersion
--commented by Lalit April 02,2011
--now current term  endorsemnt can be revert back
 --SELECT @LastRenewVersion= isnull(max(policy_version_id),0) FROM POL_POLICY_PROCESS WITH(NOLOCK)     
 --WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND PROCESS_ID=18   --and isnull(REVERT_BACK,'N')<>'Y'    
 --and policy_version_id < @LastRenewVersion    
     
  SELECT POL.CUSTOMER_ID,        
  POL.POLICY_ID,        
  POL.POLICY_VERSION_ID,       
  PC.POLICY_DISP_VERSION AS POLICY_DISPLAY_VERSION,  
  ROW_ID,        
  POL.PROCESS_ID,        
 -- convert(varchar,CUSTOMER_ID) + '^'+ convert(varchar,POLICY_ID) +'^'+ convert(varchar,POLICY_VERSION_ID) +'^'+ convert(varchar,POL.PROCESS_ID) + '^' + convert(varchar,ROW_ID) as PROCESS_UNIQUE_ID,        
  convert(varchar,POL.CUSTOMER_ID) + '^'+ convert(varchar,POL.POLICY_ID) +'^'+ convert(varchar,NEW_POLICY_VERSION_ID) +'^'+ convert(varchar,POL.PROCESS_ID) + '^' + convert(varchar,ROW_ID)+ '^' + convert(varchar,POL.POLICY_VERSION_ID) as PROCESS_UNIQUE_ID,
        
  ISNULL(PR_MULTI.PROCESS_DESC,PR.PROCESS_DESC) PROCESS_NAME,  
  POL.PROCESS_TYPE,        
  NEW_CUSTOMER_ID,        
  NEW_POLICY_ID,        
  NEW_POLICY_VERSION_ID,        
  POLICY_PREVIOUS_STATUS,        
  POLICY_CURRENT_STATUS,        
  ISNULL(ST_MSTR_MULTI.POLICY_DESCRIPTION,ST_MSTR.POLICY_DESCRIPTION) PROCESS_STATUS,  
--  POL.CREATED_BY,        
  UL.USER_FNAME + ' ' + UL.USER_LNAME as CREATED_BY,        
  POL.CREATED_DATETIME,        
  COMPLETED_BY,        
  CASE WHEN @LANG_ID = 2  
 THEN CONVERT(VARCHAR,COMPLETED_DATETIME,103)  
  ELSE   
 CONVERT(VARCHAR,COMPLETED_DATETIME,101)  
  END  COMPLETED_DATETIME,  
          
  COMMENTS,        
  PRINT_COMMENTS,        
  REQUESTED_BY,      
  CASE WHEN @LANG_ID = 2  
 THEN CONVERT(VARCHAR,EFFECTIVE_DATETIME,103)  
  ELSE   
 CONVERT(VARCHAR,EFFECTIVE_DATETIME,101)  
  END  Effective_Date,  
  EXPIRY_DATE,        
  CANCELLATION_OPTION,        
  CANCELLATION_TYPE,        
  REASON,        
  OTHER_REASON,        
  RETURN_PREMIUM,        
  PAST_DUE_PREMIUM,        
  ENDORSEMENT_NO,        
  PROPERTY_INSPECTION_CREDIT,        
  POL.POLICY_TERMS,        
  NEW_POLICY_TERM_EFFECTIVE_DATE,        
  NEW_POLICY_TERM_EXPIRATION_DATE,        
  RETURN_PREMIUM_AMOUNT,        
  RETURN_MCCA_FEE_AMOUNT,        
  RETURN_OTHER_FEE_AMOUNT,        
  PRINTING_OPTIONS,        
  INSURED,        
  SEND_INSURED_COPY_TO,        
  AUTO_ID_CARD,        
  NO_COPIES,        
  ADD_INT,        
  POL.ADD_INT_ID,        
  SEND_ALL,        
  AGENCY_PRINT,        
  OTHER_RES_DATE_CD,        
  OTHER_RES_DATE,        
  INTERNAL_CHANGE,        
  APPLY_REINSTATE_FEE,        
  ADVERSE_LETTER_REQD,        
  CFD_AMT,        
  DUE_DATE,        
  CANCELLATION_NOTICE_SENT,        
  REVERT_BACK        
        FROM  POL_POLICY_PROCESS POL with(nolock)         
 INNER JOIN POL_PROCESS_MASTER PR ON PR.PROCESS_ID=POL.PROCESS_ID    
 LEFT JOIN POL_PROCESS_MASTER_MULTILINGUAL PR_MULTI ON   
 PR_MULTI.PROCESS_ID=POL.PROCESS_ID   
 AND PR_MULTI.LANG_ID = @LANG_ID        
 LEFT OUTER JOIN POL_POLICY_STATUS_MASTER ST_MSTR  ON  UPPER(ST_MSTR.POLICY_STATUS_CODE)= UPPER(POL.PROCESS_STATUS)  
 LEFT OUTER JOIN POL_POLICY_STATUS_MASTER_MULTILINGUAL ST_MSTR_MULTI  ON  UPPER(ST_MSTR_MULTI.POLICY_STATUS_CODE)= UPPER(POL.PROCESS_STATUS)  
 AND ST_MSTR_MULTI.LANG_ID = @LANG_ID  
   
 left outer Join MNT_USER_LIST UL on UL.USER_ID = POL.CREATED_BY    
 LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST PC  WITH(NOLOCK)  
 ON    
 PC.CUSTOMER_ID = POL.CUSTOMER_ID and   
 PC.POLICY_VERSION_ID = POL.NEW_POLICY_VERSION_ID  
 and PC.POLICY_ID = POL.POLICY_ID  
 WHERE POL.CUSTOMER_ID=@CUSTOMER_ID        
 and POL.POLICY_ID=@POLICY_ID         
 and POL.POLICY_VERSION_ID>@LastCancelVersion        
 and isnull(REVERT_BACK,'N')<>'Y'    
 and POL.POLICY_VERSION_ID > @LastRenewVersion     
-- and NEW_POLICY_VERSION_ID>=    
-- (select ISNULL(max(policy_version_id),0) from pol_customer_policy_list with(nolock)    
--   where CUSTOMER_ID=@CUSTOMER_ID        
--  and POLICY_ID=@POLICY_ID and POLICY_STATUS='NORMAL'    
-- )    
 and POL.process_id in(14)
 --,18,20) --commented by LAlit May 2,2011 .i-track # 1169,
  order by pol.policy_version_id ,completed_datetime       
   
 --select @LANG_ID   
end        
else        
 SELECT         
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
  RETURN_PREMIUM_AMOUNT,        
  RETURN_MCCA_FEE_AMOUNT,        
  RETURN_OTHER_FEE_AMOUNT,        
  PRINTING_OPTIONS,        
  INSURED,        
  SEND_INSURED_COPY_TO,        
  AUTO_ID_CARD,        
  NO_COPIES,        
  ADD_INT,        
  ADD_INT_ID,        
  SEND_ALL,        
  AGENCY_PRINT,        
  OTHER_RES_DATE_CD,        
  OTHER_RES_DATE,        
  INTERNAL_CHANGE,        
  APPLY_REINSTATE_FEE,        
  ADVERSE_LETTER_REQD,        
  CFD_AMT,        
  DUE_DATE,        
  CANCELLATION_NOTICE_SENT,        
  REVERT_BACK         
   FROM  POL_POLICY_PROCESS with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID        
  and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID        
  and ROW_ID=@ROW_ID-1        
 --PROCESS_ID=PROCESS_ID        
        
END          
--GO  
        
--EXEC   Proc_GetPreviousProcessInfo   2885,282,8,0,0,'ALLCOMMIT',2

GO

