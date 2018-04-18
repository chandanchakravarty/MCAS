IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUTL_RECEIVED_XML_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUTL_RECEIVED_XML_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE Proc_InsertUTL_RECEIVED_XML_LIST
(
	@RECV_FILE_PATH		varchar(255),
	@RECV_FILE_NAME		varchar(255),
	@RECV_FILE_EXTN		varchar(10)	=	null,
	@RECORD_DATE		datetime	=	null
)
AS
BEGIN
	declare		@maxId	int;
	select	@maxId = isnull(max(RECEIVING_ID),0)+1 from UTL_RECEIVED_XML_LIST

	if(@RECORD_DATE is null)
	begin
		set @RECORD_DATE	=	GETDATE()
	end

	if(@RECV_FILE_EXTN is null)
	begin
		set @RECV_FILE_EXTN	=	'xml'
	end

	insert into UTL_RECEIVED_XML_LIST
	(
		RECEIVING_ID,
		UPLOADED_FILE_NAME,
		FULL_FILE_NAME,
		IS_VALID,
		RECORD_DATE
	)
	values
	(
		@maxId,
		@RECV_FILE_NAME + '.' + @RECV_FILE_EXTN,
		@RECV_FILE_NAME + '_' + convert(varchar,@maxId) + '.' + @RECV_FILE_EXTN,
		0,
		@RECORD_DATE
	)
	return	@maxId
END



GO

