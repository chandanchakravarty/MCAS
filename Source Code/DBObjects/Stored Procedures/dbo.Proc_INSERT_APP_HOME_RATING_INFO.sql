IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_INSERT_APP_HOME_RATING_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_INSERT_APP_HOME_RATING_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.INSERT_APP_HOME_RATING_INFO
Created by      : Anurag Verma
Date            : 5/13/2005
Purpose    	  :Inserting records in APP_HOME_RATING_INFO
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_INSERT_APP_HOME_RATING_INFO
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
@RATING_METHOD    int =null,
@NEED_OF_UNITS VARCHAR (10) =null
)
AS
BEGIN
INSERT INTO APP_HOME_RATING_INFO
(
CUSTOMER_ID,
APP_ID,
APP_VERSION_ID,
DWELLING_ID,
HYDRANT_DIST,
FIRE_STATION_DIST,
IS_UNDER_CONSTRUCTION,
EXPERIENCE_CREDIT,
IS_AUTO_POL_WITH_CARRIER,
PERSONAL_LIAB_TER_CODE,
PROT_CLASS,
RATING_METHOD,
NEED_OF_UNITS
)
VALUES
(
@CUSTOMER_ID,
@APP_ID,
@APP_VERSION_ID,
@DWELLING_ID,
@HYDRANT_DIST,
@FIRE_STATION_DIST,
@IS_UNDER_CONSTRUCTION,
@EXPERIENCE_CREDIT,
@IS_AUTO_POL_WITH_CARRIER,
@PERSONAL_LIAB_TER_CODE,
@PROT_CLASS,
@RATING_METHOD,
@NEED_OF_UNITS
)
END




GO

