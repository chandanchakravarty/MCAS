GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_LOCATION]    Script Date: 08/30/2011 18:22:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_POLICY_LOCATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_LOCATION]
GO


GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_LOCATION]    Script Date: 08/30/2011 18:22:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<ATUL KUMAR SINGH>
-- Create date: <2011-08-30>
-- Description:	<Insert Policy Location info in System>
-- =============================================
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_LOCATION]
	-- Add the parameters for the stored procedure here

-------------------------------- INPUT PARAMETER
@IMPORT_REQUEST_ID		INT,
@IMPORT_SERIAL_NO		INT,
@IMPORT_FILE_NO			INT

-------------------------------------------------

AS
BEGIN

-------------------------------- DECLARATION PART
----------------------------------------------------------------------------------------

DECLARE @CUSTOMER_ID INT
DECLARE @POLICY_ID INT
DECLARE @POLICY_VERSION_ID INT
DECLARE @LOCATION_ID INT
DECLARE @CREATED_BY INT=3 -- ADMINSTRATOR
----------------------------------------------------------------------------------------


--------------------------------- SET CUSTOMER ID, POLICY_ID AND POLICY_VERSION_ID
----------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------	


--------------------------------- SET LOCATION_ID
----------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------	


--------------------------------- INSERT INTO POL_LOCATION
----------------------------------------------------------------------------------------

INSERT INTO [dbo].[POL_LOCATIONS]
           ([CUSTOMER_ID]
           ,[POLICY_ID]
           ,[POLICY_VERSION_ID]
           ,[LOCATION_ID]
           ,[LOC_NUM]
           ,[IS_PRIMARY]
           ,[LOC_ADD1]
           ,[LOC_ADD2]
           ,[LOC_CITY]
           ,[LOC_COUNTY]
           ,[LOC_STATE]
           ,[LOC_ZIP]
           ,[LOC_COUNTRY]
           ,[PHONE_NUMBER]
           ,[FAX_NUMBER]
           ,[DEDUCTIBLE]
           ,[NAMED_PERILL]
           ,[DESCRIPTION]
           ,[IS_ACTIVE]
           ,[CREATED_BY]
           ,[CREATED_DATETIME]
           ,[MODIFIED_BY]
           ,[LAST_UPDATED_DATETIME]
           ,[LOC_TERRITORY]
           ,[LOCATION_TYPE]
           ,[RENTED_WEEKLY]
           ,[WEEKS_RENTED]
           ,[LOSSREPORT_ORDER]
           ,[LOSSREPORT_DATETIME]
           ,[REPORT_STATUS]
           ,[CAL_NUM]
           ,[NAME]
           ,[NUMBER]
           ,[DISTRICT]
           ,[OCCUPIED]
           ,[EXT]
           ,[CATEGORY]
           ,[ACTIVITY_TYPE]
           ,[CONSTRUCTION]
           ,[SOURCE_LOCATION_ID]
           ,[IS_BILLING]
           ,[CO_RISK_ID]
		   )

	SELECT	@CUSTOMER_ID
			,@POLICY_ID
			,@POLICY_VERSION_ID
			,@LOCATION_ID
			,LOCATION_CODE
			
			,'N'              --IS_PRIMARY
			,[ADDRESS]		  -- ADD1
			,NULL			  -- ADD1	
			,CITY
			,NULL			  -- LOCATION COUNTY
			,STATE
			,[ZIP CODE]
			,COUNTRY
			,PHONE
			,FAX
			,NULL
			,NULL
			,NULL
			,'Y'
			,3              -- CREATED BY
			,GETDATE()
			,NULL
			,NULL
			,NULL
			,0
			,NULL
			,NULL
			,NULL
			,NULL
			,NULL
			,NULL			-- CAL_NUM
			,BUILDING_NAME
			,NUMBER
			,DISTRICT
			,OCCUPIED_AS
			,EXT
			,NULL		-- CATEGORY
			,ACTIVITY_TYPES
			,0	-- CONSTRUCTION
			,NULL --SOURCE CONTROL ID
			,IS_BILLING_ADDRESS
			,NULL
	FROM	dbo.MIG_IL_POLICY_LOCATION_DETAILS WITH (NOLOCK)
	WHERE	IMPORT_REQUEST_ID   =   @IMPORT_REQUEST_ID
	AND		IMPORT_SERIAL_NO	=	@IMPORT_SERIAL_NO 
	AND		IS_PROCESSED		=	'N'	
----------------------------------------------------------------------------------------	
	


---------------------------------------------------------- UPDATE TABLE	MIG_IL_POLICY_LOCATION_DETAILS
-------------------------------------------------------------------------------------------------
	UPDATE		MIG_IL_POLICY_LOCATION_DETAILS 
	SET			POLICY_ID			=	@POLICY_ID
				,POLICY_VERSION_ID	=	@POLICY_VERSION_ID
				,CUSTOMER_ID		=	@CUSTOMER_ID
				,IS_PROCESSED		=	'Y'
				
				
	WHERE		IMPORT_REQUEST_ID	=	@IMPORT_REQUEST_ID 
	AND			IMPORT_SERIAL_NO	=	@IMPORT_SERIAL_NO 
	AND			IS_PROCESSED		=	'N'	
--------------------------------------------------------------------------------------------------



END
GO
