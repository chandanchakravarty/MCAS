IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertDefaultHierarchy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertDefaultHierarchy]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name     : Proc_InsertDefaultHierarchy
Created by      : Gaurav Tyagi
Date            : 9/5/2005
Purpose    	  :To insert record in default hierarchy.
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Proc_InsertDefaultHierarchy
(
@REC_ID     int out,
@AGENCY_ID     int,
@DIV_ID     int,
@DEPT_ID     int,
@PC_ID     int,
@IS_ACTIVE     nchar(1),
@CREATED_BY     int,
@CREATED_DATETIME     datetime
)
AS
BEGIN
select @REC_ID=isnull(Max(REC_ID),0)+1 from MNT_DEFAULT_HIERARCHY
INSERT INTO MNT_DEFAULT_HIERARCHY
(
REC_ID,
AGENCY_ID,
DIV_ID,
DEPT_ID,
PC_ID,
IS_ACTIVE,
CREATED_BY,
CREATED_DATETIME
)
VALUES
(
@REC_ID,
@AGENCY_ID,
@DIV_ID,
@DEPT_ID,
@PC_ID,
@IS_ACTIVE,
@CREATED_BY,
@CREATED_DATETIME

)
END


GO

