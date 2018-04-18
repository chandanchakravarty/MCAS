CREATE PROCEDURE [dbo].[Proc_CompleteClaim]
	@AccidentClaimId [int],
	@PolicyId [int]
WITH EXECUTE AS CALLER
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


