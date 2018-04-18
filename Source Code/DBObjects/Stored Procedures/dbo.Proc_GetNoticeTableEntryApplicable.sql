IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNoticeTableEntryApplicable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNoticeTableEntryApplicable]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetNoticeTableEntryApplicable
Created by      : Vijay Arora    
Date            : 05-01-2006
Purpose         : Notice Table Generation Entry is Applicable or not
Revison History :        
Used In         : Wolverine          
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC Proc_GetNoticeTableEntryApplicable    
@CUSTOMER_ID INT,    
@APP_ID INT,
@APP_VERSION_ID INT,
@POLICY_ID INT,    
@POLICY_VERSION_ID SMALLINT,
@RETVAL INT OUTPUT  
AS    
BEGIN    
	
	DECLARE @RECEIVED_PREMIUM INT
	DECLARE @SUSPENSE_PAYMENT VARCHAR(5)
	DECLARE @RETURN_PREMIUM INT
	DECLARE @RETURN_PAYMENT INT

	IF EXISTS (SELECT RECEIVED_PRMIUM FROM APP_LIST WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
		AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID)
		BEGIN
			SELECT @RECEIVED_PREMIUM = CAST(RECEIVED_PRMIUM AS INT) FROM APP_LIST WITH (NOLOCK) 
			WHERE CUSTOMER_ID = @CUSTOMER_ID 
			AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID					
				
			IF @RECEIVED_PREMIUM IS NOT NULL AND @RECEIVED_PREMIUM <> 0 
				BEGIN
						
					SET @RETURN_PREMIUM = 1
				END
			ELSE	
				BEGIN
					SET @RETURN_PREMIUM = 0
				END
		END
	ELSE

		BEGIN

			SET @RETURN_PREMIUM = 0	
		END


	IF EXISTS(SELECT ITEM_STATUS FROM ACT_CUSTOMER_OPEN_ITEMS WITH (NOLOCK) 
		WHERE CUSTOMER_ID = @CUSTOMER_ID 
		AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)
		BEGIN
			SELECT @SUSPENSE_PAYMENT = ITEM_STATUS 
			FROM ACT_CUSTOMER_OPEN_ITEMS WITH (NOLOCK)
			WHERE CUSTOMER_ID = @CUSTOMER_ID 
			AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID

			IF @SUSPENSE_PAYMENT = 'SP'
				BEGIN
					SET @RETURN_PAYMENT = 0
				END
			ELSE
				BEGIN
					SET @RETURN_PAYMENT = 1
				END
					
				
		END
	
	ELSE
		BEGIN
			SET @RETURN_PAYMENT = 0

		END	



	IF @RETURN_PREMIUM = 1 AND @RETURN_PAYMENT = 1
		BEGIN
			SET @RETVAL = 1
		END

	ELSE
		BEGIN
			SET @RETVAL = 0
		END
END




GO

