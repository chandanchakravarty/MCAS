CREATE PROCEDURE Proc_GetClaimTPVehicleNo
@AccidentClaimId [int]
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;
BEGIN
Select ClaimId,TPVehicleNo From CLM_Claims Where TPVehicleNo is not null and AccidentClaimId=@AccidentClaimId order by ClaimType
END