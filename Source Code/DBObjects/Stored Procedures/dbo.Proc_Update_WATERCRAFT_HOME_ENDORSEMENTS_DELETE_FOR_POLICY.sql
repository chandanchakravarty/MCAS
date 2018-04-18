IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_WATERCRAFT_HOME_ENDORSEMENTS_DELETE_FOR_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_WATERCRAFT_HOME_ENDORSEMENTS_DELETE_FOR_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Update_WATERCRAFT_HOME_ENDORSEMENTS_DELETE_FOR_POLICY                    
                    
/*----------------------------------------------------------                                                    
Proc Name   : dbo.Proc_Update_WATERCRAFT_HOME_ENDORSEMENTS_DELETE_FOR_POLICY                                                   
Created by  : Pradeep                                                    
Date        : 12/12/2005                                                  
Purpose     :  Deletes/Inserts  relevant coverages                                 
  when a watercraft is updated                                        
Revison History  :                                                          
------------------------------------------------------------                                                                
Date     Review By          Comments                                                              
-----------------------------------------------------------*/                                              
CREATE   PROCEDURE Proc_Update_WATERCRAFT_HOME_ENDORSEMENTS_DELETE_FOR_POLICY                                            
(                                             
 @CUSTOMER_ID int,                                            
 @POLICY_ID int,                                            
 @POLICY_VERSION_ID smallint                                  
)                                            
                                            
As

BEGIN
	DECLARE @STATE_ID Int                                            
 	DECLARE @LOB_ID int                                            
        DECLARE @PRODUCT Int
                                       
	 SELECT @STATE_ID = STATE_ID,                                            
	  	@LOB_ID = POLICY_LOB,
		@PRODUCT = POLICY_TYPE                                              
	 FROM POL_CUSTOMER_POLICY_LIST                                            
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                            
	  POLICY_ID = @POLICY_ID AND                                            
	  POLICY_VERSION_ID =  @POLICY_VERSION_ID              

	--If no boats exists, teh delete WP-100 endorsement from Homes
	--for    
	--HO-2, HO-3, HO-4, HO-5, HO-6, HO-2 Repair, HO-3 Repair    
	IF ( @LOB_ID = 1 )
	BEGIN
		IF NOT EXISTS
		(
			SELECT * FROM POL_WATERCRAFT_INFO
			 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                            
			  POLICY_ID = @POLICY_ID AND                                            
			  POLICY_VERSION_ID =  @POLICY_VERSION_ID     
		)
		BEGIN
			DECLARE @HO865 Int    
			 DECLARE @DWELLING_ID Int    
			     
			 IF ( @STATE_ID = 14 ) SET @HO865 = 295    
			 IF ( @STATE_ID = 22 ) SET @HO865 = 294   
			
			
				DELETE FROM POL_DWELLING_ENDORSEMENTS
				WHERE ENDORSEMENT_ID = @HO865 AND
					CUSTOMER_ID = @CUSTOMER_ID AND                                            
					  POLICY_ID = @POLICY_ID AND                                            
					  POLICY_VERSION_ID =  @POLICY_VERSION_ID     
			 
		 
		END
	END
	
END                           


GO

