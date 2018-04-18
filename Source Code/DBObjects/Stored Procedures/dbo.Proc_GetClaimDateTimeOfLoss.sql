IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimDateTimeOfLoss]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimDateTimeOfLoss]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetClaimDateTimeOfLoss  
Created by      : Vijay Arora  
Date            : 5/4/2006  
Purpose     	: To get the date and time of the claim.
Revison History :  
Used In  	: Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetClaimDateTimeOfLoss  
(  
	@CLAIM_ID int  
)  
AS  
BEGIN  
	SELECT CONVERT(VARCHAR(10),LOSS_DATE,101) AS LOSS_DATE, 
	LOSS_DATE AS LOSS_TIME, LOSS_TIME_AM_PM
	FROM CLM_CLAIM_INFO WHERE CLAIM_ID = @CLAIM_ID
END  
  



GO

