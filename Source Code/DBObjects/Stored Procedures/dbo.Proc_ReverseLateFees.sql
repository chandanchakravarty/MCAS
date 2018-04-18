IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ReverseLateFees]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ReverseLateFees]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*--------------------------------------------------------------------    
Proc Name       : dbo.Proc_ReverseLateFees          
Created by      : Ravindra     
Date            : 02-19-2008         
Purpose			: Reverse Unpaid Late fees at cancellation rollback            
Revison History :          
Used In         : Wolverine          
---------------------------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       ----------------------------------------------------*/          
--drop proc  Proc_ReverseLateFees
CREATE Proc dbo.Proc_ReverseLateFees
(          
	@CUSTOMER_ID			INt,          
	@POLICY_ID				Int,          
	@POLICY_VERSION_ID		INT          
)          
AS          
BEGIN          
	UPDATE ACT_CUSTOMER_OPEN_ITEMS SET TOTAL_DUE = ISNULL(TOTAL_PAID,0) 
	WHERE CUSTOMER_ID = @CUSTOMER_ID 
	AND	POLICY_ID		= @POLICY_ID 
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID 
	AND ITEM_TRAN_CODE = 'LF'
	AND ITEM_TRAN_CODE_TYPE = 'FEES'
	--Ravindra(02-03-2009)
	AND UPDATED_FROM <> 'F'
END          
          
        



GO

