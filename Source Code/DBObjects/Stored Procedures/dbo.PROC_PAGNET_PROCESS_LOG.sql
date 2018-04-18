IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_PAGNET_PROCESS_LOG]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_PAGNET_PROCESS_LOG]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--select * from PAGNET_PROCESS_LOG


--drop PROC PROC_PAGNET_PROCESS_LOG
CREATE PROC [dbo].[PROC_PAGNET_PROCESS_LOG]
 @PROCESS_TYPE  NVARCHAR(100)=null,
 @ACTIVITY_DESCRIPTION [nvarchar](1000)=null,
 @START_DATETIME DATETIME =null,
 @END_DATETIME DATETIME=null,
 @STATUS [nvarchar](20)=null,
 @ADDITIONAL_INFO [nvarchar](2000)=null
 AS
 BEGIN
 
    INSERT INTO PAGNET_PROCESS_LOG
    (
		[PROCESS_TYPE] ,
		[ACTIVITY_DESCRIPTION],
		[START_DATETIME] ,
		[END_DATETIME] ,
		[STATUS],	
		[ADDITIONAL_INFO]
	)
	VALUES
	(
		@PROCESS_TYPE  ,
		@ACTIVITY_DESCRIPTION ,
		GETDATE(),
		GETDATE(),
		@STATUS ,
		@ADDITIONAL_INFO 	
	)
	
 END
GO

