
function do_SearchMaterial(page) {
    $.disable_enable_button.disable();
    $.ajax({
        type: "POST",
        url: "/Common/MaterialLookup/SearchMaterialGrid",
        data: JSON.stringify({
            SelectionType: $("#selectionType").val(),
            IdTextReturn: $("#idTextReturn").val(),
            MAT_NO: $("#txtMaterialLookup").val(),
            page: page,
            pageSize: $('.page-size-Material').val() || 10
        }),
        contentType: "application/json; charset=utf-8",
        success: function (resultMessage) {
            var maxperpage = $('.page-size-Material').val() || 10;
            $('#datagridMaterial').html(resultMessage);
            $('.page-size-Material').val(maxperpage);
            $.disable_enable_button.enable();
        },
        error: function (resultMessage) {
            $.disable_enable_button.enable();
        }
    })
}

function do_LookupMaterial(type, idText, idUOM, idMatDecs, idContainer) {
    $.ajax({
        type: "POST",
        url: "/Common/MaterialLookup/SearchMaterial",
        data: JSON.stringify({
            MAT_NO: $(idText).val(),
            SelectionType: type,
            IdTextReturn: idText,
            IdUOMReturn: idUOM,
            idMatDescreturn: idMatDecs

        }),
        contentType: "application/json; charset=utf-8",
        success: function (resultMessage) {
            var maxperpage = $('.page-size-Material').val() || 10;
            $(idContainer).html(resultMessage);
            $('.page-size-Material').val(maxperpage);
            $.disable_enable_button.enable();
            $(idContainer).on('shown.bs.modal', function () {
                $("#txtMaterialLookup").focus();
            });
            $(idContainer).modal();

        },
        error: function (resultMessage) {
            $.disable_enable_button.enable();
        }
    })
}

function popupLookup_Selected(setValue) {

    var idText = $("#idTextReturn").val();
    var uom = $("#IdUOMReturn").val();
    var idDesc = $("#IdDescReturn").val(); //tambahan dari wot untuk kebutuhan menampilkan mat desc
    if ($("#selectionType").val() == 1) {
        var result = '';
        if (($(".popupLookup-checkbox-body:checked").length > 0) && (setValue == 'OK')) {
            for (i = 0; i < $(".popupLookup-checkbox-body").length; i++) {
                if ($(".popupLookup-checkbox-body")[i].checked)
                    result = result + ((result != '') ? ';' : '') + $(".popupLookup-checkbox-body")[i].dataset.value;
            }
            $(idText).val(result);
        }

        if (setValue == 'CS') {
            $(".popupLookup-checkbox-body").prop('checked', false);
            $(idText).val('');
            $(idDesc).val(''); //tambahan dari wot untuk kebutuhan clear mat desc
        }
    } else {
        if (setValue == 'OK') {

            var arrayValues = $('input[name=radioMatNo]:checked').val().split(',');

            console.log(arrayValues)
            $(idText).val(arrayValues[0]);
            $(uom).val(arrayValues[1]);
            $(idDesc).val(arrayValues[2]);//tambahan dari wot untuk kebutuhan menampilkan mat desc

        } else {
            $(idText).val('');
        }

    }
}
