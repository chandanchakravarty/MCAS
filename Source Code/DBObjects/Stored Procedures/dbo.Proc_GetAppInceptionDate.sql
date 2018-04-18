IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppInceptionDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppInceptionDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_GetAppInceptionDate                        
Created by      : swastika                        
Date                : 23rd Aug'06                        
Purpose          :                        
Revison History :                        
Used In  : Wolverine                        
                        
        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/    
-- drop proc dbo.Proc_GetAppInceptionDate                    
CREATE PROC dbo.Proc_GetAppInceptionDate    
(                        
@CUSTOMER_ID     int,                        
@APP_ID     int ,                        
@APP_VERSION_ID     smallint
)                        
AS                        
BEGIN                    
	SELECT year(convert(varchar(20),ISNULL(APP_INCEPTION_DATE,''),101)) FROM APP_LIST (NOLOCK)
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
	APP_ID = @APP_ID AND
	APP_VERSION_ID = @APP_VERSION_ID
	
END  



GO

