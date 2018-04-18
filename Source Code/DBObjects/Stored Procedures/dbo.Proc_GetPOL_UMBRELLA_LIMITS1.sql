IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPOL_UMBRELLA_LIMITS1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPOL_UMBRELLA_LIMITS1]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  ----------------------------------------------------------        
Proc Name       : dbo.Proc_GetPOL_UMBRELLA_LIMITS1    
Created by      : Ravindra  
Date            : 03-22-2006  
Purpose         : Selects a single record from UMBRELLA_LIITS        
Revison History :        
Modified by 	:Pravesh 
Modified Date	:13 june 2007
Purpose		: fetch new Column CLIENT_UPDATE_DATE as well
Used In         : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------       
*/    
--DROP    PROCEDURE dbo.Proc_GetPOL_UMBRELLA_LIMITS1     
CREATE      PROCEDURE dbo.Proc_GetPOL_UMBRELLA_LIMITS1    
(    
 @CUSTOMER_ID int,    
 @POLICY_ID int,    
 @POLICY_VERSION_ID smallint    
)    
    
As    
    
SELECT CUSTOMER_ID,    
 POLICY_ID,    
 POLICY_VERSION_ID,    
 POLICY_LIMITS,    
 RETENTION_LIMITS,    
 UNINSURED_MOTORIST_LIMIT,    
 UNDERINSURED_MOTORIST_LIMIT,    
 OTHER_LIMIT,    
 OTHER_DESCRIPTION,
 TERRITORY,    
 CREATED_DATETIME ,
 convert(varchar,CLIENT_UPDATE_DATE,101) as CLIENT_UPDATE_DATE
FROM POL_UMBRELLA_LIMITS    
WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
 POLICY_ID = @POLICY_ID AND    
 POLICY_VERSION_ID = @POLICY_VERSION_ID    
    
    
    
    
    
    
    
    
    
  





GO

