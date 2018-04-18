CREATE PROCEDURE [dbo].[Proc_GetCountryList]
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;

BEGIN

select *,'Y' as status FROM MNT_Country order by CountryName asc

END


