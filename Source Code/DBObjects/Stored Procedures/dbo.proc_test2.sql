IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_test2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_test2]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--DROP PROC proc_test2 
create proc proc_test2 
(
@A Int =null ,
@B int = null
)
as
BEGIN
DECLARE @QUERY  Varchar(8000)

SET @QUERY = ' SET @A= 1;SET @B = @A; SELECT @A;'
--select @QUERY
--print @QUERY
EXECUTE (@QUERY)
--SET @A= 1;SET @B = @A; SELECT @A;
end


GO

