IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_UMBRELLA_POL_INFO_OTHER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_UMBRELLA_POL_INFO_OTHER]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_InsertAPP_UMBRELLA_POL_INFO_OTHER
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_InsertAPP_UMBRELLA_POL_INFO_OTHER
(

@CUSTOMER_ID int,
@APP_ID int,
@APP_VERSION_ID int,
@COMBINED_SINGLE_LIMIT decimal (18,2),
@BODILY_INJURY decimal (18,2),
@PROPERTY_DAMAGE decimal (18,2),
@IS_ACTIVE nchar,
@CREATED_BY int,
@CREATED_DATETIME datetime,
@MODIFIED_BY int,
@LAST_UPDATED_DATETIME datetime
	
)
AS
BEGIN
INSERT INTO APP_UMBRELLA_POL_INFO_OTHER
(
CUSTOMER_ID,APP_ID,APP_VERSION_ID,COMBINED_SINGLE_LIMIT,BODILY_INJURY,PROPERTY_DAMAGE,
IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME
)
VALUES
(
@CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@COMBINED_SINGLE_LIMIT,@BODILY_INJURY,
@PROPERTY_DAMAGE,@IS_ACTIVE,@CREATED_BY,@CREATED_DATETIME,@MODIFIED_BY,
@LAST_UPDATED_DATETIME
)
END


GO

