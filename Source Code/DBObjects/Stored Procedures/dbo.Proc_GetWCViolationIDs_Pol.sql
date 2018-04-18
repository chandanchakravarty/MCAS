IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWCViolationIDs_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWCViolationIDs_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


      
/* ---------------------------------------------------------                
Proc Name   : dbo.Proc_GetWCViolationIDs_Pol      
Created by  : Ashwini      
Date        : 13 Mar ,2006  
Purpose     : Get the WC driver's IDs     
Revison History  :                      
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
DROP PROCEDURE dbo.Proc_GetWCViolationIDs_Pol                  
------   ------------       -------------------------*/                
CREATE PROCEDURE [dbo].[Proc_GetWCViolationIDs_Pol]  
(      
 @CUSTOMER_ID int,      
 @POLICY_ID int,      
 @POLICY_VERSION_ID int,      
 @DRIVER_ID int      
)          
AS               
BEGIN                
      
 SELECT APP_WATER_MVR_ID      
 FROM POL_WATERCRAFT_MVR_INFORMATION       
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  and DRIVER_ID=@DRIVER_ID  
 AND ISNULL(IS_ACTIVE,'Y')='Y'      
 ORDER BY   APP_WATER_MVR_ID                  
End    
    
  
  
  
GO

