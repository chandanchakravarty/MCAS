/*---------------------------------------------------------------  
Proc Name          : dbo.[DELETE_POL_SUP_FORM_WAREHOUSE] 
Created by      : SNEHA          
Date            : 23/11/2011                      
--------------------------------------------------------  
--Date     Review By          Comments          
------   ------------       -------------------------*/         
-- drop proc dbo.[DELETE_POL_SUP_FORM_WAREHOUSE]  
------   ------------       -------------------------*/         
-- drop proc dbo.[DELETE_POL_SUP_FORM_WAREHOUSE]  

CREATE PROC dbo.[DELETE_POL_SUP_FORM_WAREHOUSE]  
(
@CUSTOMER_ID				[INT],
@POLICY_ID					[INT],
@POLICY_VERSION_ID			[SMALLINT],
@LOCATION_ID				[SMALLINT],
@PREMISES_ID				[INT],
@WAREHOUSE_ID				[INT] 
)
AS
BEGIN 
	DELETE 
			FROM 
					POL_SUP_FORM_WAREHOUSE 
	WHERE 
			CUSTOMER_ID				=@CUSTOMER_ID       AND
			POLICY_ID				=@POLICY_ID         AND
			POLICY_VERSION_ID		=@POLICY_VERSION_ID	AND
			LOCATION_ID				=@LOCATION_ID       AND
			PREMISES_ID				=@PREMISES_ID       AND
			WAREHOUSE_ID			=@WAREHOUSE_ID      
END