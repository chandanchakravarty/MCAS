IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePolicyMotorDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePolicyMotorDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_ActivateDeactivatePolicyMotorDriver  
Created by      : Vijay Arora       
Date            : 14-11-2005    
Purpose         : To Activate/Deactivate the record of driver    
Revison History :        
Used In         : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/ 
       
CREATE PROC dbo.Proc_ActivateDeactivatePolicyMotorDriver    
(        
 @CUSTOMER_ID int,    
 @POLICY_ID  int,    
 @POLICY_VERSION_ID int,    
 @DRIVER_ID int,    
 @IS_ACTIVE   Char(1),
 @CALLEDFROM CHAR(5)=NULL        
)        
AS   
     
BEGIN 
DECLARE @DRIVER_DOB DATETIME  
DECLARE @VEHICLE_ID INT  
 IF(UPPER(@CALLEDFROM)='MOT')  
  BEGIN  
 IF(UPPER(@IS_ACTIVE)='N')  --UPDATE THE CLASS AS IF THE DRIVER IS DELETE/DEACTIVATED  
   EXEC Proc_SetMotorVehicleClassRuleOnDeleteForPolicy @CUSTOMER_ID, @POLICY_ID,  @POLICY_VERSION_ID,@DRIVER_ID                   
END 
       
 UPDATE POL_DRIVER_DETAILS    
 SET         
    IS_ACTIVE  = @IS_ACTIVE       
 WHERE        
  CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
  DRIVER_ID = @DRIVER_ID    

IF(UPPER(@CALLEDFROM)='MOT')  
BEGIN  
 IF(UPPER(@IS_ACTIVE)='Y')  
  BEGIN--UPDATE THE CLASS WHEN THE DRIVER IS ACTIVATED  
     SELECT @DRIVER_DOB=DRIVER_DOB,@VEHICLE_ID=VEHICLE_ID FROM POL_DRIVER_DETAILS WHERE   
       CUSTOMER_ID = @CUSTOMER_ID AND    
       POLICY_ID = @POLICY_ID AND    
       POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
       DRIVER_ID = @DRIVER_ID     
     EXEC Proc_SetMotorVehicleClassRuleForPolicy @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @DRIVER_ID, @VEHICLE_ID, @DRIVER_DOB 
 END  
END
        
END    
    
    
    
    
    
    
  



GO

