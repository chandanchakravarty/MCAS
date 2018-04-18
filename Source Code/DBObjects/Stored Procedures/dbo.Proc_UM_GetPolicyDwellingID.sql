IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UM_GetPolicyDwellingID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UM_GetPolicyDwellingID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_UM_GetPolicyDwellingID            
Created by      : Ravindra
Date            : 03-21-2006

------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/   

CREATE PROCEDURE dbo.Proc_UM_GetPolicyDwellingID
(      
 @CUSTOMER_ID int,      
 @POLICY_ID int,      
 @POLICY_VERSION_ID int,  
 @LOCATION_ID int  
)          
AS               
BEGIN                    
 SELECT DWELLING_ID  
     FROM POL_UMBRELLA_DWELLINGS_INFO       
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID     
  AND IS_ACTIVE='Y' AND LOCATION_ID=@LOCATION_ID       
  
End    
    
  



GO

