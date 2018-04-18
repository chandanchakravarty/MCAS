IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillPreviousProcessInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillPreviousProcessInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------      
Proc Name          : Proc_FillPreviousProcessInfo  
Created by         : Pravesh k. Chandel    
Date               : 11/03/2006      
Purpose            : To get Transaction Information  from POL_Policy_PROCESS    
Revison History    :      
Used In            :   Wolverine      
Modified By        :      
Reason             :       

Reviewed By	:	Anurag Verma
Reviewed On	:	06-07-2007
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------    
drop proc  dbo.Proc_FillPreviousProcessInfo    
  
*/      
CREATE PROC dbo.Proc_FillPreviousProcessInfo      
(    
@CUSTOMER_ID int,    
@POLICY_ID int,    
@POLICY_VERSION_ID smallint,    
@PROCESS_ID int,    
@ROW_ID int    
)    
AS      
    
BEGIN     
  
  
 declare @LastCancelVersion int 
 declare @LastRenewVersion int   
   
 select @LastCancelVersion = isnull(max(policy_version_id),0) from POL_POLICY_PROCESS with(nolock) WHERE     
   CUSTOMER_ID=@CUSTOMER_ID    
   and POLICY_ID=@POLICY_ID and process_id=12   

 
 SELECT @LastRenewVersion= isnull(max(policy_version_id),0) FROM POL_POLICY_PROCESS WITH(NOLOCK) 
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND PROCESS_ID=18   --and isnull(REVERT_BACK,'N')<>'Y'
 
 SELECT @LastRenewVersion= isnull(max(policy_version_id),0) FROM POL_POLICY_PROCESS WITH(NOLOCK) 
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND PROCESS_ID=18   --and isnull(REVERT_BACK,'N')<>'Y'
	and policy_version_id < @LastRenewVersion
  	
 
    
 SELECT CUSTOMER_ID,    
  POLICY_ID,    
  POLICY_VERSION_ID,    
  ROW_ID,    
  POL.PROCESS_ID,    
  convert(varchar,CUSTOMER_ID) + '^'+ convert(varchar,POLICY_ID) +'^'+ convert(varchar,NEW_POLICY_VERSION_ID) +'^'+ convert(varchar,POL.PROCESS_ID) + '^' + convert(varchar,ROW_ID) +'^'+ convert(varchar,POLICY_VERSION_ID) as PROCESS_UNIQUE_ID,    
 -- PR.PROCESS_DESC PROCESS_NAME,     
  POL.PROCESS_TYPE,    
  NEW_CUSTOMER_ID,    
  NEW_POLICY_ID,    
  NEW_POLICY_VERSION_ID,    
  POLICY_PREVIOUS_STATUS,    
  POLICY_CURRENT_STATUS,    
  PROCESS_STATUS,    
--  POL.CREATED_BY,    
 -- UL.USER_FNAME + ' ' + UL.USER_LNAME as CREATED_BY,    
  POL.CREATED_DATETIME,    
  COMPLETED_BY,    
  COMPLETED_DATETIME,    
  COMMENTS,    
  PRINT_COMMENTS,    
  REQUESTED_BY,    
  EFFECTIVE_DATETIME Effective_Date,    
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
        FROM  POL_POLICY_PROCESS POL with(nolock)     
-- INNER JOIN POL_PROCESS_MASTER PR ON PR.PROCESS_ID=POL.PROCESS_ID     
 --left outer Join MNT_USER_LIST UL on UL.USER_ID = POL.CREATED_BY    
 WHERE CUSTOMER_ID=@CUSTOMER_ID    
 and POLICY_ID=@POLICY_ID     
 and POLICY_VERSION_ID>@LastCancelVersion  
 and POLICY_VERSION_ID>@LastRenewVersion  
 and isnull(REVERT_BACK,'N')<>'Y'  
 and POL.process_id in(14,18,20) order by policy_version_id ,completed_datetime    
    
END      
      
    
    
    
    
    
    
    
    
    
    
    
  
  
  







GO

