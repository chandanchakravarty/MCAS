IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_getLOOKUP_FRAME_OR_MASONRY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_getLOOKUP_FRAME_OR_MASONRY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc Proc_getLOOKUP_FRAME_OR_MASONRY
as
begin 
	select LOOKUP_UNIQUE_ID , isnull(LOOKUP_FRAME_OR_MASONRY,'-') as LOOKUP_FRAME_OR_MASONRY
	from mnt_lookup_values with (NoLock)
	where lookup_id = 1002
end



GO

