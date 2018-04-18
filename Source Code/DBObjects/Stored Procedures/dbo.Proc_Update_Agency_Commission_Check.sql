IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_Agency_Commission_Check]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_Agency_Commission_Check]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-----------------------------------------------------------------------------------    
Proc Name   	: dbo.Proc_Update_Agency_Commission_Check    
Created by  	: Ashwani    
Date        	: 12 July,2006
Purpose    	: Delete the record into ACT_CHECK_INFORMATION
Revison History :
Used In 	:   Wolverine       
 -------------------------------------------------------------------------------------                    
Date     Review By          Comments              
     
-------------------------    ----------   ------------       -------------------------*/   
create PROC dbo.Proc_Update_Agency_Commission_Check
(
	@AGENCY_ID int,
	@MONTH int ,
	@YEAR int ,
	@AMOUNT decimal(18,2),
	@CHECK_TYPE nvarchar(10),
	@CREATED_DATETIME datetime,
	@CHECK_ID int 

)
AS
BEGIN
	DELETE ACT_AGENCY_STATEMENT 
	FROM ACT_AGENCY_STATEMENT 
	INNER JOIN ACT_AGENCY_OPEN_ITEMS AOI on ACT_AGENCY_STATEMENT.AGENCY_OPEN_ITEM_ID = AOI.IDEN_ROW_ID
	WHERE AOI.SOURCE_ROW_ID=@CHECK_ID AND AOI.UPDATED_FROM='C'


	DELETE FROM ACT_AGENCY_OPEN_ITEMS
	WHERE SOURCE_ROW_ID=@CHECK_ID AND UPDATED_FROM='C'
	
	
	DELETE FROM ACT_CHECK_INFORMATION
	WHERE CHECK_ID = @CHECK_ID AND AGENCY_ID = @AGENCY_ID
	
	EXEC PROC_INSERT_AGENCY_COMMISSION_CHECK @AGENCY_ID,@MONTH,@YEAR,@AMOUNT,@CHECK_TYPE,@CREATED_DATETIME,@CHECK_ID
		

END










GO

