IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UM_GetPolicyLocationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UM_GetPolicyLocationDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : dbo.Proc_UM_GetPolicyLocationDetails              
Created by      : Ravindra  
Date            : 03-21-2006  
Purpose         : To fetch location details  
Revison History :         
Used In         :   Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/     
  
--  drop proc dbo.Proc_UM_GetPolicyLocationDetails
CREATE PROCEDURE dbo.Proc_UM_GetPolicyLocationDetails    
(        
 @LOCATION_ID int
    
)            
AS                 
    
BEGIN                      
    
SELECT     
  CONVERT(VARCHAR(20),ISNULL(CLIENT_LOCATION_NUMBER,'')) + ' - ' +                
  ISNULL(ADDRESS_1,'') + ' ' + ISNULL(ADDRESS_2,'') + ', ' +               
  ISNULL(CITY,'') + ', ' +               
         CONVERT(VARCHAR(20),ISNULL(STATE,'')) + ' ' +               
         ISNULL(ZIPCODE,'')           
           
   as Address--,               

 FROM POL_UMBRELLA_REAL_ESTATE_LOCATION     
WHERE LOCATION_ID=@LOCATION_ID 
   
    
End      


GO

