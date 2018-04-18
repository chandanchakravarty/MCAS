IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Fetch_DocCodePolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Fetch_DocCodePolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------                                
--Proc Name        :[Proc_Fetch_DocCodePolicy]                   
--Created by       : Naveen Pujari                           
--Date             : 28/March/2011                               
--Purpose          : Retrieving data from PRINT_JOBS  for Generating Document on the Fly                                          
--Used In          : Ebix Advantage        
--                 : select * from  PRINT_JOBS                      
--------------------------------------------------------------   
   
  -- exec [Proc_Fetch_DocCodePolicy]  
  -- drop proc Proc_Fetch_DocCodePolicy 28236,95,1,'CLM_RECEIPT',935,5  
  -- CUSTOMER_ID=28236 and POLICY_ID=95  and POLICY_VERSION_ID=1 and CLAIM_ID=935 and ACTIVITY_ID=2  
CREATE PROCEDURE [dbo].[Proc_Fetch_DocCodePolicy]   
       
  
@CustomerID INT,  
@PolicyID INT,  
@PolicyVersionID INT,  
@DocumentCode NVARCHAR(50)=NULL,  
@ClaimID INT = NULL,  
@ActivityID INT= NULL ,
--print job_ID is applied to generate selected document
@Print_Job_ID INT =NULL
 
   
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
   
 SET NOCOUNT ON;  
     
   IF(@ClaimID='' OR @ClaimID<1)  
      SET @ClaimID = NULL  
        
   IF(@ActivityID='' OR @ActivityID<1)  
      SET @ActivityID =0  
   IF(@Print_Job_ID<1)  
      SET @Print_Job_ID =null       
       
     
     --IF(@ClaimID='' OR @ClaimID IS NULL OR @ClaimID<1)  
     if(@DocumentCode is null or @DocumentCode='')  
     BEGIN  
       SELECT  P_JOBS.CUSTOMER_ID , P_JOBS.POLICY_ID,P_JOBS.POLICY_VERSION_ID,  P_JOBS.PRINT_JOBS_ID,  
       CASE WHEN (LEN(P_JOBS.ClAIM_ID) <1 or LEN(P_JOBS.ClAIM_ID)  is null) THEN 0 ELSE P_JOBS.ClAIM_ID END AS ClAIM_ID  
      ,CASE WHEN (LEN(P_JOBS.ACTIVITY_ID) <1 or LEN(P_JOBS.ACTIVITY_ID)  is null) THEN 0 ELSE P_JOBS.ACTIVITY_ID END AS ACTIVITY_ID,  
       CASE WHEN (LEN(P_JOBS.ENTITY_ID) <1 or LEN(P_JOBS.ENTITY_ID)  is null) THEN 0 ELSE P_JOBS.ENTITY_ID END AS ENTITY_ID,  
        P_JOBS.DOCUMENT_CODE, P_JOBS.ENTITY_TYPE  ,P_JOBS.URL_PATH,P_JOBS.FILE_NAME,P_JOBS.ATTEMPTS    
        from   PRINT_JOBS P_JOBS  WITH(NOLOCK)   WHERE  CUSTOMER_ID=@CustomerID and POLICY_ID=@PolicyID and POLICY_VERSION_ID=@PolicyVersionID    AND  (@Print_Job_ID IS NULL OR PRINT_JOBS_ID=@Print_Job_ID )   
      END  
      ELSE -- FOR CLAIM RECEIPT AND LETTERS  
      BEGIN  
        
       SELECT  P_JOBS.CUSTOMER_ID , P_JOBS.POLICY_ID,P_JOBS.POLICY_VERSION_ID,  P_JOBS.PRINT_JOBS_ID,  
       CASE WHEN (LEN(P_JOBS.ClAIM_ID) <1 or LEN(P_JOBS.ClAIM_ID)  is null) THEN 0 ELSE P_JOBS.ClAIM_ID END AS ClAIM_ID  
      ,CASE WHEN (LEN(P_JOBS.ACTIVITY_ID) <1 or LEN(P_JOBS.ACTIVITY_ID)  is null) THEN 0 ELSE P_JOBS.ACTIVITY_ID END AS ACTIVITY_ID,  
       CASE WHEN (LEN(P_JOBS.ENTITY_ID) <1 or LEN(P_JOBS.ENTITY_ID)  is null) THEN 0 ELSE P_JOBS.ENTITY_ID END AS ENTITY_ID,  
       P_JOBS.DOCUMENT_CODE, P_JOBS.ENTITY_TYPE  ,P_JOBS.URL_PATH,P_JOBS.FILE_NAME,P_JOBS.ATTEMPTS    
       from   PRINT_JOBS P_JOBS  WITH(NOLOCK)    
       WHERE CUSTOMER_ID=@CustomerID and POLICY_ID=@PolicyID and POLICY_VERSION_ID=@PolicyVersionID and DOCUMENT_CODE=@DocumentCode AND  
              (@ClaimID IS NULL OR CLAIM_ID = @ClaimID) AND    
              (@ActivityID IS NULL OR ACTIVITY_ID = @ActivityID)   AND  (@Print_Job_ID IS NULL OR PRINT_JOBS_ID=@Print_Job_ID )      
         
      END  
        
  
END   
  

GO

