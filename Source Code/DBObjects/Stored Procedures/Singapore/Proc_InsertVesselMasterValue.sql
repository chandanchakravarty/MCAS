  
/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_InsertVesselMasterValue    
Created by           : Abhishek Goel   
Date                    : 14/03/2012  
Purpose               :     
Revison History :    
Used In                :   Singapore Demo      
------------------------------------------------------------    
*/    
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertVesselMasterValue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertVesselMasterValue]
GO
CREATE   PROCEDURE [dbo].[Proc_InsertVesselMasterValue]    
(    
@VESSEL_ID     int output ,    
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
    
SELECT @VESSEL_ID = IsNull(Max(VESSEL_ID),0) + 1     
FROM MNT_VESSEL_MASTER    
    
    
INSERT INTO MNT_VESSEL_MASTER    
(    
VESSEL_ID,VESSEL_NAME,GRT,IMO,NRT,FLAG,CLASSIFICATION,YEAR_BUILT,LINER,TYPE_OF_VESSEL,CLASS_TYPE,CLASS,IS_ACTIVE   
)    
VALUES    
(    
@VESSEL_ID,@VESSEL_NAME,@GRT,@IMO,@NRT, @FLAG,@CLASSIFICATION,@YEAR_BUILT,@LINER,@TYPE_OF_VESSEL,@CLASS_TYPE,@CLASS,@IS_ACTIVE   
)    
  

  
END    
    