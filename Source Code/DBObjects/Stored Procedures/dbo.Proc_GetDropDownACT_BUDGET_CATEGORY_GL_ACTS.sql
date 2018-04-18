IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDropDownACT_BUDGET_CATEGORY_GL_ACTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDropDownACT_BUDGET_CATEGORY_GL_ACTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetDropDownACT_BUDGET_CATEGORY_GL_ACTS  
Created by      : Vijay Arora  
Date            : Oct. 05 2005  
Purpose         : To get all the active budget categories from ACT_BUDGET_CATEGORY table  
Revison History :  
Used In         : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- Drop PROC dbo.Proc_GetDropDownACT_BUDGET_CATEGORY_GL_ACTS 
CREATE PROC dbo.Proc_GetDropDownACT_BUDGET_CATEGORY_GL_ACTS  
AS  
BEGIN  
 SELECT CATEGEORY_ID,CATEGORY_DEPARTEMENT_NAME FROM ACT_BUDGET_CATEGORY WHERE IS_ACTIVE = 'Y' ORDER BY CATEGORY_DEPARTEMENT_NAME ASC

END  


















GO

