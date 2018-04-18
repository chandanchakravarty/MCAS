IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_PKG_LOB_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_PKG_LOB_DETAILS]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





/*----------------------------------------------------------
Proc Name       : dbo.APP_PKG_LOB_DETAILS
Created by      : Ajit Singh Chahal
Date            : 4/28/2005
Purpose    	  :To insert records in APP_PKG_LOB_DETAILS.
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC Dbo.Proc_InsertAPP_PKG_LOB_DETAILS
(
@REC_ID     int output,
@CUSTOMER_ID     int,
@APP_ID     int,
@APP_VERSION_ID     smallint,
@LOB     nvarchar(10),
@SUB_LOB     nvarchar(10)
)
AS
Begin

declare @count int

select @count=count(*) from APP_PKG_LOB_DETAILS 
where CUSTOMER_ID=@CUSTOMER_ID and APP_VERSION_ID=@APP_VERSION_ID and APP_ID=@APP_ID and LOB = @LOB and SUB_LOB = @SUB_LOB

if(@count<=0)
BEGIN
	select  @REC_ID=isnull(Max(REC_ID),0)+1 from APP_PKG_LOB_DETAILS
	INSERT INTO APP_PKG_LOB_DETAILS
	(
		REC_ID,
		CUSTOMER_ID,
		APP_ID,
		APP_VERSION_ID,
		LOB,
		SUB_LOB
	)
	VALUES
	(
		@REC_ID,
		@CUSTOMER_ID,
		@APP_ID,
		@APP_VERSION_ID,
		@LOB,
		@SUB_LOB
	)
	END
else
	set @REC_ID = -1
end


GO

