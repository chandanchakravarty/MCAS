IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimOwners]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimOwners]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ----------------------------------------------------------          
Proc Name           : Dbo.Proc_GetClaimOwners
Created by            : Amar  
Date                    : 02 May 2006  
Purpose               : To get all the Drivers for the claim
Revison History  :          
Used In                :   Wolverine          
------   ------------       -------------------------*/          
--drop proc Proc_GetClaimOwners
CREATE PROC Dbo.Proc_GetClaimOwners
(          
@CLAIM_ID  int  
)          
AS          
BEGIN         
   
Select Owner_ID, Name From CLM_OWNER_INFORMATION Where Claim_ID = @CLAIM_ID And IS_Active = 'Y' Order by Name
  
END          
        
        
      
    
  


GO

