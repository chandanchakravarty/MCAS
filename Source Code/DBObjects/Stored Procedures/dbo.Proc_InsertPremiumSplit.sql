IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPremiumSplit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPremiumSplit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       : dbo.Proc_InsertPremiumSplit
Created by      : Deepak Gupta         
Date            : 09-26-2006                         
Purpose         : To Insert Premium Split Information
modified by	:Pravesh
Date		:18 may 2007
Revison History :         
modified by	:Pravesh
Date		:14 July 2008
Purpose         : To Insert new Actual Policy version id Column
       
Used In         : Wolverine                
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/      
-- drop proc  dbo.Proc_InsertPremiumSplit   
CREATE PROC [dbo].[Proc_InsertPremiumSplit]
(                
	@CUSTOMER_ID INT,
	@APP_ID INT,
	@APP_VERSION_ID INT,
	@POLICY_ID INT,
	@POLICY_VERSION_ID INT,
	@RISK_ID INT,
	@RISK_TYPE NVARCHAR(10),
	@PROCESS_TYPE NVARCHAR(10)
)                
AS
DECLARE @MAXUNIQUEID NUMERIC
BEGIN                
	IF @APP_ID = 0 and @APP_VERSION_ID = 0 
	BEGIN
		DELETE FROM CLT_PREMIUM_SPLIT_EXCEPTIONS 
		WHERE SPLIT_UNIQUE_ID IN (SELECT UNIQUE_ID FROM CLT_PREMIUM_SPLIT with(nolock) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_TYPE = @PROCESS_TYPE 
			AND RISK_TYPE = @RISK_TYPE AND RISK_ID = @RISK_ID)
		DELETE FROM CLT_PREMIUM_SPLIT_DETAILS 
		WHERE SPLIT_UNIQUE_ID IN (SELECT UNIQUE_ID FROM CLT_PREMIUM_SPLIT with(nolock) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_TYPE = @PROCESS_TYPE 
			AND RISK_TYPE = @RISK_TYPE AND RISK_ID = @RISK_ID)
		DELETE FROM CLT_PREMIUM_SPLIT  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_TYPE = @PROCESS_TYPE AND RISK_TYPE = @RISK_TYPE AND RISK_ID = @RISK_ID
	END
	ELSE IF @POLICY_ID = 0 and @POLICY_VERSION_ID = 0 
	BEGIN
		DELETE FROM CLT_PREMIUM_SPLIT_EXCEPTIONS WHERE SPLIT_UNIQUE_ID IN (SELECT UNIQUE_ID FROM CLT_PREMIUM_SPLIT with(nolock) WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID 
			AND RISK_TYPE = @RISK_TYPE AND RISK_ID = @RISK_ID)
		DELETE FROM CLT_PREMIUM_SPLIT_DETAILS WHERE SPLIT_UNIQUE_ID IN (SELECT UNIQUE_ID FROM CLT_PREMIUM_SPLIT  with(nolock)WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID 
			AND RISK_TYPE = @RISK_TYPE AND RISK_ID = @RISK_ID)
		DELETE FROM CLT_PREMIUM_SPLIT  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND RISK_TYPE = @RISK_TYPE AND RISK_ID = @RISK_ID
	END
	

	SELECT @MAXUNIQUEID = ISNULL(MAX(UNIQUE_ID),0)+1 FROM CLT_PREMIUM_SPLIT with(nolock)
	
	INSERT INTO CLT_PREMIUM_SPLIT (UNIQUE_ID,
						CUSTOMER_ID,
						APP_ID,
						APP_VERSION_ID,
						POLICY_ID,
						POLICY_VERSION_ID,
						RISK_ID,
						RISK_TYPE,
						PROCESS_TYPE,
						ACTL_POLICY_VERSION_ID) 
					VALUES
					(	@MAXUNIQUEID,
						@CUSTOMER_ID,
						@APP_ID,
						@APP_VERSION_ID,
						@POLICY_ID,
						@POLICY_VERSION_ID,
						@RISK_ID,
						@RISK_TYPE,
						@PROCESS_TYPE,
						@POLICY_VERSION_ID) 
	SELECT @MAXUNIQUEID UNIQUE_ID;
END









GO

