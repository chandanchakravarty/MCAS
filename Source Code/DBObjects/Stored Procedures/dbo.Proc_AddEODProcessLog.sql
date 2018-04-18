IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AddEODProcessLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AddEODProcessLog]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
Proc Name       :  dbo.Proc_AddEODProcessLog  
Created by      :  Ravindra  
Date            :  06-05-2007  
Purpose         :  To log activity level information of EOD Process  
Revison History :                  
Used In         :  Wolverine                  
                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
-- drop proc dbo.Proc_AddEODProcessLog  
CREATE PROC [dbo].[Proc_AddEODProcessLog]  
(  
 @ACTIVITY_DESCRIPTION   Varchar(1000),  
 @STATUS   Varchar(20) = null ,  
 @START_DATETIME  DateTime,  
 @END_DATETIME  Datetime,  
 @ACTIVITY  Varchar(50)= null,  
 @SUB_ACTIVITY  Varchar(50)= null,  
 @CLIENT_ID  Int= null,  
 @POLICY_ID  Int= null,  
 @POLICY_VERSOIN_ID SmallInt= null,  
 @ADDITIONAL_INFO Varchar(2000)= null  
  
)  
AS  
BEGIN   
  
 --Ravindra: For iTrack 4439  
 DECLARE @CURRENT_VERSION_ID Int  
   
 IF(@SUB_ACTIVITY = 104 )   
 BEGIN   
  SELECT @CURRENT_VERSION_ID =ISNULL(MAX(NEW_POLICY_VERSION_ID),0) 
  FROM POL_POLICY_PROCESS   with(nolock)                                              
  WHERE CUSTOMER_ID=@CLIENT_ID                                           
  AND POLICY_ID=@POLICY_ID                       
  AND PROCESS_ID IN(18,25,32)                                                
  AND ISNULL(REVERT_BACK,'N')  <> 'Y'  
 END  
 ELSE  
 BEGIN   
  SELECT @CURRENT_VERSION_ID  =  @POLICY_VERSOIN_ID  
 END  
  
 INSERT INTO EOD_PROCESS_LOG ( ACTIVITY_DESCRIPTION , STATUS , ACTIVITY, SUB_ACTIVITY ,  
  CLIENT_ID ,POLICY_ID ,POLICY_VERSOIN_ID,ADDITIONAL_INFO ,  
  START_DATETIME, END_DATETIME)  
 VALUES (@ACTIVITY_DESCRIPTION , @STATUS , @ACTIVITY, @SUB_ACTIVITY ,  
  @CLIENT_ID ,@POLICY_ID ,@CURRENT_VERSION_ID, @ADDITIONAL_INFO ,  
  @START_DATETIME, @END_DATETIME)  
  
  
END  
  
  
  
  
  
GO

