IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolWatercraftEngine]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolWatercraftEngine]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC dbo.Proc_DeletePolWatercraftEngine
(
@CUSTOMERID 	int,
@POLICY_ID		int,
@POLICY_VERSION_ID	int,
@ENGINE_ID		int

)
AS
BEGIN


DELETE 
FROM     POL_WATERCRAFT_ENGINE_INFO
WHERE     (CUSTOMER_ID = @CUSTOMERID)   and (POLICY_ID=@POLICY_ID) 
          AND (POLICY_VERSION_ID=@POLICY_VERSION_ID) AND (ENGINE_ID= @ENGINE_ID);

END
--select * from POL_WATERCRAFT_ENGINE_INFO




GO

