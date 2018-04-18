IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetYearAgencyStatement]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetYearAgencyStatement]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*---------------------------------------------------------------------------          
CREATE BY       : Uday Shanker          
CREATE DATETIME : 16 Jan 2008 04.20.00 PM          
PURPOSE         : To Fetch the agency statement details of specified year :         

exec Proc_Proc_GetYearAgencyStatement 
----------------------------------------------------------------------------*/          
-- drop proc Proc_GetYearAgencyStatement   
CREATE  PROC dbo.Proc_GetYearAgencyStatement       
 (
  @YEAR  INT  ,
  @COMM_TYPE VARCHAR(10),
  @RETVALUE INT OUTPUT
 )          
 AS         
 
BEGIN

DECLARE @MONTHNO INT
SELECT 
@MONTHNO= MAX(MONTH_NUMBER) FROM ACT_AGENCY_STATEMENT WITH(NOLOCK)
WHERE MONTH_YEAR = @YEAR 
AND MONTH_NUMBER IS NOT NULL 
AND COMM_TYPE ='CAC'
IF(@MONTHNO = 12)
 SET @RETVALUE = 1
-- SELECT @year=
-- ISNULL(max(MONTH_NUMBER),0) AS MONTH_NUMBER FROM ACT_AGENCY_STATEMENT WHERE MONTH_YEAR = 2007+1 and MONTH_NUMBER is not null and COMM_TYPE ='CAC' order by MONTH_NUMBER

END





GO

