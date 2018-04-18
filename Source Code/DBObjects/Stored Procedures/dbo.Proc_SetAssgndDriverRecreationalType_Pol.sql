IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetAssgndDriverRecreationalType_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetAssgndDriverRecreationalType_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- drop proc dbo.Proc_SetAssgndDriverRecreationalType_Pol          
CREATE PROCEDURE dbo.Proc_SetAssgndDriverRecreationalType_Pol          
(          
@CUSTOMER_ID int,          
@POL_ID int,          
@POL_VERSION_ID int,          
@REC_VEH_ID int        --boat ID  
)          
as          
BEGIN          
 Declare @Driver_ID  varchar(100)         
           
 DECLARE AddRecrAssDrv CURSOR FOR           
  SELECT Driver_ID FROM POL_WATERCRAFT_DRIVER_DETAILS (NOLOCK)          
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID =@POL_VERSION_ID AND IS_ACTIVE='Y'          
           
 OPEN AddRecrAssDrv          
           
           
 FETCH NEXT FROM AddRecrAssDrv into @Driver_ID         
           
 WHILE @@FETCH_STATUS = 0          
 BEGIN          
       
           
   Insert into POL_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE          
   (          
    CUSTOMER_ID,          
    POLICY_ID,          
    POLICY_VERSION_ID,          
    DRIVER_ID,          
    RECREATIONAL_VEH_ID,          
    POL_REC_VEHICLE_PRIN_OCC_ID          
   )          
   values          
   (          
    @CUSTOMER_ID,          
    @POL_ID,          
    @POL_VERSION_ID,          
    @Driver_ID,          
    @REC_VEH_ID,          
    0          
   )          
           
  FETCH NEXT FROM AddRecrAssDrv into @Driver_ID         
           
 END          
 CLOSE AddRecrAssDrv          
 DEALLOCATE AddRecrAssDrv            
    
END          
        
      
    
    








GO

