USE [CDGI]
GO

/****** Object:  StoredProcedure [dbo].[Proc_GetMaxDeductibleRecoveryList]    Script Date: 03/31/2015 15:56:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMaxDeductibleRecoveryList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMaxDeductibleRecoveryList]
GO

USE [CDGI]
GO

/****** Object:  StoredProcedure [dbo].[Proc_GetMaxDeductibleRecoveryList]    Script Date: 03/31/2015 15:56:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_GetMaxDeductibleRecoveryList]
(
@AccidentId int ,
@ClaimType int 
)
AS
BEGIN
	SET NOCOUNT ON;
DECLARE @deductAmout NUMERIC(18,2),@i INT,@PaymentAmt INT,@numrows INT
DECLARE @totalpay NUMERIC(18,2) = 0.00
DECLARE @tmptable TABLE (
    idx SMALLINT PRIMARY KEY IDENTITY(1,1)
    , PaymentId INT,
    total_Amt NUMERIC(18,2)
)
DECLARE @finaltable TABLE (
     PaymentId INT
)
SET @deductAmout = (SELECT c.DeductibleAmt FROM ClaimAccidentDetails a 
   JOIN MNT_OrgCountry b ON a.Organization = b.id 
   JOIN MNT_Deductible c ON b.InsurerType = c.OrgCategory and b.CountryOrgazinationCode = c.OrgCategoryName
   WHERE AccidentClaimId = @AccidentId)
   
INSERT @tmptable
SELECT DISTINCT PaymentId,Total_D FROM CLM_Payment WHERE AccidentClaimId = @AccidentId and ClaimType = @ClaimType

SET @i = 1
SET @numrows = (SELECT COUNT(*) FROM @tmptable)
IF @numrows > 0
 begin
    WHILE (@i <= (SELECT MAX(idx) FROM @tmptable))
    BEGIN

        SET @PaymentAmt = (SELECT total_Amt FROM @tmptable WHERE idx = @i)
		SET @totalpay =  @totalpay + @PaymentAmt 
        IF @deductAmout < @totalpay
       insert into @finaltable SELECT PaymentId FROM @tmptable WHERE  idx = @i
		
        SET @i = @i + 1
    END
  end
  
  select * from @finaltable  
  
END





GO


