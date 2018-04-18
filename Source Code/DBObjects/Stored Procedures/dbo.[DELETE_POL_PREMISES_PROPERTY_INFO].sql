/*---------------------------------------------------------------  
Proc Name          : dbo.[DELETE_POL_PREMISES_PROPERTY_INFO] 
Created by      : SNEHA          
Date            : 16/11/2011                      
--------------------------------------------------------  
--Date     Review By          Comments          
------   ------------       -------------------------*/         
-- drop proc dbo.[DELETE_POL_PREMISES_PROPERTY_INFO]  
------   ------------       -------------------------*/         
-- drop proc dbo.[DELETE_POL_PREMISES_PROPERTY_INFO]  

CREATE PROC  dbo.[DELETE_POL_PREMISES_PROPERTY_INFO] 
(
@CUSTOMER_ID				[int],
@POLICY_ID					[int],
@POLICY_VERSION_ID			[smallint],
@LOCATION_ID				[smallint],
@PREMISES_ID				[int],
@PROPERTY_ID				[int]
)

AS
BEGIN
	DELETE 
			FROM 
					POL_PREMISES_PROPERTY_INFO 
	WHERE 
			CUSTOMER_ID					=@CUSTOMER_ID AND
			POLICY_ID					=@POLICY_ID	AND
			POLICY_VERSION_ID			=@POLICY_VERSION_ID AND
			LOCATION_ID					=@LOCATION_ID AND
			PREMISES_ID					=@PREMISES_ID	AND
			PROPERTY_ID					=@PROPERTY_ID				
END