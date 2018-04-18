IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePrimaryNamedInsured]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePrimaryNamedInsured]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_DeletePrimaryNamedInsured    
Created by         : Vijay Arora  
Date               : 28-10-2005  
Purpose            :     
Revison History :    
Used In            :   Wolverine      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE   PROCEDURE Proc_DeletePrimaryNamedInsured    
(    
 @CUSTOMER_ID int,    
 @POLICY_ID int,    
 @POLICY_VERSION_ID int    
)    
AS    
BEGIN    
DELETE FROM  POL_APPLICANT_LIST    
WHERE     
   CUSTOMER_ID=@CUSTOMER_ID    AND     
   POLICY_ID=@POLICY_ID              AND    
   POLICY_VERSION_ID =@POLICY_VERSION_ID    
    
END    
  



GO

