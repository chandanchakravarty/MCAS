IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATEAPP_HOME_RATING_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATEAPP_HOME_RATING_INFO]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_UPDATEAPP_HOME_RATING_INFO
Created by      : Anurag Verma
Date            : 5/13/2005
Purpose    	  :UPDATING record to APP_HOME_RATING_INFO
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UPDATEAPP_HOME_RATING_INFO
(
@CUSTOMER_ID     int,
@APP_ID     int,
@APP_VERSION_ID     smallint,
@DWELLING_ID     smallint,
@HYDRANT_DIST     real,
@FIRE_STATION_DIST     real,
@IS_UNDER_CONSTRUCTION     char(1),
@EXPERIENCE_CREDIT     char(1),
@IS_AUTO_POL_WITH_CARRIER     char(1),
@PERSONAL_LIAB_TER_CODE     nvarchar(100),
@PROT_CLASS     nvarchar(100),
@RATING_METHOD    int
)
AS
BEGIN
UPDATE APP_HOME_RATING_INFO
SET 
HYDRANT_DIST=@HYDRANT_DIST  ,
FIRE_STATION_DIST =@FIRE_STATION_DIST    ,
IS_UNDER_CONSTRUCTION =@IS_UNDER_CONSTRUCTION    ,
EXPERIENCE_CREDIT=@EXPERIENCE_CREDIT   ,
IS_AUTO_POL_WITH_CARRIER =@IS_AUTO_POL_WITH_CARRIER   ,
PERSONAL_LIAB_TER_CODE=@PERSONAL_LIAB_TER_CODE     ,
PROT_CLASS   =@PROT_CLASS   ,
RATING_METHOD     =@RATING_METHOD     
WHERE 
CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND
APP_VERSION_ID=@APP_VERSION_ID AND DWELLING_ID=@DWELLING_ID
END

GO

