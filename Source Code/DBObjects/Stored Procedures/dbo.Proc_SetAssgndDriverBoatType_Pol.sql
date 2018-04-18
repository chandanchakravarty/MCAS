IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetAssgndDriverBoatType_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetAssgndDriverBoatType_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- drop proc dbo.Proc_SetAssgndDriverBoatType_Pol          
CREATE PROCEDURE dbo.Proc_SetAssgndDriverBoatType_Pol          
(          
@CUSTOMER_ID int,          
@POL_ID int,          
@POL_VERSION_ID int,          
@BOAT_ID int        --boat ID  
)          
as          
BEGIN          
 Declare @Driver_ID  varchar(100)          
 Declare @Driver_DOB datetime          
           
 DECLARE AddAssDrv CURSOR FOR           
  SELECT Driver_ID, DRIVER_DOB FROM POL_WATERCRAFT_DRIVER_DETAILS (NOLOCK)          
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID =@POL_VERSION_ID AND IS_ACTIVE='Y'          
           
 OPEN AddAssDrv          
           
           
 FETCH NEXT FROM AddAssDrv into @Driver_ID, @Driver_DOB          
           
 WHILE @@FETCH_STATUS = 0          
 BEGIN          
        -- This is executed as long as the previous fetch succeeds.                
           
   DECLARE @DrvAge AS INTEGER          
   DECLARE @PrnOcc AS INTEGER           
   Select @DrvAge = datediff(yy,@Driver_DOB,getdate())          
             
  
--temp  
    --set @PrnOcc = '11926'  
     set @PrnOcc = '11936' --Principal Operator
  
           
   Insert into POL_OPERATOR_ASSIGNED_BOAT          
   (          
    CUSTOMER_ID,          
    POLICY_ID,          
    POLICY_VERSION_ID,          
    DRIVER_ID,          
    BOAT_ID,          
    APP_VEHICLE_PRIN_OCC_ID          
   )          
   values          
   (          
    @CUSTOMER_ID,          
    @POL_ID,          
    @POL_VERSION_ID,          
    @Driver_ID,          
    @BOAT_ID,          
    @PrnOcc          
   )          
           
  FETCH NEXT FROM AddAssDrv into @Driver_ID, @Driver_DOB          
           
 END          
 CLOSE AddAssDrv          
 DEALLOCATE AddAssDrv            
    
END          
        
      
    
    







GO

