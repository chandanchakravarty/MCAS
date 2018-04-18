

IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[Proc_ReAssignDiaryTo_PreviouslyEscalated]')
          and OBJECTPROPERTY(id, N'IsProcedure')= 1)
BEGIN
     DROP PROCEDURE Proc_ReAssignDiaryTo_PreviouslyEscalated
 END
