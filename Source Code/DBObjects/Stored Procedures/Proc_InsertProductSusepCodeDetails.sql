IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertProductSusepCodeDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertProductSusepCodeDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pradeep Kushwaha>
-- Create date: <19-Aug-2011>
-- Description:	<Insert the product SUSEP Code details >
-- =============================================
CREATE Proc dbo.Proc_InsertProductSusepCodeDetails
	-- Add the parameters for the stored procedure here
	@LOB_SUSEPCODE_ID INT OUT,
	@LOB_ID SMALLINT,
	@EFFECTIVE_FROM DATETIME ,
	@EFFECTIVE_TO DATETIME ,
	@SUSEP_LOB_CODE NVARCHAR(10),
	@CREATED_BY       INT,      
	@CREATED_DATETIME  DATETIME  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SELECT @LOB_SUSEPCODE_ID =ISNULL(MAX(LOB_SUSEPCODE_ID),0)+1  FROM MNT_LOB_SUSEPCODE_MASTER WITH(NOLOCK)
      
 INSERT INTO MNT_LOB_SUSEPCODE_MASTER      
 (      
	LOB_SUSEPCODE_ID,
	LOB_ID,
	EFFECTIVE_FROM,
	EFFECTIVE_TO,
	SUSEP_LOB_CODE,
	CREATED_BY,
	CREATED_DATETIME
 )      
 VALUES      
 (      
    @LOB_SUSEPCODE_ID,
	@LOB_ID,
	@EFFECTIVE_FROM,
	@EFFECTIVE_TO,
	@SUSEP_LOB_CODE,
	@CREATED_BY,
	@CREATED_DATETIME   
 )    
  RETURN 1   
	
END
GO
