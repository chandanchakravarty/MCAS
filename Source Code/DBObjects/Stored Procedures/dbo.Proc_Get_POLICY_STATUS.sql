IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_POLICY_STATUS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_POLICY_STATUS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Get_POLICY_STATUS    
/*                          
----------------------------------------------------------                              
Proc Name       : dbo.Proc_Get_POLICY_STATUS                          
Created by      : Pradeep                            
Date            : Mar 22, 2006                           
Purpose         : Returns the current status of the policy whether                              
		 New business or renewal
Revison History :                              
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------                             
*/                          
                          
CREATE  PROCEDURE Proc_Get_POLICY_STATUS
(                          
	@CUSTOMER_ID int,                          
	@POLICY_ID int,                          
	@POLICY_VERSION_ID smallint
)

AS

BEGIN
	DECLARE @EFFECTIVE_DATE DateTime
	DECLARE @INCEPTION_DATE DateTime
	DECLARE @APP_TERMS Int
	DECLARE @EXPIRATION_DATE DateTime
	DECLARE @INTERVAL Int	
	DECLARE @TOT_DAYS Int	
	
	/*
	SELECT @EFFECTIVE_DATE = APP_EFFECTIVE_DATE,
		@INCEPTION_DATE = APP_INCEPTION_DATE,
		@APP_TERMS = APP_TERMS
	FROM POL_CUSTOMER_POLICY_LIST
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		POLICY_ID = @POLICY_ID AND
		POLICY_VERSION_ID = @POLICY_VERSION_ID
	
	--Get interval in months
	SET @INTERVAL = DATEDIFF(mm,@EFFECTIVE_DATE,@INCEPTION_DATE)
	--SET @TOT_DAYS = @APP_TERMS * 30 

	IF ( ( @INTERVAL / @APP_TERMS ) > 1 )
	BEGIN
		--Renewal
		RETURN 2
	END
	ELSE
	BEGIN
		--new bsuiness
		RETURN 1
	END

	------------------
	*/

	--Alternate ----
	IF EXISTS
	(
	SELECT * FROM POL_POLICY_PROCESS
	WHERE PROCESS_ID IN (5,18) AND 
		PROCESS_STATUS <> 'ROLLBACK'
		AND POLICY_ID = @POLICY_ID AND 
		CUSTOMER_ID = @CUSTOMER_ID 
		AND POLICY_VERSION_ID <= @POLICY_VERSION_ID
	)
	BEGIN
		--Renewal
		RETURN 2
	END
	ELSE
	BEGIN
		--New Business
		RETURN 1
	END
	-------------
	
	
END





GO

