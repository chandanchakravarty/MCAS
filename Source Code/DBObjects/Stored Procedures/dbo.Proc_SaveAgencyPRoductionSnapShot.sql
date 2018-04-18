IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SaveAgencyPRoductionSnapShot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SaveAgencyPRoductionSnapShot]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--BEGIN TRAN
--DROP PROC Proc_SaveAgencyPRoductionSnapShot
--GO
/*----------------------------------------------------------          
Proc Name        : dbo.Proc_SaveAgencyPRoductionSnapShot          
Created by       : Raghav Gupta         
Date             : 12/12/2009
Purpose          : Procedure to Save Agency Production Record.           
Revison History  :          
Used In          : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/ 

CREATE PROC [dbo].[Proc_SaveAgencyPRoductionSnapShot]
(
@MONTH INT,
@YEAR INT
)

AS
BEGIN

IF  EXISTS (SELECT * FROM RPT_AGENCY_PRODUCTION_DATA with(nolock) WHERE  MONTH_NUMBER =  @MONTH  AND YEAR_NUMBER = @YEAR)
BEGIN
    DELETE RPT_AGENCY_PRODUCTION_DATA WHERE  MONTH_NUMBER =  @MONTH  AND YEAR_NUMBER = @YEAR
END


BEGIN
	INSERT INTO RPT_AGENCY_PRODUCTION_DATA
	SELECT *  FROM VW_FETCHAGENCYPRODUCTIONREPORT
	WHERE  MONTH_NUMBER =  @MONTH  AND 
	YEAR_NUMBER = @YEAR  
END

   
END

--GO
--EXEC Proc_SaveAgencyPRoductionSnapShot 10,2009
--ROLLBACK TRAN












GO

