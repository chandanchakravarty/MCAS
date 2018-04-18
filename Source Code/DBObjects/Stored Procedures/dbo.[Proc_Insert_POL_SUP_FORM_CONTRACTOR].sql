  
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_Insert_POL_SUP_FORM_CONTRACTOR]              
Created by      : SNEHA          
Date            : 24/11/2011                      
Purpose         :INSERT RECORDS IN POL_SUP_FORM_CONTRACTOR TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_Insert_POL_SUP_FORM_CONTRACTOR]        
      
*/  

CREATE PROC dbo.[Proc_Insert_POL_SUP_FORM_CONTRACTOR]
(
@CUSTOMER_ID					int,
@POLICY_ID						int,
@POLICY_VERSION_ID				smallint,
@LOCATION_ID					smallint,
@PREMISES_ID					int,
@CONTRACTOR_ID					int OUT,
@TYP_CONTRACTOR					nvarchar (100),
@YR_EXP							nvarchar (100),
@CONT_LICENSE					nvarchar (100),
@LICENSE_HOLDER					nvarchar(10),
@LMT_CONTRACTOR_OCC				nvarchar(10),
@LMT_CONTRACTOR_AGG				nvarchar(10),
@TOT_CST_PST_YR					decimal(18,9),
@IS_EXPL_ENVRNT					nvarchar (10),
@IS_FIRE_ALARM					nvarchar (10),
@IS_HOSPITALS					nvarchar (10),
@IS_EXP_ENVRNT					nvarchar (10),
@IS_SWIMMING_POOL				nvarchar (10),
@IS_BRG_ALARM					nvarchar (10),
@IS_PWR_PLANTS					nvarchar (10),
@IS_BCK_EQUIPMNT				nvarchar (10),
@IS_LIVE_WIRES					nvarchar (10),
@IS_ARPT_CONSTRCT				nvarchar (10),
@IS_HIGH_VOLTAGE				nvarchar (10),
@IS_TRAFFIC_WRK					nvarchar (10),
@IS_LND_FILL					nvarchar (10),
@IS_DAM_CONSTRNT				nvarchar (10),
@IS_REFINERY					nvarchar (10),
@IS_HZD_MATERIAL				nvarchar (10),
@IS_PETRO_PLNT					nvarchar (10),
@IS_NUCL_PLNT					nvarchar (10),
@IS_PWR_LINES					nvarchar (10),
@DRW_PLANS						nvarchar (10),
@OPR_BLASTING					nvarchar (10),
@OPR_TRENCHING					nvarchar (10),
@OPR_EXCAVACATION				nvarchar (10),
@IS_SECTY_POLICY				nvarchar (10),
@ANY_DEMOLITION					nvarchar (10),
@ANY_CRANES						nvarchar (10),
@PERCENT_ROOFING				nvarchar (150),
@ANY_LESS_LMTS					nvarchar (10),
@ANY_SHOP_WRK					nvarchar (10),
@PERCENT_RENOVATION				nvarchar (150),
@ANY_GUTTING					nvarchar (10),
@PERCENT_SNOWPLOWING			nvarchar (150),
@ANY_WRK_LOAD					nvarchar (10),
@PERCENT_PNTG_OUTSIDE			nvarchar (150),
@PERCENT_PNTG_INSIDE			nvarchar (150),
@ANY_PNTG_TNKS					nvarchar (10),
@PERCENT_PNTG_SPRY				nvarchar (150),
@ANY_EPOXIES					nvarchar (10),
@ANY_ACID						nvarchar (10),
@ANY_LEASE_EQUIPMNT				nvarchar (10),
@ANY_BOATS_OWND					nvarchar (10),
@DRCT_SIGHT_WRK_IN_PRGRSS		nvarchar (10),
@PRDCT_SOLD_IN_APPL_NAME		nvarchar (10),
@UTILITY_CMPNY_CALLED			nvarchar (10),
@TYP_IN_DGGN_PRCSS				nvarchar (150),
@PERMIT_OBTAINED				nvarchar (10),
@PERCENT_SPRINKLE_WRK			nvarchar (150),
@ANY_EXCAVAION					nvarchar (10),
@ANY_PEST_SPRAY					nvarchar (10),
@PERCENT_TREE_TRIMNG			nvarchar (150),
@ANY_WRK_OFFSEASON				nvarchar (10),
@ANY_MIX_TRANSIT				nvarchar (10),
@ANY_CONTSRUCTION_WRK			nvarchar (10),
@ANY_WRK_ABVE_THREE_STORIES		nvarchar (10),
@ANY_SCAFHOLDING_ABVE_TWELVE_FEET	nvarchar (10),
@ANY_PNTG_TOWERS				nvarchar (10),
@ANY_SPRAY_GUNS					nvarchar (10),
@ANY_REMOVAL_DONE				nvarchar (10),
@ANY_WAXING_FLOORS				nvarchar (10),
@PER_RESIDENT nvarchar (10),
@PER_COMMERICAL nvarchar (10),
@PER_CONST nvarchar (10),
@PER_REMODEL nvarchar (10),
@MAJOR_ELECT nvarchar (10),
@CARRY_LIMITS nvarchar (10)
)

AS
BEGIN
SELECT @CONTRACTOR_ID =ISNULL(MAX(CONTRACTOR_ID),0)+1 FROM POL_SUP_FORM_CONTRACTOR

INSERT INTO POL_SUP_FORM_CONTRACTOR
(
CUSTOMER_ID,
POLICY_ID,
POLICY_VERSION_ID,
LOCATION_ID,
PREMISES_ID,
CONTRACTOR_ID,
TYP_CONTRACTOR,
YR_EXP,
CONT_LICENSE,
LICENSE_HOLDER,
LMT_CONTRACTOR_OCC,
LMT_CONTRACTOR_AGG,
TOT_CST_PST_YR,
IS_EXPL_ENVRNT,
IS_FIRE_ALARM,
IS_HOSPITALS,
IS_EXP_ENVRNT,
IS_SWIMMING_POOL,
IS_BRG_ALARM,
IS_PWR_PLANTS,
IS_BCK_EQUIPMNT,
IS_LIVE_WIRES,
IS_ARPT_CONSTRCT,
IS_HIGH_VOLTAGE,
IS_TRAFFIC_WRK,
IS_LND_FILL,
IS_DAM_CONSTRNT,
IS_REFINERY,
IS_HZD_MATERIAL,
IS_PETRO_PLNT,
IS_NUCL_PLNT,
IS_PWR_LINES,
DRW_PLANS,
OPR_BLASTING,
OPR_TRENCHING,
OPR_EXCAVACATION,
IS_SECTY_POLICY,
ANY_DEMOLITION,
ANY_CRANES,
PERCENT_ROOFING,
ANY_LESS_LMTS,
ANY_SHOP_WRK,
PERCENT_RENOVATION,
ANY_GUTTING,
PERCENT_SNOWPLOWING,
ANY_WRK_LOAD,
PERCENT_PNTG_OUTSIDE,
PERCENT_PNTG_INSIDE,
ANY_PNTG_TNKS,
PERCENT_PNTG_SPRY,
ANY_EPOXIES,
ANY_ACID,
ANY_LEASE_EQUIPMNT,
ANY_BOATS_OWND,
DRCT_SIGHT_WRK_IN_PRGRSS,
PRDCT_SOLD_IN_APPL_NAME,
UTILITY_CMPNY_CALLED,
TYP_IN_DGGN_PRCSS,
PERMIT_OBTAINED,
PERCENT_SPRINKLE_WRK,
ANY_EXCAVAION,
ANY_PEST_SPRAY,
PERCENT_TREE_TRIMNG,
ANY_WRK_OFFSEASON,
ANY_MIX_TRANSIT,
ANY_CONTSRUCTION_WRK,
ANY_WRK_ABVE_THREE_STORIES,
ANY_SCAFHOLDING_ABVE_TWELVE_FEET,
ANY_PNTG_TOWERS,
ANY_SPRAY_GUNS,
ANY_REMOVAL_DONE,
ANY_WAXING_FLOORS,
PER_RESIDENT,
PER_COMMERICAL,
PER_CONST,
PER_REMODEL,
MAJOR_ELECT,
CARRY_LIMITS

)

VALUES
(
@CUSTOMER_ID					,
@POLICY_ID						,
@POLICY_VERSION_ID				,
@LOCATION_ID					,
@PREMISES_ID					,
@CONTRACTOR_ID					,
@TYP_CONTRACTOR					,
@YR_EXP							,
@CONT_LICENSE					,
@LICENSE_HOLDER					,
@LMT_CONTRACTOR_OCC				,
@LMT_CONTRACTOR_AGG				,
@TOT_CST_PST_YR					,
@IS_EXPL_ENVRNT					,
@IS_FIRE_ALARM					,
@IS_HOSPITALS					,
@IS_EXP_ENVRNT					,
@IS_SWIMMING_POOL				,
@IS_BRG_ALARM					,
@IS_PWR_PLANTS					,
@IS_BCK_EQUIPMNT				,
@IS_LIVE_WIRES					,
@IS_ARPT_CONSTRCT				,
@IS_HIGH_VOLTAGE				,
@IS_TRAFFIC_WRK					,
@IS_LND_FILL					,
@IS_DAM_CONSTRNT				,
@IS_REFINERY					,
@IS_HZD_MATERIAL				,
@IS_PETRO_PLNT					,
@IS_NUCL_PLNT					,
@IS_PWR_LINES					,
@DRW_PLANS						,
@OPR_BLASTING					,
@OPR_TRENCHING					,
@OPR_EXCAVACATION				,
@IS_SECTY_POLICY				,
@ANY_DEMOLITION					,
@ANY_CRANES						,
@PERCENT_ROOFING				,
@ANY_LESS_LMTS					,
@ANY_SHOP_WRK					,
@PERCENT_RENOVATION				,
@ANY_GUTTING					,
@PERCENT_SNOWPLOWING			,
@ANY_WRK_LOAD					,
@PERCENT_PNTG_OUTSIDE			,
@PERCENT_PNTG_INSIDE			,
@ANY_PNTG_TNKS					,
@PERCENT_PNTG_SPRY				,
@ANY_EPOXIES					,
@ANY_ACID						,
@ANY_LEASE_EQUIPMNT				,
@ANY_BOATS_OWND					,
@DRCT_SIGHT_WRK_IN_PRGRSS		,
@PRDCT_SOLD_IN_APPL_NAME		,
@UTILITY_CMPNY_CALLED			,
@TYP_IN_DGGN_PRCSS				,
@PERMIT_OBTAINED				,
@PERCENT_SPRINKLE_WRK			,
@ANY_EXCAVAION					,
@ANY_PEST_SPRAY					,
@PERCENT_TREE_TRIMNG			,
@ANY_WRK_OFFSEASON				,
@ANY_MIX_TRANSIT				,
@ANY_CONTSRUCTION_WRK			,
@ANY_WRK_ABVE_THREE_STORIES		,
@ANY_SCAFHOLDING_ABVE_TWELVE_FEET	,
@ANY_PNTG_TOWERS				,
@ANY_SPRAY_GUNS					,
@ANY_REMOVAL_DONE				,
@ANY_WAXING_FLOORS				,
@PER_RESIDENT,
@PER_COMMERICAL,
@PER_CONST,
@PER_REMODEL,
@MAJOR_ELECT,
@CARRY_LIMITS
)
END