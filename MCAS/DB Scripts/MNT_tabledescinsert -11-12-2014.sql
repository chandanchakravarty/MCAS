

IF NOT EXISTS (SELECT 1 FROM MNT_TableDesc WITH(NOLOCK) WHERE TableName='CLM_ServiceProvider')
BEGIN
INSERT INTO .[MNT_TableDesc]([TableName],[TableDesc],[Type])VALUES('CLM_ServiceProvider','Service Provider','Claims')
End

 
IF NOT EXISTS (SELECT 1 FROM MNT_TableDesc WITH(NOLOCK) WHERE TableName='CLM_Claim')
BEGIN
INSERT INTO .[MNT_TableDesc]([TableName],[TableDesc],[Type])VALUES('CLM_Claim','Claim','Claims')
End
 

IF NOT EXISTS (SELECT 1 FROM MNT_TableDesc WITH(NOLOCK) WHERE TableName='CLM_ServiceProvider')
BEGIN
INSERT INTO .[MNT_TableDesc]([TableName],[TableDesc],[Type])VALUES('CLM_ServiceProvider','Service Provider','Claims')
End


IF NOT EXISTS (SELECT 1 FROM MNT_TableDesc WITH(NOLOCK) WHERE TableName='MNT_interchange')
BEGIN
INSERT INTO .[MNT_TableDesc]([TableName],[TableDesc],[Type])VALUES('MNT_interchange','Interchange Master','System Admin')
End


IF NOT EXISTS (SELECT 1 FROM MNT_TableDesc WITH(NOLOCK) WHERE TableName='MNT_Deductible')
BEGIN
INSERT INTO .[MNT_TableDesc]([TableName],[TableDesc],[Type])VALUES('MNT_Deductible','Deductible Master','System Admin')
End



IF NOT EXISTS (SELECT 1 FROM MNT_TableDesc WITH(NOLOCK) WHERE TableName='CLM_Reserve')
BEGIN
INSERT INTO .[MNT_TableDesc]([TableName],[TableDesc],[Type])VALUES('CLM_Reserve','Reserve','Claims')
End