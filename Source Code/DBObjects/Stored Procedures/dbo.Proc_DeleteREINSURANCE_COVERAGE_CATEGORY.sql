IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteREINSURANCE_COVERAGE_CATEGORY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteREINSURANCE_COVERAGE_CATEGORY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : Dbo.Proc_DeleteREINSURANCE_COVERAGE_CATEGORY  
CREATED BY 	: Swarup 
CREATED DATE	: August 09, 2007  
Purpose     : Delete records from MNT_REINSURANCE_COVERAGE_CATEGORY table  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc Proc_DeleteREINSURANCE_COVERAGE_CATEGORY  
CREATE PROC Dbo.Proc_DeleteREINSURANCE_COVERAGE_CATEGORY  
(  
 @COVERAGE_CATEGORY_ID  int  
)  
AS  
BEGIN  
  
 DELETE FROM MNT_REINSURANCE_COVERAGE_CATEGORY   
  WHERE COVERAGE_CATEGORY_ID = @COVERAGE_CATEGORY_ID 
 return 1  
   
END  
  
  
  
  
  
  
  
  
  
  
  
  
  



GO

