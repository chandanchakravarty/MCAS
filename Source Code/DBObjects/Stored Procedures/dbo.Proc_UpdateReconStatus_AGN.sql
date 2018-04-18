IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateReconStatus_AGN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateReconStatus_AGN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : Proc_UpdateReconStatus_AGN  
Created by      : Ravindra
Date            : 7 Dec 2006
Purpose      	: Reconciles the deposit againt agency open items and post that deposit also  
Revison History :  

------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       --------------------------------*/  
-- DROP PROC dbo.Proc_UpdateReconStatus_AGN  
CREATE PROCEDURE dbo.Proc_UpdateReconStatus_AGN  
(  
 @CD_LINE_ITEM_ID  INT,  
 @STATUS Char(2)
)
AS
 UPDATE ACT_CURRENT_DEPOSIT_LINE_ITEMS 
 SET IS_RECONCILED = @STATUS
 WHERE CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID
 RETURN 1









GO

