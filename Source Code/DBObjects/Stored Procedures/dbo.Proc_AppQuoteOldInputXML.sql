IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AppQuoteOldInputXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AppQuoteOldInputXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*======================================================================================================      
Proc Name       : dbo.Proc_AppQuoteOldInputXML        
Created by      : Ashwani         
Date            : 28 Feb. 2006
Purpose         : Return the QUOTE_ID,QUOTE_INPUT_XML  against the given       
      customerID,appID,appVersionID        
Revison History :        
Used In        :          
======================================================================================================      
Date     Review By          Comments        
======================================================================================================*/      
CREATE   proc Dbo.Proc_AppQuoteOldInputXML    
(        
 @CUSTOMER_ID int,        
 @APP_ID int,        
 @APP_VERSION_ID smallint       
)        
as        
begin        
 select QUOTE_ID,QUOTE_INPUT_XML      
 from QOT_CUSTOMER_QUOTE_LIST           
 where   CUSTOMER_ID =@CUSTOMER_ID AND   APP_ID=@APP_ID AND   APP_VERSION_ID=@APP_VERSION_ID        
end            
    
  
    
  



GO

