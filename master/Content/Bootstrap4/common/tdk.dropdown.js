!function ($) {
  "use strict";

  $.dropdown = {
    populateAll: function () {
      $('select[data-dropdown-id]').each(function (i, obj) {
        populate(obj);
        setOnchange(obj);
      })
    },


    populate: function (obj, param) {
      var $this = $(obj);
      var dropdownId = $(obj).data('dropdown-id');
      var dataValue = $this.data('dropdown-static-param');
      //idvar dataText = $this.data('text');
      if (typeof param === 'undefined') {
        if (typeof dataValue === 'undefined') {
          param = ";";
        }
        else {
          var idValue = dataValue.split(';');
          param = '';
          for (var i = 0; i < idValue.length; i++) {
            param += idValue[i] + ';';
          }
          
        }
        
      }
      
      $.ajax({
        type: "POST",
        url: "/Common/Lookup/GetDropdown",
        async: false,
        data: JSON.stringify({ id: dropdownId, param: param}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
          $(obj).empty();
          $.each(data, function (index) {
            $(obj).append($("<option></option>").attr("value", data[index]['VALUE']).text(data[index]['DESCRIPTION'])
           // $(obj).append($("<option></option>").val(data[index]['VALUE']).html(data[index]['DESCRIPTION'])
            )
          });

          if ($(obj).hasClass('chosen-select')) {
            //$(obj).chosen(
            //  { allow_single_deselect: true, width: "100%" });
            $(obj).trigger("chosen:updated");
          }
        },
        error: function (err) {
          console.log(err);
        }
      });

      
    },

    //parent child combo
    setOnchange: function (obj) {
      if (typeof $(obj).data('dropdown-child') !== 'undefined') {
        var idChild = $(obj).data('dropdown-child')
        var idEl = $(idChild);
        
        if (typeof $(idEl).data('dropdown-parent') !== 'undefined') {

          $(obj).on('change', function () {
            var idParent = $(idEl).data('dropdown-parent');
            var idParents = idParent.split(';');
            var param = '';
            for (var i = 0; i < idParents.length; i++) {
              param += $(idParents[i]).val() +';' ;
            }
            if ($(idEl).prop('multiple')) {
              param = param.replace(/,/g, ';'); //added by fid.feri, handle multiselect parent-child
            }
            $.dropdown.populate($(idEl), param);
          });
        }
      }
    }
  }
}(window.jQuery);
