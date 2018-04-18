IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCoveragesAndRanges]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCoveragesAndRanges]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------  
Proc Name            : Dbo.Proc_GetCoveragesAndRanges  
Created by           : Pradeep  
Date                 : 27/04/2005  
Purpose              : To get the coverages and ranges
Revison History :  
Used In              :   Wolverine  
  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetCoveragesAndRanges  
(  
	@CUSTOMER_ID  int,  
	@APP_ID  int,  
	@APP_VERSION_ID int

  
)  
AS  
BEGIN  

DECLARE @STATE_ID SmallInt  
DECLARE @LOB_ID NVarCHar(5)  

SELECT @STATE_ID = STATE_ID,  
 @LOB_ID = APP_LOB  
FROM APP_LIST  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
 APP_ID = @APP_ID AND  
 APP_VERSION_ID = @APP_VERSION_ID  

--Coverage    
SELECT * FROM MNT_COVERAGE
WHERE STATE_ID = @STATE_ID AND
	LOB_ID = @LOB_ID

--Coverage ranges
SELECT * FROM MNT_COVERAGE_RANGES R
INNER JOIN MNT_COVERAGE C ON
	R.COV_ID = C.COV_ID 
WHERE
	C.STATE_ID = @STATE_ID AND
	C.LOB_ID = @LOB_ID
	
END



GO

