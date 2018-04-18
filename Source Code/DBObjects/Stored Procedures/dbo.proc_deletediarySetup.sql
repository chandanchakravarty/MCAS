IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_deletediarySetup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_deletediarySetup]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
CREATED BY	: ANURAG VERMA
CREATED ON	: 05/03/2007
PURPOSE		: tO DELETE DIARY DETAILS ACCORDING TO MODULE AND DIARY TYPE BEFORE INSERTION
*/


CREATE          procedure proc_deletediarySetup    
(    
@MDD_DIARYTYPE_ID  numeric(9) ,    
@MDD_MODULE_ID int

)    
as    

BEGIN    
DELETE FROM MNT_DIARY_DETAILS WHERE MDD_MODULE_ID=@MDD_MODULE_ID AND MDD_DIARYTYPE_ID=@MDD_DIARYTYPE_ID
     
END    
  





GO

