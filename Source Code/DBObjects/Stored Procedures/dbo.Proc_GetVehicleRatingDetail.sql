 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVehicleRatingDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVehicleRatingDetail]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_GetVehicleRatingDetail]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   





CREATE PROC Proc_GetVehicleRatingDetail
(
			@CUSTOMER_ID int
           ,@POLICY_ID int
           ,@POLICY_VERSION_ID smallint
           ,@QUOTE_ID int
)

AS

SELECT * FROM QQ_MOTOR_QUOTE_DETAILS WHERE 
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID
AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND QUOTE_ID = @QUOTE_ID