   
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_Insert_POL_PREMISES_PROPERTY_INFO]              
Created by      : SNEHA          
Date            : 17/11/2011                      
Purpose         :INSERT RECORDS IN POL_PREMISES_PROPERTY_INFO TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_Insert_POL_PREMISES_PROPERTY_INFO]        
      
*/  

CREATE PROC  [dbo].[Proc_Insert_POL_PREMISES_PROPERTY_INFO]        
(
@CUSTOMER_ID				[int],
@POLICY_ID					[int],
@POLICY_VERSION_ID			[smallint],
@LOCATION_ID				[smallint],
@PREMISES_ID				[int],
@PROPERTY_ID				[int] OUT,
@PROP_DEDUCT				[nvarchar](10),
@PROP_WNDSTORM				[nvarchar](150),
@OPT_CVG					[nvarchar](10),
@BLD_LMT					[nvarchar](150),
@BLD_PERCENT_COINS			[nvarchar](10),
@BLD_VALU					[nvarchar](10),
@BLD_INF					[nvarchar](10),
@BPP_LMT					[nvarchar](150),
@BPP_PERCENT_COINS			[nvarchar](10),
@BPP_VALU					[nvarchar](10),
@BPP_STOCK					[nvarchar](150),
@YEAR_BUILT					[nvarchar](150),
@CONST_TYPE					[nvarchar](10),
@NUM_STORIES				[nvarchar](10),
@PERCENT_SPRINKLERS			[nvarchar](150),
@BP_PRESENT					[nvarchar](10),
@BP_FNSHD					[nvarchar](10),
@BP_OPEN					[nvarchar](10),
@BI_WIRNG_YR				[nvarchar](150),
@BI_ROOFING_YR				[nvarchar](150),
@BI_PLMG_YR					[nvarchar](150),
@BI_ROOF_TYP				[nvarchar](10),
@BI_HEATNG_YR				[nvarchar](150),
@BI_WIND_CLASS				[nvarchar](10)
)
AS
BEGIN

SELECT @PROPERTY_ID =ISNULL(MAX(PROPERTY_ID),0)+1 FROM POL_PREMISES_PROPERTY_INFO

INSERT INTO POL_PREMISES_PROPERTY_INFO
(
CUSTOMER_ID,
POLICY_ID,
POLICY_VERSION_ID,
LOCATION_ID,
PREMISES_ID,
PROPERTY_ID,
PROP_DEDUCT,
PROP_WNDSTORM,
OPT_CVG,
BLD_LMT,
BLD_PERCENT_COINS,
BLD_VALU,
BLD_INF,
BPP_LMT,
BPP_PERCENT_COINS,
BPP_VALU,
BPP_STOCK,
YEAR_BUILT,
CONST_TYPE,
NUM_STORIES,
PERCENT_SPRINKLERS,
BP_PRESENT,
BP_FNSHD,
BP_OPEN,
BI_WIRNG_YR,
BI_ROOFING_YR,
BI_PLMG_YR,
BI_ROOF_TYP,
BI_HEATNG_YR,
BI_WIND_CLASS
)
VALUES
(
@CUSTOMER_ID,
@POLICY_ID,
@POLICY_VERSION_ID,
@LOCATION_ID,
@PREMISES_ID,
@PROPERTY_ID,
@PROP_DEDUCT,
@PROP_WNDSTORM,
@OPT_CVG,
@BLD_LMT,
@BLD_PERCENT_COINS,
@BLD_VALU,
@BLD_INF,
@BPP_LMT,
@BPP_PERCENT_COINS,
@BPP_VALU,
@BPP_STOCK,
@YEAR_BUILT,
@CONST_TYPE,
@NUM_STORIES,
@PERCENT_SPRINKLERS,
@BP_PRESENT,
@BP_FNSHD,
@BP_OPEN,
@BI_WIRNG_YR,
@BI_ROOFING_YR,
@BI_PLMG_YR,
@BI_ROOF_TYP,
@BI_HEATNG_YR,
@BI_WIND_CLASS
)
END