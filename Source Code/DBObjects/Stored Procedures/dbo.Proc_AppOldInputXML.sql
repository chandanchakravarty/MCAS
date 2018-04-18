IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AppOldInputXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AppOldInputXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*======================================================================================================    
Proc Name       : dbo.Proc_AppOldInputXML      
Created by      : Ashwani       
Date            : 11 Nov.,2005  
Purpose         : Return the RULE_INPUT_XML,APP_VERIFICATION_XML,SHOW_QUOTE  against the given     
      customerID,appID,appVersionID      
Revison History :      
Used In        :        
======================================================================================================    
Date     Review By          Comments      
======================================================================================================*/    
CREATE   proc Dbo.Proc_AppOldInputXML  
(      
 @CUSTOMER_ID int,      
 @APP_ID int,      
 @APP_VERSION_ID smallint     
)      
as      
begin      
select RULE_INPUT_XML, APP_VERIFICATION_XML ,SHOW_QUOTE   
from APP_LIST         
 where   CUSTOMER_ID =@CUSTOMER_ID AND   APP_ID=@APP_ID AND   APP_VERSION_ID=@APP_VERSION_ID      
end          
  

  



GO

