IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_INSURED_VEHICLE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_INSURED_VEHICLE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name       : Proc_InsertCLM_INSURED_VEHICLE            
Created by      : Amar            
Date            : 5/1/2006            
Purpose       :Evaluation            
Revison History :            
Used In        : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
-- DROP PROC dbo.Proc_InsertCLM_INSURED_VEHICLE            
CREATE PROC dbo.Proc_InsertCLM_INSURED_VEHICLE            
(            
@INSURED_VEHICLE_ID     int output,            
@CLAIM_ID     int,            
@NON_OWNED_VEHICLE     char(1),            
@VEHICLE_YEAR     varchar(4),            
@MAKE     varchar(150),            
@MODEL     varchar(150),            
@VIN     varchar(150),            
@BODY_TYPE     varchar(150),            
@PLATE_NUMBER     varchar(10),            
@STATE     int,            
@IS_ACTIVE     char(1),            
@CREATED_BY     int,            
@CREATED_DATETIME     datetime  ,        
@OWNER_ID INT,        
@DRIVER_ID INT,    
@WHERE_VEHICLE_SEEN varchar(50),    
@WHEN_VEHICLE_SEEN varchar(25),  
@DESCRIBE_DAMAGE varchar(100),  
@USED_WITH_PERMISSION int,  
@PURPOSE_OF_USE varchar(50),  
@ESTIMATE_AMOUNT decimal(12,2),  
@OTHER_VEHICLE_INSURANCE varchar(100),
@POLICY_VEHICLE_ID int
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
            
select @INSURED_VEHICLE_ID=isnull(Max(INSURED_VEHICLE_ID),0)+1 from CLM_INSURED_VEHICLE         
Where Claim_ID = @Claim_ID      
         
INSERT INTO CLM_INSURED_VEHICLE            
(            
INSURED_VEHICLE_ID,            
CLAIM_ID,            
NON_OWNED_VEHICLE,            
VEHICLE_YEAR,            
MAKE,            
MODEL,            
VIN,            
BODY_TYPE,            
PLATE_NUMBER,            
STATE,            
IS_ACTIVE,            
CREATED_BY,            
CREATED_DATETIME  ,        
OWNER_ID,        
DRIVER_ID,    
WHERE_VEHICLE_SEEN,    
WHEN_VEHICLE_SEEN,  
DESCRIBE_DAMAGE,  
USED_WITH_PERMISSION,  
PURPOSE_OF_USE,  
ESTIMATE_AMOUNT,  
OTHER_VEHICLE_INSURANCE,
POLICY_VEHICLE_ID  
)            
VALUES            
(            
@INSURED_VEHICLE_ID,            
@CLAIM_ID,            
@NON_OWNED_VEHICLE,            
@VEHICLE_YEAR,            
@MAKE,            
@MODEL,            
@VIN,            
@BODY_TYPE,            
@PLATE_NUMBER,            
@STATE,            
@IS_ACTIVE,            
@CREATED_BY,            
@CREATED_DATETIME,        
@OWNER_ID,        
@DRIVER_ID,    
@WHERE_VEHICLE_SEEN,    
@WHEN_VEHICLE_SEEN,  
@DESCRIBE_DAMAGE,  
@USED_WITH_PERMISSION,  
@PURPOSE_OF_USE,  
@ESTIMATE_AMOUNT,  
@OTHER_VEHICLE_INSURANCE,
@POLICY_VEHICLE_ID    
)  

--Added by Asfa (29-Apr-2008) - iTrack issue #3546
--INSERT "lien holder(mortgagee)" DATA AT CLAIM PARTIES TABLE                                    
SET @DETAIL_TYPE_LIEN_HOLDER = 157  
  
 --Done for Itrack Issue 7150 on 1 April 2010 -- To Fetch Additional Interest which were added manually
 DECLARE CUR CURSOR    
	FOR 
	SELECT ISNULL(MHIL.HOLDER_NAME,'') HOLDER_NAME, MHIL.HOLDER_ADD1, MHIL.HOLDER_ADD2, MHIL.HOLDER_CITY, 
	MHIL.HOLDER_STATE, MHIL.HOLDER_ZIP, MHIL.HOLDER_COUNTRY
	FROM MNT_HOLDER_INTEREST_LIST MHIL
	INNER JOIN POL_ADD_OTHER_INT PAOI WITH (NOLOCK)
	ON PAOI.HOLDER_ID = MHIL.HOLDER_ID
	INNER JOIN CLM_CLAIM_INFO CCI WITH (NOLOCK)
	ON PAOI.CUSTOMER_ID = CCI.CUSTOMER_ID AND PAOI.POLICY_ID = CCI.POLICY_ID 
	AND PAOI.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID
	LEFT JOIN POL_VEHICLES PV WITH (NOLOCK)
	ON PAOI.CUSTOMER_ID = PV.CUSTOMER_ID AND PAOI.POLICY_ID = PV.POLICY_ID 
	AND PAOI.POLICY_VERSION_ID = PV.POLICY_VERSION_ID AND PAOI.VEHICLE_ID = PV.VEHICLE_ID
	WHERE CCI.CLAIM_ID=@CLAIM_ID AND PV.VEHICLE_ID = @POLICY_VEHICLE_ID

	UNION

	SELECT ISNULL(PAOI.HOLDER_NAME,'') HOLDER_NAME, PAOI.HOLDER_ADD1, PAOI.HOLDER_ADD2, PAOI.HOLDER_CITY, 
	PAOI.HOLDER_STATE, PAOI.HOLDER_ZIP, PAOI.HOLDER_COUNTRY
	FROM POL_ADD_OTHER_INT PAOI
	INNER JOIN CLM_CLAIM_INFO CCI WITH (NOLOCK)
	ON PAOI.CUSTOMER_ID = CCI.CUSTOMER_ID AND PAOI.POLICY_ID = CCI.POLICY_ID 
	AND PAOI.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID
	LEFT JOIN POL_VEHICLES PV WITH (NOLOCK)
	ON PAOI.CUSTOMER_ID = PV.CUSTOMER_ID AND PAOI.POLICY_ID = PV.POLICY_ID 
	AND PAOI.POLICY_VERSION_ID = PV.POLICY_VERSION_ID AND PAOI.VEHICLE_ID = PV.VEHICLE_ID
	WHERE CCI.CLAIM_ID=@CLAIM_ID AND PV.VEHICLE_ID = @POLICY_VEHICLE_ID AND PAOI.HOLDER_ID IS NULL
	
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

GO

