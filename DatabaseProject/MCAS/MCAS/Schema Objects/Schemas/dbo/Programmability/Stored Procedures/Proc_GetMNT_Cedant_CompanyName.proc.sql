CREATE PROCEDURE [dbo].[Proc_GetMNT_Cedant_CompanyName]
	@w_InsurerType [char](1),
	@w_PartyTypeId [nvarchar](10),  
	@w_Status nvarchar(10)     
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;
IF OBJECT_ID('tempdb..#mytemptable') IS NOT NULL
DROP TABLE #mytemptable
CREATE TABLE #mytemptable(
CedantId int not null,
CedantName nvarchar(200))

BEGIN    
    
IF @w_PartyTypeId = '1'    
Begin    
IF @w_Status = 'Select'  
Begin  
Insert INTO #mytemptable (CedantId,CedantName) select CedantId ,CedantName from MNT_Cedant where InsurerType in(@w_InsurerType,'3')      
and (EffectiveTo is null OR Convert(Date,EffectiveTo,103) >= Convert(Date,GETDATE(),103)) --and Status != 0     
End  
Else  
Begin  
Insert INTO #mytemptable (CedantId,CedantName) select CedantId ,CedantName from MNT_Cedant where InsurerType in(@w_InsurerType,'3') and Status != 0 and (EffectiveTo is null OR Convert(Date,EffectiveTo,103) >= Convert(Date,GETDATE(),103))   
End  
END    
    
IF @w_PartyTypeId = '2'    
Begin    
IF @w_Status = 'Select'  
Begin  
Insert INTO #mytemptable (CedantId,CedantName) select AdjusterId,AdjusterName from MNT_Adjusters where AdjusterCode Like 'SVY%' and InsurerType in (@w_InsurerType,'3') and (EffectiveTo is null OR Convert(Date,EffectiveTo,103) >= Convert(Date,GETDATE(),103
)) --and Status = 'Active'     
End  
Else  
Begin  
Insert INTO #mytemptable (CedantId,CedantName) select AdjusterId,AdjusterName from MNT_Adjusters where AdjusterCode Like 'SVY%' and InsurerType in (@w_InsurerType,'3') and Status = 'Active' and (EffectiveTo is null OR Convert(Date,EffectiveTo,103) >= Convert(Date,GETDATE(),103))  
End  
END    
    
IF @w_PartyTypeId = '3'    
Begin    
IF @w_Status = 'Select'  
Begin  
Insert INTO #mytemptable (CedantId,CedantName) select AdjusterId,AdjusterName from MNT_Adjusters where AdjusterCode Like 'SOL%' and InsurerType in(@w_InsurerType,'3') and (EffectiveTo is null OR Convert(Date,EffectiveTo,103) >= Convert(Date,GETDATE(),103)
) --and Status = 'Active'    
End  
Else  
Begin  
Insert INTO #mytemptable (CedantId,CedantName) select AdjusterId,AdjusterName from MNT_Adjusters where AdjusterCode Like 'SOL%' and InsurerType in(@w_InsurerType,'3') and Status = 'Active' and (EffectiveTo is null OR Convert(Date,EffectiveTo,103) >= Convert(Date,GETDATE(),103))  
End  
END    
    
IF @w_PartyTypeId = '4'    
Begin   
IF @w_Status = 'Select'  
Begin   
Insert INTO #mytemptable (CedantId,CedantName) select DepotId,CompanyName from MNT_DepotMaster    
 where WorkShopType in(@w_InsurerType,'3') and (EffectiveTo is null OR Convert(Date,EffectiveTo,103) >= Convert(Date,GETDATE(),103)) --and Status != 0   
End     
Else  
Begin  
Insert INTO #mytemptable (CedantId,CedantName) select DepotId,CompanyName from MNT_DepotMaster    
 where WorkShopType in(@w_InsurerType,'3') and Status != 0 and (EffectiveTo is null OR Convert(Date,EffectiveTo,103) >= Convert(Date,GETDATE(),103))  
End  
END    
    
select CedantId,CedantName from #mytemptable    
    
END    