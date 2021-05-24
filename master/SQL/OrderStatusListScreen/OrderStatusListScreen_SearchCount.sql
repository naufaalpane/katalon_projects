SELECT  count(1)
FROM TB_R_ORDER_CONTROL A
INNER JOIN TB_M_DESTINATION B ON A.IMPORTER_CD = B.DEST_CD AND B.COMPANY_CD = A.COMPANY_CD
INNER JOIN TB_R_ORDER_SPEC C ON A.RECEIVE_NO = C.RECEIVE_NO AND A.COMPANY_CD = C.COMPANY_CD

where 1=1
		and  ((@STATUS_CD is null OR @STATUS_CD = '') OR (A.STATUS_CD like '%' +@STATUS_CD+'%' ))
		and  ((@IMPORTER_CD is null OR @IMPORTER_CD = '') OR (A.IMPORTER_CD like '%' +@IMPORTER_CD+'%' ))
		and  ((@EXPORTER_CD is null OR @EXPORTER_CD = '') OR (A.EXPORTER_CD like '%' +@EXPORTER_CD+'%' ))
		and  ((@ORD_TYPE is null OR @ORD_TYPE = '') OR (A.ORD_TYPE like '%' +@ORD_TYPE+'%' ))
		and  ((@PACK_MONTH is null OR @PACK_MONTH = '') OR (A.PACK_MONTH like '%' +@PACK_MONTH+'%' ))
		and  ((@CFC is null OR @CFC = '') OR (A.CFC like '%' +@CFC+'%' ))
