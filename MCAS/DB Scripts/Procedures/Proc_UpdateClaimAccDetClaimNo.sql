IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Proc_UpdateClaimAccDetClaimNo')AND type in (N'P', N'PC'))
DROP PROCEDURE Proc_UpdateClaimAccDetClaimNo
GO

 CREATE PROCEDURE Proc_UpdateClaimAccDetClaimNo
@p_ClaimNo nvarchar(100) ,
@w_AccidentClaimId int 
AS
SET FMTONLY OFF;  

BEGIN
UPDATE [dbo].ClaimAccidentDetails SET ClaimNo=@p_ClaimNo 
	WHERE AccidentClaimId=@w_AccidentClaimId
END
GO
