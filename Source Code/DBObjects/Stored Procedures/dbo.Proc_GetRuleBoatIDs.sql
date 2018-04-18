IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRuleBoatIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRuleBoatIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name   : dbo.Proc_GetRuleBoatIDs    
Created by  : Ashwini    
Date        : 20 October,2005    
Purpose     : Get the Boat IDs               
Revison History  :                    
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/              
CREATE PROCEDURE dbo.Proc_GetRuleBoatIDs    
(    
 @CUSTOMER_ID int,    
 @APP_ID int,    
 @APP_VERSION_ID int    
)        
AS             
BEGIN              
    
SELECT   BOAT_ID    
FROM       APP_WATERCRAFT_INFO     
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y'     
   ORDER BY   BOAT_ID                
End   



GO

