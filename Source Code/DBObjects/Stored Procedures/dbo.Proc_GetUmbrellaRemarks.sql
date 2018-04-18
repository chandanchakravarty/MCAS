IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUmbrellaRemarks]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUmbrellaRemarks]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetUmbrellaRemarks
Created by         : Priya
Date               : 15/06/2005
Purpose            : To get remarks  from APP_UMBRELLA_VEHICLE_INFO table
Revison History    :
Used In            : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetUmbrellaRemarks
(
@CUSTOMER_ID 	int,
@APP_ID         int, 
@APP_VERSION_ID   smallint,
@VEHICLE_ID      smallint
)
AS
BEGIN

SELECT  REMARKS  FROM  APP_UMBRELLA_VEHICLE_INFO	
WHERE CUSTOMER_ID      =  @CUSTOMER_ID AND
      APP_ID           =  @APP_ID  AND
      APP_VERSION_ID   =  @APP_VERSION_ID AND
      VEHICLE_ID      =  @VEHICLE_ID

END





GO

