IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_FillVINFromISO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_FillVINFromISO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc dbo.proc_FillVINFromISO 

CREATE proc dbo.proc_FillVINFromISO  
as  
begin  

  
EXECUTE dbo.[Proc_FetchDuplicateVIN]
  
 
--MNT_VIN_MASTER_TEMP  
INSERT INTO MNT_VIN_MASTER
(  
	VIN,MAKE_CODE,MODEL_YEAR,MAKE_NAME,SERIES_NAME,BODY_TYPE,ANTI_LCK_BRAKES,AIRBAG,SYMBOL  
)  
  


SELECT substring(rtrim(ltrim(Vin)),1,10),  
  
rtrim(ltrim(MANUFACTURER)),  
  
CASE SUBSTRING(rtrim(ltrim(VIN)),10,1)  
  
--WHEN 'B' THEN '1981'  
--WHEN 'C' THEN '1982'  
WHEN 'D' THEN '1983'                                 
WHEN 'E' THEN '1984'  
WHEN 'F' THEN '1985'  
WHEN 'G' THEN '1986'
WHEN 'H' THEN '1987'  
WHEN 'J' THEN '1988'  
WHEN 'K' THEN '1989'  
WHEN 'L' THEN '1990'  
WHEN 'M' THEN '1991'  
WHEN 'N' THEN '1992'  
WHEN 'P' THEN '1993'  
WHEN 'R' THEN '1994'  
WHEN 'S' THEN '1995'  
WHEN 'T' THEN '1996'  
WHEN 'V' THEN '1997'  
WHEN 'W' THEN '1998'  
WHEN 'X' THEN '1999'  
WHEN 'Y' THEN '2000'  
WHEN '1' THEN '2001'  
WHEN '2' THEN '2002'  
WHEN '3' THEN '2003'  
WHEN '4' THEN '2004'  
WHEN '5' THEN '2005'  
WHEN '6' THEN '2006'  
WHEN '7' THEN '2007'  
WHEN '8' THEN '2008' 
WHEN '9' THEN '2009' 
WHEN 'A' THEN '2010'  
WHEN 'B' THEN '2011'  --Assuming that 1981 will not import
WHEN 'C' THEN '2012'  --Assuming that 1982 will not import
  
END AS YEAR,  
  
rtrim(ltrim(MANUFACTURER)),  
  
rtrim(ltrim([FUll MODEL NAME])),  
  
rtrim(ltrim([BODY STYLE])),  
  
rtrim(ltrim(ALB_INDICATOR)),  
  
--D, B and S are converted into C,D and B after consulting Deepak for rating engine and QQ  
--#N,--Y--J--H--X,P--Z--B--F--U,A--I--C--O--D--R--E--T--V--S--Q,M--L,K--W,NULL g
Case rtrim(ltrim(REST_INDICATOR))  
  
WHEN 'B' THEN 'C'  
  
WHEN 'H' THEN 'C'  
  
WHEN 'X' THEN 'C'  
  
WHEN 'D' THEN 'C'  
  
WHEN 'T' THEN 'D'  
  
WHEN 'O' THEN 'D'  
  
WHEN 'I' THEN 'D'  
  
WHEN 'E' THEN 'D'  
  
WHEN 'F' THEN 'D'  
  
WHEN 'Q' THEN 'D'  
  
WHEN 'U' THEN 'D'  
  
WHEN 'V' THEN 'D'  
  
WHEN 'G' THEN 'D'  
  
WHEN 'L' THEN 'D'  
  
WHEN 'Z' THEN 'D'  
  
WHEN '2' THEN 'D'  
  
WHEN '3' THEN 'D'  
  
WHEN 'S' THEN 'B'  
  
WHEN 'R' THEN 'B'  
  
WHEN 'J' THEN 'B'  
  
WHEN 'W' THEN 'B'  
  
WHEN 'Y' THEN 'B'  
  
WHEN 'C' THEN 'B'  
  
WHEN '1' THEN 'B'  
  
WHEN '4' THEN 'B'  
  
WHEN '5' THEN 'B'  
  
WHEN '6' THEN 'B'  
  
WHEN '8' THEN 'B'  
  
WHEN '9' THEN 'B'  
  
END AS REST_INDICATOR,  
  
rtrim(ltrim(VSR_SYM))  

FROM ##MNT_ISO_MASTER_2009  

--MNT_ISO_MASTER_2009
  
   
DROP TABLE ##MNT_ISO_MASTER_2009
  
end  
  



GO

