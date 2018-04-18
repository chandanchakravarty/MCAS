IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_POL_OWNER_RECREATIONAL_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_POL_OWNER_RECREATIONAL_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name   : dbo.Proc_Delete_POL_OWNER_RECREATIONAL_VEHICLES         
Created by  : Pradeep          
Date        : 11/10/2005        
Purpose     :           
Revison History  :                
 ------------------------------------------------------------                      
Date     Review By          Comments                    
           
------   ------------       -------------------------*/    
  
CREATE     PROCEDURE Proc_Delete_POL_OWNER_RECREATIONAL_VEHICLES  
(  
   
 @CUSTOMER_ID Int,  
 @POLICY_ID Int,  
 @POLICY_VERSION_ID SmallInt,  
 @REC_VEH_ID SmallInt  
)  
  
As  
  
IF EXISTS  
(  
 SELECT * FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES  
 WHERE  
 CUSTOMER_ID = CUSTOMER_ID AND  
       POLICY_ID = @POLICY_ID AND   
       POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
       REC_VEH_ID = @REC_VEH_ID  
)  
DELETE FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES  
WHERE CUSTOMER_ID = CUSTOMER_ID AND  
       POLICY_ID = @POLICY_ID AND   
       POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
       REC_VEH_ID = @REC_VEH_ID  
  
IF @@ERROR <> 0  
BEGIN  
 RETURN -10  
END   
  
DELETE FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES  
WHERE CUSTOMER_ID = CUSTOMER_ID AND  
      POLICY_ID = @POLICY_ID AND   
      POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
      REC_VEH_ID = @REC_VEH_ID      
   
  
RETURN 1     
  
  
  
  
  
  
  
  



GO

