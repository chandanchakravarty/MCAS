IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateLoggedStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateLoggedStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name          : Dbo.Proc_UpdateLoggedStatus          
Created by         : Praveen kasana     
Date               : 17/08/2007   
Purpose            : To Update login SessionID    
Revison History    :          
Used In            : Wolverine            
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/      
 
CREATE   PROCEDURE Dbo.Proc_UpdateLoggedStatus          
 @USER_ID   INT,  
 @LOG_OUT varchar(10) = null ,
 @SESSION_ID varchar(200) = null
        
AS         
  
       
BEGIN          
    
	UPDATE MNT_USER_LIST SET SESSION_ID = @SESSION_ID WHERE [USER_ID] = @USER_ID  
        
END          
          
          
          
          
          
        
      
    
  





GO

