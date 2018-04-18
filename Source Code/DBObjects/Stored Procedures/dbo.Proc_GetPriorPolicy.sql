IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPriorPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPriorPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------      
Proc Name       : dbo.GetPriorPolicy      
Created by      : Vijay      
Date            : 4/26/2005      
Purpose     : Evaluation      
Revison History :      
Used In  : Wolverine      
      
Modified By : Anurag Verma      
Modified On : 4/5/2005       
Purpose : removing app_id and app_version_id from query      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--DROP PROC Dbo.Proc_GetPriorPolicy      
CREATE PROC [dbo].[Proc_GetPriorPolicy]      
(      
 @CUSTOMER_ID       int,      
 @APP_PRIOR_CARRIER_INFO_ID smallint,    
 @LANG_ID int=null  
)      
AS      
BEGIN      
      
 -- Retreiving the info       
 SELECT  CUSTOMER_ID, APP_PRIOR_CARRIER_INFO_ID, OLD_POLICY_NUMBER, CARRIER,      
  LOB, SUB_LOB, Convert(varchar,EFF_DATE,case when @LANG_ID=2 then 103 else 101 end ) As EFF_DATE, Convert(Varchar,EXP_DATE,case when @LANG_ID=2 then 103 else 101 end ) As EXP_DATE, POLICY_CATEGORY, POLICY_TERM_CODE, POLICY_TYPE, YEARS_PRIOR_COMP,      
  ACTUAL_PREM, ASSIGNEDRISKYN, PRIOR_PRODUCER_INFO_ID, RISK_NEW_AGENCY, MOD_FACTOR,rtrim(ltrim(ANNUAL_PREM)) ANNUAL_PREM,      
  IS_ACTIVE,--, CREATED_BY, CREATED_DATETIME , LAST_UPDATED_DATETIME, MODIFIED_BY    
 PRIOR_BI_CSL_LIMIT --Added for Itrack Issue 6449 on 23 Oct 09    
 FROM APP_PRIOR_CARRIER_INFO      
 WHERE      
  CUSTOMER_ID   = @CUSTOMER_ID AND      
  APP_PRIOR_CARRIER_INFO_ID = @APP_PRIOR_CARRIER_INFO_ID      
       
END      
    
  
  
  


GO

