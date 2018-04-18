IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_VEHICLE_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_VEHICLE_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_SAVE_VEHICLE_ENDORSEMENTS  
Created by      : Pradeep  
Date            : 10/13/2005  
Purpose     	: Inserts/Updates records in Vehicle_Endorsements   
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
drop proc dbo.Proc_SAVE_VEHICLE_ENDORSEMENTS  
------   ------------       -------------------------*/  
CREATE            PROC dbo.Proc_SAVE_VEHICLE_ENDORSEMENTS  
(  
	 @CUSTOMER_ID     int,  
	 @APP_ID     int,  
	 @APP_VERSION_ID     smallint,  
	 @VEHICLE_ID smallint,  
	 @ENDORSEMENT_ID int,  
	 @REMARKS NVarChar(500),
	 @VEHICLE_ENDORSEMENT_ID Int,
	 @EDITION_DATE  nvarchar(10)=null
)  
AS  
  
DECLARE @VEHICLE_END_ID_MAX int  

BEGIN  
   
	IF EXISTS
	(
		SELECT * FROM APP_VEHICLE_ENDORSEMENTS
		WHERE CUSTOMER_ID = @CUSTOMER_ID and   
			   APP_ID=@APP_ID and   
			   APP_VERSION_ID = @APP_VERSION_ID   
			   and VEHICLE_ID = @VEHICLE_ID  AND
				ENDORSEMENT_ID = @ENDORSEMENT_ID
	)
	BEGIN
		UPDATE APP_VEHICLE_ENDORSEMENTS
		SET REMARKS = @REMARKS
		,EDITION_DATE= @EDITION_DATE
		WHERE CUSTOMER_ID = @CUSTOMER_ID and   
			   APP_ID=@APP_ID and   
			   APP_VERSION_ID = @APP_VERSION_ID   
			   and VEHICLE_ID = @VEHICLE_ID  AND
				ENDORSEMENT_ID = @ENDORSEMENT_ID
	END	
	ELSE
	BEGIN
		
		select  @VEHICLE_END_ID_MAX = isnull(Max(VEHICLE_ENDORSEMENT_ID),0)+1 
			from APP_VEHICLE_ENDORSEMENTS  
		  where CUSTOMER_ID = @CUSTOMER_ID and   
		   APP_ID=@APP_ID and   
		   APP_VERSION_ID = @APP_VERSION_ID   
		   and VEHICLE_ID = @VEHICLE_ID  
	
		INSERT INTO APP_VEHICLE_ENDORSEMENTS
		(
			CUSTOMER_ID,
			APP_ID,
			APP_VERSION_ID,
			VEHICLE_ID,
			ENDORSEMENT_ID,
			REMARKS,
			VEHICLE_ENDORSEMENT_ID,
			EDITION_DATE
		)
		VALUES
		(
			@CUSTOMER_ID,
			@APP_ID,
			@APP_VERSION_ID,
			@VEHICLE_ID,
			@ENDORSEMENT_ID,
			@REMARKS,
			@VEHICLE_END_ID_MAX,
			@EDITION_DATE
		)
	
	
	END
END	
 
  
  










GO

