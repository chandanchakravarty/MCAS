IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckUmbrellaRenewalNotice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckUmbrellaRenewalNotice]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
proc  		Proc_CheckUmbrellaRenewalNotice
created by 	:Pravesh K. Chandel
dated 		:8 june 2007
purpose		: to check umbrella Renewal Letter to be send or not
Used in 	:Wolvorine 's EOD process
*/
--drop proc dbo.Proc_CheckUmbrellaRenewalNotice
create proc dbo.Proc_CheckUmbrellaRenewalNotice
(
@CUSTOMER_ID INT,
@POLICY_ID INT,
@POLICY_VERSION_ID SMALLINT
)
as
BEGIN


DECLARE @POL_EXPIRATION_DATE DATETIME
DECLARE @POL_INCEPTION_DATE DATETIME
DECLARE @CLIENT_UPDATE_DATE DATETIME


SELECT @POL_EXPIRATION_DATE=APP_EXPIRATION_DATE,@POL_INCEPTION_DATE=APP_INCEPTION_DATE FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) 
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID

  IF (DATEDIFF(day,@POL_INCEPTION_DATE,@POL_EXPIRATION_DATE) < 3*365)
   return 1
--fetch  Client Update dates
     SELECT @CLIENT_UPDATE_DATE=CLIENT_UPDATE_DATE FROM POL_UMBRELLA_LIMITS  WITH(NOLOCK) 
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
   if (@CLIENT_UPDATE_DATE is null) 
	return 1
   IF (DATEDIFF(day,@CLIENT_UPDATE_DATE,@POL_EXPIRATION_DATE) < 3*365)
  	return 1

return 2
END






GO

