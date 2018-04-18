IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateLookUpValue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateLookUpValue]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetBodyTypeFromVINMASTER
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_UpdateLookUpValue
(
	@LOOKUP_UNIQUE_ID     int,
	--@LOOKUP_ID     smallint,
	--@LOOKUP_VALUE_ID     smallint,
	@LOOKUP_VALUE_CODE     nvarchar(80),
	@LOOKUP_VALUE_DESC     nvarchar(200)--,
	--@LOOKUP_SYS_DEF     nchar(2),
	--@IS_ACTIVE     nchar(2),
	--@LAST_UPDATED_DATETIME     datetime,
	--@LOOKUP_FRAME_OR_MASONRY     varchar(1),
	--@Type     nvarchar(2)
)
AS
BEGIN
UPDATE MNT_LOOKUP_VALUES
SET  LOOKUP_VALUE_CODE=@LOOKUP_VALUE_CODE ,LOOKUP_VALUE_DESC =@LOOKUP_VALUE_DESC 
WHERE LOOKUP_UNIQUE_ID =@LOOKUP_UNIQUE_ID 
END


GO

