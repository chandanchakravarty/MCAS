IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAuthenticationKeyEOD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAuthenticationKeyEOD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name          : dbo.GetAuthenticationKeyEOD
Created by         : Praveen kasana     
Date               : 16 March 2009
Purpose            : To Get login AUTHENTICATION_TOKEN For EOD
Revison History    :          
Used In            : Wolverine            
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/      
 
CREATE   PROCEDURE dbo.GetAuthenticationKeyEOD       
 @EOD_USER_ID   INT
        
AS         
  
       
BEGIN      

SELECT ISNULL(AUTHENTICATION_TOKEN,'') AS AUTHENTICATION_TOKEN
FROM     
MNT_USER_LIST WHERE [USER_ID] = @EOD_USER_ID  
 

END   
GO

