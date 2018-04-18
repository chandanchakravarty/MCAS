IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertLitigationInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertLitigationInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                                            
Proc Name             : Dbo.Proc_InsertLitigationInformation                                                            
Created by            : Santosh Kumar Gautam                                                           
Date                  : 11/11/2010                                                           
Purpose               : To insert the litigation information details                  
Revison History       :                                                            
Used In               : claim module                  
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc Proc_InsertLitigationInformation                                                   
------   ------------       -------------------------*/                                                            
--                               
                                
--                             
                          
CREATE PROCEDURE [dbo].[Proc_InsertLitigationInformation]                                
                 
 @LITIGATION_ID     int OUTPUT                  
,@CLAIM_ID        int                  
,@JUDICIAL_PROCESS_NO nvarchar(20)            
,@JUDICIAL_COMPLAINT_STATE int            
,@PLAINTIFF_NAME nvarchar(40)            
,@PLAINTIFF_CPF nvarchar(40)          
,@PLAINTIFF_REQUESTED_AMOUNT decimal(14,2)            
,@DEFEDANT_OFFERED_AMOUNT decimal(14,2)           
,@ESTIMATE_CLASSIFICATION int            
,@OPERATION_REASON int            
,@JUDICIAL_PROCESS_DATE datetime          
,@EXPERT_SERVICE_ID int -- FOR LAWYER          
,@CREATED_BY int            
,@CREATED_DATETIME datetime            
            
                                
AS                                
BEGIN                     
                    
  SELECT @LITIGATION_ID=(ISNULL(MAX([LITIGATION_ID]),0)+1)  FROM [dbo].[CLM_LITIGATION_INFORMATION]            
              
 INSERT INTO [dbo].[CLM_LITIGATION_INFORMATION]            
           ([CLAIM_ID]                                
           ,[LITIGATION_ID]            
           ,[JUDICIAL_PROCESS_NO]            
           ,[JUDICIAL_COMPLAINT_STATE]            
           ,[PLAINTIFF_NAME]            
           ,[PLAINTIFF_CPF]            
           ,[PLAINTIFF_REQUESTED_AMOUNT]            
           ,[DEFEDANT_OFFERED_AMOUNT]            
           ,[ESTIMATE_CLASSIFICATION]            
           ,[OPERATION_REASON]           
           ,[JUDICIAL_PROCESS_DATE]           
           ,[EXPERT_SERVICE_ID]          
           ,[IS_ACTIVE]            
           ,[CREATED_BY]            
           ,[CREATED_DATETIME]            
           )            
     VALUES            
           (            
            @CLAIM_ID            
           ,@LITIGATION_ID            
           ,@JUDICIAL_PROCESS_NO            
           ,@JUDICIAL_COMPLAINT_STATE            
           ,@PLAINTIFF_NAME            
           ,@PLAINTIFF_CPF            
           ,@PLAINTIFF_REQUESTED_AMOUNT            
           ,@DEFEDANT_OFFERED_AMOUNT            
           ,@ESTIMATE_CLASSIFICATION            
           ,@OPERATION_REASON            
           ,@JUDICIAL_PROCESS_DATE          
           ,@EXPERT_SERVICE_ID          
           ,'Y'            
           ,@CREATED_BY            
           ,@CREATED_DATETIME            
           )            
            
                    
END 
GO

