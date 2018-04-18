IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimDrivers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimDrivers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/* ----------------------------------------------------------            
Proc Name           : Dbo.GetClaimDrivers  
Created by            : Amar    
Date                    : 02 May 2006    
Purpose               : To get all the Drivers for the claim  
Revison History  :            
Used In                :   Wolverine            
------   ------------       -------------------------*/            
--drop proc Proc_GetClaimDrivers  
CREATE PROC dbo.Proc_GetClaimDrivers  
(            
@CLAIM_ID  int    
)            
AS            
BEGIN           
     
Select Driver_ID, Name From CLM_DRIVER_INFORMATION Where Claim_ID = @CLAIM_ID And IS_Active = 'Y' Order by Name  
    
END        

          
          
        
      
    




GO

