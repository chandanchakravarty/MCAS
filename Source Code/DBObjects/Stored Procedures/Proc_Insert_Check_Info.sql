/* =============================================                          
Proc Name       : dbo.Proc_Insert_Check_Info                            
Created by      : Shikha Chourasiya                            
Date            : 10-11-2011                                                     
Used In			: Ebix Advantage                            
============================================= */

-- Drop stored procedure if it already exists
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_Check_Info]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_Check_Info]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.Proc_Insert_Check_Info
(
	@CHECKTYPE VARCHAR(6), 
    @ISSUINGBANK INT, 
    @CONSOLIDATEATLEVEL VARCHAR(10), 
    @USER INT, 
    @REFUNDITEMSLIST AS dbo.SAMPLE_ACT_CHECK_INFORMATION READONLY
)
AS
BEGIN
--CODE FOR INSERT AND/OR UPDATE TABLE
SELECT * FROM @REFUNDITEMSLIST
END	
GO


