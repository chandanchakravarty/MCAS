IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAllCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAllCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*============================================================================================  
Proc Name       : dbo.Proc_DeleteAllCoverages
Created by      : Ravindra
Date            : 04-17-2007
Purpose       	: To delete all coverages for specific policy
Revison History :      
MODIFIED BY		: PRAVESH K CHANDEL
MODIFIED DATE	: 17 APRIL 2008
PURPOSE			: ALSO DELETE RELATED ENDORSEMENT OF THE COVERAGE

Used In    : Wolverine/clsRewriteProcess      
===============================================================================================  
Date     Review By          Comments      
====== ==============  =========================================================================  */
--drop proc   dbo.Proc_DeleteAllCoverages
create proc dbo.Proc_DeleteAllCoverages
(
	@CUSTOMER_ID 		Int,
	@POLICY_ID		Int,
	@POLICY_VERSION_ID	Int,
	@LOB_ID			Int
)
AS 
BEGIN 
--Home / Rental
IF(@LOB_ID = 1 OR @LOB_ID = 6)
BEGIN 
	DELETE FROM POL_DWELLING_SECTION_COVERAGES
	WHERE CUSTOMER_ID = @CUSTOMER_ID 
	AND   POLICY_ID   = @POLICY_ID
	AND   POLICY_VERSION_ID  = @POLICY_VERSION_ID

  DELETE FROM POL_DWELLING_ENDORSEMENTS
	WHERE CUSTOMER_ID = @CUSTOMER_ID 
	AND   POLICY_ID   = @POLICY_ID
	AND   POLICY_VERSION_ID  = @POLICY_VERSION_ID

    
END

-- Auto /Motor
IF(@LOB_ID = 2 OR @LOB_ID = 3)
BEGIN 
	DELETE FROM POL_VEHICLE_COVERAGES
	WHERE CUSTOMER_ID = @CUSTOMER_ID 
	AND   POLICY_ID   = @POLICY_ID
	AND   POLICY_VERSION_ID  = @POLICY_VERSION_ID	

	DELETE FROM POL_VEHICLE_ENDORSEMENTS
	WHERE CUSTOMER_ID = @CUSTOMER_ID 
	AND   POLICY_ID   = @POLICY_ID
	AND   POLICY_VERSION_ID  = @POLICY_VERSION_ID
END
-- Watercraft
IF(@LOB_ID = 4)
BEGIN 
	DELETE FROM POL_WATERCRAFT_COVERAGE_INFO
	WHERE CUSTOMER_ID = @CUSTOMER_ID 
	AND   POLICY_ID   = @POLICY_ID
	AND   POLICY_VERSION_ID  = @POLICY_VERSION_ID	

	DELETE FROM POL_WATERCRAFT_ENDORSEMENTS
	WHERE CUSTOMER_ID = @CUSTOMER_ID 
	AND   POLICY_ID   = @POLICY_ID
	AND   POLICY_VERSION_ID  = @POLICY_VERSION_ID

END

END



GO

