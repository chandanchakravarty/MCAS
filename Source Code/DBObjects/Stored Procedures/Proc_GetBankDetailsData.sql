IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetBankDetailsData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetBankDetailsData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pradeep Kushwaha>
-- Create date: <23-Sep-2011>
-- Description:	<Get Bank information details data>
-- =============================================
CREATE Proc dbo.Proc_GetBankDetailsData
	-- Add the parameters for the stored procedure here
	@SearchData  nvarchar(50),
	@CalledFor nvarchar(50) ,
	@SearchFor nvarchar(50)=null ,
	@Calledfrom nvarchar(50)=null 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	IF(UPPER(@CalledFor)='BANK' and UPPER(@Calledfrom)='BANKNAME')
	BEGIN
		 SELECT DISTINCT  BANK_NAME AS SEARCHDATA FROM MNT_BANK_INFO_MASTER WITH(NOLOCK) WHERE BANK_NAME LIKE '%'+@SEARCHDATA+'%' 
		 OPTION (FAST 10)
	END
	ELSE IF(UPPER(@CalledFor)='BANK' and UPPER(@Calledfrom)='BANKNUMBER')
	BEGIN
		 SELECT DISTINCT  BANK_NUMBER AS SEARCHDATA FROM MNT_BANK_INFO_MASTER WITH(NOLOCK) WHERE BANK_NAME LIKE '%'+@SEARCHDATA+'%' 
		 OPTION (FAST 10)
	END
	ELSE  IF(UPPER(@CalledFor)='BANK' and UPPER(@Calledfrom)='BRANCHNUMBER')
	BEGIN
		 SELECT DISTINCT  BANK_BRANCH_NUMBER AS SEARCHDATA FROM MNT_BANK_INFO_MASTER WITH(NOLOCK) WHERE BANK_NAME LIKE '%'+@SEARCHDATA+'%' 
		 OPTION (FAST 10)
	END
	ELSE  IF(UPPER(@CalledFor)='VALIDATATION' and UPPER(@Calledfrom)='BANKNAME' )
	BEGIN
		 SELECT DISTINCT  BANK_NAME AS SEARCHDATA FROM MNT_BANK_INFO_MASTER WITH(NOLOCK) WHERE BANK_NAME=@SearchFor
		 OPTION (FAST 10)
	END
	ELSE  IF(UPPER(@CalledFor)='VALIDATATION' and UPPER(@Calledfrom)='BANKNUMBER' )
	BEGIN
		 SELECT DISTINCT  BANK_NUMBER AS SEARCHDATA FROM MNT_BANK_INFO_MASTER WITH(NOLOCK) WHERE BANK_NAME=@SEARCHDATA  and BANK_NUMBER=@SearchFor
		 OPTION (FAST 10)
	END
	ELSE  IF(UPPER(@CalledFor)='VALIDATATION' and UPPER(@Calledfrom)='BRANCHNUMBER' )
	BEGIN
		 SELECT DISTINCT  BANK_BRANCH_NUMBER AS SEARCHDATA FROM MNT_BANK_INFO_MASTER WITH(NOLOCK) WHERE BANK_NAME=@SEARCHDATA  and BANK_BRANCH_NUMBER=@SearchFor
		 OPTION (FAST 10)
	END
 

END
GO
