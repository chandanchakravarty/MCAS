IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCustomerAkaDba]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCustomerAkaDba]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO









/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateCustomerAkaDba
Created by      : Pradeep
Date            : 26/04/2005
Purpose         : To add record in profit center table
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE       PROC Dbo.Proc_UpdateCustomerAkaDba
(
	@AkaDba_ID Int,
	@AkaDba_Type Int,
	@AkaDba_Name NVarChar(255),
	@AkaDba_Add NVarChar(70),
	@AkaDba_Add2 NVarChar(140),
	@AkaDba_City NVarChar(40),
	@AkaDba_State Int,
	@AkaDba_Zip NVarChar(11),
	@AkaDba_Country Int,
	@AkaDba_Website NVarChar(150),
	@AkaDba_Email NVarChar(50),
	@AkaDba_Legal_Entity_Code Int,
	@AkaDba_Name_On_Form NChar(1),
	@AkaDba_Disp_Order smallint,
	@AkaDba_Memo NVarChar(35),
	@Modified_By Int
)

AS

BEGIN
	
	UPDATE CLT_CUSTOMER_AKADBA
	SET AkaDba_Type = @AkaDba_Type,
		AkaDba_Name = @AkaDba_Name,
		AkaDba_Add = @AkaDba_Add,
		AkaDba_Add2 = @AkaDba_Add2,
		AkaDba_City = @AkaDba_City,
		AkaDba_State = @AkaDba_State,
		AkaDba_Zip = @AkaDba_Zip,
		AkaDba_Country = @AkaDba_Country,
		AkaDba_Website = @AkaDba_Website,
		AkaDba_Email = @AkaDba_Email,
		AkaDba_Legal_Entity_Code = @AkaDba_Legal_Entity_Code,
		AkaDba_Name_On_Form = @AkaDba_Name_On_Form,
		AkaDba_Disp_Order = @AkaDba_Disp_Order,
		AkaDba_Memo = @AkaDba_Memo,
		Modified_By = @Modified_By,
		Last_Updated_DateTime = GetDate()
	WHERE AkaDba_ID = @AkaDba_ID
	
	IF @@ERROR <> 0
	BEGIN
		RETURN
	END

	
	
	
	
END







GO

