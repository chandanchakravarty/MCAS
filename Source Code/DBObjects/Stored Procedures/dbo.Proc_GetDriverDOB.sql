IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDriverDOB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDriverDOB]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


/*----------------------------------------------------------
Proc Name          : Dbo. Proc_GetDriverDOB
Created by           : Mohit Gupta
Date                    : 08/11/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_GetDriverDOB
(
	@CUSTOMER_ID int,
	@APP_ID              int,
	@APP_VERSION_ID int,
             @DRIVER_ID            int	
	
)
AS
BEGIN
SELECT convert(varchar,DRIVER_DOB,101) DRIVER_DOB 
FROM APP_DRIVER_DETAILS 
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND 
APP_VERSION_ID=@APP_VERSION_ID AND
DRIVER_ID=@DRIVER_ID


END

GO

