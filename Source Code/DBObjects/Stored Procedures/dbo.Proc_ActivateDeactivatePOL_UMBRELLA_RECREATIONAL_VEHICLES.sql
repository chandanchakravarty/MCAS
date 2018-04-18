IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePOL_UMBRELLA_RECREATIONAL_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePOL_UMBRELLA_RECREATIONAL_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name   : dbo.Proc_ActivateDeactivatePOL_UMBRELLA_RECREATIONAL_VEHICLES         
Created by  : Sumit Chhabra          
Date        : 23 March,2006        
Purpose     :           
Revison History  :                
 ------------------------------------------------------------                      
Date     Review By          Comments                    
           
------   ------------       -------------------------*/    
  
CREATE    PROCEDURE Proc_ActivateDeactivatePOL_UMBRELLA_RECREATIONAL_VEHICLES  
(  
   
 @CUSTOMER_ID Int,  
 @POLICY_ID Int,  
 @POLICY_VERSION_ID SmallInt,  
 @REC_VEH_ID SmallInt,  
 @ACTIVE nchar(1)  
)  
  
As  
  
UPDATE POL_UMBRELLA_RECREATIONAL_VEHICLES  
SET ACTIVE = @ACTIVE  
WHERE CUSTOMER_ID = CUSTOMER_ID AND  
      POLICY_ID = @POLICY_ID AND   
      POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
      REC_VEH_ID = @REC_VEH_ID      
   
  
RETURN 1     
  
  
  
  
  
  
  



GO

