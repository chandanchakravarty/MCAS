IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATE_ACT_CHECK_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_UPDATE_ACT_CHECK_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.TEMP_ACT_CHECK_INFORMATION  
Created by      : Ajit Singh Chahal  
Date            : 13, Sept 2005  
Purpose     : To insert records in check information.  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC dbo.PROC_UPDATE_ACT_CHECK_INFORMATION  
(  
@CHECK_ID     int ,  
@CHECK_TYPE     nvarchar(10),  
@ACCOUNT_ID     int,  
@CHECK_DATE     datetime,  
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
@PAYEE_NOTE     nvarchar(200),  
@CUSTOMER_ID     int,  
@POLICY_ID     smallint,  
@POLICY_VER_TRACKING_ID     smallint,  
@CREATED_BY     int,  
@CREATED_DATETIME     datetime,  
@MODIFIED_BY     int,  
@LAST_UPDATED_DATETIME     datetime  
)  
AS  
BEGIN  
update  ACT_CHECK_INFORMATION set  
  
   
CHECK_TYPE = @CHECK_TYPE,  
ACCOUNT_ID = @ACCOUNT_ID,  
CHECK_DATE=@CHECK_DATE ,  
CHECK_AMOUNT= @CHECK_AMOUNT,  
CHECK_NOTE=@CHECK_NOTE ,  
PAYEE_ENTITY_ID= @PAYEE_ENTITY_ID,  
PAYEE_ENTITY_TYPE=@PAYEE_ENTITY_TYPE ,  
PAYEE_ENTITY_NAME= @PAYEE_ENTITY_NAME,  
PAYEE_ADD1= @PAYEE_ADD1,  
PAYEE_ADD2= @PAYEE_ADD2,  
PAYEE_CITY=@PAYEE_CITY ,  
PAYEE_STATE=@PAYEE_STATE ,  
PAYEE_ZIP= @PAYEE_ZIP,  
PAYEE_NOTE= @PAYEE_NOTE,  
CUSTOMER_ID= @CUSTOMER_ID,  
POLICY_ID= @POLICY_ID,  
POLICY_VER_TRACKING_ID=@POLICY_VER_TRACKING_ID ,  
CREATED_BY= @CREATED_BY,  
CREATED_DATETIME= @CREATED_DATETIME,  
MODIFIED_BY= @MODIFIED_BY,  
LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME   
where  CHECK_ID = @CHECK_ID  
  
end  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  



GO

