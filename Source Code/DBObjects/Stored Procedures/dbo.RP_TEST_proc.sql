IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RP_TEST_proc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RP_TEST_proc]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc RP_TEST_proc
as
begin
select top 5 * from RP_TEST
end 


GO

