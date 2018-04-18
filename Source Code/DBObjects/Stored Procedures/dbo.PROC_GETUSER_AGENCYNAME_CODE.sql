IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETUSER_AGENCYNAME_CODE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETUSER_AGENCYNAME_CODE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN
--
--DROP PROC PROC_GETUSER_AGENCYNAME_CODE
--
--GO

CREATE PROC PROC_GETUSER_AGENCYNAME_CODE
( 
  @USERID INT
)

AS

BEGIN

SELECT AGENCY_CODE,AGENCY_DISPLAY_NAME FROM MNT_AGENCY_LIST WHERE AGENCY_CODE = (SELECT USER_SYSTEM_ID FROM MNT_USER_LIST WHERE USER_ID = @USERID)

END

--GO
--PROC_GETUSER_AGENCYNAME_CODE 349
--
--ROLLBACK TRAN
GO

