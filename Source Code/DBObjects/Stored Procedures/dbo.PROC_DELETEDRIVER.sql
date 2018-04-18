IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_DELETEDRIVER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_DELETEDRIVER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- DROP PROC PROC_DELETEDRIVER    
CREATE  PROCEDURE dbo.PROC_DELETEDRIVER                                        
(                                        
                                         
 @CUSTOMER_ID int,                                        
 @APP_ID  int,                                        
 @APP_VERSION_ID smallint,                                        
 @DRIVER_ID INT,                                         
 @CALLEDFROM varchar(10),                                         
 @CALLEDFOR varchar(5)=null                                
)                                        
AS                                        
BEGIN                                        
if (UPPER(@CALLEDFROM) ='UMB' and UPPER(@CALLEDFOR) ='WAT')                                        
 BEGIN                                        
       
   --delete data from the app_water_mvr_information table also                              
   DELETE FROM APP_WATER_MVR_INFORMATION                        
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND                                         
   APP_VERSION_ID = @APP_VERSION_ID AND DRIVER_ID = @DRIVER_ID                                        
        
   DELETE FROM APP_UMBRELLA_OPERATOR_INFO                                         
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND                                         
   APP_VERSION_ID = @APP_VERSION_ID AND DRIVER_ID = @DRIVER_ID             
                         
       
       
 END                                        
else if (UPPER(@CALLEDFROM) ='PPA' or UPPER(@CALLEDFROM) ='MOT')                       
 BEGIN                                        
   if(UPPER(@CALLEDFROM) ='MOT')                       
   EXEC PROC_SETMOTORVEHICLECLASSRULEONDELETE @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@DRIVER_ID                    
       
  --delete data from the app_mvr_information table also                              
  DELETE FROM APP_MVR_INFORMATION                                        
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND                                         
  APP_VERSION_ID = @APP_VERSION_ID AND DRIVER_ID = @DRIVER_ID      
    
 -- DELETE FROM APP_DRIVER_ASSIGNED_VEHICLE    
 DELETE FROM APP_DRIVER_ASSIGNED_VEHICLE                                        
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND                                         
   APP_VERSION_ID = @APP_VERSION_ID AND DRIVER_ID = @DRIVER_ID       
                                          
               
  DELETE FROM APP_DRIVER_DETAILS                                         
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND                                         
  APP_VERSION_ID = @APP_VERSION_ID AND DRIVER_ID = @DRIVER_ID                                        
                                 
 END                           
ELSE IF (UPPER(@CALLEDFROM) ='UMB' and UPPER(@CALLEDFOR) ='PPA')           
 BEGIN                                        
   --delete data from the app_mvr_information table also                              
   DELETE FROM APP_UMBRELLA_MVR_INFORMATION                                        
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND                                         
   APP_VERSION_ID = @APP_VERSION_ID AND DRIVER_ID = @DRIVER_ID     
     
     
   DELETE FROM APP_UMBRELLA_DRIVER_DETAILS                                         
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND                                         
   APP_VERSION_ID = @APP_VERSION_ID AND DRIVER_ID = @DRIVER_ID                                           
                                 
 END                                        
ELSE IF (UPPER(@CALLEDFROM) ='WAT' OR UPPER(@CALLEDFROM) ='HOME' OR UPPER(@CALLEDFROM) ='RENT')                                        
 BEGIN            
   --delete data from  MVR table first and then driver details      
   --delete data from the app_water_mvr_information table also                           
   DELETE FROM APP_WATER_MVR_INFORMATION                        
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND                                         
   APP_VERSION_ID = @APP_VERSION_ID AND DRIVER_ID = @DRIVER_ID                          
        
   DELETE FROM APP_WATERCRAFT_DRIVER_DETAILS                                        
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND                                         
   APP_VERSION_ID = @APP_VERSION_ID AND DRIVER_ID = @DRIVER_ID                                        
 END   

--Added 13 sep 2007
IF (UPPER(@CALLEDFROM) ='WAT')
BEGIN
 DELETE FROM APP_OPERATOR_ASSIGNED_BOAT                                     
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND                                         
   APP_VERSION_ID = @APP_VERSION_ID AND DRIVER_ID = @DRIVER_ID    
END

 IF (UPPER(@CALLEDFROM) ='HOME')
 BEGIN

  DELETE FROM APP_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE  
  WHERE
	CUSTOMER_ID = @CUSTOMER_ID AND APP_ID =  @APP_ID AND 
	APP_VERSION_ID = @APP_VERSION_ID 
	AND DRIVER_ID = @DRIVER_ID
 END
                                       
END                          
      
      
    
  




GO

