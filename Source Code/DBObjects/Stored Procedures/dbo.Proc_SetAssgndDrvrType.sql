IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetAssgndDrvrType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetAssgndDrvrType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- drop proc dbo.Proc_SetAssgndDrvrType      
CREATE PROCEDURE dbo.Proc_SetAssgndDrvrType      
(      
@CUSTOMER_ID int,      
@APP_ID int,      
@APP_VERSION_ID int,      
@VEHICLE_ID int      
)      
AS      
BEGIN      
	Declare @Driver_ID  varchar(100)      
	Declare @Driver_DOB datetime      
	
	DECLARE AddAssDrv CURSOR FOR       
	SELECT Driver_ID, DRIVER_DOB FROM APP_DRIVER_DETAILS (NOLOCK)      
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID =@APP_VERSION_ID AND IS_ACTIVE='Y'      
	
	OPEN AddAssDrv      
       
       
	FETCH NEXT FROM AddAssDrv into @Driver_ID, @Driver_DOB      
	
	WHILE @@FETCH_STATUS = 0      
	BEGIN      
	-- This is executed as long as the previous fetch succeeds.            
	
	DECLARE @DrvAge AS INTEGER      
	DECLARE @PrnOcc AS INTEGER       
	Select @DrvAge = datediff(yy,@Driver_DOB,getdate())      
	 
	/*if @DrvAge>=25       
	Select @PrnOcc = '11926'--Occasional - No Points Assigned      
	else      
	Select @PrnOcc = '11928'--Youthful Occasional - No Points Assigned
	*/      
	--Added by Manoj Rathore on 27th May 2009 Itrack # 5799
	Select @PrnOcc = '11931'  --Not Rated   

	INSERT INTO APP_DRIVER_ASSIGNED_VEHICLE      
	(      
		CUSTOMER_ID,      
		APP_ID,      
		APP_VERSION_ID,      
		DRIVER_ID,      
		VEHICLE_ID,      
		APP_VEHICLE_PRIN_OCC_ID      
	)      
	VALUES      
	(      
		@CUSTOMER_ID,      
		@APP_ID,      
		@APP_VERSION_ID,      
		@Driver_ID,      
		@VEHICLE_ID,      
		@PrnOcc      
	)      
       
  FETCH NEXT FROM AddAssDrv into @Driver_ID, @Driver_DOB      
       
 END      
 CLOSE AddAssDrv      
 DEALLOCATE AddAssDrv        

END      
    
  






GO

