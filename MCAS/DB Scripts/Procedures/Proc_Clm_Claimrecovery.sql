


CREATE PROCEDURE [dbo].[Proc_Clm_Claimrecovery] @AccidentClaimId INT,
												@PolicyId INT,
												@ClaimentId INT,
												@UserName       NVARCHAR(50), 
                                                @Address        NVARCHAR(max), 
                                                @RecoveryReason NVARCHAR(500), 
                                                @CurrCode       NCHAR(10), 
                                                @ClaimID         INT, 
                                                @ExchangeRate   NUMERIC, 
                                                @LocalCurrAmt   NUMERIC, 
                                                @Status         INT, 
                                                @PaymentDetails NVARCHAR(200), 
                                                @CreatedBy      NVARCHAR(25), 
                                                @ModifiedBy     NVARCHAR(25) 
AS 
    INSERT INTO clm_claimrecovery 
				([AccidentClaimId],
				 [PolicyId],
				 [ClaimentId],
                 [RecoverFrom],   
                 [Address1],   
                 [recoveryreason],   
                 --[currcode],  
                 [ClaimID],   
                 --[exchangerate],   
                 --[localcurramt],   
                 --[status],   
                 --[paymentdetails],   
                 --[isactive],   
                 [createddate],   
                 [createdby],   
                 [modifiedby],   
                 [modifieddate])   
    VALUES      (@AccidentClaimId,
				 @PolicyId,
				 @ClaimentId,
				 @UserName,   
                 @Address,   
                 @RecoveryReason,   
                 --@CurrCode,  
                 @ClaimID,   
                 --@ExchangeRate,   
                 --@LocalCurrAmt,   
                 --@Status,   
                 --@PaymentDetails,   
                 --'Y',   
                 Getdate(),   
                 @CreatedBy,   
                 @ModifiedBy,   
                 Getdate())



GO


