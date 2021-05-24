var selected_material_lookup = [];

function doSearchMaterial(page) {
  $.disable_enable_button.disable();
  $.ajax({
    type: "POST",
    url: "/Common/Lookup/SearchMaterialGrid",
    data: JSON.stringify({
      SelectionType: $("#selectionType").val(),
      IdTextReturn: $("#idTextReturn").val(),
      MAT_NO: $("#txtMaterialLookup").val(),
      page: page,
      pageSize: $('#cbodisplayMaterial').val() || 10
    }),
    contentType: "application/json; charset=utf-8",
    success: function (resultMessage) {
      var maxperpage = $('#cbodisplayMaterial').val() || 10;
      $('#datagridMaterial').html(resultMessage);
      $('#cbodisplayMaterial').val(maxperpage);
      $.disable_enable_button.enable();
      selected_material_lookup.forEach(function (item, index) {
        if ($('#cblookupmatno-' + item)) {
          $('#cblookupmatno-' + item).prop('checked', true);
        }
      });
    },
    error: function (resultMessage) {
      $.disable_enable_button.enable();
    }
  })
}

function populate_matno(matno) {
  const index = selected_material_lookup.indexOf(matno);
  if ($('#cblookupmatno-' + matno).prop('checked') == true) {
    if (index < 0) {
      selected_material_lookup.push(matno);
    }
  }
  else {
    if (index > -1) {
      selected_material_lookup.splice(index, 1);
    }
  }
}

function doLookupMaterial(type, idText,idUOM, idContainer) {
  $.ajax({
    type: "POST",
    url: "/Common/Lookup/SearchMaterial",
    data: JSON.stringify({
      MAT_NO: $(idText).val(),
      SelectionType: type,
      IdTextReturn: idText,
      IdUOMReturn: idUOM
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
      doSearchMaterial(1);
    },
    error: function (resultMessage) {
      $.disable_enable_button.enable();
    }
  })
}

  function popupLookupSelected(setValue) {
    var idText = $("#idTextReturn").val();
    var uom = $("#IdUOMReturn").val();
    if ($("#selectionType").val() == 1) {
      var result = '';
      if (setValue == 'OK') {
        for (i = 0; i < selected_material_lookup.length; i++) {
            result = result + ((result != '') ? ';' : '') + selected_material_lookup[i];
        }
        $(idText).val(result);
      }

      if (setValue == 'CS') {
        $(".popupLookup-checkbox-body").prop('checked', false);
        $(idText).val('');
      }
    } else {
      if (setValue == 'OK') {
        var arrayValues = $('input[name=radioMatNo]:checked').val().split(',');
        $(idText).val(arrayValues[0]);
        $(uom).val(arrayValues[1]);

      } else {
        $(idText).val('');
      }

    }
  }

function doClearMaterial() {

  $('#txtMaterialLookup').val('');
  selected_material_lookup = [];
}
