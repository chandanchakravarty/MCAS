IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DelPol_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DelPol_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_DelPol_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL
Created by      : Swastika
Date            : 24th July,06
Purpose    	  :Evaluation
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc dbo.Proc_DelPol_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL
CREATE PROC dbo.Proc_DelPol_HOME_OWNER_SCH_ITEMS_CVGS_DETAIL
(
@CUSTOMER_ID int,
@POL_ID int,
@POL_VERSION_ID int,
@ITEM_ID int,
@ITEM_DETAIL_ID int)
As
Begin
	DELETE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS 
	WHERE CUSTOMER_ID=@CUSTOMER_ID and 
	POL_ID=@POL_ID and 
	POL_VERSION_ID=@POL_VERSION_ID and 
	ITEM_ID=@ITEM_ID and
	ITEM_DETAIL_ID=@ITEM_DETAIL_ID 
END






GO

