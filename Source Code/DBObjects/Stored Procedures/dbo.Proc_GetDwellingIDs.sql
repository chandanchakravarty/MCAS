IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDwellingIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDwellingIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name   : dbo.Proc_GetDwellingIDs    
Created by  : Ashwani    
Date        : 02 Dec.,2005    
Purpose     : Get the Dwelling IDs  for HO rules
Revison History  :                    
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/              
CREATE PROCEDURE dbo.Proc_GetDwellingIDs    
(    
 @CUSTOMER_ID int,    
 @APP_ID int,    
 @APP_VERSION_ID int    
)        
AS             
BEGIN                  
	SELECT DWELLING_ID
    	FROM APP_DWELLINGS_INFO     
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID   
	 AND IS_ACTIVE='Y'     
	ORDER BY   DWELLING_ID               
End  
  




GO

