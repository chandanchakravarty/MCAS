CREATE PROCEDURE [dbo].[Proc_SetCDGIClaimsRefNo]    
 @AccidentClaimId [int],    
 @OrganizationType [varchar](5),    
 @ClaimPrefix [varchar](4)    
WITH EXECUTE AS CALLER    
AS    
SET FMTONLY OFF;            
BEGIN TRY              
               
   DECLARE @Month  Varchar(2),              
           @Year  INT,   
           @VehicleNo Varchar(10),  
           @OICInitial varchar(5),  
           @SPInitial varchar(5),             
           @Prefix VARCHAR(50),              
           @Temp VARCHAR(50),              
           @Sfix varchar(6),              
           @Len  INT,              
           @Val  INT,  
           @Count INT,             
           @ClaimNo varchar(50)  
               
             
                      
             SET NOCOUNT ON;              
     
			  SET @Year = (Select RIGHT(YEAR(AccidentDate),2) YY FROM ClaimAccidentDetails where AccidentClaimId=@AccidentClaimId)  
              SET @Month= (SELECT RIGHT('0' + RTRIM(MONTH(AccidentDate)), 2) from ClaimAccidentDetails where AccidentClaimId =@AccidentClaimId)   
              SELECT @VehicleNo= VehicleNo FROM  ClaimAccidentDetails (nolock) where AccidentClaimId = @AccidentClaimId   
			
          SELECT @OICInitial= MU.Initial FROM MNT_Users  MU  
          LEFT JOIN   
          CLM_Claims CL   
          ON   
          MU.SNo=CL.ClaimsOfficer   
          where   
          CL.AccidentClaimId= @AccidentClaimId  
               
          SELECT @SPInitial= MU.Initial FROM MNT_Users MU  
          LEFT JOIN   
          CLM_Claims CLM  
          ON   
          MU.SNo=CLM.AdminSupport   
          where   
          CLM.AccidentClaimId= @AccidentClaimId  
           
          SET @Prefix=@ClaimPrefix+'/'+CAST(@Month as VARCHAR)+'/'+CAST(@Year as VARCHAR)+'/'+isnull(@VehicleNo,'')+'/'+isnull(@OICInitial,'')+'('+isnull(@SPInitial,'')+') '   
                
              SELECT @Temp=MAX( cast(replace(CDGIClaimRef,@Prefix,'') as int)) FROM  ClaimAccidentDetails (nolock) WHERE CDGIClaimRef LIKE @Prefix+'%'             
                 IF(@Temp IS NULL OR @Temp='')              
                    BEGIN              
                      SET @ClaimNo=@Prefix        
                      SELECT @ClaimNo As CDGIClaimRef          
                      UPDATE ClaimAccidentDetails SET CDGIClaimRef=@ClaimNo WHERE AccidentClaimId=@AccidentClaimId              
                    END              
                 ELSE              
                    BEGIN                 
                        SET @Sfix= @Temp --SUBSTRING(@Temp,LEN(@Temp),charindex('-',@Temp))            
                        SET @Val=CAST(@Sfix as int)        
                        SET @ClaimNo=@Prefix        
                        SELECT @ClaimNo As CDGIClaimRef           
                    END    
                      
    UPDATE ClaimAccidentDetails SET CDGIClaimRef=@ClaimNo WHERE AccidentClaimId=@AccidentClaimId   
                 
                     
                    
 END TRY              
               
 BEGIN CATCH              
  RAISERROR('PROBLEM IN QUERY',16,1)         
 END CATCH  
  
  
  
   




