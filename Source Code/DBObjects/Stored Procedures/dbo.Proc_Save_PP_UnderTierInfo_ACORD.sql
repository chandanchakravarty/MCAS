IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Save_PP_UnderTierInfo_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Save_PP_UnderTierInfo_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name       : dbo.Proc_Save_PP_UnderTierInfo_ACORD              
Created by      : praveen Kasana               
Date            : 04 Jan 2010      
Purpose     :  Inserts/Upadtes record in PP Underwriting Tier Info table          
Revison History :              
Used In  : Wolverine              
  --DROP PROC dbo.Proc_Save_PP_UnderTierInfo_ACORD          
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/    
  
CREATE PROC Dbo.Proc_Save_PP_UnderTierInfo_ACORD              
(              
	 @CUSTOMER_ID    INT,              
	 @APP_ID         INT,              
	 @APP_VERSION_ID SMALLINT,
	 @UNDRWRTINGTIER varchar(5),   
	 @UNTIER_ASSIGNED_DATE DATETIME	 
)              
AS BEGIN IF exists (SELECT 1 FROM APP_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID			and app_lob='2' and STATE_ID = 14)BEGIN	INSERT INTO APP_UNDERWRITING_TIER                
	(                
		CUSTOMER_ID,
		APP_ID,
		APP_VERSION_ID,
		UNDERWRITING_TIER,
		UNTIER_ASSIGNED_DATE
	)
	VALUES
	(
		@CUSTOMER_ID,
		@APP_ID,
		@APP_VERSION_ID,
		@UNDRWRTINGTIER,
		@UNTIER_ASSIGNED_DATE 

	) 
END
END
GO

