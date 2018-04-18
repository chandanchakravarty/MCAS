IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertExcessLayer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertExcessLayer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO





/*----------------------------------------------------------
Created by		: Deepak Batra
Date                 	: 06 Jan 2006
Purpose              	: To implement the Insert on the Excess Layer screen
Revison History      	:
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_InsertExcessLayer
(	
	@EXCESS_ID		INT OUTPUT,
	@CONTRACT_ID 		INT,
	@LAYER_AMOUNT		DECIMAL,
	@UNDERLYING_AMOUNT	DECIMAL,
	@LAYER_PREMIUM		DECIMAL,
	@CEDING_COMMISSION	DECIMAL,
	@AC_PREMIUM		DECIMAL,
	@IS_ACTIVE		CHAR(1),
	@CREATED_BY		INT,
	@CREATED_DATETIME	DATETIME,		
	@PARTICIPATION_AMOUNT	DECIMAL,		
	@PRORATA_AMOUNT		DECIMAL,		
	@LAYER_TYPE		CHAR(1)
)
AS
BEGIN
SELECT @EXCESS_ID = IsNull(Max(EXCESS_ID),0) + 1 FROM MNT_REINSURANCE_EXCESS
INSERT INTO  MNT_REINSURANCE_EXCESS
(
	EXCESS_ID, 
	CONTRACT_ID, 
	LAYER_AMOUNT, 
	UNDERLYING_AMOUNT, 
	LAYER_PREMIUM, 
	CEDING_COMMISSION, 	
	AC_PREMIUM,
	IS_ACTIVE,
	CREATED_BY,
	CREATED_DATETIME,
	PARTICIPATION_AMOUNT,
	PRORATA_AMOUNT,
	LAYER_TYPE
)
VALUES
(
	@EXCESS_ID,
	@CONTRACT_ID,
	@LAYER_AMOUNT,
	@UNDERLYING_AMOUNT,
	@LAYER_PREMIUM,
	@CEDING_COMMISSION,
	@AC_PREMIUM,
	@IS_ACTIVE,
	@CREATED_BY,
	@CREATED_DATETIME,
	@PARTICIPATION_AMOUNT,
	@PRORATA_AMOUNT,
	@LAYER_TYPE		
)
END



GO

