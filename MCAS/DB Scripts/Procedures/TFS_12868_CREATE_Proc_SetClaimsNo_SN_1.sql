
/****** Object:  StoredProcedure [dbo].[Proc_SetClaimsNo]    Script Date: 04/14/2015 12:29:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetClaimsNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetClaimsNo]
GO


/****** Object:  StoredProcedure [dbo].[Proc_SetClaimsNo]    Script Date: 04/14/2015 12:29:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* =============================================          
-- Author:  ASHISH KUMAR       
-- Modified By:         
-- Create date: 04-02-2015         
-- Description: PROCEDURE FOR GENERATION OF CLAIM_NO      
Modified by: Pravesh K Chandel  
MOdified Date: 3 March 2014  
Purpose: Correct the Logic to generate the Claim Number  
--*/  


CREATE PROCEDURE [dbo].[Proc_SetClaimsNo]          
 (      
  @AccidentClaimId INT,          
  @OrganizationType VARCHAR(5),      
  @ClaimPrefix VARCHAR(4)  -- For Unreported Claim Number 'UCLM' and Claim Number 'CLM'      
 )          
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
GO


