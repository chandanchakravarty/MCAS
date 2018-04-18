IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Pol_DeleteDiscountSurcharge]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Pol_DeleteDiscountSurcharge]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- changes by praveer
--Drop PROC Proc_Pol_DeleteDiscountSurcharge  
CREATE PROC [dbo].[Proc_Pol_DeleteDiscountSurcharge]  
@CUSTOMER_ID INT,        
@POLICY_ID INT,        
@POLICY_VERSION_ID INT,   
@DISCOUNT_ROW_ID INT  
  
AS  
BEGIN  
 DELETE FROM POL_DISCOUNT_SURCHARGE WHERE DISCOUNT_ROW_ID=@DISCOUNT_ROW_ID  AND 
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID
END  
GO

