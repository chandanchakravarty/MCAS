IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyMotorCycle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyMotorCycle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*------------------------------------------------------------------------------------------------    
Proc Name           : Dbo.Proc_GetPolicyMotorCycle    
Created by            : Ashwini    
Date                    : 10 Nov.,2005    
Purpose               : For fetching the motor cycles for the input parameters.      
Revison History  :    
Used In                 :   Wolverine      
---------------------------------------------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       ------------------------------------------------------------------------*/    
create   PROCEDURE Proc_GetPolicyMotorCycle    
(    
@POLICY_ID int,    
@CUSTOMER_ID int,    
@POLICY_VERSION_ID int     
)    
AS    
BEGIN    
SELECT VEHICLE_ID, CONVERT(VARCHAR,VEHICLE_ID) +'-'+ CONVERT(VARCHAR,MAKE)  VEHICLE_DESC     
FROM POL_VEHICLES WHERE     
CUSTOMER_ID=@CUSTOMER_ID   AND  POLICY_ID= @POLICY_ID    AND POLICY_VERSION_ID= @POLICY_VERSION_ID  and upper(isnull(is_active, 'N'))='Y' 
END    
  



GO

