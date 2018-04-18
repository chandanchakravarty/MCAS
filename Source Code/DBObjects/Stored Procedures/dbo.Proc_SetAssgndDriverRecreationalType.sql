IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetAssgndDriverRecreationalType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetAssgndDriverRecreationalType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- BEGIN TRAN
-- DROP PROC dbo.Proc_SetAssgndDriverRecreationalType
-- GO
--drop proc dbo.Proc_SetAssgndDriverRecreationalType          
CREATE PROCEDURE dbo.Proc_SetAssgndDriverRecreationalType          
(          
	@CUSTOMER_ID int,          
	@APP_ID int,          
	@APP_VERSION_ID int,          
	@REC_VEH_ID int         
)          
AS          
BEGIN          
	Declare @Driver_ID  varchar(100)          
	   
	DECLARE AddAssDrvRec CURSOR FOR           
	SELECT Driver_ID FROM APP_WATERCRAFT_DRIVER_DETAILS (NOLOCK)          
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID =@APP_VERSION_ID AND IS_ACTIVE='Y'          
           
 OPEN AddAssDrvRec        
           
           
 FETCH NEXT FROM AddAssDrvRec into @Driver_ID         
           
 WHILE @@FETCH_STATUS = 0          
 BEGIN          
	  
	   INSERT INTO APP_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE          
	   (          
	    CUSTOMER_ID,          
	    APP_ID,          
	    APP_VERSION_ID,          
	    DRIVER_ID,          
	    RECREATIONAL_VEH_ID,          
	    APP_REC_VEHICLE_PRIN_OCC_ID          
	   )          
	   VALUES          
	   (          
	    @CUSTOMER_ID,          
	    @APP_ID,          
	    @APP_VERSION_ID,          
	    @Driver_ID,          
	    @REC_VEH_ID,          
	    0          
	   )          
      
  FETCH NEXT FROM AddAssDrvRec into @Driver_ID          
           
 END          
 CLOSE AddAssDrvRec          
 DEALLOCATE AddAssDrvRec            
    
END          
        
--       
-- GO
-- EXEC dbo.Proc_SetAssgndDriverBoatType 547,38,1,1
-- ROLLBACK TRAN    
     












GO

