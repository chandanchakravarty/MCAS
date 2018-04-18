IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_COPY_UMBRELLA_RECREATION_VEHICLES_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_COPY_UMBRELLA_RECREATION_VEHICLES_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
CREATE PROC dbo.PROC_COPY_UMBRELLA_RECREATION_VEHICLES_POLICY    
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
 FROM POL_UMBRELLA_RECREATIONAL_VEHICLES            
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
       POLICY_ID = @POL_ID AND            
       POLICY_VERSION_ID = @POL_VERSION_ID            
             
 --GET NEXT COMPANY_ID_NUMBER            
 SELECT @NEXT_COMPANY_ID_NUMBER = ISNULL(MAX(COMPANY_ID_NUMBER),0)            
 FROM POL_UMBRELLA_RECREATIONAL_VEHICLES            
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
  POLICY_ID = @POL_ID AND             
  POLICY_VERSION_ID = @POL_VERSION_ID             
                
-- IF @NEXT_COMPANY_ID_NUMBER = 2147483647            
 IF @NEXT_COMPANY_ID_NUMBER = 9999         
 BEGIN            
  RETURN -1            
 END            
             
 SET @NEXT_COMPANY_ID_NUMBER = @NEXT_COMPANY_ID_NUMBER + 1            
            
 INSERT INTO POL_UMBRELLA_RECREATIONAL_VEHICLES            
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
 VEHICLE_TYPE,  
 ACTIVE,  
 CREATED_BY,  
 CREATED_DATETIME,  
 MODIFIED_BY,  
 LAST_UPDATED_DATETIME,  
 VEHICLE_MODIFIED_DETAILS,  
 VEH_LIC_ROAD,  
 REC_VEH_TYPE,  
 REC_VEH_TYPE_DESC,  
 USED_IN_RACE_SPEED_CONTEST  
  
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
 VEHICLE_TYPE,  
 ACTIVE,  
 CREATED_BY,  
 CREATED_DATETIME,  
 MODIFIED_BY,  
 LAST_UPDATED_DATETIME,  
 VEHICLE_MODIFIED_DETAILS,  
 VEH_LIC_ROAD,  
 REC_VEH_TYPE,  
 REC_VEH_TYPE_DESC,  
 USED_IN_RACE_SPEED_CONTEST  
      
 FROM POL_UMBRELLA_RECREATIONAL_VEHICLES            
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
       POLICY_ID = @POL_ID AND            
       POLICY_VERSION_ID = @POL_VERSION_ID AND            
       REC_VEH_ID = @REC_VEH_ID               
  
 RETURN 1            
            
END            
  
  
  



GO

