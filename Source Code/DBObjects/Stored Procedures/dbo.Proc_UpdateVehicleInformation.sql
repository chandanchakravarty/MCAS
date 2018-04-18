IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateVehicleInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateVehicleInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
Proc Name       : dbo.Proc_UpdateVehicleInformation                                
Created by      : Nidhi                                
Date            : 5/4/2005                                
Purpose         : To update the record in APP_VEHICLE table                                
Revison History :                                
Used In         :                                    
                                
Modified By : Anurag Verma                                
Modidied On : 20/09/2005                                
Purpose : Personal vehicle info screen is merged with vehicle info                                
                              
Modified By : Vijay Arora                              
Modidied On : 10-10-2005                               
Purpose : To update the vehicle use, class and type                              
                             
Modified By : Vijay Arora                              
Modidied On : 13-10-2005                               
Purpose : Commented the insured vehicle number updation.                            
                          
Modified By : Pradeep Iyer                          
Modidied On : 10-15-2005                               
Purpose : Removed dependent coverages from APP_VEHICLE_COVERAGES                         
                        
Modified By : Mohit                         
Modified On :21/10/2005                        
Purpose     :Changing parameter names as field names are changes.                         
                    
Modified By : Sumit Chhabra                    
Modified On :08/11/2005                        
Purpose     :Modified the parameter value Annual Mileage from (18,2) to (18,0)                    
                  
Modified By : Sumit Chhabra                    
Modified On :21/11/2005                   
Purpose     :is_active column has been removed from being updated by the procedure, its value in db will not be altered                  
Modified By : Sumit Chhabra                    
Modified On :29/12/2005                   
Purpose     :Modified the parameter names                
APP_USE_VEHICLE_ID, ---------------------->>>>>>>>USE_VEHICLE                               
ANTI_LCK_BRAKES ---------->>>>>>>>>>>>>>>>>      ANTI_LOCK_BRAKES                
APP_VEHICLE_PERCLASS_ID---------->>>>>>>>>>>>>>CLASS_PER                
APP_VEHICLE_COMCLASS_ID ------------>>>>>>>>>>> CLASS_COM                 
APP_VEHICLE_PERTYPE_ID------->>>>>>>>>>VEHICLE_TYPE_PER                
APP_VEHICLE_COMTYPE_ID-------->>>>>>>>>>>VEHICLE_TYPE_COM                
 

Modified By : Pravesh K Chandel
Modified On :30 oct 2008
purpose		: Add new param Is_suspended
                            
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                                
CREATE PROC dbo.Proc_UpdateVehicleInformation                                
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
 @GRG_ADD1  nvarchar(70),                                
 @GRG_ADD2  nvarchar(70),                                
 @GRG_CITY  nvarchar(80),                                
 @GRG_COUNTRY      nvarchar(10),            
 @GRG_STATE  nvarchar(10),                                
 @GRG_ZIP  nvarchar(20),                                
 @REGISTERED_STATE  nvarchar(20),                                
 @TERRITORY  nvarchar(20),                                
 @CLASS  nvarchar(150),                                
 @REGN_PLATE_NUMBER nvarchar(150),                                
 @ST_AMT_TYPE   nvarchar(10),                                
 @VEHICLE_TYPE int,                 
 @VEHICLE_TYPE_CODE NVarChar(10)=null,                                
 @AMOUNT decimal=null,                                
 @SYMBOL int=null,                               
 @VEHICLE_AGE decimal=null,                                
 @VEHICLE_CC int =0,                                
 @MOTORCYCLE_TYPE int =0,                           
-- @IS_ACTIVE  nchar(1),                                
 @MODIFIED_BY     int,                                
 @LAST_UPDATED_DATETIME  datetime,                   
                                
 -- Modified by Anurag Verma 20/09/2005                                
 @IS_OWN_LEASE      nvarchar(10)=null,                                
 @PURCHASE_DATE     datetime=null,                                
 @IS_NEW_USED      nchar(1)=null,                                
 @MILES_TO_WORK      nvarchar(5)=null,                                
 @VEHICLE_USE      nvarchar(5)=null,                                
 @VEH_PERFORMANCE      nvarchar(5)=null,                                
 @MULTI_CAR       nvarchar(5)=null,                                
 @ANNUAL_MILEAGE      decimal (18, 0)=null,                                
 @PASSIVE_SEAT_BELT      nvarchar(5)=null,                                
 @AIR_BAG  nvarchar(5)=null,                                
 @ANTI_LOCK_BRAKES nvarchar(5)=null,                                
                                 
-- @UNINS_MOTOR_INJURY_COVE  nchar(5)=null,                                
-- @UNINS_PROPERTY_DAMAGE_COVE  nchar(5)=null,                                
-- @UNDERINS_MOTOR_INJURY_COVE  nchar(5)=null,                                 
                              
                              
-- field name changed by mohit  on 21-10-2005                         
 -- Modified by Vijay Arora 10-10-2005                            
                         
-- old field names                         
 @USE_VEHICLE INT,                              
 @CLASS_PER INT,                              
 @CLASS_COM INT,                              
 @VEHICLE_TYPE_PER INT,                              
 @VEHICLE_TYPE_COM INT,  
 @BUSS_PERM_RESI INT  = null,  
 @SNOWPLOW_CONDS INT  = null,  
 @CAR_POOL INT  = null,  
 @SAFETY_BELT int = null,  
@AUTO_POL_NO VARCHAR(10) = NULL,
@RADIUS_OF_USE INT = NULL,
@TRANSPORT_CHEMICAL VARCHAR(10) = NULL,
@COVERED_BY_WC_INSU VARCHAR(10) = NULL,
@CLASS_DESCRIPTION VARCHAR(10) = NULL  ,   
                         
-- new field names                           
-- @USE_VEHICLE   int,                            
-- @CLASS_PER int,                            
-- @CLASS_COM int,                            
-- @VEHICLE_TYPE_PER  int,                            
-- @VEHICLE_TYPE_COM  int                           
                        
--end                          
@IS_SUSPENDED  int =null
                              
)                                
AS                                
BEGIN                                
 DECLARE @VEHTYPE_ID Int                                
 --DECLARE @UNINS_MOTOR_INJURY_COVE NChar(5)                          
 --DECLARE @UNINS_PROPERTY_DAMAGE_COVE NChar(5)                          
 --DECLARE @UNDERINS_MOTOR_INJURY_COVE NChar(5)                    
        
IF(@USE_VEHICLE=11333)--COMMERCIAL USER        
BEGIN        
 SET @CLASS_PER=0        
 SET @VEHICLE_TYPE_PER=0        
END        
ELSE        
BEGIN        
 SET @CLASS_COM=0        
 SET @VEHICLE_TYPE_COM=0        
END        
                              
                          
 UPDATE APP_VEHICLES                                
 SET                                   
   -- modified by vj on 13-10-2005                            
 --INSURED_VEH_NUMBER=@INSURED_VEH_NUMBER,                                
  VEHICLE_YEAR=@VEHICLE_YEAR,            MAKE=@MAKE,                                
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
  VEHICLE_TYPE = @VEHICLE_TYPE,                                
  AMOUNT=@AMOUNT,                         
  SYMBOL=@SYMBOL,                                
  VEHICLE_AGE=@VEHICLE_AGE,                                
  VEHICLE_CC=@VEHICLE_CC,                                
  MOTORCYCLE_TYPE=@MOTORCYCLE_TYPE,                                
--  IS_ACTIVE  =@IS_ACTIVE,                                
  MODIFIED_BY  =@MODIFIED_BY,                        
  LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME,                                
  IS_OWN_LEASE=@IS_OWN_LEASE,                                
  PURCHASE_DATE=@PURCHASE_DATE,                                
  IS_NEW_USED=@IS_NEW_USED,                                
  MILES_TO_WORK=@MILES_TO_WORK,                                
  VEHICLE_USE=@VEHICLE_USE,                                
  VEH_PERFORMANCE=@VEH_PERFORMANCE,                                
  MULTI_CAR=@MULTI_CAR,                                
  ANNUAL_MILEAGE=@ANNUAL_MILEAGE,                                
  PASSIVE_SEAT_BELT=@PASSIVE_SEAT_BELT,                                
  AIR_BAG=@AIR_BAG,                                
  ANTI_LOCK_BRAKES=@ANTI_LOCK_BRAKES,                                
                                  
--  UNINS_MOTOR_INJURY_COVE=@UNINS_MOTOR_INJURY_COVE,                                
--  UNINS_PROPERTY_DAMAGE_COVE=@UNINS_PROPERTY_DAMAGE_COVE,                                
--  UNDERINS_MOTOR_INJURY_COVE=@UNDERINS_MOTOR_INJURY_COVE,                                
                              
  -- changes done by mohit on 21/10/2005                         
  -- added by vj on 10-10-2005                              
--  USE_VEHICLE = @USE_VEHICLE,                              
--  CLASS_PER = @CLASS_PER,                              
--  CLASS_COM = @CLASS_COM,                              
--  VEHICLE_TYPE_PER = @VEHICLE_TYPE_PER,                              
--  VEHICLE_TYPE_COM = @VEHICLE_TYPE_COM                         
  USE_VEHICLE=@USE_VEHICLE,                        
 CLASS_PER=@CLASS_PER,                        
 CLASS_COM =@CLASS_COM,                            
 VEHICLE_TYPE_PER=@VEHICLE_TYPE_PER,                   
 VEHICLE_TYPE_COM =@VEHICLE_TYPE_COM,  
 BUSS_PERM_RESI =@BUSS_PERM_RESI,  
SNOWPLOW_CONDS=@SNOWPLOW_CONDS,  
CAR_POOL=@CAR_POOL,  
SAFETY_BELT=@SAFETY_BELT,  
AUTO_POL_NO =@AUTO_POL_NO,
RADIUS_OF_USE=@RADIUS_OF_USE,
TRANSPORT_CHEMICAL=@TRANSPORT_CHEMICAL,
COVERED_BY_WC_INSU=@COVERED_BY_WC_INSU,
CLASS_DESCRIPTION=@CLASS_DESCRIPTION ,
IS_SUSPENDED=@IS_SUSPENDED,
IS_UPDATED=10963
--end                         
                              
  
                                  
 WHERE                                
  CUSTOMER_ID  =@CUSTOMER_ID AND                  
  APP_ID   =@APP_ID AND                                
  APP_VERSION_ID =@APP_VERSION_ID AND                                
  VEHICLE_ID  =@VEHICLE_ID                                
       
/*                       
 --Delete coverages from APP_VEHICLE_COVERAGES if required                          
 SELECT @UNINS_MOTOR_INJURY_COVE = UNINS_MOTOR_INJURY_COVE,                          
   @UNINS_PROPERTY_DAMAGE_COVE  = UNINS_PROPERTY_DAMAGE_COVE,                          
  @UNDERINS_MOTOR_INJURY_COVE = UNDERINS_MOTOR_INJURY_COVE                          
 FROM APP_VEHICLES                          
 WHERE                        
   CUSTOMER_ID  =@CUSTOMER_ID AND                         
   APP_ID   =@APP_ID AND                                
   APP_VERSION_ID =@APP_VERSION_ID AND                                
   VEHICLE_ID  =@VEHICLE_ID                                
            
 DECLARE @ID int      
                          
 IF ( @UNINS_MOTOR_INJURY_COVE = '10963' )                          
                           
   SELECT @ID = COVERAGE_ID FROM APP_VEHICLE_COVERAGES AVC                          
   INNER JOIN MNT_COVERAGE M ON                          
    AVC.COVERAGE_CODE_ID = M.COV_ID                          
   WHERE                                
     AVC.CUSTOMER_ID  = @CUSTOMER_ID AND                                
     AVC.APP_ID   = @APP_ID AND                                
 AVC.APP_VERSION_ID = @APP_VERSION_ID AND                                
     AVC.VEHICLE_ID  = @VEHICLE_ID AND                          
     M.COV_CODE =  'UMCSL'                          
                            
  IF ( @ID IS NOT NULL )                          
  BEGIN                          
   DELETE FROM APP_VEHICLE_COVERAGES                          
   WHERE                                
     CUSTOMER_ID  = @CUSTOMER_ID AND                                
     APP_ID   = @APP_ID AND                                
     APP_VERSION_ID = @APP_VERSION_ID AND                                
     VEHICLE_ID  = @VEHICLE_ID AND                           
     COVERAGE_ID = @ID                          
  END                          
                           
 IF ( @UNINS_PROPERTY_DAMAGE_COVE = '10963' )                          
 BEGIN                          
                        
   SELECT @ID = COVERAGE_ID FROM APP_VEHICLE_COVERAGES AVC                          
   INNER JOIN MNT_COVERAGE M ON                          
    AVC.COVERAGE_CODE_ID = M.COV_ID                          
   WHERE                                
     AVC.CUSTOMER_ID  = @CUSTOMER_ID AND                                
     AVC.APP_ID   = @APP_ID AND                                
     AVC.APP_VERSION_ID = @APP_VERSION_ID AND                                
     AVC.VEHICLE_ID  = @VEHICLE_ID AND                          
     M.COV_CODE =  'PD'                          
                             
   IF ( @ID IS NOT NULL )                          
  BEGIN                          
   DELETE FROM APP_VEHICLE_COVERAGES                          
   WHERE                                
    CUSTOMER_ID  = @CUSTOMER_ID AND                                
     APP_ID   = @APP_ID AND                                
     APP_VERSION_ID = @APP_VERSION_ID AND                                
     VEHICLE_ID  = @VEHICLE_ID AND                           
    COVERAGE_ID = @ID                          
  END                          
 END                          
                           
 IF ( @UNDERINS_MOTOR_INJURY_COVE = '10963' )                          
 BEGIN                          
                            
   SELECT @ID = COVERAGE_ID FROM APP_VEHICLE_COVERAGES AVC                          
   INNER JOIN MNT_COVERAGE M ON                     
    AVC.COVERAGE_CODE_ID = M.COV_ID                          
   WHERE             
     AVC.CUSTOMER_ID  = @CUSTOMER_ID AND                                
     AVC.APP_ID   = @APP_ID AND                                
     AVC.APP_VERSION_ID = @APP_VERSION_ID AND                                
     AVC.VEHICLE_ID  = @VEHICLE_ID AND                          
     M.COV_CODE =  'UNCSL'                          
                             
  IF ( @ID IS NOT NULL )                          
  BEGIN                          
   DELETE FROM APP_VEHICLE_COVERAGES                          
   WHERE                                
     CUSTOMER_ID  = @CUSTOMER_ID AND                                
     APP_ID   = @APP_ID AND                                
     APP_VERSION_ID = @APP_VERSION_ID AND                                
     VEHICLE_ID  = @VEHICLE_ID AND                           
     COVERAGE_ID = @ID                          
  END                          
 END                          
*/     
                         
END                              
                              
             
          
                        
                      
                    
                    
                  
                
              
            
          
        
      
    
  
  
  
  
  





GO

