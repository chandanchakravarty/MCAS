IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_POL_RECREATION_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_POL_RECREATION_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name       : dbo.Proc_Insert_POL_OWNER_RECREATION_VEHICLES              
Created by      : Pradeep Iyer              
Date            : 11/10/2005              
Purpose       : Inserts a record in POL_HOME_OWNER_RECREATION_VEHICLES              
Revison History :              
Used In   : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
--drop proc Proc_Insert_POL_RECREATION_VEHICLES              
CREATE PROC dbo.Proc_Insert_POL_RECREATION_VEHICLES              
(              
 @CUSTOMER_ID int,              
 @POLICY_ID int,              
 @POLICY_VERSION_ID smallint,              
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
 @CREATED_BY int,
 @INSURING_VALUE decimal(18,2),
 @DEDUCTIBLE decimal ,
 @UNIT_RENTED nchar(2)= null,   
 @UNIT_OWNED_DEALERS nchar(2)= null,   
 @YOUTHFUL_OPERATOR_UNDER_25 nchar(2)=null , 
  --Added LIABILITY , MEDICAL_PAYMENTS , PHYSICAL_DAMAGE For Itrack Issue #6710 
 @LIABILITY varchar(20), 
 @MEDICAL_PAYMENTS varchar(20),
 @PHYSICAL_DAMAGE varchar(20)        
               
        
)              
AS              
              
DECLARE @REC_VEH_ID smallint              
              
BEGIN              
               
 IF EXISTS              
 (              
  SELECT * FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES              
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
         POLICY_ID = @POLICY_ID AND              
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND              
   COMPANY_ID_NUMBER = @COMPANY_ID_NUMBER              
 )              
 BEGIN              
  RETURN -1              
 END              
              
 SELECT  @REC_VEH_ID = ISNULL(MAX(REC_VEH_ID),0) + 1              
 FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
       POLICY_ID = @POLICY_ID AND              
       POLICY_VERSION_ID = @POLICY_VERSION_ID              
                    
 INSERT INTO POL_HOME_OWNER_RECREATIONAL_VEHICLES              
 (              
  CUSTOMER_ID,              
  POLICY_ID,              
  POLICY_VERSION_ID,              
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
  --DISPLACEMENT,              
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
  INSURING_VALUE,
  DEDUCTIBLE,
  UNIT_RENTED,   
  UNIT_OWNED_DEALERS ,  
  YOUTHFUL_OPERATOR_UNDER_25 , 
 --Added LIABILITY , MEDICAL_PAYMENTS , PHYSICAL_DAMAGE For Itrack Issue #6710
  LIABILITY ,    
  MEDICAL_PAYMENTS ,  
  PHYSICAL_DAMAGE                  
)              
 VALUES              
 (              
  @CUSTOMER_ID,              
  @POLICY_ID,              
  @POLICY_VERSION_ID,              
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
 --@DISPLACEMENT,              
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
  @INSURING_VALUE,
  @DEDUCTIBLE,
  @UNIT_RENTED,   
  @UNIT_OWNED_DEALERS ,  
  @YOUTHFUL_OPERATOR_UNDER_25 ,
 --Added LIABILITY , MEDICAL_PAYMENTS , PHYSICAL_DAMAGE For Itrack Issue #6710
  @LIABILITY ,      
  @MEDICAL_PAYMENTS , 
  @PHYSICAL_DAMAGE                 
 )              
               
 IF @@ERROR <> 0           
 BEGIN              
  RETURN -4              
 END              
 
             
 RETURN @REC_VEH_ID              
              

END              







GO

