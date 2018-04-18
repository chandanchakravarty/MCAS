IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateApplicationQuoteForRules]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateApplicationQuoteForRules]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------      
Proc Name       : dbo.Proc_UpdateApplicationQuoteForRules      
Created by      : Nidhi      
Date            : 8/26/2005      
Purpose         : Update      
Revison History :      
Used In         :        
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE   PROC dbo.Proc_UpdateApplicationQuoteForRules      
(      
 @CUSTOMER_ID     int,      
 @APP_ID     int,      
 @APP_VERSION_ID     smallint,        
 @SHOW_QUOTE     nchar(1)  
)      
AS      
BEGIN      
 UPDATE APP_LIST      
 SET       
  SHOW_QUOTE= @SHOW_QUOTE  
 WHERE      
 CUSTOMER_ID =@CUSTOMER_ID AND      
 APP_ID=@APP_ID AND      
 APP_VERSION_ID=@APP_VERSION_ID      
END      
    
    
    
  




GO

