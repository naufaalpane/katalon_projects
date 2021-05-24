select count(1) from TB_R_ORDER_SPEC_RIO B 
LEFT JOIN TB_R_ORDER_CONTROL_RIO A ON A.COMPANY_CD = B.COMPANY_CD AND A.RECEIVE_NO = B.RECEIVE_NO
LEFT JOIN TB_R_ORDER_MONTHLY C ON C.COMPANY_CD = B.COMPANY_CD AND C.RECEIVE_NO = B.RECEIVE_NO AND C.DETAIL_NO = B.DETAIL_NO
where 1=1
	  and ((@company_cd is null OR @company_cd = '') OR (A.COMPANY_CD = @company_cd ))
	  and ((@status_cd is null OR @status_cd = '') OR (A.STATUS_CD = @status_cd ))
	  and ((@importer_cd is null OR @importer_cd = '') OR (A.IMPORTER_CD = @importer_cd ))
	  and ((@exporter_cd is null OR @exporter_cd = '') OR (A.EXPORTER_CD = @exporter_cd ))
	  and ((@cfc is null OR @cfc = '') OR (A.CFC = @cfc ))
	  and ((@order_type is null OR @order_type = '') OR (A.ORD_TYPE = @order_type ))
	  and ((@pack_month is null OR @pack_month = '') OR (A.PACK_MONTH = @pack_month ))
	  and ((@re_export_cd is null OR @re_export_cd = '') OR (B.RE_EXPORT_CD = @re_export_cd ))
	  and ((@disable_flag is null OR @disable_flag = '') OR (A.DISABLE_FLAG = @disable_flag ))
	  and C.YYMM = @yymm