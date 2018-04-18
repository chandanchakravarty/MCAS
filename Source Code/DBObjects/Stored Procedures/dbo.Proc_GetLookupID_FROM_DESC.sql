IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLookupID_FROM_DESC]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLookupID_FROM_DESC]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetLookupID_FROM_DESC  
Created by          : Pradeep  
Date                : 01/07/2005  
Purpose             : Returns the LookuID for a particular Lookup code  
Revison History :  
Used In             :   Wolverine  
  
------------------------------------------------------------  
  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE  PROCEDURE Proc_GetLookupID_FROM_DESC    
(@LOOKUP_NAME NVarChar(6),@LOOKUP_VALUE_DESC NVarChar(100))    
    
AS    
BEGIN    
 DECLARE @LOOKUP_ID Int    
     
 SELECT @LOOKUP_ID = MLV.LOOKUP_UNIQUE_ID    
 FROM MNT_LOOKUP_VALUES MLV    
  INNER JOIN MNT_LOOKUP_TABLES MLT ON     
 MLV.LOOKUP_ID = MLT.LOOKUP_ID    
 WHERE MLT.LOOKUP_NAME = @LOOKUP_NAME AND    
  MLV.LOOKUP_VALUE_DESC = @LOOKUP_VALUE_DESC    
   
 RETURN ISNULL(@LOOKUP_ID,0)    
END    
    
    
    
  



GO

