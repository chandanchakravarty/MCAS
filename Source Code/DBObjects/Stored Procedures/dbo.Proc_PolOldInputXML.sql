IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PolOldInputXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PolOldInputXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*======================================================================================================      
Proc Name       : dbo.Proc_PolOldInputXML        
Created by      : Ashwani         
Date            : 15 Sep,2006  
Purpose         : Return the RULE_INPUT_XML,APP_VERIFICATION_XML,SHOW_QUOTE    
            
Revison History :        
Used In        :          
======================================================================================================      
Date     Review By          Comments        
======================================================================================================*/      
CREATE   proc dbo.Proc_PolOldInputXML    
(        
 @CUSTOMER_ID int,        
 @POLICY_ID int,        
 @POLICY_VERSION_ID smallint       
)        
as        
begin        
 select RULE_INPUT_XML, APP_VERIFICATION_XML,SHOW_QUOTE     
 from   POL_CUSTOMER_POLICY_LIST           
 where  CUSTOMER_ID =@CUSTOMER_ID AND   POLICY_ID=@POLICY_ID AND   POLICY_VERSION_ID=@POLICY_VERSION_ID     
end            
    
  
    
  
  
  
  
  



GO

