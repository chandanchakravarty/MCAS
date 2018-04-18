IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertDiscountSurcharge]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertDiscountSurcharge]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name       : dbo.POL_PERILS            
Created by      : Chetna Agarwal  
Date            : 12/04/2010            
Purpose       :To insert records in MNT_DISCOUNT_SURCHARGE table.            
Revison History :            
Used In        : Ebix Advantage            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
--DROP PROC dbo.Proc_InsertDISCOUNTSURCHARGE 

CREATE PROC [dbo].[Proc_InsertDiscountSurcharge]  
(  
@DISCOUNT_ID int out,
@TYPE_ID int,
@LOB_ID int=NULL,
@SUBLOB_ID int=NULL,
@DISCOUNT_TYPE INT=NULL,
@DISCOUNT_DESCRIPTION VARCHAR(40)=NULL,
@PERCENTAGE DECIMAL=NULL,
@IS_ACTIVE NCHAR(1)=NULL,
@CREATED_BY INT=NULL,
@CREATED_DATETIME DATETIME=NULL,
@MODIFIED_BY INT=NULL,
@LAST_UPDATED_DATETIME DATETIME=NULL,
@EFFECTIVE_DATE DATETIME=NULL,
@FINAL_DATE DATETIME=NULL,
@LEVEL INT
)
AS
BEGIN
SELECT  @DISCOUNT_ID=isnull(Max(DISCOUNT_ID),0)+1 from MNT_DISCOUNT_SURCHARGE

INSERT INTO MNT_DISCOUNT_SURCHARGE
(
 DISCOUNT_ID,
 TYPE_ID,
 LOB_ID,
 SUBLOB_ID,
 DISCOUNT_TYPE,
 DISCOUNT_DESCRIPTION,
 PERCENTAGE,
 IS_ACTIVE,
 CREATED_BY,
 CREATED_DATETIME,
 MODIFIED_BY,
 LAST_UPDATED_DATETIME,
 EFFECTIVE_DATE,
 FINAL_DATE,
 LEVEL
 )
 VALUES
 (
 @DISCOUNT_ID,
 @TYPE_ID ,
 @LOB_ID ,
 @SUBLOB_ID ,
 @DISCOUNT_TYPE ,
 @DISCOUNT_DESCRIPTION ,
 @PERCENTAGE ,
 @IS_ACTIVE ,
 @CREATED_BY ,
 @CREATED_DATETIME,
 @MODIFIED_BY ,
 @LAST_UPDATED_DATETIME ,
 @EFFECTIVE_DATE ,
 @FINAL_DATE ,
 @LEVEL
 )
 
END
GO

