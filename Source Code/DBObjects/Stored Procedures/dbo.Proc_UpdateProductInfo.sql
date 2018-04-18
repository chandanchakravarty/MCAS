IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateProductInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateProductInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /*----------------------------------------------------------                    
PROC NAME       : DBO.MNT_LOB_MASTER                    
CREATED BY      : PRADEEP KUSHWAHA           
DATE            : 07/Sep/2010                    
PURPOSE			: TO UPDATE RECORDS IN MNT_LOB_MASTER TABLE.                    
REVISON HISTORY :                    
USED IN        : EBIX ADVANTAGE                    
------------------------------------------------------------                    
DATE     REVIEW BY          COMMENTS                    
------   ------------       -------------------------*/                    
--DROP PROC DBO.Proc_UpdateProductInfo  5,'Umbrella', 'U','U','U','App',1000001,'R100001','PL',3 ,'0114', 396,'10/17/2010',43,'4656~GFHGF~GFHGFY745'            
          
CREATE PROC [dbo].[Proc_UpdateProductInfo] 
(          
         
@LOB_ID INT,
@LOB_DESC NVARCHAR(70),
@LOB_CATEGORY NVARCHAR(10),
@LOB_TYPE CHAR(1),
@LOB_PREFIX NVARCHAR(5),
@LOB_SUFFIX NVARCHAR(5),
@LOB_SEED INT, 
@REWRITE_SEED NVARCHAR(7),
@COMMISSION_LEVEL CHAR(2),
@SUSEP_LOB_ID INT,
@SUSEP_LOB_CODE NVARCHAR(10),
@MODIFIED_BY INT,
@LAST_UPDATED_DATETIME DATETIME ,
@APPLICABLE_COMMISSION NVARCHAR(100),
@SUSEP_PROCESS_NUMBERS NVARCHAR(1000),
@ADMINISTRATIVE_EXPENSE DECIMAL(10,2)
)          
AS           
        
BEGIN          
  
   UPDATE MNT_LOB_MASTER                                                    
     SET           
        LOB_DESC=@LOB_DESC,
		LOB_CATEGORY=@LOB_CATEGORY,
		LOB_TYPE=@LOB_TYPE,
		LOB_PREFIX=@LOB_PREFIX,
		LOB_SUFFIX=@LOB_SUFFIX,
		LOB_SEED=@LOB_SEED, 
		REWRITE_SEED=@REWRITE_SEED, 
		COMMISSION_LEVEL=@COMMISSION_LEVEL ,
		SUSEP_LOB_ID=@SUSEP_LOB_ID,
		SUSEP_LOB_CODE=@SUSEP_LOB_CODE,
        MODIFIED_BY=@MODIFIED_BY  ,
        LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,
        APPLICABLE_COMMISSION=@APPLICABLE_COMMISSION,
        ADMINISTRATIVE_EXPENSE = @ADMINISTRATIVE_EXPENSE
    WHERE   
     
		LOB_ID=@LOB_ID
		
		DELETE  FROM MNT_PRODUCT_SUSEP_PROCESS_NUMBER WHERE LOB_ID =@LOB_ID
		
		
		
		DECLARE @INDEX INT = 1
		DECLARE @COUNT INT
		DECLARE @PROCESS_NUMBER NVARCHAR(50)
				
		SELECT @COUNT=COUNT(ITEM) FROM DBO.FUNC_SPLIT(@SUSEP_PROCESS_NUMBERS,'~')
		
		WHILE(@COUNT >= @INDEX)
	    BEGIN 
	    DECLARE @PROCESS_ID INT
			SELECT @PROCESS_ID= isnull(MAX(SUSEP_PROCESS_ID),0)+1  FROM MNT_PRODUCT_SUSEP_PROCESS_NUMBER
			
			SELECT @PROCESS_NUMBER =  DBO.PIECE(@SUSEP_PROCESS_NUMBERS,'~',@INDEX)
			INSERT INTO MNT_PRODUCT_SUSEP_PROCESS_NUMBER
			(LOB_ID,SUSEP_PROCESS_ID,SUSEP_PROCESS_NO,IS_ACTIVE,CREATED_BY,CREATED_DATETIME) 
			VALUES
			(@LOB_ID,@PROCESS_ID,@process_number ,'Y',@MODIFIED_BY,GETDATE())
			--(5,1,dbo.Piece('345325~45345~TGHHG','~',2),'Y',398,GETDATE())
			SET @INDEX= @INDEX+1
			
	    END
	    
  END    
  

GO

