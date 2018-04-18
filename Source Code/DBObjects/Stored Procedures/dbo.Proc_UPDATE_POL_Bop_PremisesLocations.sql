

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPDATE_POL_BOP_PREMISESLOCATIONS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPDATE_POL_BOP_PREMISESLOCATIONS]
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
   
CREATE  PROCEDURE [dbo].[UPDATE_POL_BOP_PREMISESLOCATIONS]      
(       
  
			@PREMLOC_ID int 	     
	  	    ,@LOCATION_ID smallint		 
            ,@BUILDING  int       
         
           ,@STREET_ADDR nvarchar(150)
           ,@CITY nvarchar(150)
           ,@STATE nvarchar(10)
           ,@COUNTY nvarchar(150)
           ,@ZIP nvarchar(150)
           ,@INTEREST nvarchar(10)
           ,@FL_TM_EMP nvarchar(150)
           ,@PT_TM_EMP nvarchar(150)
           ,@ANN_REVENUE decimal(18,2)
           ,@OCC_AREA decimal(18,2)
           ,@OPEN_AREA decimal(18,2)
           ,@TOT_AREA decimal(18,2)
           ,@AREA_LEASED nvarchar(10)

)        
AS                
BEGIN      


UPDATE [dbo].[POL_BOP_PREMISESLOCATIONS]
	SET 
	   
		    LOCATION_ID=@LOCATION_ID
		    ,BUILDING	           =@BUILDING
           ,PREMLOC_ID	           =@PREMLOC_ID
           ,STREET_ADDR	           =@STREET_ADDR
           ,CITY	           =@CITY
           ,STATE	           =@STATE
           ,COUNTY	           =@COUNTY
           ,ZIP	           =@ZIP
           ,INTEREST	           =@INTEREST
           ,FL_TM_EMP	           =@FL_TM_EMP
           ,PT_TM_EMP	           =@PT_TM_EMP
           ,ANN_REVENUE	           =@ANN_REVENUE
           ,OCC_AREA	           =@OCC_AREA
           ,OPEN_AREA	           =@OPEN_AREA
           ,TOT_AREA	           =@TOT_AREA
           ,AREA_LEASED	           =@AREA_LEASED


 
	 WHERE PREMLOC_ID=@PREMLOC_ID


END


