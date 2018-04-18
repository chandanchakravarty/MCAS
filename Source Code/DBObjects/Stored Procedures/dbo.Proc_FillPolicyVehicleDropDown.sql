IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillPolicyVehicleDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillPolicyVehicleDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*            
----------------------------------------------------------                
Proc Name        : dbo.Proc_FillPolicyVehicleDropDown            
Created by       : Vijay Arora  
Date             : 07-11-2005  
Purpose   : Selects Policy Related Vehicles.  
Revison History  :                
Used In          : Wolverine                
------------------------------------------------------------                
Date     	Modified By          Comments              

09-03-2006  	Swastika Gaur	   IS_ACTIVE conditon added.
------   ------------       -------------------------               
*/            
--drop proc Proc_FillPolicyVehicleDropDown
  CREATE PROCEDURE [dbo].[Proc_FillPolicyVehicleDropDown]     
(    
 @CUSTOMER_ID int,     
 @POLICY_ID int,     
 @POLICY_VERSION_ID int    
)    
AS    
begin    
    
SELECT VEHICLE_ID,convert(varchar,VEHICLE_ID) + ' '  + isNull(VEHICLE_YEAR,'')  + ' ' + IsNull(MAKE,'') As MODEL_MAKE FROM POL_VEHICLES     
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND UPPER(IS_ACTIVE)='Y'   
ORDER BY 2    
    
End    
    
  





GO

