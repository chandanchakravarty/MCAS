-- =============================================
-- Script Template
-- =============================================
If Exists(SELECT 1 FROM MNT_TEMPLATE_MASTER WHERE parentId=21)
 Begin
    Update MNT_TEMPLATE_MASTER Set OutPutFormat='WORD' where parentId=21
 End



