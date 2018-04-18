    
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_FETCH_POL_SUP_FORM_CONTRACTOR]              
Created by      : SNEHA          
Date            : 23/11/2011                      
Purpose         :INSERT RECORDS IN POL_SUP_FORM_CONTRACTOR TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_FETCH_POL_SUP_FORM_CONTRACTOR]  28,1,1,1,1     
*/

CREATE PROC dbo.[Proc_FETCH_POL_SUP_FORM_CONTRACTOR]
(
@CUSTOMER_ID				[INT],
@POLICY_ID					[INT],
@POLICY_VERSION_ID			[SMALLINT],
@LOCATION_ID				[SMALLINT],
@PREMISES_ID				[INT]
)

AS 
DECLARE @CONTRACTOR_ID INT

BEGIN
     IF NOT EXISTS(SELECT CONTRACTOR_ID FROM POL_SUP_FORM_CONTRACTOR WHERE CUSTOMER_ID	=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LOCATION_ID=@LOCATION_ID AND  PREMISES_ID=@PREMISES_ID)      
     BEGIN
			SELECT -1 AS CONTRACTOR_ID
		END
	ELSE
	 BEGIN
		SELECT ISNULL(CONTRACTOR_ID,0) AS CONTRACTOR_ID FROM POL_SUP_FORM_CONTRACTOR 
			WHERE 
				CUSTOMER_ID				=@CUSTOMER_ID       AND
				POLICY_ID				=@POLICY_ID         AND
				POLICY_VERSION_ID		=@POLICY_VERSION_ID	AND
				LOCATION_ID				=@LOCATION_ID       AND
				PREMISES_ID				=@PREMISES_ID       
			
		SELECT @CONTRACTOR_ID AS CONTRACTOR_ID
	END
END