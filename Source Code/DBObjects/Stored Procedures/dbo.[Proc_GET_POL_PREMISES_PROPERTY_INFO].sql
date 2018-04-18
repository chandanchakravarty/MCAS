    
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_GET_POL_PREMISES_PROPERTY_INFO]              
Created by      : SNEHA          
Date            : 16/11/2011                      
Purpose         :INSERT RECORDS IN POL_PREMISES_PROPERTY_INFO TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_GET_POL_PREMISES_PROPERTY_INFO] 8,1,1,1,1,1
*/

CREATE PROC dbo.[Proc_GET_POL_PREMISES_PROPERTY_INFO] 
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
	SELECT  
			PROPERTY_ID,PROP_DEDUCT,PROP_WNDSTORM,OPT_CVG,BLD_LMT,BLD_PERCENT_COINS,BLD_VALU,BLD_INF,BPP_LMT,BPP_PERCENT_COINS,BPP_VALU,BPP_STOCK,YEAR_BUILT,CONST_TYPE,NUM_STORIES,PERCENT_SPRINKLERS,BP_PRESENT,BP_FNSHD,BP_OPEN,BI_WIRNG_YR,BI_ROOFING_YR,BI_PLMG_YR,BI_ROOF_TYP,BI_HEATNG_YR,BI_WIND_CLASS
	FROM
			POL_PREMISES_PROPERTY_INFO
	WHERE
			PROPERTY_ID					=@PROPERTY_ID AND
			CUSTOMER_ID					=@CUSTOMER_ID AND
			POLICY_ID					=@POLICY_ID	AND
			POLICY_VERSION_ID			=@POLICY_VERSION_ID AND
			LOCATION_ID					=@LOCATION_ID AND
			PREMISES_ID					=@PREMISES_ID	
					
END	