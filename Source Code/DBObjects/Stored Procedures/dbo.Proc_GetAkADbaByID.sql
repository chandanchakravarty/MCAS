IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAkADbaByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAkADbaByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*
----------------------------------------------------------    
Proc Name       : dbo.Proc_GetAkADbaByID
Created by      : Pradeep  
Date            : 11 May,2005    
Purpose         : Selects a single record from AKA/DBA    
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------   
*/

CREATE    PROCEDURE Proc_GetAkADbaByID
(
	@AKADBA_ID Int
)

As

SELECT AKADBA_TYPE,
       AKADBA_NAME,
	AKADBA_ADD,
	AKADBA_ADD2,
	AKADBA_CITY,
	AKADBA_STATE,
	AKADBA_ZIP,
	AKADBA_COUNTRY,
	AKADBA_WEBSITE,
	AKADBA_EMAIL,
	AKADBA_LEGAL_ENTITY_CODE,
	AKADBA_NAME_ON_FORM,
	AKADBA_DISP_ORDER,
	AKADBA_MEMO 		  	 
FROM CLT_CUSTOMER_AKADBA
WHERE AKADBA_ID = @AKADBA_ID






GO

