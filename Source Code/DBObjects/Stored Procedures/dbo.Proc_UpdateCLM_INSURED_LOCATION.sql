IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_INSURED_LOCATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_INSURED_LOCATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateCLM_INSURED_LOCATION
Created by      : Vijay Arora
Date            : 5/1/2006
Purpose    	: To update the record in table named CLM_INSURED_LOCATION
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--drop proc  dbo.Proc_UpdateCLM_INSURED_LOCATION
CREATE PROC dbo.Proc_UpdateCLM_INSURED_LOCATION
(
@INSURED_LOCATION_ID     int,
@CLAIM_ID     int,
@LOCATION_DESCRIPTION     varchar(500),--Done for Itrack Issue 6892 on 30 Dec 09
@ADDRESS1     varchar(50),
@ADDRESS2     varchar(50),
@CITY     varchar(50),
@STATE     int,
@ZIP     varchar(10),
@COUNTRY     int,
@MODIFIED_BY     int
)
AS
BEGIN
UPDATE  CLM_INSURED_LOCATION
SET
LOCATION_DESCRIPTION = @LOCATION_DESCRIPTION,
ADDRESS1 = @ADDRESS1,
ADDRESS2 = @ADDRESS2,
CITY = @CITY,
STATE = @STATE,
ZIP = @ZIP,
MODIFIED_BY = @MODIFIED_BY,
LAST_UPDATED_DATETIME = GETDATE(),
COUNTRY = @COUNTRY
WHERE INSURED_LOCATION_ID = @INSURED_LOCATION_ID AND CLAIM_ID = @CLAIM_ID
END








GO

