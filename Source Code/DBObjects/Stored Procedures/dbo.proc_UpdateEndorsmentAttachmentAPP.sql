IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_UpdateEndorsmentAttachmentAPP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_UpdateEndorsmentAttachmentAPP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/**************************************************************
Proc Name   : dbo.proc_UpdateEndorsmentAttachmentAPP
Created by  : Pravesh K. Chandel
Date        : 25 march 2007
Purpose     : Get the All Vehicle asssociated with a policy
Revison History  :                          
 ------------------------------------------------------------                                
Date     Review By          Comments                              
                     
***************************************************************/  
--DROP proc proc_UpdateEndorsmentAttachmentAPP
CREATE PROC dbo.proc_UpdateEndorsmentAttachmentAPP
(
 @CUSTOMER_ID int,          
 @APP_ID int,          
 @APP_VERSION_ID int    
)
as
begin

                   
  DECLARE @ENDORSEMENT_ID Int                                                           
  DECLARE @VEHICLE_ENDORSEMENT_ID int                                        
  declare  @APP_EFFECTIVE_DATE datetime  
declare @EDITION_DATE varchar(10) 
  declare @LOB_ID int
  
                       
 
 SELECT  @LOB_ID = APP_LOB,      
   @APP_EFFECTIVE_DATE= APP_EFFECTIVE_DATE  --POLICY_EFFECTIVE_DATE      
  FROM APP_LIST with(nolock) WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                          
    APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                     

if (@LOB_ID=1 or @LOB_ID=6)
	BEGIN
	 declare  CR CURSOR for select ENDORSEMENT_ID   from APP_DWELLING_ENDORSEMENTS
				WHERE CUSTOMER_ID = @CUSTOMER_ID and  APP_ID=@APP_ID and         
			        APP_VERSION_ID = @APP_VERSION_ID         
		OPEN CR
		-- Perform the first fetch.
		FETCH NEXT FROM CR into @ENDORSEMENT_ID
		-- Check @@FETCH_STATUS to see if there are any more rows to fetch.
		WHILE @@FETCH_STATUS = 0
		BEGIN
		set @EDITION_DATE=null
		    SELECT    @EDITION_DATE =ENDORSEMENT_ATTACH_ID  FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID AND       
		   @APP_EFFECTIVE_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-12') AND  @APP_EFFECTIVE_DATE<=ISNULL(DISABLED_DATE,'3000-12-12')       
	
		   update APP_DWELLING_ENDORSEMENTS set EDITION_DATE=@EDITION_DATE
			WHERE CUSTOMER_ID = @CUSTOMER_ID and  APP_ID=@APP_ID and         
			      APP_VERSION_ID = @APP_VERSION_ID         
		     	and ENDORSEMENT_ID =@ENDORSEMENT_ID  
	
		   FETCH NEXT FROM CR into @ENDORSEMENT_ID
		END
		
		CLOSE CR
		DEALLOCATE CR

	END


if (@LOB_ID=4)
	BEGIN
	 declare  CR CURSOR for select ENDORSEMENT_ID   from APP_WATERCRAFT_ENDORSEMENTS
				WHERE CUSTOMER_ID = @CUSTOMER_ID and  APP_ID=@APP_ID and         
			        APP_VERSION_ID = @APP_VERSION_ID         
		OPEN CR
		-- Perform the first fetch.
		FETCH NEXT FROM CR into @ENDORSEMENT_ID
		-- Check @@FETCH_STATUS to see if there are any more rows to fetch.
		WHILE @@FETCH_STATUS = 0
		BEGIN
		set @EDITION_DATE=null   
		 SELECT    @EDITION_DATE =ENDORSEMENT_ATTACH_ID  FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID AND       
		   @APP_EFFECTIVE_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-12') AND  @APP_EFFECTIVE_DATE<=ISNULL(DISABLED_DATE,'3000-12-12')       
	
		update APP_WATERCRAFT_ENDORSEMENTS set EDITION_DATE=@EDITION_DATE
			WHERE CUSTOMER_ID = @CUSTOMER_ID and  APP_ID=@APP_ID and         
			      APP_VERSION_ID = @APP_VERSION_ID         
		     	and ENDORSEMENT_ID = @ENDORSEMENT_ID
	
		   FETCH NEXT FROM CR into @ENDORSEMENT_ID
		END
		
		CLOSE CR
		DEALLOCATE CR

	END

if (@LOB_ID=2 or @LOB_ID=3 )
	BEGIN
	  declare  CR CURSOR  for select ENDORSEMENT_ID   from APP_VEHICLE_ENDORSEMENTS
				WHERE CUSTOMER_ID = @CUSTOMER_ID and  APP_ID=@APP_ID and         
			        APP_VERSION_ID = @APP_VERSION_ID         
		
		OPEN CR
		-- Perform the first fetch.
		FETCH NEXT FROM CR into @ENDORSEMENT_ID
		-- Check @@FETCH_STATUS to see if there are any more rows to fetch.
		WHILE @@FETCH_STATUS = 0
		BEGIN
		set @EDITION_DATE=null
		SELECT    @EDITION_DATE =ENDORSEMENT_ATTACH_ID  FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID AND       
		   @APP_EFFECTIVE_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-12') AND  @APP_EFFECTIVE_DATE<=ISNULL(DISABLED_DATE,'3000-12-12')       
	     --select @EDITION_DATE
		update APP_VEHICLE_ENDORSEMENTS set EDITION_DATE=@EDITION_DATE
			WHERE CUSTOMER_ID = @CUSTOMER_ID and  APP_ID=@APP_ID and         
			      APP_VERSION_ID = @APP_VERSION_ID         
		     	and ENDORSEMENT_ID =@ENDORSEMENT_ID
	
		   FETCH NEXT FROM CR into @ENDORSEMENT_ID
		END
		
		CLOSE CR
		DEALLOCATE CR

	END

end
 


GO

