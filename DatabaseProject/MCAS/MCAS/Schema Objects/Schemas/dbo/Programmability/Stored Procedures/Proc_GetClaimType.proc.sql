CREATE PROCEDURE [dbo].[Proc_GetClaimType]
	@AccidentClaimId [int]
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;
DECLARE @ODSTATUS CHAR(1),
        @TPClaimentStatus CHAR(1)=NULL
DECLARE @OD char(1),
        @TP char(1),
        @BI char(1)
        
SELECT @ODSTATUS=ODSTATUS,@TPClaimentStatus=TPClaimentStatus  FROM ClaimAccidentDetails WHERE AccidentClaimId=@AccidentClaimId
set @OD = case when isnull(@ODSTATUS,'N')='Y' then '1' else '0' end
set @TP = case when isnull(@TPClaimentStatus,'N')='Y' then '2' else '0' end
set @BI = case when isnull(@TPClaimentStatus,'N')='Y' then '3' else '0' end
 Select Lookupvalue,Description from MNT_Lookups 
  WHERE Category='ClaimType' 
  and Lookupvalue in(@OD,@TP,@BI)


