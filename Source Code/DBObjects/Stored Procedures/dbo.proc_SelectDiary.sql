IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_SelectDiary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_SelectDiary]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc proc_SelectDiary



create     procedure proc_SelectDiary
(
@LISTID		numeric(9)
)
as
BEGIN
		Select	
		LISTID,
		RECBYSYSTEM,
		RECDATE,
		FOLLOWUPDATE,
		LISTTYPEID,
		POLICYBROKERID,
		SUBJECTLINE,
		LISTOPEN,
		SYSTEMFOLLOWUPID,
		PRIORITY,
		TOUSERID,
		FROMUSERID,
		STARTTIME,
		ENDTIME,
		NOTE,
		PROPOSALVERSION,
		QUOTEID,
		CLAIMID,
		CLAIMMOVEMENTID,
		TOENTITYID,
		FROMENTITYID
	FROM 	TODOLIST
	WHERE 	LISTID		=	@LISTID
END








GO

