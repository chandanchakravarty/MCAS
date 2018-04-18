IF  EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('INSERT_MNT_ACCUMULATION_REFERENCE') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[INSERT_MNT_ACCUMULATION_REFERENCE]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.INSERT_MNT_ACCUMULATION_REFERENCE
--Created by         : Kuldeep Saxena         
--Date               :  24 OCTOBER 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[INSERT_MNT_FUND_TYPES]      
CREATE  PROCEDURE [dbo].[INSERT_MNT_ACCUMULATION_REFERENCE]      
( 
	@ACC_ID int output,
	@ACC_REF_NO nvarchar(10),
	@LOB_ID int,
	@CRITERIA_ID int ,
	@CRITERIA_VALUE nvarchar(40),
	@TREATY_CAPACITY_LIMIT decimal(18, 2),
	@USED_LIMIT decimal(18, 2),
	@EFFECTIVE_DATE datetime ,
	@EXPIRATION_DATE datetime ,
	@IS_ACTIVE char(2)

)        
AS       
BEGIN   
        
SELECT @ACC_ID = isnull(Max(ACC_ID),0)+1 FROM MNT_ACCUMULATION_REFERENCE
                
  INSERT INTO MNT_ACCUMULATION_REFERENCE                
  (                 
			[ACC_ID]
           ,[ACC_REF_NO]
           ,[LOB_ID]
           ,[CRITERIA_ID]
           ,[CRITERIA_VALUE]
           ,[TREATY_CAPACITY_LIMIT]
           ,[USED_LIMIT]
           ,[EFFECTIVE_DATE]
           ,[EXPIRATION_DATE]
           ,[IS_ACTIVE]
                 
  )                  
  VALUES                  
  (      
			@ACC_ID
           ,@ACC_REF_NO
           ,@LOB_ID
           ,@CRITERIA_ID
           ,@CRITERIA_VALUE
           ,@TREATY_CAPACITY_LIMIT
           ,@USED_LIMIT
           ,@EFFECTIVE_DATE
           ,@EXPIRATION_DATE
           ,@IS_ACTIVE
    )     
End

--declare @ACC_id int
--exec INSERT_MNT_ACCUMULATION_REFERENCE @ACC_id output,'12',38,2,'test',10000.00,1000.00,'2001-10-01','2001-10-31','Y'
