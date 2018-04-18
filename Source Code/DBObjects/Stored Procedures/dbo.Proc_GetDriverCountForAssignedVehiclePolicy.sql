IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDriverCountForAssignedVehiclePolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDriverCountForAssignedVehiclePolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
  
Proc Name    : Dbo.Proc_ GetDriverCountForAssignedVehiclePolicy    
  
Created by   : Swastika Gaur      
  
Date         : 09-03-2006    
  
Purpose      : Get the driver count for no. of assigned vehicles in automobile policy.      
  
Used In      : Wolverine                     
  
                 
  
------   ------------       -------------------------*/       
  
   
  
CREATE PROC Dbo.Proc_GetDriverCountForAssignedVehiclePolicy    
  
(                        
  
@CUSTOMER_ID     int,                        
  
@POL_ID     int ,                        
  
@POLICY_VERSION_ID     smallint,                        
  
@VEHICLE_ID     smallint,
@CALLED_FROM varchar(10)=null  
  
)                        
  
AS                        
  
BEGIN             
  
  DECLARE @COUNT int  

 if(UPPER(@CALLED_FROM)='UMB')
 begin
	SELECT @COUNT = COUNT(CUSTOMER_ID) FROM POL_UMBRELLA_DRIVER_DETAILS WHERE  
	  
	  CUSTOMER_ID=@CUSTOMER_ID and     
	  
	  POLICY_ID=@POL_ID and     
	  
	  POLICY_VERSION_ID = @POLICY_VERSION_ID and    
	  
	  VEHICLE_ID = @VEHICLE_ID    
 end
 else
	begin
	  SELECT @COUNT = COUNT(CUSTOMER_ID) FROM POL_DRIVER_DETAILS WHERE  
	  
	  CUSTOMER_ID=@CUSTOMER_ID and     
	  
	  POLICY_ID=@POL_ID and     
	  
	  POLICY_VERSION_ID = @POLICY_VERSION_ID and    
	  
	  VEHICLE_ID = @VEHICLE_ID    
	end  
  
 RETURN @COUNT   
  
END   
  
  



GO

