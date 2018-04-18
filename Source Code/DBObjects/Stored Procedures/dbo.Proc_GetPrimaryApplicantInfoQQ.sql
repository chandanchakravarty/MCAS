 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPrimaryApplicantInfoQQ]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPrimaryApplicantInfoQQ]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_GetPrimaryApplicantInfoQQ]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   




CREATE PROC dbo.Proc_GetPrimaryApplicantInfoQQ    
(    
 @CUSTOMERID int,    
 @POLICYID int,    
 @POLICYVERSIONID int     
)    
    
As    
    
    
SELECT CL.CUSTOMER_CODE,LV.LOOKUP_VALUE_DESC,    
CL.CUSTOMER_FIRST_NAME,CL.CUSTOMER_MIDDLE_NAME,    
CL.CUSTOMER_LAST_NAME,CN.COUNTRY_NAME    
FROM POL_CUSTOMER_POLICY_LIST PL    
INNER JOIN CLT_QUICKQUOTE_LIST QL    
ON PL.CUSTOMER_ID = QL.CUSTOMER_ID    
AND PL.POLICY_ID = QL.APP_ID    
AND PL.POLICY_VERSION_ID = QL.APP_VERSION_ID    
INNER JOIN QQ_CUSTOMER_PARTICULAR CL    
ON CL.CUSTOMER_ID = QL.CUSTOMER_ID    
AND CL.QUOTE_ID = QL.QQ_ID    
INNER JOIN MNT_COUNTRY_LIST CN    
ON CN.COUNTRY_ID = CL.NATIONALITY    
INNER JOIN MNT_LOOKUP_VALUES LV    
ON LV.LOOKUP_UNIQUE_ID = CL.CUSTOMER_TYPE    
WHERE PL.CUSTOMER_ID = @CUSTOMERID    
AND POLICY_ID = @POLICYID    
AND POLICY_VERSION_ID = @POLICYVERSIONID    
    
    
    
    
    
    