IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Fetch_FollowUp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Fetch_FollowUp]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


/*
Created By	:	Anurag Verma
Created	On	:	20/03/2007
Purpose		:	fetch those diary types which are categorised as Follow Up
*/

CREATE PROC dbo.Proc_Fetch_FollowUp
@type_flag nchar(1)='D'
AS
BEGIN
Select typeid,typedesc from todolisttypes where type_flag=@type_flag 
END







GO

