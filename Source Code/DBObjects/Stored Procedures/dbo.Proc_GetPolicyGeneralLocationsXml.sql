IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyGeneralLocationsXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyGeneralLocationsXml]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name          : Dbo.Proc_GetPolicyGeneralLocationsXml      
Created by         : Sumit Chhabra      
Date               : 03/04/2006      
Purpose            : To get details from POL_LOCATIONS      
Revison History :      
Used In                :   Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
create  PROC Dbo.Proc_GetPolicyGeneralLocationsXml      
(      
 @CUSTOMER_ID int,      
 @POLICY_ID int,      
 @POLICY_VERSION_ID smallint      
-- @LOCATION_ID int      
             
)      
      
AS      
BEGIN      
 SELECT CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,LOCATION_ID,      
               LOC_NUM,LOC_ADD1,LOC_ADD2,      
        LOC_CITY,LOC_COUNTY,LOC_STATE,LOC_ZIP,LOC_TERRITORY,LOC_COUNTRY             
 FROM POL_LOCATIONS      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
  POLICY_VERSION_ID = @POLICY_VERSION_ID      
--AND LOCATION_ID=@LOCATION_ID      
      
END    
    
  



GO

