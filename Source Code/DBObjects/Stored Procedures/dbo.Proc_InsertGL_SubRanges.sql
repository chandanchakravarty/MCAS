IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertGL_SubRanges]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertGL_SubRanges]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.InsertGL_SubRanges
Created by      : Ajit singh Chahal
Date            : 5/12/2005
Purpose    	  :To update  sub ranges for chart of accounts.
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC Dbo.Proc_InsertGL_SubRanges
(
@CATEGORY_ID     smallint output,
@PARENT_CATEGORY_ID     smallint,
@CATEGORY_DESC     varchar(70),
@RANGE_FROM     decimal(7,2),
@RANGE_TO     decimal(7,2),
@IS_ACTIVE     nchar(2),
@CREATED_BY     int,
@CREATED_DATETIME     datetime,
@MODIFIED_BY     int,
@LAST_UPDATED_DATETIME     datetime
)
AS
BEGIN
select  @CATEGORY_ID=isnull(Max(CATEGORY_ID),0)+1 from ACT_GL_ACCOUNT_RANGES
INSERT INTO ACT_GL_ACCOUNT_RANGES
(
CATEGORY_ID,
PARENT_CATEGORY_ID,
CATEGORY_DESC,
RANGE_FROM,
RANGE_TO,
IS_ACTIVE,
CREATED_BY,
CREATED_DATETIME,
MODIFIED_BY,
LAST_UPDATED_DATETIME
)
VALUES
(
@CATEGORY_ID,
@PARENT_CATEGORY_ID,
@CATEGORY_DESC,
@RANGE_FROM,
@RANGE_TO,
@IS_ACTIVE,
@CREATED_BY,
@CREATED_DATETIME,
@MODIFIED_BY,
@LAST_UPDATED_DATETIME
)
END








GO

