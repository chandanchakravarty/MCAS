IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetAuthenticationKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetAuthenticationKey]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name          : Dbo.SetAuthenticationKey           
Created by         : Praveen kasana     
Date               : 16 March 2009
Purpose            : To Update login AUTHENTICATION_TOKEN
Revison History    :          
Used In            : Wolverine            
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/      
 
CREATE   PROCEDURE dbo.Proc_SetAuthenticationKey       
 @USER_ID   INT,  
 @AUTHENTICATION_TOKEN varchar(2000) = null
        
AS         
  
       
BEGIN          
    
	UPDATE MNT_USER_LIST SET AUTHENTICATION_TOKEN = @AUTHENTICATION_TOKEN WHERE [USER_ID] = @USER_ID  
        
END     



          
          
          
          
          
        
      
    
  






GO

