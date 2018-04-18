IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_MVR_AUTO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_MVR_AUTO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
PRoc dbo.Proc_Insert_MVR_AUTO
Created By : Pravesh Chandel
Date	    : 13 dec 2006
Purpose     :  To Insert MVR fetch from MVR Report from IIX web service.	
DROP PROC dbo.Proc_Insert_MVR_AUTO
*/

CREATE PROCEDURE dbo.Proc_Insert_MVR_AUTO
	(
--	 @APP_MVR_ID	INT OUT,
	 @CUSTOMER_ID 	[int],
	 @APP_ID 	[int],
	 @APP_VERSION_ID 	[smallint],
	 @DRIVER_ID   int,
         @MVR_CODE 	[varchar](50),
         @MVR_DATE     datetime,
	 @MVR_DESCRIPTION 	[varchar](500))

AS 
begin

DECLARE @TEMP_APP_MVR_ID INT

SELECT @TEMP_APP_MVR_ID=ISNULL(MAX(APP_MVR_ID),0) +1 FROM APP_MVR_INFORMATION WHERE CUSTOMER_ID=@CUSTOMER_ID 
AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND DRIVER_ID=@DRIVER_ID

INSERT INTO APP_MVR_INFORMATION
	 ( 
	 APP_MVR_ID,
	 CUSTOMER_ID,
	 APP_ID,
	 APP_VERSION_ID,
	 DRIVER_ID,
	 MVR_DATE,
	 IS_ACTIVE
	-- MVR_CODE,
	 ) 
 
VALUES 
	(
	@TEMP_APP_MVR_ID,
	 @CUSTOMER_ID,
	 @APP_ID,
	 @APP_VERSION_ID,
	 @DRIVER_ID,
--	 @POL_VERSION_ID,
	 @MVR_DATE,
--	 @MVR_DESCRIPTION
	 'Y'	
)

end



GO

