IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetBodyTypeFromVINMASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetBodyTypeFromVINMASTER]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetBodyTypeFromVINMASTER
Created by           : Nidhi
Date                    : 29/04/2005
Purpose               : To get Make from VINMASTER
Revison History :
Used In                :   Wolverine

  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetBodyTypeFromVINMASTER
(
@MODEL_YEAR	NVARCHAR (5) ,
@MAKE_CODE          nvarchar (150),
@SERIES_NAME	nvarchar (150)=null

)
AS
if (@SERIES_NAME is not null)

BEGIN
	SELECT   DISTINCT BODY_TYPE 
	FROM         MNT_VIN_MASTER  
	WHERE     (MODEL_YEAR= @MODEL_YEAR AND MAKE_CODE=@MAKE_CODE  AND SERIES_NAME=@SERIES_NAME)
order by body_type
END

ELSE
BEGIN
	SELECT   DISTINCT BODY_TYPE 
	FROM         MNT_VIN_MASTER  
	WHERE     (MODEL_YEAR= @MODEL_YEAR AND MAKE_CODE=@MAKE_CODE )
order by body_type
END


GO

