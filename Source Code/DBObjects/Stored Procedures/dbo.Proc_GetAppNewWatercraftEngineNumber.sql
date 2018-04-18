IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppNewWatercraftEngineNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppNewWatercraftEngineNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name            : dbo.Proc_GetAppNewWatercraftEngineNumber        
Created by           : Swastika        
Date                 : 26th July'06        
Purpose              : To get the new Engine Number          
Revison History      :        
Used In              :   Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--DROP PROC dbo.Proc_GetAppNewWatercraftEngineNumber   
CREATE PROC dbo.Proc_GetAppNewWatercraftEngineNumber        
@CUSTOMER_ID INT,        
@APP_ID INT,        
@APP_VERSION_ID INT,
@BOAT_NO INT    
AS        
BEGIN        

 BEGIN
  SELECT    (isnull(MAX(ENGINE_NO),0)) +1 as ENGINE_NO  
  FROM         APP_WATERCRAFT_ENGINE_INFO WITH(NOLOCK)  
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND ASSOCIATED_BOAT = @BOAT_NO
 END        
     
END    


GO

