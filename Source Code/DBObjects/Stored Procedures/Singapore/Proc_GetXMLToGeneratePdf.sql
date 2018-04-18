  
  
/*-----------------------------------------------------------------------        
 Created By     : Naveen Pujari  
 Created date   : 01/Dec/2010            
 Purpose        : Fetch XML To geerate Pdf   
 used in        : EbixAdvantage            
     
   --drop proc Proc_GetXMLToGeneratePdf   27986,10,2,'CLM_RECEIPT',733 ,6,1 ,'CLM_HO'         
   Exec  Proc_GetXMLToGeneratePdf 2156, 77, 1  
   select  * from ACT_PREMIUM_NOTICE_PROCCESS_LOG  
-----------------------------------------------------------------------*/       
  
  
ALTER procedure [dbo].[Proc_GetXMLToGeneratePdf] 
(        
 @Customer_Id INT =0,            
 @Policy_ID INT =0,            
 @Policy_Version_ID INT=0 ,  
 @Document_Code varchar(20)='',   
 @Claim_Id  int=0,       
 @Activity_Id int=0,  
 @PAYEE_ID int =0 ,  
 @Entity_Type varchar(20)=''   
)                              
 AS        
 BEGIN       
  if(@Document_Code='CLM_RECEIPT' OR @Document_Code='CLM_REMIND')  
     BEGIN     
   SELECT PRT_JOB.ENTITY_TYPE,PRT_JOB.FILE_NAME,  PRT_JOB.CUSTOMER_ID,PRT_JOB.POLICY_ID,PRT_JOB.POLICY_VERSION_ID,CPL.PAYEE_ID,CPL.DOC_TEXT  FROM  CLM_PROCESS_LOG  CPL WITH(NOLOCK)  
      join PRINT_JOBS PRT_JOB WITH(NOLOCK) ON CPL.PROCESS_TYPE=PRT_JOB.DOCUMENT_CODE AND CPL.PAYEE_ID=PRT_JOB.ENTITY_ID and  
      CPL.CLAIM_ID =PRT_JOB.CLAIM_ID  AND CPL.ACTIVITY_ID=PRT_JOB.ACTIVITY_ID  AND CPL.PAYEE_ID=PRT_JOB.ENTITY_ID  
     WHERE   
     CPL.CLAIM_ID =@Claim_Id AND CPL.ACTIVITY_ID=@Activity_Id  AND CPL.PAYEE_ID=@PAYEE_ID AND CPL.PROCESS_TYPE=@Document_Code  and PRT_JOB.ENTITY_TYPE=@Entity_Type  
       
     END  
       
     ELSE  
     BEGIN  
     SELECT  PNP.CUSTOMER_ID,PNP.POLICY_ID,PNP.DEC_CUSTOMERXML,PNP.POLICY_VERSION_ID,PNP.CALLED_FOR,    
     PNP.IS_FILE_GENERATED FROM  ACT_PREMIUM_NOTICE_PROCCESS_LOG PNP WITH(NOLOCK)    
     WHERE PNP.CUSTOMER_ID=@Customer_Id AND PNP.POLICY_ID=@Policy_ID    
     AND PNP.POLICY_VERSION_ID=@Policy_Version_ID     
     ORDER BY PNP.CUSTOMER_ID,PNP.POLICY_ID,PNP.POLICY_VERSION_ID    
     END  
         
 END     
  
  
  