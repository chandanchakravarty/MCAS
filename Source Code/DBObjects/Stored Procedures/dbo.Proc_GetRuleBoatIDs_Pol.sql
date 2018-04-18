IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRuleBoatIDs_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRuleBoatIDs_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ----------------------------------------------------------                
Proc Name   : dbo.Proc_GetRuleBoatIDs_POL      
Created by  : Ashwini      
Date        : 02 Mar. 2006
Purpose     : Get the Boat IDs                 
Revison History  :                      
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/                
CREATE PROCEDURE dbo.Proc_GetRuleBoatIDs_Pol      
(      
 @CUSTOMER_ID int,      
 @POLICY_ID int,      
 @POLICY_VERSION_ID int      
)          
AS               
BEGIN                
      
SELECT   BOAT_ID      
FROM       POL_WATERCRAFT_INFO       
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID and IS_ACTIVE='Y'       
   ORDER BY   BOAT_ID                  
End     
  



GO

