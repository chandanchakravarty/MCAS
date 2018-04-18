IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchWaterViolationInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchWaterViolationInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
Proc Name        : dbo.Proc_FetchWaterViolationInfo    
Created by       : Mohit Agarwal    
Date             : 11/22/2006
Purpose          : retrieving data from APP_WATER_MVR_INFORMATION     
Revison History  :    
Used In          : Wolverine    
      
*/  
-- drop proc dbo.Proc_FetchWaterViolationInfo
CREATE PROC dbo.Proc_FetchWaterViolationInfo    
@CUSTOMER_ID INT,    
@APP_ID INT,    
@APP_VERSION_ID INT,    
@MVR_DATE DATETIME,
@SERIOUS VARCHAR(2)    
AS 
BEGIN   
IF @SERIOUS='Y'
    SELECT APP.* FROM APP_WATER_MVR_INFORMATION APP
	INNER JOIN MNT_VIOLATIONS MNT
	ON APP.VIOLATION_TYPE=MNT.VIOLATION_ID

	WHERE APP.CUSTOMER_ID=@CUSTOMER_ID
		AND APP.APP_ID=@APP_ID AND APP.APP_VERSION_ID=@APP_VERSION_ID
		AND APP.MVR_DATE>=@MVR_DATE AND MNT.VIOLATION_PARENT=1
ELSE
    SELECT APP.* FROM APP_WATER_MVR_INFORMATION APP
	INNER JOIN MNT_VIOLATIONS MNT
	ON APP.VIOLATION_TYPE=MNT.VIOLATION_ID

	WHERE APP.CUSTOMER_ID=@CUSTOMER_ID
		AND APP.APP_ID=@APP_ID AND APP.APP_VERSION_ID=@APP_VERSION_ID
		AND APP.MVR_DATE>=@MVR_DATE AND MNT.VIOLATION_PARENT!=1
  
END





GO

