
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNT_FileUploadListSearch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNT_FileUploadListSearch]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  
  
  
  
create Proc [dbo].[Proc_GetMNT_FileUploadListSearch]  
(  
@query nvarchar(max)  
)  
as  
BEGIN  
  
IF OBJECT_ID('tempdb..#TempTable') IS NOT NULL DROP TABLE #TempTable  
SET FMTONLY OFF;  
create table #TempTable  
(   [FileId][int] NULL,
    [FileRefNo] [nvarchar](50) NOT NULL,
	[FileName] [nvarchar](100) NOT NULL,
	[FileType] [nvarchar](10) NOT NULL,
	[UploadType] [nvarchar](10) NOT NULL,
	[UploadPath] [nvarchar](100) NOT NULL,
	[UploadedDate] [datetime] NOT NULL,
	[TotalRecords] [int] NULL,
	[SuccessRecords] [int] NULL,
	[FailedRecords] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[Is_Processed] [varchar](1) NULL,
	[Processed_Date] [datetime] NULL,
	[Is_Active] [varchar](1) NULL,
	[HasError] [varchar](50) NULL,
	[CreatedBy] [varchar](50) NULL
)  
exec (@query)  
  
SELECT FileId ,FileRefNo ,FileName ,FileType ,UploadType ,UploadPath ,UploadedDate ,TotalRecords ,SuccessRecords ,FailedRecords ,Status ,Is_Processed ,Processed_Date ,Is_Active ,HasError ,CreatedBy from #TempTable  
  
END  
  
  
  
  
  

GO


