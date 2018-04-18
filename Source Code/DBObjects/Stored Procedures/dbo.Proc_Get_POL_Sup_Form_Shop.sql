IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_POL_SUP_FORM_SHOP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_POL_SUP_FORM_SHOP]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.GET_POL_SUP_FORM_SHOP]
--Created by         : Rajeev        
--Date               :  11 NOVEMBER 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/    
     
CREATE  PROCEDURE [dbo].[GET_POL_SUP_FORM_SHOP]      
(       
 
 @CUSTOMER_ID int,
 @POLICY_ID int ,
 @POLICY_VERSION_ID smallint,
 @LOCATION_ID smallint,
 @PREMISES_ID INT
)        
AS       
BEGIN      
		Select * FROM POL_SUP_FORM_SHOP
		
		--where SHOP_ID=@SHOP_ID
		
		--Select * FROM POL_SUP_FORM_SHOP
		
		where 
		CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID 
		and POLICY_VERSION_ID=@POLICY_VERSION_ID
		AND LOCATION_ID=@LOCATION_ID
		AND PREMISES_ID=@PREMISES_ID
End