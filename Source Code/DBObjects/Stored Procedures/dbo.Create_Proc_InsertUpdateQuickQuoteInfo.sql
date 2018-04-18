 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUpdateQuickQuoteInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUpdateQuickQuoteInfo]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : Proc_InsertUpdateQuickQuoteInfo    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose       :Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC [dbo].[Proc_InsertUpdateQuickQuoteInfo]   
(    
 @CUSTOMER_ID     int,    
 @QQ_ID   Int,    
 @QQ_NUMBER   varchar(100),    
 @QQ_TYPE  varchar(20),    
 @QQ_STATE  varchar(20) = null   
)    
AS    
DECLARE @NEW_QQ_ID INT    
BEGIN    
 IF @QQ_ID=0 OR @QQ_ID=-1    
 BEGIN    
  IF NOT EXISTS(SELECT 1 FROM CLT_QUICKQUOTE_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND QQ_NUMBER=@QQ_NUMBER AND QQ_TYPE=@QQ_TYPE)    
  BEGIN    
   SELECT @NEW_QQ_ID=ISNULL(MAX(QQ_ID),0)+1 FROM CLT_QUICKQUOTE_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID    
   INSERT INTO CLT_QUICKQUOTE_LIST (CUSTOMER_ID,QQ_ID,QQ_NUMBER,QQ_TYPE,IS_ACTIVE,QQ_STATE)    
   VALUES (@CUSTOMER_ID,@NEW_QQ_ID,@QQ_NUMBER,@QQ_TYPE,'Y',@QQ_STATE)    
  END    
  ELSE    
   SELECT @NEW_QQ_ID=-2    
 END    
 ELSE    
 BEGIN     
  SELECT @NEW_QQ_ID=@QQ_ID    
    
  IF EXISTS(SELECT 1 FROM CLT_QUICKQUOTE_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND QQ_ID = @QQ_ID AND QQ_TYPE=@QQ_TYPE AND QQ_NUMBER <> @QQ_NUMBER)    
  BEGIN    
   IF NOT EXISTS(SELECT 1 FROM CLT_QUICKQUOTE_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND QQ_NUMBER=@QQ_NUMBER AND QQ_TYPE=@QQ_TYPE)    
   BEGIN    
    UPDATE CLT_QUICKQUOTE_LIST    
    SET QQ_NUMBER=@QQ_NUMBER,    
        QQ_TYPE=@QQ_TYPE,    
        QQ_STATE=@QQ_STATE    
    WHERE CUSTOMER_ID=@CUSTOMER_ID AND QQ_ID = @QQ_ID     
   END    
   ELSE    
    SELECT @NEW_QQ_ID=-2      
  END    
 END    
 SELECT @NEW_QQ_ID QQ_ID    
END    
    
    