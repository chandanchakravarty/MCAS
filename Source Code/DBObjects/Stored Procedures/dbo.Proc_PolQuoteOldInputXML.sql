IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PolQuoteOldInputXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PolQuoteOldInputXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*======================================================================================================          
Proc Name       : dbo.Proc_PolQuoteOldInputXML            
Created by      : Ashwani             
Date            : 07 Mar, 2006    
Purpose         : Return the QUOTE_ID,QUOTE_INPUT_XML  against the given           
        customerID,polID,polVersionID            
Revison History :            
Used In        :              
  
Modified By : Ravindra   
Modified On  : 10-05-2006  
Purpose  : Added @QUOTE_TYPE   
======================================================================================================          
Date     Review By          Comments            
======================================================================================================*/          
-- drop proc  Dbo.Proc_PolQuoteOldInputXML  
CREATE   proc [dbo].[Proc_PolQuoteOldInputXML]        
(            
 @CUSTOMER_ID int,            
 @POLICY_ID int,            
 @POLICY_VERSION_ID smallint,  
 @QUOTE_TYPE varchar(20)           
)            
as            
begin            
 select QUOTE_ID,QUOTE_INPUT_XML          
 from QOT_CUSTOMER_QUOTE_LIST_POL  WITH(NOLOCK)              
 where   CUSTOMER_ID =@CUSTOMER_ID AND   POLICY_ID=@POLICY_ID AND   POLICY_VERSION_ID=@POLICY_VERSION_ID            
 AND QUOTE_TYPE = @QUOTE_TYPE          
end                
        
      
        
      
    
  
  
  
  
GO

