IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_PrintJobsFor_BoletoAndPolicyDoc_Navin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_PrintJobsFor_BoletoAndPolicyDoc_Navin]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[Proc_Update_PrintJobsFor_BoletoAndPolicyDoc_Navin]        
        
               
@CUSTOMER_ID int=NULL,      
@POLICY_ID int=NULL,                
@POLICY_VERSION_ID int=NULL,         
@PRINT_JOBS_ID INT=NULL ,      
@STATUS BIT  ,    
@Claim_Id INT,    
@Activity_Id INT,    
@Document_Code varchar(20)='' ,  
@ChkOntheFly int  
      
AS        
BEGIN        
         
  SET NOCOUNT ON;        
    
  DECLARE @GENERATED_FROM   NVARCHAR(20)  
  SET @GENERATED_FROM=@ChkOntheFly  
  --This block is executed when there is not Exception  
  IF @STATUS=1        
   BEGIN      
       
   if(@Document_Code='CLM_RECEIPT' OR @Document_Code='CLM_REMIND')    
   begin    
      
     
     UPDATE PRINT_JOBS   SET IS_PROCESSED=0, PRINTED_DATETIME=GETDATE(),  
     GENERATED_FROM =  
     CASE WHEN @GENERATED_FROM=2 THEN   
     'GENERATED_ON_THE_FLY' ELSE  'GENERATED_BY_SERVICE' END  WHERE        
     PRINT_JOBS_ID=@PRINT_JOBS_ID    
     AND CLAIM_ID=@Claim_Id AND ACTIVITY_ID=@Activity_Id  AND PRINT_JOBS_ID=@PRINT_JOBS_ID  
    end    
    else    
    begin    
       
      UPDATE PRINT_JOBS   SET IS_PROCESSED=0, PRINTED_DATETIME=GETDATE(),  
      GENERATED_FROM =  
      CASE WHEN @GENERATED_FROM=2 THEN   
     'GENERATED_ON_THE_FLY' ELSE  'GENERATED_BY_SERVICE' END WHERE  CUSTOMER_ID=@CUSTOMER_ID         
      AND  POLICY_ID=  @POLICY_ID  AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND PRINT_JOBS_ID=@PRINT_JOBS_ID      
    end    
    end    
        
   ELSE      
     --This block is executed when there is some Exception on Executing the code  
       if(@Document_Code='CLM_RECEIPT' OR @Document_Code='CLM_REMIND')    
    begin   
     if(@ChkOntheFly<>2)   
     UPDATE PRINT_JOBS   SET ATTEMPTS=0 WHERE         
     PRINT_JOBS_ID=@PRINT_JOBS_ID    
     AND CLAIM_ID=@Claim_Id AND ACTIVITY_ID=@Activity_Id   AND PRINT_JOBS_ID=@PRINT_JOBS_ID  
    end    
    else  
      BEGIN     
      if(@ChkOntheFly<>2)    
       UPDATE PRINT_JOBS   SET ATTEMPTS=0 WHERE  CUSTOMER_ID=@CUSTOMER_ID         
       AND  POLICY_ID=@POLICY_ID  AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND PRINT_JOBS_ID=@PRINT_JOBS_ID       
       END      
            
END   
GO

