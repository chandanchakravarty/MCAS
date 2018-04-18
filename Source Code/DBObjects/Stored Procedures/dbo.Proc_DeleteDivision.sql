IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteDivision]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteDivision]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO






/*----------------------------------------------------------    
Proc Name   	: dbo.Proc_DeleteDivision    
Created by  	: Gaurav    
Date        	: July 13, 2005
Purpose    	: Deletes a record from MNT_DIV_LIST
Revison History :
Used In 	:   Wolverine       
 ------------------------------------------------------------                
Date     Review By          Comments              
     
------   ------------       -------------------------*/    
CREATE    PROC dbo.Proc_DeleteDivision
(
	@DIV_ID Int
)
AS
BEGIN
	IF NOT EXISTS
	(
		SELECT DIV_ID FROM MNT_DIV_DEPT_MAPPING
		WHERE 	DIV_ID = @DIV_ID	
	)
	BEGIN

		DELETE FROM MNT_DIV_LIST
		WHERE 	DIV_ID = @DIV_ID	
	END
	ELSE
	BEGIN
		return -1
	END
		
END


GO

