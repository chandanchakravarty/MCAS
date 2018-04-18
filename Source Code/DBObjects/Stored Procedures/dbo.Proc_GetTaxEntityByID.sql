IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTaxEntityByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTaxEntityByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetTaxEntityByID  
Created by      : Pradeep  
Date            : 02/05/2005  
Purpose         : To retrive a single record   
    from Tax entity  
Revison History :  
Used In         :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--DROP PROCEDURE Proc_GetTaxEntityByID   
CREATE   PROCEDURE Proc_GetTaxEntityByID  
(  
 @TAX_ID Int  
)  
  
As  
  
SELECT   
 TAX_ID,  
 TAX_NAME,  
 TAX_CODE,  
 TAX_ADDRESS1,  
 TAX_ADDRESS2,  
 TAX_CITY,  
 TAX_COUNTRY,  
 TAX_STATE,  
 TAX_ZIP,  
 TAX_PHONE,  
 TAX_EXT,  
 TAX_FAX,  
 TAX_EMAIL,  
 TAX_WEBSITE,  
 IS_ACTIVE/*,  
 CREATED_BY,  
 CREATED_DATETIME,  
 MODIFIED_BY,  
 LAST_UPDATED_DATETIME  */
FROM MNT_TAX_ENTITY_LIST  
WHERE TAX_ID = @TAX_ID  
  
  
  
  



GO

