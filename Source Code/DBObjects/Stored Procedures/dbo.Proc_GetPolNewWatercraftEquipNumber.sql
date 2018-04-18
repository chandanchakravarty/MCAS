IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolNewWatercraftEquipNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolNewWatercraftEquipNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name            : dbo.Proc_GetPolNewWatercraftEquipNumber        
Created by           : Swastika        
Date                 : 26th July'06        
Purpose              : To get the new Equip Number          
Revison History      :        
Used In              :   Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--DROP PROC dbo.Proc_GetPolNewWatercraftEquipNumber   
CREATE PROC dbo.Proc_GetPolNewWatercraftEquipNumber        
@CUSTOMER_ID INT,        
@POLICY_ID INT,        
@POLICY_VERSION_ID INT       
as        
BEGIN        

 begin        
  SELECT    (isnull(MAX(EQUIP_NO),0)) +1 as EQUIP_NO  
  FROM         POL_WATERCRAFT_EQUIP_DETAILLS WITH(NOLOCK)  
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID        
 end        
     
END        





GO

