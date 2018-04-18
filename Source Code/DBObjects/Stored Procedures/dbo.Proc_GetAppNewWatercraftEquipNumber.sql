IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppNewWatercraftEquipNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppNewWatercraftEquipNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name            : dbo.Proc_GetAppNewWatercraftEquipNumber        
Created by           : Swastika        
Date                 : 26th July'06        
Purpose              : To get the new Equip Number          
Revison History      :        
Used In              :   Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--DROP PROC dbo.Proc_GetAppNewWatercraftEquipNumber   
CREATE PROC dbo.Proc_GetAppNewWatercraftEquipNumber        
@CUSTOMER_ID INT,        
@APP_ID INT,        
@APP_VERSION_ID INT       
as        
BEGIN        

 begin        
  SELECT    (isnull(MAX(EQUIP_NO),0)) +1 as EQUIP_NO  
  FROM         APP_WATERCRAFT_EQUIP_DETAILLS WITH(NOLOCK)  
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID        
 end        
     
END        



GO

