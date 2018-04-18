IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateUmbrellaRemarks]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateUmbrellaRemarks]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateUmbrellaRemarks
Created by      :Priya
Date            : 6/15/2005
Purpose         : To update the record APP_UMBRELLA_VEHICLE_INFO table
Revison History :
Used In         :   wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateUmbrellaRemarks

(
@CUSTOMER_ID  int,
@APP_ID        int,
@APP_VERSION_ID   smallint,
@VEHICLE_ID      smallint,
@REMARKS  nvarchar(250)
)
AS
BEGIN
	UPDATE APP_UMBRELLA_VEHICLE_INFO
	SET 
		REMARKS	=@REMARKS
		
	WHERE
                CUSTOMER_ID=@CUSTOMER_ID AND
		APP_ID     =@APP_ID  AND
		APP_VERSION_ID=@APP_VERSION_ID AND
		@VEHICLE_ID    =@VEHICLE_ID
		
END





GO

