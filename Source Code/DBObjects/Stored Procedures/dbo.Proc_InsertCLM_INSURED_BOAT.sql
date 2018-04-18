IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_INSURED_BOAT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_INSURED_BOAT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name       : dbo.Proc_InsertCLM_INSURED_BOAT          
Created by      : Sumit Chhabra      
Date            : 5/24/2006          
Purpose         :To insert records in CLM_INSURED_BOAT .          
Revison History :          
Used In        : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
-- DROP  PROC dbo.Proc_InsertCLM_INSURED_BOAT          
CREATE   PROC dbo.Proc_InsertCLM_INSURED_BOAT          
(          
@CLAIM_ID int,        
@BOAT_ID int output,        
@SERIAL_NUMBER varchar(150),        
@YEAR int,        
@MAKE varchar(150),        
@MODEL varchar(150),        
@BODY_TYPE int,        
@LENGTH nvarchar(20),        
@WEIGHT nvarchar(10),        
@HORSE_POWER decimal(10,2),    
@OTHER_HULL_TYPE varchar(50),        
@PLATE_NUMBER varchar(10),        
@STATE int,        
@CREATED_BY int,        
@CREATED_DATETIME datetime,    
@POLICY_BOAT_ID int,    
@WHERE_BOAT_SEEN varchar(50),    
@INCLUDE_TRAILER int        
)          
AS          
begin          
       
DECLARE @PARTY_ID INT    
DECLARE @HOLDER_NAME VARCHAR(60)      
DECLARE @HOLDER_ADDRESS1  VARCHAR(150)      
DECLARE @HOLDER_ADDRESS2  VARCHAR(150)      
DECLARE @HOLDER_CITY   VARCHAR(70)      
DECLARE @HOLDER_STATE   INT      
DECLARE @HOLDER_ZIP  VARCHAR(20)      
DECLARE @HOLDER_COUNTRY   INT    
DECLARE @DETAIL_TYPE_LIEN_HOLDER INT    
    
      
 SELECT         
  @BOAT_ID=ISNULL(MAX(BOAT_ID),0)+1         
 FROM         
  CLM_INSURED_BOAT         
 WHERE        
  CLAIM_ID=@CLAIM_ID        
  
        
 INSERT INTO CLM_INSURED_BOAT        
 (        
  CLAIM_ID,        
  BOAT_ID,        
  SERIAL_NUMBER,        
  YEAR,        
  MAKE,        
  MODEL,        
  BODY_TYPE,        
  LENGTH,        
  WEIGHT,        
  HORSE_POWER,        
  OTHER_HULL_TYPE,        
  PLATE_NUMBER,        
  STATE,        
  CREATED_BY,        
  CREATED_DATETIME,        
  IS_ACTIVE,    
  POLICY_BOAT_ID,    
  WHERE_BOAT_SEEN,    
  INCLUDE_TRAILER    
 )        
 VALUES        
 (        
  @CLAIM_ID,        
  @BOAT_ID,        
  @SERIAL_NUMBER,        
  @YEAR,        
  @MAKE,        
  @MODEL,        
  @BODY_TYPE,        
  @LENGTH,        
  @WEIGHT,        
  @HORSE_POWER,        
  @OTHER_HULL_TYPE,        
  @PLATE_NUMBER,        
  @STATE,        
  @CREATED_BY,        
  @CREATED_DATETIME,        
  'Y',    
  @POLICY_BOAT_ID,    
  @WHERE_BOAT_SEEN,    
  @INCLUDE_TRAILER    
 )    
    
--Added by Asfa (29-Apr-2008) - iTrack issue #3546    
--INSERT "lien holder(mortgagee)" DATA AT CLAIM PARTIES TABLE                                        
SET @DETAIL_TYPE_LIEN_HOLDER = 157      
      
DECLARE CUR CURSOR        
    FOR SELECT ISNULL(MHIL.HOLDER_NAME,'') HOLDER_NAME, MHIL.HOLDER_ADD1, MHIL.HOLDER_ADD2, MHIL.HOLDER_CITY,     
  MHIL.HOLDER_STATE, MHIL.HOLDER_ZIP, MHIL.HOLDER_COUNTRY    
  FROM MNT_HOLDER_INTEREST_LIST MHIL    
  INNER JOIN POL_WATERCRAFT_COV_ADD_INT PWCAI WITH (NOLOCK)    
  ON PWCAI.HOLDER_ID = MHIL.HOLDER_ID    
  INNER JOIN CLM_CLAIM_INFO CCI WITH (NOLOCK)    
  ON PWCAI.CUSTOMER_ID = CCI.CUSTOMER_ID AND PWCAI.POLICY_ID = CCI.POLICY_ID     
  AND PWCAI.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID    
  LEFT JOIN POL_WATERCRAFT_INFO PWI WITH (NOLOCK)    
  ON PWI.CUSTOMER_ID = PWCAI.CUSTOMER_ID AND PWI.POLICY_ID = PWCAI.POLICY_ID     
  AND PWI.POLICY_VERSION_ID = PWCAI.POLICY_VERSION_ID AND PWI.BOAT_ID = PWCAI.BOAT_ID    
  WHERE CCI.CLAIM_ID=@CLAIM_ID AND PWI.BOAT_ID = @POLICY_BOAT_ID    
    
    
    
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
        
 --Added for Itrack Issue 6665 on 29 Oct 09
IF(@INCLUDE_TRAILER = 10963)
BEGIN
 DECLARE CUR CURSOR        
    FOR SELECT ISNULL(MHIL.HOLDER_NAME,'') HOLDER_NAME, MHIL.HOLDER_ADD1, MHIL.HOLDER_ADD2, MHIL.HOLDER_CITY,     
  MHIL.HOLDER_STATE, MHIL.HOLDER_ZIP, MHIL.HOLDER_COUNTRY    
  FROM MNT_HOLDER_INTEREST_LIST MHIL    
  INNER JOIN POL_WATERCRAFT_TRAILER_ADD_INT PWTRAI WITH (NOLOCK)    
  ON PWTRAI.HOLDER_ID = MHIL.HOLDER_ID    
  INNER JOIN CLM_CLAIM_INFO CCI WITH (NOLOCK)    
  ON PWTRAI.CUSTOMER_ID = CCI.CUSTOMER_ID AND PWTRAI.POLICY_ID = CCI.POLICY_ID     
  AND PWTRAI.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID    
  LEFT JOIN POL_WATERCRAFT_TRAILER_INFO PWTI WITH (NOLOCK)    
  ON PWTI.CUSTOMER_ID = PWTRAI.CUSTOMER_ID AND PWTI.POLICY_ID = PWTRAI.POLICY_ID     
  AND PWTI.POLICY_VERSION_ID = PWTRAI.POLICY_VERSION_ID AND PWTI.TRAILER_ID = PWTRAI.TRAILER_ID    
  WHERE CCI.CLAIM_ID=@CLAIM_ID AND PWTI.TRAILER_ID = @POLICY_BOAT_ID    
    
    
    
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
END   
-----------------------------      
        
END  
GO

