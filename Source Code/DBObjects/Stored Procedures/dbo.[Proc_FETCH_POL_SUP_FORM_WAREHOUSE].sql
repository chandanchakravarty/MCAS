    
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_FETCH_POL_SUP_FORM_WAREHOUSE]              
Created by      : SNEHA          
Date            : 23/11/2011                      
Purpose         :INSERT RECORDS IN POL_SUP_FORM_WAREHOUSE TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_FETCH_POL_SUP_FORM_WAREHOUSE]  8,1,1,1,1     
      
*/  

CREATE PROC dbo.[Proc_FETCH_POL_SUP_FORM_WAREHOUSE]
(
@CUSTOMER_ID				[INT],
@POLICY_ID					[INT],
@POLICY_VERSION_ID			[SMALLINT],
@LOCATION_ID				[SMALLINT],
@PREMISES_ID				[INT]				 
)
AS
	DECLARE @WAREHOUSE_ID  INT
BEGIN
    IF NOT EXISTS (SELECT WAREHOUSE_ID FROM POL_SUP_FORM_WAREHOUSE WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID	AND	LOCATION_ID	=@LOCATION_ID  AND PREMISES_ID=@PREMISES_ID)
		BEGIN
			SELECT -1 AS WAREHOUSE_ID
		END
	ELSE
	 BEGIN
		SELECT ISNULL(WAREHOUSE_ID,0) AS WAREHOUSE_ID FROM POL_SUP_FORM_WAREHOUSE 
			WHERE 
				CUSTOMER_ID				=@CUSTOMER_ID       AND
				POLICY_ID				=@POLICY_ID         AND
				POLICY_VERSION_ID		=@POLICY_VERSION_ID	AND
				LOCATION_ID				=@LOCATION_ID       AND
				PREMISES_ID				=@PREMISES_ID       
			
		SELECT @WAREHOUSE_ID AS WAREHOUSE_ID
	END
END