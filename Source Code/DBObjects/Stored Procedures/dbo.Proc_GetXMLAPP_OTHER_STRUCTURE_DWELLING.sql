IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLAPP_OTHER_STRUCTURE_DWELLING]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLAPP_OTHER_STRUCTURE_DWELLING]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc dbo.Proc_GetXMLAPP_OTHER_STRUCTURE_DWELLING  
   
CREATE PROC dbo.Proc_GetXMLAPP_OTHER_STRUCTURE_DWELLING        
(        
@CUSTOMER_ID     int,        
@APP_ID     int,        
@APP_VERSION_ID     smallint,        
@DWELLING_ID     smallint,        
@OTHER_STRUCTURE_ID     smallint        
)        
AS        
BEGIN        
select CUSTOMER_ID,        
APP_ID,        
APP_VERSION_ID,        
DWELLING_ID,        
OTHER_STRUCTURE_ID,        
PREMISES_LOCATION,        
PREMISES_DESCRIPTION,        
PREMISES_USE,        
PREMISES_CONDITION,        
PICTURE_ATTACHED,        
COVERAGE_BASIS,        
SATELLITE_EQUIPMENT,        
LOCATION_ADDRESS,        
LOCATION_CITY,        
LOCATION_STATE,        
LOCATION_ZIP,        
ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED,        
INSURING_VALUE,        
INSURING_VALUE_OFF_PREMISES,        
MODIFIED_BY,        
LAST_UPDATED_DATETIME,        
IS_ACTIVE,      
COVERAGE_AMOUNT,    
LIABILITY_EXTENDED,  
SOLID_FUEL_DEVICE, --Added by Charles on 27-Nov-09 for Itrack 6681        
APPLY_ENDS --Added by Charles on 3-Dec-09 for Itrack 6405
from  APP_OTHER_STRUCTURE_DWELLING        
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND DWELLING_ID=@DWELLING_ID AND OTHER_STRUCTURE_ID=@OTHER_STRUCTURE_ID         
END        
        
        
        
      
    
GO

