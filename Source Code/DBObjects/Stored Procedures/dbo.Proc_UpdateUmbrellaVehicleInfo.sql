IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateUmbrellaVehicleInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateUmbrellaVehicleInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--Text                                                                                                                                                                                                                                                         
 
     
         
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
       
/*----------------------------------------------------------                            
Proc Name       : dbo.Proc_UpdateUmbrellaVehicleInfo                            
Created by      :Mohit                            
Date            : 5/27/2005                            
Purpose         : To update the record in APP_UMBRELLA_VEHICLE_INFO table                            
Revison History :                            
Used In         :                            
                          
Modified By : Vijay Arora                          
Modified Date : 11-10-2005                          
Purpose  : To update the vehicle use, class and type.                          
                        
Modified By : Vijay Arora                          
Modified Date : 13-10-2005                          
Purpose  : Commented the updation of vehicle insurance number.                      
                      
Modified By : Mohit                      
Modified Date : 21-10-2005                          
Purpose  : field name changed.                      
              
Modified By : Sumit Chhabra              
Modified Date : 09-11-2005                          
Purpose  : The parameter annual mileage has been chaged from real to decimal (24,0)              
            
Modified By : Sumit Chhabra              
Modified Date : 21-11-2005                          
Purpose  : The parameter is_active will not be changed while updating the records            
          
Modified By : Sumit Chhabra              
Modified Date : 29-12-2005                          
Purpose  : modified the parameter names          
          
          
APP_USE_VEHICLE_ID, ---------------------->>>>>>>>USE_VEHICLE                         
ANTI_LCK_BRAKES ---------->>>>>>>>>>>>>>>>>      ANTI_LOCK_BRAKES          
APP_VEHICLE_PERCLASS_ID---------->>>>>>>>>>>>>>CLASS_PER          
APP_VEHICLE_COMCLASS_ID ------------>>>>>>>>>>> CLASS_COM           
APP_VEHICLE_PERTYPE_ID------->>>>>>>>>>VEHICLE_TYPE_PER          
APP_VEHICLE_COMTYPE_ID-------->>>>>>>>>>>VEHICLE_TYPE_COM                               
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/      
--drop PROC dbo.Proc_UpdateUmbrellaVehicleInfo                          
CREATE PROC dbo.Proc_UpdateUmbrellaVehicleInfo                            
(                            
@CUSTOMER_ID    int,                            
@APP_ID          int,                            
@APP_VERSION_ID  int,                            
@VEHICLE_ID     smallint,                            
@INSURED_VEH_NUMBER  int,                            
@VEHICLE_YEAR int,                            
@MAKE nvarchar(150),                            
@MODEL  nvarchar(150),                            
@VIN  nvarchar(150),                            
@BODY_TYPE  nvarchar(150),                            
@GRG_ADD1  nvarchar(150),                            
@GRG_ADD2  nvarchar(150),                            
@GRG_CITY  nvarchar(80),                            
@GRG_COUNTRY      nvarchar(10),                            
@GRG_STATE  nvarchar(10),                            
@GRG_ZIP  nvarchar(20),                            
@REGISTERED_STATE  nvarchar(20),                            
@TERRITORY  nvarchar(20),                            
@CLASS  nvarchar(150),                            
@REGN_PLATE_NUMBER nvarchar(150),                            
@ST_AMT_TYPE   nvarchar(10),                            
@AMOUNT DECIMAL(18,2) = null,                            
@SYMBOL int=null,       
@VEHICLE_AGE decimal=null,                            
@VEHICLE_CC int =0,                            
@MOTORCYCLE_TYPE int =0,                            
--@IS_ACTIVE  nchar(1),                            
@MODIFIED_BY     int,                            
@LAST_UPDATED_DATETIME  datetime,                          
@USE_VEHICLE int,                          
@CLASS_PER int,                          
@CLASS_COM int,                          
@VEHICLE_TYPE_PER int,                          
@VEHICLE_TYPE_COM int,                      
--@USE_VEHICLE int,                          
--@CLASS_PER int,                          
--@CLASS_COM int,                          
--@VEHICLE_TYPE_PER int,                          
--@VEHICLE_TYPE_COM int                       
--new parameters being added by Sumit on Oct 24,2005                  
@IS_OWN_LEASE nvarchar(20),                  
@PURCHASE_DATE datetime,                  
@IS_NEW_USED nchar(2),                  
@VEHICLE_USE nvarchar(10),                  
@MULTI_CAR nvarchar(10),      
@SAFETY_BELT nvarchar(5) = null,                  
@ANNUAL_MILEAGE decimal(24,0),              
@PASSIVE_SEAT_BELT nvarchar(10),                  
@AIR_BAG nvarchar(10),                  
@ANTI_LOCK_BRAKES nvarchar(10),                
--@UNINS_MOTOR_INJURY_COVE nchar(10),                
--@UNINS_PROPERTY_DAMAGE_COVE  nchar(10),                
--@UNDERINS_MOTOR_INJURY_COVE nchar(10),        
@MILES_TO_WORK varchar(5) ,    
@CALLED_FOR varchar(20),  
@IS_EXCLUDED int,
@OTHER_POLICY nvarchar(150)               
                         
)     
                          
AS                            
BEGIN     
     
 if(@CALLED_FOR!='Motor Home')      
     BEGIN                      
  UPDATE APP_UMBRELLA_VEHICLE_INFO                            
  SET                               
   --INSURED_VEH_NUMBER=@INSURED_VEH_NUMBER,                            
   VEHICLE_YEAR=@VEHICLE_YEAR,                            
   MAKE=@MAKE,                            
   MODEL=@MODEL,                            
   VIN=@VIN,                            
   BODY_TYPE=@BODY_TYPE,               
   GRG_ADD1=@GRG_ADD1,                            
   GRG_ADD2=@GRG_ADD2,                            
   GRG_CITY=@GRG_CITY,                            
   GRG_COUNTRY=@GRG_COUNTRY,                            
   GRG_STATE=@GRG_STATE,                            
   GRG_ZIP=@GRG_ZIP,                            
   REGISTERED_STATE=@REGISTERED_STATE,                            
   TERRITORY=@TERRITORY,                            
   CLASS=@CLASS,                    
   REGN_PLATE_NUMBER=@REGN_PLATE_NUMBER,                            
   ST_AMT_TYPE=@ST_AMT_TYPE,                            
   AMOUNT=@AMOUNT,                            
   SYMBOL=@SYMBOL,                            
   VEHICLE_AGE=@VEHICLE_AGE,                            
   VEHICLE_CC=@VEHICLE_CC,                            
   MOTORCYCLE_TYPE=@MOTORCYCLE_TYPE,                            
 --  IS_ACTIVE  =@IS_ACTIVE,                            
   MODIFIED_BY  =@MODIFIED_BY,                            
   LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME,                            
                           
 -- added by vj on 11-10-2005                          
 --  USE_VEHICLE =  @USE_VEHICLE,                          
 --  CLASS_PER = @CLASS_PER,                          
 --  CLASS_COM = @CLASS_COM,                          
 --  VEHICLE_TYPE_PER = @VEHICLE_TYPE_PER,                          
 --  VEHICLE_TYPE_COM = @VEHICLE_TYPE_COM                        
   USE_VEHICLE=@USE_VEHICLE,                          
   CLASS_PER=@CLASS_PER,                          
   CLASS_COM=@CLASS_COM,                          
   VEHICLE_TYPE_PER=@VEHICLE_TYPE_PER,                          
   VEHICLE_TYPE_COM=@VEHICLE_TYPE_COM,                         
  --added by Sumit on Oct 24,2005                  
   IS_OWN_LEASE=@IS_OWN_LEASE,                  
   PURCHASE_DATE=@PURCHASE_DATE,                   
   IS_NEW_USED=@IS_NEW_USED,                   
   VEHICLE_USE=@VEHICLE_USE,                   
   MULTI_CAR=@MULTI_CAR,                   
   SAFETY_BELT=@SAFETY_BELT,      
   ANNUAL_MILEAGE=@ANNUAL_MILEAGE,                   
   PASSIVE_SEAT_BELT=@PASSIVE_SEAT_BELT,         
   AIR_BAG=@AIR_BAG,                   
   ANTI_LOCK_BRAKES=@ANTI_LOCK_BRAKES,                
 --  UNINS_MOTOR_INJURY_COVE=@UNINS_MOTOR_INJURY_COVE,                
 --  UNINS_PROPERTY_DAMAGE_COVE=@UNINS_PROPERTY_DAMAGE_COVE,                
 --  UNDERINS_MOTOR_INJURY_COVE=@UNDERINS_MOTOR_INJURY_COVE,        
  MILES_TO_WORK=@MILES_TO_WORK ,  
  IS_EXCLUDED=@IS_EXCLUDED ,
  OTHER_POLICY = @OTHER_POLICY   
  WHERE                            
  CUSTOMER_ID  =@CUSTOMER_ID AND                            
  APP_ID   =@APP_ID AND                            
  APP_VERSION_ID =@APP_VERSION_ID AND                            
  VEHICLE_ID  =@VEHICLE_ID      
 END      
    ELSE     
 BEGIN    
   UPDATE APP_UMBRELLA_VEHICLE_INFO                            
   SET                               
   --INSURED_VEH_NUMBER=@INSURED_VEH_NUMBER,                            
   VEHICLE_YEAR=@VEHICLE_YEAR,                            
   MAKE=@MAKE,                            
   MODEL=@MODEL,                            
   VIN=@VIN,                            
   BODY_TYPE=@BODY_TYPE,               
   GRG_ADD1=@GRG_ADD1,                            
   GRG_ADD2=@GRG_ADD2,                            
   GRG_CITY=@GRG_CITY,                            
   GRG_COUNTRY=@GRG_COUNTRY,                            
   GRG_STATE=@GRG_STATE,                            
   GRG_ZIP=@GRG_ZIP,                            
   REGISTERED_STATE=@REGISTERED_STATE,                            
   TERRITORY=@TERRITORY,                            
   CLASS=@CLASS,                    
   REGN_PLATE_NUMBER=@REGN_PLATE_NUMBER,                            
   ST_AMT_TYPE=@ST_AMT_TYPE,                            
   AMOUNT=@AMOUNT,                            
   SYMBOL=@SYMBOL,                            
   VEHICLE_AGE=@VEHICLE_AGE,                            
   VEHICLE_CC=@VEHICLE_CC,                            
   MOTORCYCLE_TYPE=@VEHICLE_TYPE_PER,                            
 --  IS_ACTIVE  =@IS_ACTIVE,                            
   MODIFIED_BY  =@MODIFIED_BY,                            
   LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME,                            
                           
   -- added by vj on 11-10-2005                          
 --  USE_VEHICLE =  @USE_VEHICLE,                          
 --  CLASS_PER = @CLASS_PER,                          
 --  CLASS_COM = @CLASS_COM,                          
 --  VEHICLE_TYPE_PER = @VEHICLE_TYPE_PER,                          
 --  VEHICLE_TYPE_COM = @VEHICLE_TYPE_COM                        
   USE_VEHICLE=@USE_VEHICLE,                          
   CLASS_PER=@CLASS_PER,                          
   CLASS_COM=@CLASS_COM,                          
   VEHICLE_TYPE_PER=@MOTORCYCLE_TYPE,                          
   VEHICLE_TYPE_COM=@VEHICLE_TYPE_COM,                         
  --added by Sumit on Oct 24,2005                  
   IS_OWN_LEASE=@IS_OWN_LEASE,                  
   PURCHASE_DATE=@PURCHASE_DATE,                   
   IS_NEW_USED=@IS_NEW_USED,                   
   VEHICLE_USE=@VEHICLE_USE,                   
   MULTI_CAR=@MULTI_CAR,                   
   SAFETY_BELT=@SAFETY_BELT,      
   ANNUAL_MILEAGE=@ANNUAL_MILEAGE,                   
   PASSIVE_SEAT_BELT=@PASSIVE_SEAT_BELT,                   
   AIR_BAG=@AIR_BAG,                   
   ANTI_LOCK_BRAKES=@ANTI_LOCK_BRAKES,                
 --  UNINS_MOTOR_INJURY_COVE=@UNINS_MOTOR_INJURY_COVE,                
 --  UNINS_PROPERTY_DAMAGE_COVE=@UNINS_PROPERTY_DAMAGE_COVE,                
 --  UNDERINS_MOTOR_INJURY_COVE=@UNDERINS_MOTOR_INJURY_COVE,        
  MILES_TO_WORK=@MILES_TO_WORK ,  
  IS_EXCLUDED=@IS_EXCLUDED,
  OTHER_POLICY = @OTHER_POLICY                   
                           
  WHERE                            
CUSTOMER_ID  =@CUSTOMER_ID AND                            
   APP_ID   =@APP_ID AND                            
   APP_VERSION_ID =@APP_VERSION_ID AND                            
   VEHICLE_ID  =@VEHICLE_ID      
 END                         
END                            
                 
              
            
          
        
      
      
      
    
    
    
    
  



GO

