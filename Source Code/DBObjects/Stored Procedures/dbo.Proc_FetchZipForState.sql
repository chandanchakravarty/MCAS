IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchZipForState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchZipForState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name          : dbo.Proc_FetchZipForState
Created by         : 
Date               : 
Purpose            : 
Revison History    :
modified by		:Pravesh K Chandel
modified Date	:	14 nov 2007
Purpose		:	 Returning Some Name for Blank County
Used In            :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/ 
-- drop proc dbo.Proc_FetchZipForState 
CREATE PROCEDURE dbo.Proc_FetchZipForState 
(
	@STATE_ID int,
	@ZIP_ID VARCHAR(10)
)
AS
BEGIN 
	-- SELECT DISTINCT COUNTY from MNT_TERRITORY_CODES (NOLOCK)  
	 SELECT DISTINCT case COUNTY when '' then 'BLANK COUNTY' else COUNTY end as COUNTY from MNT_TERRITORY_CODES (NOLOCK)
	   WHERE  STATE = @STATE_ID and ZIP like (SUBSTRING(@ZIP_ID,1,5) + '%')
END
 





GO

