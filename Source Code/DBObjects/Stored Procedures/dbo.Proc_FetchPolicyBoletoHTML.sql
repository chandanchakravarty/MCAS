IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchPolicyBoletoHTML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchPolicyBoletoHTML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                    
Proc Name        : dbo.[Proc_FetchPolicyBoletoHTML]                                    
Created by       : Praveen Kumar                                  
Date             : 01/06/2010                                    
Purpose          : retrieving HTML for generated Boleto from POL_INSTALLMENT_BOLETO      
Used In          : Ebix Advantage                                
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------*/                                    

 --DROP PROCEDURE Proc_FetchPolicyBoletoHTML 2126,179,2 ,2292
      
CREATE PROCEDURE [dbo].[Proc_FetchPolicyBoletoHTML]       
@CUSTOMER_ID int=null,      
@POLICY_ID int=null,      
@POLICY_VERSION_ID int=null,      
@INSTALLEMT_ID int=null ,
@INSTALLMENT_NO int=null      
AS      
BEGIN      
     DECLARE @query nvarchar(max); 
      
 
 set @query ='select BOLETO_ID, CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,
INSTALLEMT_ID,INSTALLMENT_NO,BANK_ID,BOLETO_HTML
 
 from POL_INSTALLMENT_BOLETO  WITH(NOLOCK) WHERE  
 CUSTOMER_ID= '''+ CAST(@CUSTOMER_ID AS NVARCHAR)  + '''
 AND POLICY_ID = '''+ CAST(@POLICY_ID AS NVARCHAR) + '''
 AND POLICY_VERSION_ID = '''+ CAST(@POLICY_VERSION_ID AS NVARCHAR)+ ''''
  IF @INSTALLEMT_ID is not NULL  
   BEGIN   
 
   set @query = @query +    
			' AND INSTALLEMT_ID=' + CAST(@INSTALLEMT_ID AS NVARCHAR) 
 END  
 
   IF @INSTALLMENT_NO is not NULL  
   BEGIN   
 
   set @query = @query +    
			' AND INSTALLMENT_NO=' + CAST(@INSTALLMENT_NO AS NVARCHAR) 
 END 
   
Exec (@query) 
 
      
END 


GO

