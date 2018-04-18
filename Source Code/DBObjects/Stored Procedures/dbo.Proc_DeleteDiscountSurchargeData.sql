IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteDiscountSurchargeData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteDiscountSurchargeData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

/*----------------------------------------------------------                          
Proc Name      : dbo.[Proc_DeleteDiscountSurchargeData]                          
Created by     : Chetna Agarwal                        
Date           : 13-04-2010                          
Purpose        : Delete data from MNT_DISCOUNT_SURCHARGE                                                  
Used In        : Ebix Advantage                      
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
--drop proc dbo.[Proc_DeleteDiscountSurchargeData]

CREATE PROC [dbo].[Proc_DeleteDiscountSurchargeData]
(
@DISCOUNT_ID INT
)
AS
BEGIN

IF EXISTS (  SELECT DISCOUNT_ID FROM POL_DISCOUNT_SURCHARGE  
  	 	WHERE  DISCOUNT_ID = @DISCOUNT_ID 	)  
	return -2

	DELETE FROM MNT_DISCOUNT_SURCHARGE       
	WHERE DISCOUNT_ID=@DISCOUNT_ID 
	return 1
END
GO

