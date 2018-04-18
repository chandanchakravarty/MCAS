IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateProductSusepCodeDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateProductSusepCodeDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pradeep Kushwaha>
-- Create date: <19-Aug-2011>
-- Description:	<Update the product SUSEP Code details >
-- =============================================
CREATE Proc dbo.Proc_UpdateProductSusepCodeDetails
	-- Add the parameters for the stored procedure here
	@LOB_SUSEPCODE_ID INT,
	@LOB_ID SMALLINT,
	@EFFECTIVE_FROM DATETIME ,
	@EFFECTIVE_TO DATETIME ,
	@SUSEP_LOB_CODE NVARCHAR(10),
	@MODIFIED_BY INT,
	@LAST_UPDATED_DATETIME DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
UPDATE MNT_LOB_SUSEPCODE_MASTER                                              
  SET     
 	EFFECTIVE_FROM=@EFFECTIVE_FROM,
	EFFECTIVE_TO=@EFFECTIVE_TO,
	SUSEP_LOB_CODE=@SUSEP_LOB_CODE,
	MODIFIED_BY=@MODIFIED_BY,
	LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME

 WHERE   
  LOB_ID =@LOB_ID AND
  LOB_SUSEPCODE_ID=@LOB_SUSEPCODE_ID

RETURN 1 	
END
GO
