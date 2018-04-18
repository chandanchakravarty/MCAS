IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GET_NEXT_AUTHORITY_LEVEL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GET_NEXT_AUTHORITY_LEVEL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                      
Proc Name       : Sumit Chhabra                                      
Created by      : Proc_GET_NEXT_AUTHORITY_LEVEL                                      
Date            : 05/15/2006
Purpose    			: To get the next authority level
Revison History :                                      
Used In  : Wolverine                                                                            
------------------------------------------------------------*/
CREATE PROC dbo.Proc_GET_NEXT_AUTHORITY_LEVEL
AS                              
BEGIN 
DECLARE @NextAuthorityLevel INT
SET @NextAuthorityLevel = -1

SELECT @NextAuthorityLevel = ISNULL(MAX(AUTHORITY_LEVEL),0)+1 FROM CLM_AUTHORITY_LIMIT
RETURN @NextAuthorityLevel
END



GO

