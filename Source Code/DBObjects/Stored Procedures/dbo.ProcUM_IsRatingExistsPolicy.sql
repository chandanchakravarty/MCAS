IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProcUM_IsRatingExistsPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProcUM_IsRatingExistsPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.ProcUM_IsRatingExistsPolicy            
Created by      : Ravindra
Date            : 03-22-2006
Purpose         : To Check for rating info       
Revison History :       
Used In         :   Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/   


CREATE PROCEDURE dbo.ProcUM_IsRatingExistsPolicy  
(      
 @CUSTOMER_ID int,      
 @POLICY_ID int,      
 @POLICY_VERSION_ID int,  
 @DWELLING_ID int  
)          
AS               
BEGIN                    
 SELECT DWELLING_ID  
     FROM POL_UMBRELLA_RATING_INFO       
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND 
	POLICY_ID=@POLICY_ID AND 
	POLICY_VERSION_ID=@POLICY_VERSION_ID  AND 
	DWELLING_ID = @DWELLING_ID    
End    
    
  



GO

