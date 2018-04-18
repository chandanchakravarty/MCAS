IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAppWatercraftEngine]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAppWatercraftEngine]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC dbo.Proc_DeleteAppWatercraftEngine
(
@CUSTOMERID 	int,
@APPID		int,
@APPVERSIONID	int,
@ENGINE_ID		int

)
AS
BEGIN


Delete 
FROM     app_watercraft_engine_info
WHERE     (CUSTOMER_ID = @CUSTOMERID)   and (APP_ID=@APPID) AND (APP_VERSION_ID=@APPVERSIONID) AND (ENGINE_ID= @ENGINE_ID);

END





GO

