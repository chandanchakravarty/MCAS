IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNextUMBRELLARecrCompany_ID_Number]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNextUMBRELLARecrCompany_ID_Number]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO











/*----------------------------------------------------------        
Proc Name   : dbo.Proc_GetNextUMBRELLARecrCompany_ID_Number       
Created by  :Pradeep        
Date        :17 Jun,2005      
Purpose     : Returns the next COMPANY_ID_NUMBER for the 
	      CUSTOMERID, APPID and APPVersionID	        
Revison History  :              
 ------------------------------------------------------------                    
Date     Review By          Comments                  
         
------   ------------       -------------------------*/  

CREATE      PROCEDURE Proc_GetNextUMBRELLARecrCompany_ID_Number
(
	
	@CUSTOMER_ID Int,
	@APP_ID Int,
	@APP_VERSION_ID SmallInt
)

As

	DECLARE @MAX BigInt
	
	SELECT @MAX = ISNULL(MAX(COMPANY_ID_NUMBER),0)
	FROM APP_UMBRELLA_RECREATIONAL_VEHICLES
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND 
		APP_VERSION_ID = @APP_VERSION_ID 
	
	IF @MAX = 2147483647
	BEGIN
		SELECT -1
		RETURN		
	END

	SELECT @MAX + 1








GO

