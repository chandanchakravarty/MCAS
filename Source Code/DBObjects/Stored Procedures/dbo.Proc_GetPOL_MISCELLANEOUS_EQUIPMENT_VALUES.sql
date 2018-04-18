IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPOL_MISCELLANEOUS_EQUIPMENT_VALUES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPOL_MISCELLANEOUS_EQUIPMENT_VALUES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                              
Proc Name       : dbo.Proc_GetPOL_MISCELLANEOUS_EQUIPMENT_VALUES                                                                  
Created by      : Sumit Chhabra                                                                            
Date            : 22/08/2006                                                                              
Purpose         : Get data from POL_MISCELLANEOUS_EQUIPMENT_VALUES        
Created by      : Sumit Chhabra                                                                             
Revison History :                                                                              
Used In        : Wolverine                                                                              
------------------------------------------------------------                                                                              
Date     Review By          Comments                                                                              
------   ------------       -------------------------*/                                                                              
CREATE PROC dbo.Proc_GetPOL_MISCELLANEOUS_EQUIPMENT_VALUES                                                                    
@CUSTOMER_ID int,        
@POLICY_ID int,        
@POLICY_VERSION_ID smallint,        
@VEHICLE_ID smallint  
AS                                                                              
BEGIN                            
 SELECT        
  ITEM_ID,  
  ITEM_DESCRIPTION,  
  ITEM_VALUE  
 FROM   
  POL_MISCELLANEOUS_EQUIPMENT_VALUES       
 WHERE  
  CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POLICY_ID AND  
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  VEHICLE_ID = @VEHICLE_ID  
     
END  



GO

