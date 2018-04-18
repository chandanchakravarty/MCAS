IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAPP_TRAILER_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAPP_TRAILER_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
          
Proc Name       : Proc_DeleteAPP_TRAILER_INFORMATION          
Created by      : Swastika Gaur          
Date            : 31st Mar'06          
Purpose         : Delete Trailer Information, Add. Int. of Trailer          
Revison History :          
Used In         : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/  
-- drop proc dbo.Proc_DeleteAPP_TRAILER_INFORMATION                  
CREATE PROC dbo.Proc_DeleteAPP_TRAILER_INFORMATION          
(          
 @CUSTOMER_ID INT,    
 @APP_ID INT,    
 @APP_VERSION_ID INT,    
 @TRAILER_ID INT   
  
)          
AS          
BEGIN     
     

 -- Delete Additional Int. attached to the Trailer
 DELETE FROM APP_WATERCRAFT_TRAILER_ADD_INT
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND TRAILER_ID=@TRAILER_ID 

 
 -- Delete Trailer Info
 DELETE FROM APP_WATERCRAFT_TRAILER_INFO
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND TRAILER_ID=@TRAILER_ID 

END         
      



GO

