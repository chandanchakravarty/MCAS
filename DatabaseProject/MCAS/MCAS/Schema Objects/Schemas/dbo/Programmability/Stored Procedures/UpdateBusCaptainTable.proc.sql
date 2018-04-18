CREATE PROCEDURE [dbo].[UpdateBusCaptainTable]
	@BusCaptainCode [nvarchar](50),
	@BusCaptainName [nvarchar](50),
	@NRICPassportNo [nvarchar](50),
	@ContactNo [nvarchar](50),
	@Nationality [nvarchar](50)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;
BEGIN
    SET NOCOUNT ON;

    update MNT_BusCaptain
    set BusCaptainCode=@BusCaptainCode,
    BusCaptainName=@BusCaptainName,
    NRICPassportNo=@NRICPassportNo,
    ContactNo =@ContactNo,
    Nationality=@Nationality
    where BusCaptainCode=@BusCaptainCode
END


