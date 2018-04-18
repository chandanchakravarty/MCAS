 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQQVehicleRatingInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQQVehicleRatingInfo]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_GetQQVehicleRatingInfo]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   




CREATE PROC dbo.Proc_GetQQVehicleRatingInfo  
(  
 @CUSTOMERID int,  
 @policyID int,  
 @policyVERSIONID int   
)  
  
As  
  
  
SELECT QL.QQ_ID,QL.QQ_NUMBER,PL.RECEIVED_PRMIUM,CL.DEMERIT_DISCOUNT,  
NAMED_DRIVER_AMT,  
UNNAMED_DRIVER_AMT,  
BASE_PREMIUM,  
DEMERIT_DISC_AMT,  
GST_AMOUNT,  
FINAL_PREMIUM  
FROM POL_CUSTOMER_POLICY_LIST PL  
INNER JOIN CLT_QUICKQUOTE_LIST QL  
ON PL.CUSTOMER_ID = QL.CUSTOMER_ID  
AND PL.POLICY_ID = QL.APP_ID  
AND PL.POLICY_VERSION_ID = QL.APP_VERSION_ID  
INNER JOIN QQ_CUSTOMER_PARTICULAR CL  
ON CL.CUSTOMER_ID = QL.CUSTOMER_ID  
AND CL.QUOTE_ID = QL.QQ_ID  
INNER JOIN QQ_MOTOR_QUOTE_DETAILS QD  
ON QD.CUSTOMER_ID = PL.CUSTOMER_ID  
AND QD.POLICY_ID = PL.POLICY_ID  
AND QD.POLICY_VERSION_ID = PL.POLICY_VERSION_ID  
INNER JOIN MNT_COUNTRY_LIST CN  
ON CN.COUNTRY_ID = CL.NATIONALITY  
INNER JOIN MNT_LOOKUP_VALUES LV  
ON LV.LOOKUP_UNIQUE_ID = CL.CUSTOMER_TYPE  
WHERE PL.CUSTOMER_ID = @CUSTOMERID  
AND PL.POLICY_ID = @policyID  
AND PL.POLICY_VERSION_ID = @policyVERSIONID  