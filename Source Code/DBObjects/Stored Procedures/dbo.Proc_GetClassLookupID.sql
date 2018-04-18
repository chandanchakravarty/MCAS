IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClassLookupID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClassLookupID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name          : Dbo.[Proc_GetClassLookupID]
Created by          : Pradeep
Date                : 01/07/2005
Purpose             : Returns the LookuID for a particular Lookup code
Revison History :
Used In             :   Wolverine

------------------------------------------------------------

Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc DBO.[Proc_GetClassLookupID]  
CREATE  PROCEDURE [dbo].[Proc_GetClassLookupID]  
(@LOOKUP_NAME NVarChar(6),@LOOKUP_VALUE_CODE NVarChar(40),@EFFECTIVEDATE nvarchar(50),@STATE int)  
  
AS  
BEGIN  
 DECLARE @LOOKUP_ID Int  
	IF( @EFFECTIVEDATE IS NULL)
	BEGIN

		SELECT @LOOKUP_ID = MLV.LOOKUP_UNIQUE_ID  
		FROM MNT_LOOKUP_VALUES MLV  
			INNER JOIN MNT_LOOKUP_TABLES MLT ON   
		MLV.LOOKUP_ID = MLT.LOOKUP_ID  
		WHERE MLT.LOOKUP_NAME = @LOOKUP_NAME AND  
			MLV.LOOKUP_VALUE_CODE = @LOOKUP_VALUE_CODE   
			AND ISNULL(MLV.IS_ACTIVE,'Y') = 'Y' and Type = convert(nvarchar(50),@STATE) and LOOKUP_FRAME_OR_MASONRY IS NULL
	END
	ELSE
	BEGIN
		IF(@STATE = 14)
		begin	
			SELECT @LOOKUP_ID = MLV.LOOKUP_UNIQUE_ID  
			FROM MNT_LOOKUP_VALUES MLV  
			INNER JOIN MNT_LOOKUP_TABLES MLT ON   
			MLV.LOOKUP_ID = MLT.LOOKUP_ID  
			WHERE MLT.LOOKUP_NAME = @LOOKUP_NAME AND  
			MLV.LOOKUP_VALUE_CODE = @LOOKUP_VALUE_CODE   
			AND ISNULL(MLV.IS_ACTIVE,'Y') = 'Y' and 
			Type = convert(nvarchar(50),@STATE)  and LOOKUP_FRAME_OR_MASONRY IS NOT NULL
		END
		ELSE
		BEGIN	
			SELECT @LOOKUP_ID = MLV.LOOKUP_UNIQUE_ID  
			FROM MNT_LOOKUP_VALUES MLV  
			INNER JOIN MNT_LOOKUP_TABLES MLT ON   
			MLV.LOOKUP_ID = MLT.LOOKUP_ID  
			WHERE MLT.LOOKUP_NAME = @LOOKUP_NAME AND  
			MLV.LOOKUP_VALUE_CODE = @LOOKUP_VALUE_CODE   
			AND ISNULL(MLV.IS_ACTIVE,'Y') = 'Y' and 
			Type = convert(nvarchar(50),@STATE)  and LOOKUP_FRAME_OR_MASONRY IS NULL
			
		END
	END
 RETURN ISNULL(@LOOKUP_ID,0)  
END  




GO

