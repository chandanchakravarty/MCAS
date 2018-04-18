IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_MVR_EXCEPTION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_MVR_EXCEPTION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

/*
MODIFIED BY 	: PRAVESH CHANDEL
DATE		:14 DEC 2006
PURPOSE		:TO INSERT MVR EXCEPTON VIOLATION
DROP PROC [dbo].[Proc_Insert_MVR_EXCEPTION]
*/
CREATE PROCEDURE [dbo].[Proc_Insert_MVR_EXCEPTION]
	(
	 @CUSTOMER_ID 	[int],
	 @APP_ID 	[int],
	 @APP_VERSION_ID 	[smallint],
	 @POL_ID 	[int],
	 @POL_VERSION_ID 	[smallint],
	 @MVR_CODE 	[varchar](50),
	 @MVR_DATE	[datetime],
	 @MVR_DESCRIPTION 	[varchar](500),
	 @MVR_ID	int out	
	)

AS 

if not exists(select CUSTOMER_ID from MVR_EXCEPTION where   CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID
		AND APP_VERSION_ID=@APP_VERSION_ID and MVR_CODE=@MVR_CODE )
BEGIN
INSERT INTO [dbo].[MVR_EXCEPTION] 
	 ( 
	 [CUSTOMER_ID],
	 [APP_ID],
	 [APP_VERSION_ID],
	 [POL_ID],
	 [POL_VERSION_ID],
	 [MVR_CODE],
	 [MVR_DATE],
	 [MVR_DESCRIPTION]) 
 
VALUES 
	(
	 @CUSTOMER_ID,
	 @APP_ID,
	 @APP_VERSION_ID,
	 @POL_ID,
	 @POL_VERSION_ID,
	 @MVR_CODE,
	 @MVR_DATE,
	 @MVR_DESCRIPTION)
	select @MVR_ID=max(ID) from MVR_EXCEPTION

END
else

set @MVR_ID=-1





GO

