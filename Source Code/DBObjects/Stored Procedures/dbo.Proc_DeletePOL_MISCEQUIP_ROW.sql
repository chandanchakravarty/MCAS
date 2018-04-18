IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePOL_MISCEQUIP_ROW]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePOL_MISCEQUIP_ROW]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                                                                
Proc Name       : dbo.Proc_DeletePOL_MISCEQUIP_ROW                                                                    
Created by      : Sibin Philip                                                                              
Date            : 31/10/2008                                                                                
Purpose         : Delete row at POLICY_MISCELLANEOUS_EQUIPMENT_VALUES          
Created by      :                                                                                
Revison History :                                                                                
Used In        : Wolverine                                                                                
------------------------------------------------------------                                                                                
Date     Review By          Comments                                                                               
------   ------------       -------------------------*/                                                                                
CREATE PROC dbo.Proc_DeletePOL_MISCEQUIP_ROW                                                                      
@CUSTOMER_ID int,          
@POLICY_ID int,          
@POLICY_VERSION_ID smallint,      
@VEHICLE_ID smallint,    
@ITEM_DESCRIPTION nvarchar(1000),      
@ITEM_VALUE decimal      
      
AS                                                                                
BEGIN                              
 delete from POL_MISCELLANEOUS_EQUIPMENT_VALUES where         
  CUSTOMER_ID=@CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND      
  VEHICLE_ID = @VEHICLE_ID  AND     
  ITEM_DESCRIPTION = @ITEM_DESCRIPTION AND      
  ITEM_VALUE = @ITEM_VALUE       
         
          
END        
GO

