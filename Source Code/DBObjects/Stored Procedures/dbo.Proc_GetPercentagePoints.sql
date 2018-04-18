IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPercentagePoints]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPercentagePoints]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name        : dbo.Proc_GetPercentagePoints    
Created by       : Sumit Chhabra    
Date             : 16/12/2005                        
Purpose       :  Calculate percentage points based on state and policy type
Revison History :                        
Used In  : Wolverine                         
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                         
CREATE PROCEDURE Proc_GetPercentagePoints    
(                        
                         
 @CUSTOMER_ID int,                        
 @APP_ID  int,                        
 @APP_VERSION_ID smallint                        
)                        
AS                        
declare @state_id int
declare @policy_type INT    

BEGIN         
SELECT @state_id=state_id, @policy_type=policy_type FROM app_list
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID 

if((@policy_type=11409 or @policy_type=11410) and @state_id=22)
	return 100
else
	return 80        
END       
  



GO

