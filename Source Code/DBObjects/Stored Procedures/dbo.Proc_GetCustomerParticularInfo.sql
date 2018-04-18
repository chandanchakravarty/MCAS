 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerParticularInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerParticularInfo]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_GetCustomerParticularInfo]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   




CREATE PROC dbo.Proc_GetCustomerParticularInfo (@CUSTOMER_ID int,@ID int)

AS

BEGIN

	SELECT * FROM QQ_CUSTOMER_PARTICULAR 
	WHERE CUSTOMER_ID = @CUSTOMER_ID and QUOTE_ID = @ID
END