IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateACT_TRANSACTION_CODES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateACT_TRANSACTION_CODES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_ActivateDeactivateACT_TRANSACTION_CODES  
Created by      :  Ajit Singh Chahal  
Date            :  3/15/2005  
Purpose         :  To update the record in MNT_USER_LIST table  
Revison History :  
Used In         :    Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
create  PROC dbo.Proc_ActivateDeactivateACT_TRANSACTION_CODES  
(  
@CODE  int,  
@IS_ACTIVE  Char(1)     
)  
AS  
BEGIN  
UPDATE ACT_TRANSACTION_CODES  
 SET   
  IS_ACTIVE = @IS_ACTIVE  
 WHERE  
  TRAN_ID  = @CODE  
END  
  
  
  
  



GO

