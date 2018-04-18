CREATE PROCEDURE [dbo].[Proc_SaveReserve]  
(  
 @ReserveSummary AS [dbo].[TVP_ReserveSummary] READONLY,  
 @ReserveDetailsList AS [dbo].[TVP_ReserveDetails] READONLY,  
 @XMLSummary NVARCHAR(MAX)=NULL,  
 @XMLDetails NVARCHAR(MAX)=NULL,  
 @ActionSummary NVARCHAR(10),  
 @ActionDetails NVARCHAR(10)  
)  
AS  
BEGIN  
 DECLARE @reserveId INT  
 DECLARE @AccidentClaimId [INT]  
 DECLARE @ClaimID [INT]  
 DECLARE @UserId NVARCHAR(100)  
   
 BEGIN TRANSACTION;  
 BEGIN TRY   
   
 SELECT @AccidentClaimId=AccidentClaimId,@ClaimID=ClaimID,@UserId=Createdby FROM @ReserveSummary        
   
 --First Step:  
 --INSERTING INTO CLM_ReserveSummary TABLE...  
 INSERT INTO CLM_ReserveSummary(AccidentClaimId,ClaimID,ClaimType,MovementType,InitialReserve,PreReserve,MovementReserve,CurrentReserve,Createdby,  
  Createddate,Modifiedby,Modifieddate,IsActive)   
 SELECT AccidentClaimId,ClaimID,ClaimType,MovementType,InitialReserve,PreReserve,MovementReserve,CurrentReserve,Createdby,  
  Createddate,Modifiedby,Modifieddate,IsActive  
 FROM @ReserveSummary  
   
 SET @reserveId=SCOPE_IDENTITY()   
   
 --Second Step:  
 --INSERTING INTO CLM_ReserveDetails TABLE using reserveId of CLM_ReserveSummary Table...   
 INSERT INTO CLM_ReserveDetails(ReserveId,CmpCode,InitialReserve,PreReserve,MovementReserve,CurrentReserve,InitialNoofdays,  
  MovementNoofdays,CurrentNoofdays,InitialRateperday,MovementlRateperday,CurrentRateperday,Createdby,Createddate,  
  IsActive,AccidentClaimId,ClaimID,MovementType)  
 SELECT @reserveId,CmpCode,InitialReserve,PreReserve,MovementReserve,CurrentReserve,InitialNoofdays,MovementNoofdays,  
  CurrentNoofdays,InitialRateperday,MovementlRateperday,CurrentRateperday,Createdby,Createddate,IsActive,  
  AccidentClaimId,ClaimID,MovementType   
 FROM @ReserveDetailsList  
   
 --Third Step:  
 --CREATE XML  
   
 EXEC Proc_InsertTransactionAuditLog 'CLM_ReserveSummary','ReserveId',@UserId,@ClaimID,@AccidentClaimId,@ActionSummary,@XMLSummary  
 EXEC Proc_InsertTransactionAuditLog 'CLM_ReserveDetails','ReserveDetailID',@UserId,@ClaimID,@AccidentClaimId,@ActionDetails,@XMLDetails  
   
 END TRY  
 BEGIN CATCH  
  IF @@TRANCOUNT > 0  
        ROLLBACK TRANSACTION;  
          
        DECLARE @ErrorProcedure nvarchar(100)  
        DECLARE @ErrorMessage nvarchar(MAX)  
        DECLARE @entityType nvarchar(100)          
  DECLARE @SNo [INT]          
          
  SELECT @entityType=[Type] from MNT_TableDesc where TableName='CLM_ReserveSummary'           
  SELECT @ErrorProcedure=ERROR_PROCEDURE(),@ErrorMessage=ERROR_MESSAGE()    
  select @SNo=SNo from MNT_Users where UserId=@UserId  
        
  EXEC Proc_InsertExceptionLog @ErrorMessage,null,@AccidentClaimId,null,null,@ClaimID,null,@entityType,@ErrorProcedure,@ErrorMessage,null,null,null,'MCAS',@SNo,'SqlException'     
                 
 END CATCH  
   
 IF @@TRANCOUNT > 0  
 BEGIN  
  COMMIT TRANSACTION;  
  --RETURN 1;  
  --SELECT @reserveId;      
 END  
 ELSE  
 BEGIN  
  --RETURN 0;  
  --SELECT 0;  
  SET @reserveId=0  
 END  
   
 RETURN @reserveId  
END  
  