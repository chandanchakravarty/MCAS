IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckDuplicatePolAppNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckDuplicatePolAppNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sonal
-- Create date: 19 july 2010
-- Description:	to check wether application or policy number exists in a system or not
-- =============================================
-- DROP PROCEDURE  dbo.Proc_CheckDuplicatePolAppNumber '889982010210196000231','APP'

CREATE PROCEDURE [dbo].[Proc_CheckDuplicatePolAppNumber]
	@POLICYAPP_NUMBER VARCHAR(150),
	@CALLED_FROM NVARCHAR(10) = NULL,
	@POLICY_LOB int  , 
	@ISSUE_DATE DATETIME  ,  
	@RETURN INT OUT
	
AS
BEGIN
 DECLARE @ISSUE_YEAR VARCHAR(4)
 
SET @ISSUE_YEAR = CONVERT(VARCHAR,YEAR(@ISSUE_DATE))  
    IF (@CALLED_FROM='APP')
    BEGIN
		IF EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  WHERE APP_NUMBER=@POLICYAPP_NUMBER AND POLICY_LOB=@POLICY_LOB AND  substring(app_number,6,4)=@ISSUE_YEAR)
			BEGIN
			SET @RETURN = 1
			RETURN @RETURN
			END
		ELSE
			BEGIN
			SET @RETURN =0
			RETURN @RETURN
			END
	END
	ELSE
	BEGIN
		IF EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  WHERE POLICY_NUMBER=@POLICYAPP_NUMBER AND POLICY_LOB=@POLICY_LOB AND  substring(app_number,6,4)=@ISSUE_YEAR)	
		BEGIN
		    SET @RETURN = 1
			RETURN @RETURN
		END
		ELSE
		BEGIN
			 SET @RETURN =0
			 RETURN @RETURN
		END
	END
END





GO

