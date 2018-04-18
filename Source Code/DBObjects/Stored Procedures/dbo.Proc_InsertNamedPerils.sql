IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertNamedPerils]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertNamedPerils]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
  /*----------------------------------------------------------            
Proc Name       : dbo.POL_PERILS            
Created by      : Pradeep kushwaha   
Date            : 23/03/2010            
Purpose       :To insert records in POL_PERILS table.            
Revison History :            
Used In        : Ebix Advantage            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
--DROP PROC dbo.Proc_InsertNamedPerils     
/****** Script for Proc_InsertNamedPerils into DATABASE  ******/  
   
         
CREATE PROC [dbo].[Proc_InsertNamedPerils]   
(        
 @CUSTOMER_ID int,  
 @POLICY_ID int ,  
 @POLICY_VERSION_ID smallint,  
 @PERIL_ID smallint out,  
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
 @IS_ACTIVE nchar(2)=null,     
 @CREATED_BY int=null,  
 @CREATED_DATETIME datetime=null,  
 @RAWVALUES nvarchar(40) =null,  
 @REMARKS nvarchar(4000)=null,  
 @PARKING_SPACES nvarchar(40)=null,  
 @CLAIM_RATIO DECIMAL(12,2)=null,  
 @RAW_MATERIAL_VALUE nvarchar (40)=null,  
 @CONTENT_VALUE nvarchar(40)=null,  
 @ASSIST24 int=null,  
 @BONUS DECIMAL(12,2)  ,
 @CO_APPLICANT_ID INT = NULL,
 @LOCATION_NUMBER INT=NULL,
 @ITEM_NUMBER INT=NULL,
 @ACTUAL_INSURED_OBJECT NVARCHAR(250)=NULL,
 @ORIGINAL_VERSION_ID INT=NULL,
 @EXCEEDED_PREMIUM INT = NULL 
)            
AS   
DECLARE @COUNT INT           
BEGIN  
  
  
SELECT  @PERIL_ID=isnull(Max(PERIL_ID),0)+1 from POL_PERILS  WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID    

SELECT  @COUNT= COUNT(*) FROM POL_PERILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID 
 and POLICY_VERSION_ID=@POLICY_VERSION_ID AND LOCATION_NUMBER=@LOCATION_NUMBER AND ITEM_NUMBER=@ITEM_NUMBER AND IS_ACTIVE='Y'
  IF(@COUNT>=1)   
  BEGIN
  RETURN -5
  END
 ELSE
 BEGIN    
      
INSERT INTO POL_PERILS            
(            
 CUSTOMER_ID ,  
 POLICY_ID,  
 POLICY_VERSION_ID,  
 PERIL_ID,  
 LOCATION,  
 OCCUPANCY,  
 CONSTRUCTION,  
 ACTIVITY_TYPE,  
 VR,  
 LMI,  
 BUILDING,  
 MRI,  
 TYPE,  
 LOSS,  
 LOYALTY,  
 PERC_LOYALTY,  
 DEDUCTIBLE_OPTION,  
 MULTIPLE_DEDUCTIBLE,  
 CATEGORY,  
 IS_ACTIVE,     
 CREATED_BY ,  
 CREATED_DATETIME,  
 RAWVALUES,  
 REMARKS,  
 PARKING_SPACES,  
 CLAIM_RATIO,  
 RAW_MATERIAL_VALUE,  
 CONTENT_VALUE,  
 ASSIST24,  
 BONUS  ,
 CO_APPLICANT_ID,
 LOCATION_NUMBER,
 ITEM_NUMBER,
 ACTUAL_INSURED_OBJECT,
 ORIGINAL_VERSION_ID,
  EXCEEDED_PREMIUM 
)            
VALUES            
(            
 @CUSTOMER_ID ,  
 @POLICY_ID,  
 @POLICY_VERSION_ID,  
 @PERIL_ID,  
 @LOCATION,  
 @OCCUPANCY,  
 @CONSTRUCTION,  
 @ACTIVITY_TYPE,  
 @VR,  
 @LMI,  
 @BUILDING,  
 @MRI,  
 @TYPE,  
 @LOSS,  
 @LOYALTY,  
 @PERC_LOYALTY,  
 @DEDUCTIBLE_OPTION,  
 @MULTIPLE_DEDUCTIBLE,  
 @CATEGORY,  
 @IS_ACTIVE,     
 @CREATED_BY ,  
 @CREATED_DATETIME,  
 @RAWVALUES,  
 @REMARKS,  
 @PARKING_SPACES,  
 @CLAIM_RATIO,  
 @RAW_MATERIAL_VALUE,  
 @CONTENT_VALUE,  
 @ASSIST24,  
 @BONUS  ,
 @CO_APPLICANT_ID,
 @LOCATION_NUMBER ,
 @ITEM_NUMBER ,
 @ACTUAL_INSURED_OBJECT,
 @ORIGINAL_VERSION_ID,
   @EXCEEDED_PREMIUM 
)           
 END
 RETURN 1 
END            
--END           
------ADDED BY PRAVESH TO SAVE DEFAULT COVERAGS  
 EXEC Proc_SaveProductDefaultCoverages        
 @CUSTOMER_ID,     
 @POLICY_ID   ,    
 @POLICY_VERSION_ID,  
 @PERIL_ID ,  
 @CREATED_BY   
          
        
      
    
    
    
    
GO

