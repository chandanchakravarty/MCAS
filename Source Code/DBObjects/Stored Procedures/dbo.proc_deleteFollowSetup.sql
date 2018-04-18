IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_deleteFollowSetup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_deleteFollowSetup]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



/*
CREATED BY	: ANURAG VERMA
CREATED ON	: 20/03/2007
PURPOSE		: To DELETE DIARY DETAILS ACCORDING TO MODULE AND lob id for follow up BEFORE INSERTION
*/


CREATE          procedure proc_deleteFollowSetup    
(    
@MDD_LOB_ID  numeric(9) ,    
@MDD_MODULE_ID int

)    
as    

BEGIN    
DELETE FROM MNT_DIARY_DETAILS WHERE MDD_MODULE_ID=@MDD_MODULE_ID AND MDD_LOB_ID=@MDD_LOB_ID     
END      










GO

