CREATE PROC [dbo].[Proc_Insert_MNT_BusCaptain]
(
	@BusCaptainCode nvarchar(14),
	@BusCaptainName nvarchar(400),
	@NRICPassportNo varchar(50),
	@Nationality nvarchar(200),
	@DateJoined datetime =null,
	@DateResigned datetime =null,
	@CreatedBy nvarchar(50),
	@UploadFileRefNo nvarchar(100)
)
AS
BEGIN
	BEGIN TRY 
	
	DECLARE @returnValue int
	
	IF EXISTS(SELECT 1 FROM MNT_BusCaptain 
						WHERE BusCaptainCode=@BusCaptainCode AND BusCaptainName=@BusCaptainName 
								AND NRICPassportNo =@NRICPassportNo AND Nationality=@Nationality 
								AND ISNULL(DateJoined,'')=ISNULL(@DateJoined,'') 
								AND ISNULL(DateResigned,'')=ISNULL(@DateResigned,'')) 								
	BEGIN
			SET @returnValue=2
	END
	ELSE
	BEGIN
			IF EXISTS(SELECT 1 FROM MNT_BusCaptain WHERE BusCaptainCode=@BusCaptainCode)
			BEGIN
				UPDATE MNT_BusCaptain 
					SET BusCaptainName=@BusCaptainName,
						NRICPassportNo=@NRICPassportNo,
						Nationality =@Nationality,
						DateJoined =@DateJoined ,
						DateResigned =@DateResigned,
						ModifiedBy = @CreatedBy,
						ModifiedDate =GETDATE (),
						UploadFileRefNo =@UploadFileRefNo
					WHERE BusCaptainCode=@BusCaptainCode
			END
			ELSE
			BEGIN
				Insert into MNT_BusCaptain(BusCaptainCode,BusCaptainName,NRICPassportNo,Nationality,DateJoined,DateResigned,CreatedBy,CreatedDate,UploadFileRefNo) 
				Values(@BusCaptainCode,@BusCaptainName,@NRICPassportNo,@Nationality,@DateJoined,@DateResigned,@CreatedBy,GETDATE (),@UploadFileRefNo)
			END
			
			SET @returnValue=1
	END		
	
	END TRY
	BEGIN CATCH
	
	SET @returnValue=0
	    
	END CATCH
	
	SELECT @returnValue
END