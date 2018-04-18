IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetReplacementCost]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetReplacementCost]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
Proc Name        : dbo.Proc_GetReplacementCost      
Created by       : Sumit Chhabra      
Date             : 19/12/2005                          
Purpose      		 :  Calculate percentage points based on policy type
Revison History :                          
Used In  : Wolverine                           
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                           
CREATE PROCEDURE Proc_GetReplacementCost      
(                          
                           
 @CUSTOMER_ID int,                          
 @APP_ID  int,                          
 @APP_VERSION_ID smallint                          
)                          
AS                          

declare @policy_type INT      
  
BEGIN           
SELECT @policy_type=policy_type FROM app_list  
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID   
  
if(@policy_type=11458)
 return 11486 
else  
 return 11484          
END         
    
  



GO

