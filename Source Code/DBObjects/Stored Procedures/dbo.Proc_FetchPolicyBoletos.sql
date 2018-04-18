IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchPolicyBoletos]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchPolicyBoletos]
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
    
 -- DROP PROCEDURE Proc_FetchPolicyBoletos 28033,338,4,NULL  ,null,'INSTALLMENT'    
---- Proc_FetchPolicyBoletos 2043,443,null,1,null  ,'GENERATED_BOLETO' 
          
Create PROCEDURE [dbo].[Proc_FetchPolicyBoletos]           
@CUSTOMER_ID int=NULL,    
@POLICY_ID int=NULL,     
@POLICY_VERSION_ID int=NULL,             
@INSTALLMENT_NO INT=NULL,    
@INSTALLEMT_ID INT=NULL,     
@CALLED_FROM NVARCHAR(50), 
@CO_APPLICANT_ID INT=NULL
AS          
BEGIN    
DECLARE @REGENERATE_PAID_BOLETO INT  --iTrack-1361
   
IF(UPPER(@CALLED_FROM)='GENERATED_BOLETO')--Get only generated boleto
BEGIN  
     DECLARE @query nvarchar(max);     
          
     
 set @query ='SELECT ACT.POLICY_ID,ACT.POLICY_VERSION_ID,ACT.CUSTOMER_ID,APP_ID,APP_VERSION_ID,INSTALLMENT_AMOUNT,    
  INSTALLMENT_EFFECTIVE_DATE,RELEASED_STATUS,ROW_ID,ACT.INSTALLMENT_NO,INTEREST_AMOUNT,FEE,TAXES,TOTAL ,    
      
  BOLETO_ID, INSTALLEMT_ID,BANK_ID,BOLETO_HTML  ,CO_APPLICANT_ID  
      
  from ACT_POLICY_INSTALLMENT_DETAILS AS ACT  WITH(NOLOCK)           
  left join POL_INSTALLMENT_BOLETO  BOLETO WITH(NOLOCK)     
  on  ACT.CUSTOMER_ID=BOLETO.CUSTOMER_ID    
  AND  ACT.POLICY_ID=BOLETO.POLICY_ID    
  AND ACT.POLICY_VERSION_ID=BOLETO.POLICY_VERSION_ID    
  AND ACT.ROW_ID=BOLETO.INSTALLEMT_ID    
  AND ACT.INSTALLMENT_NO=BOLETO.INSTALLMENT_NO    
  WHERE  UPPER(ISNULL(BOLETO.IS_ACTIVE,''Y''))!=''N'' AND 
  BOLETO.BOLETO_HTML IS NOT NULL AND --Itrack 685 
 ACT.CUSTOMER_ID= '''+ CAST(@CUSTOMER_ID AS NVARCHAR)  + '''    
 AND ACT.POLICY_ID = '''+ CAST(@POLICY_ID AS NVARCHAR) + ''''    
 if(@POLICY_VERSION_ID is not null)
 BEGIN 
	set @query = @query + ' AND ACT.POLICY_VERSION_ID =  '+ CAST(@POLICY_VERSION_ID AS NVARCHAR)   
 end
 IF @INSTALLEMT_ID is not NULL     
   BEGIN       
     
   set @query = @query +        
   ' AND INSTALLEMT_ID=' + CAST(@INSTALLEMT_ID AS NVARCHAR)     
 END      
     
   IF @INSTALLMENT_NO is not NULL      
   BEGIN       
     
   set @query = @query +        
   ' AND ACT.INSTALLMENT_NO=' + CAST(@INSTALLMENT_NO AS NVARCHAR)     
   
 END  
 if(@CO_APPLICANT_ID IS NOT NULL)
 BEGIN
   set @query = @query +        
   ' AND ACT.CO_APPLICANT_ID=' + CAST(@CO_APPLICANT_ID AS NVARCHAR)     
 
 END 
   set @query = @query + ' order by POLICY_VERSION_ID,INSTALLMENT_NO '
  
Exec (@query)     
     
END     
ELSE IF(UPPER(@CALLED_FROM)='INSTALLMENT')--Get Installemet details to generate boletos 
BEGIN
--Regenerate boleto process - if the REGENERATE_PAID_BOLETO column value is 1 then get both paid and unpaid installment details data 
--else get only unpaid installment details data -iTrack -1361

SELECT @REGENERATE_PAID_BOLETO=CASE WHEN REGENERATE_PAID_BOLETO =1 THEN NULL ELSE 0 END FROM MNT_SYSTEM_PARAMS WITH(NOLOCK)--iTrack-1361 

SELECT ACT.POLICY_ID,ACT.POLICY_VERSION_ID,ACT.CUSTOMER_ID,APP_ID,APP_VERSION_ID,INSTALLMENT_AMOUNT,    
  INSTALLMENT_EFFECTIVE_DATE,RELEASED_STATUS,ACT.ROW_ID,ACT.INSTALLMENT_NO,INTEREST_AMOUNT,FEE,TAXES,TOTAL ,
  ACT.CO_APPLICANT_ID
  FROM ACT_POLICY_INSTALLMENT_DETAILS AS ACT  WITH(NOLOCK) 
  left outer join POL_POLICY_PROCESS PPP with(nolock) on  ACT.CUSTOMER_ID= PPP.CUSTOMER_ID  AND
		ACT.POLICY_ID = PPP.POLICY_ID AND ACT.POLICY_VERSION_ID = PPP.NEW_POLICY_VERSION_ID 
		        
  WHERE  ACT.CUSTOMER_ID= @CUSTOMER_ID  AND
		ACT.POLICY_ID = @POLICY_ID AND 
		ACT.POLICY_VERSION_ID = @POLICY_VERSION_ID
		AND ISNULL(ACT.RELEASED_STATUS,'N')<>'U'  --Added by aditya on 07-09-2011 for ITrack # 1573 
		AND (@REGENERATE_PAID_BOLETO IS NULL OR ISNULL(ACT.RELEASED_STATUS,'N')='N' )--Do not get installment data of paid boleto (installment)iTrack 1383,1361
 AND POLICY_CURRENT_STATUS<>'UREVERT' AND PROCESS_STATUS<>'ROLLBACK'  
END
END 



GO

