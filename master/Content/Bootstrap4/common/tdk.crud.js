!function ($) {
  "use strict";
  //revised for GPPSU
  $.crud = {

    doInitSelect: function () {
      $(".chosen-select").chosen(
        { allow_single_deselect: true, width: "100%" });
      $(".chosen-select").each(function (i, obj) {
        $.crud.doInitSelectSingle($(obj));
      });
      $('body').on('focus', '.chosen-container-single input', function () {
        if (!$(this).closest('.chosen-container').hasClass('chosen-container-active')) {
          $(this).closest('.chosen-container').prev().trigger('chosen:open');
        }
      });
     
    },

    doInitSelectSingle: function (obj) {
      $(obj).chosen(
        { allow_single_deselect: true, width: "100%" });
      if ($(obj).data('dropdown-id')) {
        $.dropdown.populate($(obj));
        $.dropdown.setOnchange($(obj));
      }
      if ($(obj).hasClass('mandatory')) {
        $(obj).siblings().find('a').addClass("mandatory");
      }
    },

    doInitDateRangePicker: function () {
      $('.date-range-picker').each(function (i, obj) {
        $.crud.doInitDateRangePickerSingle($(obj));
      });
     
    },

    doInitDateRangePickerSingle: function (obj) {
      $(obj).daterangepicker({
        showDropdowns: true,
        autoUpdateInput: false,
        'applyClass': 'btn-sm btn-success',
        'cancelClass': 'btn-sm btn-default',
        locale: {
          applyLabel: 'Apply',
          cancelLabel: 'Cancel',
          format: 'DD.MM.YYYY'
        }
      })

      $(obj).on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('DD.MM.YYYY') + ' - ' + picker.endDate.format('DD.MM.YYYY'));
      });

      $(obj).on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
      });
    },

    doInitDatePicker: function () {
      $('.date-picker').each(function (i, obj) {
        $.crud.doInitDatePickerSingle(obj);
      });
      
    },

    doInitDatePickerSingle: function (obj) {
      $(obj).daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        autoUpdateInput: true,
        autoApply: true,
        timePicker: false,
        locale: {
          format: 'DD.MM.YYYY'
        }
      },
        function (start, end, label) {
          $(obj).data('drp_empty', false); // means value was SET
        });

      $(obj).on('change input keyup', function () {
        if (!$(obj).val()) { // only if empty. Means value ERASED. If there is any value - we ignore it
          $(obj).data('drp_empty', true); // remember our empty state (until reset again in cb)
        }
      });

      $(obj).on('blur', function () {
        if ($(obj).data('drp_empty')) { // means we were erasing it
          $(obj).val(''); // so we force erasing it
        }
      });

      $(obj).val('');
    },

    doInitMonthPicker: function () {
      $('.month-picker').each(function (i, obj) {
        $.crud.doInitMonthPickerSingle(obj);
      });
     
    },

    doInitMonthPickerSingle: function (obj) {
      $(obj).daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        autoUpdateInput: true,
        autoApply: true,
        timePicker: false,
        locale: {
          format: 'MM.YYYY'
        }
      },
        function (start, end, label) {
          $(obj).data('drp_empty', false); // means value was SET
        }
      );

      $(obj).on('change input keyup', function () {
        if (!$(obj).val()) { // only if empty. Means value ERASED. If there is any value - we ignore it
          $(obj).data('drp_empty', true); // remember our empty state (until reset again in cb)
        }
      });

      $(obj).on('blur', function () {
        if ($(obj).data('drp_empty')) { // means we were erasing it
          $(obj).val(''); // so we force erasing it
        }
      });

      $(obj).val('');
    },

    doInitElement: function () {
      
      $.crud.doInitSelect();
      $.crud.doInitDatePicker();
      $.crud.doInitMonthPicker();
      $.crud.doInitDateRangePicker();
    },

    doInit: function () {
      //enabled all
      $('#form-mode').val('VIEW');

      $(':button.btn-std').removeClass('d-none');
      $(':input:not(:button)').prop('disabled', false);
      $('.chosen-select').prop('disabled', false).trigger('chosen:updated');

      //disabled/hide ADD EDIT
      $(':button.btn-std[data-enabled*="ADD"]').addClass('d-none');
      $(':input:not(:button)[data-enabled*="ADD"]').prop('disabled', true);
      $('.chosen-select[data-enabled*="ADD"]').prop('disabled', true).trigger('chosen:updated');

      $(':button.btn-std[data-enabled*="EDIT"]').addClass('d-none');
      $(':input:not(:button)[data-enabled*="EDIT"]').prop('disabled', true);
      $('.chosen-select[data-enabled*="EDIT"]').prop('disabled', true).trigger('chosen:updated');

      //enable VIEW
      $(':button.btn-std[data-enabled*="VIEW"]').removeClass('d-none');
      $(':input:not(:button)[data-enabled*="VIEW"]').prop('disabled', false);
      $('.chosen-select[data-enabled*="VIEW"]').prop('disabled', false).trigger('chosen:updated');
    },

    doSearch: function (url, page, dataName) {
      $.messagepanel.close();
      if (typeof dataName == 'undefined') {
        dataName = '';
      }
      var searchParameter = {};
      $('._search' + dataName).each(function (i, obj) {
        searchParameter[$(obj).data('name')] = $(obj).val();
      });
      searchParameter['page'] = page;
      searchParameter['pageSize'] = $('.page-size' + dataName).val() || 10;
      $.disable_enable_button.disable();
      $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(searchParameter),
        contentType: "application/json; charset=utf-8",
        success: function (resultMessage) {
          var maxperpage = $('.page-size' + dataName).val() || 10;
          $('#datagrid' + dataName).html(resultMessage);
          $('.page-size' + dataName).val(maxperpage);
          $.disable_enable_button.enable();
          $.crud.doInit();
        },
        error: function (resultMessage) {
          $.disable_enable_button.enable();
          $.messagepanel.showContent(resultMessage.responseText);
          $.crud.doInit();
        }
      });

    },

    doClear: function (url) {
      $.messagepanel.close();
      $('._search').each(function (i, item) {
        var element = $(item);
        var elementType = GetType(element[0]);
        switch (elementType) {
          case "HTMLInputElement":
            if (element.attr("type") === "text")
              element.val("");
            else if (element.attr("type") === "number")
              element.val(0);
            break;
          case "HTMLSelectElement":
            element.val(undefined);
            $(item).trigger("chosen:updated");
            break;
        }
      });

      if (url) {
        $.disable_enable_button.disable();
        $.ajax({
          type: "POST",
          url: url,
          success: function (resultMessage) {
            var maxperpage = $('.page-size').val() || 10;
            $('#datagrid').html(resultMessage);
            $('.page-size').val(maxperpage);
            $.disable_enable_button.enable();
            $.crud.doInit();
          },
          error: function (resultMessage) {
            $.disable_enable_button.enable();
            $.crud.doInit();
          }
        });
      }
    },

    doAdd: function () {
      $('#form-mode').val('ADD');
      $("#templaterow").show();
      $(".chosen-select").chosen();

      //$(':button').prop('disabled', true);
      $(':button.btn-std').addClass('d-none');
      $(':input:not(:button)').prop('disabled', true);
      $('.chosen-select').prop('disabled', true).trigger('chosen:updated');

      $("[id^=editlink]").addClass("_link-disabled");
      $("[id^=deletelink]").addClass("_link-disabled");

      $(':button.btn-std[data-enabled*="ADD"]').removeClass('d-none');
      $(':input:not(:button)[data-enabled*="ADD"]').prop('disabled', false);
      $(':input:not(:button)[data-enabled*="ADD"]').each(function (i, obj) {
       if ($(obj).hasClass('chosen-select')) {
          $.crud.doInitSelectSingle(obj);
          $(obj).prop('disabled', false).trigger('chosen:updated');
        }
        if ($(obj).hasClass('date-picker')) {
          $.crud.doInitDatePickerSingle(obj);
        }
        if ($(obj).hasClass('date-range-picker')) {
          $.crud.doInitDateRangePickerSingle(obj);
        }
        if ($(obj).hasClass('month-picker')) {
          $.crud.doInitMonthPickerSingle(obj);
        }
      });

      $('._add[data-mandatory*="ADD"]').each(function (i, obj) {
        if ($(obj).hasClass('chosen-select')) {
          $(obj).siblings().find('a').addClass("mandatory");
        } else {
          $(obj).addClass("mandatory");
        }
      });
    },

    doAddInline: function () {
      //TODO
      doAdd();
    },

    doEdit: function (dataName='') {

      if ($(".grid-checkbox-body:checked").length == 0) {
        $.messagepanel.show("MCSTSTD002E: A single record must be selected to execute Edit operation.");
      } else if ($(".grid-checkbox-body:checked").length > 1) {
        $.messagepanel.show("MCSTSTD002E: A single record must be selected to execute Edit operation.");
      } else {
        var trs = $(".grid-checkbox-body:checked").closest("tr"); //get tr elements of checked inputs
        var i = $.map(trs, function (tr) { return $(tr).index(); });
        $('#form-mode').val('EDIT');

        $(':button.btn-std').addClass('d-none');
        $(':input:not(:button)').prop('disabled', true);
        $('.chosen-select').prop('disabled', true).trigger('chosen:updated');

        $("[id^=editlink]").addClass("_link-disabled");
        $("[id^=deletelink]").addClass("_link-disabled");

        $(':button.btn-std[data-enabled*="EDIT"]').removeClass('d-none');
        $(':input:not(:button)[data-enabled*="EDIT"]').prop('disabled', false);
        $("#hiddenrow" + dataName).empty();
        $("#hiddenrow" + dataName).html($(trs).html());
        $(trs).empty();
        $(trs).html($("#templaterow" + dataName).html());
        $('#templaterow' + dataName).empty();

        $(trs).find('td').each(function (i, obj) {
          //$(trs): nth - child(2)
          var found = false;
          var tds = $('#hiddenrow' + dataName).find('td').eq(i);
          $(obj).find('input:text, input:radio, select,input:checkbox').each(function (j, obj2) {
            found = true;
            if ($(obj2).hasClass('chosen-select')) {
              $.crud.doInitSelectSingle($(obj2));
              $(obj2).val($(tds).text().trim()).trigger('chosen:updated');
              $(obj2).data('enabled').includes('EDIT') ? $(obj2).prop('disabled', false).trigger('chosen:updated') : $(obj2).prop('disabled', true).trigger('chosen:updated');
            }
            else if ($(obj2).hasClass('_add') || $(obj2).hasClass('_edit')) {
              $(obj2).val($(tds).text().trim());
              $(obj2).data('enabled').includes('EDIT') ? $(obj2).prop('disabled', false) : $(obj2).prop('disabled', true);
            } else {
              $(obj2).val($(tds).html().trim());
            }
          });

          if (!found) {
            /*Rahman Show Btn Grid 20200608*/
            if ($(tds)[0].className != "actBtnGrid") {
              $(obj).text($(tds).text().trim());
            } else {
              $(".actBtnEditDelete" + dataName).hide();
            }
          }
         
        });

        $('._add[data-mandatory*="EDIT"]').each(function(i, obj) {
          if ($(obj).hasClass('chosen-select')) {
            $(obj).siblings().find('a').addClass("mandatory");
          } else {
          $(obj).addClass("mandatory");
          }
        });
      };
    },

    doEditInline: function () {
      //TODO
      doEdit();
    },

    doCancel: function (url, dataname = '') {
      if ($('#form-mode').val() == 'ADD') {
        $.messagebox.show(
          "Cancel Add",
          "MCSTSTD011C: Are you sure you want to cancel the operation?",
          "INF",
          "CONFIRM",
          "$.crud.doSearch('" + url + "',1,'" + dataname + "')",
          ""
        );
      } else if ($('#form-mode').val() == 'EDIT') {
        $.messagebox.show(
          "Cancel Edit",
          "MCSTSTD011C: Are you sure you want to cancel the operation?",
          "INF",
          "CONFIRM",
          "$.crud.doSearch('" + url + "',1,'" + dataname + "')",
          ""
        );
      }
    },


    doSaveAdd: function (url, preValidations) {
      

      var dataMandatory = [];
      $('._add[data-mandatory*="ADD"]').each(function (i, obj) {
          dataMandatory.push('#' + $(obj).attr('id'));
      });

      //var isValid = $.crud.validateMandatory(dataMandatory);
      //if (!isValid) return;

      $.messagebox.show(
        "Save Data",
        "MCSTSTD007C: Are you sure you want to confirm all records?",
        "INF",
        "CONFIRM",
        "$.crud.doSaveAddProcess('" + url + "')",
        ""
      );
      

    },

    doSaveAddProcess(url) {
      var addParameter = {};
      $('._add').each(function (i, obj) {
        addParameter[$(obj).data('name')] = $(obj).val();
      });

      $.progressbox.show("Save Data", "Saving Data onprogress . . .");
      $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(addParameter),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
          $.progressbox.hide();
          doSearch(1);
          $.messagepanel.showContent(res);
        },

        error: function (data) {
          $.progressbox.hide();
          $.messagepanel.showContent(data.responseText);
        }
      });
      $.progressbox.hide();
    },
    

    doSaveEdit: function (url,moduleName = '') {
      var dataMandatory = [];
      $('._edit[data-mandatory*="EDIT"]').each(function (i, obj) {
        dataMandatory.push('#' + $(obj).attr('id'));
      });

      //var isValid = $.crud.validateMandatory(dataMandatory);
      //if (!isValid) return;

      $.messagebox.show(
        "Save Data",
        "MCSTSTD007C: Are you sure you want to confirm all records?",
        "INF",
        "CONFIRM",
        "$.crud.doSaveEditProcess('" + url + "','" + moduleName+"')",
        ""
      );
    },

    doSaveEditProcess(url, moduleName='') {
      var editParameter = {};
      $('._edit' + moduleName).each(function (i, obj) {
        editParameter[$(obj).data('name')] = $(obj).val();
      });
      var a = JSON.stringify(editParameter);
      $.progressbox.show("Save Data", "Saving Data onprogress . . .");
      $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(editParameter),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
          $.progressbox.hide();
          let searchFunction = window["doSearch" + moduleName];
          searchFunction(1);
          $.messagepanel.showContent(res);
        },

        error: function (data) {
          $.progressbox.hide();
          $.messagepanel.showContent(data.responseText);
        }
      });
      $.progressbox.hide();
    }
    ,
    doDelete: function (url) {
      if ($(".grid-checkbox-body:checked").length == 0) {
        $.messagepanel.show("MCSTSTD010E: A single record must be selected to execute Delete operation.");
        return;
      } else {
        $.messagebox.show(
          "Delete Data",
          "MCSTSTD014C: Are you sure you want to delete the record ?",
          "INF",
          "CONFIRM",
          "$.crud.doDeleteSelected('" + url + "')",
          "$.crud.doClearCheckBox()"
        );
        
      }
    },

    doClearCheckBox: function () {
      $("#tblDetail tbody .isrow").each(function (i, row) {
        $(row).find(".grid-checkbox-body").prop("checked", false);
      });
    },

    doDeleteSelected: function (url) {
      var deletedKeys = [];
      $(".grid-checkbox-body").each(function (i, obj) {
        if (obj.id != null && obj.id.length > 0 && obj.checked) {
          var deletedKey = {};
          var data = $(obj).data();
          for (var key in data) {
            deletedKey[key] = data[key];
          }
          deletedKeys.push(deletedKey);
        }
      }
      );
      $.progressbox.show("Delete Data", "Delete Data onprogress . . .");
      $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(deletedKeys),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
          $.progressbox.hide();
          doSearch(1);
          $.messagepanel.showContent(res);
        },
        error: function (data) {
        $.progressbox.hide();
        $.messagepanel.showContent(data.responseText);
       
      }
      });
      $.progressbox.hide();
    },

    validateMandatory: function (idList, predicate) {
      if (predicate && GetType(predicate) !== "Function")
        throw new InvalidOperationException("predicate must be a function.");

      var mandatoryEmptyList = [];
      for (var i = 0; i < idList.length; i++) {
        var $this = $(idList[i]);
        var $thisElement = $this;
        var $thisValue = $this.valOrDefault();

        if ($this.hasClass("lookup")) {
          $thisElement = $this.find("[id^=txtlookup]");
          $thisValue = $thisElement.valOrDefault();
        }

        var isNotValid = predicate ? !predicate($thisValue) : $thisValue === "";//|| $thisValue === "0";
        if (isNotValid) {
          if ($thisElement.hasClass('chosen-select')) {
            $thisElement.siblings().find('a').addClass("mandatory-empty");
          } else {
            $thisElement.addClass("mandatory-empty");
          }
          mandatoryEmptyList.push($thisElement);
        }
      }

      if (mandatoryEmptyList.length > 0) {
        var mandatoryTimer = setTimeout(function () {
          for (var i = 0; i < mandatoryEmptyList.length; i++) {
            if ($(mandatoryEmptyList[i]).hasClass('chosen-select')) {
              $(mandatoryEmptyList[i]).siblings().find('a').removeClass("mandatory-empty");
            } else {
              $(mandatoryEmptyList[i]).removeClass("mandatory-empty");
            }
          }
          clearTimeout(mandatoryTimer);
        }, 10000);
      }
      return mandatoryEmptyList.length === 0;
    },

    displaymessagebox: {
      show: function (displayMessageId, type, param) {
        $.ajax({
          type: "POST",
          url: "/Common/GetMessage",
          async: false,
          data: {
            messageId: displayMessageId,
            messageParam: param
          },
          success: function (data) {
            type = type || displayMessageId.substring(0, 3);
            $.messagebox.show(type, data, type);
          },
          error: function (data) {
            $.messagebox.hide();
            console.log(data.responseText);
          }
        });
      }
    }
  }

}(window.jQuery);
