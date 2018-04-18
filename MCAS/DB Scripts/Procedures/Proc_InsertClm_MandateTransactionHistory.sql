IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertClm_MandateTransactionHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertClm_MandateTransactionHistory]
GO

CREATE PROCEDURE [dbo].[Proc_InsertClm_MandateTransactionHistory]
@p_PaymentId int,
@p_MandateId int,
@p_AccidentClaimId int,
@p_ClaimID int,
@p_ComponentID nchar(10),
@p_Initial decimal,
@p_ReserveMovement decimal,
@p_Outstanding decimal  
AS
SET FMTONLY OFF;    
BEGIN
IF EXISTS(select top 1* from Clm_MandateTransactionHistory where MandateId=@p_MandateId and ComponentID=@p_ComponentID and PaymentId = @p_PaymentId)
Begin
update Clm_MandateTransactionHistory set [Initial]=@p_Initial,ReserveMovement=@p_ReserveMovement,Outstanding=@p_Outstanding where MandateId=@p_MandateId and ComponentID=@p_ComponentID and PaymentId = @p_PaymentId
End
ELSE
Begin
Insert into Clm_MandateTransactionHistory (MandateId,ComponentID,[Initial],ReserveMovement,Outstanding,PaymentId,AccidentClaimId,ClaimID)
values(@p_MandateId,@p_ComponentID,@p_Initial,@p_ReserveMovement,@p_Outstanding,@p_PaymentId,@p_AccidentClaimId,@p_ClaimID)
End
END

GO