IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CopyHOME_OWNER_RECREATION_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CopyHOME_OWNER_RECREATION_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_CopyHOME_OWNER_RECREATION_VEHICLES     
CREATE PROC Proc_CopyHOME_OWNER_RECREATION_VEHICLES        
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
 FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
       APP_ID = @APP_ID AND        
       APP_VERSION_ID = @APP_VERSION_ID        
         
 --Get next Company_ID_NUMBER        
 SELECT @NEXT_COMPANY_ID_NUMBER = ISNULL(MAX(COMPANY_ID_NUMBER),0)        
 FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
  APP_ID = @APP_ID AND         
  APP_VERSION_ID = @APP_VERSION_ID         
            
-- IF @NEXT_COMPANY_ID_NUMBER = 2147483647        
 IF @NEXT_COMPANY_ID_NUMBER = 9999     
 BEGIN        
  RETURN -1        
 END        
         
 SET @NEXT_COMPANY_ID_NUMBER = @NEXT_COMPANY_ID_NUMBER + 1        
        
 INSERT INTO APP_HOME_OWNER_RECREATIONAL_VEHICLES        
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
  CREATED_DATETIME ,  
  INSURING_VALUE,  
  DEDUCTIBLE ,
  UNIT_RENTED,
  YOUTHFUL_OPERATOR_UNDER_25,
  UNIT_OWNED_DEALERS ,
--Added For Itrack Issue # 6710 LIABILITY MEDICAL_PAYMENTS  PHYSICAL_DAMAGE
  LIABILITY ,
  MEDICAL_PAYMENTS,
  PHYSICAL_DAMAGE
 

  
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
  CREATED_DATETIME      ,  
  INSURING_VALUE,  
  DEDUCTIBLE,
  UNIT_RENTED,
  YOUTHFUL_OPERATOR_UNDER_25,
  UNIT_OWNED_DEALERS , 
   LIABILITY ,
  MEDICAL_PAYMENTS,
  PHYSICAL_DAMAGE
   
  
 FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
       APP_ID = @APP_ID AND        
       APP_VERSION_ID = @APP_VERSION_ID AND        
       REC_VEH_ID = @REC_VEH_ID           


INSERT INTO APP_HOMEOWNER_REC_VEH_ADD_INT
SELECT
	CUSTOMER_ID, 
	APP_ID ,
	APP_VERSION_ID ,
	HOLDER_ID ,
	@NEXT_REC_VEH_ID ,
	MEMO ,  
	NATURE_OF_INTEREST ,
	RANK ,
	LOAN_REF_NUMBER ,
	IS_ACTIVE ,
	CREATED_BY , 
	CREATED_DATETIME ,
	MODIFIED_BY ,
	LAST_UPDATED_DATETIME ,
	ADD_INT_ID ,
	HOLDER_NAME , 
	HOLDER_ADD1 , 
	HOLDER_ADD2 , 
	HOLDER_CITY , 
	HOLDER_COUNTRY,
	HOLDER_STATE ,
	HOLDER_ZIP  
FROM APP_HOMEOWNER_REC_VEH_ADD_INT 
WHERE           
CUSTOMER_ID = @CUSTOMER_ID AND          
       APP_ID = @APP_ID AND          
       APP_VERSION_ID = @APP_VERSION_ID AND 
	REC_VEH_ID = @REC_VEH_ID

        
 RETURN 1        
        
END        
      
    
  
  
  








GO

