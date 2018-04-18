

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPDATE_POL_SUP_FORM_SHOP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPDATE_POL_SUP_FORM_SHOP]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------
--Proc Name          : dbo.UPDATE_POL_SUP_FORM_SHOP
--Created by         : Rajeev        
--Date               :  9 NOVEMBER 2011        
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/        
   
CREATE  PROCEDURE [dbo].[UPDATE_POL_SUP_FORM_SHOP]      
(       
  
			@SHOP_ID int 	     
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


UPDATE [dbo].[POL_SUP_FORM_SHOP]
	SET 
	   
		    UNITS	           =@UNITS
           ,PERCENT_OCUP	           =@PERCENT_OCUP
           ,RESTURANT_OCUP	           =@RESTURANT_OCUP
           ,FLAME_COOKING	           =@FLAME_COOKING
           ,NUM_FRYERS	           =@NUM_FRYERS
           ,NUM_GRILLS	           =@NUM_GRILLS
           ,DUCT_SYS	           =@DUCT_SYS
           ,SUPPR_SYS	           =@SUPPR_SYS
           ,DUCT_CLND_PST_SIX_MONTHS	           =@DUCT_CLND_PST_SIX_MONTHS
           ,IS_INSURED	           =@IS_INSURED
           ,TENANT_LIABILITY	           =@TENANT_LIABILITY
           ,PERCENT_SALES	           =@PERCENT_SALES
           ,SEPARATE_BAR	           =@SEPARATE_BAR
           ,BBQ_PIT	           =@BBQ_PIT
           ,BBQ_PIT_DIST	           =@BBQ_PIT_DIST
           ,BLDG_TYPE_COOKNG	           =@BLDG_TYPE_COOKNG
           ,IS_ENTERTNMT =@IS_ENTERTNMT

 
	 WHERE SHOP_ID=@SHOP_ID


END


