IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_POL_HOME_OWNER_RECREATION_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_POL_HOME_OWNER_RECREATION_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
/*----------------------------------------------------------              
Proc Name       : dbo.Proc_Update_POL_HOME_OWNER_RECREATION_VEHICLES              
Created by      : Pradeep Iyer              
Date            : 11/10/2005              
Purpose      : Updates a record in POL_HOME_OWNER_RECREATION_VEHICLES              
Revison History :              
Used In  : Wolverine                     
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              

-- drop proc Proc_Update_POL_HOME_OWNER_RECREATION_VEHICLES                            
CREATE  PROC dbo.Proc_Update_POL_HOME_OWNER_RECREATION_VEHICLES              
(              
 @CUSTOMER_ID int,              
 @POLICY_ID int,              
 @POLICY_VERSION_ID smallint,              
 @REC_VEH_ID smallint,              
 @COMPANY_ID_NUMBER     int,              
 @YEAR int,              
 @MAKE nvarchar(75),              
 @MODEL nvarchar(75),              
 @SERIAL nvarchar(75),              
 @STATE_REGISTERED int,              
 @VEHICLE_TYPE int,              
 @MANUFACTURER_DESC nvarchar(100),              
 @HORSE_POWER nvarchar(10),              
 --@DISPLACEMENT nvarchar(10),              
 @REMARKS nvarchar(500),              
 @USED_IN_RACE_SPEED nchar(1),              
 @PRIOR_LOSSES nchar(1),              
 @IS_UNIT_REG_IN_OTHER_STATE nchar(1),              
 @RISK_DECL_BY_OTHER_COMP nchar(1),              
 @DESC_RISK_DECL_BY_OTHER_COMP nvarchar(500),              
 @VEHICLE_MODIFIED nchar(1),              
 @MODIFIED_BY int,
 @INSURING_VALUE decimal(18,2),  
 @DEDUCTIBLE decimal,
 @UNIT_RENTED nchar(2), 
 @UNIT_OWNED_DEALERS nchar(2),
 @YOUTHFUL_OPERATOR_UNDER_25 nchar(2),
--Added LIABILITY , MEDICAL_PAYMENTS , PHYSICAL_DAMAGE For Itrack Issue #6710 
 @LIABILITY varchar(20),
 @MEDICAL_PAYMENTS varchar(20),
 @PHYSICAL_DAMAGE varchar(20) 
                              
              
)              
AS              
              
BEGIN              
                 
 IF EXISTS              
 (              
  SELECT * FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES              
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
         POLICY_ID = @POLICY_ID AND              
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND              
   COMPANY_ID_NUMBER = @COMPANY_ID_NUMBER AND              
   REC_VEH_ID <> @REC_VEH_ID              
 )              
 BEGIN              
  RETURN -1              
 END              
               
 UPDATE POL_HOME_OWNER_RECREATIONAL_VEHICLES              
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
  --DISPLACEMENT = @DISPLACEMENT,              
  REMARKS = @REMARKS,              
  USED_IN_RACE_SPEED = @USED_IN_RACE_SPEED,              
  PRIOR_LOSSES = @PRIOR_LOSSES,              
  IS_UNIT_REG_IN_OTHER_STATE = @IS_UNIT_REG_IN_OTHER_STATE,              
  RISK_DECL_BY_OTHER_COMP = @RISK_DECL_BY_OTHER_COMP,              
  DESC_RISK_DECL_BY_OTHER_COMP = @DESC_RISK_DECL_BY_OTHER_COMP,              
  VEHICLE_MODIFIED = @VEHICLE_MODIFIED,              
  MODIFIED_BY = @MODIFIED_BY,              
  LAST_UPDATED_DATETIME = GetDate(),
  INSURING_VALUE=@INSURING_VALUE ,
  DEDUCTIBLE =@DEDUCTIBLE,
  UNIT_RENTED=@UNIT_RENTED , 
  UNIT_OWNED_DEALERS=@UNIT_OWNED_DEALERS,
  YOUTHFUL_OPERATOR_UNDER_25= @YOUTHFUL_OPERATOR_UNDER_25  ,
--Added LIABILITY , MEDICAL_PAYMENTS , PHYSICAL_DAMAGE For Itrack Issue #6710 
  LIABILITY  = @LIABILITY ,  
  MEDICAL_PAYMENTS = @MEDICAL_PAYMENTS ,
  PHYSICAL_DAMAGE = @PHYSICAL_DAMAGE 
    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
       POLICY_ID = @POLICY_ID AND              
       POLICY_VERSION_ID = POLICY_VERSION_ID AND              
       REC_VEH_ID = @REC_VEH_ID              
 
 --EXEC Proc_UpdatePolicyHomeEndorsementFromRV @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID             
 RETURN 1               
              
END              






GO

