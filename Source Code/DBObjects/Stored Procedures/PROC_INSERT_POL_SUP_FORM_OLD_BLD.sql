CREATE PROC	PROC_INSERT_POL_SUP_FORM_OLD_BLD
			@CUSTOMER_ID int
           ,@POLICY_ID int
           ,@POLICY_VERSION_ID smallint
           ,@LOCATION_ID smallint
           ,@PREMISES_ID int
           ,@OLDBLD_ID int OUT
           ,@WHN_WIRING_UPDT nvarchar(150)
           ,@WIRING_IN_CNDCT nvarchar(10)
           ,@FUSES_RPLCD	nvarchar(10)
           ,@ALM_WIRING		nvarchar(10)
           ,@WHN_PLBMG_MODRS	nvarchar(150)
           ,@TYP_WTR_PIPS	nvarchar(10)
           ,@WHN_HEATNG_MODRS	nvarchar(150)
           ,@TYP_SYS	nvarchar(10)
           ,@TYP_FUEL	nvarchar(10)
           ,@WHN_ROOF_REPRD	nvarchar(150)
           ,@WHN_ROOF_REPLCD	nvarchar(150)
           ,@ROOF_MTRL	nvarchar(10)
           ,@SPF	nvarchar(10)
           ,@ANY_ABSTS	nvarchar(10)
           ,@ANY_FRB_ABSTS	nvarchar(10)
           ,@ABSTS_ABT_DNE	nvarchar(10)
 AS        
SELECT @OLDBLD_ID =ISNULL(MAX(OLDBLD_ID),0)+1 FROM POL_SUP_FORM_OLD_BLD
INSERT INTO POL_SUP_FORM_OLD_BLD
           (
			CUSTOMER_ID
           ,POLICY_ID
           ,POLICY_VERSION_ID
           ,LOCATION_ID
           ,PREMISES_ID
           ,OLDBLD_ID
           ,WHN_WIRING_UPDT
           ,WIRING_IN_CNDCT
           ,FUSES_RPLCD
           ,ALM_WIRING
           ,WHN_PLBMG_MODRS
           ,TYP_WTR_PIPS
           ,WHN_HEATNG_MODRS
           ,TYP_SYS
           ,TYP_FUEL
           ,WHN_ROOF_REPRD
           ,WHN_ROOF_REPLCD
           ,ROOF_MTRL
           ,SPF
           ,ANY_ABSTS
           ,ANY_FRB_ABSTS
           ,ABSTS_ABT_DNE
           )
     VALUES
           (
           @CUSTOMER_ID
           ,@POLICY_ID
           ,@POLICY_VERSION_ID
           ,@LOCATION_ID
           ,@PREMISES_ID
           ,@OLDBLD_ID
           ,@WHN_WIRING_UPDT
           ,@WIRING_IN_CNDCT
           ,@FUSES_RPLCD
           ,@ALM_WIRING
           ,@WHN_PLBMG_MODRS
           ,@TYP_WTR_PIPS
           ,@WHN_HEATNG_MODRS
           ,@TYP_SYS
           ,@TYP_FUEL
           ,@WHN_ROOF_REPRD
           ,@WHN_ROOF_REPLCD
           ,@ROOF_MTRL
           ,@SPF
           ,@ANY_ABSTS
           ,@ANY_FRB_ABSTS
           ,@ABSTS_ABT_DNE
           )
GO


