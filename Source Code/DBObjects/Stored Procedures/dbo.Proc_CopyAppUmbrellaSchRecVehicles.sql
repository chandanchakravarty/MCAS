IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CopyAppUmbrellaSchRecVehicles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CopyAppUmbrellaSchRecVehicles]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
Proc Name   : dbo.Proc_CopyAppUmbrellaSchRecVehicles                
Created by  : Sumit Chhabra      
Date        : 09 Oc,2006                
Purpose     : Copy locations at umbrella sch.       
Revison History  :                                
 ------------------------------------------------------------                                      
Date     Review By          Comments                                    
                           
------   ------------       -------------------------*/                          
CREATE PROCEDURE dbo.Proc_CopyAppUmbrellaSchRecVehicles                
(          
@CUSTOMER_ID int,  
@APP_ID int,  
@APP_VERSION_ID smallint,  
@COMPANY_ID_NUMBER int,  
@YEAR int,  
@MAKE nvarchar(75),  
@MODEL nvarchar(75),  
@SERIAL  nvarchar(75),  
@STATE_REGISTERED nchar(2),  
@MANUFACTURER_DESC  nvarchar(100),  
@HORSE_POWER  nvarchar(10),  
@DISPLACEMENT  nvarchar(10),  
@REMARKS  nvarchar(500),  
@USED_IN_RACE_SPEED nchar(1),  
@PRIOR_LOSSES nchar(1),  
@IS_UNIT_REG_IN_OTHER_STATE nchar(1),  
@RISK_DECL_BY_OTHER_COMP nchar(1),  
@DESC_RISK_DECL_BY_OTHER_COMP nvarchar(500),  
@VEHICLE_MODIFIED nchar(1),  
@CREATED_BY int,  
@VEHICLE_TYPE int  
  
)                    
AS                         
BEGIN                          
      
declare @REC_VEH_ID smallint      
SELECT @REC_VEH_ID = ISNULL(MAX(REC_VEH_ID),0)+1,@COMPANY_ID_NUMBER = ISNULL(MAX(COMPANY_ID_NUMBER),0)+1 FROM APP_UMBRELLA_RECREATIONAL_VEHICLES WHERE      
 CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID = @APP_VERSION_ID      
      
  
insert into APP_UMBRELLA_RECREATIONAL_VEHICLES      
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
VEHICLE_TYPE, CREATED_DATETIME  
)      
values      
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
@VEHICLE_TYPE,  
GETDATE()  
)      
              
END                
                
                
              
            
          
        
        
        
        
        
      
    
  



GO

