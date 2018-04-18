CREATE FUNCTION dbo.ufnGetReserveId(@ClaimID int)
RETURNS int 
AS 
BEGIN
    DECLARE @ret int;
        
    SELECT TOP(1) @ret=ReserveId FROM CLM_ReserveSummary 
    WHERE ClaimID=@ClaimID 
    ORDER BY ReserveId DESC      
        
    RETURN @ret;
END