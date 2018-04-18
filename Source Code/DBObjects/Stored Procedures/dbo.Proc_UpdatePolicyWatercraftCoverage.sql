IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyWatercraftCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyWatercraftCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_UpdatePolicyWatercraftCoverage
Created by      : Ravindra
Date            : 06-09-2006
Purpose         : Updates records in Watercraft Coverages (Policy Level)
Revison History :   
Modified by      : Pravesh                      
Date            : 31 jULY-2007                      
Purpose      : SavING LINKED eNDORSMENTS                          
Used In  : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/         
--- drop proc Proc_UpdatePolicyWatercraftCoverage
CREATE  proc  Proc_UpdatePolicyWatercraftCoverage
(                  
	@CUSTOMER_ID     int,                  
	@POLICY_ID     int,                  
	@POLICY_VERSION_ID     smallint,                  
	@VEHICLE_ID smallint,                  
	@COVERAGE_CODE VarChar(10),                 
	@LIMIT_1 Decimal(18,2),                        
	@LIMIT1_AMOUNT_TEXT NVarChar(100),                        
	@LIMIT_2 Decimal(18,2),                      
	@LIMIT2_AMOUNT_TEXT NVarChar(100),                        
	@DEDUCTIBLE_1 DECIMAL(18,2),                        
	@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                        
	@DEDUCTIBLE_2 DECIMAL(18,2),                        
	@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100)                        
)                  
AS                  
                  
DECLARE @COVERAGE_ID_MAX smallint 
DECLARE @LIMIT_1_TYPE NVarChar(10)                        
DECLARE @LIMIT_2_TYPE NVarChar(10)   
DECLARE @DEDUCTIBLE_1_TYPE NVarChar(10)                        
DECLARE @DEDUCTIBLE_2_TYPE NVarChar(10)   
DECLARE @COVERAGE_ID int      
DECLARE	@COVERAGE_CODE_ID int   
DECLARE @BOAT_TYPE NVarChar(10)       
      
BEGIN                  
      
	SET @COVERAGE_CODE_ID = 0

	SELECT @COVERAGE_CODE_ID = ISNULL(MNT.COV_ID ,0 )
	FROM MNT_COVERAGE MNT INNER JOIN POL_CUSTOMER_POLICY_LIST POL ON      
  		MNT.LOB_ID = 4 AND	--For watercraft only
  		MNT.STATE_ID=POL.STATE_ID 
	WHERE   POL.CUSTOMER_ID = @CUSTOMER_ID AND      
		POL.POLICY_ID = @POLICY_ID AND      
		POL.POLICY_VERSION_ID = @POLICY_VERSION_ID and     
		MNT.COV_CODE = @COVERAGE_CODE AND       
		MNT.IS_ACTIVE = 'Y'            

      
	IF (  @COVERAGE_CODE_ID = 0 )      
	BEGIN      
		RETURN      
	END


	
	declare @ISLIMITAPPLICABLE int, @ISDEDUCTAPPLICABLE INT, @LIMIT_ID INT, @DEDUCT_ID INT
	DECLARE @LIMIT_TYPE INT, @DEDUCTIBLE_TYPE INT
	
	--Get Range ID-----------------------------------------------------------------  
	SELECT @ISLIMITAPPLICABLE = IsLimitApplicable,  
	@ISDEDUCTAPPLICABLE = IsDeductApplicable,  
	@LIMIT_TYPE = LIMIT_TYPE,  
	@DEDUCTIBLE_TYPE = DEDUCTIBLE_TYPE  
	FROM MNT_COVERAGE  
	WHERE COV_ID = @COVERAGE_CODE_ID  
	
	IF  ( @ISLIMITAPPLICABLE = '1' )  
	BEGIN  
	 --Flat  
	 --IF ( @LIMIT_TYPE = 1 OR @LIMIT_TYPE = 2)  
	 --BEGIN  
	

  		SELECT @LIMIT_ID = LIMIT_DEDUC_ID  
  		FROM MNT_COVERAGE_RANGES  
  		WHERE COV_ID = @COVERAGE_CODE_ID AND  
   		ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@LIMIT_1,0) AND  
   		ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@LIMIT_2,0) AND  
   		ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT_TEXT)),'') = ISNULL(RTRIM(LTRIM(@LIMIT1_AMOUNT_TEXT)),'') AND  
   		ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT1_TEXT)),'') = ISNULL(RTRIM(LTRIM(@LIMIT2_AMOUNT_TEXT)),'')  
 
	END  
  
	IF  ( @ISDEDUCTAPPLICABLE = '1' )  
	BEGIN  
		--by pravesh
		select @BOAT_TYPE=LKP.TYPE from POL_WATERCRAFT_INFO WAT_INFO WITH(NOLOCK)
			INNER JOIN MNT_LOOKUP_VALUES LKP WITH(NOLOCK) ON            
			  WAT_INFO.TYPE_OF_WATERCRAFT = LKP.LOOKUP_UNIQUE_ID    
			and  WAT_INFO.CUSTOMER_ID=@CUSTOMER_ID
			AND WAT_INFO.POLICY_ID=@POLICY_ID
			AND WAT_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID
			and wat_info.BOAT_ID = @VEHICLE_ID  
 		--Flat  
 		IF ( @DEDUCTIBLE_TYPE = 1 OR @DEDUCTIBLE_TYPE = 2)  
 		BEGIN  
		if (@BOAT_TYPE='JS' and @COVERAGE_CODE='BDEDUC' )
			SELECT @DEDUCT_ID = LIMIT_DEDUC_ID    
			  FROM MNT_COVERAGE_RANGES    
			  WHERE COV_ID = @COVERAGE_CODE_ID AND  
			  ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_1,0) AND    
			  ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@DEDUCTIBLE_2,0) AND    
			  ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE1_AMOUNT_TEXT)),'') AND    
			  ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT1_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE2_AMOUNT_TEXT)),'')  
			 and LIMIT_DEDUC_ID in (1433,1434,1435,1436,1437,1438)
		 else if (@BOAT_TYPE!='JS' and @COVERAGE_CODE='BDEDUC' )
			SELECT @DEDUCT_ID = LIMIT_DEDUC_ID    
			  FROM MNT_COVERAGE_RANGES    
			  WHERE COV_ID = @COVERAGE_CODE_ID AND  
			  ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_1,0) AND    
			  ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@DEDUCTIBLE_2,0) AND    
			  ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE1_AMOUNT_TEXT)),'') AND    
			  ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT1_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE2_AMOUNT_TEXT)),'')  
			 and LIMIT_DEDUC_ID not in(1433,1434,1435,1436,1437,1438)
		 --end here			
		else
		SELECT @DEDUCT_ID = LIMIT_DEDUC_ID  
			FROM MNT_COVERAGE_RANGES  
			WHERE COV_ID = @COVERAGE_CODE_ID AND  
			ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_1,0) AND  
			ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@DEDUCTIBLE_2,0) AND  
			ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE1_AMOUNT_TEXT)),'') AND  
			ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT1_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE2_AMOUNT_TEXT)),'')  
 		END  
	END
          
	IF  EXISTS          
	(          
		SELECT COVERAGE_CODE_ID FROM POL_WATERCRAFT_COVERAGE_INFO          
		WHERE CUSTOMER_ID = @CUSTOMER_ID and                   
		POLICY_ID=@POLICY_ID and                   
		POLICY_VERSION_ID = @POLICY_VERSION_ID                   
		and BOAT_ID = @VEHICLE_ID AND          
		COVERAGE_CODE_ID =  @COVERAGE_CODE_ID                  
	)                  
	BEGIN                  
                    
		--Update                  
		UPDATE POL_WATERCRAFT_COVERAGE_INFO                  
		SET                  
		LIMIT_1 = @LIMIT_1,                        
		LIMIT_2 = @LIMIT_2,                        
		DEDUCTIBLE_1_TYPE = @DEDUCTIBLE_1_TYPE,                        
		DEDUCTIBLE_2_TYPE = @DEDUCTIBLE_2_TYPE,                    
		DEDUCTIBLE_1 = @DEDUCTIBLE_1,                        
		DEDUCTIBLE_2 = @DEDUCTIBLE_2,                          
		LIMIT1_AMOUNT_TEXT = @LIMIT1_AMOUNT_TEXT,                      
		LIMIT2_AMOUNT_TEXT = @LIMIT2_AMOUNT_TEXT,                      
		DEDUCTIBLE1_AMOUNT_TEXT = @DEDUCTIBLE1_AMOUNT_TEXT,                      
		DEDUCTIBLE2_AMOUNT_TEXT = @DEDUCTIBLE2_AMOUNT_TEXT,
		LIMIT_ID = @LIMIT_ID,      
		DEDUC_ID = @DEDUCT_ID                      
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND                  
		POLICY_ID = @POLICY_ID AND                  
		POLICY_VERSION_ID = @POLICY_VERSION_ID AND                  
		COVERAGE_CODE_ID = @COVERAGE_CODE_ID  AND        
		BOAT_ID = @VEHICLE_ID    
	   -----UPDATING LINKED ENDORSEMENTS
		 EXEC Proc_UPDATE_WATERCRAFT_LINKED_ENDORSEMENTS_POLICY  @CUSTOMER_ID, @POLICY_ID ,@POLICY_VERSION_ID ,@VEHICLE_ID,0,@COVERAGE_CODE_ID                 
                 
	END
                    
	IF @@ERROR <> 0      
	BEGIN      
 		RAISERROR ('Unable to update Watercraft coverage.', 16, 1)      
      
	END           
	--************************************************************     
  
 	RETURN @COVERAGE_CODE_ID            
              
END








GO

