IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_POL_Additional_CoverageB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_POL_Additional_CoverageB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
/*----------------------------------------------------------
Proc Name       : dbo.Proc_Update_POL_Additional_CoverageB
Created by      : Shafii
Date            : 6/23/2006
Purpose    	    :UPDATE ADDITIONAL AMOUNT OF COVERAGE B ACCORDING THE VALUES OF OTHERSTRUCTURE and increase in value
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_Update_POL_Additional_CoverageB
(
@CUSTOMER_ID     int,
@POLICY_ID     int,
@POLICY_VERSION_ID     smallint,
@DWELLING_ID     smallint,
@COVERAGEBADD   int
)
as
BEGIN




DECLARE @LOB_ID INT

	SELECT  @LOB_ID=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE
	CUSTOMER_ID = @CUSTOMER_ID
	AND POLICY_ID = @POLICY_ID
	AND POLICY_VERSION_ID=@POLICY_VERSION_ID
    
   IF(@LOB_ID !=  6)
     RETURN

update  POL_DWELLING_SECTION_COVERAGES set DEDUCTIBLE_1=@COVERAGEBADD
where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID AND COVERAGE_CODE_ID IN (774,794)




    

END







GO

