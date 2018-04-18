IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLobStateName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLobStateName]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/******************
Name : Proc_GetLobStateName
Created By: Kranti Prakash Singh
Date	:   25-Apr-2007
Purpose: To get State Name for a Lob
***********************/
--drop PROCEDURE dbo.Proc_GetLobStateName
CREATE PROCEDURE dbo.Proc_GetLobStateName
(
@LOB_ID varchar(20)
)
AS
BEGIN
SELECT DISTINCT L.STATE_ID,S.STATE_NAME 
FROM MNT_LOB_STATE L INNER JOIN MNT_COUNTRY_STATE_LIST S ON L.STATE_ID=S.STATE_ID 
where L.LOB_ID=@LOB_ID
END



GO

