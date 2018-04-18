IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_MISCELLANEOUS_EQUIPMENT_VALUES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_MISCELLANEOUS_EQUIPMENT_VALUES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                                                                
Proc Name       : dbo.Proc_GetAPP_MISCELLANEOUS_EQUIPMENT_VALUES                                                                    
Created by      : Sumit Chhabra                                                                              
Date            : 22/08/2006                                                                                
Purpose         : Get data from APP_MISCELLANEOUS_EQUIPMENT_VALUES          
Created by      : Sumit Chhabra                                                                               
Revison History :                                                                                
Used In        : Wolverine                                                                                
------------------------------------------------------------                                                                                
Date     Review By          Comments    
drop proc               dbo.Proc_GetAPP_MISCELLANEOUS_EQUIPMENT_VALUES                                                                      
------   ------------       -------------------------*/                                                                                
CREATE PROC dbo.Proc_GetAPP_MISCELLANEOUS_EQUIPMENT_VALUES                                                                      
@CUSTOMER_ID int,          
@APP_ID int,          
@APP_VERSION_ID smallint,          
@VEHICLE_ID smallint,  
@IS_ACTIVE char(1)   = null 

AS                                                                                
BEGIN                              
 SELECT          
  ITEM_ID,    
  ITEM_DESCRIPTION,    
  ITEM_VALUE,  
  IS_ACTIVE    
 FROM     
  APP_MISCELLANEOUS_EQUIPMENT_VALUES         
 WHERE    
  CUSTOMER_ID = @CUSTOMER_ID AND    
  APP_ID = @APP_ID AND    
  APP_VERSION_ID = @APP_VERSION_ID AND   
  VEHICLE_ID = @VEHICLE_ID AND  
  ISNULL(@IS_ACTIVE, IS_ACTIVE) = IS_ACTIVE    
       
END

GO

