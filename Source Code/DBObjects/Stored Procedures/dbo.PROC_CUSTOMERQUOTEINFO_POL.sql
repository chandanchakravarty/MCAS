IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_CUSTOMERQUOTEINFO_POL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_CUSTOMERQUOTEINFO_POL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /* ==================================================================            
Proc Name        : dbo.Proc_CustomerQuoteInfo_Pol              
Created by       : Ashwani            
Date             : 07 Mar 2006            
Purpose          : retrieving data from QOT_CUSTOMER_QUOTE_LIST              
Revison History  :              
Modified by      :               
Description      :               
Used In          : Wolverine              
==================================================================            
Date     Review By          Comments              
==================================================================*/              
 
--drop proc Dbo.Proc_CustomerQuoteInfo_Pol     2144,1,3,2,'ONLYEFFECTIVE'  
CREATE  PROC [dbo].[PROC_CUSTOMERQUOTEINFO_POL]      
@CUSTOMER_ID INT,              
@QUOTE_ID INT,          
@POLICY_ID INT ,          
@POLICY_VERSION_ID INT ,  
@CALLED_FROM VARCHAR(50)= NULL        
              
AS              
BEGIN              
IF(@CALLED_FROM is NULL or @CALLED_FROM = '')  
  BEGIN  
  SELECT QUOTE_XML,QUOTE_INPUT_XML FROM QOT_CUSTOMER_QUOTE_LIST_POL WITH(NOLOCK)             
  WHERE CUSTOMER_ID = @CUSTOMER_ID-- AND QUOTE_ID=@QUOTE_ID       
     AND POLICY_ID = @POLICY_ID  AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
  END   
 ELSE IF (@CALLED_FROM = 'ONLYEFFECTIVE')  
  BEGIN  
  
  
   SELECT QUOTE_XML,QUOTE_INPUT_XML FROM QOT_CUSTOMER_QUOTE_LIST_POL WITH(NOLOCK)             
    WHERE CUSTOMER_ID = @CUSTOMER_ID-- AND QUOTE_ID=@QUOTE_ID       
      AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID < @POLICY_VERSION_ID  order by POLICY_VERSION_ID desc
  END  
   
END            
            
            
            
          
        
        
      
      
GO

