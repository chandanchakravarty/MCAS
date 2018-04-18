IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHORule_RVInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHORule_RVInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                                        
Proc Name                : dbo.Proc_GetHORule_RVInfo_Pol                                                                                    
Created by               : Manoj Rathore                                                                                        
Date                     : 24 Nov ,2006                                        
Purpose                  : To get the Recreational Vehicles detail for HO rules                                        
Revison History          :                                                                                        
Used In                  : Wolverine                                                                                        
------------------------------------------------------------                                                                                        
Date     Review By          Comments                                                                                        
------   ------------       -------------------------*/                              
--drop proc dbo.Proc_GetHORule_RVInfo_Pol                                                                          
CREATE proc dbo.Proc_GetHORule_RVInfo_Pol                           
(                                                                                        
 @CUSTOMER_ID    int,                                                                                        
 @POLICY_ID    int,                                                                                        
 @POLICY_VERSION_ID   int,                          
 @RV_VEHICLEID int                                      
                                                                                                  
)                                                                                        
AS                                                                                            
BEGIN                                                         
  -- Mandatery Info                           
  DECLARE @COMPANY_ID_NUMBER INT                                       
  DECLARE @RV_YEAR NVARCHAR(20)                                        
  DECLARE @RV_MAKE NVARCHAR(75)                                        
  DECLARE @RV_MODEL NVARCHAR(75)                            
  DECLARE @RV_SERIAL NVARCHAR(75)                            
  DECLARE @RV_STATE_REGISTERED NVARCHAR(20)                            
  DECLARE @RV_HORSE_POWER NVARCHAR(10)                            
  DECLARE @RV_PRIOR_LOSSES NVARCHAR(1)                                        
  DECLARE @INTRV_TYPE INT                        
  DECLARE @RV_TYPE NVARCHAR(20)                            
  DECLARE @RV_INSURING_VALUE DECIMAL(18,0)                            
  DECLARE @RV_USED_IN_RACE_SPEED  NVARCHAR(2)  -- Changed from nvarchar(1) by Charles on 10-Dec-09 for Itrack 6841                             
  DECLARE @RV_DEDUCTIBLE  DECIMAL(18,0)    
  declare @RV_COV_SELC nvarchar(20)                            
 --- declare @VEHICLEID smallint                            
 --- declare @intREC_VEH_ID smallint                                   
                                         
 IF EXISTS (SELECT CUSTOMER_ID FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES                            
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND REC_VEH_ID=@RV_VEHICLEID)                                        
 BEGIN                                         
 SELECT  @COMPANY_ID_NUMBER=isnull (COMPANY_ID_NUMBER,0),                          
 @RV_YEAR=isnull(convert(nvarchar(20),YEAR),''),@RV_MAKE=isnull(MAKE,''),                                        
 @RV_MODEL=isnull(MODEL,''),                      
 @intRV_TYPE=isnull(VEHICLE_TYPE,0),                          
 @RV_HORSE_POWER=ISNULL(HORSE_POWER,''),                      
 @RV_USED_IN_RACE_SPEED=ISNULL(USED_IN_RACE_SPEED,'-1') ,   -- Changed from '' by Charles on 10-Dec-09 for Itrack 6841                         
 @RV_SERIAL=ISNULL(SERIAL,''),                            
 @RV_STATE_REGISTERED=ISNULL(CONVERT(nvarchar(20),STATE_REGISTERED),'' ),                          
 @RV_INSURING_VALUE=ISNULL(INSURING_VALUE,0),                            
 @RV_DEDUCTIBLE =ISNULL(DEDUCTIBLE,0)                          
 FROM    POL_HOME_OWNER_RECREATIONAL_VEHICLES                             
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND REC_VEH_ID=@RV_VEHICLEID                                        
 END             
 ELSE             
 BEGIN            
 -- MANDATORY                             
 SET @COMPANY_ID_NUMBER =0              
 SET @RV_YEAR =''            
 SET @RV_MAKE =''                                                                   
 SET @RV_MODEL =''             
 SET @INTRV_TYPE =0                                                                                                       
 SET @RV_HORSE_POWER =''             
 SET @RV_USED_IN_RACE_SPEED ='-1'   -- Changed from '' by Charles on 10-Dec-09 for Itrack 6841            
 SET @RV_SERIAL =''                                                                                                       
 SET @RV_STATE_REGISTERED =''             
 SET @RV_INSURING_VALUE =0             
 SET @RV_DEDUCTIBLE =0             
 END                                                         
------------------------------------------------                
          
--Added by Charles on 27-Jul-09 for Itrack 6176, when engine displacement exceeds 750 cc            
 DECLARE @RV_HORSE_POWER_OVER_750 varchar(10)               
            
 IF (CONVERT(BIGINT,@RV_HORSE_POWER) > 750 AND (@INTRV_TYPE = 11434 OR @INTRV_TYPE = 11435)) --MINI-BIKES & TRAILBIKES            
 BEGIN            
 SET @RV_HORSE_POWER_OVER_750 ='Y'            
 END            
 ELSE            
 BEGIN            
 SET @RV_HORSE_POWER_OVER_750 ='N'            
 END            
--Added till here             
        
-- Added by Charles on 19-Nov-09 for Itrack 6710          
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES WITH(NOLOCK)           
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND REC_VEH_ID=@RV_VEHICLEID AND PHYSICAL_DAMAGE = '10964')          
BEGIN           
 SET @RV_INSURING_VALUE = -1                      
END          
--Added till here                     
                         
  ---REJECT IF RECREATIONAL VEHICLE IS  ATV/4-6WHEELER AND  SNOWMOBILES                                    
--Commented by Charles on 18-Jan-10 for Itrack 6780  
/*  
IF(@INTRV_TYPE = 11431 OR @INTRV_TYPE = 11433)                                     
      BEGIN                             
           SET @RV_TYPE ='Y'                            
      END     
*/                   
                            
  ---Reject if Snowmobile 600 cc to 800 cc                 
  declare @RV_HORSE_POWER_OVER_800 varchar(20)                         
                              
  IF (CONVERT(BIGINT,@RV_HORSE_POWER) BETWEEN 600 AND 800 AND @INTRV_TYPE=11431)                                     
      BEGIN                                    
           SET @RV_HORSE_POWER ='Y'                                    
      END               
   ELSE IF (CONVERT(BIGINT,@RV_HORSE_POWER) >800 AND(@INTRV_TYPE = 11431 OR @INTRV_TYPE = 11433))   --IF HP OVER 800                             
--Added RV_Type check for ATV/4-6wheeler(11433) and  Snowmobiles(11431), Itrack 6176, 27-Jul-09, Charles.              
    BEGIN                                  
     SET @RV_HORSE_POWER_OVER_800 ='Y'                                  
     END               
  ELSE              
     BEGIN              
     SET   @RV_HORSE_POWER='N'              
     END                                    
                                  
  ---Reject if race speed is yes   
 IF(@RV_USED_IN_RACE_SPEED='Y')                              
  BEGIN                            
  SET @RV_USED_IN_RACE_SPEED='Y'                             
  END    
 ELSE -- Added by Charles on 10-Dec-09 for Itrack 6841      
 BEGIN  
  SET @RV_USED_IN_RACE_SPEED='-1'    
 END   --Added till here                                  
END      
    
-- Added by Charles on 7-Dec-09 for Itrack 6798      
 DECLARE @REC_VEHICLE_PRIN_OCC_ID CHAR(2)      
 DECLARE @POLICY_LOB NVARCHAR(10)               
 SET @REC_VEHICLE_PRIN_OCC_ID = '-1'    
    
SELECT @POLICY_LOB=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                  
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID     
     
IF @POLICY_LOB = '1'        
BEGIN                 
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE WITH(NOLOCK)          
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND POL_REC_VEHICLE_PRIN_OCC_ID = 0)          
 BEGIN          
  SET @REC_VEHICLE_PRIN_OCC_ID=''          
 END          
END   
  
SET @RV_COV_SELC='N'  
IF EXISTS(SELECT 1 FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES  
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID   
   AND REC_VEH_ID = @RV_VEHICLEID AND ISNULL(LIABILITY,'10964')='10964' AND  ISNULL(MEDICAL_PAYMENTS,'10964')='10964'   
   AND ISNULL(PHYSICAL_DAMAGE,'10964')='10964')  
 BEGIN  
  SET @RV_COV_SELC='Y'  
 END     
--- Added till here                    
                  
-------------Recreational Vehicles ----------                                     
BEGIN                                        
SELECT                                         
  @COMPANY_ID_NUMBER as COMPANY_ID_NUMBER,                                  
  @RV_MAKE  as  RV_MAKE,                                        
  @RV_MODEL as  RV_MODEL,                                        
  @RV_TYPE  as  RV_TYPE,                                        
  @RV_YEAR  as  RV_YEAR ,          
  @RV_SERIAL as RV_SERIAL,                            
  @RV_HORSE_POWER        as RV_HORSE_POWER,                            
  @RV_USED_IN_RACE_SPEED as RV_USED_IN_RACE_SPEED,                            
  @RV_DEDUCTIBLE         as RV_DEDUCTIBLE,                            
  @RV_STATE_REGISTERED   as RV_STATE_REGISTERED,                            
  @RV_INSURING_VALUE     as RV_INSURING_VALUE  ,              
  @RV_HORSE_POWER_OVER_800 as RV_HORSE_POWER_OVER_800,          
  @RV_HORSE_POWER_OVER_750 as RV_HORSE_POWER_OVER_750,  --Added by Charles on 27-Jul-09 for Itrack 6176            
  @REC_VEHICLE_PRIN_OCC_ID AS REC_VEHICLE_PRIN_OCC_ID, -- Added by Charles on 7-Dec-09 for Itrack 6798              
  @RV_COV_SELC as RV_COV_SELC                                       
END   
GO

