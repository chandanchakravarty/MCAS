IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SubLocationNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SubLocationNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------        
Proc Name       : dbo.Proc_LocationNumber
Created by      : Gaurav        
Date            : 5/31/2005        
Purpose       : Return the Query       
Revison History :        
Used In   : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC Dbo.Proc_SubLocationNumber        
(        
	@CUSTOMER_ID  int   ,
	@APP_ID		int,
	@APP_VERSION_ID int,
	@LOCATION_ID int

)        
AS        
BEGIN        
	DECLARE @SUB_LOC_NUM numeric
	SELECT
		@SUB_LOC_NUM = isnull(Max(Sub_Loc_Number), 0) + 1 
	FROM
		APP_SUB_LOCATIONS
	WHERE
		CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID  AND LOCATION_ID=@LOCATION_ID

	if (SELECT Len(Convert(varchar, @SUB_LOC_NUM))) > 5
		SELECT null as Sub_Loc_Number
	else
		SELECT @SUB_LOC_NUM as Sub_Loc_Number
END





GO

