IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETXMLPOL_OTHER_STRUCTURE_DWELLING]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETXMLPOL_OTHER_STRUCTURE_DWELLING]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

      
--drop proc dbo.PROC_GETXMLPOL_OTHER_STRUCTURE_DWELLING         
CREATE proc dbo.PROC_GETXMLPOL_OTHER_STRUCTURE_DWELLING         
(            
@CUSTOMER_ID     INT,            
@POL_ID     INT,            
@POL_VERSION_ID     SMALLINT,            
@DWELLING_ID     SMALLINT,            
@OTHER_STRUCTURE_ID     SMALLINT            
)            
AS            
BEGIN            
SELECT CUSTOMER_ID,            
POLICY_ID,            
POLICY_VERSION_ID,            
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
CASE WHEN INSURING_VALUE = 0 THEN  null  else INSURING_VALUE END AS INSURING_VALUE,       
INSURING_VALUE_OFF_PREMISES,            
MODIFIED_BY,            
LAST_UPDATED_DATETIME,            
IS_ACTIVE,          
COVERAGE_AMOUNT,        
LIABILITY_EXTENDED,  
SOLID_FUEL_DEVICE, --Added by Charles on 27-Nov-09 for Itrack 6681        
APPLY_ENDS --Added by Charles on 3-Dec-09 for Itrack 6405
          
FROM  POL_OTHER_STRUCTURE_DWELLING            
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND DWELLING_ID=@DWELLING_ID AND OTHER_STRUCTURE_ID=@OTHER_STRUCTURE_ID             
END            
            
      
            
            
            
          
        
        
GO

