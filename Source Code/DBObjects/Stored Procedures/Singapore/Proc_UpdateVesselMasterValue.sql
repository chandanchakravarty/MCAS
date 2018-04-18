  /*----------------------------------------------------------  
Proc Name          : Dbo.Proc_UpdateVesselMasterValue  
Created by           : Abhishek Goel 
Date                    : 14/03/2012
Purpose               :   Singapore Demo
Revison History :  
Used In                :   Singapore Demo    

------------------------------------------------------------*/  
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateVesselMasterValue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateVesselMasterValue]
GO
CREATE   PROCEDURE Proc_UpdateVesselMasterValue 
(  
@VESSEL_ID     int,    
@VESSEL_NAME    varchar(40),
@GRT varchar(10),
@IMO varchar(10),
@NRT varchar(10),
@FLAG varchar(20), 
@CLASSIFICATION varchar(10),
@YEAR_BUILT int,
@LINER int,
@TYPE_OF_VESSEL int,
@CLASS_TYPE int,
@CLASS varchar(5),
@IS_ACTIVE nchar(2)
--@DAY_PRIOR_TO_SAILING_DATE varchar(10),
--@TOTAL_SHIPMENT_DAY varchar(10)
)  
AS  
BEGIN  
UPDATE MNT_VESSEL_MASTER  
SET  VESSEL_NAME = @VESSEL_NAME,
GRT = @GRT,
IMO = @IMO,
NRT = @NRT,
FLAG = @FLAG,
CLASSIFICATION = @CLASSIFICATION,
YEAR_BUILT = @YEAR_BUILT,
LINER = @LINER,
TYPE_OF_VESSEL = @TYPE_OF_VESSEL,
CLASS_TYPE = @CLASS_TYPE,
CLASS = @CLASS,
IS_ACTIVE = @IS_ACTIVE
--DAY_PRIOR_TO_SAILING_DATE = @DAY_PRIOR_TO_SAILING_DATE,
--TOTAL_SHIPMENT_DAY = @TOTAL_SHIPMENT_DAY
WHERE VESSEL_ID =@VESSEL_ID   
END  
  