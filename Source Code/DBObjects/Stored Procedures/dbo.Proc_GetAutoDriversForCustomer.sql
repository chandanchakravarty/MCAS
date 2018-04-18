IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAutoDriversForCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAutoDriversForCustomer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                        
Proc Name            : dbo.Proc_GetAutoDriversForCustomer                                        
Created by           : Sumit Chhabra                                       
Date                 : 23/08/2006                                        
Purpose              : To get the all the auto drivers for the customer                            
Revison History  :                                        
Used In                 :   Wolverine                                          
------------------------------------------------------------                                        
Date     Review By          Comments                                        
------   ------------       -------------------------*/                                        
--drop proc Proc_GetAutoDriversForCustomer                                      
CREATE PROC [dbo].[Proc_GetAutoDriversForCustomer]                                          
@CUSTOMER_ID  int                                      
as                                        
begin                                        
DECLARE @AUTO_LOB INT                            
DECLARE @MOTOR_LOB INT          
SET @AUTO_LOB = 2          
SET @MOTOR_LOB = 3          
                            
                            
--GET THE LIST OF ALL DRIVERS FOR THE CURRENT CUSTOMER FOR AUTO LOB                            
 select                     
  (CAST(D.CUSTOMER_ID AS VARCHAR) + '^' + CAST(D.POLICY_ID AS VARCHAR) + '^' + CAST(D.POLICY_VERSION_ID AS VARCHAR) + '^' +                 
 CAST(D.DRIVER_ID AS VARCHAR) + '^' + 'APP') AS DRIVER_ID,                
  (ISNULL(D.DRIVER_FNAME,'') + ' ' + ISNULL(D.DRIVER_MNAME,'') + ' ' + ISNULL(D.DRIVER_LNAME,'')                 
 + '-' + ISNULL(D.DRIVER_CODE,'') + ' (' + ISNULL(APP_NUMBER,'') + ' - Ver ' + cast(D.POLICY_ID as varchar) + '.0)'       
)AS DRIVER_NAME,  
--Added by Sibin on 25 Feb 09 for Itrack Issue 5483  
(ISNULL(D.DRIVER_FNAME,'') + ' ' + ISNULL(D.DRIVER_MNAME,'') + ' ' + ISNULL(D.DRIVER_LNAME,'')+ '-' +   
 ISNULL(D.DRIVER_CODE,'')) AS DRIVERNAME,   
 A.POLICY_LOB AS LOB,APP_NUMBER AS POLICY_NUMBER,A.APP_VERSION_ID AS POLICY_VERSION_ID    
--Added till here                    
--  D.DRIVER_ID                      
 FROM                             
 POL_DRIVER_DETAILS D                            
 JOIN                            
 POL_CUSTOMER_POLICY_LIST A                            
 ON                            
  D.CUSTOMER_ID = A.CUSTOMER_ID AND                            
  D.POLICY_ID = A.POLICY_ID AND                            
  D.POLICY_VERSION_ID = A.POLICY_VERSION_ID AND  --Uncommented for 5457 on 6 May 09                          
  D.IS_ACTIVE='Y' AND                            
  A.IS_ACTIVE='Y'                            
 WHERE                            
  A.CUSTOMER_ID = @CUSTOMER_ID AND                            
  (A.POLICY_LOB = @AUTO_LOB OR A.POLICY_LOB = @MOTOR_LOB)       
                      
union                 
 select                 
  (CAST(D.CUSTOMER_ID AS VARCHAR) + '^' + CAST(D.POLICY_ID AS VARCHAR) + '^' + CAST(D.POLICY_VERSION_ID AS VARCHAR) + '^' +                 
 CAST(D.DRIVER_ID AS VARCHAR) + '^' + 'POL') AS DRIVER_ID,                
 (ISNULL(D.DRIVER_FNAME,'') + ' ' + ISNULL(D.DRIVER_MNAME,'') + ' ' + ISNULL(D.DRIVER_LNAME,'')                 
 + '-' + ISNULL(D.DRIVER_CODE,'') + ' (' + ISNULL(A.POLICY_NUMBER,'') + ' - Ver ' + cast(D.POLICY_VERSION_ID as varchar) + '.0)')      
AS DRIVER_NAME,  
--Added by Sibin on 25 Feb 09 for Itrack Issue 5483  
(ISNULL(D.DRIVER_FNAME,'') + ' ' + ISNULL(D.DRIVER_MNAME,'') + ' ' + ISNULL(D.DRIVER_LNAME,'')+ '-' + ISNULL(D.DRIVER_CODE,''))AS DRIVERNAME,  
A.POLICY_LOB AS LOB,A.POLICY_NUMBER AS POLICY_NUMBER,A.POLICY_VERSION_ID AS POLICY_VERSION_ID                      --Added till here  
--  D.DRIVER_ID                      
 FROM                 
 POL_DRIVER_DETAILS D                            
 JOIN                            
 pol_customer_policy_list A    
 ON                            
  D.CUSTOMER_ID = A.CUSTOMER_ID AND                            
  D.POLICY_ID = A.POLICY_ID AND                            
  D.POLICY_VERSION_ID = A.POLICY_VERSION_ID AND    --Uncommented for 5457 on 6 May 09                       
  D.IS_ACTIVE='Y' AND                            
  A.IS_ACTIVE='Y'                            
 WHERE                            
  A.CUSTOMER_ID = @CUSTOMER_ID AND                            
  (A.POLICY_LOB = @AUTO_LOB OR A.POLICY_LOB = @MOTOR_LOB)          
      
 ORDER BY DRIVERNAME,POLICY_NUMBER DESC,POLICY_VERSION_ID  
end

GO

