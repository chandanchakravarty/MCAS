IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQuickQuoteVinMaster]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQuickQuoteVinMaster]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetQuickQuoteVinMaster
Created by         : Deepak Gupta
Date               : 29/07/2005
Purpose            : To fetch the vin master values for xml
Revison History    :
Used In            : Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--drop proc dbo.Proc_GetQuickQuoteVinMaster

CREATE   PROCEDURE Proc_GetQuickQuoteVinMaster
	@TYPE 		NVARCHAR(5),
	@YEAR 		INT,
	@MAKE 		NVARCHAR(20)='',
	@MODEL 		NVARCHAR(50)=''
AS
BEGIN
	IF @TYPE='MAKE'
	BEGIN
		SELECT DISTINCT LTRIM(RTRIM(MAKE_NAME)) MAKE_NAME FROM MNT_VIN_MASTER WHERE MODEL_YEAR =@YEAR ORDER BY 1 FOR XML AUTO;
	END
	ELSE IF @TYPE='MODEL'
	BEGIN
		SELECT DISTINCT LTRIM(RTRIM(SERIES_NAME)) MODEL_NAME FROM MNT_VIN_MASTER WHERE MODEL_YEAR = @YEAR AND MAKE_NAME=@MAKE ORDER BY 1 FOR XML AUTO;
	END
	ELSE IF @TYPE='VIN'
	BEGIN
		SELECT DISTINCT LTRIM(RTRIM(VIN)) VIN_NO,LTRIM(RTRIM(ISNULL(SYMBOL,''))) SYMBOL,CASE ANTI_LCK_BRAKES 
		WHEN '' THEN 'false' When 'N' THEN 'false' When 'W' THEN 'false' When 'O' THEN 'false' ELSE 'true' END 
		BRAKES, CASE AIRBAG WHEN 'C' THEN 'C' WHEN 'D' THEN 'D' WHEN 'B' THEN 'B' ELSE '' END AIRBAG
		FROM MNT_VIN_MASTER WHERE MODEL_YEAR = @YEAR AND MAKE_NAME=@MAKE AND SERIES_NAME=@MODEL 
		ORDER BY 1 FOR XML AUTO;
	END
END

/*
Proc_GetQuickQuoteVinMaster 'MAKE',2005
Proc_GetQuickQuoteVinMaster 'MODEL',2005,'ISZU'
Proc_GetQuickQuoteVinMaster 'VIN',2005,'ISZU','ASCENDER EXTENDED WHEELBASE'
SELECT * FROM MNT_VIN_MASTER 
*/











GO

