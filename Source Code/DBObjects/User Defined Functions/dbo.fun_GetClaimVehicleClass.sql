IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_GetClaimVehicleClass]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_GetClaimVehicleClass]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                        
Function Name : dbo.fun_GetClaimVehicleClass  
CREATED BY      : Asfa Praveen          
DATE            : 30 june 2008      
PURPOSE         : TO GET THE Class of Vehicle(s) added at Claim  
REVISON HISTORY :                                        
USED IN         : WOLVERINE   
------------------------------------------------------------                                        
*/      
--select dbo.fun_GetClaimVehicleClass(975)                           
-- drop function dbo.fun_GetClaimVehicleClass              
CREATE function dbo.fun_GetClaimVehicleClass    
                               
(                                 
 @CLAIM_ID INT  
)         RETURNS VARCHAR(8000)      
      
AS              
BEGIN        
DECLARE @VEHICLE_CLASS VARCHAR(8000)     
DECLARE @RETURN_STRING VARCHAR(8000)  
  
SET @VEHICLE_CLASS = ''  
SET @RETURN_STRING = ''  
  
DECLARE CUR CURSOR        
FOR   
SELECT   
 CASE WHEN APP_USE_VEHICLE_ID='11332' THEN MLV3.LOOKUP_VALUE_DESC   
      --Added by Manoj Rathore on 28th May 2009 Itrack # 5893  
      WHEN LOB_ID=3 THEN MLV4.LOOKUP_VALUE_DESC  
 ELSE MLV2.LOOKUP_VALUE_DESC END VEHICLE_CLASS  
FROM CLM_CLAIM_INFO CCI  
LEFT JOIN CLM_INSURED_VEHICLE CIV ON CIV.CLAIM_ID= CCI.CLAIM_ID  AND CIV.IS_ACTIVE='Y'
LEFT JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID= CCI.CUSTOMER_ID AND PV.POLICY_ID= CCI.POLICY_ID   
AND PV.POLICY_VERSION_ID= CCI.POLICY_VERSION_ID AND CIV.POLICY_VEHICLE_ID = PV.VEHICLE_ID  
LEFT JOIN MNT_LOOKUP_VALUES MLV2 ON PV.APP_VEHICLE_COMCLASS_ID  = MLV2.LOOKUP_UNIQUE_ID   
LEFT JOIN MNT_LOOKUP_VALUES MLV3 ON PV.APP_VEHICLE_PERCLASS_ID  = MLV3.LOOKUP_UNIQUE_ID  
LEFT JOIN MNT_LOOKUP_VALUES MLV4 ON PV.APP_VEHICLE_CLASS  = MLV4.LOOKUP_UNIQUE_ID  
WHERE CCI.CLAIM_ID = @CLAIM_ID  
  
OPEN CUR        
FETCH NEXT FROM CUR INTO @VEHICLE_CLASS  
WHILE @@FETCH_STATUS = 0        
  BEGIN    
 SET @RETURN_STRING = @RETURN_STRING + @VEHICLE_CLASS + ';<br>'  
  
 FETCH NEXT FROM CUR INTO @VEHICLE_CLASS  
  END        
CLOSE CUR        
DEALLOCATE CUR      
  
SET @RETURN_STRING = SUBSTRING(@RETURN_STRING,0, LEN(@RETURN_STRING)-4)  
RETURN @RETURN_STRING  
  
END
GO

