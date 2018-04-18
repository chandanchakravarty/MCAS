IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetViolationIDs_Policy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetViolationIDs_Policy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name   : Proc_GetViolationIDs_Policy      
Created by  : praveen Singh       
Date        : 01 January ,2006      
Purpose     : Get the Violation IDs  for motorCycle in policy     
Revison History  :                      
 ------------------------------------------------------------                            
Date     Review By          Comments                          
      
*/    
    
CREATE PROCEDURE dbo.Proc_GetViolationIDs_Policy        
(        
 @CUSTOMER_ID int,        
 @POL_ID int,        
 @POL_VERSION_ID int,        
 @DRIVER_ID int        
)            
AS                 
BEGIN                  
SELECT   POL_MVR_ID        
FROM       POL_MVR_INFORMATION with(nolock)        
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID  and DRIVER_ID=@DRIVER_ID        
		AND ISNULL(IS_ACTIVE,'Y')='Y'
   ORDER BY   POL_MVR_ID                    
End     




GO

