/****** Object:  StoredProcedure [dbo].[Proc_GetClaimNo]    Script Date: 06/30/2014 16:16:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:  ASHISH KUMAR  
-- Create date: 12-06-2014  
-- Description: PROCEDURE FOR GENERATION OF CLAIM_NO  
-- =============================================  
 ALTER PROCEDURE [dbo].[Proc_GetClaimNo]  
 (  
  @AccidentClaimId INT   
 )  
 AS  
 BEGIN TRY  
   
   DECLARE @RowNo INT,  
           @Year  INT,  
           @Prefix VARCHAR(3),  
           @Temp VARCHAR(9),  
           @Sfix varchar(6),  
           @Len  INT,  
           @Val  INT,  
           @ClaimNo varchar(9)  
       --  @ErrorMessage NVARCHAR(4000)  
              SET NOCOUNT ON;  
              SELECT @Year= Right(YEAR(GETDATE()),2)  
              SET @Prefix='T'+CAST(@Year as VARCHAR)  
              SELECT @Temp=MAX(CLAIMNO) FROM  ClaimAccidentDetails WHERE CLAIMNO LIKE @Prefix+'%'  
                 IF(@Temp IS NULL OR @Temp='')  
                    BEGIN  
                      SELECT @Prefix+'000001'  
                      SET @ClaimNo=@Prefix+'000001'  
                      UPDATE ClaimAccidentDetails SET ClaimNo=@ClaimNo WHERE AccidentClaimId=@AccidentClaimId  
                    END  
                 ELSE  
                    BEGIN  
                        SELECT @Sfix=RIGHT(@Temp,6)  
                        SELECT @Val=CAST(@Sfix as int)+1  
                        SELECT @Len=LEN(@Val)  
                          
                         IF(@Len=1)  
                         BEGIN  
                            SELECT @Prefix+'00000'+CAST(@Val AS VARCHAR)AS ClaimNo  
                            SET @ClaimNo=@Prefix+'00000'+CAST(@Val AS VARCHAR)  
                            --UPDATE ClaimAccidentDetails SET ClaimNo=@ClaimNo WHERE AccidentClaimId=@AccidentClaimId  
                         END  
                        IF(@Len=2)  
                         BEGIN  
                            SELECT @Prefix+'0000'+CAST(@Val AS VARCHAR) AS ClaimNo  
                            SET @ClaimNo=@Prefix+'0000'+CAST(@Val AS VARCHAR)  
                            --UPDATE ClaimAccidentDetails SET ClaimNo=@ClaimNo WHERE AccidentClaimId=@AccidentClaimId  
                         END  
                        IF(@Len=3)  
                         BEGIN  
                            SELECT @Prefix+'000'+CAST(@Val AS VARCHAR)AS ClaimNo  
                            SET @ClaimNo=@Prefix+'000'+CAST(@Val AS VARCHAR)  
                            --UPDATE ClaimAccidentDetails SET ClaimNo=@ClaimNo WHERE AccidentClaimId=@AccidentClaimId  
                         END  
                        IF(@Len=4)  
                         BEGIN  
                            SELECT @Prefix+'00'+CAST(@Val AS VARCHAR)AS ClaimNo  
                            SET @ClaimNo=@Prefix+'00'+CAST(@Val AS VARCHAR)  
                            --UPDATE ClaimAccidentDetails SET ClaimNo=@ClaimNo WHERE AccidentClaimId=@AccidentClaimId  
                         END  
                       IF(@Len=5)  
                         BEGIN  
                            SELECT @Prefix+'0'+CAST(@Val AS VARCHAR)AS ClaimNo  
                            SET @ClaimNo=@Prefix+'0'+CAST(@Val AS VARCHAR)  
                            --UPDATE ClaimAccidentDetails SET ClaimNo=@ClaimNo WHERE AccidentClaimId=@AccidentClaimId  
                         END  
                       IF(@Len=6)  
                         BEGIN  
                            SELECT @Prefix+@Val AS ClaimNo  
                            SET @ClaimNo=@Prefix+@Val  
                            --UPDATE ClaimAccidentDetails SET ClaimNo=@ClaimNo WHERE AccidentClaimId=@AccidentClaimId  
                         END  
                  END  
                  UPDATE ClaimAccidentDetails SET ClaimNo=@ClaimNo WHERE AccidentClaimId=@AccidentClaimId 
 END TRY  
   
 BEGIN CATCH  
  RAISERROR('PROBLEM IN QUERY',16,1)  
 END CATCH  
                      
           
  
GO


