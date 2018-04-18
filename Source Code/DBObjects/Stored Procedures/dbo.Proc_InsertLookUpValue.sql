IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertLookUpValue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertLookUpValue]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /*----------------------------------------------------------  
Proc Name          : Dbo.Proc_InsertLookUpValue  
Created by           : Mohit Gupta  
Date                    : 19/05/2005  
Purpose               :   
Revison History :  
Used In                :   Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
-- drop proc Proc_InsertLookUpValue
------   ------------       -------------------------*/  
CREATE   PROCEDURE [dbo].[Proc_InsertLookUpValue]  
(  
@LOOKUP_UNIQUE_ID     int output ,  
@LOOKUP_ID     smallint,  
@LOOKUP_VALUE_ID     smallint output,  
@LOOKUP_VALUE_CODE     nvarchar(80),  
@LOOKUP_VALUE_DESC     nvarchar(200),  
--@LOOKUP_SYS_DEF     nchar(2),  
@IS_ACTIVE     nchar(2)--,  
--@LAST_UPDATED_DATETIME     datetime,  
--@LOOKUP_FRAME_OR_MASONRY     varchar(1),  
--@Type     nvarchar(2)   
)  
AS  
BEGIN  
  
SELECT @LOOKUP_UNIQUE_ID = IsNull(Max(LOOKUP_UNIQUE_ID),0) + 1   
FROM MNT_LOOKUP_VALUES  
  
SELECT @LOOKUP_VALUE_ID=IsNull(Max(LOOKUP_VALUE_ID),0) + 1 FROM MNT_LOOKUP_VALUES  
WHERE LOOKUP_ID=@LOOKUP_ID  
   
INSERT INTO MNT_LOOKUP_VALUES  
(  
LOOKUP_UNIQUE_ID,LOOKUP_ID,LOOKUP_VALUE_ID,LOOKUP_VALUE_CODE,LOOKUP_VALUE_DESC,LOOKUP_SYS_DEF,IS_ACTIVE,LAST_UPDATED_DATETIME,  
LOOKUP_FRAME_OR_MASONRY,Type  
)  
VALUES  
(  
@LOOKUP_UNIQUE_ID,@LOOKUP_ID,@LOOKUP_VALUE_ID,@LOOKUP_VALUE_CODE,@LOOKUP_VALUE_DESC,'1',@IS_ACTIVE,GetDate(),  
null,null  
)  

--Added by Charles on 8-Apr-10 for Multilingual Support
 EXEC PROC_REPLICATE_MASTER 'LOOKUP_UNIQUE_ID',@LOOKUP_UNIQUE_ID,'MNT_LOOKUP_VALUES','MNT_LOOKUP_VALUES_MULTILINGUAL','LOOKUP_VALUE_DESC'

END  
  
GO

