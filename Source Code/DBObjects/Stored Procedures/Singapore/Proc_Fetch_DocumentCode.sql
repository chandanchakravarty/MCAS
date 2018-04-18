    
    
--------------------------------------------------------------                                  
--Proc Name        : [Proc_Fetch_DocumentCode]                     
--Created by       : Naveen Pujari                             
--Date             : 01/December/2010                                  
--Purpose          : Retrieving data from PRINT_JOBS  for Document type , Path, Documentname                                             
--Used In          : Ebix Advantage          
--Modified by :Naveen Pujari, aug,08,2011(added new column picked_by for load balancing)    
--                 : select * from  PRINT_JOBS                        
--------------------------------------------------------------     
  -- select * from POL_INSTALLMENT_BOLETO     
  -- exec [Proc_Fetch_DocumentCode]    
  -- drop  procedure Proc_Fetch_DocumentCode    
      
ALTER PROCEDURE   [dbo].[Proc_Fetch_DocumentCode]     
       
    
       
    
AS    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
     
 SET NOCOUNT ON;    
     SET NOCOUNT ON;      
        
      SELECT TOP 10  P_JOBS.CUSTOMER_ID ,    
      CASE WHEN (LEN(P_JOBS.ClAIM_ID) <1 or LEN(P_JOBS.ClAIM_ID)  is null) THEN 0 ELSE P_JOBS.ClAIM_ID END AS ClAIM_ID    
       
      ,CASE WHEN (LEN(P_JOBS.ACTIVITY_ID) <1 or LEN(P_JOBS.ACTIVITY_ID)  is null) THEN 0 ELSE P_JOBS.ACTIVITY_ID END AS ACTIVITY_ID,    
       CASE WHEN (LEN(P_JOBS.ENTITY_ID) <1 or LEN(P_JOBS.ENTITY_ID)  is null) THEN 0 ELSE P_JOBS.ENTITY_ID END AS ENTITY_ID    
      ,P_JOBS.POLICY_ID,P_JOBS.POLICY_VERSION_ID,  P_JOBS.PRINT_JOBS_ID,      
      P_JOBS.DOCUMENT_CODE, P_JOBS.ENTITY_TYPE  ,P_JOBS.URL_PATH,P_JOBS.FILE_NAME,P_JOBS.ATTEMPTS        
      from   PRINT_JOBS P_JOBS  with(rowlock)  WHERE IS_PROCESSED=0 AND isnull(ATTEMPTS,0)<3 AND  isnull(PICKED_BY,'')=''      
          
          SELECT * FROM CLM_PARTIES WHERE CLAIM_ID in     
          (    
           SELECT TOP 100     
      CASE WHEN (LEN(P_JOBS.ClAIM_ID) <1 or LEN(P_JOBS.ClAIM_ID)  is null) THEN 0 ELSE P_JOBS.ClAIM_ID END AS ClAIM_ID    
       
          
      from   PRINT_JOBS P_JOBS  WITH(NOLOCK)   WHERE IS_PROCESSED=0 AND isnull(ATTEMPTS,0)<2     
              
          ) AND PARTY_TYPE_ID=619    
    
END     