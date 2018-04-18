IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERTMARITIME]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_INSERTMARITIME]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
PROC NAME       : DBO.POL_MARITIME                  
CREATED BY      : PRADEEP KUSHWAHA         
DATE            : 09/04/2010                  
PURPOSE       :TO INSERT RECORDS IN POL_MARITIME TABLE.                  
REVISON HISTORY :                  
USED IN        : EBIX ADVANTAGE                  
------------------------------------------------------------                  
DATE     REVIEW BY          COMMENTS                  
------   ------------       -------------------------*/                  
--DROP PROC DBO.PROC_INSERTMARITIME           
/****** SCRIPT FOR PROC_INSERTMARITIME INTO DATABASE  ******/        
         
         
               
CREATE PROC [dbo].[PROC_INSERTMARITIME]         
(              
@CUSTOMER_ID INT,        
@POLICY_ID INT,        
@POLICY_VERSION_ID INT,        
@MARITIME_ID INT OUT,        
@VESSEL_NUMBER NUMERIC,        
@NAME_OF_VESSEL NVARCHAR(140),        
@TYPE_OF_VESSEL NVARCHAR(40),        
@MANUFACTURE_YEAR INT=NULL,        
@MANUFACTURER NVARCHAR(100)=NULL,        
@BUILDER NVARCHAR(100)=NULL,        
@CONSTRUCTION NVARCHAR(200)=NULL,        
@PROPULSION NVARCHAR(40)=NULL,        
@CLASSIFICATION NVARCHAR(100)=NULL,        
@LOCAL_OPERATION NVARCHAR(200)=NULL,        
@LIMIT_NAVIGATION NVARCHAR(200)=NULL,        
@PORT_REGISTRATION NVARCHAR(100)=NULL,        
@REGISTRATION_NUMBER NVARCHAR(100)=NULL,        
@TIE_NUMBER NVARCHAR(100)=NULL,        
@VESSEL_ACTION_NAUTICO_CLUB INT=NULL,        
@NAME_OF_CLUB NVARCHAR(140)=NULL,        
@LOCAL_CLUB NVARCHAR(400)=NULL,        
@NUMBER_OF_CREW INT=NULL,        
@NUMBER_OF_PASSENGER INT=NULL,        
@REMARKS NVARCHAR(4000)=NULL,        
@IS_ACTIVE NVARCHAR(4),        
@CREATED_BY INT,        
@CREATED_DATETIME DATETIME,
@CO_APPLICANT_ID INT = NULL,
@ORIGINAL_VERSION_ID INT=NULL,
@EXCEEDED_PREMIUM INT = NULL 
)                  
AS                  
BEGIN        
        
IF NOT EXISTS(SELECT VESSEL_NUMBER  FROM POL_MARITIME WITH(NOLOCK)   WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND VESSEL_NUMBER=@VESSEL_NUMBER )       
 BEGIN  
 SELECT  @MARITIME_ID=ISNULL(MAX(MARITIME_ID),0)+1 FROM POL_MARITIME WITH(NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID        
      
 INSERT INTO POL_MARITIME                  
 (                  
          
 CUSTOMER_ID,        
 POLICY_ID,        
 POLICY_VERSION_ID,        
 MARITIME_ID,        
 VESSEL_NUMBER,        
 NAME_OF_VESSEL,        
 TYPE_OF_VESSEL,        
 MANUFACTURE_YEAR,        
 MANUFACTURER,        
 BUILDER,        
 CONSTRUCTION,        
 PROPULSION,        
 CLASSIFICATION,        
 LOCAL_OPERATION,        
 LIMIT_NAVIGATION,        
 PORT_REGISTRATION,        
 REGISTRATION_NUMBER,        
 TIE_NUMBER,        
 VESSEL_ACTION_NAUTICO_CLUB,        
 NAME_OF_CLUB,        
 LOCAL_CLUB,        
 NUMBER_OF_CREW,        
 NUMBER_OF_PASSENGER,        
 REMARKS,        
 IS_ACTIVE,        
 CREATED_BY,        
 CREATED_DATETIME,
 CO_APPLICANT_ID,
 ORIGINAL_VERSION_ID,
  EXCEEDED_PREMIUM   
 )                  
 VALUES                  
 (                  
 @CUSTOMER_ID,        
 @POLICY_ID,        
 @POLICY_VERSION_ID,        
 @MARITIME_ID,        
 @VESSEL_NUMBER,        
 @NAME_OF_VESSEL,        
 @TYPE_OF_VESSEL,        
 @MANUFACTURE_YEAR,        
 @MANUFACTURER,        
 @BUILDER,        
 @CONSTRUCTION,        
 @PROPULSION,        
 @CLASSIFICATION,        
 @LOCAL_OPERATION,        
 @LIMIT_NAVIGATION,        
 @PORT_REGISTRATION,        
 @REGISTRATION_NUMBER,        
 @TIE_NUMBER,        
 @VESSEL_ACTION_NAUTICO_CLUB,        
 @NAME_OF_CLUB,        
 @LOCAL_CLUB,        
 @NUMBER_OF_CREW,        
 @NUMBER_OF_PASSENGER,        
 @REMARKS,        
 @IS_ACTIVE,        
 @CREATED_BY,        
 @CREATED_DATETIME,
 @CO_APPLICANT_ID,
 @ORIGINAL_VERSION_ID,
   @EXCEEDED_PREMIUM 
         
 )                 
         
  ----added by Pravesh updating Default Product Coverages       
 exec Proc_SaveProductDefaultCoverages       
  @CUSTOMER_ID ,                              
  @POLICY_ID ,                              
  @POLICY_VERSION_ID   ,                              
  @MARITIME_ID ,         
  @CREATED_BY       
        
  RETURN 1  
END   
ELSE  
 RETURN -2         
END 
GO

