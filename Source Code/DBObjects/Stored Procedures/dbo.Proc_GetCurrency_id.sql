IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCurrency_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCurrency_id]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROC Dbo.Proc_GetCurrency_id        
------   ------------       -------------------------*/        
        
CREATE  PROC [dbo].[Proc_GetCurrency_id]     
(        
@CURRENCY_ID  int ,@CURR_DESC nvarchar(50)          
)        
AS        
BEGIN        

Select CURRENCY_ID,CURR_DESC  from  MNT_CURRENCY_MASTER with(nolock) where IS_ACTIVE='Y'
          
END        
      
GO

