IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePolicyWatercraftOperator]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePolicyWatercraftOperator]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_ActivateDeactivatePolicyWatercraftOperator        
Created by      : Swastika Gaur    
Date            : 26th Apr'06        
 
Revison History :        
Used In         : Wolverine        
    
       
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--drop proc dbo.Proc_ActivateDeactivatePolicyWatercraftOperator
CREATE PROC dbo.Proc_ActivateDeactivatePolicyWatercraftOperator        
(        
@CUSTOMER_ID     INT,        
@POLICY_ID     INT,        
@POLICY_VERSION_ID     SMALLINT,        
@DRIVER_ID     SMALLINT,
@IS_ACTIVE NCHAR(1)
)        
AS        
BEGIN        

UPDATE POL_WATERCRAFT_DRIVER_DETAILS       SET IS_ACTIVE=@IS_ACTIVE WHERE
	CUSTOMER_ID=@CUSTOMER_ID AND 
	POLICY_ID=@POLICY_ID AND
	POLICY_VERSION_ID=@POLICY_VERSION_ID AND
	DRIVER_ID=@DRIVER_ID
END



GO

