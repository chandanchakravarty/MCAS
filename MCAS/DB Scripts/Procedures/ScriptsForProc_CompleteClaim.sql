/****** Object:  StoredProcedure [dbo].[Proc_CompleteClaim]    Script Date: 07/16/2014 17:58:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Shikha Chourasiya
-- Create date: 07/14/2014
-- Description:	Generate Permanant Claim number
-- Exec [dbo].[Proc_GeneratePerClaimNo] 147,62
-- =============================================
CREATE PROCEDURE [dbo].[Proc_CompleteClaim]
	@AccidentClaimId INT,
	@PolicyId INT
AS
BEGIN TRY  
   
   DECLARE @RowNo INT,  
           @Year  INT,  
           @Prefix VARCHAR(7), 
           @SubClassCode VARCHAR(3), 
           @Temp VARCHAR(14),  
           @Sfix varchar(7),  
           @Len  INT,  
           @Val  INT,  
           @ClaimNo varchar(14)  
 
              SET NOCOUNT ON;  
              SELECT @Year= RIGHT(YEAR(AccidentDate),2) FROM dbo.[ClaimAccidentDetails] WHERE  AccidentClaimId = @AccidentClaimId
              SELECT @SubClassCode = LEFT(a.ClassCode,3) FROM dbo.MNT_ProductClass a INNER JOIN [MNT_InsruanceM] b ON a.Id = b.SubClassId WHERE PolicyId = @PolicyId
              SET @Prefix='CL'+CAST(@Year as VARCHAR)+@SubClassCode
              SELECT @Temp= isnull(MAX(CLAIMNO),'0') 
              FROM ClaimAccidentDetails WHERE RIGHT(YEAR(AccidentDate),2) = @Year
              and  CLAIMNO LIKE 'CL'+ CAST(@Year as VARCHAR) + '%'
                 
                 IF(@Temp = '0' OR @Temp='')  
                    BEGIN  
                      SELECT @Prefix+'0000001'  
                      SET @ClaimNo=@Prefix+'0000001'  
                      UPDATE ClaimAccidentDetails SET ClaimNo=@ClaimNo WHERE AccidentClaimId=@AccidentClaimId  
                    END  
                 ELSE  
                    BEGIN  
                        SELECT @Sfix=RIGHT(@Temp,7)  
                        SELECT @Val=CAST(@Sfix as int)+1  
                        SELECT @Len=LEN(@Val)  
                          
                         IF(@Len=1)  
                         BEGIN  
                            SELECT @Prefix+'000000'+CAST(@Val AS VARCHAR)AS ClaimNo  
                            SET @ClaimNo=@Prefix+'000000'+CAST(@Val AS VARCHAR)  
                         END  
                        IF(@Len=2)  
                         BEGIN  
                            SELECT @Prefix+'00000'+CAST(@Val AS VARCHAR) AS ClaimNo  
                            SET @ClaimNo=@Prefix+'00000'+CAST(@Val AS VARCHAR)  
                         END  
                        IF(@Len=3)  
                         BEGIN  
                            SELECT @Prefix+'0000'+CAST(@Val AS VARCHAR)AS ClaimNo  
                            SET @ClaimNo=@Prefix+'0000'+CAST(@Val AS VARCHAR)  
                         END  
                        IF(@Len=4)  
                         BEGIN  
                            SELECT @Prefix+'000'+CAST(@Val AS VARCHAR)AS ClaimNo  
                            SET @ClaimNo=@Prefix+'000'+CAST(@Val AS VARCHAR)  
                         END  
                       IF(@Len=5)  
                         BEGIN  
                            SELECT @Prefix+'00'+CAST(@Val AS VARCHAR)AS ClaimNo  
                            SET @ClaimNo=@Prefix+'00'+CAST(@Val AS VARCHAR)    
                         END  
                      IF(@Len=6)  
                         BEGIN  
                            SELECT @Prefix+'0'+CAST(@Val AS VARCHAR)AS ClaimNo  
                            SET @ClaimNo=@Prefix+'0'+CAST(@Val AS VARCHAR)
                         END  
                       IF(@Len=7)  
                         BEGIN  
                            SELECT @Prefix+CAST(@Val AS VARCHAR)AS ClaimNo 
                            SET @ClaimNo=@Prefix+@Val   
                         END  
                  END  
                  UPDATE ClaimAccidentDetails SET ClaimNo=@ClaimNo WHERE AccidentClaimId=@AccidentClaimId 
                  UPDATE CLM_Claims SET ClaimStatus = 2 WHERE AccidentClaimId=@AccidentClaimId AND PolicyId = @PolicyId
 END TRY  
   
 BEGIN CATCH  
  RAISERROR('PROBLEM IN QUERY',16,1)  
 END CATCH  



GO


