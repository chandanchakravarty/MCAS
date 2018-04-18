IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spVendorByState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spVendorByState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spVendorByState] 

@VendorState varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT VendorId,VendorFName,VendorLName,VendorCity,VendorState,VendorCountry,PostedDate,VendorDescription
          FROM Vendor Where VendorState = @VendorState ORDER BY PostedDate

END

GO

