IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_REIN_INSERT_SPECIALACCEPTANCEAMOUNT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MNT_REIN_INSERT_SPECIALACCEPTANCEAMOUNT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------              
Proc Name       : dbo.MNT_REIN_INSERT_SPECIALACCEPTANCEAMOUNT            
Created by      : Harmanjeet              
Date            : 11/04/2007              
Purpose         : To insert the data into table.              
Revison History :              
Used In         : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/    
-- drop PROC [dbo].[MNT_REIN_INSERT_SPECIALACCEPTANCEAMOUNT]             
CREATE PROC [dbo].[MNT_REIN_INSERT_SPECIALACCEPTANCEAMOUNT]              
(    
	@SPECIAL_ACCEPTANCE_LIMIT_ID int OUTPUT,  
	@SPECIAL_ACCEPTANCE_LIMIT     nvarchar(50),
	@EFFECTIVE_DATE DATETIME,
	@LOB_ID SMALLINT,
	@CREATED_BY INT,    
	@CREATED_DATETIME DATETIME  
)  
AS  
BEGIN  
--DELETE FROM MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT  
SELECT @SPECIAL_ACCEPTANCE_LIMIT_ID=IsNull(Max(SPECIAL_ACCEPTANCE_LIMIT_ID),0)+1 FROM  MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT  
  
INSERT INTO MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT              
   (SPECIAL_ACCEPTANCE_LIMIT_ID, SPECIAL_ACCEPTANCE_LIMIT,EFFECTIVE_DATE,LOB_ID,CREATED_BY,CREATED_DATETIME,IS_ACTIVE)              
   VALUES              
   (@SPECIAL_ACCEPTANCE_LIMIT_ID, @SPECIAL_ACCEPTANCE_LIMIT,@EFFECTIVE_DATE,@LOB_ID,@CREATED_BY,@CREATED_DATETIME,'Y'  )             
  
  
END








GO

