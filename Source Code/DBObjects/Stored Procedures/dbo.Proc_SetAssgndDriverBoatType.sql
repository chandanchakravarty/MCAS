IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetAssgndDriverBoatType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetAssgndDriverBoatType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- BEGIN TRAN
-- DROP PROC dbo.Proc_SetAssgndDriverBoatType
-- GO
--drop proc dbo.Proc_SetAssgndDriverBoatType          
CREATE PROCEDURE dbo.Proc_SetAssgndDriverBoatType          
(          
	@CUSTOMER_ID int,          
	@APP_ID int,          
	@APP_VERSION_ID int,          
	@BOAT_ID int        --boat ID  
)          
AS          
BEGIN          
	Declare @Driver_ID  varchar(100)          
	Declare @Driver_DOB datetime          
	   
	DECLARE AddAssDrv CURSOR FOR           
	SELECT Driver_ID, DRIVER_DOB FROM APP_WATERCRAFT_DRIVER_DETAILS (NOLOCK)          
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID =@APP_VERSION_ID AND IS_ACTIVE='Y'          
           
 OPEN AddAssDrv        
           
           
 FETCH NEXT FROM AddAssDrv into @Driver_ID, @Driver_DOB          
           
 WHILE @@FETCH_STATUS = 0          
 BEGIN          
	    -- This is executed as long as the previous fetch succeeds.                
	           
	   DECLARE @DrvAge AS INTEGER          
	   DECLARE @PrnOcc AS INTEGER           
	   SELECT @DrvAge = datediff(yy,@Driver_DOB,getdate())          
	             
	  
	   --temp  
	    --set @PrnOcc = '11926'  
	        --SET @PrnOcc ='11936' --Principal Operator
		DECLARE @COUNT INT
		set @count=0

		SELECT @COUNT=COUNT(APP_VEHICLE_PRIN_OCC_ID) FROM APP_OPERATOR_ASSIGNED_BOAT 
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID =@APP_VERSION_ID 
			 AND APP_VEHICLE_PRIN_OCC_ID = '11936' and driver_id=@driver_id

-- 		SELECT @COUNT=COUNT(APP_VEHICLE_PRIN_OCC_ID) FROM APP_OPERATOR_ASSIGNED_BOAT 
-- 		WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID =@APP_VERSION_ID 
-- 			 AND APP_VEHICLE_PRIN_OCC_ID = '11936' 
		PRINT @COUNT
		IF(@COUNT > 0)
		BEGIN
	  		SET @PrnOcc ='11937'
		END
		ELSE 
		BEGIN
	        	SET @PrnOcc ='11936' --Principal Operator  
		END
	   INSERT INTO APP_OPERATOR_ASSIGNED_BOAT          
	   (          
	    CUSTOMER_ID,          
	    APP_ID,          
	    APP_VERSION_ID,          
	    DRIVER_ID,          
	    BOAT_ID,          
	    APP_VEHICLE_PRIN_OCC_ID          
	   )          
	   VALUES          
	   (          
	    @CUSTOMER_ID,          
	    @APP_ID,          
	    @APP_VERSION_ID,          
	    @Driver_ID,          
	    @BOAT_ID,          
	    @PrnOcc          
	   )          
       --SELECT * FROM     APP_OPERATOR_ASSIGNED_BOAT WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID =@APP_VERSION_ID 
  FETCH NEXT FROM AddAssDrv into @Driver_ID, @Driver_DOB          
           
 END          
 CLOSE AddAssDrv          
 DEALLOCATE AddAssDrv            
    
END          
        
--       
-- GO
-- EXEC dbo.Proc_SetAssgndDriverBoatType 1009,479,1,3
-- ROLLBACK TRAN    
--     











GO

