IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyWatercraftEngineInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyWatercraftEngineInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name           : Dbo.Proc_GetPolicyWatercraftEngineInfo    
Created by            : Vijay Arora  
Date                    : 22-11-2005  
Purpose                : To get the vehicle  information  from pol_watercraft_engine_info  table    
Revison History  :    
Used In                 :   Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
-- drop proc dbo.Proc_GetPolicyWatercraftEngineInfo    
CREATE  PROC dbo.Proc_GetPolicyWatercraftEngineInfo    
(    
@CUSTOMER_ID  int,    
@POLICY_ID  int,    
@POLICY_VERSION_ID int,    
@ENGINE_ID  int    
)    
AS    
BEGIN    
    
    
SELECT      
ENGINE_NO,    
YEAR,    
MAKE,    
MODEL,    
SERIAL_NO,    
HORSEPOWER,    
INSURING_VALUE,    
ASSOCIATED_BOAT,  
FUEL_TYPE,    
OTHER,
IS_ACTIVE    
FROM   POL_WATERCRAFT_ENGINE_INFO    
WHERE  CUSTOMER_ID = @CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND ENGINE_ID= @ENGINE_ID;    
END    
    
  
  
  



GO

