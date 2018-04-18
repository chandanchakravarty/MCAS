IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyEquipIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyEquipIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name   : dbo.Proc_GetPolicyEquipIDs        
Created by  : Ashwini        
Date        : 09 Mar 2006
Purpose     : Get the Equip IDs for policy rate                   
Revison History  :                        
 ------------------------------------------------------------                              
Date     Review By          Comments                            
                   
------   ------------       -------------------------*/                  
CREATE PROCEDURE dbo.Proc_GetPolicyEquipIDs        
(        
 @CUSTOMER_ID int,        
 @POLICY_ID int,        
 @POLICY_VERSION_ID int        
)            
AS                 
BEGIN                  
        
   SELECT EQUIP_ID        
   FROM POL_WATERCRAFT_EQUIP_DETAILLS         
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID and IS_ACTIVE='Y'         
   ORDER BY   EQUIP_ID                    
END      
    
  



GO

