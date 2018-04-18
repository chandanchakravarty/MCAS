IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetEquipIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetEquipIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name   : dbo.Proc_GetEquipIDs      
Created by  : Ashwini      
Date        : 04 Jan.,2006
Purpose     : Get the Equip IDs                 
Revison History  :                      
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/                
CREATE PROCEDURE dbo.Proc_GetEquipIDs      
(      
 @CUSTOMER_ID int,      
 @APP_ID int,      
 @APP_VERSION_ID int      
)          
AS               
BEGIN                
      
   SELECT EQUIP_ID      
   FROM APP_WATERCRAFT_EQUIP_DETAILLS       
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y'       
   ORDER BY   EQUIP_ID                  
END    
  



GO

