IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteDepartment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteDepartment]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO






/*----------------------------------------------------------    
Proc Name   	: dbo.Proc_DeleteDepartment    
Created by  	: Gaurav    
Date        	: July 13, 2005
Purpose    	: Deletes a record from MNT_DEPT_LIST
Revison History :
Used In 	:   Wolverine       
 ------------------------------------------------------------                
Date     Review By          Comments              
     
------   ------------       -------------------------*/    
CREATE    PROC dbo.Proc_DeleteDepartment
(
	@DEPT_ID Int
)
AS
BEGIN
	IF EXISTS (SELECT DEPT_ID FROM MNT_DIV_DEPT_MAPPING 	WHERE 	DEPT_ID = @DEPT_ID)	
BEGIN
return -1
END
IF EXISTS ( SELECT DEPT_ID FROM MNT_DEPT_PC_MAPPING WHERE DEPT_ID = @DEPT_ID)	
BEGIN
return -1
END


		DELETE FROM MNT_DEPT_LIST
		WHERE 	DEPT_ID = @DEPT_ID	

		
END


GO

