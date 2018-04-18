IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_CHECK_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_CHECK_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_UpdateACT_CHECK_INFORMATION    
Created by      : Ajit Singh Chahal    
Date            : 6/30/2005    
Purpose       :To update records in check information.    
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--drop proc Proc_UpdateACT_CHECK_INFORMATION  
create  PROC dbo.Proc_UpdateACT_CHECK_INFORMATION    
(    
@CHECK_ID     int ,    
@CHECK_TYPE     nvarchar(10),    
@ACCOUNT_ID     int,    
@CHECK_NUMBER     nvarchar(40),    
--@CHECK_DATE     datetime,    
@CHECK_AMOUNT     decimal(18,2),    
@CHECK_NOTE     nvarchar(200),    
@PAYEE_ENTITY_ID     int,    
@PAYEE_ENTITY_TYPE     nvarchar(10),    
@PAYEE_ENTITY_NAME     nvarchar(510),    
@PAYEE_ADD1     nvarchar(140),    
@PAYEE_ADD2     nvarchar(140),    
@PAYEE_CITY     nvarchar(80),    
@PAYEE_STATE     nvarchar(60),    
@PAYEE_ZIP     nvarchar(24),    
--@DIV_ID     smallint,    
--@DEPT_ID     smallint,    
--@PC_ID     smallint,    
@CUSTOMER_ID     int,    
@POLICY_ID     smallint,    
@POLICY_VER_TRACKING_ID smallint,    
@CHECKSIGN_1     nvarchar(200),    
@CHECKSIGN_2     nvarchar(200),    
@CHECK_MEMO     nvarchar(140),    
@TRAN_TYPE     int,    
@IS_DISPLAY_ON_STUB     char(1),    
@MODIFIED_BY     int,    
@LAST_UPDATED_DATETIME     datetime,    
@ERROR_STATUS int out    
)    
AS    
BEGIN    
set @ERROR_STATUS=1    
/*REMOVED CHECK NUMBER */  
/*--Check if current check no. already exists in system    
if exists (select CHECK_ID from ACT_CHECK_INFORMATION with (nolock) where ACCOUNT_ID=@ACCOUNT_ID and   
CHECK_NUMBER=@CHECK_NUMBER and IS_IN_CURRENT_SEQUENCE='Y' and CHECK_ID<>@CHECK_ID)    
Begin    
 set @ERROR_STATUS=-1     
 return -1    
end    
    
--check if check no. exceeds max check no. allotted to current series.     
declare @maxCheckNo int    
select @maxCheckNo=END_CHECK_NUMBER from ACT_BANK_INFORMATION with (nolock) where ACCOUNT_ID=@ACCOUNT_ID    
if @CHECK_NUMBER>@maxCheckNo    
Begin     
 set @ERROR_STATUS=-2    
 return -1    
end  */  
    
    
  
  
    
    
    
update ACT_CHECK_INFORMATION    
set    
CHECK_TYPE     = @CHECK_TYPE,    
ACCOUNT_ID     = @ACCOUNT_ID,    
--CHECK_NUMBER  = @CHECK_NUMBER,    
--CHECK_DATE     = @CHECK_DATE ,    
CHECK_AMOUNT   = @CHECK_AMOUNT,    
CHECK_NOTE     = @CHECK_NOTE,    
PAYEE_ENTITY_ID     = @PAYEE_ENTITY_ID,    
PAYEE_ENTITY_TYPE     = @PAYEE_ENTITY_TYPE,    
PAYEE_ENTITY_NAME     = @PAYEE_ENTITY_NAME,    
PAYEE_ADD1=@PAYEE_ADD1,    
PAYEE_ADD2=@PAYEE_ADD2,    
PAYEE_CITY=@PAYEE_CITY,    
PAYEE_STATE=@PAYEE_STATE,    
PAYEE_ZIP=@PAYEE_ZIP,    
--DIV_ID     = @DIV_ID,    
--DEPT_ID     = @DEPT_ID,    
--PC_ID     = @PC_ID,    
CUSTOMER_ID     = @CUSTOMER_ID,    
POLICY_ID     = @POLICY_ID,    
POLICY_VER_TRACKING_ID = @POLICY_VER_TRACKING_ID,    
CHECKSIGN_1     = @CHECKSIGN_1,    
CHECKSIGN_2     = @CHECKSIGN_2,    
CHECK_MEMO     = @CHECK_MEMO,    
TRAN_TYPE     = @TRAN_TYPE,    
IS_DISPLAY_ON_STUB     = @IS_DISPLAY_ON_STUB,    
MODIFIED_BY     = @MODIFIED_BY,    
LAST_UPDATED_DATETIME  = @LAST_UPDATED_DATETIME  
WHERE CHECK_ID = @CHECK_ID    
END    
    
    
    
    
    
    
    
    
    
    
  
  
  
  
  
  
  





GO

