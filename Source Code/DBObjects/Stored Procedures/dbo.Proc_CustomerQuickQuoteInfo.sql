IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CustomerQuickQuoteInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CustomerQuickQuoteInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_CustomerQuickQuoteInfo            
Created by      : Praveen Kasana    
Date             : 13/3/2007          
Purpose         : retrieving data from QOT_CUSTOMER_QUOTE_LIST            
Revison History:            
------------------------------------------------------------            
Modified by    :             
Description    :             
Used In        : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------------------*/            
--drop proc Proc_CustomerQuickQuoteInfo            
create  PROC Dbo.Proc_CustomerQuickQuoteInfo            
 @CUSTOMER_ID int,            
 @QUOTE_ID int        
        
            
AS            
BEGIN            
 SELECT QQ_XML,QQ_RATING_REPORT,QQ_APP_NUMBER  
FROM CLT_QUICKQUOTE_LIST            
   WHERE CUSTOMER_ID = @CUSTOMER_ID and QQ_ID=@QUOTE_ID    
      
       
END         
    
    
    
          
    
     
    
    
      
      
    
    
    
    
  



GO

