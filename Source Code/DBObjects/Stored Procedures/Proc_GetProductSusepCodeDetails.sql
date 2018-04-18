IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetProductSusepCodeDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetProductSusepCodeDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pradeep Kushwaha>
-- Create date: <19-Aug-2011>
-- Description:	<Get the product SUSEP Code details >
-- =============================================
CREATE Proc dbo.Proc_GetProductSusepCodeDetails
	-- Add the parameters for the stored procedure here
	@LOB_SUSEPCODE_ID int=NULL,
	@LOB_ID smallint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	 
	SELECT LOB_SUSEPCODE_ID ,LOB_ID,
		EFFECTIVE_FROM,EFFECTIVE_TO,SUSEP_LOB_CODE
	FROM MNT_LOB_SUSEPCODE_MASTER WITH(NOLOCK) 
	WHERE LOB_ID=@LOB_ID 
		  AND 
		 (@LOB_SUSEPCODE_ID IS NULL 
			OR 
		  LOB_SUSEPCODE_ID=@LOB_SUSEPCODE_ID
		  )
END
GO
