IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateDefaultCoInsurer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateDefaultCoInsurer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:  Charles Gomes  
-- Create date: 7-April-2010  
-- Description: Updates Default Co-Insurer  
-- DROP PROC Proc_UpdateDefaultCoInsurer 2043,230,1,'W001',14547  
-- =============================================  
--begin tran  
--go  
--DROP PROC Proc_UpdateDefaultCoInsurer  
--go  
  
CREATE PROCEDURE [dbo].[Proc_UpdateDefaultCoInsurer]     
 @CUSTOMER_ID INT,  
 @POLICY_ID INT,  
 @POLICY_VERSION_ID SMALLINT,
 @CARRIER_CODE NVARCHAR(6),  
 @LEADER_FOLLOWER INT  
AS  
BEGIN   
 --SET NOCOUNT ON;  
   
  DECLARE @REIN_COMAPANY_ID INT,
  @MAX_COINSURANCE_ID INT,
  @NON_LEAD_COINS_ID INT,
  @SYS_CARRIER_ID INT,
  @POL_CO_INSURANCE INT
  
  SELECT @POL_CO_INSURANCE = ISNULL(CO_INSURANCE,0) FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID
  
  --Co Insurance Tab Not Available for Direct
  IF @POL_CO_INSURANCE = 14547 --Direct
  AND EXISTS(SELECT COINSURANCE_ID FROM POL_CO_INSURANCE WITH(NOLOCK) 
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)
  BEGIN
	DELETE FROM POL_CO_INSURANCE WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID 
	RETURN
  END  
  
  SELECT @SYS_CARRIER_ID = SYS_CARRIER_ID FROM MNT_SYSTEM_PARAMS WITH(NOLOCK)  
    
  --SELECT @REIN_COMAPANY_ID = REIN_COMAPANY_ID FROM MNT_REIN_COMAPANY_LIST WITH(NOLOCK)  
  --WHERE REIN_COMAPANY_CODE = @CARRIER_CODE  
    
  --IF @REIN_COMAPANY_ID IS NULL  
  --BEGIN  
  -- RETURN  
  --END     
  
  IF NOT EXISTS(SELECT REIN_COMAPANY_ID FROM MNT_REIN_COMAPANY_LIST WITH(NOLOCK) WHERE REIN_COMAPANY_ID = @SYS_CARRIER_ID)
  BEGIN
	RETURN
  END 
  
  SET @REIN_COMAPANY_ID = @SYS_CARRIER_ID  
    
  IF EXISTS(SELECT COINSURANCE_ID FROM POL_CO_INSURANCE WITH(NOLOCK) WHERE    
   CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND COMPANY_ID = @REIN_COMAPANY_ID)  
  BEGIN  
   IF @LEADER_FOLLOWER = 14547--114266 --DIRECT --Changed by Charles on 17-May-10
	   BEGIN  
		DELETE FROM POL_CO_INSURANCE WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID 
		AND COMPANY_ID = @REIN_COMAPANY_ID  
	   END  
   ELSE  
	   BEGIN  
		UPDATE POL_CO_INSURANCE SET LEADER_FOLLOWER = @LEADER_FOLLOWER, LAST_UPDATED_DATETIME = GETDATE()  
		WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND COMPANY_ID = @REIN_COMAPANY_ID 
		
		-- to make leader follower number blank in case follower Changed by sonal 
		if (@LEADER_FOLLOWER = 14549 )
		BEGIN
		
				UPDATE POL_CO_INSURANCE SET LEADER_FOLLOWER = @LEADER_FOLLOWER, LAST_UPDATED_DATETIME = GETDATE(),LEADER_POLICY_NUMBER=''  
				WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND COMPANY_ID = @REIN_COMAPANY_ID
		END
		
		
		IF @LEADER_FOLLOWER = 14548 --Leader
			AND EXISTS(SELECT COINSURANCE_ID FROM POL_CO_INSURANCE WITH(NOLOCK) WHERE  --Other Leader Entries exist  
			CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND COMPANY_ID != @REIN_COMAPANY_ID 
			AND LEADER_FOLLOWER = 14548)
			BEGIN
				UPDATE POL_CO_INSURANCE SET LEADER_FOLLOWER = 14549 ,LEADER_POLICY_NUMBER='', LAST_UPDATED_DATETIME = GETDATE() --14549  --Follower  
				WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND COMPANY_ID != @REIN_COMAPANY_ID 
			END
		--ELSE IF @LEADER_FOLLOWER = 14549 --Follower
		--	-- Leader Entry does not exist 
		--	AND NOT EXISTS(SELECT COINSURANCE_ID FROM POL_CO_INSURANCE WITH(NOLOCK) WHERE    
		--	CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND COMPANY_ID != @REIN_COMAPANY_ID 
		--	AND LEADER_FOLLOWER = 14548)
		--	-- Atleast one non leader record exist 
		--	AND EXISTS(SELECT COINSURANCE_ID FROM POL_CO_INSURANCE WITH(NOLOCK) WHERE    
		--	CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND COMPANY_ID != @REIN_COMAPANY_ID)
		--	BEGIN
		--		SELECT TOP 1 @NON_LEAD_COINS_ID = COINSURANCE_ID FROM POL_CO_INSURANCE WITH(NOLOCK)
		--		WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID 
		--		AND COMPANY_ID != @REIN_COMAPANY_ID
				
		--		UPDATE POL_CO_INSURANCE SET LEADER_FOLLOWER = 14548, LAST_UPDATED_DATETIME = GETDATE() 
		--		WHERE COINSURANCE_ID = @NON_LEAD_COINS_ID
		--	END
	   END  
  END  
  ELSE IF @LEADER_FOLLOWER != 14547--114266 --Not DIRECT, either LEADER or FOLLOER --Changed by Charles on 17-May-10
  BEGIN    
	   SELECT @MAX_COINSURANCE_ID = ISNULL(MAX(COINSURANCE_ID),0)+1 FROM POL_CO_INSURANCE WITH(NOLOCK)   
	       
	   INSERT INTO POL_CO_INSURANCE(COINSURANCE_ID,COMPANY_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CO_INSURER_NAME,LEADER_FOLLOWER,COINSURANCE_PERCENT,  
	   COINSURANCE_FEE,BROKER_COMMISSION,TRANSACTION_ID,LEADER_POLICY_NUMBER,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME)  
	   VALUES  
	   (@MAX_COINSURANCE_ID,@REIN_COMAPANY_ID,@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,NULL,@LEADER_FOLLOWER,NULL,NULL,NULL,NULL,NULL,'Y',NULL,
	   GETDATE(),NULL,NULL)  
  END   
END  
  
--go  
--exec Proc_UpdateDefaultCoInsurer 2043,231,1,'W001',14547  
--go  
--select * from POL_CO_INSURANCE  
--go  
--rollback tran  
--go
GO

