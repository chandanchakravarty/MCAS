IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_ACTIVATEDEACTIVATEWATERCRAFTENGINE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_ACTIVATEDEACTIVATEWATERCRAFTENGINE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROC dbo.PROC_ACTIVATEDEACTIVATEWATERCRAFTENGINE                           
(                            
	@CUSTOMER_ID  INT,          
	@APP_ID  INT,          
	@APP_VERSION_ID INT,          
	@ENGINE_ID  INT ,         
	@IS_ACTIVE NCHAR(2)     
)                            
AS                            
BEGIN                                          
	UPDATE APP_WATERCRAFT_ENGINE_INFO SET IS_ACTIVE = @IS_ACTIVE WHERE                    
	CUSTOMER_ID = @CUSTOMER_ID AND                     
	APP_ID = @APP_ID AND                    
	APP_VERSION_ID = @APP_VERSION_ID AND                    
	ENGINE_ID = @ENGINE_ID               
END          


GO

