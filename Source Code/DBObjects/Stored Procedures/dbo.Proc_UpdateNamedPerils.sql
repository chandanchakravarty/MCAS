IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateNamedPerils]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateNamedPerils]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name       : dbo.POL_PERILS            
Created by      : Pradeep kushwaha   
Date            : 23/03/2010            
Purpose       :To Update records in POL_PERILS table.            
Revison History :            
Used In        : Ebix Advantage            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
--DROP PROC dbo.Proc_UpdateNamedPerils     
  
CREATE PROC [dbo].[Proc_UpdateNamedPerils]                                           
(  
   
@PERIL_ID smallint,  
@CUSTOMER_ID int,
@POLICY_ID int ,
@POLICY_VERSION_ID smallint,  
@LOCATION int,  
@OCCUPANCY nvarchar(8),  
@CONSTRUCTION nvarchar(8)=null,  
@ACTIVITY_TYPE nvarchar(50),  
@VR numeric(18,2)=null,  
@LMI numeric(18,2)=null,  
@BUILDING numeric(18,2)=null,  
@MRI numeric(18,2)=null,  
@TYPE int=null,  
@LOSS numeric(18,2)=null,  
@LOYALTY numeric=null,  
@PERC_LOYALTY numeric=null,  
@DEDUCTIBLE_OPTION int=null,  
@MULTIPLE_DEDUCTIBLE nvarchar(8)=null,  
@CATEGORY nvarchar(6)=null,  
@MODIFIED_BY int,  
@LAST_UPDATED_DATETIME datetime,  
@RAWVALUES nvarchar(40) =null,  
@REMARKS nvarchar(4000)=null,  
@PARKING_SPACES nvarchar(40)=null,  
@CLAIM_RATIO Decimal(12,2)=null,  
@RAW_MATERIAL_VALUE nvarchar (40)=null,  
@CONTENT_VALUE nvarchar(40)=null,  
@ASSIST24 int=null, 
@BONUS Decimal(12,2) ,
 @LOCATION_NUMBER INT=NULL,
 @ITEM_NUMBER INT=NULL,
 @ACTUAL_INSURED_OBJECT NVARCHAR(250)=NULL,
 @EXCEEDED_PREMIUM INT = NULL 

-- ,  @ORIGINAL_VERSION_ID INT=NULL
  
)  
AS 
DECLARE @COUNT INT      
BEGIN  

SELECT  @COUNT= COUNT(*) FROM POL_PERILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID 
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND LOCATION_NUMBER=@LOCATION_NUMBER AND ITEM_NUMBER=@ITEM_NUMBER and  PERIL_ID<>@PERIL_ID AND IS_ACTIVE='Y'
  IF(@COUNT>=1)   
  BEGIN
  RETURN -5
  END
 ELSE
 BEGIN  

UPDATE POL_PERILS                                            
  SET   
 LOCATION= @LOCATION ,  
 OCCUPANCY=@OCCUPANCY,  
 CONSTRUCTION=@CONSTRUCTION,  
 ACTIVITY_TYPE=@ACTIVITY_TYPE,  
 VR=@VR,  
 LMI=@LMI,  
 BUILDING=@BUILDING,  
 MRI=@MRI,  
 TYPE=@TYPE,  
 LOSS=@LOSS,  
 LOYALTY=@LOYALTY,  
 PERC_LOYALTY=@PERC_LOYALTY,  
 DEDUCTIBLE_OPTION=@DEDUCTIBLE_OPTION,  
 MULTIPLE_DEDUCTIBLE=@MULTIPLE_DEDUCTIBLE,  
 CATEGORY=@CATEGORY,  
 MODIFIED_BY=@MODIFIED_BY,  
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,  
 RAWVALUES=@RAWVALUES,  
 REMARKS=@REMARKS,  
 PARKING_SPACES=@PARKING_SPACES,  
 CLAIM_RATIO=@CLAIM_RATIO,  
 RAW_MATERIAL_VALUE=@RAW_MATERIAL_VALUE,  
 CONTENT_VALUE=@CONTENT_VALUE,  
 ASSIST24=@ASSIST24 ,
 BONUS=@BONUS, 
 LOCATION_NUMBER =@LOCATION_NUMBER ,
 ITEM_NUMBER =@ITEM_NUMBER ,
 ACTUAL_INSURED_OBJECT=@ACTUAL_INSURED_OBJECT,
 EXCEEDED_PREMIUM = @EXCEEDED_PREMIUM
 --, ORIGINAL_VERSION_ID=@ORIGINAL_VERSION_ID
 WHERE 
 CUSTOMER_ID =  @CUSTOMER_ID AND      
 POLICY_ID = @POLICY_ID AND      
 POLICY_VERSION_ID=@POLICY_VERSION_ID AND
 PERIL_ID=@PERIL_ID
   END
   RETURN 1
END  

GO

