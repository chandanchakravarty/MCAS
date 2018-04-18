IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertPremiumSubDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertPremiumSubDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------------------  
Procedure Name	: dbo.InsertPremiumSubDetails  
Created By  	: Ravindra
Created Date 	: 08-02-2007
Purpose   		: Inserting record in ACT_PREMIUM_PROCESS_SUB_DETAILS TAble
Revision History  
Revision Date  
Purpose  
-----------------------------------------------------------------------*/  
-- drop proc dbo.InsertPremiumSubDetails
CREATE PROCEDURE dbo.InsertPremiumSubDetails
(  
	@CUSTOMER_ID			Int, 
	@POLICY_ID				Int,
	@POLICY_VERSION_ID		Int,  
	@PPD_ROW_ID				Int,
	@RISK_ID				Int, 
	@RISK_TYPE				Char(10), 
	@NET_PREMIUM			Decimal(25,2), 
	@STATS_FEES				Decimal(18,2),
	@GROSS_PREMIUM			Decimal(25,2),
	@INFORCE_PREMIUM		Decimal(25,2),
	@INFORCE_FEES			Decimal(18,2)
)
As
BEGIN 
INSERT INTO ACT_PREMIUM_PROCESS_SUB_DETAILS
			(CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID , PPD_ROW_ID,
				RISK_ID , RISK_TYPE , NET_PREMIUM , STATS_FEES ,
				GROSS_PREMIUM ,
				 INFORCE_PREMIUM , INFORCE_FEES )
VALUES ( @CUSTOMER_ID , @POLICY_ID , @POLICY_VERSION_ID , @PPD_ROW_ID,
				@RISK_ID , @RISK_TYPE , @NET_PREMIUM , @STATS_FEES ,
				@GROSS_PREMIUM ,
				 @INFORCE_PREMIUM  , @INFORCE_FEES )

END









GO

