IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchPayeeName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchPayeeName]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          	: Dbo.Proc_FetchPayeeName  
Created by           	: kranti singh  
Date                    : 07/06/2007  
Purpose               	:   
Revison History :  
Used In                	:   Wolverine    

Reviewed By	:	Anurag Verma
Reviewed On	:	12-07-2007
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
---- DROP PROC Dbo.Proc_FetchPayeeName    

create procedure dbo.Proc_FetchPayeeName
(
 @CHECK_ID int
)
AS

SELECT PAYEE_ENTITY_NAME FROM TEMP_ACT_CHECK_INFORMATION with(nolock)
WHERE CHECK_ID=@CHECK_ID 














GO

