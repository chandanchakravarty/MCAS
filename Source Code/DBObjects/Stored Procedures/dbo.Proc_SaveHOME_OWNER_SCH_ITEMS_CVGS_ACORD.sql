IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SaveHOME_OWNER_SCH_ITEMS_CVGS_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SaveHOME_OWNER_SCH_ITEMS_CVGS_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*********************************************************************************************************

Modified By	: Ravindra Gupta
Modified On	: 09-05-20006
urpose		: To insert 'Y' by default in IS_ACTIVE field 

*************************************************************************************************************/
--drop proc Dbo.Proc_SaveHOME_OWNER_SCH_ITEMS_CVGS_ACORD
CREATE PROC Dbo.Proc_SaveHOME_OWNER_SCH_ITEMS_CVGS_ACORD      
(        
 @CUSTOMER_ID    int,        
 @APP_ID      int,        
 @APP_VERSION_ID smallint,        
 @ITEM_ID      smallint OUTPUT,        
 @CATEGORY      int,        
 @CATEGORY_CODE NVarChar(20),        
 --@DETAILED_DESC  nvarchar(510),
 @DETAILED_DESC  nvarchar(255),        
 --@SN_DETAILS     nvarchar(50),
 @SN_DETAILS     nvarchar(25),         
 @AMOUNT_OF_INSURANCE     decimal(9),   
 @DEDUCTIBLE     decimal(9),  
 @PREMIUM      decimal(9),        
 @RATE      decimal(9),        
 --@APPRAISAL      nchar(2),
 @APPRAISAL      nchar(1),        
 @APPRAISAL_DESC   varchar(50),        
 @PURCHASE_APPRAISAL_DATE     datetime,        
 --@BREAKAGE_COVERAGE      nchar(2),
 @BREAKAGE_COVERAGE      nchar(1),        
 @BREAKAGE_DESC          varchar(50),        
 @CREATED_BY      int,        
 @CREATED_DATETIME      datetime        
)        
AS        
BEGIN        
      
DECLARE @CATEGORY_ID Int      
DECLARE @COVERAGE_CODE_ID Int  
  
EXECUTE @COVERAGE_CODE_ID =  Proc_GetCOVERAGE_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@CATEGORY_CODE      

select @ITEM_ID  = @COVERAGE_CODE_ID

  SELECT @DEDUCTIBLE = LIMIT_DEDUC_ID      
  FROM MNT_COVERAGE_RANGES      
  WHERE COV_ID = @COVERAGE_CODE_ID AND      
   ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE,0) 

--EXECUTE @CATEGORY_ID = Proc_GetLookupID 'PCCD',@CATEGORY_CODE      
      
 /*Generating the Item Id*/        
/* SELECT @ITEM_ID = IsNull(Max(ITEM_ID),0) + 1       
FROM APP_HOME_OWNER_SCH_ITEMS_CVGS        
WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
 APP_ID = @APP_ID AND      
 APP_VERSION_ID = @APP_VERSION_ID*/
       
INSERT INTO APP_HOME_OWNER_SCH_ITEMS_CVGS        
 (        
 CUSTOMER_ID,       
 APP_ID,       
 APP_VERSION_ID,       
 ITEM_ID,      
 CATEGORY,        
 DETAILED_DESC,      
 SN_DETAILS,       
 AMOUNT_OF_INSURANCE,   
 DEDUCTIBLE,      
 PREMIUM,        
 RATE,       
 APPRAISAL,      
 PURCHASE_APPRAISAL_DATE,      
 BREAKAGE_COVERAGE,      
 APPRAISAL_DESC,      
 BREAKAGE_DESC,        
 IS_ACTIVE,      
 CREATED_BY,       
 CREATED_DATETIME        
 )        
 VALUES        
 (        
 @CUSTOMER_ID,       
 @APP_ID,       
 @APP_VERSION_ID,       
 @ITEM_ID,
 @COVERAGE_CODE_ID,        
 @DETAILED_DESC,       
 @SN_DETAILS,      
 @AMOUNT_OF_INSURANCE,  
 @DEDUCTIBLE  ,    
 @PREMIUM,        
 @RATE,       
 @APPRAISAL,      
 @PURCHASE_APPRAISAL_DATE,       
 @BREAKAGE_COVERAGE,      
 @APPRAISAL_DESC,      
 @BREAKAGE_DESC,        
 'Y',       
 @CREATED_BY,       
 @CREATED_DATETIME        
 )     

/*DECLARE @ITEM_DETAIL_ID INT
SELECT @ITEM_DETAIL_ID = ISNULL(MAX(ITEM_DETAIL_ID),0) + 1
FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS
WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND ITEM_ID = @ITEM_ID*/

--Inserting one details record
INSERT INTO APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS  
(
	CUSTOMER_ID, APP_ID, APP_VERSION_ID, ITEM_ID, ITEM_DETAIL_ID,
	ITEM_NUMBER, ITEM_DESCRIPTION, ITEM_SERIAL_NUMBER, ITEM_INSURING_VALUE,
	ITEM_APPRAISAL_BILL, ITEM_PICTURE_ATTACHED,IS_ACTIVE
)
VALUES
(
	@CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @ITEM_ID, 1,
	'1', 'Not Specified', '', @AMOUNT_OF_INSURANCE, 
	@APPRAISAL, 'N','Y'
	
)
       
END      




GO

