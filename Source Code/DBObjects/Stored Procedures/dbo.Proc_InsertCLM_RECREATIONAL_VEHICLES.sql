IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_RECREATIONAL_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_RECREATIONAL_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_InsertCLM_RECREATIONAL_VEHICLES                 
Created by      : Sumit Chhabra      
Date            : 11/08/2008                  
Purpose      : Inserts a record in CLM_RECREATION_VEHICLES                  
Revison History :                  
Used In   : Wolverine                  
                
      
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
--drop proc Proc_InsertCLM_RECREATIONAL_VEHICLES                 
                
CREATE PROC dbo.Proc_InsertCLM_RECREATIONAL_VEHICLES               
(                  
 @CLAIM_ID int,                  
 @COMPANY_ID_NUMBER     int,                  
 @YEAR int,                  
 @MAKE nvarchar(75),                  
 @MODEL nvarchar(75),                  
 @SERIAL nvarchar(75),                  
 @STATE_REGISTERED int,                  
 @VEHICLE_TYPE int,                  
 @HORSE_POWER nvarchar(10),                      
 @CREATED_BY int,            
 @CREATED_DATETIME datetime,      
 @REMARKS nvarchar(500),    
 @POL_REC_VEH_ID smallint,  
 @REC_VEH_ID smallint out         
                   
)                  
AS                  
                  
  
                  
BEGIN     
--Added for Itrack Issue 6665 on 29 Oct 09
DECLARE @PARTY_ID INT
DECLARE @HOLDER_NAME VARCHAR(60)  
DECLARE @HOLDER_ADDRESS1  VARCHAR(150)  
DECLARE @HOLDER_ADDRESS2  VARCHAR(150)  
DECLARE @HOLDER_CITY   VARCHAR(70)  
DECLARE @HOLDER_STATE   INT  
DECLARE @HOLDER_ZIP  VARCHAR(20)  
DECLARE @HOLDER_COUNTRY   INT
DECLARE @DETAIL_TYPE_LIEN_HOLDER INT             
                   
 IF EXISTS                  
 (                  
  SELECT * FROM CLM_RECREATIONAL_VEHICLES                 
  WHERE CLAIM_ID = @CLAIM_ID and      
   COMPANY_ID_NUMBER = @COMPANY_ID_NUMBER                  
 )                  
 BEGIN                  
  RETURN -2                  
 END                  
                  
 SELECT  @REC_VEH_ID = ISNULL(MAX(REC_VEH_ID),0) + 1                  
 FROM CLM_RECREATIONAL_VEHICLES                 
 WHERE CLAIM_ID = @CLAIM_ID      
                        
 INSERT INTO CLM_RECREATIONAL_VEHICLES                 
 (                  
  CLAIM_ID,                            
  REC_VEH_ID,                  
  COMPANY_ID_NUMBER,                  
  YEAR,                  
  MAKE,                  
  MODEL,                  
  SERIAL,                  
  STATE_REGISTERED,                  
  VEHICLE_TYPE,                  
  HORSE_POWER,                  
  REMARKS,                        
  ACTIVE,                  
  CREATED_BY,                  
  CREATED_DATETIME,    
  POL_REC_VEH_ID                  
 )                  
 VALUES                  
 (                  
  @CLAIM_ID,      
  @REC_VEH_ID,                  
  @COMPANY_ID_NUMBER,                  
  @YEAR,                  
  @MAKE,                  
  @MODEL,                  
  @SERIAL,                  
  @STATE_REGISTERED,                  
  @VEHICLE_TYPE,                  
  @HORSE_POWER,                  
  @REMARKS,                  
  'Y',                  
  @CREATED_BY,                  
  @CREATED_DATETIME,    
  @POL_REC_VEH_ID                  
 )                       
          
 --Added for Itrack Issue 6665 on 29 Oct 09
SET @DETAIL_TYPE_LIEN_HOLDER = 157  
  
DECLARE CUR CURSOR    
    FOR SELECT ISNULL(MHIL.HOLDER_NAME,'') HOLDER_NAME, MHIL.HOLDER_ADD1, MHIL.HOLDER_ADD2, MHIL.HOLDER_CITY, 
		MHIL.HOLDER_STATE, MHIL.HOLDER_ZIP, MHIL.HOLDER_COUNTRY
		FROM MNT_HOLDER_INTEREST_LIST MHIL
		INNER JOIN POL_HOMEOWNER_REC_VEH_ADD_INT PHORVAI WITH (NOLOCK)
		ON PHORVAI.HOLDER_ID = MHIL.HOLDER_ID
		INNER JOIN CLM_CLAIM_INFO CCI WITH (NOLOCK)
		ON PHORVAI.CUSTOMER_ID = CCI.CUSTOMER_ID AND PHORVAI.POLICY_ID = CCI.POLICY_ID 
		AND PHORVAI.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID
		LEFT JOIN POL_HOME_OWNER_RECREATIONAL_VEHICLES PHORV WITH (NOLOCK)
		ON PHORVAI.CUSTOMER_ID = PHORV.CUSTOMER_ID AND PHORVAI.POLICY_ID = PHORV.POLICY_ID 
		AND PHORVAI.POLICY_VERSION_ID = PHORV.POLICY_VERSION_ID 
		AND PHORVAI.REC_VEH_ID = PHORV.REC_VEH_ID
		WHERE CCI.CLAIM_ID=@CLAIM_ID AND PHORV.REC_VEH_ID = @POL_REC_VEH_ID

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

 --Added till here
END                  
GO

