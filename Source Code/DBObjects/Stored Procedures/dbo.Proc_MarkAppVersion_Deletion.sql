IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MarkAppVersion_Deletion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MarkAppVersion_Deletion]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------          
Proc Name        : dbo.Proc_MarkAppVersion_Deletion  
Created by        : Anurag Verma  
Date                : 12-10-2006
Purpose          : Marking app versions for deletion   
Revison History  :          
Used In          : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE  PROC dbo.Proc_MarkAppVersion_Deletion          
(          
	@CUSTOMER_ID     INT,          
	@POL_ID		 INT,
	@POL_VERSION_ID	 INT,
	@DELETION_FLAG	 CHAR(1)


)          
AS          
          
DECLARE @APP_VERSION_ID INT
DECLARE @APP_ID INT
BEGIN          
	select @APP_VERSION_ID=APP_VERSION_ID,@APP_ID=APP_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID
	
	UPDATE APP_LIST SET DELETE_FLAG=@DELETION_FLAG WHERE APP_VERSION_ID <> 	@APP_VERSION_ID
	AND CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID
	
END          
  






GO

