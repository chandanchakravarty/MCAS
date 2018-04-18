CREATE PROCEDURE [dbo].[Proc_SetClaimsNo]
	@AccidentClaimId [int],
	@OrganizationType [varchar](5),
	@ClaimPrefix [varchar](4)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;        
 BEGIN TRY          
           
   DECLARE @RowNo INT,          
           @Year  INT,          
           @Prefix VARCHAR(25),          
           @Temp VARCHAR(25),          
           @Sfix varchar(6),          
           @Len  INT,          
           @Val  INT,  
           @TempVal VARCHAR(10),         
           @ClaimNo varchar(25) ,  
           @OrgId int     
                  
             SET NOCOUNT ON;          
  
              SELECT @Year= YEAR(GETDATE())   
              select @OrgId=Organization FROM  ClaimAccidentDetails (nolock) where AccidentClaimId = @AccidentClaimId      
              SELECT @TempVal= Country+'/'+InsurerType from MNT_OrgCountry where Id=@OrgId   
  
              SET @Prefix=@ClaimPrefix+'/'+@TempVal+'/'+CAST(@Year as VARCHAR)+'-'       
              --SELECT @Temp=MAX(CLAIMNO) FROM  ClaimAccidentDetails WHERE CLAIMNO LIKE @Prefix+'%'   
              SELECT @Temp=MAX( cast(replace(CLAIMNO,@Prefix,'') as int)) FROM  ClaimAccidentDetails (nolock) WHERE CLAIMNO LIKE @Prefix+'%'         
                 IF(@Temp IS NULL OR @Temp='')          
                    BEGIN          
                      SET @ClaimNo=@Prefix+'1'    
                      SELECT @ClaimNo As ClaimNo      
                      UPDATE ClaimAccidentDetails SET ClaimNo=@ClaimNo WHERE AccidentClaimId=@AccidentClaimId          
                    END          
                 ELSE          
                    BEGIN       
                             
                        SET @Sfix= @Temp --SUBSTRING(@Temp,LEN(@Temp),charindex('-',@Temp))        
                        SET @Val=CAST(@Sfix as int)+1     
                        SET @ClaimNo=@Prefix+CAST(@Val AS VARCHAR)    
                        SELECT @ClaimNo As ClaimNo       
                    END          
                 UPDATE ClaimAccidentDetails SET ClaimNo=@ClaimNo WHERE AccidentClaimId=@AccidentClaimId   
       
 END TRY          
           
 BEGIN CATCH          
  RAISERROR('PROBLEM IN QUERY',16,1)          
 END CATCH


