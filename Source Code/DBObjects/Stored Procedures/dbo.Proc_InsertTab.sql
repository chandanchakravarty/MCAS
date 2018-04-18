IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertTab]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertTab]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------      
Proc Name     : dbo.Proc_InsertTab      
Created by      : Anurag Verma      
Date                  : 26/05/2005      
Purpose         : To insert the data into QUESTIONTABMASTER
Revison History :      
Modified by 		: Manab
Description		: Adding Reptable Controls Column
Used In         	: Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE  PROC Dbo.Proc_InsertTab     
(      
@TABID INT OUTPUT,
@CARRIERID INT,
@SCREENID INT,
@TABNAME NVARCHAR(100),
@REPEATCONTROLS INT,
@ISACTIVE NCHAR(2),
@LASTMODIFIEDBY INT,
@LASTMODIFIEDDATE DATETIME
)      
AS      
BEGIN    
 /*Check for Unique Code of Agency  */    
  Declare @Count numeric    
  DECLARE @CNTSEQNO NUMERIC
  SELECT @Count = ISNULL(MAX(TABID),0)+1,@CNTSEQNO = ISNULL(MAX(SEQNO),0)+1   FROM QUESTIONTABMASTER where  SCREENID=@SCREENID
     

  INSERT INTO QUESTIONTABMASTER
  (     
	TABID,
	CARRIERID ,
	SCREENID,
	TABNAME ,
	REPEATCONTROLS,
	ISACTIVE,
	LASTMODIFIEDBY ,
	LASTMODIFIEDDATE ,
	SEQNO 
  )      
  VALUES      
  (      
	@Count,
	@CARRIERID ,
	@SCREENID,
	@TABNAME ,
	@REPEATCONTROLS,
	@ISACTIVE,
	@LASTMODIFIEDBY ,
	@LASTMODIFIEDDATE ,
	@CNTSEQNO 
 )      
	
SELECT @TABID = ISNULL(Max(TABID),0) FROM QUESTIONTABMASTER
END



GO

