IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetProcess_Print_Document]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetProcess_Print_Document]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
proc dbo.Proc_GetProcess_Print_Document
created by 	:Pravesh k. Chandel
dated		:20 feb 2007
purpose		: to get Documens to be printed for the Process
DROP PROC dbo.Proc_GetProcess_Print_Document
select * from MNT_PRINT_DOCUMENT_TYPE
*/

create proc dbo.Proc_GetProcess_Print_Document
(
@PROCESS_ID	INT
)
as
begin
select * from POL_PROCESS_PRINT_DOCUMENT_TYPE where process_id=@PROCESS_ID
end


GO

