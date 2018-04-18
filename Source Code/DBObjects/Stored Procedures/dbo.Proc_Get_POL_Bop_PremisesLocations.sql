IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_POL_BOP_PREMISESLOCATIONS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_POL_BOP_PREMISESLOCATIONS]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.POL_BOP_PREMISES_INFO]
--Created by         : Rajeev        
--Date               :  16 NOVEMBER 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/    
     
CREATE  PROCEDURE [dbo].[GET_POL_BOP_PREMISESLOCATIONS]      
(       
 --@BLDNG_ID int,
 @CUSTOMER_ID int,
 @POLICY_ID int ,
 @POLICY_VERSION_ID smallint,
 @LOCATION_ID smallint	
 --@PREMISES_ID int
)       
AS       
BEGIN      
		Select * FROM POL_BOP_PREMISESLOCATIONS
		
		--where BLDNG_ID=@BLDNG_ID
		
		--Select * FROM POL_SUP_FORM_SHOP
		
		where 
		CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID 
		and POLICY_VERSION_ID=@POLICY_VERSION_ID
		and LOCATION_ID=@LOCATION_ID
		--and PREMISES_ID=@PREMISES_ID
		
End