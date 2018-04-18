IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_WATERCRAFT_ENDORSEMENTS_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_WATERCRAFT_ENDORSEMENTS_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_Update_WATERCRAFT_ENDORSEMENTS_POLICY                            
                            
/*----------------------------------------------------------                                                            
Proc Name   : dbo.Proc_Update_WATERCRAFT_ENDORSEMENTS_POLICY                                                           
Created by  : Pradeep                                                            
Date        : 13/03/2006                                                  
Purpose     :  Deletes/Inserts  relevant endorsements                                         
  when a watercraft is updated                                                
Revison History  :       
modified by 	:Pravesh K Chandel
Modified date	: 13 sep 2007
Purpose		: comment the code foe endorsement Op400 as it ihas been linked with coverage
                                                           
------------------------------------------------------------                                                                        
Date     Review By          Comments                                                                      
-----------------------------------------------------------*/                                                      
CREATE   PROCEDURE dbo.Proc_Update_WATERCRAFT_ENDORSEMENTS_POLICY                                                 
(                                                     
 @CUSTOMER_ID int,                                                    
 @POLICY_ID int,                                                    
 @POLICY_VERSION_ID smallint,                                                     
 @BOAT_ID smallint                                                  
)                                                    
                                                    
As                                                    
                                                     
DECLARE @TYPE  Char(10)                                                  
DECLARE @COV_TYPE_BASIS Int                                
DECLARE @AV100 Int
DECLARE @TYPE_OF_WATERCRAFT Int

SELECT   @TYPE = TYPE ,@COV_TYPE_BASIS = COV_TYPE_BASIS,
	 @TYPE_OF_WATERCRAFT = TYPE_OF_WATERCRAFT                 
FROM POL_WATERCRAFT_INFO INNER JOIN MNT_LOOKUP_VALUES ON
	POL_WATERCRAFT_INFO.TYPE_OF_WATERCRAFT=MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID
WHERE BOAT_ID = @BOAT_ID AND                                                  
 CUSTOMER_ID = @CUSTOMER_ID AND                                                  
 POLICY_ID = @POLICY_ID AND                                                  
 POLICY_VERSION_ID = @POLICY_VERSION_ID                                                  
                                            
          
DECLARE @STATE_ID Int                                                    
DECLARE @LOB_ID int          
DECLARE @PRODUCT Int                                                
                                                       
SELECT @STATE_ID = STATE_ID,                                                    
	@LOB_ID = POLICY_LOB ,      
	@PRODUCT = POLICY_TYPE                                                    
FROM POL_CUSTOMER_POLICY_LIST                                                    
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                    
	POLICY_ID = @POLICY_ID AND                                                    
	POLICY_VERSION_ID =  @POLICY_VERSION_ID                                                  
 

DECLARE @OP400 Int     
           
IF ( @STATE_ID = 49 )            
BEGIN            
	SET @OP400 = 281            
	SET @AV100= 278
END            

IF ( @STATE_ID = 14 )            
BEGIN            
	SET @OP400 = 282            
	SET @AV100= 66
END            

IF ( @STATE_ID = 22 )      
BEGIN            
	SET @OP400 = 283            
	SET @AV100= 71
END       

--If Agreed Value[11759] Insert Agreed Value (AV-100) 
IF(@COV_TYPE_BASIS = 11759) 
BEGIN 
if exists(
		SELECT COVERAGE_CODE_ID FROM POL_WATERCRAFT_COVERAGE_INFO  
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND   POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =  @POLICY_VERSION_ID AND BOAT_ID=@BOAT_ID
		AND COVERAGE_CODE_ID IN (43,85,830) 
		 )   
	EXEC Proc_Insert_POL_WATERCRAFT_ENDORSEMENT_BY_ID          
	@CUSTOMER_ID,--@CUSTOMER_ID int,                    
	@POLICY_ID,-- @POLICY_ID int,                    
	@POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,                     
	@AV100,--@ENDORSEMENT_ID smallint,                
	@BOAT_ID--@BOAT_ID smallint                   	
	IF @@ERROR <> 0             
	BEGIN            
		RAISERROR ('Unable to Delete AV100 Endorsement.', 16, 1)            
	END            
END 

--If Actual Cash Value[11758] remove Agreed Value (AV-100) 
IF(@COV_TYPE_BASIS = 11758)
BEGIN 
	EXEC Proc_Delete_POL_WATERCRAFT_ENDORSEMENT_BY_ID          
	@CUSTOMER_ID,--@CUSTOMER_ID int,                    
	@POLICY_ID,-- @POLICY_ID int,                    
	@POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,                     
	@AV100,--@ENDORSEMENT_ID smallint,                
	@BOAT_ID--@BOAT_ID smallint                   	
	IF @@ERROR <> 0             
	BEGIN            
		RAISERROR ('Unable to save AV100 Endorsement.', 16, 1)            
	END            
END 

--Boat Type Is Waverunner Or JetSki
--THIS CODE COMMENTED BY PRAVESH ON 20 MARCH 2008 AS DISCUSSED WITH RAJAN AS DO NOT REMOVE AV100 IN CASE OF JET SKI IF COVERGAE IS SELECTED
/*
IF(@TYPE_OF_WATERCRAFT = 11386 OR @TYPE='JS')
BEGIN 
	EXEC Proc_Delete_POL_WATERCRAFT_ENDORSEMENT_BY_ID          
	@CUSTOMER_ID,--@CUSTOMER_ID int,                    
	@POLICY_ID,-- @POLICY_ID int,                    
	@POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,                     
	@AV100,--@ENDORSEMENT_ID smallint,                
	@BOAT_ID--@BOAT_ID smallint                   	
	IF @@ERROR <> 0             
	BEGIN            
		RAISERROR ('Unable to save AV100 Endorsement.', 16, 1)            
	END            
END 
*/ --END HERE
                            
/*          
If Boat type is           
jetskis-with or without lift bar,           
waverunners and mini-jet boats),           
the OP400-Personal Watercraft endorsement is mandatory           
281 49 4 B O Personal Watercraft Endorsement (OP 400)          
282 14 4 B O Personal Watercraft Endorsement (OP 400)          
283 22 4 B O Personal Watercraft Endorsement (OP 400)          
          
*/
/*          
IF ( @TYPE IN ('IO','JS') )          
BEGIN          
	EXEC Proc_Insert_POL_WATERCRAFT_ENDORSEMENT_BY_ID          
	@CUSTOMER_ID,--@CUSTOMER_ID int,                    
	@POLICY_ID,-- @POLICY_ID int,                    
	@POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,                     
	@OP400,--@ENDORSEMENT_ID smallint,                
	@BOAT_ID--@BOAT_ID smallint                  
	
	IF @@ERROR <> 0           
	BEGIN          
		RAISERROR ('Unable to save OP400 Endorsement.', 16, 1)          
	END          
END     
ELSE
BEGIN
	EXEC Proc_Delete_POL_WATERCRAFT_ENDORSEMENT_BY_ID          
	@CUSTOMER_ID,--@CUSTOMER_ID int,                    
	@POLICY_ID,-- @POLICY_ID int,                    
	@POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,                     
	@OP400,--@ENDORSEMENT_ID smallint,                
	@BOAT_ID--@BOAT_ID smallint                  
	IF @@ERROR <> 0             
	BEGIN            
		RAISERROR ('Unable to save AV100 Endorsement.', 16, 1)            
	END            
END            
  */                                    
--If watercraft policy, then default WP-100--------------------------------        
IF ( @LOB_ID = 4 )        
BEGIN        
 DECLARE @WP100 Int        
        
 IF ( @STATE_ID = 14 ) SET @WP100 = 291        
 IF ( @STATE_ID = 22 ) SET @WP100 = 292        
 IF ( @STATE_ID = 49 ) SET @WP100 = 293        
     
 EXEC Proc_Insert_POL_WATERCRAFT_ENDORSEMENT_BY_ID        
   @CUSTOMER_ID,--@CUSTOMER_ID int,                      
    @POLICY_ID,-- @POLICY_ID int,                      
     @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,                       
     @WP100,--@ENDORSEMENT_ID smallint,                  
     @BOAT_ID--@BOAT_ID smallint           
        
 IF @@ERROR <> 0            
  BEGIN            
    RAISERROR ('Unable to save WP Endorsement.', 16, 1)            
         
  END                     
END        
--************************************************************************          
      
     
--If home policy, then default WP-100 for        
--HO-2, HO-3, HO-4, HO-5, HO-6, HO-2 Repair, HO-3 Repair        
--------------------------------        
IF ( @LOB_ID = 1 )        
BEGIN        
 DECLARE @HO865 Int        
 DECLARE @DWELLING_ID Int        
         
 IF ( @STATE_ID = 14 ) SET @HO865 = 295        
 IF ( @STATE_ID = 22 ) SET @HO865 = 294        
        
 --Get min dwelling ID        
 SELECT @DWELLING_ID = MIN(DWELLING_ID)        
 FROM POL_DWELLINGS_INFO        
 WHERE         
  CUSTOMER_ID = @CUSTOMER_ID AND                                                    
  POLICY_ID = @POLICY_ID AND                                                    
  POLICY_VERSION_ID = @POLICY_VERSION_ID              
         
 IF ( @DWELLING_ID IS NOT NULL )        
 BEGIN        
         
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY        
    @CUSTOMER_ID,--@CUSTOMER_ID int,                      
     @POLICY_ID,-- @POLICY_ID int,                      
      @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,                       
      @HO865,--@ENDORSEMENT_ID smallint,                  
      @DWELLING_ID--@BOAT_ID smallint           
         
  IF @@ERROR <> 0             
   BEGIN            
     RAISERROR ('Unable to save HO-865 Endorsement.', 16, 1)            
          
   END          
 END                   
END       
       
--************************************************************************       
                                      
RETURN 1                                                    
                                                   
                                                    
                                                    
                                                  
                                    
                                              
                                            
                                          
                                        
                                      
                                    
                           
                                
                              
                            
                          
                        
                      
                
                  
                
              
            
          
        
      
    
  









GO

