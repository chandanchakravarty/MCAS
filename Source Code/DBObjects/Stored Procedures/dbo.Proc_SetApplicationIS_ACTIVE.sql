IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetApplicationIS_ACTIVE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetApplicationIS_ACTIVE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*--------------------------------------------------------------------    
CREATED BY   : Vijay Arora    
CREATED DATE TIME : 04-01-2006    
PURPOSE    :  Set the IS_Active of the Application.    
REVIEW HISTORY    
REVIEW BY  DATE  PURPOSE    
    
---------------------------------------------------------------------*/    
--drop proc Proc_SetApplicationIS_ACTIVE    
CREATE PROCEDURE dbo.Proc_SetApplicationIS_ACTIVE    
(    
 @CUSTOMER_ID  INT,      
 @POLICY_ID    INT,       
 @POLICY_VERSION_ID  INT,       
 @IS_ACTIVE   NVARCHAR(5)    
)    
AS    
BEGIN    
    
 DECLARE @APP_ID INT    
 DECLARE @APP_VERSION_ID INT    
    
 SELECT @APP_ID = APP_ID, @APP_VERSION_ID = APP_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID     
    
 IF @IS_ACTIVE = 'N'
	 BEGIN 
		UPDATE APP_LIST SET IS_ACTIVE = @IS_ACTIVE, APP_STATUS = 'Rejected'
  		WHERE CUSTOMER_ID = @CUSTOMER_ID AND     
      		APP_ID = @APP_ID AND    
		APP_VERSION_ID = @APP_VERSION_ID    
	 END 
 ELSE
	 BEGIN 
		UPDATE APP_LIST SET IS_ACTIVE = @IS_ACTIVE    
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND     
	        APP_ID = @APP_ID AND    
	         APP_VERSION_ID = @APP_VERSION_ID    
	 END

      
END    
    
  



GO

