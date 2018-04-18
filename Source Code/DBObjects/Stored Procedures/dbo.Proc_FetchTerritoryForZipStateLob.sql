IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchTerritoryForZipStateLob]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchTerritoryForZipStateLob]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN
--drop PROCEDURE dbo.Proc_FetchTerritoryForZipStateLob 
--GO
/*----------------------------------------------------------  
Proc Name             : dbo.Proc_FetchTerritoryForZipStateLob  
Created by            : Sumit Chhabra
Date                  : 22/02/2006  
Purpose               :  To search for a territory based on state,zip and lob_id 
Revison History       :  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/ 
-- drop PROCEDURE dbo.Proc_FetchTerritoryForZipStateLob 
CREATE   PROCEDURE dbo.Proc_FetchTerritoryForZipStateLob --'46001',3,14,909,72,1,'APP',0 
(  
 @ZIP_ID VARCHAR(10),  
 @LOB_ID  smallint,
 @STATE_ID  smallint, 
 @CUSTOMER_ID smallint,          
 @APP_ID smallint=NULL,          
 @APP_VERSION_ID smallint=NULL,
 @CALLED_FROM varchar(10) =NULL,
 @VEHICLE_USE SMALLINT =NULL
)  
AS  
BEGIN 
	DECLARE @APP_EFFECTIVE_DATE  DATETIME,
		@APP_EXPIRATION_DATE DATETIME
		
	
	IF(@CALLED_FROM='APP')
	BEGIN
		SELECT @APP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE,@APP_EXPIRATION_DATE=APP_EXPIRATION_DATE
		FROM APP_LIST         
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID
	END
	ELSE IF(@CALLED_FROM='POL')
	BEGIN
		SELECT @APP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE,@APP_EXPIRATION_DATE=APP_EXPIRATION_DATE
		FROM POL_CUSTOMER_POLICY_LIST         
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@APP_ID AND POLICY_VERSION_ID=@APP_VERSION_ID
	END
 	IF(@VEHICLE_USE = 11332 OR @STATE_ID = 22)
	BEGIN
		SELECT TERR FROM MNT_TERRITORY_CODES WHERE ZIP=SUBSTRING(@ZIP_ID,1,5) AND LOBID=@LOB_ID  and STATE=@STATE_ID 
		and @APP_EFFECTIVE_DATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')
		AND AUTO_VEHICLE_TYPE IS NULL
   	END
	IF(@VEHICLE_USE = 11333 AND @STATE_ID = 14)
	BEGIN
		SELECT TERR FROM MNT_TERRITORY_CODES WHERE ZIP=SUBSTRING(@ZIP_ID,1,5) AND LOBID=@LOB_ID  and STATE=@STATE_ID 
		and @APP_EFFECTIVE_DATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')
		AND AUTO_VEHICLE_TYPE LIKE '%COM%'
   	END
	
	IF(@VEHICLE_USE = 0 )
	BEGIN 
		SELECT TERR FROM MNT_TERRITORY_CODES WHERE ZIP=SUBSTRING(@ZIP_ID,1,5) AND LOBID=@LOB_ID  and STATE=@STATE_ID 
		and @APP_EFFECTIVE_DATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')
	END
	
END 


--GO
--EXEC dbo.Proc_FetchTerritoryForZipStateLob '46001',3,14,909,72,1,'APP',0 
--ROLLBACK TRAN






GO

