IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteProfitCenter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteProfitCenter]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------    
Proc Name   	: dbo.Proc_DeleteProfitCenter    
Created by  	: Priya  
Date        	: Sep 27, 2005
Purpose    	: Deletes a record from MNT_PROFIT_CENTER_LIST
Revison History :
Used In 	:   Wolverine       
 ------------------------------------------------------------                
Date     Review By          Comments              
     
------   ------------       -------------------------*/    

CREATE    PROC dbo.Proc_DeleteProfitCenter
(
	@ProfitCenterId Int
)
AS
BEGIN
	
IF EXISTS ( SELECT PC_ID FROM MNT_DEPT_PC_MAPPING WHERE PC_ID = @ProfitCenterId)	
BEGIN
return -1
END


		DELETE FROM MNT_PROFIT_CENTER_LIST
		WHERE 	PC_ID =@ProfitCenterId

		
END

GO

