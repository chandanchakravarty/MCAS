IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAgencyInfo_Inactive]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAgencyInfo_Inactive]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc dbo.Proc_GetAgencyInfo_Inactive 
CREATE PROC dbo.Proc_GetAgencyInfo_Inactive 
(
	@APP_EFFECTIVE_DATE DATETIME
) 
AS  
BEGIN  
  
SELECT AGENCY_ID,(RTRIM(ISNULL (AGENCY_CODE,'')) +'-'+ ISNULL(AGENCY_DISPLAY_NAME,'')) AS AGENCY_DISPLAY_NAME,
	CASE IS_ACTIVE
	WHEN 'Y' THEN RTRIM(ISNULL (AGENCY_CODE,'')) +'-'+ ISNULL(AGENCY_DISPLAY_NAME,'') + '- (Active)'
	WHEN 'N' THEN RTRIM(ISNULL (AGENCY_CODE,'')) +'-'+ ISNULL(AGENCY_DISPLAY_NAME,'') + '- (Inactive)'
	END AS AGENCY_NAME_ACTIVE_STATUS
FROM MNT_AGENCY_LIST 
WHERE 
	ISNULL(DATEDIFF(DAY,TERMINATION_DATE, @APP_EFFECTIVE_DATE),0) > 0 
   --OR ISNULL(DATEDIFF(DAY,TERMINATION_DATE_RENEW, @APP_EFFECTIVE_DATE),0) > 0 -- commented by Pravesh on 12 dec 08 as no check required for Renewal termination date at app level
	
ORDER BY  AGENCY_DISPLAY_NAME ASC  
  
END  









GO

