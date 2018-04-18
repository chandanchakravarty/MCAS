IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetViolationType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetViolationType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name          : Dbo.Proc_GetQuickQuoteHomeApplications                    
Created by         : Praveen kasana                   
Date               : 05 April 2006                    
Purpose            : To fetch the VIOLATION TYPES form MNT VIOLATION (WHERE PARENT =0 )    
Revison History    :                    
Used In            : Wolverine                      
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
CREATE   PROCEDURE Proc_GetViolationType                    
 @STATE_NAME varchar(20),                  
 @LOB_CODE varchar(5)=null                   
AS                    
BEGIN                    
--get StateName                  
Declare @STATE_ID int                  
select @STATE_ID=STATE_ID from MNT_COUNTRY_STATE_LIST                  
where STATE_NAME=@STATE_NAME                  
    
--get LOB ID    
Declare @LOB_ID int                  
select @LOB_ID=LOB from MNT_VIOLATIONS V left join MNT_LOB_MASTER L on  L.LOB_ID=V.LOB    
where LOB_CODE=@LOB_CODE     
    
            
Select VIOLATION_ID,VIOLATION_DES          
FROM MNT_VIOLATIONS WHERE VIOLATION_PARENT=0 and STATE=@STATE_ID                   
and LOB=@LOB_ID ORDER BY VIOLATION_DES FOR XML AUTO;                  
                   
END          
  



GO

