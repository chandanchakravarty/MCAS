IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyUmbrellaFarmInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyUmbrellaFarmInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
              
Proc Name       : Proc_DeletePolicyUmbrellaFarmInfo              
Created by      : Sumit Chhabra              
Date            : 23/03/2006              
Purpose         : Delete data of Farm Info  
Revison History :              
Used In         : Wolverine              
      
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
CREATE PROC Proc_DeletePolicyUmbrellaFarmInfo  
(      
 @CUSTOMER_ID              int,      
 @POLICY_ID                   int,      
 @POLICY_VERSION_ID           smallint,      
 @FARM_ID               smallint       
)      
AS      
      
BEGIN      
  
 DELETE FROM POL_UMBRELLA_FARM_INFO WHERE       
  CUSTOMER_ID=@CUSTOMER_ID AND         
  POLICY_ID=@POLICY_ID AND                     
  POLICY_VERSION_ID=@POLICY_VERSION_ID AND    
  FARM_ID =@FARM_ID         
    
END    



GO

