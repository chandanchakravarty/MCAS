IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateRequestResponseLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateRequestResponseLog]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name           	: Dbo.Proc_UpdateRequestResponseLog
Created by            	: Vijay Arora    
Date                    : 21-03-2006    
Purpose                	:       
Revison History  	:      
Used In                 :   Wolverine        
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/ 
--drop proc dbo.Proc_UpdateRequestResponseLog     
CREATE PROCEDURE dbo.Proc_UpdateRequestResponseLog      
(
	@ROW_ID INT, 
	@RESPONSE_DATETIME DATETIME,
	@IIX_REQUEST text,
	@IIX_RESPONSE text
)
AS      
BEGIN      
IF EXISTS (SELECT ROW_ID FROM MNT_REQUEST_RESPONSE_LOG WHERE ROW_ID = @ROW_ID)
	UPDATE MNT_REQUEST_RESPONSE_LOG 
	SET 
		RESPONSE_DATETIME = @RESPONSE_DATETIME, 
		IIX_REQUEST=@IIX_REQUEST,
		IIX_RESPONSE=@IIX_RESPONSE 
	WHERE ROW_ID = @ROW_ID 
END    
    
-- kill 57  
    
  


















GO

