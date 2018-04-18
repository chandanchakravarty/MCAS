
ALTER PROCEDURE [dbo].[Proc_SendMail]
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @EMAIL_FROM NVARCHAR(100)
	DECLARE @EMAIL_TO NVARCHAR(MAX)
	DECLARE @EMAIL_BODY NVARCHAR(MAX)
	DECLARE @IDEN_ROW_ID INT
	DECLARE @SUBJECT NVARCHAR(500)
	DECLARE @TRIGGER_POINT NVARCHAR(500)
	DECLARE @EMAIL_TO_RESENT NVARCHAR(2000)
	DECLARE @SENT_STATUS NVARCHAR(2)
	DECLARE @USER_ID INT
	DECLARE @ERROR_NUMBER INT
	DECLARE @ERROR_SEVERITY INT
	DECLARE @ERROR_STATE INT
	DECLARE @ERROR_PROCEDURE VARCHAR(512)
	DECLARE @ERROR_LINE INT
	DECLARE @ERROR_MESSAGE NVARCHAR(MAX)
	DECLARE @ERROR_MESSAGE_1 NVARCHAR(MAX)
	DECLARE @profile_name_val VARCHAR(100)

	SET @SUBJECT = (
			SELECT EmailSubject
			FROM dbo.MNT_SYS_PARAMS
			)

	IF EXISTS (
			SELECT TOP 1 1
			FROM POL_EMAIL_SPOOL(NOLOCK)
			WHERE ISNULL(SENT_STATUS, 'N') IN (
					'N'
					,'R'
					)
			)
	BEGIN
		SET @EMAIL_TO = (
				SELECT SUBSTRING((
							SELECT ';' + ME.EMAIL
							FROM MNT_EMAIL ME WITH (NOLOCK)
							WHERE ISNULL(IsActive, 'N') = 'Y'
							ORDER BY ME.EMAIL
							FOR XML PATH('')
							), 2, 200000)
				)

		IF (ISNULL(@EMAIL_TO, '') <> '')
		BEGIN --BEGIN IF
			CREATE TABLE #TEMP_POL_EMAIL_SPOOL (
				RECORD_ID INT IDENTITY(1, 1)
				,EMAIL_BODY NVARCHAR(MAX)
				,IDEN_ROW_ID INT
				,FUNCTIONALITY_ID INT
				,EMAIL_TO NVARCHAR(2000)
				,SENT_STATUS NVARCHAR(2)
				)

			INSERT INTO #TEMP_POL_EMAIL_SPOOL (
				EMAIL_BODY
				,IDEN_ROW_ID
				,EMAIL_TO
				,SENT_STATUS
				)
			SELECT TOP 20 EMAIL_TEXT
				,INDEN_ROW_ID
				,EMAIL_TO
				,ISNULL(SENT_STATUS, 'N')
			FROM POL_EMAIL_SPOOL(NOLOCK)
			WHERE ISNULL(SENT_STATUS, 'N') IN (
					'N'
					,'R'
					)
			ORDER BY INDEN_ROW_ID ASC

			DECLARE @COUTER BIGINT

			SET @COUTER = 1

			DECLARE @MAX_RECORDS BIGINT

			SET @MAX_RECORDS = 0

			SELECT @MAX_RECORDS = MAX(RECORD_ID)
			FROM #TEMP_POL_EMAIL_SPOOL

			WHILE (@COUTER <= @MAX_RECORDS)
			BEGIN
				BEGIN TRY
					SET @EMAIL_BODY = NULL
					SET @IDEN_ROW_ID = NULL

					SELECT @EMAIL_FROM = (
							SELECT FromUserEmailId
							FROM dbo.MNT_SYS_PARAMS
							)
						,@EMAIL_BODY = EMAIL_BODY
						,@IDEN_ROW_ID = IDEN_ROW_ID
						,@EMAIL_TO_RESENT = EMAIL_TO
						,@SENT_STATUS = SENT_STATUS
					FROM #TEMP_POL_EMAIL_SPOOL WITH (NOLOCK)
					WHERE RECORD_ID = @COUTER

					SET @EMAIL_TO = CASE 
							WHEN @SENT_STATUS = 'R'
								THEN @EMAIL_TO_RESENT
							ELSE @EMAIL_TO
							END
					SET @profile_name_val = (
							SELECT EmailProfile
							FROM dbo.MNT_SYS_PARAMS
							)

					--*****************************SEND EMAIL *************************************************
					EXEC msdb.dbo.sp_send_dbmail @profile_name = @profile_name_val
						,@recipients = @EMAIL_TO
						,@subject = @SUBJECT
						,--@SUBJECT,       
						@body = @EMAIL_BODY
						,@body_format = 'HTML';

					--*****************************UPDATE EMAIL SPOOL TABLE *************************************************   
					UPDATE POL_EMAIL_SPOOL
					SET SENT_TIME = CASE 
							WHEN ISNULL(SENT_STATUS, 'N') <> 'R'
								THEN GETDATE()
							ELSE SENT_TIME
							END
						,SENT_STATUS = 'Y'
						,EMAIL_FROM = @EMAIL_FROM
						,EMAIL_TO = CASE 
							WHEN ISNULL(SENT_STATUS, 'N') <> 'R'
								THEN @EMAIL_TO
							ELSE EMAIL_TO
							END
						,REMARK = CASE 
							WHEN ISNULL(SENT_STATUS, 'N') <> 'R'
								THEN 'Email have been send successfully'
							ELSE REMARK + ' : Email have been send successfully at ' + cast(GETDATE() AS NVARCHAR(25))
							END
					WHERE INDEN_ROW_ID = @IDEN_ROW_ID

					SET @COUTER = @COUTER + 1
				END TRY

				BEGIN CATCH
					SELECT @ERROR_NUMBER = ERROR_NUMBER()
						,@ERROR_SEVERITY = ERROR_SEVERITY()
						,@ERROR_STATE = ERROR_STATE()
						,@ERROR_PROCEDURE = ERROR_PROCEDURE()
						,@ERROR_LINE = ERROR_LINE()
						,@ERROR_MESSAGE = ERROR_MESSAGE()

					SET @ERROR_MESSAGE_1 = 'Error Occured :' + isnull(@ERROR_PROCEDURE, '') + ' Error Severity :' + convert(NVARCHAR, isnull(@ERROR_SEVERITY, '')) + ' Error State:' + convert(NVARCHAR, isnull(@ERROR_STATE, '')) + ' Error Line Number:' + convert(NVARCHAR, isnull(@ERROR_LINE, '')) + + ' Error Description :' + isnull(@ERROR_MESSAGE, '')

					EXEC dbo.Proc_InsertExceptionLog @exceptiondesc = @ERROR_MESSAGE_1
						,@customer_id = NULL
						,@app_id = NULL
						,@app_version_id = NULL
						,@policy_id = NULL
						,@policy_version_id = NULL
						,@claim_id = NULL
						,@qq_id = NULL
						,@source = @ERROR_PROCEDURE
						,@message = @ERROR_MESSAGE_1
						,@class_name = NULL
						,@method_name = NULL
						,@query_string_params = NULL
						,@system_id = NULL
						,@user_id = @USER_ID
						,@lob_id = NULL
						,@exception_type = 'SqlException'

					UPDATE POL_EMAIL_SPOOL
					SET SENT_STATUS = 'N'
						,REMARK = 'Error while Email send '
						,ERROR_DESCRIPTION = @ERROR_MESSAGE_1
					WHERE INDEN_ROW_ID = @IDEN_ROW_ID
				END CATCH
			END --END LOOP BEGIN

			DROP TABLE #TEMP_POL_EMAIL_SPOOL
		END --END IF BEGIN @EMAIL_TO
	END --END IF SPOOL
END --START BEGIN END



GO


