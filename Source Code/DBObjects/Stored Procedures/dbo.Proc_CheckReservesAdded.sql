IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckReservesAdded]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckReservesAdded]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--DROP PROC dbo.Proc_CheckReservesAdded
--go
/*----------------------------------------------------------
Proc Name       : dbo.Proc_CheckReservesAdded
Created by      : Sumit Chhabra
Date            : 06/20/2006
Purpose        : Checks for the existence of reserve actvity
Revison History :
Used In  : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--DROP PROC dbo.Proc_CheckReservesAdded
CREATE PROC dbo.Proc_CheckReservesAdded
(
 @CLAIM_ID     int
)
AS
BEGIN
DECLARE @NEW_RESERVE INT
DECLARE @LOB_ID INT
DECLARE @AUTO_LOB INT
DECLARE @BOAT_LOB INT
DECLARE @REDW_LOB INT
DECLARE @CYCL_LOB INT
DECLARE @HOME_LOB INT
--Done for Itrack Issue 7702(Attachment 4) on 9 Aug 2010
DECLARE @LOCATION INT
DECLARE @BOAT INT
--Done till here
SET @NEW_RESERVE = 165
SET @CYCL_LOB = 3
SET @AUTO_LOB = 2
SET @BOAT_LOB = 4
SET @REDW_LOB = 6
SET @HOME_LOB = 1



 SELECT
  @LOB_ID = POLICY_LOB
 FROM
  CLM_CLAIM_INFO C
 JOIN
  POL_CUSTOMER_POLICY_LIST P
 ON
  P.CUSTOMER_ID = C.CUSTOMER_ID AND
  P.POLICY_ID = C.POLICY_ID AND
  P.POLICY_VERSION_ID = C.POLICY_VERSION_ID
 WHERE
  C.CLAIM_ID = @CLAIM_ID


 IF @LOB_ID = @AUTO_LOB OR @LOB_ID = @CYCL_LOB
 BEGIN
  IF NOT EXISTS(SELECT CLAIM_ID FROM CLM_INSURED_VEHICLE WHERE CLAIM_ID=@CLAIM_ID)
  RETURN 2
 END
 ELSE IF @LOB_ID = @BOAT_LOB
 BEGIN
  IF NOT EXISTS(SELECT CLAIM_ID FROM CLM_INSURED_BOAT WHERE CLAIM_ID=@CLAIM_ID)
  RETURN 2
 END
 ELSE IF (@LOB_ID = @REDW_LOB)--@LOB_ID = @HOME_LOB
 BEGIN
  IF NOT EXISTS(SELECT CLAIM_ID FROM CLM_INSURED_LOCATION WHERE CLAIM_ID=@CLAIM_ID)
  RETURN 2
 END
 --Done for Itrack Issue 7702(Attachment 4) on 9 Aug 2010
 ELSE IF (@LOB_ID = @HOME_LOB) 
 BEGIN
    SELECT @LOCATION = COUNT(CLAIM_ID) FROM CLM_INSURED_LOCATION WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE = 'Y'
	SELECT @BOAT = COUNT(CLAIM_ID) FROM CLM_INSURED_BOAT WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE = 'Y'
	
	IF(@LOCATION = 0 AND @BOAT = 0) 
	RETURN 2
 END

--  IF EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_REASON=@NEW_RESERVE)
  IF EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTION_ON_PAYMENT=@NEW_RESERVE)
    RETURN 1
  ELSE
    RETURN 0
END


--go
--exec Proc_CheckReservesAdded 2952
--rollback tran

GO

