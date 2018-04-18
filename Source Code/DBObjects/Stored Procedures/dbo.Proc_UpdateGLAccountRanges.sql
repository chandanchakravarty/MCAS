IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateGLAccountRanges]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateGLAccountRanges]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.UpdateGLAccountRanges
Created by      : Ajit Singh Chahal
Date            : 5/11/2005
Purpose    	  :To update records  od Chart Of Account ranges.
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc dbo.Proc_UpdateGLAccountRanges
CREATE PROC dbo.Proc_UpdateGLAccountRanges
(
@CATEGORY_ID     smallint,
@RANGE_FROM     decimal(7,2),
@RANGE_TO     decimal(7,2),
@MODIFIED_BY     int,
@LAST_UPDATED_DATETIME     datetime
)
AS
BEGIN
declare @count int
select @count=count(*) from ACT_GL_ACCOUNT_RANGES (nolock) where PARENT_CATEGORY_ID = @CATEGORY_ID
and (@RANGE_FROM > RANGE_FROM or @RANGE_TO <RANGE_TO)
if @count=0
  Begin	
	update  ACT_GL_ACCOUNT_RANGES
	set
	RANGE_FROM = @RANGE_FROM,
	RANGE_TO=@RANGE_TO,
	MODIFIED_BY=@MODIFIED_BY,
	LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME
	where CATEGORY_ID = @CATEGORY_ID
  End
END





GO

