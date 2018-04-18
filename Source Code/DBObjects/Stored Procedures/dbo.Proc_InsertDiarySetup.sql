IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertDiarySetup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertDiarySetup]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*
CREATED BY	: ANURAG VERMA
CREATED ON	: 05/03/2007
PURPOSE		: tO STORE DIARY DETAILS ACCORDING TO MODULE AND DIARY TYPE
*/

-- drop procedure dbo.Proc_InsertDiarySetup    
CREATE procedure dbo.Proc_InsertDiarySetup    
(    

@MDD_FOLLOWUP numeric(3)= NULL,    
@MDD_DIARYTYPE_ID  numeric(9) = NULL,    
@MDD_SUBJECTLINE  varchar(512) = NULL,    
--@MDD_NOTIFICATION_LIST numeric(9) = NULL,    
@MDD_PRIORITY  nchar(1) = NULL,    
@MDD_MODULE_ID int,
@MDD_LOB_ID int,
@MDD_USERGROUP_ID nvarchar(1000),
@MDD_USERLIST_ID nvarchar(1000),
@MDD_IS_ACTIVE nchar(2),
@MDD_CREATED_DATETIME datetime=null,
@MDD_CREATED_BY int=null,
@MDD_LAST_UPDATED_DATETIME datetime=null,
@MDD_MODIFIED_BY int=null
)    
as    

BEGIN    
 --use if identity is false    
 --SELECT @newlistid = (IsNull(Max(listid),0)+1) FROM TODOLIST    

-- Added by Asfa Praveen (04-Feb-2008) - iTrack issue #3526 Part 1.
IF @MDD_FOLLOWUP= -1 
BEGIN
  SET @MDD_FOLLOWUP=0
END
 INSERT into MNT_DIARY_DETAILS    
 (    
	MDD_MODULE_ID,
	MDD_DIARYTYPE_ID,
	MDD_LOB_ID,
	MDD_USERGROUP_ID,
	MDD_USERLIST_ID,
	MDD_FOLLOWUP,
	MDD_SUBJECTLINE,
	MDD_PRIORITY,
--	MDD_NOTIFICATION_LIST,
	MDD_IS_ACTIVE,
	MDD_CREATED_DATETIME,
	MDD_CREATED_BY
 )    
 values    
 (    
	@MDD_MODULE_ID,
	@MDD_DIARYTYPE_ID,
	@MDD_LOB_ID,
	@MDD_USERGROUP_ID,
	@MDD_USERLIST_ID,
	@MDD_FOLLOWUP,
	@MDD_SUBJECTLINE,
	@MDD_PRIORITY,
	--@MDD_NOTIFICATION_LIST,
	@MDD_IS_ACTIVE,
	@MDD_CREATED_DATETIME,
	@MDD_CREATED_BY
 )    
     
END    
  










GO

