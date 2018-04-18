IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLCLM_INSURED_LOCATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLCLM_INSURED_LOCATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetXMLCLM_INSURED_LOCATION  
Created by      : Vijay Arora  
Date            : 5/1/2006  
Purpose     : To get the details of the Location from table named CLM_INSURED_LOCATION  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
DROP PROC dbo.Proc_GetXMLCLM_INSURED_LOCATION
------   ------------       -------------------------*/  
CREATE PROC dbo.Proc_GetXMLCLM_INSURED_LOCATION  
(  
@INSURED_LOCATION_ID     int,  
@CLAIM_ID int  
)  
AS  
BEGIN  
SELECT INSURED_LOCATION_ID,  
CLAIM_ID,  
LOCATION_DESCRIPTION,  
ADDRESS1,  
ADDRESS2,  
CITY,  
STATE,  
ZIP,  
COUNTRY,
IS_ACTIVE, --Added for Itrack Issue 5833 -- To activate/deactivate Location 
ISNULL(POLICY_LOCATION_ID,0) AS POLICY_LOCATION_ID  
FROM CLM_INSURED_LOCATION  
WHERE INSURED_LOCATION_ID = @INSURED_LOCATION_ID AND CLAIM_ID = @CLAIM_ID  
END  
  
  
  
  
GO

