IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillApplicationDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillApplicationDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 -- Proc_FillApplicationDropDown 1199    
-- drop proc dbo.Proc_FillApplicationDropDown    
CREATE PROCEDURE [dbo].[Proc_FillApplicationDropDown] --28033       
(      
 @CUSTOMER_ID int     , 
 @LANG_ID INT =1
)      
AS      
begin      
        
Select       
 CONVERT(VARCHAR, AL.APP_ID) +'-'+ CONVERT(VARCHAR, AL.APP_VERSION_ID) + '-APP'   AS VERSION_ID,       
 AL.APP_NUMBER + '(Ver:'+ CONVERT(VARCHAR, AL.APP_VERSION_ID) +') - '+dbo.fun_GetLookupDesc (8438,@LANG_ID)  as APP_NUMBER     
FROM  APP_LIST AL (NOLOCK)     
WHERE CUSTOMER_ID=@CUSTOMER_ID      


      
UNION      
      
Select       
 CONVERT(VARCHAR, ISNULL(CQL.QQ_ID,0)) +'-'+ CONVERT(VARCHAR, ISNULL(CQL.APP_ID,0))  + '-QQ' + '-' + CONVERT(VARCHAR, ISNULL(CQL.APP_VERSION_ID,0)) AS VERSION_ID,       
 CQL.QQ_NUMBER    + ' - '+dbo.fun_GetLookupDesc (10580,@LANG_ID)     
FROM  CLT_QUICKQUOTE_LIST CQL (NOLOCK)     
 LEFT JOIN APP_LIST AL ON AL.APP_NUMBER = CQL.QQ_APP_NUMBER AND AL.CUSTOMER_ID = CQL.CUSTOMER_ID      
WHERE CQL.CUSTOMER_ID=@CUSTOMER_ID      
    
UNION      
      
Select       
 CONVERT(VARCHAR, AL.Policy_ID) +'-'+ CONVERT(VARCHAR, AL.Policy_VERSION_ID)  + '-POL' AS VERSION_ID,       
 AL.Policy_NUMBER   + '(Ver:'+ CONVERT(VARCHAR, AL.Policy_VERSION_ID) +') - '++dbo.fun_GetLookupDesc (8440,@LANG_ID)       
FROM  POL_CUSTOMER_POLICY_LIST AL  (NOLOCK)    
WHERE CUSTOMER_ID=@CUSTOMER_ID      
      
End      
      
    
    
    
    
  
  
GO

