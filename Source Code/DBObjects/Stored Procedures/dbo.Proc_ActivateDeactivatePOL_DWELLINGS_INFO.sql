IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePOL_DWELLINGS_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePOL_DWELLINGS_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------          
Proc Name       : dbo.Proc_ActivateDeactivatePOL_DWELLINGS_INFO         
Created by      :  shafi          
Date                :  17 FEB 2006     
Purpose         :  To update the record in POL_DWELLINGS_INFO   table          
Revison History :          
Used In         :    Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
--DROP PROC Proc_ActivateDeactivatePOL_DWELLINGS_INFO  
CREATE   PROC Proc_ActivateDeactivatePOL_DWELLINGS_INFO   
(          
@CUSTOMER_ID INT,      
@POL_ID INT,      
@POL_VERSION_ID SMALLINT,      
@DWELLING_ID SMALLINT,      
@IS_ACTIVE  CHAR(2),    
@LOCATION_ID SMALLINT          
)          
AS          
BEGIN         

DECLARE @AGENCY_BILL_MORTAGAGEE SMALLINT,@INSURED_BILL_MORTAGAGEE SMALLINT,@MORTAGAGEE_INCEPTION SMALLINT    
 SET @AGENCY_BILL_MORTAGAGEE = 11277    
 SET @INSURED_BILL_MORTAGAGEE = 11278  
 SET @MORTAGAGEE_INCEPTION = 11276
--when the dwellling is being deactivated, check that that the dwelling has not been assigned to another     
--active dwelling during the time...if it has been then prevent the user from deactivating the dwelling    
if(upper(@is_active)='Y')    
begin    
if exists(select dwelling_id from POL_dwellings_info where    
       CUSTOMER_ID=@CUSTOMER_ID AND       
       POLICY_ID=@POL_ID AND      
       POLICY_VERSION_ID=@POL_VERSION_ID AND      
       LOCATION_ID= @LOCATION_ID AND    
       IS_ACTIVE='Y')    
RETURN -2    
end    

--When the record is being deactivated and the current record is the selected mortagagee for the application,  
--set the mortagagee value to 0 for the application  
if(UPPER(@IS_ACTIVE)='N')  
BEGIN   
IF exists(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID   
  AND POLICY_VERSION_ID=@POL_VERSION_ID   
  AND DWELLING_ID=@DWELLING_ID   
  AND ISNULL(IS_ACTIVE,'N')='Y'  
  AND BILL_TYPE_ID IN (@AGENCY_BILL_MORTAGAGEE,@INSURED_BILL_MORTAGAGEE,@MORTAGAGEE_INCEPTION))  
 UPDATE POL_CUSTOMER_POLICY_LIST SET DWELLING_ID = 0,ADD_INT_ID = 0 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND             
    POLICY_VERSION_ID=@POL_VERSION_ID   
END  
    
UPDATE POL_DWELLINGS_INFO          
 SET IS_ACTIVE = @IS_ACTIVE          
 WHERE       
 CUSTOMER_ID=@CUSTOMER_ID AND       
 POLICY_ID=@POL_ID AND      
 POLICY_VERSION_ID=@POL_VERSION_ID AND      
 DWELLING_ID= @DWELLING_ID      
END          
    
  






GO

