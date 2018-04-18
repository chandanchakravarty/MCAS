IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateCoverageCatagory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateCoverageCatagory]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.Proc_ActivateDeactivateCoverageCatagory      
CREATED BY 	: Swarup 
CREATED DATE	: August 09, 2007   
Purpose         : To activate/deactivate the record in MNT_REINSURANCE_COVERAGE_CATEGORY table      
Revison History :      
Used In         : Wolvorine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC dbo.Proc_ActivateDeactivateCoverageCatagory      
(      
 @COVERAGE_CATEGORY_ID int,      
 @IS_ACTIVE  Char(1)      
)      
AS      
BEGIN      
    
 UPDATE MNT_REINSURANCE_COVERAGE_CATEGORY  SET   IS_ACTIVE = @IS_ACTIVE      
 WHERE  COVERAGE_CATEGORY_ID= @COVERAGE_CATEGORY_ID      
END      
      
      
    
  



GO

