IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMinPremiumForEnforsement]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMinPremiumForEnforsement]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------------
Created By		: Ravindra Gupta
Created Date	: 04-15-2008
Purpose			: Fetch Minimum Endorsement Premium which would be posted in GL
Revision History
Revision Date		 
Purpose
-----------------------------------------------------------------------*/
-- drop proc dbo.Proc_GetMinPremiumForEnforsement
CREATE PROC dbo.Proc_GetMinPremiumForEnforsement
AS
BEGIN 
	SELECT ISNULL(SYS_MIN_ENDORSEMENT,0) AS MIN_END_PREM FROM MNT_SYSTEM_PARAMS
END

GO

