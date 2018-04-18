IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_StartEODProcess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_StartEODProcess]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/***********************************************************
CREATE BY		: Vijay Kumar Joshi
CREATE DATE		: 25 Nov 2005, 12:52 AM
PURPOSE			: Starts the process and perform certain
				activity, and logs the status of activity 
				in EOD_PROCESS table

REVISION HISTORY
Review By	Review Date		Purpose

************************************************************/
CREATE PROCEDURE dbo.Proc_StartEODProcess
(
	@ACTIVITY	VARCHAR(30),	--Activity to perform for the process
	@EOD_DATE	DATETIME,		--Process date time
	@STARTED_BY	SMALLINT		--Process started by (either by system or by any user)
)
AS
BEGIN
	
	SET NOCOUNT ON

	DECLARE @PROCESS_ID INT, @CREATE_DATE DATETIME
	SELECT @CREATE_DATE = GETDATE()

	IF @ACTIVITY = 'SUSPENSE_TO_NORMAL'
	BEGIN

		--Loging the process detail
		EXEC @PROCESS_ID = Proc_InsertACT_EOD_PROCESS 1, @ACTIVITY, 
			'Moving suspense payment to normal payment', 
			@CREATE_DATE, NULL, 'S', @STARTED_BY
		
		IF @@ERROR <> 0
			GOTO ERRHANDLER

		--Moving the suspense payment to normal payment
		EXEC DBO.Proc_MoveSuspensePaymentToNormalPayment @EOD_DATE

		IF @@ERROR <> 0
			GOTO ERRHANDLER
		

		--Updating the status of activity to completed
		UPDATE ACT_EOD_PROCESS
		SET STATUS = 'OK'
		WHERE PROCESS_ID = @PROCESS_ID
	END
	ELSE IF @ACTIVITY = 'PREMIUM_NOTICE'
	BEGIN
		
		--Loging the process detail
		EXEC @PROCESS_ID = Proc_InsertACT_EOD_PROCESS 1, @ACTIVITY, 
			'Printing premium notice.', 
			@CREATE_DATE, NULL, 'S', @STARTED_BY
		
		IF @@ERROR <> 0
			GOTO ERRHANDLER

		--Moving the suspense payment to normal payment
		EXEC DBO.Proc_PremiumNotice @EOD_DATE

		IF @@ERROR <> 0
			GOTO ERRHANDLER

		--Updating the status of activity to completed
		UPDATE ACT_EOD_PROCESS
		SET STATUS = 'OK'
		WHERE PROCESS_ID = @PROCESS_ID
	END	

	SET NOCOUNT OFF
	RETURN 1

	ERRHANDLER:
	SET NOCOUNT OFF
	RETURN 0		
END


GO

