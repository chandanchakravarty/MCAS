IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHomeCoverageLimit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHomeCoverageLimit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
Proc Name        : dbo.Proc_GetHomeCoverageLimit    
Created by         : Praveen kasana     
Date               :       
Purpose            : Fetches Home Coverages from QQ Watercraft
			Coverage E - Personal Liability Each Occurrence 
	         	Coverage F - Medical Payment Each Person       
Revison History    :      
Used In            : Wolverine        
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/  
--drop proc  dbo.Proc_GetHomeCoverageLimit 1009,103,1
CREATE PROC dbo.Proc_GetHomeCoverageLimit 
(
 @CUSTOMER_ID INT,
 @APP_ID INT,
 @APP_VERSION_ID INT
)
AS
BEGIN


DECLARE @STATE_ID INT
DECLARE @LIMIT_PL INT
DECLARE @LIMIT_MEDPM INT
DECLARE @COV_PL_ID INT
DECLARE @COV_MEDPM_ID INT


SELECT 
@STATE_ID = STATE_ID
FROM APP_LIST WITH(NOLOCK)
WHERE 
@CUSTOMER_ID  = CUSTOMER_ID 
AND APP_ID = @APP_ID
AND APP_VERSION_ID = @APP_VERSION_ID


SELECT @COV_PL_ID = COV_ID FROM MNT_COVERAGE WITH(NOLOCK)
WHERE STATE_ID = @STATE_ID AND
LOB_ID = 1 --HOME
AND COV_CODE = 'PL'

SELECT @COV_MEDPM_ID = COV_ID FROM MNT_COVERAGE
WHERE STATE_ID = @STATE_ID AND
LOB_ID = 1 -- HOME
AND COV_CODE = 'MEDPM'


SELECT @LIMIT_PL = ISNULL(LIMIT_1,0) FROM APP_DWELLING_SECTION_COVERAGES WITH(NOLOCK)
WHERE CUSTOMER_ID = @CUSTOMER_ID 
AND APP_ID = @APP_ID
AND APP_VERSION_ID = @APP_VERSION_ID
AND COVERAGE_CODE_ID = @COV_PL_ID

SELECT @LIMIT_MEDPM =ISNULL(LIMIT_1,0) FROM APP_DWELLING_SECTION_COVERAGES WITH(NOLOCK)
WHERE CUSTOMER_ID = @CUSTOMER_ID 
AND APP_ID = @APP_ID
AND APP_VERSION_ID = @APP_VERSION_ID
AND COVERAGE_CODE_ID = @COV_MEDPM_ID

SELECT 
ISNULL(@LIMIT_PL,0) AS PL_LIMIT,
CAST(ISNULL(@LIMIT_MEDPM,0) as varchar) + '/' + '25000' AS MEDPM_LIMIT


-- select * from mnt_coverage where cov_id in (170,171)
-- select * from mnt_coverage where cov_code in ('PL','MEDPM')
-- and lob_id = 1
--Proc_GetHomeCoverageLimit 'H1003791APP' ,98,1
-- select * from app_list where customer_id =1009 and app_id=103 and app_version_id=1
-- select * from APP_DWELLING_SECTION_COVERAGES where customer_id =1009 and app_id=103 and app_version_id=1
-- SELECT ISNULL(LIMIT_1,0) FROM APP_DWELLING_SECTION_COVERAGES WITH(NOLOCK)
-- WHERE CUSTOMER_ID = 1009 
-- AND APP_ID = 103
-- AND APP_VERSION_ID = 1



END









GO

