IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_AUTO_ID_CARD_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_AUTO_ID_CARD_INFO]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.APP_AUTO_ID_CARD_INFO
Created by      : Shrikant Bhatt
Date            : 4/28/2005
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     		Review By          Comments
12/05/05	Nidhi		Added two more fields (SPECIAL_WORD_FRONT,SPECIAL_WORD_BACK) for motorcycle case
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_InsertAPP_AUTO_ID_CARD_INFO
(
@CUSTOMER_ID		int,
@APP_ID			int,
@APP_VERSION_ID		smallint,
@VEHICLE_ID			smallint,
@ID_EFFECTIVE_DATE     	datetime,
@ID_EXPITATION_DATE     	datetime,
@NAME_TYPE     		nvarchar(2),
@NAME_ID     			int,
@A_NAME     			nvarchar(510),
@ADDRESS1     		nvarchar(300),
@ADDRESS2     		nvarchar(300),
@CITY     			nvarchar(160),
@STATE     			nvarchar(20),
@ZIP     			nvarchar(40),
@EMAIL     			nvarchar(200),
@NAME_PRINT     		char(1),
@STATE_TYPE     		char(1),
@IS_ACTIVE     		nchar(2),
@CREATED_BY     		int,
@CREATED_DATETIME     	datetime,
@MODIFIED_BY     		int,
@LAST_UPDATED_DATETIME datetime,
@SPECIAL_WORD_FRONT      varchar(100) =null,
@SPECIAL_WORD_BACK         varchar(100) =null,
@IS_UPDATED	    bit =null
)
AS
BEGIN
INSERT INTO APP_AUTO_ID_CARD_INFO
(
CUSTOMER_ID,
APP_ID,
APP_VERSION_ID,
VEHICLE_ID,
ID_EFFECTIVE_DATE,
ID_EXPITATION_DATE,
NAME_TYPE,
NAME_ID,
A_NAME,
ADDRESS1,
ADDRESS2,
CITY,
STATE,
ZIP,
EMAIL,
NAME_PRINT,
STATE_TYPE,
IS_ACTIVE,
CREATED_BY,
CREATED_DATETIME,
MODIFIED_BY,
LAST_UPDATED_DATETIME,
SPECIAL_WORD_FRONT ,
SPECIAL_WORD_BACK  ,
IS_UPDATED
)
VALUES
(
@CUSTOMER_ID,
@APP_ID,
@APP_VERSION_ID,
@VEHICLE_ID,
@ID_EFFECTIVE_DATE,
@ID_EXPITATION_DATE,
@NAME_TYPE,
@NAME_ID,
@A_NAME,
@ADDRESS1,
@ADDRESS2,
@CITY,
@STATE,
@ZIP,
@EMAIL,
@NAME_PRINT,
@STATE_TYPE,
@IS_ACTIVE,
@CREATED_BY,
@CREATED_DATETIME,
@MODIFIED_BY,
@LAST_UPDATED_DATETIME,
@SPECIAL_WORD_FRONT  ,
@SPECIAL_WORD_BACK ,
@IS_UPDATED
)
END


GO

