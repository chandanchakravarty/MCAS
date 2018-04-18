IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InstallActivateDeactivate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InstallActivateDeactivate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InstallActivateDeactivate  
Created by      : Shafi  
Date            : 28/12/2005  
Purpose         : To update the record in Install table  
Revison History :  
Used In         :   wolvorine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC dbo.Proc_InstallActivateDeactivate   
(  
@IDEN_PLAN_ID   int, 
@IS_ACTIVE  nvarchar(1)
)  
AS  
BEGIN  
  

 IF @IDEN_PLAN_ID   > 0
  BEGIN

 UPDATE ACT_INSTALL_PLAN_DETAIL  SET  Is_Active = @IS_ACTIVE   WHERE  IDEN_PLAN_ID = @IDEN_PLAN_ID

  RETURN 1  
 END
END  
  
  



GO

