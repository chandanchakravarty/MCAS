IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateDPVATCivilTransportVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateDPVATCivilTransportVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Pradeep Kushwaha>
-- Create date: <14-Dec-2010>
-- Description:	<To Update records in POL_CIVIL_TRANSPORT_VEHICLES table for DPVAT (Cat. 3 e 4) product>
-- Drop PROC Proc_UpdateDPVATCivilTransportVehicle
-- =============================================
CREATE PROC [dbo].[Proc_UpdateDPVATCivilTransportVehicle]
	-- Add the parameters for the stored procedure here
	@CUSTOMER_ID INT,        
	@POLICY_ID INT,        
	@POLICY_VERSION_ID SMALLINT,        
	@VEHICLE_ID INT OUT,     
	@CATEGORY INT ,     
	@TICKET_NUMBER INT,
	@STATE_ID INT,
	@MODIFIED_BY INT,          
	@LAST_UPDATED_DATETIME DATETIME,
	 @EXCEEDED_PREMIUM INT = NULL  
	--,@ORIGINAL_VERSION_ID INT=NULL   
AS
BEGIN
DECLARE  @CO_APPLICANT_ID INT   --- changes by praveer for TFS# 726
SELECT @CO_APPLICANT_ID=APPLICANT_ID  FROM POL_APPLICANT_LIST  WITH(NOLOCK)       
  WHERE IS_PRIMARY_APPLICANT=1 AND  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID
 UPDATE POL_CIVIL_TRANSPORT_VEHICLES                                                      
    SET    
    TICKET_NUMBER=@TICKET_NUMBER,
    CATEGORY=@CATEGORY,
    STATE_ID=@STATE_ID ,
    MODIFIED_BY=@MODIFIED_BY, 
    LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME 
    --, ORIGINAL_VERSION_ID=@ORIGINAL_VERSION_ID,
	,CO_APPLICANT_ID=@CO_APPLICANT_ID,  --- changes by praveer for TFS# 726
	  EXCEEDED_PREMIUM = @EXCEEDED_PREMIUM
    WHERE      
     CUSTOMER_ID=@CUSTOMER_ID AND           
     POLICY_ID=@POLICY_ID AND              
     POLICY_VERSION_ID = @POLICY_VERSION_ID AND      
     VEHICLE_ID=@VEHICLE_ID  
     
    RETURN 1  
END
 

GO

