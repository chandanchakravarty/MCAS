IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPriorPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPriorPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name       : dbo.InsertPriorPolicy    
Created by      : Vijay    
Date            : 4/26/2005    
Purpose     : Evaluation    
Revison History :    
Used In  : Wolverine    
    
Modified By : Anurag Verma    
Modified On: 4/5/2005    
Purpose : Removing Add_id and App_version_id  parameters    
------------------------------------------------------------    
Date     Review By          Comments  
drop PROC Dbo.Proc_InsertPriorPolicy  
------   ------------       -------------------------*/    
CREATE PROC Dbo.Proc_InsertPriorPolicy    
(    
 @CUSTOMER_ID       int,    
 @APP_PRIOR_CARRIER_INFO_ID smallint output,    
     
 @OLD_POLICY_NUMBER       nvarchar(150),    
 @CARRIER        varchar(100),    
 @LOB        nvarchar(10),    
 @SUB_LOB        nvarchar(10),    
 @EFF_DATE        datetime,    
 @EXP_DATE        datetime,    
 @POLICY_CATEGORY       nvarchar(10),    
 @POLICY_TERM_CODE       varchar(25),    
 @POLICY_TYPE       nvarchar(60),    
 @YEARS_PRIOR_COMP       int,    
 @ACTUAL_PREM       decimal(9),    
 @ASSIGNEDRISKYN       nchar(2),    
 @PRIOR_PRODUCER_INFO_ID      smallint,    
 @RISK_NEW_AGENCY       nchar(2),    
 @MOD_FACTOR       nchar(20),    
 @ANNUAL_PREM       nchar(20),    
 @CREATED_BY       int,    
 @CREATED_DATETIME       datetime,    
 @MODIFIED_BY       int,    
 @LAST_UPDATED_DATETIME      datetime,    
 @INSERTUPDATE   varchar(1),
 @PRIOR_BI_CSL_LIMIT nvarchar(20)  --Added for Itrack Issue 6449 on 23 Oct 09 
)    
AS    
BEGIN    
    
if @INSERTUPDATE = 'I'    
BEGIN    
 --Inserting the record    
    
 --retreiving the maximum info id , which we will save in database    
 SELECT @APP_PRIOR_CARRIER_INFO_ID = IsNull(Max(APP_PRIOR_CARRIER_INFO_ID),0) + 1    
 FROM APP_PRIOR_CARRIER_INFO    
    
 INSERT INTO APP_PRIOR_CARRIER_INFO    
 (    
  CUSTOMER_ID,    
  APP_PRIOR_CARRIER_INFO_ID,    
  OLD_POLICY_NUMBER,    
  CARRIER,    
  LOB,    
  SUB_LOB,    
  EFF_DATE,    
  EXP_DATE,    
  POLICY_CATEGORY,    
  POLICY_TERM_CODE,    
  POLICY_TYPE,    
  YEARS_PRIOR_COMP,    
  ACTUAL_PREM,    
  ASSIGNEDRISKYN,    
  PRIOR_PRODUCER_INFO_ID,    
  RISK_NEW_AGENCY,    
  MOD_FACTOR,    
  ANNUAL_PREM,    
  IS_ACTIVE,    
  CREATED_BY,    
  CREATED_DATETIME,
  --Added for Itrack Issue 6449 on 23 Oct 09
  PRIOR_BI_CSL_LIMIT    
 )    
 VALUES    
 (    
  @CUSTOMER_ID,    
  @APP_PRIOR_CARRIER_INFO_ID,    
  @OLD_POLICY_NUMBER,    
  @CARRIER,    
  @LOB,    
  @SUB_LOB,    
  @EFF_DATE,    
  @EXP_DATE,    
  @POLICY_CATEGORY,    
  @POLICY_TERM_CODE,    
  @POLICY_TYPE,    
  @YEARS_PRIOR_COMP,    
  @ACTUAL_PREM,    
  @ASSIGNEDRISKYN,    
  @PRIOR_PRODUCER_INFO_ID,    
  @RISK_NEW_AGENCY,    
  @MOD_FACTOR,    
  @ANNUAL_PREM,    
  'Y',    
  @CREATED_BY,    
  @CREATED_DATETIME,
  --Added for Itrack Issue 6449 on 23 Oct 09
  @PRIOR_BI_CSL_LIMIT    
 )    
END    
ELSE    
BEGIN    
 --Updating the record    
 UPDATE APP_PRIOR_CARRIER_INFO    
 SET     
  OLD_POLICY_NUMBER  = @OLD_POLICY_NUMBER,    
  CARRIER   = @CARRIER,    
  LOB    = @LOB,    
  SUB_LOB   = @SUB_LOB,    
  EFF_DATE   = @EFF_DATE,    
  EXP_DATE   = @EXP_DATE,    
  POLICY_CATEGORY  = @POLICY_CATEGORY,    
  POLICY_TERM_CODE  = @POLICY_TERM_CODE,    
  POLICY_TYPE   = @POLICY_TYPE,    
  YEARS_PRIOR_COMP  = @YEARS_PRIOR_COMP,    
  ACTUAL_PREM   = @ACTUAL_PREM,    
  ASSIGNEDRISKYN   = @ASSIGNEDRISKYN,    
  PRIOR_PRODUCER_INFO_ID  = @PRIOR_PRODUCER_INFO_ID,    
  RISK_NEW_AGENCY  = @RISK_NEW_AGENCY,    
  MOD_FACTOR   = @MOD_FACTOR,    
  ANNUAL_PREM   = @ANNUAL_PREM,    
  MODIFIED_BY  = @MODIFIED_BY,    
  LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,
  --Added for Itrack Issue 6449 on 23 Oct 09
  PRIOR_BI_CSL_LIMIT = @PRIOR_BI_CSL_LIMIT    
 WHERE    
  CUSTOMER_ID   = @CUSTOMER_ID AND    
  APP_PRIOR_CARRIER_INFO_ID = @APP_PRIOR_CARRIER_INFO_ID    
    
END    
    
END  
  
  
  
  
  
GO

