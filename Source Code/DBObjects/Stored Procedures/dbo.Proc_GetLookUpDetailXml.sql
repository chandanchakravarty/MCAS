IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLookUpDetailXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLookUpDetailXml]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetLookUpDetailXml
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_GetLookUpDetailXml
(
	@LookUpUniqueId int	
)
AS
BEGIN
--SELECT  LOOKUP_VALUE_CODE,LOOKUP_VALUE_DESC 
--FROM MNT_LOOKUP_VALUES 
--WHERE LOOKUP_UNIQUE_ID=@LookUpUniqueId
SELECT  T1.LOOKUP_ID LOOKUP_ID,T2.LOOKUP_VALUE_CODE LOOKUP_VALUE_CODE,T2.LOOKUP_VALUE_DESC LOOKUP_VALUE_DESC,T2.IS_ACTIVE
FROM MNT_LOOKUP_TABLES T1,MNT_LOOKUP_VALUES T2
WHERE T1.LOOKUP_ID=T2.LOOKUP_ID AND T2.LOOKUP_UNIQUE_ID=@LookUpUniqueId
END


GO

