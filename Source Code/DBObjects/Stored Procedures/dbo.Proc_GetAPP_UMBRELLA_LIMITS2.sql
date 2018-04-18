IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_UMBRELLA_LIMITS2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_UMBRELLA_LIMITS2]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*
----------------------------------------------------------    
Proc Name       : dbo.Proc_GetAPP_UMBRELA_LIMITS2
Created by      : Pradeep  
Date            : 26 May,2005    
Purpose         : Selects a single record from UMBRELLA_LIITS    
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------   
*/

CREATE PROCEDURE Proc_GetAPP_UMBRELLA_LIMITS2
(
	@CUSTOMER_ID int,
	@APP_ID int,
	@APP_VERSION_ID smallint
)

AS

SELECT CUSTOMER_ID,
	APP_ID,
	APP_VERSION_ID,
	BASIC,
	RESIDENCES_OWNER_OCCUPIED,
	NUM_OF_RENTAL_UNITS,
	RENTAL_UNITS,
	NUM_OF_AUTO,
	AUTOMOBILES,
	NUM_OF_OPERATORS,
	Cast(OPER_UNDER_AGE As decimal(10,0)) AS OPER_UNDER_AGE,
	NUM_OF_UNLIC_RV,
	UNLIC_RV,
	NUM_OF_UNINSU_MOTORIST,
	UNISU_MOTORIST,
	UNDER_INSURED_MOTORIST,
	WATERCRAFT,
	NUM_OF_OTHER,
	OTHER,
	DEPOSIT,
	ESTIMATED_TOTAL_PRE,
	CALCULATIONS
FROM APP_UMBRELLA_LIMITS	
WHERE CUSTOMER_ID = @CUSTOMER_ID AND
	APP_ID = @APP_ID AND
	APP_VERSION_ID = @APP_VERSION_ID



GO

