IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchAllDwellingsPOL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchAllDwellingsPOL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/**************************************************************
Proc Name   : dbo.Proc_FetchAllDwellingsPOL
Created by  : Ravindra
Date        : 06-23-2006
Purpose     : Get the All Dwelling asssociated with an Policy
Revison History  :                          
 ------------------------------------------------------------                                
Date     Review By          Comments                              
                     
***************************************************************/  
--DROP PROC Proc_FetchAllDwellingsPOL
CREATE PROCEDURE dbo.Proc_FetchAllDwellingsPOL
(          
  @CUSTOMER_ID int,          
  @POLICY_ID int,          
  @POLICY_VERSION_ID int          
)              
AS                   
BEGIN                    
	SELECT   
	DWELLING_ID AS RISK_ID,          
	DWELLING_NUMBER,          
	LOCATION_ID          
	FROM  POL_DWELLINGS_INFO WITH(NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND           
	POLICY_ID = @POLICY_ID AND           
	POLICY_VERSION_ID = @POLICY_VERSION_ID  


END 






GO

