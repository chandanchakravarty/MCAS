IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNextDwelling_Number]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNextDwelling_Number]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO












/*----------------------------------------------------------        
Proc Name   : dbo.Proc_GetNextDwelling_Number       
Created by  :Pradeep        
Date        :25 May,2005      
Purpose     : Returns the next DWELLING_NUMBER for the 
	      CUSTOMERID, APPID and APPVersionID	        
Revison History  :              
 ------------------------------------------------------------                    
Date     Review By          Comments                  
         
------   ------------       -------------------------*/  

CREATE      PROCEDURE Proc_GetNextDwelling_Number
(
	
	@CUSTOMER_ID Int,
	@APP_ID Int,
	@APP_VERSION_ID SmallInt
)

As

DECLARE @MAX BigInt

	
	SELECT @MAX = ISNULL(MAX(DWELLING_NUMBER),0)
	FROM APP_DWELLINGS_INFO
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

