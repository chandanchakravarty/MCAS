CREATE PROCEDURE [dbo].[Proc_GetPaymentProcessSearch]    
 @query [nvarchar](max)    
WITH EXECUTE AS CALLER    
AS    
BEGIN    
    
  IF OBJECT_ID('tempdb..#TempTable') IS NOT NULL    
    DROP TABLE #TempTable    
  SET FMTONLY OFF;    
  CREATE TABLE #TempTable ( 
    [Id] [int] IDENTITY(1,1) PRIMARY KEY,        
    [IsComplete] [int] NULL,        
    [AccidentClaimId] [int] NULL,        
    [ClaimID] [int] NULL,        
    [ClaimType] [int] NULL,        
    [ClaimOfficer] [nvarchar](100) NULL,         
    [ClaimNo] [nvarchar](100) NULL,         
    [VehicleNo] [nvarchar](100) NULL,         
    [TPSurname] [nvarchar](100) NULL,        
    [ClaimStatus] [int] NULL,        
    [AccidentDate] [datetime] NULL,         
    [IPNo] [nvarchar](100) NULL,        
    [ClaimDate] [datetime] NULL,          
    [VehicleRegnNo] [varchar](100) NULL,          
    [ClaimantStatus] [int] NULL,
    [InsurerType] [nvarchar](50) NULL,
    [CountryOrgazinationCode] [nvarchar](100) NULL     
  )
  
  CREATE INDEX IDX_TempTable_InsurerType ON #TempTable(InsurerType)
  CREATE INDEX IDX_TempTable_CountryOrgazinationCode ON #TempTable(CountryOrgazinationCode)
            
  EXEC (@query)     
   SELECT          
    [IsComplete],        
    [AccidentClaimId],        
    [ClaimID],        
    [ClaimType],         
    [ClaimOfficer] ,         
    [ClaimNo] ,         
    [VehicleNo] ,         
    [TPSurname] ,        
    [ClaimStatus] ,        
    [AccidentDate],         
    [IPNo],        
    [ClaimDate],          
    [VehicleRegnNo],          
    [ClaimantStatus]         
  FROM #TempTable temp
  --,MNT_UserOrgAccess orgAccess 
  --where orgAccess.OrgCode = temp.InsurerType and orgAccess.OrgName = temp.CountryOrgazinationCode --and orgAccess.UserId ='Admin' 
    
END