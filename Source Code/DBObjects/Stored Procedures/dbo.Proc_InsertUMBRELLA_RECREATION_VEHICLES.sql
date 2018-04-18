IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUMBRELLA_RECREATION_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUMBRELLA_RECREATION_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
/*----------------------------------------------------------        
Proc Name       : dbo.Proc_InsertUMBRELLA_RECREATION_VEHICLES        
Created by      : Pradeep Iyer        
Date            : Jun 17, 2005        
Purpose      : Inserts a record in UMBRELLA_RECREATION_VEHICLES        
Revison History :        
Used In   : Wolverine        
------------------------------------------------------------        
Date   : July 14, 2005        
Modified By  : Anshuman        
Comments  : set parameter length according to table definition        
------   ------------       -------------------------*/        
CREATE PROC dbo.Proc_InsertUMBRELLA_RECREATION_VEHICLES        
(        
 @CUSTOMER_ID int,        
 @APP_ID int,        
 @APP_VERSION_ID smallint,        
 @COMPANY_ID_NUMBER     int,        
 @YEAR int,        
 @MAKE nvarchar(75),        
 @MODEL nvarchar(75),        
 @SERIAL nvarchar(75),        
 @STATE_REGISTERED nvarchar(5),        
 @VEHICLE_TYPE nvarchar(4),        
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
 @CREATED_BY int,        
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
        
DECLARE @REC_VEH_ID smallint        
        
BEGIN        
         
 IF EXISTS        
 (        
  SELECT * FROM APP_UMBRELLA_RECREATIONAL_VEHICLES        
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
         APP_ID = @APP_ID AND        
   APP_VERSION_ID = @APP_VERSION_ID AND        
   COMPANY_ID_NUMBER = @COMPANY_ID_NUMBER        
 )        
 BEGIN        
  RETURN -1        
 END        
        
 SELECT  @REC_VEH_ID = ISNULL(MAX(REC_VEH_ID),0) + 1        
 FROM APP_UMBRELLA_RECREATIONAL_VEHICLES        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
       APP_ID = @APP_ID AND        
       APP_VERSION_ID = @APP_VERSION_ID        
              
 INSERT INTO APP_UMBRELLA_RECREATIONAL_VEHICLES        
 (        
  CUSTOMER_ID,        
  APP_ID,        
  APP_VERSION_ID,        
  REC_VEH_ID,        
  COMPANY_ID_NUMBER,        
  [YEAR],        
  MAKE,        
  MODEL,        
  SERIAL,        
  STATE_REGISTERED,        
  VEHICLE_TYPE,        
  MANUFACTURER_DESC,        
  HORSE_POWER,        
  DISPLACEMENT,        
  REMARKS,        
  USED_IN_RACE_SPEED,        
  PRIOR_LOSSES,        
  IS_UNIT_REG_IN_OTHER_STATE,        
  RISK_DECL_BY_OTHER_COMP,        
  DESC_RISK_DECL_BY_OTHER_COMP,        
  VEHICLE_MODIFIED,        
  ACTIVE,        
  CREATED_BY,        
  CREATED_DATETIME,  
  VEHICLE_MODIFIED_DETAILS,
  VEH_LIC_ROAD,
  REC_VEH_TYPE,
  REC_VEH_TYPE_DESC,
  USED_IN_RACE_SPEED_CONTEST,
  OTHER_POLICY,
  C44,
  IS_BOAT_EXCLUDED
        
 )        
 VALUES        
 (        
  @CUSTOMER_ID,        
  @APP_ID,        
  @APP_VERSION_ID,        
  @REC_VEH_ID,        
  @COMPANY_ID_NUMBER,        
  @YEAR,        
  @MAKE,        
  @MODEL,        
  @SERIAL,        
  @STATE_REGISTERED,        
  @VEHICLE_TYPE,        
  @MANUFACTURER_DESC,        
  @HORSE_POWER,        
  @DISPLACEMENT,        
  @REMARKS,        
  @USED_IN_RACE_SPEED,        
  @PRIOR_LOSSES,        
  @IS_UNIT_REG_IN_OTHER_STATE,  
  @RISK_DECL_BY_OTHER_COMP,        
  @DESC_RISK_DECL_BY_OTHER_COMP,        
  @VEHICLE_MODIFIED,        
  'Y',        
  @CREATED_BY,        
  GetDate(),  
  @VEHICLE_MODIFIED_DETAILS,
  @VEH_LIC_ROAD,
  @REC_VEH_TYPE,
  @REC_VEH_TYPE_DESC,
  @USED_IN_RACE_SPEED_CONTEST,
  @OTHER_POLICY,
  @C44,
  @IS_BOAT_EXCLUDED
 )        
         
 IF @@ERROR <> 0        
 BEGIN        
  RETURN -4        
 END        
        
 RETURN @REC_VEH_ID        
        
END        
        
        
        
        
        
        
        
        
        
        
      
    
  







GO

