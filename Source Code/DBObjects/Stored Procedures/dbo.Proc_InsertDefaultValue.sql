IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertDefaultValue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertDefaultValue]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name      : dbo.Proc_InsertDefaultValue
Created by      	: Anurag Verma
Date            	: 4/25/2005
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_InsertDefaultValue
(
@DEFV_ENTITY_NAME     varchar(200),
@DEFV_VALUE     varchar(200),
@IS_ACTIVE     nchar(2),
@CREATED_BY     int,
@CREATED_DATETIME     datetime,
@MODIFIED_BY	INT,
@LAST_UPDATED_DATETIME DATETIME,	
@DEFV_ID     int  OUTPUT 
)
AS
BEGIN
declare @DEFVID int 
select @DEFVID=ISNULL(max(DEFV_id),0)+1 from MNT_DEFAULT_VALUE_LIST

INSERT INTO MNT_DEFAULT_VALUE_LIST
(
DEFV_ID,
DEFV_ENTITY_NAME,
DEFV_VALUE,
IS_ACTIVE,
CREATED_BY,
CREATED_DATETIME,
MODIFIED_BY,
LAST_UPDATED_DATETIME
)
VALUES
(
@DEFVID,
@DEFV_ENTITY_NAME,
@DEFV_VALUE,
@IS_ACTIVE,
@CREATED_BY,
@CREATED_DATETIME,
@MODIFIED_BY,
@LAST_UPDATED_DATETIME
)

SELECT @DEFVID=ISNULL(MAX(DEFV_ID),0) FROM MNT_DEFAULT_VALUE_LIST
SET @DEFV_ID=@DEFVID

END


GO

