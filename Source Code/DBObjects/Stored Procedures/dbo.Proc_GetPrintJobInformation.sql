IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPrintJobInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPrintJobInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name       : Dbo.Proc_GetPrintJobInformation          
Created by      : Mohit Agarwal          
Date            : 24-Jul-2007         
Purpose         : To get the Dec Page details from Print Jobs           
Revison History :          
Used In         : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
        
Modify By :        
Modify On :     
Purpose   :         
------   ------------       -------------------------*/          
  -- drop Proc dbo.Proc_GetPrintJobInformation          
CREATE  PROC dbo.Proc_GetPrintJobInformation          
(          
 @CUSTOMER_ID  int,          
 @POLICY_ID  int,          
 @POLICY_VERSION_ID int          
)          
AS          
BEGIN          
SELECT * FROM PRINT_JOBS   with(nolock)
WHERE    (CUSTOMER_ID = @CUSTOMER_ID)   AND (POLICY_ID=@POLICY_ID) AND (POLICY_VERSION_ID=@POLICY_VERSION_ID);          
          
END          
          
    
    
  




GO

