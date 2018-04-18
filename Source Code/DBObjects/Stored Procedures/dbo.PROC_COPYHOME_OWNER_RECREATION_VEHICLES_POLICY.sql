IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_COPYHOME_OWNER_RECREATION_VEHICLES_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_COPYHOME_OWNER_RECREATION_VEHICLES_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc PROC_COPYHOME_OWNER_RECREATION_VEHICLES_POLICY 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROC PROC_COPYHOME_OWNER_RECREATION_VEHICLES_POLICY  
(          
 @CUSTOMER_ID INT,          
 @POL_ID INT,          
 @POL_VERSION_ID SMALLINT,          
 @SERIAL NVARCHAR(150),          
 @REC_VEH_ID SMALLINT                     
)          
AS          
          
DECLARE @NEXT_REC_VEH_ID SMALLINT          
DECLARE @NEXT_COMPANY_ID_NUMBER INT          
          
BEGIN          
           
 --GET NEXT REC_VEH_ID          
 SELECT  @NEXT_REC_VEH_ID = ISNULL(MAX(REC_VEH_ID),0) + 1          
 FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
       POLICY_ID = @POL_ID AND          
       POLICY_VERSION_ID = @POL_VERSION_ID          
           
 --GET NEXT COMPANY_ID_NUMBER          
 SELECT @NEXT_COMPANY_ID_NUMBER = ISNULL(MAX(COMPANY_ID_NUMBER),0)          
 FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
  POLICY_ID = @POL_ID AND           
  POLICY_VERSION_ID = @POL_VERSION_ID           
              
-- IF @NEXT_COMPANY_ID_NUMBER = 2147483647          
 IF @NEXT_COMPANY_ID_NUMBER = 9999       
 BEGIN          
  RETURN -1          
 END          
           
 SET @NEXT_COMPANY_ID_NUMBER = @NEXT_COMPANY_ID_NUMBER + 1          
          
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
  DEDUCTIBLE,
  UNIT_RENTED,
  UNIT_OWNED_DEALERS,
  YOUTHFUL_OPERATOR_UNDER_25,
--Added For Itrack Issue # 6710 LIABILITY MEDICAL_PAYMENTS  PHYSICAL_DAMAGE
   LIABILITY ,
  MEDICAL_PAYMENTS,
  PHYSICAL_DAMAGE
     
 )          
 SELECT          
  CUSTOMER_ID,          
  POLICY_ID,          
  POLICY_VERSION_ID,          
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
  UNIT_OWNED_DEALERS,
  YOUTHFUL_OPERATOR_UNDER_25,
   LIABILITY ,
  MEDICAL_PAYMENTS,
  PHYSICAL_DAMAGE
     
    
 FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
       POLICY_ID = @POL_ID AND          
       POLICY_VERSION_ID = @POL_VERSION_ID AND          
       REC_VEH_ID = @REC_VEH_ID             

INSERT INTO POL_HOMEOWNER_REC_VEH_ADD_INT
SELECT
CUSTOMER_ID, 
POLICY_ID ,
POLICY_VERSION_ID ,
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
FROM POL_HOMEOWNER_REC_VEH_ADD_INT 
WHERE           
CUSTOMER_ID = @CUSTOMER_ID AND          
       POLICY_ID = @POL_ID AND          
       POLICY_VERSION_ID = @POL_VERSION_ID AND 
	REC_VEH_ID = @REC_VEH_ID
 RETURN 1          
          
END          






GO

