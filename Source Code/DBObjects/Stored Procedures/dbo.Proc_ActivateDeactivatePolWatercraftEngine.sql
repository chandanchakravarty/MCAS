IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePolWatercraftEngine]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePolWatercraftEngine]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_ActivateDeactivatePolWatercraftEngine          
Created by      : Sumit Chhabra          
Date            : 24/11/2006          
Purpose         : To activate/deactivate outboard engine info
Revison History :          
Used In         :   Wolverine                
------   ------------       -------------------------*/          
-- drop proc dbo.Proc_ActivateDeactivatePolWatercraftEngine          
CREATE PROC dbo.Proc_ActivateDeactivatePolWatercraftEngine          
( 
 @CUSTOMER_ID INT,          
 @POLICY_ID INT,
 @POLICY_VERSION_ID SMALLINT,
 @ENGINE_ID SMALLINT,  
 @IS_ACTIVE NCHAR(2)    
)          
AS          
BEGIN          
	UPDATE 
		POL_WATERCRAFT_ENGINE_INFO 
	SET 
		IS_ACTIVE=@IS_ACTIVE 
	WHERE
		CUSTOMER_ID=@CUSTOMER_ID AND
		POLICY_ID=@POLICY_ID AND
		POLICY_VERSION_ID=@POLICY_VERSION_ID AND
		ENGINE_ID=@ENGINE_ID
END


GO

