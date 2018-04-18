IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePPAMiscEquip]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePPAMiscEquip]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

   
/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_ActivateDeactivatePPAMiscEquip          
Created by      : Sibin Philp           
Date            : 19/09/2008                            
Purpose         :Activate/ Deactivate vehicle         
Revison History :                  
Used In         : Wolverine                  
  
 Purpose : Deassign the vehicle from app_driver_details if it's deactivated      
                 
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
  -- drop proc    Proc_ActivateDeactivatePPAMiscEquip
CREATE PROC dbo.Proc_ActivateDeactivatePPAMiscEquip                   
(                  
@CUSTOMER_ID int,        
@APP_ID int,        
@APP_VERSION_ID smallint,    
@VEHICLE_ID smallint,  
@ITEM_DESCRIPTION nvarchar(1000),    
@ITEM_VALUE decimal,    
@IS_ACTIVE char(1))
    
AS                                                                              
BEGIN                            
 update APP_MISCELLANEOUS_EQUIPMENT_VALUES 
set is_active = @IS_ACTIVE
where       
  CUSTOMER_ID=@CUSTOMER_ID AND      
  APP_ID = @APP_ID AND      
  APP_VERSION_ID = @APP_VERSION_ID AND    
  VEHICLE_ID = @VEHICLE_ID  AND   
  ITEM_DESCRIPTION = @ITEM_DESCRIPTION AND    
  ITEM_VALUE = @ITEM_VALUE     
       
        
END      
    
GO

