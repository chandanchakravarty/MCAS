IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateDiscountSurcharge]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateDiscountSurcharge]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name       : dbo.Proc_UpdateDiscountSurcharge              
Created by      : Chetna Agarwal    
Date            : 12/04/2010              
Purpose   :To Update records in MNT_DISCOUNT_SURCHARGE table.              
Revison History :              
Used In   : Ebix Advantage              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
--DROP PROC dbo.Proc_UpdateDiscountSurcharge   
  
CREATE PROC [dbo].[Proc_UpdateDiscountSurcharge]    
(    
@DISCOUNT_ID int,  
@TYPE_ID int,  
@LOB_ID int=NULL,  
@SUBLOB_ID int=NULL,  
@DISCOUNT_TYPE INT=NULL,  
@DISCOUNT_DESCRIPTION VARCHAR(40)=NULL,  
@PERCENTAGE DECIMAL(12,4)=NULL,  
@MODIFIED_BY INT=NULL,  
@LAST_UPDATED_DATETIME DATETIME=NULL,  
@EFFECTIVE_DATE DATETIME=NULL,  
@FINAL_DATE DATETIME=NULL,
@LEVEL INT  
)  
AS  
BEGIN  
  
UPDATE MNT_DISCOUNT_SURCHARGE  
SET  
 [TYPE_ID]=@TYPE_ID,  
 LOB_ID=@LOB_ID,  
 SUBLOB_ID=@SUBLOB_ID,  
 DISCOUNT_TYPE=@DISCOUNT_TYPE,  
 DISCOUNT_DESCRIPTION=@DISCOUNT_DESCRIPTION,  
 PERCENTAGE=@PERCENTAGE,  
 MODIFIED_BY=@MODIFIED_BY,  
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,  
 EFFECTIVE_DATE=@EFFECTIVE_DATE,  
 FINAL_DATE=@FINAL_DATE, 
 LEVEL=@LEVEL 
    
 WHERE  
 DISCOUNT_ID=@DISCOUNT_ID  
   
END
GO

