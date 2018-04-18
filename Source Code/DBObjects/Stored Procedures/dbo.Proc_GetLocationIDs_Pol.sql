IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLocationIDs_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLocationIDs_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
       
/* ----------------------------------------------------------                
Proc Name     : dbo.Proc_GetLocationIDs_Pol      
Created by   : Ashwani      
Date         : 02 Mar 2006
Purpose      : Get the location IDs  for HO policy rules  
Revison History  :                      
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/                
CREATE PROCEDURE dbo.Proc_GetLocationIDs_Pol      
(      
 @CUSTOMER_ID int,      
 @POLICY_ID int,      
 @POLICY_VERSION_ID int      
)          
AS               
BEGIN                
	
	SELECT   LOCATION_ID      
	FROM       POL_LOCATIONS       
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID     
		AND IS_ACTIVE='Y'       
	ORDER BY   LOCATION_ID                  
End    
    
  



GO

