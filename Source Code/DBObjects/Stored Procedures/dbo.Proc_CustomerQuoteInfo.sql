IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CustomerQuoteInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CustomerQuoteInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------        
Proc Name       : dbo.Proc_CustomerQuoteInfo        
Created by       : Nidhi and Shrikant      
Date             : 7/8/2005      
Purpose         : retrieving data from QOT_CUSTOMER_QUOTE_LIST        
Revison History:        
------------------------------------------------------------        
Modified by    :         
Description    :         
Used In        : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------------------*/        
--drop proc Proc_CustomerQuoteInfo        
create  PROC Dbo.Proc_CustomerQuoteInfo        
 @CUSTOMER_ID int,        
 @QUOTE_ID int,    
 @APP_ID int,    
 @APP_VERSION_ID int      
        
AS        
BEGIN        
 SELECT QUOTE_XML,QUOTE_TYPE,CUSTOMER_ID,QUOTE_ID,QUOTE_VERSION_ID,APP_ID,APP_VERSION_ID,QUOTE_TYPE,      
         QUOTE_NUMBER,QUOTE_DESCRIPTION,IS_ACCEPTED,IS_ACTIVE,  QUOTE_INPUT_XML        
 FROM QOT_CUSTOMER_QUOTE_LIST        
   WHERE CUSTOMER_ID = @CUSTOMER_ID --and QUOTE_ID=@QUOTE_ID  
and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID      
END        
      
  
  
  




GO

