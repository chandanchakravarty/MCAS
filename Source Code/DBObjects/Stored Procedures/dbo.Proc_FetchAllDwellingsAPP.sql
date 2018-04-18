IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchAllDwellingsAPP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchAllDwellingsAPP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
/**************************************************************
Proc Name   : dbo.Proc_FetchAllDwellingsAPP
Created by  : Ravindra
Date        : 06-23-2006
Purpose     : Get the All Dwelling asssociated with an Application
Revison History  :                          
 ------------------------------------------------------------                                
Date     Review By          Comments                              
                     
***************************************************************/  
--DROP PROC Proc_FetchAllDwellingsAPP
CREATE PROCEDURE dbo.Proc_FetchAllDwellingsAPP
(          
  @CUSTOMER_ID int,          
  @APP_ID int,          
  @APP_VERSION_ID int          
)              
AS                   
BEGIN                    
	SELECT   
	DWELLING_ID, 
    DWELLING_ID AS RISK_ID,         
	DWELLING_NUMBER,          
	LOCATION_ID          
	FROM  APP_DWELLINGS_INFO WITH(NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND           
	APP_ID = @APP_ID AND           
	APP_VERSION_ID = @APP_VERSION_ID  


END 






GO

