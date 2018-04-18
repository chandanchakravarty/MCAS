IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_COVERAGE_DESCRIPTION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_COVERAGE_DESCRIPTION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                          
Proc Name       : dbo.Proc_GetCLM_COVERAGE_DESCRIPTION                                                              
Created by      : Sumit Chhabra                                                                        
Date            : 05/06/2006                                                                          
Purpose         : Get Policy coverages for the given claim id
Created by      : Sumit Chhabra                                                                         
Revison History :                                                                          
Used In        : Wolverine                                                                          
------------------------------------------------------------                                                                          
Date     Review By          Comments                                                                          
------   ------------       -------------------------*/                                                                          
-- DROP PROC dbo.Proc_GetCLM_COVERAGE_DESCRIPTION 
CREATE PROC dbo.Proc_GetCLM_COVERAGE_DESCRIPTION 
@CLAIM_ID int
AS                                                                          
BEGIN   

/*
1	HOME	Homeowners	
2	AUTOP	Automobile	
3	CYCL	Motorcycle	
4	BOAT	Watercraft	
5	UMB	Umbrella	
6	REDW	Rental	
7	GENL	General Liability	
*/
                      
DECLARE @CUSTOMER_ID INT
DECLARE @POLICY_ID INT
DECLARE @POLICY_VERSION_ID SMALLINT
DECLARE @LOB_ID INT      
DECLARE @TEMPTABLENAME VARCHAR(100)
DECLARE @TEMPSTR VARCHAR(1000)
DECLARE @DUMMY_POLICY_ID INT

SELECT 
  @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID, @DUMMY_POLICY_ID=DUMMY_POLICY_ID 
FROM 
  CLM_CLAIM_INFO 
WHERE CLAIM_ID=@CLAIM_ID

IF(@DUMMY_POLICY_ID IS NOT NULL)
  BEGIN
     SELECT * FROM MNT_CLAIM_COVERAGE WHERE CLAIM_ID= @CLAIM_ID AND IS_ACTIVE='Y'
  END
ELSE
  BEGIN

	SELECT
		@LOB_ID=POLICY_LOB 
	FROM 
		POL_CUSTOMER_POLICY_LIST 
	WHERE 
		CUSTOMER_ID=@CUSTOMER_ID AND
		POLICY_ID=@POLICY_ID AND
		POLICY_VERSION_ID=@POLICY_VERSION_ID

	IF(@LOB_ID IS NULL OR @LOB_ID=0)
		RETURN
	
	
	
	
	IF(@LOB_ID=1 OR @LOB_ID=6)
		SET @TEMPTABLENAME = 'POL_DWELLING_SECTION_COVERAGES'
	ELSE IF(@LOB_ID=2 OR @LOB_ID=3)
			SET @TEMPTABLENAME = 'POL_VEHICLE_COVERAGES'
	ELSE IF(@LOB_ID=4)
			SET @TEMPTABLENAME = 'POL_WATERCRAFT_COVERAGE_INFO'
	ELSE 
		RETURN
	
	
	SET @TEMPSTR = ' SELECT MC.COV_ID AS ID,MC.COV_DES AS DESCRIPTION FROM ' + 
								@TEMPTABLENAME + ' AS TMP ' + 
								' LEFT JOIN 	MNT_COVERAGE MC ON TMP.COVERAGE_CODE_ID = MC.COV_ID WHERE ' + 
								' TMP.CUSTOMER_ID = ' + CAST(@CUSTOMER_ID AS VARCHAR(50)) + ' AND TMP.POLICY_ID =' + CAST(@POLICY_ID AS VARCHAR(50)) + ' AND	TMP.POLICY_VERSION_ID = ' + CAST(@POLICY_VERSION_ID AS VARCHAR(50))
	
	EXEC (@TEMPSTR)
	
   END
END

GO

