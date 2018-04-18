IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDiscountSurchargeData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDiscountSurchargeData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

/*----------------------------------------------------------                          
Proc Name      : dbo.[Proc_GetDiscountSurchargeData]                          
Created by       : Chetna Agarwal                        
Date             : 13-04-2010                          
Purpose       : retrieving data from MNT_DISCOUNT_SURCHARGE                          
Revison History :                 
Modify by       :                          
Date             :                          
Purpose       :         
                       
Used In        : Ebix Advantage                      
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
--drop proc dbo.[Proc_GetDiscountSurchargeData]

CREATE PROCEDURE [dbo].[Proc_GetDiscountSurchargeData] 
@DISCOUNT_ID INT
AS
BEGIN 
SELECT
DISCOUNT_ID,
[TYPE_ID],
 LOB_ID,
 SUBLOB_ID,
 DISCOUNT_TYPE,
 DISCOUNT_DESCRIPTION,
 isnull(PERCENTAGE,0)as PERCENTAGE,
 IS_ACTIVE,
 CREATED_BY,
 CREATED_DATETIME,
 MODIFIED_BY,
 LAST_UPDATED_DATETIME,
 EFFECTIVE_DATE,
 FINAL_DATE,
 LEVEL
 FROM MNT_DISCOUNT_SURCHARGE WITH(NOLOCK) 
 WHERE DISCOUNT_ID=@DISCOUNT_ID
END     

GO

