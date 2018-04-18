IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetREMARKS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetREMARKS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetRemarks
Created by           : Priya
Date                    : 27/05/2005
Purpose               : To get remarks  from APP_UMBRELLA_REAL_ESTATE_LOCATION	 table
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetREMARKS
(
@CUSTOMER_ID 	int,
@APP_ID         int, 
@APP_VERSION_ID   smallint,
@LOCATION_ID      smallint
)
AS
BEGIN

SELECT  REMARKS  FROM  APP_UMBRELLA_REAL_ESTATE_LOCATION	
WHERE CUSTOMER_ID      =  @CUSTOMER_ID AND
      APP_ID           =  @APP_ID  AND
      APP_VERSION_ID   =  @APP_VERSION_ID AND
      LOCATION_ID      =  @LOCATION_ID

END


GO

