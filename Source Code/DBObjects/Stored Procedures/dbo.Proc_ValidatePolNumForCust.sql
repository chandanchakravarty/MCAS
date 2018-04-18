IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ValidatePolNumForCust]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ValidatePolNumForCust]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*********************************************************  
CREATED BY   		: Swastika 
CREATED DATETIME 	: 23 Aug'2007  
PURPOSE    			: Validate Policy Number entered for a customer  

exec Proc_ValidatePolNumForCust   @CUSTOMER_ID=1199,@POLICY_NUMBER='W1002622'

**********************************************************/  
-- drop proc dbo.Proc_ValidatePolNumForCust  

CREATE PROCEDURE dbo.Proc_ValidatePolNumForCust  
(  
 @CUSTOMER_ID INT,  
 @POLICY_NUMBER Varchar(20)
)  
AS  
BEGIN  
	SELECT COUNT(POLICY_NUMBER) POLICY_NUMBER_COUNT FROM POL_CUSTOMER_POLICY_LIST
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_NUMBER LIKE '%' + @POLICY_NUMBER + '%'
END  









GO

