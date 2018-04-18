 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CoverageByCoverageID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CoverageByCoverageID]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_CoverageByCoverageID]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   




CREATE PROC dbo.Proc_CoverageByCoverageID
(
	@COV_ID int
)

AS

SELECT * FROM MNT_COVERAGE where COV_ID = @COV_ID
