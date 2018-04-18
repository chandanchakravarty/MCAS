IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetREINSURANCE_COVERAGE_CATEGORY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetREINSURANCE_COVERAGE_CATEGORY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : Proc_GetREINSURANCE_COVERAGE_CATEGORY    
Created by      : Swarup      
Date            : 9-Aug-2007     
Purpose     : Get values from  MNT_REINSURANCE_COVERAGE_CATEGORY table      
Revison History :      
Used In  : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
  
-- drop proc dbo.Proc_GetREINSURANCE_COVERAGE_CATEGORY     
CREATE PROC dbo.Proc_GetREINSURANCE_COVERAGE_CATEGORY       
(      
 @COVERAGE_CATEGORY_ID      int      
)      
AS      
BEGIN      
 SELECT      
 	COVERAGE_CATEGORY_ID,  
	convert(varchar,EFFECTIVE_DATE,101) as EFFECTIVE_DATE ,  
 	LOB_ID,  
 	CATEGORY,  
 	IS_ACTIVE  
    
 FROM 
	MNT_REINSURANCE_COVERAGE_CATEGORY  
 WHERE       
  	COVERAGE_CATEGORY_ID = @COVERAGE_CATEGORY_ID      
      
END      
      
      


GO

