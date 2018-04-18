IF  EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('INSERT_POL_BOP_PREMISES_LOC_DETAILS') AND type in (N'P', N'PC'))


DROP PROCEDURE [dbo].[INSERT_POL_BOP_PREMISES_LOC_DETAILS]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO 

---------------------------------------------------------------
--Proc Name          : dbo.POL_SUP_FORM_SHOP
--Created by         : Rajeev        
--Date               :  11 NOVEMBER 2011         
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/      
CREATE  PROCEDURE [dbo].[INSERT_POL_BOP_PREMISES_LOC_DETAILS]      
( 
			@LOC_DETAILS_ID int output		
		   ,@CUSTOMER_ID int
		   ,@POLICY_ID int
		   ,@POLICY_VERSION_ID smallint
		   ,@LOCATION_ID smallint
		   ,@PREMISES_ID int  
                   
                   
           ,@DESC_BLDNG nvarchar(150)
           ,@DESC_OPERTN nvarchar(150)
           ,@LST_ALL_OCCUP nvarchar(150)
           ,@ANN_SALES decimal(18,2)
           ,@TOT_PAYROLL decimal(18,2)
           --,@RATE_NUM int
           --,@RATE_GRP int
           --,@RATE_TER_NUM int
           ,@PROT_CLS nvarchar(150)
           ,@IS_ALM_USED nvarchar(10)
           ,@IS_RES_SPACE nvarchar(10)
           ,@RES_SPACE_SMK_DET nvarchar(10)
           ,@RES_OCC nvarchar(10)
           ,@FIRE_HYDRANT_DIST real
           ,@FIRE_STATION_DIST real
           ,@FIRE_DIST_NAME nvarchar(150)
           ,@FIRE_DIST_CODE nvarchar(150)
           ,@BCEGS nvarchar(150)
           ,@CITY_LMT nvarchar(10)
           ,@SWIMMING_POOL nvarchar(10)
           ,@PLAY_GROUND nvarchar(10)
           ,@BUILD_UNDER_CON nvarchar(10)
           ,@BUILD_SHPNG_CENT nvarchar(10)
           ,@BOILER nvarchar(10)
           ,@MED_EQUIP nvarchar(10)
           ,@ALARM_TYPE  nvarchar(10)
           ,@ALARM_DESC nvarchar(10)
           ,@SAFE_VAULT nvarchar(10)
           ,@PREMISE_ALARM nvarchar(10)
           ,@CYL_DOOR_LOCK nvarchar(10)
           ,@SAFE_VAULT_LBL nvarchar(10)
           ,@SAFE_VAULT_CLASS nvarchar(150)
           ,@SAFE_VAULT_MANUFAC nvarchar(150)
           ,@MAX_CASH_PREM  decimal(18,2)
           ,@MAX_CASH_MSG  decimal(18,2)
           ,@MONEY_OVER_NIGHT  decimal(18,2)
           ,@FREQUENCY_DEPOSIT int
           ,@SAFE_DOOR_CONST nvarchar(50)
           ,@GRADE nvarchar(10)
           ,@OTH_PROTECTION nvarchar(100)
           ,@RIGHT_EXP_DESC nvarchar(100)
           ,@RIGHT_EXP_DIST nvarchar(100)
           ,@LEFT_EXP_DESC nvarchar(100)
           ,@LEFT_EXP_DIST nvarchar(100)
           ,@FRONT_EXP_DESC nvarchar(100)
           ,@FRONT_EXP_DIST nvarchar(100)
           ,@REAR_EXP_DESC nvarchar(100)
           ,@REAR_EXP_DIST nvarchar(100)
           ,@COUNTY nvarchar(150)
		   

)
AS       
BEGIN
	--SELECT @LOC_DETAILS_ID = isnull(Max(BLDNG_ID),0)+ 1 FROM POL_BOP_PREMISES_INFO
	
	SET @LOC_DETAILS_ID=(SELECT isnull(Max(@LOC_DETAILS_ID),0)+ 1 FROM POL_BOP_PREMISES_LOC_DETAILS )
	INSERT INTO [dbo].[POL_BOP_PREMISES_LOC_DETAILS]
           (
            [CUSTOMER_ID]
           ,[POLICY_ID]
           ,[POLICY_VERSION_ID]
           ,[LOCATION_ID]
           ,[PREMISES_ID]
          ,[LOC_DETAILS_ID]
           ,[DESC_BLDNG]
           ,[DESC_OPERTN]
           ,[LST_ALL_OCCUP]
           ,[ANN_SALES]
           ,[TOT_PAYROLL]
           --,[RATE_NUM]
           --,[RATE_GRP]
           --,[RATE_TER_NUM]
           ,[PROT_CLS]
           ,[IS_ALM_USED]
           ,[IS_RES_SPACE]
           ,[RES_SPACE_SMK_DET]
           ,[RES_OCC]
           ,[FIRE_HYDRANT_DIST]
           ,[FIRE_STATION_DIST]
           ,[FIRE_DIST_NAME]
           ,[FIRE_DIST_CODE]
           ,[BCEGS]
           ,[CITY_LMT]
           ,[SWIMMING_POOL]
           ,[PLAY_GROUND]
           ,[BUILD_UNDER_CON]
           ,[BUILD_SHPNG_CENT]
           ,[BOILER]
           ,[MED_EQUIP]
           ,[ALARM_TYPE]
           ,[ALARM_DESC]
           ,[SAFE_VAULT]
           ,[PREMISE_ALARM]
           ,[CYL_DOOR_LOCK]
           ,[SAFE_VAULT_LBL]
           ,[SAFE_VAULT_CLASS]
           ,[SAFE_VAULT_MANUFAC]
           ,[MAX_CASH_PREM]
           ,[MAX_CASH_MSG]
           ,[MONEY_OVER_NIGHT]
           ,[FREQUENCY_DEPOSIT]
           ,[SAFE_DOOR_CONST]
           ,[GRADE]
           ,[OTH_PROTECTION]
           ,[RIGHT_EXP_DESC]
           ,[RIGHT_EXP_DIST]
           ,[LEFT_EXP_DESC]
           ,[LEFT_EXP_DIST]
           ,[FRONT_EXP_DESC]
           ,[FRONT_EXP_DIST]
           ,[REAR_EXP_DESC]
           ,[REAR_EXP_DIST]
           ,[COUNTY]
           )
     VALUES
           (
            @CUSTOMER_ID
           ,@POLICY_ID
           ,@POLICY_VERSION_ID
           ,@LOCATION_ID
           ,@PREMISES_ID
           ,@LOC_DETAILS_ID
          
           ,@DESC_BLDNG
           ,@DESC_OPERTN
           ,@LST_ALL_OCCUP
           ,@ANN_SALES
           ,@TOT_PAYROLL
           --,@RATE_NUM
           --,@RATE_GRP
           --,@RATE_TER_NUM
           ,@PROT_CLS
           ,@IS_ALM_USED
           ,@IS_RES_SPACE
           ,@RES_SPACE_SMK_DET
           ,@RES_OCC
           ,@FIRE_HYDRANT_DIST
           ,@FIRE_STATION_DIST
           ,@FIRE_DIST_NAME
           ,@FIRE_DIST_CODE
           ,@BCEGS
           ,@CITY_LMT
           ,@SWIMMING_POOL
           ,@PLAY_GROUND
           ,@BUILD_UNDER_CON
           ,@BUILD_SHPNG_CENT
           ,@BOILER
           ,@MED_EQUIP
           ,@ALARM_TYPE
           ,@ALARM_DESC
           ,@SAFE_VAULT
           ,@PREMISE_ALARM
           ,@CYL_DOOR_LOCK
           ,@SAFE_VAULT_LBL
           ,@SAFE_VAULT_CLASS
           ,@SAFE_VAULT_MANUFAC
           ,@MAX_CASH_PREM
           ,@MAX_CASH_MSG
           ,@MONEY_OVER_NIGHT
           ,@FREQUENCY_DEPOSIT
           ,@SAFE_DOOR_CONST
           ,@GRADE
           ,@OTH_PROTECTION
           ,@RIGHT_EXP_DESC
           ,@RIGHT_EXP_DIST
           ,@LEFT_EXP_DESC
           ,@LEFT_EXP_DIST
           ,@FRONT_EXP_DESC
           ,@FRONT_EXP_DIST
           ,@REAR_EXP_DESC
           ,@REAR_EXP_DIST
           ,@COUNTY
           )
END


