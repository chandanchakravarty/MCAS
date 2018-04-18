IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_TransLog_1099Check_Details]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_TransLog_1099Check_Details]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Proc_Insert_TransLog_1099Check_Details]
(
 @FORM_1099_ID int,
 @TRANS_ID varchar(20) 
)
as
Insert into CHECK_DETAILS_1099 
select @FORM_1099_ID ,@TRANS_ID,getdate(),'T'




GO

