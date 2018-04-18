IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAUTO_ID_CARD_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAUTO_ID_CARD_INFO]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*------------------------------------------------------------------------------------------
Proc Name      	: dbo.APP_AUTO_ID_CARD_INFO
Created by      	: Shrikant Bhatt
Date            	: 4/28/2005
Purpose    	:Evaluation

Revison History 	:
Modifed by      	: nidhi
Date            	: 05/12/2005
Purpose    	:Added two more fields (SPECIAL_WORD_FRONT,SPECIAL_WORD_BACK) for Motorcycle case

Used In 	: Wolverine
---------------------------------------------------------------------------------------------
Date     Review By          Comments
---------------------------------------------------------------------------------------------*/
CREATE PROC Dbo.Proc_UpdateAUTO_ID_CARD_INFO
(
@CUSTOMER_ID     		int,
@APP_ID    		 	int,
@APP_VERSION_ID     		smallint,
@VEHICLE_ID     		smallint,
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
@IS_UPDATED			bit = null
)
AS
BEGIN

UPDATE APP_AUTO_ID_CARD_INFO
SET
CUSTOMER_ID			=	@CUSTOMER_ID,
APP_ID				=	@APP_ID,
APP_VERSION_ID		=	@APP_VERSION_ID,
VEHICLE_ID			=	@VEHICLE_ID,
ID_EFFECTIVE_DATE		=	@ID_EFFECTIVE_DATE,
ID_EXPITATION_DATE		=	@ID_EXPITATION_DATE,
NAME_TYPE			=	@NAME_TYPE,
NAME_ID			=	@NAME_ID,
A_NAME			=	@A_NAME,
ADDRESS1			=	@ADDRESS1,
ADDRESS2			=	@ADDRESS2,
CITY				=	@CITY,
STATE				=	@STATE,
ZIP				=	@ZIP,
EMAIL				=	@EMAIL,
NAME_PRINT			=	@NAME_PRINT,
STATE_TYPE			=	@STATE_TYPE,
IS_ACTIVE			=	@IS_ACTIVE,
CREATED_BY			=	@CREATED_BY,
CREATED_DATETIME		=	@CREATED_DATETIME,
MODIFIED_BY			=	@MODIFIED_BY,
LAST_UPDATED_DATETIME	=	@LAST_UPDATED_DATETIME,
SPECIAL_WORD_FRONT	=	@SPECIAL_WORD_FRONT,
SPECIAL_WORD_BACK		=	@SPECIAL_WORD_BACK ,
IS_UPDATED			=	@IS_UPDATED
WHERE
CUSTOMER_ID 		=	@CUSTOMER_ID AND
APP_ID				=	@APP_ID AND
APP_VERSION_ID		=	@APP_VERSION_ID AND
VEHICLE_ID			=	@VEHICLE_ID
END


GO

