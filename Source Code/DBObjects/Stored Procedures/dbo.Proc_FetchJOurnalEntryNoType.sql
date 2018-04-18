IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchJOurnalEntryNoType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchJOurnalEntryNoType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc dbo.Proc_FetchJOurnalEntryNoType
create procedure dbo.Proc_FetchJOurnalEntryNoType 
(
 @JOURNAL_ID int
)
AS

select 'Jounal Entry No :' + JOURNAL_ENTRY_NO + ';' + ' Journal Type : ' + 

Case
 when JOURNAL_GROUP_TYPE = 'ML' then 'Manual'
 when JOURNAL_GROUP_TYPE = 'RC' then 'Recurring'
 when JOURNAL_GROUP_TYPE = 'TP' then 'Template' else '' end  as CUST_INFO

from ACT_JOURNAL_MASTER  with(nolock)
where JOURNAL_ID = @JOURNAL_ID







GO

