IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_MISCELLANEOUS_EQUIPMENT_VALUES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_MISCELLANEOUS_EQUIPMENT_VALUES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                  
Proc Name       : dbo.Proc_InsertAPP_MISCELLANEOUS_EQUIPMENT_VALUES                                                                      
Created by      : Sumit Chhabra                                                                                
Date            : 22/08/2006                                                                                  
Purpose         : Insert data in APP_MISCELLANEOUS_EQUIPMENT_VALUES            
Created by      : Sumit Chhabra                                                                                 
Revison History :                                                                                  
Used In        : Wolverine                                                                                  
------------------------------------------------------------                                                                                  
Date     Review By          Comments           
drop  PROC dbo.Proc_InsertAPP_MISCELLANEOUS_EQUIPMENT_VALUES                                                                                                      
------   ------------       -------------------------*/                                                                                  
CREATE PROC [dbo].[Proc_InsertAPP_MISCELLANEOUS_EQUIPMENT_VALUES]                                                                        
@CUSTOMER_ID int,            
@APP_ID int,            
@APP_VERSION_ID smallint,            
@VEHICLE_ID smallint,        
@ITEM_ID int,            
@ITEM_DESCRIPTION nvarchar(500),            
@ITEM_VALUE decimal(10,2),      
@CREATED_BY int,      
@CREATED_DATETIME datetime            
AS                                                                                  
BEGIN                                

IF @ITEM_ID IS NULL 
BEGIN 
	SELECT @ITEM_ID = ISNULL(MAX(ITEM_ID),0) + 1  FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES WHERE      
	CUSTOMER_ID = @CUSTOMER_ID AND    
	  APP_ID = @APP_ID  AND          
   APP_VERSION_ID = @APP_VERSION_ID AND         
   VEHICLE_ID = @VEHICLE_ID  
END  

IF NOT EXISTS(  
   SELECT * FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES WHERE      
   CUSTOMER_ID = @CUSTOMER_ID AND    
   APP_ID = @APP_ID  AND          
   APP_VERSION_ID = @APP_VERSION_ID AND         
   VEHICLE_ID = @VEHICLE_ID AND       
   ITEM_ID = @ITEM_ID   
   )  
BEGIN   
  --Checking for duplicate Entry  
 IF NOT EXISTS(  
    SELECT * FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES WHERE      
    CUSTOMER_ID = @CUSTOMER_ID AND    
    APP_ID = @APP_ID  AND          
    APP_VERSION_ID = @APP_VERSION_ID AND         
    VEHICLE_ID = @VEHICLE_ID AND        
    ITEM_DESCRIPTION = @ITEM_DESCRIPTION AND            
    ITEM_VALUE = @ITEM_VALUE  
    )    
 BEGIN    
  --Insert  
  INSERT INTO APP_MISCELLANEOUS_EQUIPMENT_VALUES            
  (            
  CUSTOMER_ID,            
  APP_ID,            
  APP_VERSION_ID,         
  VEHICLE_ID,        
  ITEM_ID,            
  ITEM_DESCRIPTION,            
  ITEM_VALUE,          
  IS_ACTIVE,      
  CREATED_BY,      
  CREATED_DATETIME      
  )            
  VALUES            
  (            
  @CUSTOMER_ID,            
  @APP_ID,            
  @APP_VERSION_ID,        
  @VEHICLE_ID,            
  @ITEM_ID,            
  @ITEM_DESCRIPTION,            
  @ITEM_VALUE,          
  'Y',      
  @CREATED_BY,      
  @CREATED_DATETIME          
  )        
 END    
END  
ELSE  
BEGIN   
  --Checking for duplicate Entry before updating  
  IF NOT EXISTS(  
    SELECT * FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES WHERE      
    CUSTOMER_ID = @CUSTOMER_ID AND    
    APP_ID = @APP_ID  AND          
    APP_VERSION_ID = @APP_VERSION_ID AND         
    VEHICLE_ID = @VEHICLE_ID AND        
    ITEM_DESCRIPTION = @ITEM_DESCRIPTION AND            
    ITEM_VALUE = @ITEM_VALUE  
    )    
  BEGIN    
   --Updating - Done by Sibin on 3 Dec 08 for Itrack Issue 5114  
   UPDATE APP_MISCELLANEOUS_EQUIPMENT_VALUES SET ITEM_DESCRIPTION = @ITEM_DESCRIPTION,            
    ITEM_VALUE = @ITEM_VALUE,      
    MODIFIED_BY = @CREATED_BY ,   
    LAST_UPDATED_DATETIME = @CREATED_DATETIME  
   WHERE     
   CUSTOMER_ID = @CUSTOMER_ID AND    
   APP_ID = @APP_ID  AND          
   APP_VERSION_ID = @APP_VERSION_ID AND         
   VEHICLE_ID = @VEHICLE_ID AND        
   ITEM_ID = @ITEM_ID   
  END  
END  
   
END                            
GO

