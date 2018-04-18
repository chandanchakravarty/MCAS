IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateUmbrellaOperator]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateUmbrellaOperator]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_ActivateDeactivateUmbrellaOperator
Created by      : Sumit Chhabra    
Date            : 24/10/2005        
Purpose       :Evaluation        
Modified by      : Sumit Chhabra    
Date            : 11/11/2005        
Purpose       :Activate/ Deactivate umbrella operator
Revison History :        
Used In        : Wolverine        
    
       
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC DBO.Proc_ActivateDeactivateUmbrellaOperator        
(        
@CUSTOMER_ID     INT,        
@APP_ID     INT,        
@APP_VERSION_ID     SMALLINT,        
@DRIVER_ID     SMALLINT,
@IS_ACTIVE NCHAR(2)
)        
AS        
BEGIN        

UPDATE APP_UMBRELLA_OPERATOR_INFO SET IS_ACTIVE=@IS_ACTIVE WHERE
	CUSTOMER_ID=@CUSTOMER_ID AND 
	APP_ID=@APP_ID AND
	APP_VERSION_ID=@APP_VERSION_ID AND
	DRIVER_ID=@DRIVER_ID
END


GO

