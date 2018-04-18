IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateAppTrailer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateAppTrailer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_ActivateDeactivateAppTrailer        
Created by      : Swastika Gaur          
Date            :  31st Mar'06                          
Purpose         :Activate/ Deactivate trailer       
Revison History :                
Used In         : Wolverine                
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
    
CREATE PROC DBO.Proc_ActivateDeactivateAppTrailer              
(                
@CUSTOMER_ID     INT,                
@APP_ID     INT,                
@APP_VERSION_ID     SMALLINT,                
@TRAILER_ID     SMALLINT,        
@IS_ACTIVE NCHAR(1)        
)                
AS                
BEGIN                
        
	UPDATE APP_WATERCRAFT_TRAILER_INFO SET IS_ACTIVE=@IS_ACTIVE WHERE        
	 CUSTOMER_ID=@CUSTOMER_ID AND         
	 APP_ID=@APP_ID AND        
	 APP_VERSION_ID=@APP_VERSION_ID AND        
	 TRAILER_ID=@TRAILER_ID     

END        
    
      


GO

