IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQuickQuoteNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQuickQuoteNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------        
Proc Name       : Proc_GetQuickQuoteNumber        
Created by      : Manoj Rathore      
Date            : 24-May-2007        
Purpose         :Get the Number of QQ from App Number    
Revison History :        
Used In        : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments    
drop PROC dbo.Proc_GetQuickQuoteNumber      
------   ------------       -------------------------*/        
CREATE PROC dbo.Proc_GetQuickQuoteNumber       
(        
 @CUSTOMER_ID  int,    
 @QQ_ID int    
)        
AS        
BEGIN        
 SELECT C.QQ_NUMBER, C.QQ_TYPE,M.LOB_DESC, C.QQ_STATE ,isnull(C.QQ_APP_NUMBER,'') as QQ_APP_NUMBER  FROM  CLT_QUICKQUOTE_LIST C   
 LEFT OUTER JOIN MNT_LOB_MASTER M ON  
 C.QQ_TYPE = M.LOB_CODE   
 WHERE  CUSTOMER_ID = @CUSTOMER_ID AND QQ_ID = @QQ_ID        
     
END        
  
  
  


GO

