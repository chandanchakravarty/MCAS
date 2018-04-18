IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERT_SMOKER_COVERAGE_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_INSERT_SMOKER_COVERAGE_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC PROC_INSERT_SMOKER_COVERAGE_ACORD
(                                        
 @CUSTOMER_ID     INT,                                        
 @APP_ID     INT,                                        
 @APP_VERSION_ID     SMALLINT                                                         
)                                        
AS                                        

BEGIN
	INSERT INTO APP_DWELLING_SECTION_COVERAGES
	(CUSTOMER_ID, APP_ID,APP_VERSION_ID, DWELLING_ID ,COVERAGE_ID, COVERAGE_CODE_ID)
	VALUES (@CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,1,937,937)
END


GO

