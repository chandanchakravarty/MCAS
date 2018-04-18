IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPayeeInfoForCheck]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPayeeInfoForCheck]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------          
Proc Name       : Proc_GetPayeeInfoForCheck          
Created by      :          
Date            :        
Purpose     	: To get Payee Info from AGENCY table        
-----------------------------------------------------------*/   
       
-- drop proc dbo.Proc_GetPayeeInfoForCheck  
CREATE PROC dbo.Proc_GetPayeeInfoForCheck
(
 @CUSTOMER_ID INT,
 @POLICY_ID INT,
 @POLICY_VERSION_ID INT
)
AS
DECLARE @AGENCY_ID INT
BEGIN
	SELECT @AGENCY_ID = AGENCY_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID
	SELECT AGENCY_DISPLAY_NAME,AGENCY_ADD1,AGENCY_ADD2,AGENCY_CITY,AGENCY_STATE,AGENCY_ZIP 
	FROM MNT_AGENCY_LIST 
	WHERE AGENCY_ID = @AGENCY_ID
END




GO

