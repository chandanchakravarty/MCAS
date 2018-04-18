IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UM_GetDwellingsID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UM_GetDwellingsID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE dbo.Proc_UM_GetDwellingsID
(    
 @CUSTOMER_ID int,    
 @APP_ID int,    
 @APP_VERSION_ID int    
)        
AS             
BEGIN                  
	SELECT DWELLING_ID
    	FROM APP_UMBRELLA_DWELLINGS_INFO     
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID   
	 AND IS_ACTIVE='Y'     
	ORDER BY   DWELLING_ID               
End  
  



GO

