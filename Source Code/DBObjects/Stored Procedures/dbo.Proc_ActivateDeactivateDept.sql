IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateDept]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateDept]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_ActivateDeactivateDept    
Created by      : Ashwani    
Date            : 10 Mar,2005    
Purpose         : To Activate/Deactivate the record in Dept table    
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE  PROC dbo.Proc_ActivateDeactivateDept    
(    
	@CODE     numeric(9),    
 	@IS_ACTIVE  Char(1)    
)    
AS    
BEGIN  
	/*If assigned then return -1*/
	IF @IS_ACTIVE = 'N'
	BEGIN
		/*Check whether division assigned or not*/	
		IF EXISTS(SELECT DEPT_ID FROM MNT_DIV_DEPT_MAPPING WHERE DEPT_ID = @CODE)
			RETURN -1

		/*Checking whether profit center assigned in department*/
		IF EXISTS(SELECT DEPT_ID FROM MNT_DEPT_PC_MAPPING WHERE DEPT_ID = @CODE)
			RETURN -1	
	END  

  	UPDATE MNT_DEPT_LIST    
  	SET Is_Active = @IS_ACTIVE    
  	WHERE DEPT_ID   = @CODE    
    
	return 1
    
END




GO

