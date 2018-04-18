IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCustomerAkaDba]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCustomerAkaDba]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO










/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertCustomerAkaDba
Created by      : Pradeep
Date            : 26/04/2005
Purpose         : To add record in profit center table
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE        PROC Dbo.Proc_InsertCustomerAkaDba
(
	@Customer_ID Int,
	@AkaDba_Type Int,
	@AkaDba_Name NVarChar(255),
	@AkaDba_Add NVarChar(70),
	@AkaDba_Add2 NVarChar(140),
	@AkaDba_City NVarChar(40),
	@AkaDba_State Int,
	@AkaDba_Country Int,
	@AkaDba_Zip NVarChar(11),
	@AkaDba_Website NVarChar(150),
	@AkaDba_Email NVarChar(50),
	@AkaDba_Legal_Entity_Code Int,
	@AkaDba_Name_On_Form NChar(1),
	@AkaDba_Disp_Order smallint,
	@AkaDba_Memo NVarChar(35),
	@Is_Active NChar(1),
	@Created_By Int,
	@Modified_By Int,
	@AkaDba_ID Int	OUTPUT 
)

AS

DECLARE @MAX Int


BEGIN
		
	SELECT @MAX = ISNULL(MAX(AKADBA_ID),0) + 1
	FROM CLT_CUSTOMER_AKADBA
		
	INSERT INTO CLT_CUSTOMER_AKADBA
	(
		AkaDba_ID,
		Customer_ID,
		AkaDba_Type ,
		AkaDba_Name ,
		AkaDba_Add ,
		AkaDba_Add2 ,
		AkaDba_City ,
		AkaDba_State ,
		AkaDba_Zip ,
		AkaDba_Country ,
		AkaDba_Website ,
		AkaDba_Email ,
		AkaDba_Legal_Entity_Code ,
		AkaDba_Name_On_Form,
		AkaDba_Disp_Order ,
		AkaDba_Memo,
		Is_Active ,
		Created_By,
		Created_DateTime,
		Modified_By,
		Last_Updated_DateTime
	)
	VALUES
	(	
		@MAX,
		@Customer_ID,
		@AkaDba_Type ,
		@AkaDba_Name ,
		@AkaDba_Add ,
		@AkaDba_Add2,
		@AkaDba_City ,
		@AkaDba_State ,
		@AkaDba_Zip ,
		@AkaDba_Country,
		@AkaDba_Website ,
		@AkaDba_Email ,
		@AkaDba_Legal_Entity_Code ,
		@AkaDba_Name_On_Form ,
		@AkaDba_Disp_Order ,
		@AkaDba_Memo ,
		@Is_Active ,
		@Created_By ,
		GetDate(),
		@Modified_By ,
		GetDate() 
	)
	
	IF @@ERROR <> 0
	BEGIN
		RETURN
	END

	SET @AkaDba_ID = @MAX
	
	
	
END








GO

