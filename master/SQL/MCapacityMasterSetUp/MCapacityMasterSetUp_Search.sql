SELECT * FROM (
		SELECT ROW_NUMBER() OVER(ORDER BY A.COMPANY_CD asc) AS r,
		A.COMPANY_CD,
		B.PROCESS_CD,
		B.LINE_CD,
		A.SEQ_NO,
		A.HEIJUNKA_CD,
		A.CAPACITY,
		CONVERT(VARCHAR(MAX), A.TC_FROM, 111) TC_FROM,
		CONVERT(VARCHAR(MAX), A.TC_TO, 111) TC_TO,	
		A.CREATED_BY,
		CONVERT(VARCHAR(MAX), A.CREATED_DT, 111) CREATED_DT
	FROM TB_M_CAPACITY A 
	INNER JOIN TB_M_LINE B ON A.LINE_CD = B.LINE_CD AND A.COMPANY_CD = B.COMPANY_CD
	INNER JOIN TB_M_HEIJUNKA C ON A.HEIJUNKA_CD = C.HEIJUNKA_CD AND A.COMPANY_CD = C.COMPANY_CD
	WHERE 1=1
	AND ((@proccd is null OR @proccd = '') OR (B.PROCESS_CD like '%' +@proccd+'%' ))
	AND ((@linecd is null OR @linecd = '') OR (B.LINE_CD like '%' +@linecd+'%' ))
	AND ((@hjkcd is null OR @hjkcd = '') OR (C.HEIJUNKA_CD like '%' +@hjkcd+'%' ))

) tb where 1=1 and tb.r between @RowStart and @RowEnd