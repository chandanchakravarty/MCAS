if not exists(select top 1* from MNT_TableDesc where TableName='CLM_ReserveSummary')
begin
INSERT INTO [MNT_TableDesc]([TableName],[TableDesc],[Type]) VALUES ('CLM_ReserveSummary','Reserve','Claims')
end

if not exists(select top 1* from MNT_TableDesc where TableName='CLM_MandateSummary')
begin
INSERT INTO [MNT_TableDesc]([TableName],[TableDesc],[Type]) VALUES ('CLM_MandateSummary','Mandate','Claims')
end

if not exists(select top 1* from MNT_TableDesc where TableName='CLM_PaymentSummary')
begin
INSERT INTO [MNT_TableDesc]([TableName],[TableDesc],[Type]) VALUES ('CLM_PaymentSummary','Payment','Claims')
end


if not exists(select top 1* from MNT_TableDesc where TableName='CLM_ReserveDetails')
begin
INSERT INTO [MNT_TableDesc]([TableName],[TableDesc],[Type]) VALUES ('CLM_ReserveDetails','Reserve Details','Claims')
end

if not exists(select top 1* from MNT_TableDesc where TableName='CLM_MandateDetails')
begin
INSERT INTO [MNT_TableDesc]([TableName],[TableDesc],[Type]) VALUES ('CLM_MandateDetails','Mandate Details','Claims')
end

if not exists(select top 1* from MNT_TableDesc where TableName='CLM_PaymentDetails')
begin
INSERT INTO [MNT_TableDesc]([TableName],[TableDesc],[Type]) VALUES ('CLM_PaymentDetails','Payment Details','Claims')
end