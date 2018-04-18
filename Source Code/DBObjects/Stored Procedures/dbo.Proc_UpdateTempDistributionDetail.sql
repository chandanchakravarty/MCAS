IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateTempDistributionDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateTempDistributionDetail]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_UpdateTempDistributionDetail
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

/*----------------------------------------------------------
Proc Name          : Dbo.Proc_UpdateDistributionDetail
Created by           :ajit
Date                    : 13/9/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_UpdateTempDistributionDetail
(
       @IDEN_ROW_ID int,     
       @ACCOUNT_ID int,
       @DISTRIBUTION_PERCT decimal(18,2),
       @DISTRIBUTION_AMOUNT decimal(18,2) ,
       @NOTE nvarchar(255),
       @MODIFIED_BY int
		
)
AS
BEGIN
UPDATE  TEMP_ACT_DISTRIBUTION_DETAILS 
SET DISTRIBUTION_PERCT= @DISTRIBUTION_PERCT,
DISTRIBUTION_AMOUNT= @DISTRIBUTION_AMOUNT,
NOTE= @NOTE,
MODIFIED_BY=@MODIFIED_BY,
LAST_UPDATED_DATETIME= GETDATE() 
WHERE 
IDEN_ROW_ID=@IDEN_ROW_ID
END





GO

