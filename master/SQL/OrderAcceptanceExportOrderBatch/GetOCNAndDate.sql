select top 1 replace(convert(varchar(max),getdate(),120) , '-' , '/') as LOCALDATE 
, IMPORTERDCD + '/' + EXPORTERCD + '/' + PCMN + '/' + ODRTYPE [OCN]  from [TB_T_EXPORT_ORDER_ATTACHMENT]