IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppPolDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppPolDetail]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                  
Proc Name                : Dbo.roc_GetAppPolDetail                        
Created by               :Ashish kumar                                                
Date                    : 11 jan 2006                                               
Purpose                  : To get current policy no and version     
MODIFIED BY			: PRAVESH 
MODIFIED DATE		: 21 april 2008
 
Used In                  : Wolverine                                                  
------------------------------------------------------------                                                  
Date     Review By          Comments                                                  
------   ------------       -------------------------*/                                                  
CREATE  proc Dbo.Proc_GetAppPolDetail          
(                                                  
 @CUSTOMER_ID    int,                                                  
 @APP_ID    int,                                                  
 @APP_VERSION_ID   int   
         
)                                                  
as                                   

SELECT     POLICY_ID,POLICY_VERSION_ID,APP_ID,APP_VERSION_ID,STATE_ID
FROM        POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
where ( CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID )


GO

