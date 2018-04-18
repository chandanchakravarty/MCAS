IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateDefaultHierarchy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateDefaultHierarchy]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : Proc_UpdateDefaultHierarchy
Created by      : Gaurav
Date            : 9/5/2005
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Proc_UpdateDefaultHierarchy
(
@REC_ID     int,
@AGENCY_ID     int,
@DIV_ID     int,
@DEPT_ID     int,
@PC_ID     int,
@IS_ACTIVE nchar(1),
@MODIFIED_BY     int,
@LAST_UPDATED_DATETIME     datetime
)
AS
BEGIN
Update  MNT_DEFAULT_HIERARCHY
set
AGENCY_ID  =  @AGENCY_ID,
DIV_ID  =  @DIV_ID,
DEPT_ID  =  @DEPT_ID,
PC_ID  =  @PC_ID,
IS_ACTIVE = @IS_ACTIVE,
MODIFIED_BY  =  @MODIFIED_BY,
LAST_UPDATED_DATETIME  =  @LAST_UPDATED_DATETIME

where 	REC_ID = @REC_ID

END




GO

