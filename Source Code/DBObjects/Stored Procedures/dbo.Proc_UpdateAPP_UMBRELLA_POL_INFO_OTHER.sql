IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_UMBRELLA_POL_INFO_OTHER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_UMBRELLA_POL_INFO_OTHER]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : DboProc_UpdateAPP_UMBRELLA_POL_INFO_OTHER
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_UpdateAPP_UMBRELLA_POL_INFO_OTHER
(
@CUSTOMER_ID int,
@APP_ID int,
@APP_VERSION_ID int,	
@COMBINED_SINGLE_LIMIT decimal  (18,2),
@BODILY_INJURY decimal (18,2),
@PROPERTY_DAMAGE decimal (18,2),
@IS_ACTIVE nchar,
@MODIFIED_BY int,
@LAST_UPDATED_DATETIME datetime
)
AS
BEGIN
UPDATE APP_UMBRELLA_POL_INFO_OTHER
SET COMBINED_SINGLE_LIMIT=@COMBINED_SINGLE_LIMIT,
BODILY_INJURY=@BODILY_INJURY,
PROPERTY_DAMAGE=@PROPERTY_DAMAGE,
IS_ACTIVE=@IS_ACTIVE,
MODIFIED_BY=@MODIFIED_BY,
LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME
FROM APP_UMBRELLA_POL_INFO_OTHER
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID= @APP_ID AND  APP_VERSION_ID=@APP_VERSION_ID
END


GO

