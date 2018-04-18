IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateInstall_Plan_Data_BoletoReprint]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateInstall_Plan_Data_BoletoReprint]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Pradeep Kushwaha>
-- Create date: <07-Dec-2010>
-- Description:	<Update Plan Data table while Boleto reprint>
-- Drop Proc Proc_UpdateInstall_Plan_Data_BoletoReprint    
-- =============================================
CREATE PROC [dbo].[Proc_UpdateInstall_Plan_Data_BoletoReprint]
	-- Add the parameters for the stored procedure here
@CUSTOMER_ID   INT,                         
@POLICY_ID  INT,                        
@POLICY_VERSION_ID SMALLINT,             
@MODIFIED_BY INT,    
@LAST_UPDATED_DATETIME DATETIME  ,
@TOTAL_AMOUNT  DECIMAL(12,2),
@TOTAL_INTEREST_AMOUNT  DECIMAL(12,2)

AS
BEGIN
 DECLARE @CURRENT_TERM INT 
 
 SELECT @CURRENT_TERM=CURRENT_TERM 
 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND 
 POLICY_ID=@POLICY_ID AND 
 POLICY_VERSION_ID=@POLICY_VERSION_ID 

 UPDATE ACT_POLICY_INSTALL_PLAN_DATA SET 
	TOTAL_INTEREST_AMOUNT=TOTAL_INTEREST_AMOUNT+@TOTAL_INTEREST_AMOUNT, 
    TOTAL_AMOUNT=TOTAL_AMOUNT+@TOTAL_AMOUNT, 
    MODIFIED_BY=@MODIFIED_BY,    
    LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
    WHERE POLICY_VERSION_ID IN( 
    SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID 
    AND POLICY_VERSION_ID >= @POLICY_VERSION_ID AND CURRENT_TERM=@CURRENT_TERM 
    )
    exec Proc_Reprint_Boleto_Commit @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID ,@MODIFIED_BY
    
   IF(@@ERROR<>0)    
    RETURN -1    
END


GO

