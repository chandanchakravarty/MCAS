IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteProductSusepCodeDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteProductSusepCodeDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pradeep Kushwaha>
-- Create date: <19-Aug-2011>
-- Description:	<Delete the product SUSEP Code details >
-- =============================================
CREATE Proc dbo.Proc_DeleteProductSusepCodeDetails
	-- Add the parameters for the stored procedure here
	@LOB_SUSEPCODE_ID INT,
	@LOB_ID SMALLINT 
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
DELETE FROM MNT_LOB_SUSEPCODE_MASTER WHERE LOB_SUSEPCODE_ID=@LOB_SUSEPCODE_ID AND LOB_ID=@LOB_ID
 
	
END
GO
