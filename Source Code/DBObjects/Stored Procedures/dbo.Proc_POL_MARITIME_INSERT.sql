IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_POL_MARITIME_INSERT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_POL_MARITIME_INSERT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[Proc_POL_MARITIME_INSERT]                        
(
@CUSTOMER_ID int,
@POLICY_ID int,
@POLICY_VERSION_ID int,
@MARITIME_ID int output,
@VESSEL_NUMBER nvarchar(15),
@NAME_OF_VESSEL nvarchar(70),
@TYPE_OF_VESSEL nvarchar(20),
@MANUFACTURE_YEAR int,
@MANUFACTURER nvarchar(50),
@BUILDER nvarchar(50),
@CONSTRUCTION nvarchar(100),
@PROPULSION nvarchar(20),
@CLASSIFICATION nvarchar(50),
@LOCAL_OPERATION nvarchar(100),
@LIMIT_NAVIGATION nvarchar(100),
@PORT_REGISTRATION nvarchar(50),
@REGISTRATION_NUMBER nvarchar(50),
@TIE_NUMBER nvarchar(50),
@VESSEL_ACTION_NAUTICO_CLUB int,
@NAME_OF_CLUB nvarchar(70),
@LOCAL_CLUB nvarchar(100),
@NUMBER_OF_CREW int,
@NUMBER_OF_PASSENGER int,
@REMARKS nvarchar(200),
@IS_ACTIVE nvarchar(2),
@CREATED_BY int,
@CREATED_DATETIME datetime,
@LAST_UPDATED_DATETIME datetime,
@MODIFIED_BY int
)
As
BEGIN

SELECT @MARITIME_ID = isnull(Max(MARITIME_ID),0)+1 FROM POL_MARITIME

INSERT INTO [POL_MARITIME]
           ([CUSTOMER_ID]
           ,[POLICY_ID]
           ,[POLICY_VERSION_ID]
           ,[MARITIME_ID]
           ,[VESSEL_NUMBER]
           ,[NAME_OF_VESSEL]
           ,[TYPE_OF_VESSEL]
           ,[MANUFACTURE_YEAR]
           ,[MANUFACTURER]
           ,[BUILDER]
           ,[CONSTRUCTION]
           ,[PROPULSION]
           ,[CLASSIFICATION]
           ,[LOCAL_OPERATION]
           ,[LIMIT_NAVIGATION]
           ,[PORT_REGISTRATION]
           ,[REGISTRATION_NUMBER]
           ,[TIE_NUMBER]
           ,[VESSEL_ACTION_NAUTICO_CLUB]
           ,[NAME_OF_CLUB]
           ,[LOCAL_CLUB]
           ,[NUMBER_OF_CREW]
           ,[NUMBER_OF_PASSENGER]
           ,[REMARKS]
           ,[IS_ACTIVE]
           ,[CREATED_BY]
           ,[CREATED_DATETIME]
           ,[LAST_UPDATED_DATETIME]
           ,[MODIFIED_BY])
     VALUES 
     (
        @CUSTOMER_ID ,
		@POLICY_ID ,
		@POLICY_VERSION_ID ,
		@MARITIME_ID ,
		@VESSEL_NUMBER ,
		@NAME_OF_VESSEL ,
		@TYPE_OF_VESSEL ,
		@MANUFACTURE_YEAR ,
		@MANUFACTURER ,
		@BUILDER ,
		@CONSTRUCTION ,
		@PROPULSION ,
		@CLASSIFICATION ,
		@LOCAL_OPERATION ,
		@LIMIT_NAVIGATION ,
		@PORT_REGISTRATION ,
		@REGISTRATION_NUMBER ,
		@TIE_NUMBER ,
		@VESSEL_ACTION_NAUTICO_CLUB ,
		@NAME_OF_CLUB,
		@LOCAL_CLUB ,
		@NUMBER_OF_CREW ,
		@NUMBER_OF_PASSENGER ,
		@REMARKS ,
		@IS_ACTIVE ,
		@CREATED_BY ,
		@CREATED_DATETIME ,
		@LAST_UPDATED_DATETIME ,
		@MODIFIED_BY 
     )

END
GO

