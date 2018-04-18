IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPDATE_MNT_ACCUMULATION_REFERENCE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPDATE_MNT_ACCUMULATION_REFERENCE]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.UPDATE_MNT_ACCUMULATION_REFERENCE
--Created by         : Kuldeep Saxena         
--Date               : 24 OCTOBER 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/        
-- drop proc dbo.[UPDATE_MNT_ACCUMULATION_REFERENCE]      
CREATE  PROCEDURE [dbo].[UPDATE_MNT_ACCUMULATION_REFERENCE]      
(       
  @ACC_ID int output,
	@ACC_REF_NO nvarchar(10),
	@LOB_ID int,
	@CRITERIA_ID int ,
	@CRITERIA_VALUE nvarchar(40),
	@TREATY_CAPACITY_LIMIT decimal(18, 2),
	@USED_LIMIT decimal(18, 2),
	@EFFECTIVE_DATE datetime ,
	@EXPIRATION_DATE datetime 
)        
AS                
BEGIN      
       
   UPDATE MNT_ACCUMULATION_REFERENCE
	SET        
	ACC_REF_NO=@ACC_REF_NO,
	LOB_ID=@LOB_ID,
	CRITERIA_ID=@CRITERIA_ID,
	CRITERIA_VALUE=@CRITERIA_VALUE,
	TREATY_CAPACITY_LIMIT=@TREATY_CAPACITY_LIMIT,
	USED_LIMIT=@USED_LIMIT,
	EFFECTIVE_DATE=@EFFECTIVE_DATE,
	EXPIRATION_DATE=@EXPIRATION_DATE
	
WHERE  ACC_ID=@ACC_ID          
    
 END
