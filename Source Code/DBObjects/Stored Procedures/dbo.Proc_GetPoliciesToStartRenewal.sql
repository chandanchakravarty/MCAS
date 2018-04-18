IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPoliciesToStartRenewal]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPoliciesToStartRenewal]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************************************************
Proc  		: dbo.Proc_GetPoliciesToStartRenewal
Created By 	: Ravindra Gupta
Date 		: 
Purpose		: 
Used in 	: Wolvorine 's EOD process

*********************************************************************************************/
-- drop proc dbo.Proc_GetPoliciesToStartRenewal
CREATE PROC dbo.Proc_GetPoliciesToStartRenewal
AS
BEGIN
DECLARE @CUSTOMER_ID INT
DECLARE @POLICY_ID INT 
DECLARE @POLICY_VERSION_ID INT 
DECLARE @ELIGIBLE INT
DECLARE @POLICY_STATUS nvarchar(15)
DECLARE @UMBRELLA_LOB smallint
DECLARE @POLICY_NORMAL nvarchar(15)
DECLARE @POLICY_LOB smallint
DECLARE @RENEWAL_LAUNCH smallint,
		@FROM_AS400 Char(1)

SET @UMBRELLA_LOB=5
SET @POLICY_NORMAL='NORMAL'
SET @RENEWAL_LAUNCH = 5 

CREATE TABLE #POLICIES_TO_RENEW
(
	IDEN_ROW_ID 		int identity(1,1),
	CUSTOMER_ID		int	,
	POLICY_ID		int	,
	POLICY_VERSION_ID 	smallint ,
	STATUS			nvarchar(20),
	LOB_ID 			Int,
	FROM_AS400		Char(1)
)

DECLARE CR CURSOR FOR 
SELECT  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_STATUS,POLICY_LOB ,
			ISNULL(FROM_AS400, 'N') 
FROM POL_CUSTOMER_POLICY_LIST 
WHERE 
DATEDIFF(DAY,GETDATE(),APP_EXPIRATION_DATE)<= CASE POLICY_LOB WHEN 5 THEN 60 ELSE 45 END 
--Ravindra(10-05-2007) Expired policies will not be renewed by EOD
AND DATEDIFF(DAY,GETDATE(),APP_EXPIRATION_DATE)>=0
AND POLICY_STATUS IN(@POLICY_NORMAL)

OPEN CR
FETCH NEXT FROM CR INTO @CUSTOMER_ID,@POLICY_ID ,@POLICY_VERSION_ID,@POLICY_STATUS,@POLICY_LOB,
						@FROM_AS400
WHILE @@FETCH_STATUS=0
begin
	IF(@FROM_AS400 <> 'Y' )
	BEGIN 
		SET @ELIGIBLE=0
		EXEC Proc_CheckProcessEligibility @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@RENEWAL_LAUNCH,@ELIGIBLE out
	END
	ELSE
	--If policy is from AS400 conversion and corrective user process is pending a diary entry will be created
	BEGIN 
		SET @ELIGIBLE=0
	END
	IF (@ELIGIBLE=1)
	BEGIN
		INSERT INTO #POLICIES_TO_RENEW( CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,STATUS,LOB_ID , FROM_AS400)
		VALUES
		(@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@POLICY_STATUS,@POLICY_LOB , @FROM_AS400 )
	END	
    FETCH next from cr into @CUSTOMER_ID,@POLICY_ID ,@POLICY_VERSION_ID,@POLICY_STATUS,@POLICY_LOB ,@FROM_AS400
end

CLOSE cr
DEALLOCATE cr

SELECT CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID ,STATUS ,LOB_ID  , FROM_AS400
FROM #POLICIES_TO_RENEW 

DROP TABLE #POLICIES_TO_RENEW

END












GO

