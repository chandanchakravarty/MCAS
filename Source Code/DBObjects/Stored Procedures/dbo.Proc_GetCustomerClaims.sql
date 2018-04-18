IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerClaims]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerClaims]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc Proc_GetCustomerClaims
--go

/*----------------------------------------------------------      
Proc Name       : dbo.Proc_GetCustomerClaims      
Created by      : Vijay Arora
Date            : 07/05/2005
Purpose      	: Get the Claim number of the Customer
Revison History :      
Used In   	: Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--drop proc Dbo.Proc_GetCustomerClaims 
CREATE PROC Dbo.Proc_GetCustomerClaims       
(      
	 @CUSTOMER_ID  int ,
	 @POLICY_ID    varchar(20) = null ,  
     @CALLED_FROM VARCHAR(20) = null       
)      


AS
 
BEGIN 

IF(@CALLED_FROM = 'POL')
BEGIN

	IF EXISTS(SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID
       AND APP_ID = @POLICY_ID)

    BEGIN	
		SELECT CLAIM_ID, CLAIM_NUMBER, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID FROM CLM_CLAIM_INFO
		WHERE CUSTOMER_ID = @CUSTOMER_ID  AND  POLICY_ID = @POLICY_ID 
        

	END

END

IF(@CALLED_FROM = 'QQ')
	BEGIN
---------APPLOCATION CASE AND QUOTE CASE 

		IF EXISTS(SELECT POLICY_ID  FROM pol_customer_policy_list PCPL LEFT JOIN  CLT_QUICKQUOTE_LIST CQL ON 
		PCPL.CUSTOMER_ID = CQL.CUSTOMER_ID AND PCPL.APP_ID = CQL.APP_ID
		WHERE PCPL.CUSTOMER_ID = @CUSTOMER_ID AND PCPL.APP_ID = @POLICY_ID)  
	    
		 BEGIN
		 SELECT CLAIM_ID, CLAIM_NUMBER, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID FROM CLM_CLAIM_INFO
		 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID      	      
		 END	
	END

IF(@POLICY_ID = '')

BEGIN 
          SELECT CLAIM_ID, CLAIM_NUMBER, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID FROM CLM_CLAIM_INFO
		  WHERE CUSTOMER_ID = @CUSTOMER_ID

END 


IF(@CALLED_FROM = 'CLAIM')

BEGIN 
          SELECT CLAIM_ID, CLAIM_NUMBER, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID FROM CLM_CLAIM_INFO
			WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID

END 

	
END    

--
--go
--exec Proc_GetCustomerClaims 1779,'10','QQ'
--
--
--rollback tran
    
     






GO

