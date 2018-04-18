IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePriorInfoOrdered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePriorInfoOrdered]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name          : Dbo.Proc_UpdatePriorInfoOrdered
Created by         : Sibin Philip
Date               : 23 Nov 09
Purpose            : To update PriorInfo_Ordered field when Prior Policy is fetched
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
--drop PROC Proc_UpdatePriorInfoOrdered
------   ------------       -------------------------*/
CREATE   PROC Dbo.Proc_UpdatePriorInfoOrdered 
(
	@CUSTOMER_ID Int
)

AS
BEGIN

 UPDATE CLT_CUSTOMER_LIST SET PRIORINFO_ORDERED = GetDate() WHERE CUSTOMER_ID = @CUSTOMER_ID

END


GO

