IF  EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('INSERT_POL_SUP_FORM_RESTAURANT') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[INSERT_POL_SUP_FORM_RESTAURANT]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO 

---------------------------------------------------------------
--Proc Name          : dbo.POL_SUP_FORM_OLD_BLD
--Created by         : Rajeev        
--Date               :  11 NOVEMBER 2011         
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/      
CREATE  PROCEDURE [dbo].[INSERT_POL_SUP_FORM_RESTAURANT]      
( 
			@RESTAURANT_ID int output		
		   ,@CUSTOMER_ID int
		   ,@POLICY_ID int
		   ,@POLICY_VERSION_ID smallint
		   ,@LOCATION_ID smallint
		   ,@PREMISES_ID int
		   ,@SEATINGCAPACITY int
           ,@BUS_TYP_RESTURANT bit
           ,@BUS_TYP_FM_STYLE bit
           ,@BUS_TYP_NGHT_CLUB bit
           ,@BUS_TYP_FRNCHSED bit
           ,@BUS_TYP_NT_FRNCHSED bit
           ,@BUS_TYP_SEASONAL bit
           ,@BUS_TYP_YR_ROUND bit
           ,@BUS_TYP_DINNER bit
           ,@BUS_TYP_BNQT_HALL bit
           ,@BUS_TYP_BREKFAST bit
           ,@BUS_TYP_FST_FOOD bit
           ,@BUS_TYP_TAVERN bit
           ,@BUS_TYP_OTHER bit
           ,@STAIRWAYS bit
           ,@ELEVATORS bit
           ,@ESCALATORS bit
           ,@GRILLING bit
           ,@FRYING bit
           ,@BROILING bit
           ,@ROASTING bit
           ,@COOKING bit
           ,@PRK_TYP_VALET bit
           ,@PRK_TYP_PREMISES bit
           ,@OPR_ON_PREMISES bit
           ,@OPR_OFF_PREMISES bit
           ,@EMRG_LIGHTS bit
           ,@WOOD_STOVE bit
           ,@HIST_MARKER bit
           ,@EXTNG_SYS_COV_COOKNG bit
           ,@EXTNG_SYS_MNT_CNTRCT bit
           ,@GAS_OFF_COOKNG bit
           ,@HOOD_FILTER_CLND bit
           ,@HOOD_DUCTS_EQUIP bit
           ,@HOOD_DUCTS_MNT_SCH bit
           ,@BC_EXTNG_AVL bit
           ,@ADQT_CLEARANCE bit
           ,@BEER_SALES bit
           ,@WINE_SALES bit
           ,@FULL_BAR bit
           ,@TOT_EXPNS_FOOD_LIQUOR decimal(18,2)
           ,@TOT_EXPNS_OTHERS decimal(18,2)
           ,@NET_PROFIT decimal(18,2)
           ,@ACCNT_PAYABLE decimal(18,2)
           ,@NOTES_PAYABLE decimal(18,2)
           ,@BNK_LOANS_PAYABLE decimal(18,2)
		   

)
AS       
BEGIN
	--SELECT @RESTAURANT_ID = isnull(Max(RESTAURANT_ID),0)+ 1 FROM POL_SUP_FORM_RESTAURANT
	SET @RESTAURANT_ID=(SELECT isnull(Max(RESTAURANT_ID),0)+ 1  FROM POL_SUP_FORM_RESTAURANT)
	INSERT INTO [dbo].[POL_SUP_FORM_RESTAURANT]
           ([CUSTOMER_ID]
           ,[POLICY_ID]
           ,[POLICY_VERSION_ID]
           ,[LOCATION_ID]
           ,[PREMISES_ID]
           ,[RESTAURANT_ID]
           ,[SEATINGCAPACITY]
           ,[BUS_TYP_RESTURANT]
           ,[BUS_TYP_FM_STYLE]
           ,[BUS_TYP_NGHT_CLUB]
           ,[BUS_TYP_FRNCHSED]
           ,[BUS_TYP_NT_FRNCHSED]
           ,[BUS_TYP_SEASONAL]
           ,[BUS_TYP_YR_ROUND]
           ,[BUS_TYP_DINNER]
           ,[BUS_TYP_BNQT_HALL]
           ,[BUS_TYP_BREKFAST]
           ,[BUS_TYP_FST_FOOD]
           ,[BUS_TYP_TAVERN]
           ,[BUS_TYP_OTHER]
           ,[STAIRWAYS]
           ,[ELEVATORS]
           ,[ESCALATORS]
           ,[GRILLING]
           ,[FRYING]
           ,[BROILING]
           ,[ROASTING]
           ,[COOKING]
           ,[PRK_TYP_VALET]
           ,[PRK_TYP_PREMISES]
           ,[OPR_ON_PREMISES]
           ,[OPR_OFF_PREMISES]
           ,[EMRG_LIGHTS]
           ,[WOOD_STOVE]
           ,[HIST_MARKER]
           ,[EXTNG_SYS_COV_COOKNG]
           ,[EXTNG_SYS_MNT_CNTRCT]
           ,[GAS_OFF_COOKNG]
           ,[HOOD_FILTER_CLND]
           ,[HOOD_DUCTS_EQUIP]
           ,[HOOD_DUCTS_MNT_SCH]
           ,[BC_EXTNG_AVL]
           ,[ADQT_CLEARANCE]
           ,[BEER_SALES]
           ,[WINE_SALES]
           ,[FULL_BAR]
           ,[TOT_EXPNS_FOOD_LIQUOR]
           ,[TOT_EXPNS_OTHERS]
           ,[NET_PROFIT]
           ,[ACCNT_PAYABLE]
           ,[NOTES_PAYABLE]
           ,[BNK_LOANS_PAYABLE])
     VALUES
           (
            @CUSTOMER_ID 
		   ,@POLICY_ID 
		   ,@POLICY_VERSION_ID 
		   ,@LOCATION_ID 
		   ,@PREMISES_ID 
		   ,@RESTAURANT_ID
		   ,@SEATINGCAPACITY 
           ,@BUS_TYP_RESTURANT 
           ,@BUS_TYP_FM_STYLE 
           ,@BUS_TYP_NGHT_CLUB 
           ,@BUS_TYP_FRNCHSED 
           ,@BUS_TYP_NT_FRNCHSED 
           ,@BUS_TYP_SEASONAL 
           ,@BUS_TYP_YR_ROUND 
           ,@BUS_TYP_DINNER 
           ,@BUS_TYP_BNQT_HALL 
           ,@BUS_TYP_BREKFAST 
           ,@BUS_TYP_FST_FOOD 
           ,@BUS_TYP_TAVERN 
           ,@BUS_TYP_OTHER 
           ,@STAIRWAYS 
           ,@ELEVATORS 
           ,@ESCALATORS 
           ,@GRILLING 
           ,@FRYING 
           ,@BROILING 
           ,@ROASTING 
           ,@COOKING 
           ,@PRK_TYP_VALET 
           ,@PRK_TYP_PREMISES 
           ,@OPR_ON_PREMISES 
           ,@OPR_OFF_PREMISES 
           ,@EMRG_LIGHTS 
           ,@WOOD_STOVE 
           ,@HIST_MARKER 
           ,@EXTNG_SYS_COV_COOKNG 
           ,@EXTNG_SYS_MNT_CNTRCT 
           ,@GAS_OFF_COOKNG 
           ,@HOOD_FILTER_CLND 
           ,@HOOD_DUCTS_EQUIP 
           ,@HOOD_DUCTS_MNT_SCH 
           ,@BC_EXTNG_AVL 
           ,@ADQT_CLEARANCE 
           ,@BEER_SALES 
           ,@WINE_SALES 
           ,@FULL_BAR 
           ,@TOT_EXPNS_FOOD_LIQUOR 
           ,@TOT_EXPNS_OTHERS 
           ,@NET_PROFIT 
           ,@ACCNT_PAYABLE 
           ,@NOTES_PAYABLE 
           ,@BNK_LOANS_PAYABLE 
           )
END


