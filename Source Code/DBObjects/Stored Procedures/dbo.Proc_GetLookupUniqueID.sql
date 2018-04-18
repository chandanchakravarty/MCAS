IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLookupUniqueID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLookupUniqueID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name          : Dbo.Proc_GetLookupUniqueID      
Created by          : kranti      
Date                : 01/07/2005      
Purpose             : Returns the LookupUniqueID for a particular Lookup_value_code      
Revison History :      
Used In             :   Wolverine      
      
------------------------------------------------------------      
      
Date     Review By          Comments      
--drop PROCEDURE Proc_GetLookupUniqueID    
------   ------------       -------------------------*/      
CREATE  PROCEDURE Proc_GetLookupUniqueID        
(@LOOKUP_VALUE_CODE NVarChar(40),  
 @LOB_ID  int    
)        
        
AS        
BEGIN        
 DECLARE @LOOKUP_ID int        
  
IF  (@LOB_ID = 1 OR @LOB_ID =6)    
BEGIN    
SELECT @LOOKUP_ID = MLV.LOOKUP_UNIQUE_ID    
  FROM MNT_LOOKUP_VALUES MLV    
  INNER JOIN MNT_LOOKUP_TABLES MLT ON     
   MLV.LOOKUP_ID = MLT.LOOKUP_ID    
  WHERE MLV.TYPE = @LOOKUP_VALUE_CODE AND MLT.LOOKUP_NAME = 'BLCODE' AND MLV.Lookup_unique_id in(8459,11150,11191,11276,11277,11278)    
  ORDER BY MLV.LOOKUP_VALUE_DESC ASC    
    
    
END    
ELSE    
BEGIN    
SELECT @LOOKUP_ID = MLV.LOOKUP_UNIQUE_ID  
  FROM MNT_LOOKUP_VALUES MLV    
  INNER JOIN MNT_LOOKUP_TABLES MLT ON     
   MLV.LOOKUP_ID = MLT.LOOKUP_ID    
  WHERE MLV.TYPE = @LOOKUP_VALUE_CODE AND MLT.LOOKUP_NAME = 'BLCODE' AND MLV.Lookup_unique_id in(8459,8460,11191)    
  ORDER BY MLV.LOOKUP_VALUE_DESC ASC    
END           
  
 RETURN ISNULL(@LOOKUP_ID,0)        
END      




GO

