IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUmbrellaPolicyOthersXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUmbrellaPolicyOthersXml]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetUmbrellaPolicyOthersXml
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_GetUmbrellaPolicyOthersXml
(
@CUSTOMER_ID int,
@APP_ID int,
@APP_VERSION_ID int	
)
AS
BEGIN
SELECT 
CUSTOMER_ID,APP_ID,APP_VERSION_ID,COMBINED_SINGLE_LIMIT,BODILY_INJURY,PROPERTY_DAMAGE,
IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME
FROM APP_UMBRELLA_POL_INFO_OTHER
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID= @APP_ID AND  APP_VERSION_ID=@APP_VERSION_ID
END


GO

