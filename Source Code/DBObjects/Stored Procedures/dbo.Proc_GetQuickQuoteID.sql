 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQuickQuoteID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQuickQuoteID]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_GetQuickQuoteID]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   


    
  
--  Proc_GetQuickQuoteID  -100,'C1000001QQ'   
         
CREATE PROC dbo.Proc_GetQuickQuoteID           
(            
 @CUSTOMER_ID  int,        
 @QQ_NUMBER nvarchar(100)     
)            
AS            
BEGIN       
  
print  @CUSTOMER_ID  
print   @QQ_NUMBER    
 SELECT C.QQ_ID,QQ_NUMBER, C.QQ_TYPE,M.LOB_DESC,   
 C.QQ_STATE ,isnull(C.QQ_APP_NUMBER,'') as QQ_APP_NUMBER    
 FROM  CLT_QUICKQUOTE_LIST C       
 LEFT OUTER JOIN MNT_LOB_MASTER M ON      
 C.QQ_TYPE = M.LOB_CODE       
 WHERE  CUSTOMER_ID = @CUSTOMER_ID  
 --AND C.QQ_TYPE = @QQ_TYPE  
 AND C.QQ_NUMBER = @QQ_NUMBER            
         
END            
      
      
      