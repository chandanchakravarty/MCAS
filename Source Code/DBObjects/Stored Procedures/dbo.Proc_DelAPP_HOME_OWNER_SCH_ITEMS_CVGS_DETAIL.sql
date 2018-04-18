IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DelAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DelAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL
Created by      : Raman Pal Singh
Date            : 6/16/2006
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_DelAPP_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL
(
@CUSTOMER_ID int,
@APP_ID int,
@APP_VERSION_ID int,
@ITEM_ID int,
@ITEM_DETAIL_ID int)
As
Begin
DELETE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS 
WHERE CUSTOMER_ID=@CUSTOMER_ID and 
APP_ID=@APP_ID and 
APP_VERSION_ID=@APP_VERSION_ID and 
ITEM_ID=@ITEM_ID and
ITEM_DETAIL_ID=@ITEM_DETAIL_ID 
END



GO

