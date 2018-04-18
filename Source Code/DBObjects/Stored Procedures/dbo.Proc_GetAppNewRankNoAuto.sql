IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppNewRankNoAuto]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppNewRankNoAuto]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------      
Proc Name           : dbo.Proc_GetAppNewRankNo      
Created by          : Swastika Gaur      
Date                : 2nd Jun'06      
Purpose             : To get the new additional rank no.     
Revison History     :      
Used In             :   Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--DROP PROC dbo.Proc_GetAppNewRankNo 
CREATE  PROC dbo.Proc_GetAppNewRankNoAuto      
@CUSTOMER_ID INT,      
@APP_ID INT,      
@APP_VERSION_ID INT,   
@VEHICLE_ID INT,    
@CALLEDFROM varchar(10)       
as      
BEGIN   
DECLARE @RANK AS numeric   
-- AUTO  AND  MOT
IF (@CALLEDFROM ='MOT' OR @CALLEDFROM ='PPA')      
   BEGIN 
	IF EXISTS (SELECT CUSTOMER_ID FROM APP_ADD_OTHER_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID) 	 
	   BEGIN
		SELECT (isnull(MAX(RANK),0)) +1 as RANK      
	  	FROM  APP_ADD_OTHER_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID      
	   END
	ELSE
	   SELECT 1 AS RANK
    END
END      




GO

