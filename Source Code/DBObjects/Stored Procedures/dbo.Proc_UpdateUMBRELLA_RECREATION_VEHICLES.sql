IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateUMBRELLA_RECREATION_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateUMBRELLA_RECREATION_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
/*----------------------------------------------------------          
Proc Name       : dbo.Proc_UpdateUMBRELLA_RECREATION_VEHICLES          
Created by      : Pradeep Iyer          
Date            : 17 Jun 2005          
Purpose     : Updates a record in HOME_OWNER_RECREATION_VEHICLES          
Revison History :          
Used In  : Wolverine          
MOdified By  : Shafi        
Date    :  16 Dec 2005        
Purpose   :Change The Size Of STATE_REGISTERED        
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
          
CREATE PROC dbo.Proc_UpdateUMBRELLA_RECREATION_VEHICLES          
(          
 @CUSTOMER_ID int,          
 @APP_ID int,          
 @APP_VERSION_ID smallint,          
 @REC_VEH_ID smallint,          
 @COMPANY_ID_NUMBER     int,          
 @YEAR int,          
 @MAKE nvarchar(75),          
 @MODEL nvarchar(75),          
 @SERIAL nvarchar(75),          
 @STATE_REGISTERED nvarchar(5),          
 @VEHICLE_TYPE int,          
 @MANUFACTURER_DESC nvarchar(100),          
 @HORSE_POWER nvarchar(10),          
 @DISPLACEMENT nvarchar(10),          
 @REMARKS nvarchar(500),          
 @USED_IN_RACE_SPEED nchar(1),          
 @PRIOR_LOSSES nchar(1),          
 @IS_UNIT_REG_IN_OTHER_STATE nchar(1),          
 @RISK_DECL_BY_OTHER_COMP nchar(1),          
 @DESC_RISK_DECL_BY_OTHER_COMP nvarchar(500),          
 @VEHICLE_MODIFIED nchar(1),          
 @MODIFIED_BY int,  
 @VEHICLE_MODIFIED_DETAILS varchar(100),
 @VEH_LIC_ROAD smallint,
 @REC_VEH_TYPE smallint,
 @REC_VEH_TYPE_DESC varchar(25),
 @USED_IN_RACE_SPEED_CONTEST varchar(250),
 @OTHER_POLICY nvarchar(150) = null,
 @C44 smallint = null,
 @IS_BOAT_EXCLUDED int = null
           
)          
AS          
          
BEGIN          
             
 IF EXISTS          
 (          
  SELECT * FROM APP_UMBRELLA_RECREATIONAL_VEHICLES          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
         APP_ID = @APP_ID AND          
   APP_VERSION_ID = @APP_VERSION_ID AND          
   COMPANY_ID_NUMBER = @COMPANY_ID_NUMBER AND          
   REC_VEH_ID <> @REC_VEH_ID          
 )          
 BEGIN          
  RETURN -1          
 END          
           
 UPDATE APP_UMBRELLA_RECREATIONAL_VEHICLES          
 SET          
            
  COMPANY_ID_NUMBER = @COMPANY_ID_NUMBER,          
  YEAR = @YEAR,          
  MAKE = @MAKE,          
  MODEL = @MODEL,          
  SERIAL = @SERIAL,          
  STATE_REGISTERED = @STATE_REGISTERED,          
  VEHICLE_TYPE = @VEHICLE_TYPE,          
  MANUFACTURER_DESC = @MANUFACTURER_DESC,          
  HORSE_POWER = @HORSE_POWER,          
  DISPLACEMENT = @DISPLACEMENT,          
  REMARKS = @REMARKS,          
  USED_IN_RACE_SPEED = @USED_IN_RACE_SPEED,          
  PRIOR_LOSSES = @PRIOR_LOSSES,          
  IS_UNIT_REG_IN_OTHER_STATE = @IS_UNIT_REG_IN_OTHER_STATE,          
  RISK_DECL_BY_OTHER_COMP = @RISK_DECL_BY_OTHER_COMP,          
  DESC_RISK_DECL_BY_OTHER_COMP = @DESC_RISK_DECL_BY_OTHER_COMP,          
  VEHICLE_MODIFIED = @VEHICLE_MODIFIED,          
  MODIFIED_BY = @MODIFIED_BY,          
  LAST_UPDATED_DATETIME = GetDate(),  
  VEHICLE_MODIFIED_DETAILS = @VEHICLE_MODIFIED_DETAILS,
  VEH_LIC_ROAD=@VEH_LIC_ROAD,
  REC_VEH_TYPE=@REC_VEH_TYPE,
  REC_VEH_TYPE_DESC=@REC_VEH_TYPE_DESC,
  USED_IN_RACE_SPEED_CONTEST=@USED_IN_RACE_SPEED_CONTEST,
  OTHER_POLICY = @OTHER_POLICY,
  C44 = @C44,
  IS_BOAT_EXCLUDED = @IS_BOAT_EXCLUDED
           
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
       APP_ID = @APP_ID AND          
       APP_VERSION_ID = APP_VERSION_ID AND          
       REC_VEH_ID = @REC_VEH_ID          
      
 RETURN 1           
          
END          
          
          
          
          
          
          
          
          
        
      
      
    
  







GO

