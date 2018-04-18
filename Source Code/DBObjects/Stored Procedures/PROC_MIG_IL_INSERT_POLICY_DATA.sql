
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<ATUL KUMAR SINGH>
-- Create date: <2011-08-23>
-- Description:	<insert data in all ebix respective tables from Initial Load table for file '011800001APOLICE.xlsx'>
-- =============================================
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_DATA] 
		
AS
BEGIN
	
	

    SELECT * FROM dbo.MIG_IL_POLICY_DETAILS
	
END
GO
