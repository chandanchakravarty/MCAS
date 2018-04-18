IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_INSURED_BOAT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_INSURED_BOAT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name       : dbo.Proc_UpdateCLM_INSURED_BOAT    
Created by      : Sumit Chhabra  
Date            : 05/24/2006    
Purpose         :To udpate records at CLM_INSURED_BOAT .    
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
-- DROP PROC dbo.Proc_UpdateCLM_INSURED_BOAT    
CREATE  PROC dbo.Proc_UpdateCLM_INSURED_BOAT    
(    
@CLAIM_ID int,  
@BOAT_ID int,  
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
@MODIFIED_BY int,  
@LAST_UPDATED_DATETIME datetime,
--@POLICY_BOAT_ID int,
@WHERE_BOAT_SEEN varchar(50),
@INCLUDE_TRAILER int      
)    
AS    
begin    
   
 --Added for Itrack Issue 6665 on 26 Nov 09
 DECLARE @PARTY_ID INT    
 DECLARE @HOLDER_NAME VARCHAR(60)      
 DECLARE @HOLDER_ADDRESS1  VARCHAR(150)      
 DECLARE @HOLDER_ADDRESS2  VARCHAR(150)      
 DECLARE @HOLDER_CITY   VARCHAR(70)      
 DECLARE @HOLDER_STATE   INT      
 DECLARE @HOLDER_ZIP  VARCHAR(20)      
 DECLARE @HOLDER_COUNTRY   INT    
 DECLARE @DETAIL_TYPE_LIEN_HOLDER INT

 UPDATE CLM_INSURED_BOAT  
 SET    
  SERIAL_NUMBER=@SERIAL_NUMBER,  
  YEAR=@YEAR,  
  MAKE=@MAKE,  
  MODEL=@MODEL,  
  BODY_TYPE=@BODY_TYPE,  
  LENGTH=@LENGTH,  
  WEIGHT=@WEIGHT,  
  HORSE_POWER=@HORSE_POWER,  
  OTHER_HULL_TYPE=@OTHER_HULL_TYPE,  
  PLATE_NUMBER=@PLATE_NUMBER,  
  STATE=@STATE,  
  MODIFIED_BY=@MODIFIED_BY,  
  LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,
--POLICY_BOAT_ID = @POLICY_BOAT_ID,
  WHERE_BOAT_SEEN = @WHERE_BOAT_SEEN,
  INCLUDE_TRAILER = @INCLUDE_TRAILER
 WHERE  
  CLAIM_ID=@CLAIM_ID AND  
  BOAT_ID=@BOAT_ID  
  
 --Added for Itrack Issue 6665 on 26 Nov 09

SET @DETAIL_TYPE_LIEN_HOLDER = 157

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
  WHERE CCI.CLAIM_ID=@CLAIM_ID AND PWTI.TRAILER_ID = @BOAT_ID    
    
    
    
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
  @HOLDER_ZIP, @MODIFIED_BY, GETDATE(), @DETAIL_TYPE_LIEN_HOLDER,                                        
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

