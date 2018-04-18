IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_WATERCRAFT_COVERAGE_BY_ID_FOR_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_WATERCRAFT_COVERAGE_BY_ID_FOR_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_UPDATE_WATERCRAFT_COVERAGE_BY_ID_FOR_POLICY        
        
/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_UPDATE_WATERCRAFT_COVERAGE_BY_ID_FOR_POLICY                    
Created by      : Pradeep                    
Date            : Apr 10, 2006            
Purpose      : Saves records in Watercraft Coverages and inserts                  
    dependent endorsements in APP_WATERCRAFT_ENDORSEMENTS                    
Revison History :                    
Used In  : Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
CREATE           PROC Dbo.Proc_UPDATE_WATERCRAFT_COVERAGE_BY_ID_FOR_POLICY                    
(                    
 @CUSTOMER_ID     int,                    
 @POLICY_ID     int,                    
 @POLICY_VERSION_ID     smallint,                    
 @BOAT_ID smallint,                    
 @COVERAGE_CODE_ID int,                    
 @LIMIT_1 Decimal(18,2),                          
 @LIMIT_2 Decimal(18,2),                        
 @LIMIT1_AMOUNT_TEXT NVarChar(100),                          
 @LIMIT2_AMOUNT_TEXT NVarChar(100),                      
 @LIMIT_ID Int  = NULL ,        
 @DEDUC_ID Int = NULL ,    
 @Deductible_1 INT =NULL ,  
 @DEDCTIBLE_TEXT NVARCHAR(10)=NULL                 
)                    
AS                    
                    
DECLARE @COVERAGE_ID_MAX smallint                    
	BEGIN                    
	IF  EXISTS            
	(            
		SELECT * FROM POL_WATERCRAFT_COVERAGE_INFO            
		WHERE CUSTOMER_ID = @CUSTOMER_ID and                     
		POLICY_ID=@POLICY_ID and                     
		POLICY_VERSION_ID = @POLICY_VERSION_ID                     
		and BOAT_ID = @BOAT_ID AND            
		COVERAGE_CODE_ID =  @COVERAGE_CODE_ID                    
	)                    
	                 
		BEGIN                    
		 --Update                    
		 UPDATE POL_WATERCRAFT_COVERAGE_INFO                    
		 SET        
			LIMIT_1 = @LIMIT_1,                          
			LIMIT_2 = @LIMIT_2,      
			LIMIT1_AMOUNT_TEXT = @LIMIT1_AMOUNT_TEXT,                        
			LIMIT2_AMOUNT_TEXT = @LIMIT2_AMOUNT_TEXT,               
			LIMIT_ID = @LIMIT_ID,        
			DEDUC_ID = @DEDUC_ID,    
			Deductible_1 =@Deductible_1,  
			DEDUCTIBLE1_AMOUNT_TEXT= @DEDCTIBLE_TEXT              
		 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
			POLICY_ID = @POLICY_ID AND                    
			POLICY_VERSION_ID = @POLICY_VERSION_ID AND                    
			COVERAGE_CODE_ID = @COVERAGE_CODE_ID  AND          
			BOAT_ID = @BOAT_ID                  
		 END      
	      
	                      
	END                  
                  
                
              
            
          
        
      
    
  



GO

