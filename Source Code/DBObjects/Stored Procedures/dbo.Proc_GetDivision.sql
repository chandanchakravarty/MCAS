IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDivision]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDivision]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

        
        
 /*----------------------------------------------------------                    
Proc Name       : dbo.Proc_GetDivision            
Created by      : Sonal                
Date            : 13 may 2010                 
Purpose       : Return the Query                   
Revison History :                    
Used In   : Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/        
-- drop procedure Proc_GetDivision                    
Create PROC [dbo].[Proc_GetDivision]                    
(                    
   @DIV_ID  int,  
   @LANG_ID int                
)                    
AS                    
BEGIN                    
  SELECT DIV_ID,DIV_CODE,DIV_NAME,DIV_ADD1,DIV_ADD2,DIV_CITY,DIV_STATE,DIV_ZIP,DIV_COUNTRY,DIV_PHONE,DIV_EXT,              
   DIV_FAX,DIV_EMAIL,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,NAIC_CODE,BRANCH_CODE,BRANCH_CNPJ,
   ISNULL(Convert(varchar,DATE_OF_BIRTH,case when @LANG_ID=1 then 101 else 103 end),'') AS DATE_OF_BIRTH,REGIONAL_IDENTIFICATION, 
	ISNULL(Convert(varchar,REG_ID_ISSUE_DATE,case when @LANG_ID=1 then 101 else 103 end),'') AS REG_ID_ISSUE_DATE,ACTIVITY,REG_ID_ISSUE             
  FROM MNT_DIV_LIST                  
  WHERE DIV_ID = @DIV_ID                
END 
GO

