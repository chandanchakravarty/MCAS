IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillContractNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillContractNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_FillContractNumber
Created by      : Swarup
Date            : 12-Oct-2007
Purpose         : To select  record in Reinsurance Contract Type
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
--drop PROC Dbo.Proc_FillContractNumber 
------   ------------       -------------------------*/

CREATE PROC Dbo.Proc_FillContractNumber 

AS

BEGIN

	select CONTRACT_NUMBER,CONTRACT_ID from MNT_REINSURANCE_CONTRACT
END








GO

