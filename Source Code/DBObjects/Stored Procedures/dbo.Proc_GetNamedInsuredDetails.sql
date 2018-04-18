IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNamedInsuredDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNamedInsuredDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                              
Proc Name       : dbo.Proc_GetNamedInsuredDetails                                        
Created by      : Sumit Chhabra                                            
Date            : 02/05/2006                                              
Purpose         : Get Named Insured/Insured      
Created by      : Sumit Chhabra                                             
Revison History :                                              
Used In        : Wolverine                                              
------------------------------------------------------------                                              
Date     Review By          Comments                                              
------   ------------       -------------------------*/                                              
CREATE PROC dbo.Proc_GetNamedInsuredDetails                                                                      
@VEHICLE_OWNER int,      
@NAMED_INSURED_ID int      
AS                                              
BEGIN                                              
/*Lookup Unique ids of Vehicle Owner being enumerated       
   NAMED_INSURED = 11752,      
   INSURED = 11753,      
   NOT_ON_POLICY = 11754      
*/      
--We have to fetch insureds only for named_insured and insured, if not on policy comes, return      
if(@VEHICLE_OWNER is null or @VEHICLE_OWNER=11754) return      
      
--Select Named insureds for the current policy      
--A value of 0 is being passed for vehicle_owner from driver details screen to fetch named insured data  
if(@VEHICLE_OWNER=11752 or @VEHICLE_OWNER=0 or @VEHICLE_OWNER=14151)      
 SELECT             
  (ISNULL(FIRST_NAME,'') + ' ' +  ISNULL(LAST_NAME,'')) AS NAME,
	ADDRESS1,ADDRESS2,CITY,STATE,ZIP_CODE AS ZIP,PHONE AS HOME_PHONE,BUSINESS_PHONE AS WORK_PHONE,EXT AS EXTENSION,      
  MOBILE AS MOBILE_PHONE      
 FROM       
 CLT_APPLICANT_LIST       
 where       
  APPLICANT_ID = @NAMED_INSURED_ID      
else if(@VEHICLE_OWNER=11753) --select insured name for the current CLAIM DATA      
 SELECT       
  ADDRESS1,ADDRESS2,CITY,'' AS STATE,ZIP,HOME_PHONE,WORK_PHONE,EXTENSION,MOBILE_PHONE  --temporary value being given to state as state field is not present at claim info    
 from      
  CLM_CLAIM_INFO      
 WHERE      
  CLAIM_ID = @NAMED_INSURED_ID      
END              
      
  





GO

