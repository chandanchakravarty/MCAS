IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCurrencyRate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCurrencyRate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : Proc_InsertCurrencyRate            
Created by      : praveer panghal  
Date            : 21/12/2010              
Purpose       :To insert records in MNT_CURRENCY_RATE_MASTER table.              
Revison History :              
Used In        : Ebix Advantage              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
--DROP PROC dbo.Proc_InsertCurrencyRate   
--select * from MNT_CURRENCY_RATE_MASTER


CREATE PROC [dbo].[Proc_InsertCurrencyRate]
@CRR_RATE_ID int output,
@CURRENCY_ID INT ,
@RATE DECIMAL(7,4)=NULL,
@RATE_EFFETIVE_FROM DATETIME=NULL,
@RATE_EFFETIVE_TO DATETIME=NULL,
@IS_ACTIVE NCHAR(1)=NULL,  
@CREATED_BY INT=NULL,  
@CREATED_DATETIME DATETIME=NULL,  
@MODIFIED_BY INT=NULL,  
@LAST_UPDATED_DATETIME DATETIME=NULL


 
AS
BEGIN
IF EXISTS(SELECT RATE FROM MNT_CURRENCY_RATE_MASTER with(nolock) WHERE RATE_EFFETIVE_FROM >= 
convert(datetime,@RATE_EFFETIVE_FROM) AND RATE_EFFETIVE_FROM<=convert(datetime,@RATE_EFFETIVE_TO)
 AND CURRENCY_ID=@CURRENCY_ID)                                                                                                                                                                      
    BEGIN  
      SET @CRR_RATE_ID=-2  
      RETURN  
    END 


 SELECT  @CRR_RATE_ID=isnull(Max(CRR_RATE_ID),0)+1 from MNT_CURRENCY_RATE_MASTER WITH(NOLOCK)    
INSERT INTO MNT_CURRENCY_RATE_MASTER
(

CURRENCY_ID,
RATE,
RATE_EFFETIVE_FROM,
RATE_EFFETIVE_TO,
IS_ACTIVE,  
CREATED_BY,  
CREATED_DATETIME,  
MODIFIED_BY,  
LAST_UPDATED_DATETIME

)
VALUES
(

@CURRENCY_ID ,
@RATE ,
@RATE_EFFETIVE_FROM ,
@RATE_EFFETIVE_TO ,
@IS_ACTIVE ,  
@CREATED_BY ,  
@CREATED_DATETIME ,  
@MODIFIED_BY ,  
@LAST_UPDATED_DATETIME 

)
-- SET @CRR_RATE_ID=2  
END

GO

