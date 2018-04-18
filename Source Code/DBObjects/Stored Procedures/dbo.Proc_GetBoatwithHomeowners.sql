IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetBoatwithHomeowners]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetBoatwithHomeowners]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name   : dbo.Proc_GetBoatwithHomeowners    
Created by  : Manoj Rathore    
Date        : 15 Jan 2008    
Purpose     : HO rules
Revison History  :                    
 ------------------------------------------------------------                          
Date     Review By          Comments                        
DROP PROC dbo.Proc_GetBoatwithHomeowners               
------   ------------       -------------------------*/              
CREATE PROCEDURE dbo.Proc_GetBoatwithHomeowners  
(    
 @CUSTOMER_ID int,    
 @APP_ID int,    
 @APP_VERSION_ID int,
 @CALLEDFROM varchar(10)    
)        
AS             
BEGIN    
IF (@CALLEDFROM = 'APP')
BEGIN              

	SELECT ISNULL(BOAT_WITH_HOMEOWNER,'') as BOAT_WITH_HOMEOWNER
    	FROM APP_HOME_OWNER_GEN_INFO    WITH(NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID   
END
ELSE
BEGIN
	
	SELECT ISNULL(BOAT_WITH_HOMEOWNER,'') as BOAT_WITH_HOMEOWNER
    	FROM POL_HOME_OWNER_GEN_INFO    WITH(NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@APP_ID AND POLICY_VERSION_ID=@APP_VERSION_ID 
END
	               
End  
  






GO

