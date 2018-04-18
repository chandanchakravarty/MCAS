CREATE PROCEDURE [dbo].[Proc_GetRecoverFromOD]         
 @AccidentClaimId int,        
 @ClaimType int,        
 @ClaimId int        
AS   
Declare @ClaimRecordNo nvarchar(100)        
Set @ClaimRecordNo = (Select ClaimRecordNo from CLM_Claims where AccidentClaimId = @AccidentClaimId and ClaimType = @ClaimType and ClaimID = @ClaimId)        
IF (@ClaimType=3)    
 BEGIN      
   SELECT ROW_NUMBER() OVER(PARTITION BY tbl.ClaimType ORDER BY tbl.AccidentClaimId) RecordNumber, tbl.Insurer As InsurerName  
   From        
    (        
      SELECT ISNULL(d.CedantName ,'') Insurer, a.AccidentClaimId,b.ClaimType   
      FROM ClaimAccidentDetails a        
       JOIN CLM_Claims b ON a.AccidentClaimId = b.AccidentClaimId        
       JOIN CLM_ServiceProvider c ON a.AccidentClaimId = c.AccidentId  AND b.ClaimType = c.ClaimTypeId AND b.ClaimID = c.ClaimantNameId      
       LEFT JOIN MNT_Cedant d ON c.CompanyNameId = d.CedantId        
       LEFT JOIN MNT_Adjusters e on c.CompanyNameId = e.AdjusterId    
       LEFT JOIN MNT_DepotMaster f on c.CompanyNameId = f.DepotId        
      WHERE a.AccidentClaimId = @AccidentClaimId AND b.ClaimType = @ClaimType AND b.ClaimRecordNo = @ClaimRecordNo    
      AND c.PartyTypeId = 1   AND c.ServiceProviderTypeId = 1 AND c.ClaimTypeId = @ClaimType  
    ) tbl        
    WHERE tbl.Insurer IS NOT NULL  
 END  
ELSE IF(@ClaimType=1)  
BEGIN  
   SELECT ROW_NUMBER() OVER(PARTITION BY tbl.ClaimType ORDER BY tbl.AccidentClaimId) RecordNumber, tbl.Insurer As InsurerName  
     From        
     (        
      SELECT b.ClaimantName AS Insurer,a.AccidentClaimId,b.ClaimType FROM ClaimAccidentDetails a        
             JOIN CLM_Claims b ON a.AccidentClaimId = b.AccidentClaimId      
      WHERE a.AccidentClaimId = @AccidentClaimId AND b.ClaimType = @ClaimType AND b.ClaimRecordNo = @ClaimRecordNo        
      UNION ALL        
      SELECT ISNULL(d.CedantName ,'') Insurer, a.AccidentClaimId,b.ClaimType   
      FROM ClaimAccidentDetails a        
          JOIN CLM_Claims b ON a.AccidentClaimId = b.AccidentClaimId        
          JOIN CLM_ServiceProvider c ON a.AccidentClaimId = c.AccidentId  AND b.ClaimType = c.ClaimTypeId AND b.ClaimID = c.ClaimantNameId      
          LEFT JOIN MNT_Cedant d ON c.CompanyNameId = d.CedantId        
          LEFT JOIN MNT_Adjusters e on c.CompanyNameId = e.AdjusterId    
          LEFT JOIN MNT_DepotMaster f on c.CompanyNameId = f.DepotId        
       WHERE a.AccidentClaimId = @AccidentClaimId AND b.ClaimType = @ClaimType AND b.ClaimRecordNo = @ClaimRecordNo    
       AND c.PartyTypeId = 1   AND c.ServiceProviderTypeId = 2 AND c.ClaimTypeId = @ClaimType  
)tbl        
WHERE tbl.Insurer IS NOT NULL   
END    
ELSE  
    BEGIN  
     SELECT ROW_NUMBER() OVER(PARTITION BY tbl.ClaimType ORDER BY tbl.AccidentClaimId) RecordNumber, tbl.Insurer As InsurerName from        
       (        
        SELECT b.ClaimantName AS Insurer,a.AccidentClaimId,b.ClaimType FROM ClaimAccidentDetails a        
        JOIN CLM_Claims b ON a.AccidentClaimId = b.AccidentClaimId        
        WHERE a.AccidentClaimId = @AccidentClaimId AND b.ClaimType = @ClaimType AND b.ClaimRecordNo = @ClaimRecordNo        
       ) tbl        
     WHERE tbl.Insurer IS NOT NULL  
    END 