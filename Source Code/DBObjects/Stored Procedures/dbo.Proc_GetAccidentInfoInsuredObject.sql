IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAccidentInfoInsuredObject]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAccidentInfoInsuredObject]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================  
-- Author:  <Pradeep Kushwaha>  
-- Create date: <22-Feb-2011>  
-- Description: < Get Insured object Details for these product
-- Individual Personal Accident
-- Group Personal Accident for Passenger
-- Group Life  
-- drop proc Proc_GetAccidentInfoInsuredObject 2,2043,739,1,'CODE' 
-- =============================================   
CREATE PROC [dbo].[Proc_GetAccidentInfoInsuredObject]        
( 
@PERSONAL_INFO_ID INT=NULL,                 
@CUSTOMER_ID INT,  
@POLICY_ID INT,  
@POLICY_VERSION_ID SMALLINT  ,
@CALLED_FOR NVARCHAR(50)=NULL
)                      
AS                      
BEGIN   
IF(UPPER(@CALLED_FOR)<>'CODE')
	BEGIN         
		SELECT PERSONAL_INFO_ID,INDIVIDUAL_NAME +' - '+ CODE AS INDIVIDUAL_NAME,CODE FROM POL_PERSONAL_ACCIDENT_INFO WITH(NOLOCK) 
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND 
		POLICY_ID=@POLICY_ID AND 
		POLICY_VERSION_ID=@POLICY_VERSION_ID and PERSONAL_INFO_ID <> @PERSONAL_INFO_ID and IS_ACTIVE='Y'
	END
ELSE
	BEGIN
		SELECT PERSONAL_INFO_ID,INDIVIDUAL_NAME +' - '+ CODE AS INDIVIDUAL_NAME,CODE FROM POL_PERSONAL_ACCIDENT_INFO WITH(NOLOCK) 
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND 
		POLICY_ID=@POLICY_ID AND 
		POLICY_VERSION_ID=@POLICY_VERSION_ID AND 
		PERSONAL_INFO_ID=@PERSONAL_INFO_ID and IS_ACTIVE='Y'
	    
	END
END 
 

GO

