IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchDuplicateVIN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchDuplicateVIN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	CREATE PROC dbo.[Proc_FetchDuplicateVIN]
	AS


	CREATE TABLE ##MNT_ISO_MASTER_2009 (
		[VIN] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[EFFECTIVE_DATE] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[REST_INDICATOR] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[ALB_INDICATOR] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[CYN] [varchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[ENG_TYPE_IND] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[STATE] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[SYM_CHG_IND] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[VSR] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[NON_VSR] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[FCI] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[MANUFACTURER] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[MODEL] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[VSR_PER_IND] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[BODY STYLE] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[ENGINE SIZE] [varchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[FWD_IND] [varchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[PER_IND] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[FULL MODEL NAME] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[SIF] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[MODEL/SERIES] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[BODY] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[ENGINE] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[RESTRAINT] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[TRANSMISSION] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[OTHER OPTIONS] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[DRL] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[NCIC] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[CIRNO] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[VSR_SYM] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[NON_VSR_SYM] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[WB] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[CLASS CODE] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[ATI] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[CURB_WEIGHT] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[GV_WEIGHT] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[HEIGHT] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[HP] [varchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
	) 

	CREATE TABLE #VIN_COUNT
	(
	 IDEN_COL INT IDENTITY(1,1),
	 VIN VARCHAR(50),
	 COUNT INT 

	)

	INSERT INTO #VIN_COUNT
	SELECT VIN , COUNT(VIN) [COUNT] FROM MNT_ISO_MASTER--MNT_ISO_MASTER 
	--WHERE SUBSTRING(RTRIM(LTRIM(VIN)),10,1)   = '6' --to be changed
	GROUP BY VIN HAVING COUNT(VIN) >= 1
	ORDER BY COUNT(VIN) --DESC



	DECLARE @VIN VARCHAR(30)
	 DECLARE @IDEN_COL Int    
	 SET @IDEN_COL = 1    
	    
	 WHILE 1 = 1                  
	 BEGIN                  
	  IF NOT EXISTS (SELECT IDEN_COL FROM #VIN_COUNT with(nolock) WHERE IDEN_COL = @IDEN_COL)                  
	  BEGIN                  
	   BREAK                  
	  END       
	--SUBSTRING(RTRIM(LTRIM(VIN)),1,10),            
	  SELECT @vin = RTRIM(LTRIM(VIN))
	  FROM #VIN_COUNT with(nolock)   
	  WHERE IDEN_COL = @IDEN_COL  
	 ----------------FETCH THE MAX DATE RECORDS----	                
		INSERT INTO ##MNT_ISO_MASTER_2009 
		SELECT * FROM  MNT_ISO_MASTER--MNT_ISO_MASTER
		WHERE RTRIM(LTRIM(VIN)) = @VIN AND
		 SUBSTRING(RTRIM(LTRIM(ISNULL(EFFECTIVE_DATE,''))),3,2) IN 
		(SELECT MAX(SUBSTRING(RTRIM(LTRIM(EFFECTIVE_DATE)),3,2))  FROM  MNT_ISO_MASTER--MNT_ISO_MASTER
		WHERE RTRIM(LTRIM(VIN)) =@VIN )
		AND 
		 SUBSTRING(RTRIM(LTRIM(ISNULL(EFFECTIVE_DATE,''))),1,2) IN 
		(SELECT MAX(SUBSTRING(RTRIM(LTRIM(EFFECTIVE_DATE)),1,2))  FROM  MNT_ISO_MASTER--MNT_ISO_MASTER
		WHERE RTRIM(LTRIM(VIN)) =@VIN )
	         
	    
	 
	  SET @IDEN_COL = @IDEN_COL + 1    
	 END    


	SELECT * FROM ##MNT_ISO_MASTER_2009

	DROP TABLE #VIN_COUNT


----------






GO

