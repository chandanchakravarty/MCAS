IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateAPP_SOLID_FUEL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateAPP_SOLID_FUEL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------      
Proc Name       : dbo.Proc_ActivateDeactivateAPP_SOLID_FUEL     
Created by      :  Mohit      
Date                :  3/15/2005      
Purpose         :  To update the record in APP_HOME_OWNER_SOLID_FUEL   table      
Revison History :      
Used In         :    Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/ 
--DROP PROC Proc_ActivateDeactivateAPP_SOLID_FUEL     
CREATE   PROC dbo.Proc_ActivateDeactivateAPP_SOLID_FUEL      
(      
@CODE nchar (7),      
@IS_ACTIVE  Char(1),
@CUSTOMER_ID  INT,--@CUSTOMER_ID, @APP_ID, @APP_VERSION_ID added by Charles on 21-Oct-09 for Itrack 6599
@APP_ID INT,
@APP_VERSION_ID SMALLINT        
)      
AS      
BEGIN      
	UPDATE APP_HOME_OWNER_SOLID_FUEL      
	SET IS_ACTIVE = @IS_ACTIVE      
	WHERE FUEL_ID= @CODE  
	AND CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID --Added by Charles on 21-Oct-09 for Itrack 6599      
END      
 
GO

