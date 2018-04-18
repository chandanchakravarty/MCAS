/*---------------------------------------------------------------  
Proc Name          : dbo.[DELETE_POL_SUP_FORM_CONTRACTOR] 
Created by      : SNEHA          
Date            : 24/11/2011                      
--------------------------------------------------------  
--Date     Review By          Comments          
------   ------------       -------------------------*/         
-- drop proc dbo.[DELETE_POL_SUP_FORM_CONTRACTOR]  
------   ------------       -------------------------*/         
-- drop proc dbo.[DELETE_POL_SUP_FORM_CONTRACTOR]

CREATE PROC dbo.[DELETE_POL_SUP_FORM_CONTRACTOR]
(
@CUSTOMER_ID				[INT],
@POLICY_ID					[INT],
@POLICY_VERSION_ID			[SMALLINT],
@LOCATION_ID				[SMALLINT],
@PREMISES_ID				[INT],
@CONTRACTOR_ID				[INT] 
)
AS
BEGIN
DELETE 
			FROM 
					POL_SUP_FORM_CONTRACTOR 
	WHERE 
			CUSTOMER_ID				=@CUSTOMER_ID       AND
			POLICY_ID				=@POLICY_ID         AND
			POLICY_VERSION_ID		=@POLICY_VERSION_ID	AND
			LOCATION_ID				=@LOCATION_ID       AND
			PREMISES_ID				=@PREMISES_ID       AND
			CONTRACTOR_ID			=@CONTRACTOR_ID      
END  