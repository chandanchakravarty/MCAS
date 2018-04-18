IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Fetch_Followup_Details]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Fetch_Followup_Details]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Fetch_Followup_Details

/*
Created By	:	Anurag Verma
Created	On	:	20/03/2007
Purpose		:	Fetch follow up details from mnt_diary details according to module id and lob_id


*/

CREATE PROC dbo.Proc_Fetch_Followup_Details
(
	@moduleID int,
	@lob_id int
	
)
AS
BEGIN
SELECT MDD_LOB_ID,MDD_DIARYTYPE_ID,ISNULL(MDD_USERGROUP_ID,'') MDD_USERGROUP_ID,ISNULL(MDD_USERLIST_ID,'') MDD_USERLIST_ID,ISNULL(MDD_SUBJECTLINE,'') MDD_SUBJECTLINE,ISNULL(MDD_PRIORITY,'') MDD_PRIORITY
FROM MNT_DIARY_DETAILS MDD 
LEFT OUTER JOIN TODOLISTTYPES TDL ON MDD.MDD_DIARYTYPE_ID=TDL.TYPEID
WHERE TDL.TYPE_FLAG='F' AND MDD_MODULE_ID=@moduleID AND MDD_LOB_ID=@lob_id 
ORDER BY tdl.typedesc
END








GO

