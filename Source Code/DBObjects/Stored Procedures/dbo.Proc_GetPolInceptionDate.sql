IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolInceptionDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolInceptionDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_GetPolInceptionDate                        
Created by      : swastika                        
Date                : 23rd Aug'06                        
Purpose          :                        
Revison History :                        
Used In  : Wolverine                        
                        
        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/    
-- drop proc dbo.Proc_GetPolInceptionDate                    
CREATE PROC dbo.Proc_GetPolInceptionDate    
(                        
@CUSTOMER_ID     int,                        
@POLICY_ID     int ,                        
@POLICY_VERSION_ID     smallint
)                        
AS                        
BEGIN                    
	SELECT year(convert(varchar(20),ISNULL(APP_INCEPTION_DATE,''),101)) FROM POL_CUSTOMER_POLICY_LIST (NOLOCK)
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
	POLICY_ID = @POLICY_ID AND
	POLICY_VERSION_ID = @POLICY_VERSION_ID
	
END  


GO

