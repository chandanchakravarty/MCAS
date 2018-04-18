/*----------------------------------------------------------        
Proc Name       : dbo.Proc_InsertCLM_INSURED_LOCATION        
Created by      : Vijay Arora        
Date            : 5/1/2006        
Purpose     : To Insert the record in table named CLM_INSURED_LOCATION        
Revison History :        
Used In  : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/     
-- drop proc dbo.Proc_InsertCLM_INSURED_LOCATION      
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_INSURED_LOCATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_INSURED_LOCATION]
GO    
CREATE PROC dbo.Proc_InsertCLM_INSURED_LOCATION        
(        
@INSURED_LOCATION_ID int OUTPUT,        
@CLAIM_ID     int,        
@LOCATION_DESCRIPTION     varchar(500),--Done for Itrack Issue 6892 on 30 Dec 09        
@ADDRESS1     varchar(50),        
@ADDRESS2     varchar(50),        
@CITY     varchar(50),        
@STATE     int,        
@ZIP     varchar(10),        
@COUNTRY     int,        
@CREATED_BY     int,    
@POLICY_LOCATION_ID int        
)        
AS        
BEGIN        
     
DECLARE @PARTY_ID INT    
DECLARE @HOLDER_NAME VARCHAR(60)      
DECLARE @HOLDER_ADDRESS1  VARCHAR(150)      
DECLARE @HOLDER_ADDRESS2  VARCHAR(150)      
DECLARE @HOLDER_CITY   VARCHAR(70)      
DECLARE @HOLDER_STATE   INT      
DECLARE @HOLDER_ZIP  VARCHAR(20)      
DECLARE @HOLDER_COUNTRY   INT    
DECLARE @DETAIL_TYPE_LIEN_HOLDER INT    
    
     
select @INSURED_LOCATION_ID=isnull(Max(INSURED_LOCATION_ID),0)+1 from CLM_INSURED_LOCATION        
where CLAIM_ID = @CLAIM_ID      
INSERT INTO CLM_INSURED_LOCATION        
(        
INSURED_LOCATION_ID,        
CLAIM_ID,        
LOCATION_DESCRIPTION,        
ADDRESS1,        
ADDRESS2,        
CITY,        
STATE,        
ZIP,        
COUNTRY,        
IS_ACTIVE,        
CREATED_BY,        
CREATED_DATETIME,    
POLICY_LOCATION_ID        
)        
VALUES        
(        
@INSURED_LOCATION_ID,        
@CLAIM_ID,        
@LOCATION_DESCRIPTION,        
@ADDRESS1,        
@ADDRESS2,        
@CITY,        
@STATE,        
@ZIP,        
@COUNTRY,        
'Y',        
@CREATED_BY,        
GETDATE(),    
@POLICY_LOCATION_ID        
)      
    
--Added by Asfa (29-Apr-2008) - iTrack issue #3546    
--INSERT "lien holder(mortgagee)" DATA AT CLAIM PARTIES TABLE                                        
SET @DETAIL_TYPE_LIEN_HOLDER = 157      
      
--Done for Itrack Issue 7150 on 1 April 2010 -- Replaced by code below -- To Fetch Additional Interest which were added manually    
DECLARE CUR CURSOR        
  FOR   
  SELECT ISNULL(MHIL.HOLDER_NAME,'') HOLDER_NAME, MHIL.HOLDER_ADD1, MHIL.HOLDER_ADD2, MHIL.HOLDER_CITY,     
  MHIL.HOLDER_STATE, MHIL.HOLDER_ZIP, MHIL.HOLDER_COUNTRY    
  FROM MNT_HOLDER_INTEREST_LIST MHIL    
  INNER JOIN POL_HOME_OWNER_ADD_INT PHOAI WITH (NOLOCK)    
  ON PHOAI.HOLDER_ID = MHIL.HOLDER_ID    
  INNER JOIN CLM_CLAIM_INFO CCI WITH (NOLOCK)    
  ON PHOAI.CUSTOMER_ID = CCI.CUSTOMER_ID AND PHOAI.POLICY_ID = CCI.POLICY_ID     
  AND PHOAI.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID    
  LEFT JOIN POL_DWELLINGS_INFO PDI WITH (NOLOCK)    
  ON PHOAI.CUSTOMER_ID = PDI.CUSTOMER_ID AND PHOAI.POLICY_ID = PDI.POLICY_ID     
  AND PHOAI.POLICY_VERSION_ID = PDI.POLICY_VERSION_ID     
  AND PHOAI.DWELLING_ID = PDI.DWELLING_ID    
  WHERE CCI.CLAIM_ID=@CLAIM_ID AND PDI.LOCATION_ID = @POLICY_LOCATION_ID    
  
  UNION  
  
  SELECT ISNULL(PHOAI.HOLDER_NAME,'') HOLDER_NAME, PHOAI.HOLDER_ADD1, PHOAI.HOLDER_ADD2, PHOAI.HOLDER_CITY,     
  PHOAI.HOLDER_STATE, PHOAI.HOLDER_ZIP, PHOAI.HOLDER_COUNTRY    
  FROM POL_HOME_OWNER_ADD_INT PHOAI WITH (NOLOCK)    
  INNER JOIN CLM_CLAIM_INFO CCI WITH (NOLOCK)    
  ON PHOAI.CUSTOMER_ID = CCI.CUSTOMER_ID AND PHOAI.POLICY_ID = CCI.POLICY_ID     
  AND PHOAI.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID    
  LEFT JOIN POL_DWELLINGS_INFO PDI WITH (NOLOCK)    
  ON PHOAI.CUSTOMER_ID = PDI.CUSTOMER_ID AND PHOAI.POLICY_ID = PDI.POLICY_ID     
  AND PHOAI.POLICY_VERSION_ID = PDI.POLICY_VERSION_ID     
  AND PHOAI.DWELLING_ID = PDI.DWELLING_ID    
  WHERE CCI.CLAIM_ID=@CLAIM_ID AND PDI.LOCATION_ID = @POLICY_LOCATION_ID    
    
  
OPEN CUR        
FETCH NEXT FROM CUR         
INTO @HOLDER_NAME, @HOLDER_ADDRESS1, @HOLDER_ADDRESS2, @HOLDER_CITY, @HOLDER_STATE, @HOLDER_ZIP, @HOLDER_COUNTRY      
         
          
  WHILE @@FETCH_STATUS = 0        
  BEGIN      
      
SELECT                                        
  @PARTY_ID = ISNULL(MAX(PARTY_ID),0)+1                                        
 FROM                                        
  CLM_PARTIES  WITH(NOLOCK)                                       
 WHERE CLAIM_ID=@CLAIM_ID                                        
        
INSERT INTO CLM_PARTIES( PARTY_ID, CLAIM_ID, NAME, ADDRESS1, ADDRESS2, CITY, STATE,                                        
  ZIP, CREATED_BY, CREATED_DATETIME,                                        
  PARTY_TYPE_ID, COUNTRY, IS_ACTIVE )                                        
 VALUES( @PARTY_ID, @CLAIM_ID, @HOLDER_NAME, @HOLDER_ADDRESS1, @HOLDER_ADDRESS2, @HOLDER_CITY, @HOLDER_STATE,      
  @HOLDER_ZIP, @CREATED_BY, GETDATE(), @DETAIL_TYPE_LIEN_HOLDER,                                        
  @HOLDER_COUNTRY, 'Y')      
    
          
FETCH NEXT FROM CUR         
INTO @HOLDER_NAME, @HOLDER_ADDRESS1, @HOLDER_ADDRESS2, @HOLDER_CITY, @HOLDER_STATE, @HOLDER_ZIP, @HOLDER_COUNTRY      
          
END        
CLOSE CUR        
DEALLOCATE CUR         
        
      
-----------------------------      
      
END  