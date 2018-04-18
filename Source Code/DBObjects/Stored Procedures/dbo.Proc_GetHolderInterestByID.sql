IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHolderInterestByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHolderInterestByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*
----------------------------------------------------------    
Proc Name       : dbo.Proc_GetHolderInterestByID
Created by      : Pradeep  
Date            : 11 May,2005    
Purpose         : Selects a single record from MNT_HOLDER_INTEREST_LIST    
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------   
*/

CREATE  PROCEDURE Proc_GetHolderInterestByID
(
	@HOLDER_ID Int
)

As

SELECT 
	HOLDER_ID,
	HOLDER_NAME,
	HOLDER_CODE,
	HOLDER_ADD1,
	HOLDER_ADD2,
	HOLDER_CITY,
	HOLDER_COUNTRY,
	HOLDER_STATE,
	HOLDER_ZIP,
	HOLDER_MAIN_PHONE_NO,
	HOLDER_EXT,
	HOLDER_MOBILE,
	HOLDER_FAX,
	HOLDER_EMAIL,
	HOLDER_LEGAL_ENTITY,
	HOLDER_TYPE,
	HOLDER_MEMO,
	IS_ACTIVE,
	CREATED_BY,
	CREATED_DATETIME,
	MODIFIED_BY,
	LAST_UPDATED_DATETIME
FROM MNT_HOLDER_INTEREST_LIST
WHERE HOLDER_ID = @HOLDER_ID	
 	 






GO

