IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLOBId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLOBId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : Dbo.Proc_GetLOBId    
Created by      : Pradeep    
Date            : 9/5/2005    
Purpose       :Gets the LOBID for the passed code   
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE  PROCEDURE Proc_GetLOBId  
(  
 @LOB_CODE VarChar(20)  
)  
AS  
  
DECLARE @LOB_ID SmallInt  
  
SELECT @LOB_ID = LOB_ID  
FROM MNT_LOB_MASTER  
WHERE LOB_CODE = @LOB_CODE  
  
RETURN ISNULL(@LOB_ID,0)  
  
  
  
  
  
  



GO

