    
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_GET_POL_SUP_FORM_WAREHOUSE]              
Created by      : SNEHA          
Date            : 23/11/2011                      
Purpose         :INSERT RECORDS IN POL_SUP_FORM_WAREHOUSE TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_GET_POL_SUP_FORM_WAREHOUSE] 8,1,1,1,1,1
*/

CREATE PROC dbo.[Proc_GET_POL_SUP_FORM_WAREHOUSE]
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
	SELECT 
			BUILDINGS,OWN_MGMR,RES_MGMR,DAYTIME_ATTNDT,ANY_BUSS_ACTY,VLT_STYLE,TRUCK_RENTAL,MGMR_KYS_CUST_UNIT,NOTICE_SENT,SALES_TENANT_LST_TWELVE_MNTHS,ANY_COLD_STORAGE,MGMR_TYPE,STORAGEUNITS,IS_FENCED,IS_PRKNG_AVL,IS_BOAT_PRKNG_AVL,ANY_FIREARMS,TENANT_LCKS_CHK,ANY_BUSN_GUIDELINES,NO_DYS_TENANT_PROP_SOLD,DISP_PUBL,ANY_CLIMATE_CNTL,GUARD_DOG 
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