IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateAppDriverDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateAppDriverDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       : dbo.Proc_ActivateDeactivateDriverDetails            
Created by      : Vijay                
Date            : 29 April,2005                
Purpose         : To Activate/Deactivate the record of driver            
Revison History :                
Used In         : Wolverine                
------------------------------------------------------------                
Date     Review By          Comments             
drop PROC dbo.Proc_ActivateDeactivateAppDriverDetails           
------   ------------       -------------------------*/                
CREATE PROC dbo.Proc_ActivateDeactivateAppDriverDetails    
(                
 @CUSTOMER_ID int,            
 @APP_ID  int,            
 @APP_VERSION_ID int,            
 @DRIVER_ID int,            
 @IS_ACTIVE   Char(1),          
 @CALLED_FROM varchar(10)=null                
)                
AS                
BEGIN            
DECLARE @DRIVER_DOB DATETIME          
DECLARE @VEHICLE_ID INT          
DECLARE @CUSTOMER_DETAILS VARCHAR(20)--Done for Itrack Issue 5457 on 14 April 2009          
          
IF(UPPER(@CALLED_FROM)='MOT')          
BEGIN          
 IF(UPPER(@IS_ACTIVE)='N')  --UPDATE THE CLASS AS IF THE DRIVER IS DELETE/DEACTIVATED          
   EXEC PROC_SETMOTORVEHICLECLASSRULEONDELETE @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@DRIVER_ID                           
END          
          
          
 UPDATE APP_DRIVER_DETAILS            
 SET                 
    IS_ACTIVE  = @IS_ACTIVE               
 WHERE                
  CUSTOMER_ID = @CUSTOMER_ID AND            
  APP_ID = @APP_ID AND            
  APP_VERSION_ID = @APP_VERSION_ID AND            
  DRIVER_ID = @DRIVER_ID            
          
IF(UPPER(@CALLED_FROM)='MOT')          
BEGIN          
 IF(UPPER(@IS_ACTIVE)='Y')          
  BEGIN--UPDATE THE CLASS WHEN THE DRIVER IS ACTIVATED          
     SELECT @DRIVER_DOB=DRIVER_DOB,@VEHICLE_ID=VEHICLE_ID FROM APP_DRIVER_DETAILS WHERE           
       CUSTOMER_ID = @CUSTOMER_ID AND            
      APP_ID = @APP_ID AND            
      APP_VERSION_ID = @APP_VERSION_ID AND            
      DRIVER_ID = @DRIVER_ID             
    EXEC PROC_SETMOTORVEHICLECLASSRULE @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @DRIVER_ID, @VEHICLE_ID, @DRIVER_DOB                      
 END          
END          
     
--Done for Itrack Issue 5457 on 14 April 2009        
SET @CUSTOMER_DETAILS = CONVERT( VARCHAR(10),@CUSTOMER_ID) + '^' + CONVERT(VARCHAR(10),@APP_ID)+ '^' +           
CONVERT(VARCHAR(10),@APP_VERSION_ID) + '^' + CONVERT(VARCHAR(10),@DRIVER_ID) + '^APP'        
     select @CUSTOMER_DETAILS   
IF EXISTS(SELECT 1 FROM APP_PRIOR_LOSS_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND DRIVER_ID = @DRIVER_ID)-- DRIVER_NAME=@CUSTOMER_DETAILS)          
 BEGIN      
     
   UPDATE APP_PRIOR_LOSS_INFO SET DRIVER_NAME='',DRIVER_ID=''        
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND DRIVER_ID = @DRIVER_ID      
  
 END        
--Added till here            
                
END  
GO

