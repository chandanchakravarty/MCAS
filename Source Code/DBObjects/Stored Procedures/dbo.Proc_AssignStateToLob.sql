IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AssignStateToLob]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AssignStateToLob]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                            
Proc Name       : dbo.Proc_AssignStateToLob                     
Created by      : Shafi                            
Date            : 23 Dec,2005                            
Purpose         : To assign the States To Lob.                            
Revison History :                            
Used In         : Wolverine                            
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
CREATE PROC Dbo.Proc_AssignStateToLob                           
(                        
 @LobId int,                  
 @StateId  nvarchar(100)                  
)              
              
AS             
Begin            
 declare @state int            
 declare @scount  int                      
 declare @sub_lob int            
 set @scount=2            
 if @StateId <>''  
  Begin  

 set @state = dbo.piece(@StateId,',',1)            
       
 WHILE @state is not null            
 BEGIN          
         
  INSERT INTO MNT_LOB_STATE(LOB_ID,STATE_ID,PARENT_LOB)                  
      VALUES(@LobId,@state,null)              
         
         
  INSERT INTO MNT_LOB_STATE(LOB_ID,STATE_ID,PARENT_LOB)        
  SELECT SUB_LOB_ID, @STATE, @LOBID           
  FROM MNT_SUB_LOB_MASTER         
  WHERE LOB_ID = @LOBID        
         
          
      SET @STATE=DBO.PIECE(@STATEID,',',@SCOUNT)            
      SET @SCOUNT=@SCOUNT+1            
 END    
END  
else  
Print 'Else'  
        
             
         
 --Update Coutry Set N For all            
 UPDATE MNT_COUNTRY_STATE_LIST SET IS_ACTIVE='N'            
             
 --Update Country Set Y For All Active States            
             
 Update mnt_country_state_list          
 set IS_ACTIVE='Y'         
 where state_id   in (SELECT DISTINCT STATE_ID FROM MNT_LOB_STATE)            
END         
  
  



GO

