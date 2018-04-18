IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUmbDriverDOB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUmbDriverDOB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo. Proc_GetUmbDriverDOB  
Created by           : Sumit Chhabra
Date                    : 22/03/2006  
Purpose               :   
Revison History :  
Used In                :   Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE   PROCEDURE Proc_GetUmbDriverDOB  
(  
 @CUSTOMER_ID int,  
 @APP_ID              int,  
 @APP_VERSION_ID int,  
 @DRIVER_ID            int   
   
)  
AS  
BEGIN  
SELECT convert(varchar,DRIVER_DOB,101) DRIVER_DOB   
FROM APP_UMBRELLA_DRIVER_DETAILS   
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND   
APP_VERSION_ID=@APP_VERSION_ID AND  
DRIVER_ID=@DRIVER_ID    
  
END


GO

