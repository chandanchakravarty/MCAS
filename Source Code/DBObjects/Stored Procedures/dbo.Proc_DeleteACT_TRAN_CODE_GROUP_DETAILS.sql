IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteACT_TRAN_CODE_GROUP_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteACT_TRAN_CODE_GROUP_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------  
Proc Name       : dbo.Proc_DeleteACT_TRAN_CODE_GROUP_DETAILS  
Created by      : Ajit Singh Chahal  
Date            : 6th may,2005  
Purpose      	:to delete records from ACT_TRAN_CODE_GROUP_DETAILS   
Revison History :  
Used In        	: Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROCEDURE dbo.Proc_DeleteACT_TRAN_CODE_GROUP_DETAILS
(@Detail_ID int)
AS  
BEGIN  
  delete from ACT_TRAN_CODE_GROUP_DETAILS where Detail_ID = @Detail_ID  
end  
								



GO

