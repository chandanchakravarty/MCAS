    
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_FETCH_POL_PREMISES_PROPERTY_INFO]              
Created by      : SNEHA          
Date            : 17/11/2011                      
Purpose         :INSERT RECORDS IN POL_PREMISES_PROPERTY_INFO TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_FETCH_POL_PREMISES_PROPERTY_INFO]  8,1,1,1,1     
      
*/  
CREATE PROC dbo.[Proc_FETCH_POL_PREMISES_PROPERTY_INFO]
(
@CUSTOMER_ID				[int],
@POLICY_ID					[int],
@POLICY_VERSION_ID			[smallint],
@LOCATION_ID				[smallint],
@PREMISES_ID				[int]

)
AS
DECLARE @PROPERTY_ID				[int]
BEGIN
IF NOT EXISTS (SELECT PROPERTY_ID FROM POL_PREMISES_PROPERTY_INFO WHERE CUSTOMER_ID	=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND	LOCATION_ID=@LOCATION_ID AND PREMISES_ID=@PREMISES_ID)
	BEGIN
		SELECT -1 AS PROPERTY_ID
	END
ELSE
    BEGIN
		SELECT ISNULL(PROPERTY_ID,0) AS PROPERTY_ID FROM POL_PREMISES_PROPERTY_INFO 
		WHERE 
			CUSTOMER_ID	=@CUSTOMER_ID				AND 
			POLICY_ID=@POLICY_ID					AND 
			POLICY_VERSION_ID=@POLICY_VERSION_ID	AND	
			PREMISES_ID=@PREMISES_ID				AND
			LOCATION_ID=@LOCATION_ID				 
		SELECT @PROPERTY_ID AS PROPERTY_ID
	END
END			