IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdtaePremiumNoticeSpool]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdtaePremiumNoticeSpool]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       :  dbo.Proc_UpdtaePremiumNoticeSpool
Created by      :  Ravindra
Date            :  
Purpose         :  
Revison History :                
Used In         :  Wolverine                
                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- drop PROC dbo.Proc_UpdtaePremiumNoticeSpool
CREATE PROC dbo.Proc_UpdtaePremiumNoticeSpool
(
	@SPOOL_ID	Int
)
AS
BEGIN 

	UPDATE ACT_PREMIUM_NOTICE_SPOOL  SET PROCESSED = 1
	WHERE SPOOL_ID = @SPOOL_ID  
END

GO

