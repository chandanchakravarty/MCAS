IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETMAKEMODELDATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETMAKEMODELDATA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                          
Proc Name      : dbo.[Proc_GetMakeModelData]                          
Created by     : Pradeep Kushwaha                        
Date           : 03-05-2010                          
Purpose        : retrieving MAKEMODEL from MNT_FIPE_CODE_MASTER BASED ON FILE CODE                           
Revison History:                 
Modify by      :                          
Date           :                          
Purpose        :         
Used In        : Ebix Advantage                      
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
--Drop PROC DBO.PROC_GETMAKEMODELDATA 
CREATE PROC [dbo].[PROC_GETMAKEMODELDATA] 
 
	@FIPE_CODE NVARCHAR(20),
	@MAKEMODEL NVARCHAR(100) OUT,
	@LOOUP_UNIQUE_ID NVARCHAR(20)=NULL OUT,
	@CAPACITY int=null OUT
AS                          
                          
BEGIN     
DECLARE @CATEGORY NVARCHAR(20)

 
 SET @MAKEMODEL =(SELECT  FIPE_MAKE +' ' + MODEL FROM MNT_FIPE_CODE_MASTER WITH(NOLOCK)  WHERE FIPE_CODE=@FIPE_CODE ) 
 SET @CATEGORY = (SELECT CATEGORY_AUTO  FROM MNT_FIPE_CODE_MASTER WITH(NOLOCK)  WHERE FIPE_CODE=@FIPE_CODE ) 
 SET @LOOUP_UNIQUE_ID=(SELECT LOOKUP_UNIQUE_ID FROM MNT_LOOKUP_VALUES  WHERE LOOKUP_ID=1378 AND LOOKUP_VALUE_CODE=@CATEGORY)
 SET @CAPACITY = (SELECT CAPACITY  FROM MNT_FIPE_CODE_MASTER WITH(NOLOCK)  WHERE FIPE_CODE=@FIPE_CODE ) 
	 
 
END            
GO

