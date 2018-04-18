CREATE PROCEDURE [dbo].[Proc_CLM_ClaimTask_Save]  
 @p_TaskNo [int],  
 @p_ClaimID [int],  
 @p_ActionDue [datetime],  
 @p_CloseDate [datetime],  
 @p_PromtDetails [nvarchar](100),  
 @p_isApprove [int],  
 @p_ApproveDate [datetime],  
 @p_ApproveBy [nvarchar](25),  
 @p_ModifiedDate [datetime],  
 @p_CreatedDate [datetime],  
 @p_CreatedBy [nvarchar](25),  
 @p_ModifiedBy [nvarchar](25),  
 @p_Remarks [nvarchar](500),  
 @AccidentClaimId [int],  
 @Id [int] output,
 @ClaimantNames nvarchar(255),
 @ClaimsOfficer nvarchar(200) 
WITH EXECUTE AS CALLER  
AS  
IF OBJECT_ID('tempdb..#mytemptable') IS NOT NULL      
    /*Then it exists*/      
  DROP TABLE #mytemptable      
      
  DECLARE @OldData xml      
  DECLARE @NewData xml      
  DECLARE @Tname nvarchar(50)      
  DECLARE @Pdetails nvarchar(50)      
  DECLARE @CreatedBy nvarchar(50)      
  DECLARE @ClaimID int      
  DECLARE @ChangedFiled nvarchar(4000)      
  SET @Tname = 'CLM_ClaimTask'      
  DECLARE @Nodeval TABLE (      
    Nodevalue varchar(30) NULL      
  )      
BEGIN      
SET FMTONLY OFF;        
      
Insert into CLM_ClaimTask (TaskNo,ClaimID,ActionDue,CloseDate,PromtDetails,isApprove,ApproveDate,ApproveBy,ModifiedDate,CreatedDate,CreatedBy,ModifiedBy,Remarks,AccidentClaimId,ClaimantNames,ClaimsOfficer)      
            values(@p_TaskNo,@p_ClaimID,@p_ActionDue,@p_CloseDate,@p_PromtDetails,1,@p_ApproveDate,@p_ApproveBy,@p_ModifiedDate,getdate(),@p_CreatedBy,@p_ModifiedBy,@p_Remarks,@AccidentClaimId,@ClaimantNames,@ClaimsOfficer)      
SELECT @Id = Id FROM CLM_ClaimTask WHERE ID = @@Identity;     
    
SELECT * INTO #mytemptable FROM CLM_ClaimTask where ClaimID=@p_ClaimID and TaskNo=@p_TaskNo      
SET @Pdetails = (SELECT Lookupdesc  FROM MNT_Lookups where Lookupvalue=@p_PromtDetails and Category='TASKTYPE')      
Update #mytemptable set PromtDetails=@Pdetails where AccidentClaimId=@AccidentClaimId and TaskNo=@p_TaskNo      
       
        SET @NewData = (SELECT *  FROM #mytemptable AS CLM_ClaimTask  FOR xml AUTO, ELEMENTS)      
  --SELECT @NewData      
        SET @ClaimID = (SELECT TOP 1  ClaimID  FROM #mytemptable)      
        SET @CreatedBy = (SELECT TOP 1 CreatedBy  FROM #mytemptable)       
      
INSERT INTO [MNT_TransactionAuditLog] ([TimeStamp], [TableName], [UserName], [Actions], [OldData], [NewData],[ChangedColumns], [TansDescription], [ClaimID])      
VALUES (GETDATE(), 'CLM_ClaimTask', @CreatedBy, 'I', '', @NewData, '', 'Record Inserted In CLM_ClaimTask', @ClaimID)      
        
END


GO


