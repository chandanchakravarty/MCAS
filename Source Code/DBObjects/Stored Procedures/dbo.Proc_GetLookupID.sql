IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLookupID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLookupID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetLookupID
Created by          : Pradeep
Date                : 01/07/2005
Purpose             : Returns the LookuID for a particular Lookup code
Revison History :
Used In             :   Wolverine

------------------------------------------------------------

Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc DBO.Proc_GetLookupID  
CREATE  PROCEDURE DBO.Proc_GetLookupID  
(@LOOKUP_NAME NVarChar(6),@LOOKUP_VALUE_CODE NVarChar(40))  
  
AS  
BEGIN  
 DECLARE @LOOKUP_ID Int  
   
	SELECT @LOOKUP_ID = MLV.LOOKUP_UNIQUE_ID  
	FROM MNT_LOOKUP_VALUES MLV  
		INNER JOIN MNT_LOOKUP_TABLES MLT ON   
	MLV.LOOKUP_ID = MLT.LOOKUP_ID  
	WHERE MLT.LOOKUP_NAME = @LOOKUP_NAME AND  
		MLV.LOOKUP_VALUE_CODE = @LOOKUP_VALUE_CODE   
		AND ISNULL(MLV.IS_ACTIVE,'Y') = 'Y'
 RETURN ISNULL(@LOOKUP_ID,0)  
END  
  
  
  






GO

