IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CopyUMBRELLA_RECREATION_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CopyUMBRELLA_RECREATION_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_CopyUMBRELLA_RECREATION_VEHICLES    
Created by      : Pradeep Iyer    
Date            : 17 Jun 2005    
Purpose     : Copies a record in UMBRELLA_RECREATION_VEHICLES    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
    
CREATE     PROC dbo.Proc_CopyUMBRELLA_RECREATION_VEHICLES    
(    
 @CUSTOMER_ID int,    
 @APP_ID int,    
 @APP_VERSION_ID smallint,    
 @SERIAL nvarchar(150),    
 @REC_VEH_ID smallint    
     
)    
AS    
    
DECLARE @NEXT_REC_VEH_ID smallint    
DECLARE @NEXT_COMPANY_ID_NUMBER int    
    
BEGIN    
     
 --Get next REC_VEH_ID    
 SELECT  @NEXT_REC_VEH_ID = ISNULL(MAX(REC_VEH_ID),0) + 1    
 FROM APP_UMBRELLA_RECREATIONAL_VEHICLES    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
       APP_ID = @APP_ID AND    
       APP_VERSION_ID = @APP_VERSION_ID    
     
 --Get next Company_ID_NUMBER    
 SELECT @NEXT_COMPANY_ID_NUMBER = ISNULL(MAX(COMPANY_ID_NUMBER),0)    
 FROM APP_UMBRELLA_RECREATIONAL_VEHICLES    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  APP_ID = @APP_ID AND     
  APP_VERSION_ID = @APP_VERSION_ID     
        
 IF @NEXT_COMPANY_ID_NUMBER = 2147483647    
 BEGIN    
  RETURN -1    
 END    
     
 SET @NEXT_COMPANY_ID_NUMBER = @NEXT_COMPANY_ID_NUMBER + 1    
    
 INSERT INTO APP_UMBRELLA_RECREATIONAL_VEHICLES    
 (    
  CUSTOMER_ID,    
  APP_ID,    
  APP_VERSION_ID,    
  REC_VEH_ID,    
  COMPANY_ID_NUMBER,    
  YEAR,    
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
  USED_IN_RACE_SPEED_CONTEST    
 )    
 SELECT    
  CUSTOMER_ID,    
  APP_ID,    
  APP_VERSION_ID,    
  @NEXT_REC_VEH_ID,    
  @NEXT_COMPANY_ID_NUMBER,    
  YEAR,    
  MAKE,    
  MODEL,    
  @SERIAL,    
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
  USED_IN_RACE_SPEED_CONTEST
 FROM APP_UMBRELLA_RECREATIONAL_VEHICLES    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
       APP_ID = @APP_ID AND    
       APP_VERSION_ID = @APP_VERSION_ID AND    
       REC_VEH_ID = @REC_VEH_ID       
    
 RETURN 1    
    
END    
    
    
    
    
    
    
    
  



GO

