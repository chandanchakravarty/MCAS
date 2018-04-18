IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyExistingDriverForUmbrella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyExistingDriverForUmbrella]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : dbo.Proc_GetPolicyExistingDriverForUmbrella
Created by         : Swastika Gaur
Date               :27th Mar'06
Purpose    	   : retrieving data from POL_UMBRELLA_DRIVER_DETAILS
Revison History    :
Used In 	   : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--DROP PROC dbo.Proc_GetPolicyExistingDriverForUmbrella
CREATE PROC dbo.Proc_GetPolicyExistingDriverForUmbrella
@CUSTOMERID int,
@POLICYID int ,
@POLICYVERSIONID int

AS
BEGIN
SELECT    DISTINCT PD.DRIVER_ID, PD.CUSTOMER_ID AS CUSTOMERID,  PD.DRIVER_CODE, 
                      PD.DRIVER_FNAME + ' ' + PD.DRIVER_MNAME + ' ' +
		 PD.DRIVER_LNAME AS DRIVERNAME,
                       CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME + ' ' + CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME + ' ' + CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME
                       AS CUSTOMERNAME, PL.POLICY_VERSION_ID,PD. POLICY_ID,
	PL.POLICY_VERSION_ID,
	PL.POLICY_NUMBER,
	PL.POLICY_DISP_VERSION
FROM         POL_UMBRELLA_DRIVER_DETAILS PD INNER JOIN
                      POL_CUSTOMER_POLICY_LIST PL ON PD.CUSTOMER_ID = PL.CUSTOMER_ID  AND 
			PD.POLICY_ID = PL.POLICY_ID AND
			PD.POLICY_VERSION_ID = PL.POLICY_VERSION_ID
	LEFT OUTER JOIN
                      CLT_CUSTOMER_LIST ON PD.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID
WHERE     (PD.CUSTOMER_ID = @CUSTOMERID) AND (PL.POLICY_LOB = 5)  AND 
		  IsNull(PD.IS_ACTIVE,'') <> 'N' 
                    	
	
END






 




GO

