IF  EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('INSERT_POL_BOP_PREMISESLOCATIONS') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[INSERT_POL_BOP_PREMISESLOCATIONS]
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
CREATE  PROCEDURE [dbo].[INSERT_POL_BOP_PREMISESLOCATIONS]      
          ( 
			@PREMLOC_ID int output		
		   ,@CUSTOMER_ID int
		   ,@POLICY_ID int
		   ,@POLICY_VERSION_ID smallint
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
	--SELECT @PREMLOC_ID = isnull(Max(PREMLOC_ID),0)+ 1 FROM POL_BOP_PREMISESLOCATIONS
	--set @PREMLOC_ID= (SELECT @PREMLOC_ID = isnull(Max(PREMLOC_ID),0)+ 1 FROM POL_BOP_PREMISESLOCATIONS)
	
	 
	set @PREMLOC_ID=(SELECT isnull(Max(PREMLOC_ID),0)+ 1 FROM POL_BOP_PREMISESLOCATIONS)
	
	INSERT INTO [dbo].[POL_BOP_PREMISESLOCATIONS]
           (
            [CUSTOMER_ID]
           ,[POLICY_ID]
           ,[POLICY_VERSION_ID]
           ,[LOCATION_ID]
           ,[BUILDING]
           ,[PREMLOC_ID]
           ,[STREET_ADDR]
           ,[CITY]
           ,[STATE]
           ,[COUNTY]
           ,[ZIP]
           ,[INTEREST]
           ,[FL_TM_EMP]
           ,[PT_TM_EMP]
           ,[ANN_REVENUE]
           ,[OCC_AREA]
           ,[OPEN_AREA]
           ,[TOT_AREA]
           ,[AREA_LEASED]
           )
     VALUES
           (
           	
		   @CUSTOMER_ID 
		   ,@POLICY_ID 
		   ,@POLICY_VERSION_ID 
		   ,@LOCATION_ID 		 
           ,@BUILDING    
           ,@PREMLOC_ID 
           ,@STREET_ADDR 
           ,@CITY 
           ,@STATE 
           ,@COUNTY
           ,@ZIP 
           ,@INTEREST
           ,@FL_TM_EMP 
           ,@PT_TM_EMP 
           ,@ANN_REVENUE 
           ,@OCC_AREA
           ,@OPEN_AREA 
           ,@TOT_AREA 
           ,@AREA_LEASED 
           )
           
           print  @PREMLOC_ID
END


