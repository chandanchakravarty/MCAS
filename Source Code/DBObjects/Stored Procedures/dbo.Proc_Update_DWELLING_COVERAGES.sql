IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_DWELLING_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_DWELLING_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Update_DWELLING_COVERAGES                  
                  
/*----------------------------------------------------------                                                              
Proc Name   : dbo.Proc_Update_DWELLING_COVERAGES                                                             
Created by  : Pradeep                                                              
Date        : 12/29/2005                                                            
Purpose     :  Adds/deletes relevant coverages                                      
Revison History  :                              
Modified Date : 03 jan 2006                                        
Modified By : Praveen kasana                                        
Purpose : Add Coverage for Rental Dwelling (SD)                                                                      
------------------------------------------------------------                                                                          
Date     Review By          Comments                                                                        
-----------------------------------------------------------*/                                                        
CREATE   PROCEDURE Proc_Update_DWELLING_COVERAGES                                                      
(                                                       
 @CUSTOMER_ID int,                                                      
 @APP_ID int,                                                      
 @APP_VERSION_ID smallint,                                                       
 @DWELLING_ID smallint                                                           
)                                                      
                                                      
As                                                    
                                          
                                          
BEGIN                                          
                                        
DECLARE @COV_ID Int                                          
DECLARE @STATE_ID Int                                        
DECLARE @LOB_ID Int                                        
DECLARE @POLICY_TYPE Int                                       
DECLARE @MONEY Int                  
DECLARE @SILVER Int                  
DECLARE @FIREARMS Int              
DECLARE @JEWELRY Int                  
      
SELECT  @STATE_ID = STATE_ID ,                                        
@LOB_ID = APP_LOB  ,                                      
@POLICY_TYPE = POLICY_TYPE                                      
FROM APP_LIST                                        
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                        
APP_ID = @APP_ID AND                                        
APP_VERSION_ID = @APP_VERSION_ID  
IF ( @LOB_ID = 1)                                  
BEGIN     
-- FOR Repair cost PROGRAMMS "Repair Cost Homeowners (HO-289)" coverage (RECST)
IF @POLICY_TYPE IN (11193,11194,11403,11404)
BEGIN
	--Repair Cost Homeowners (HO-289)" coverage  --RECST                              
	EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                  
	@APP_ID,                                                  
	@APP_VERSION_ID,                                          
	'RECST'                                                
	
	IF ( @COV_ID = 0 )                                              
	BEGIN                  
		RAISERROR ('Coverage ID not found for Repair Cost Homeowners (HO-289)',16, 1)                                        
		--RETURN                                       
	END     
	ELSE                                      
	BEGIN                                      
	EXEC Proc_SAVE_DWELLING_COVERAGES                                       
	@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
	@APP_ID, --@APP_ID     int,                                                            
	@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
	@DWELLING_ID, --@DWELLING_ID smallint,                                                            
	-1,  --@COVERAGE_ID int,                                                       
	@COV_ID, --@COVERAGE_CODE_ID int,                                                            
	NULL,  --@LIMIT_1 Decimal(18,2),                                                            
	NULL,  --@LIMIT_2 Decimal(18,2),                                    
	NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
	NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
	END
END          

END                                       

                                  
                                  
---BEGIN OF Michigan HOME ****************************************************************                            
IF (  @STATE_ID =  22 )                          
BEGIN                          
	
	IF ( @LOB_ID = 1)                                  
		BEGIN                                  
		
		--Insert mandatory for all Products----------------------------------------------                                    
		--Fire Department Service Charge (HO-96) --EBIF96                                    
		--Personal Property Away From Premises --EBPPOP                                    
		--Business Property Increase Limits (HO-312) doubt -- IBUSP                                    
		--Credit Card and Depositors Forgery (HO-53) --                                     
		
		--end of mandatory-----------------------------------------------------------------------------                                                                           
		--Increased Fire Dept. Service Charge (HO-96)     
		--EBIF96                                      
		EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                           
		@APP_ID,                                                  
		@APP_VERSION_ID,                                                  
		'EBIF96'                                                
		
		IF ( @COV_ID = 0 )                                              
		BEGIN                                             
		print(1)                                            
		RAISERROR ('Coverage ID not found for Increased Fire Dept. Service Charge (HO-96)',16, 1)                          
		--RETURN                                       
		END                                      
		ELSE                                      
		BEGIN                                      
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		@COV_ID, --@COVERAGE_CODE_ID int,                                                            
		500,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		END                                      
		
		--Credit Card and Depositors Forgery (HO-53)                                      
		--EBICC53                                      
		EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                  
		@APP_ID,                                       
		@APP_VERSION_ID,                                                  
		'EBICC53'                                                
		
		IF ( @COV_ID = 0 )                                              
		BEGIN                                          
		print(1)                                                    
		RAISERROR ('Coverage ID not found for Credit Card and Depositors Forgery (HO-53)',16, 1)                                        
		--RETURN                                       
		END                                      
		ELSE                                      
		BEGIN                                      
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                    
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		@COV_ID, --@COVERAGE_CODE_ID int,                                                            
		500,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                         
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		END                                      
		
		--Personal Property Away From Premises --EBPPOP                              
		EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                  
		@APP_ID,                                                  
		@APP_VERSION_ID,                                          
		'EBPPOP'                                                
		
		IF ( @COV_ID = 0 )                                              
		BEGIN                  
		print(1)                                                    
		RAISERROR ('Coverage ID not found for Personal Property Away From Premises',16, 1)                                        
		--RETURN                                       
		END                 
		ELSE                                      
		BEGIN                                      
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                       
		@COV_ID, --@COVERAGE_CODE_ID int,                                                            
		NULL,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                    
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		END                                      
		
		--Business Property Increased Limits (HO-312)                                      
		EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                            
		@APP_ID,                                                  
		@APP_VERSION_ID,                                                  
		'IBUSP'                                                 
		
		IF ( @COV_ID = 0 )                                              
		BEGIN                        
		print(1)                                           
		RAISERROR ('Coverage ID not found for Personal Property Away From Premises (HO-312).',16, 1)                                        
		--RETURN                                       
		END                                      
		ELSE                                      
		BEGIN                                      
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		@COV_ID, --@COVERAGE_CODE_ID int,                      
		2500,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                      
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		END                                    
		
		--LAC -- HO 6 and HO 6 Deluxe - 1000 , else n/a                                    
		------------------------------                                    
		
		--HO-5 Premier---------------------                    
		
		IF ( @POLICY_TYPE = 11410 )                                    
		BEGIN                                    
		
		--196 EBP25 Premier V.I.P. (HO-25)                         
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                           
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		196, --@COVERAGE_CODE_ID int,                                  
		NULL,  --@LIMIT_1 Decimal(18,2),                                     
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		IF ( @@ERROR <> 0 )                    
		
		BEGIN                                  
		
		RAISERROR ('Unable to save Premier V.I.P. (HO-25).',16, 1)                                        
		RETURN                                       
		END                
		
		--56  EBEP11 Expanded Replacement Coverage A + B (HO-11)                
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		56, --@COVERAGE_CODE_ID int,                                                            
		NULL,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                
		
		IF ( @@ERROR <> 0 )                    
		
		BEGIN                                         
		
		RAISERROR ('Unable to save Expanded Replacement Coverage A + B (HO-11).',16, 1)                                        
		RETURN                                       
		END                
		
		--33  EBRCPP Replacement Cost Personal Property (HO-34)                
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                   
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		33, --@COVERAGE_CODE_ID int,                                                            
		NULL,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                    
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                         
		
		
		
		IF ( @@ERROR <> 0 )                                              
		BEGIN                                         
		--print(1)                                    
		RAISERROR ('Unable to save Replacement Cost Personal Property (HO-34).',16, 1)                                        
		RETURN                                       
		END                                         
		
		--Coverage C Increased Special Limits (HO-65 or HO-211)- Unscheduled Jewelry & Furs EBCCSL 74                                   
		
		EXEC Proc_SAVE_DWELLING_COVERAGES                                      
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                   
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		74, --@COVERAGE_CODE_ID int,                                   
		10000,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		IF ( @@ERROR <> 0 )                    
		
		BEGIN                                         
		
		RAISERROR ('Unable to save Coverage C Increased Special Limits (HO-65 or HO-211)- Unscheduled Jewelry & Furs.',16, 1)                                        
		RETURN                                            
		END                              
		
		END                                          
		----------End of   HO-5 Premier-----------------------                    
		
		--HO-3 Premier-----------------------------------------------------              
		IF ( @POLICY_TYPE = 11409)                                    
		BEGIN              
		--Coverage C Increased Special Limits (HO-65 or HO-211)- Unscheduled Jewelry & Furs EBCCSL 74                                   
		
		EXEC Proc_SAVE_DWELLING_COVERAGES                                      
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		74, --@COVERAGE_CODE_ID int,                                                            
		1000,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),              
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		IF ( @@ERROR <> 0 )                    
		
		BEGIN                                         
		
		RAISERROR ('Unable to save Coverage C Increased Special Limits (HO-65 or HO-211)- Unscheduled Jewelry & Furs.',16, 1)                                        
		RETURN           
		END                                                 
		END              
		--End of HO-3 Premier---------------------------------------------------              
		
		--Premier cases                                    
		IF ( @POLICY_TYPE = 11409 OR @POLICY_TYPE = 11410 )                                    
		BEGIN                                    
		
		--Coverage C Increased Special Limits (HO-65 or HO-211)- Money EBCCSM  189                                  
		
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                   
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		189, --@COVERAGE_CODE_ID int,                                  
		1000,  --@LIMIT_1 Decimal(18,2),                                                 
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		IF ( @@ERROR <> 0 )                    
		
		BEGIN                                         
		
		RAISERROR ('Unable to save Coverage C Increased Special Limits (HO-65 or HO-211)- Money.',16, 1)                        
		RETURN                                       
		END                                              
		
		--Coverage C Increased Special Limits (HO-65 or HO-211)- Securities ESCCSS  191                   
		
		EXEC Proc_SAVE_DWELLING_COVERAGES                          
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                               
		-1,  --@COVERAGE_ID int,                                                            
		191, --@COVERAGE_CODE_ID int,                                                            
		1000,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		IF ( @@ERROR <> 0 )                    
		
		BEGIN                                         
		
		RAISERROR ('Unable to save Coverage C Increased Special Limits (HO-65 or HO-211)- Securities.',16, 1)                                        
		RETURN                   
		END                                                    
		
		--Coverage C Increased Special Limits (HO-65 or HO-211)- Silverware EBCCSI    193                                
		
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		193, --@COVERAGE_CODE_ID int,                                                            
		10000,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                   
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		IF ( @@ERROR <> 0 )                  
		
		BEGIN                                         
		
		RAISERROR ('Unable to save Coverage C Increased Special Limits (HO-65 or HO-211)- Silverware.',16, 1)                                        
		RETURN                                       
		END                                                           
		
		--Coverage C Increased Special Limits (HO-65 or HO-211)- Firearms EBCCSF   195                                 
		
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                  
		@APP_ID, --@APP_ID     int,                                                    
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		195, --@COVERAGE_CODE_ID int,                                                 
		2500,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                             
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                         
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		IF ( @@ERROR <> 0 )                    
		
		BEGIN                                         
		
		RAISERROR ('Unable to save Coverage C Increased Special Limits (HO-65 or HO-211)- Firearms.',16, 1)                                        
		RETURN                                       
		END                                                  
		
		
		
		END                                    
		ELSE --Other than Premier                                    
		BEGIN                                    
		-- Coverage C Increased Special Limits (HO-65 or HO-211)- Money EBCCSM  189                                  
		
		--For HO-5 , Money = 1000, Silver = 10000, Firearms = 2500                  
		IF ( @POLICY_TYPE = 11401 )                   
		BEGIN                  
		SET @MONEY = 1000                  
		SET @SILVER = 10000                  
		SET @FIREARMS = 2500                  
		END                  
		ELSE                  
		BEGIN                  
		SET @MONEY = 200                  
		SET @SILVER = 2500                  
		SET @FIREARMS = 2000                  
		END                     
		
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                             
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                    
		-1,  --@COVERAGE_ID int,                                                            
		189, --@COVERAGE_CODE_ID int,                                                            
		@MONEY,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		IF ( @@ERROR <> 0 )                    
		
		BEGIN                                         
		
		RAISERROR ('Unable to save Coverage C Increased Special Limits (HO-65 or HO-211)- Money.',16, 1)                                        
		RETURN                                            
		END                 
		
		--Coverage C Increased Special Limits (HO-65 or HO-211)- Securities ESCCSS  191                                  
		EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                  
		@APP_ID,                                      
		@APP_VERSION_ID,                                                  
		'ESCCSS'                                                 
		
		
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                      
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		191, --@COVERAGE_CODE_ID int,                                                            
		1000,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		IF ( @@ERROR <> 0 )                    
		
		BEGIN                                  
		
		RAISERROR ('Unable to save Coverage C Increased Special Limits (HO-65 or HO-211)- Securities.',16, 1)                                        
		RETURN                                            
		END                                  
		
		
		
		--Coverage C Increased Special Limits (HO-65 or HO-211)- Silverware EBCCSI  193                                  
		
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                      
		@APP_ID, --@APP_ID     int,   
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                               
		-1,  --@COVERAGE_ID int,                                                            
		193, --@COVERAGE_CODE_ID int,                                                            
		@SILVER,  --@LIMIT_1 Decimal(18,2),                                                    
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)             
		
		IF ( @@ERROR <> 0 )                    
		
		BEGIN                                         
		
		RAISERROR ('Unable to save Coverage C Increased Special Limits (HO-65 or HO-211)- Securities.',16, 1)                                        
		RETURN                                            
		END                                                  
		
		--Coverage C Increased Special Limits (HO-65 or HO-211)- Firearms EBCCSF   195                                 
		
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID    int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                      
		@DWELLING_ID, --@DWELLING_ID smallint,                                       
		-1,  --@COVERAGE_ID int,                                                            
		195, --@COVERAGE_CODE_ID int,                                                            
		@FIREARMS,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                 
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		IF ( @@ERROR <> 0 )                    
		
		BEGIN                                         
		
		RAISERROR ('Unable to save Coverage C Increased Special Limits (HO-65 or HO-211)- Firearms.',16, 1)                                        
		RETURN                                            
		END                                                  
		
		--Coverage C Increased Special Limits (HO-65 or HO-211)- Unscheduled Jewelry & Furs EBCCSL   74                                 
		
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                 
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		74, --@COVERAGE_CODE_ID int,                                     
		1000,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                            
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		IF ( @@ERROR <> 0 )                    
		
		BEGIN                                         
		
		RAISERROR ('Unable to save Coverage C Increased Special Limits (HO-65 or HO-211)- Unscheduled Jewelry & Furs.',16, 1)                                        
		RETURN                                            
		END                                                       
		
		END                                    
		-----------------                                 
		
		---HO-6 Deluxe-----------------------------------------------------                                
		IF ( @POLICY_TYPE = 11408 )     
		BEGIN                                    
		--Unit Owners Coverage A Special Coverage (HO-32)                                
		EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                  
		@APP_ID,                                                  
		@APP_VERSION_ID,                                                  
		'EBCASP'                                                 
		
		IF ( @COV_ID = 0 )                                              
		BEGIN                                         
		print(1)                                           
		--RAISERROR ('Coverage ID not found for Business Property Increased Limits (HO-312).',16, 1)                                        
		--RETURN                                       
		END                                      
		ELSE                                      
		BEGIN                                      
		EXEC Proc_SAVE_DWELLING_COVERAGES              
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                  
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		@COV_ID, --@COVERAGE_CODE_ID int,                                                            
		NULL,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		END                          
		END                                
		--End of HO-6 Deluxe----------------------------------------------                                
		
		
		-------------HO-6 and HO-6 Deluxe--------------------                                    
		--LAC Loss Assessment Coverage (HO-35)    89                                
		IF ( @POLICY_TYPE = 11406 OR @POLICY_TYPE = 11408 )                                    
		BEGIN                                    
		
		
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                     
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                              
		89, --@COVERAGE_CODE_ID int,                                                            
		1000,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                           
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		IF (@@ERROR <> 0 )                                   
		BEGIN                                         
		
		RAISERROR ('Unable to save Loss Assessment Coverage (HO-35)  .',16, 1)                                        
		RETURN                    
		END                      
		
		--93 Condominium Deluxe Coverage (HO-66)                    
		EXEC Proc_SAVE_DWELLING_COVERAGES                                   
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		93, --@COVERAGE_CODE_ID int,                                                            
		NULL,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		IF (@@ERROR <> 0 )                                   
		BEGIN                                         
		
		RAISERROR ('Unable to save Condominium Deluxe Coverage (HO-66).',16, 1)                                        
		RETURN                    
		END                     
		
		END                                    
		
		
		-----------------End of HO-6 and HO-6 Deluxe-------------------------------------------                                    
		
		--HO-4 and HO-4 Deluxe-----------------------------------------------------------------                    
		IF ( @POLICY_TYPE = 11405 OR @POLICY_TYPE = 11407 )                       
		BEGIN                    
		
		--92 Renters Deluxe Coverage (HO-64)          
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                      
		-1,  --@COVERAGE_ID int,                                 
		92, --@COVERAGE_CODE_ID int,                                                            
		NULL,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                           
		
		IF (@@ERROR <> 0 )                                   
		BEGIN                                         
		
		RAISERROR ('Unable to save Renters Deluxe Coverage (HO-64).',16, 1)                                        
		RETURN                    
		END                         
		END                     
		-----------------End of HO-4 and HO-4 Deluxe-------------------------------------------                                    
		
		--HO-5--------------------------------------------------------------------------------------                                    
		IF ( @POLICY_TYPE = 11401  )                      
		BEGIN        
		
		--33  EBRCPP Replacement Cost Personal Property (HO-34)       
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                    
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		33, --@COVERAGE_CODE_ID int,                                                            
		NULL,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                   
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                    
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                         
		
		IF ( @@ERROR <> 0 )                                              
		BEGIN                                     
		--print(1)                                    
		RAISERROR ('Unable to save Replacement Cost Personal Property (HO-34).',16, 1)                                        
		RETURN                                       
		END                  
		
		--EBP23 Preferred Plus V.I.P. Coverage (HO-23)                                    
		EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                  
		@APP_ID,                                                  
		@APP_VERSION_ID,                                                  
		'EBP23'                                                
		
		IF ( @COV_ID = 0 )                                              
		BEGIN                                             
		print(1)                                              
		--RAISERROR ('Coverage ID not found for Increased Fire Dept. Service Charge (HO-96)',16, 1)                                        
		--RETURN                                  
		END                                      
		ELSE                                      
		BEGIN                                      
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                    
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                        
		@COV_ID, --@COVERAGE_CODE_ID int,                                                            
		NULL,  --@LIMIT_1 Decimal(18,2),                   
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                 
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		END                                      
		
		--Expanded Replacement (HO-11) - EBEP11                                 
		EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                  
		@APP_ID,                                          
		@APP_VERSION_ID,                                                  
		'EBEP11'                                    
		
		IF ( @COV_ID = 0 )                                              
		BEGIN                                             
		print(1)                                              
		--RAISERROR ('Coverage ID not found for Increased Fire Dept. Service Charge (HO-96)',16, 1)                                        
		--RETURN                                       
		END                                      
		ELSE                                      
		BEGIN                               
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                            
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		@COV_ID, --@COVERAGE_CODE_ID int,                                                            
		NULL,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		END                                      
		
		--Coverage C Increased Special Limits (HO-65 or HO-211)- Unscheduled Jewelry & Furs                                   
		--EBCCSL                                     
		EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                  
		@APP_ID,                                                  
		@APP_VERSION_ID,                                                  
		'EBCCSL'                                
		
		IF ( @COV_ID = 0 )                                              
		BEGIN                                          
		print(1)                                                  
		--RAISERROR ('Coverage ID not found for Coverage C Increased Special Limits (HO-65 or HO-211)- Silverware',16, 1)                                        
		--RETURN                                       
		END                                      
		ELSE                                      
		BEGIN                                      
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                   
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		@COV_ID, --@COVERAGE_CODE_ID int,          
		10000,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
		
		IF ( @@ERROR <> 0  )                    
		BEGIN                    
		RAISERROR ('Could not save Coverage C Increased Special Limits (HO-65 or HO-211)- Unscheduled Jewelry & Furs',16, 1)                                        
		RETURN                         
		END                    
		
		END                                      
		
		/*                    
		--Personal Injury (HO-82)                    
		--275  PERIJ Personal Injury (HO-82) 22                    
		EXEC Proc_SAVE_DWELLING_COVERAGES                                       
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
		@APP_ID, --@APP_ID     int,                                                   
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
		@DWELLING_ID, --@DWELLING_ID smallint,                                                            
		-1,  --@COVERAGE_ID int,                                                            
		275, --@COVERAGE_CODE_ID int,                                                            
		NULL,  --@LIMIT_1 Decimal(18,2),                                                            
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)            
		--'S2' --COVERAGE_TYPE commented for michigan                  
		
		IF ( @@ERROR <> 0  )                    
		BEGIN                    
		RAISERROR ('Could not save Personal Injury (HO-82)',16, 1)                                        
		RETURN                         
		END                    
		*/        
		
		END                                    
		
		
		--End of HO-5-------------------------------------------------------------------------------                                    
	END                                   
  	--END OF HOME OWNERS------------------------                                  
                                   
                               
	--For RENTAL                                   
	IF( @LOB_ID = 6)                          
	BEGIN                                  
		---Satellite Dishes (SD)                                   
		EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                       
		@APP_ID,                                                  
		@APP_VERSION_ID,                                                  
		'SD'                                                 
		
		IF ( @COV_ID = 0 )                                          
		BEGIN                                          
			print(1)                                           
			--RAISERROR ('Coverage ID not found for Business Property Increased Limits (HO-312).',16, 1)                                        
			--RETURN                                       
		END                 
		ELSE                                      
			BEGIN                                      
			EXEC Proc_SAVE_DWELLING_COVERAGES                                              
			@CUSTOMER_ID, --@CUSTOMER_ID                                         
			@APP_ID, --@APP_ID                                              
			@APP_VERSION_ID,--@APP_VERSION_ID                                              
			@DWELLING_ID, --@DWELLING_ID                                              
			-1,  --@COVERAGE_ID                                              
			@COV_ID,  --@COVERAGE_CODE_ID                            
			500, --@LIMIT_1                                              
			null,  --@LIMIT_2                                              
			null,  --@DEDUCTIBLE_1                                              
			null  --@DEDUCTIBLE_2                                              
		END                                    
	
	END                                    
	------End of rental                                  
 END                          
 --END OF MICHIGAN*************************************************************                                     
                           
 --**********BEGINNING OF INDIANA*************************************                          
IF ( @STATE_ID = 14 )                          
	IF( @LOB_ID = 6)                                  
	BEGIN                      
		
		---Satellite Dishes (SD)                                   
		EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                  
		@APP_ID,                                                  
		@APP_VERSION_ID,                             
		'SD'                                                 
		
	IF ( @COV_ID = 0 )                                              
	BEGIN                                         
		print(1)                                           
		--RAISERROR ('Coverage ID not found for Business Property Increased Limits (HO-312).',16, 1)                                        
		--RETURN                                       
	END                                      
	ELSE                                      
		BEGIN                                      
		EXEC Proc_SAVE_DWELLING_COVERAGES                                              
		@CUSTOMER_ID, --@CUSTOMER_ID                                         
		@APP_ID, --@APP_ID                                              
		@APP_VERSION_ID,--@APP_VERSION_ID                                              
		@DWELLING_ID, --@DWELLING_ID                                              
		-1,  --@COVERAGE_ID                                              
		@COV_ID,  --@COVERAGE_CODE_ID                    
		500, --@LIMIT_1                                              
		null,  --@LIMIT_2                                              
		null,  --@DEDUCTIBLE_1                                              
		null  --@DEDUCTIBLE_2                                              
		
	END                                    
	
	END                                    
	------End of rental                                  
        -- Indiana Home                  
	IF ( @LOB_ID = 1 )                          
	BEGIN                                   
		--Insert mandatory for all Products----------------------------------------------                                    
		--Fire Department Service Charge (HO-96) --EBIF96                                    
		--Personal Property Away From Premises --EBPPOP                                    
		--Business Property Increase Limits (HO-312) doubt -- IBUSP                                    
		--Credit Card and Depositors Forgery (HO-53) --                                     
		--Coverage C Increased Special Limits (HO-65 or HO-211)- Money EBCCSM                       
		--Coverage C Increased Special Limits (HO-65 or HO-211)- Securities ESCCSS                    
		--end of mandatory-----------------------------------------------------------------------------                                    
		
		--**********                      
		-- Coverage C Increased Special Limits (HO-65 or HO-211)- Money EBCCSM  188                                  
		
		
		
		--For HO-5 , Money = 1000, Silver = 10000 , Jewelry - 10000                 
		IF ( @POLICY_TYPE = 11149 )                   
		BEGIN                  
			SET @MONEY = 1000                 
			SET @SILVER = 10000                  
			SET @FIREARMS = 2500               
			SET @JEWELRY = 10000              
			
			--197  WBSPO Water Backup and Sump Pump Overflow (HO-327) 14    
			
			IF NOT EXISTS  
			(  
			SELECT * FROM APP_DWELLING_SECTION_COVERAGES  
			WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
			APP_ID = @APP_ID AND  
			APP_VERSION_ID = @APP_VERSION_ID AND  
			DWELLING_ID = @DWELLING_ID AND  
			COVERAGE_CODE_ID = 197   
			)     
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                             
			@APP_ID, --@APP_ID     int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                      
			-1,--@COVERAGE_ID int,                                                            
			197, --@COVERAGE_CODE_ID int,                 
			1000,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
			NULL,  --@DEDUCTIBLE_2 DECIMAL(18,2),   
			'S1',   
			338,    
			NULL           
			
			END                  
			ELSE                  
			BEGIN                  
			SET @MONEY = 200                  
			SET @SILVER = 2500                  
			SET @FIREARMS = 2000                  
			SET @JEWELRY = 1000              
			END                  
			
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,      
			@APP_ID, --@APP_ID     int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                      
			-1,--@COVERAGE_ID int,                                                            
			188, --@COVERAGE_CODE_ID int,                 
			@MONEY,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
			
			IF (@@ERROR <> 0 )                                   
			BEGIN                      
			RAISERROR ('Could not save Coverage C Increased Special Limits (HO-65 or HO-211)- Money EBCCSM.',16, 1)                                        
			RETURN                             
			END                       
			
			
			--++++++++++++++                      
			--Coverage C Increased Special Limits (HO-65 or HO-211)- Securities ESCCSS  190                                  
			
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID     int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                                          
			190, --@COVERAGE_CODE_ID int,                                                            
			1000,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
			
			IF (@@ERROR <> 0 )                                   
			BEGIN                      
			RAISERROR ('Could not save Coverage C Increased Special Limits (HO-65 or HO-211)- Securities ESCCSS',16, 1)                                        
			RETURN                             
			END                      
			
			
			
			--+++                      
			--Coverage C Increased Special Limits (HO-65 or HO-211)- Silverware EBCCSI 192                                   
			
			EXEC Proc_SAVE_DWELLING_COVERAGES                           
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID     int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                                                            
			192, --@COVERAGE_CODE_ID int,                                                            
			@SILVER,  --@LIMIT_1 Decimal(18,2),                                                    
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                              
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
			
			IF (@@ERROR <> 0 )                                   
			BEGIN                      
			RAISERROR ('Could not save Coverage C Increased Special Limits (HO-65 or HO-211)- Silverware EBCCSI',16, 1)                                        
			RETURN                             
			END                      
			
			
			
			--+++++++++                      
			--Coverage C Increased Special Limits (HO-65 or HO-211)- Firearms EBCCSF 194                                  
			
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID    int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                                                            
			194, --@COVERAGE_CODE_ID int,                                                            
			@FIREARMS,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                       
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
			
			IF (@@ERROR <> 0 )                                   
			BEGIN                      
			RAISERROR ('Could not save Coverage C Increased Special Limits (HO-65 or HO-211)- Firearms EBCCSF ',16, 1)                             
			RETURN                             
			END                      
			
			--+++++++++++                      
			--Coverage C Increased Special Limits (HO-65 or HO-211)- Unscheduled Jewelry & Furs EBCCSL 154                                    
			
			
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID     int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                                                            
			154, --@COVERAGE_CODE_ID int,                                                            
			1000,  --@LIMIT_1 Decimal(18,2),                                                          
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
			
			IF (@@ERROR <> 0 )                                   
			BEGIN                      
			RAISERROR ('Could not save Coverage C Increased Special Limits (HO-65 or HO-211)- Unscheduled Jewelry & Furs EBCCSL',16, 1)                                        
			RETURN                  
			END                                   
			
			--*********                       
			
			--Increased Fire Dept. Service Charge (HO-96)                                      
			--EBIF96                                      
			EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                  
			@APP_ID,                                                  
			@APP_VERSION_ID,                                                  
			'EBIF96'                                                
			
			IF ( @COV_ID = 0 )                                              
			BEGIN                                             
			print(1)                                              
			RAISERROR ('Coverage ID not found for Increased Fire Dept. Service Charge (HO-96)',16, 1)                                        
			--RETURN                                       
			END                                      
			ELSE                                      
			BEGIN                                      
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID     int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID   smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                                                            
			@COV_ID, --@COVERAGE_CODE_ID int,                                                            
			500,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
			
			END                                      
			
			--Credit Card and Depositors Forgery (HO-53)                                      
			--EBICC53                                      
			EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                   
			@APP_ID,                                       
			@APP_VERSION_ID,                                                  
			'EBICC53'                                                
			
			IF ( @COV_ID = 0 )                                              
			BEGIN                                          
			print(1)                                                    
			RAISERROR ('Coverage ID not found for Credit Card and Depositors Forgery (HO-53)',16, 1)                                        
			--RETURN                                       
			END                                      
			ELSE                                      
			BEGIN                                      
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                    
			@APP_ID, --@APP_ID     int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                      
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                                                            
			@COV_ID, --@COVERAGE_CODE_ID int,     
			500,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                               
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                        
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
			
			END                                                                         
			--Personal Property Away From Premises --EBPPOP                                    
			EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                  
			@APP_ID,                                                  
			@APP_VERSION_ID,                                                  
			'EBPPOP'                                                
			
			IF ( @COV_ID = 0 )                                              
			BEGIN                                          
			print(1)                                                    
			RAISERROR ('Coverage ID not found for Personal Property Away From Premises',16, 1)                                        
			--RETURN                                       
			END                                      
			ELSE                                      
			BEGIN                                      
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID     int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                                                            
			@COV_ID, --@COVERAGE_CODE_ID int,                                                            
			NULL,  --@LIMIT_1 Decimal(18,2),                                                       
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                               
			
			END                                      
			
			--Business Property Increased Limits (HO-312)                                      
			EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                  
			@APP_ID,                                                  
			@APP_VERSION_ID,                                                  
			'IBUSP'                                                 
			
			IF ( @COV_ID = 0 )                                              
			BEGIN                                         
			print(1)                                           
			RAISERROR ('Coverage ID not found for Personal Property Away From Premises (HO-312).',16, 1)                                        
			--RETURN                                       
			END                                      
			ELSE              
			BEGIN                                      
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID     int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                                                            
			@COV_ID, --@COVERAGE_CODE_ID int,                                                            
			2500,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                            
		
		END                                    
		
		
		
		
		--HO-5 ------------------------------------------------------------                          
		IF ( @POLICY_TYPE = 11149 )                            
		BEGIN                          
			--EBRCPP Replacement Cost Personal Property (HO-34)                          
			EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                  
			@APP_ID,                         
			@APP_VERSION_ID,                                                  
			'EBRCPP'                                                 
			
			IF ( @COV_ID = 0 )                                              
			BEGIN                                         
			print(1)                                        
			RAISERROR ('Coverage ID not found for Replacement Cost Personal Property (HO-34).',16, 1)                                        
			--RETURN                                       
			END                                      
			ELSE                                      
			BEGIN                                      
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID     int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                                                            
			@COV_ID, --@COVERAGE_CODE_ID int,                                                            
			NULL,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                   
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                    
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                         
			
			IF ( @@ERROR <> 0 )                                              
			BEGIN                                     
			--print(1)                                    
			RAISERROR ('Unable to save Replacement Cost Personal Property (HO-34).',16, 1)                                        
			RETURN                                       
			END                             
			
			END                                
			
			
			--143  EBP24 Preferred Plus V.I.P. Coverage (HO-24) 14 1                          
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID     int,                                                       
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                                                            
			143, --@COVERAGE_CODE_ID int,                                                            
			NULL,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                    
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                      
			
			IF ( @@ERROR <> 0 )                                              
			BEGIN                                         
			--print(1)                                    
			RAISERROR ('Unable to save Preferred Plus V.I.P. Coverage (HO-24).',16, 1)                                    
			RETURN                                       
			END                       
			
			--144  EBEP11 Expanded Replacement Coverage A + B (HO-11) 14 1                          
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID     int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                                                            
			144, --@COVERAGE_CODE_ID int,                                                            
			NULL,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                    
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                      
			
			IF ( @@ERROR <> 0 )                   
			BEGIN                                         
			--print(1)                                    
			RAISERROR ('Unable to save Expanded Replacement Coverage A + B (HO-11).',16, 1)                             
			RETURN                                       
			END                                            
			
			
			--274  PERIJ Personal Injury (HO-82)                    
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID     int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                    
			274, --@COVERAGE_CODE_ID int,                                                            
			NULL,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                    
			NULL,  --@DEDUCTIBLE_2 DECIMAL(18,2)                      
			'S2' --COVERAGE_TYPE            
			IF ( @@ERROR <> 0 )               
			BEGIN                              
			--print(1)                                    
			RAISERROR ('Unable to save Personal Injury (HO-82).',16, 1)                                        
			RETURN                                       
			END                             
		END                          
		--End of HO-5-----------------------------------------------------                          
		
		--HO-6 Deluxe------------------------------    
		IF ( @POLICY_TYPE = 11246)                          
		BEGIN                           
			--EBCASP Unit Owners Coverage A Special Coverage (HO-32) 160                                     
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID     int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                                                            
			160, --@COVERAGE_CODE_ID int,                                                   
			NULL,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                    
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
			
			
			--166 Condominium Deluxe Coverage (HO-66)                    
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID     int,                                                            
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                               
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                                                            
			166, --@COVERAGE_CODE_ID int,                                                            
			NULL,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
			
			IF (@@ERROR <> 0 )                        
			BEGIN                                         
			
			RAISERROR ('Unable to save Condominium Deluxe Coverage (HO-66).',16, 1)                                        
			RETURN                    
			END                                
		
		END                     
		--End of HO-6 Deluxe-----------------------------  
		
		--HO-6 and HO-6 Deluxe---------------------------                          
		
		IF ( @POLICY_TYPE = 11196 OR @POLICY_TYPE = 11246 )                          
		BEGIN                                              
		--+++++                    
			--LAC Loss Assessment Coverage (HO-35) 162                           
			
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID     int,                       
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                                                            
			-1,  --@COVERAGE_ID int,                                                            
			162, --@COVERAGE_CODE_ID int,                                                            
			1000,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
			NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                     
			
			IF ( @@ERROR <> 0 )                                              
			BEGIN                                         
			
			RAISERROR ('Unable to save LAC Loss Assessment Coverage (HO-35).',16, 1)                                        
			RETURN                                       
			END                                                       
		
		END                    
		
		--End of HO-6 and HO-6 Deluxe-----------------------------                          
		
		--HO-4 Deluxe-----------------------------------------------------------------                    
		IF ( @POLICY_TYPE = 11245)                       
		BEGIN                    
		
			--165 Renters Deluxe Coverage (HO-64)                    
			EXEC Proc_SAVE_DWELLING_COVERAGES                                       
			@CUSTOMER_ID, --@CUSTOMER_ID     int,                                                            
			@APP_ID, --@APP_ID     int,                                                           
			@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                            
			@DWELLING_ID, --@DWELLING_ID smallint,                        
			-1,  --@COVERAGE_ID int,                                                            
			165, --@COVERAGE_CODE_ID int,                                                            
			NULL,  --@LIMIT_1 Decimal(18,2),                                                            
			NULL,  --@LIMIT_2 Decimal(18,2),                                                                 
			NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                              
			NULL --@DEDUCTIBLE_2 DECIMAL(18,2)                                      
			
			IF (@@ERROR <> 0 )                                   
			BEGIN                                         
			
			RAISERROR ('Unable to save Renters Deluxe Coverage (HO-64).',16, 1)                                        
			RETURN                    
			END                    
		
		END                     
		-----------------End of HO-4 and HO-4 Deluxe-------------------------------------------                      
	END                          
	--END OF INDIANA-------------------------------------------                          
                          
 --*********************************************************************                                            
END                                        
                          
                                    
                                  
                                
                              
                              
                            
                          
                        
                      
                    
         
                    
                  
                
              
            
          
        
      
    
  





GO

