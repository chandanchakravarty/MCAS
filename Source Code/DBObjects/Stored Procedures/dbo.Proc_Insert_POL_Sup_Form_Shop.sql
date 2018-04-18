IF  EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('INSERT_POL_SUP_FORM_SHOP') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[INSERT_POL_SUP_FORM_SHOP]
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
CREATE  PROCEDURE [dbo].[INSERT_POL_SUP_FORM_SHOP]      
( 
			@SHOP_ID int output		
		   ,@CUSTOMER_ID int
		   ,@POLICY_ID int
		   ,@POLICY_VERSION_ID smallint
		   ,@LOCATION_ID smallint
		   ,@PREMISES_ID int		   
           ,@UNITS int
           ,@PERCENT_OCUP real
           ,@RESTURANT_OCUP nvarchar(10)
           ,@FLAME_COOKING nvarchar(10)
           ,@NUM_FRYERS nvarchar(10)
           ,@NUM_GRILLS nvarchar(10)
           ,@DUCT_SYS nvarchar(10)
           ,@SUPPR_SYS nvarchar(10)
           ,@DUCT_CLND_PST_SIX_MONTHS nvarchar(10)
           ,@IS_INSURED nvarchar 
           ,@TENANT_LIABILITY nvarchar(10)
           ,@PERCENT_SALES nvarchar(10)
           ,@SEPARATE_BAR nvarchar(10)
           ,@BBQ_PIT nvarchar(10)
           ,@BBQ_PIT_DIST real
           ,@BLDG_TYPE_COOKNG nvarchar(10)
           ,@IS_ENTERTNMT nvarchar(10)
		   

)
AS       
BEGIN
	
	SET @SHOP_ID=(SELECT isnull(Max(SHOP_ID),0)+ 1 FROM POL_SUP_FORM_SHOP)
	INSERT INTO [dbo].[POL_SUP_FORM_SHOP]
           (
            [CUSTOMER_ID]
           ,[POLICY_ID]
           ,[POLICY_VERSION_ID]
           ,[LOCATION_ID]
           ,[PREMISES_ID]
           ,[SHOP_ID]
           ,[UNITS]
           ,[PERCENT_OCUP]
           ,[RESTURANT_OCUP]
           ,[FLAME_COOKING]
           ,[NUM_FRYERS]
           ,[NUM_GRILLS]
           ,[DUCT_SYS]
           ,[SUPPR_SYS]
           ,[DUCT_CLND_PST_SIX_MONTHS]
           ,[IS_INSURED]
           ,[TENANT_LIABILITY]
           ,[PERCENT_SALES]
           ,[SEPARATE_BAR]
           ,[BBQ_PIT]
           ,[BBQ_PIT_DIST]
           ,[BLDG_TYPE_COOKNG]
           ,[IS_ENTERTNMT]
           )
     VALUES
           (
            @CUSTOMER_ID
           ,@POLICY_ID
           ,@POLICY_VERSION_ID
           ,@LOCATION_ID
           ,@PREMISES_ID
           ,@SHOP_ID
           ,@UNITS
           ,@PERCENT_OCUP
           ,@RESTURANT_OCUP
           ,@FLAME_COOKING
           ,@NUM_FRYERS
           ,@NUM_GRILLS
           ,@DUCT_SYS
           ,@SUPPR_SYS
           ,@DUCT_CLND_PST_SIX_MONTHS
           ,@IS_INSURED
           ,@TENANT_LIABILITY
           ,@PERCENT_SALES
           ,@SEPARATE_BAR
           ,@BBQ_PIT
           ,@BBQ_PIT_DIST
           ,@BLDG_TYPE_COOKNG
           ,@IS_ENTERTNMT
           )
END


