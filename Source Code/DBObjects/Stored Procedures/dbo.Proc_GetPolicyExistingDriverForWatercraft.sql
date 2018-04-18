IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyExistingDriverForWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyExistingDriverForWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------        
Proc Name        : dbo.Proc_GetPolicyExistingDriverForWatercraft        
Created by       : SWASTIKA         
Date             : 27th Mar'06    
Purpose          : retrieving data from POL_WATERCRAFT_DRIVER_DETAILS        
Revison History  :        
Used In          : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
  
--DROP PROC Proc_GetPolicyExistingDriverForWatercraft  
------   ------------       -------------------------*/        
CREATE PROC Dbo.Proc_GetPolicyExistingDriverForWatercraft        
@CUSTOMERID int,        
@POLICYID int ,        
@POLICYVERSIONID int        
        
AS        
BEGIN        
        
declare @LOBID nvarchar(5)        
select @LOBID=POLICY_LOB from POL_CUSTOMER_POLICY_LIST where  CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID    
  
--Added By Sibin on 27 Dec 09 for Itrack Issue 4905 to Copy drivers & household member of all LOB in Watercraft,Automobile and Homeowner   
SELECT DISTINCT PD.DRIVER_ID, PD.CUSTOMER_ID AS CUSTOMERID,  PD.DRIVER_CODE,         
                      PD.DRIVER_FNAME + ' ' + PD.DRIVER_MNAME + ' ' +        
   PD.DRIVER_LNAME AS DRIVERNAME,        
                       CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME + ' ' + CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME + ' ' + CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME        
                       AS CUSTOMERNAME, PL.POLICY_DISP_VERSION,PD.POLICY_ID,        
 PL.POLICY_VERSION_ID,        
 PL.POLICY_NUMBER,        
 PL.POLICY_DISP_VERSION        
FROM         POL_DRIVER_DETAILS PD INNER JOIN        
                      POL_CUSTOMER_POLICY_LIST PL ON PD.CUSTOMER_ID = PL.CUSTOMER_ID       
        AND PL.POLICY_ID=PD.POLICY_ID    
       AND PL.POLICY_VERSION_ID=PD.POLICY_VERSION_ID    
                     LEFT OUTER JOIN        
                      CLT_CUSTOMER_LIST ON PD.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID        
WHERE (PD.CUSTOMER_ID = @CUSTOMERID) AND--(PL.POLICY_LOB = @LOBID)AND --Commented by Sibin for Itrack Issue 5304  
IsNull(PD.IS_ACTIVE,'') <> 'N'    
  
UNION      
     
SELECT DISTINCT PD.DRIVER_ID, PD.CUSTOMER_ID AS CUSTOMERID,  PD.DRIVER_CODE,         
                      PD.DRIVER_FNAME + ' ' + PD.DRIVER_MNAME + ' ' +        
   PD.DRIVER_LNAME AS DRIVERNAME,        
                       CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME + ' ' + CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME + ' ' + CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME        
                       AS CUSTOMERNAME, PL.POLICY_DISP_VERSION,PD.POLICY_ID,        
 PL.POLICY_VERSION_ID,        
 PL.POLICY_NUMBER,        
 PL.POLICY_DISP_VERSION        
FROM         POL_WATERCRAFT_DRIVER_DETAILS PD INNER JOIN        
                      POL_CUSTOMER_POLICY_LIST PL ON PD.CUSTOMER_ID = PL.CUSTOMER_ID       
        AND PL.POLICY_ID=PD.POLICY_ID    
       AND PL.POLICY_VERSION_ID=PD.POLICY_VERSION_ID    
                     LEFT OUTER JOIN        
                      CLT_CUSTOMER_LIST ON PD.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID        
WHERE (PD.CUSTOMER_ID = @CUSTOMERID) AND --(PL.POLICY_LOB = @LOBID)AND--Commented by Sibin for Itrack Issue 5304        
  IsNull(PD.IS_ACTIVE,'') <> 'N'        
       
END        
      
    
    
GO

