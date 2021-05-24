!function ($) {
  "use strict";

  $.disable_enable_button = {
    disable: function () {
      $(".btndisable").attr('disabled', true);
      var loading = document.getElementById('progress-loading');
      loading.style.display = "block";
    },

    enable: function () {
      $(".btndisable").attr('disabled', false);
      var loading = document.getElementById('progress-loading');
      loading.style.display = "none";
    }
  }

}(window.jQuery);


function toggleSearch() {
  $("._criteria").toggle(200, "linear");

  if ($("#toogle-search").find("> i").hasClass("fa-angle-double-up")) {
    $("#toogle-search").find("> i").removeClass("fa-angle-double-up");
    $("#toogle-search").find("> i").addClass("fa-angle-double-down");

    document.getElementById('hrsrc').style.display = 'none';
    // $("#tScrollBody").height(450);
    $("#toogle-search").prop('title', 'Show filter');
  }
  else {
    $("#toogle-search").find("> i").removeClass("fa-angle-double-down");
    $("#toogle-search").find("> i").addClass("fa-angle-double-up");

    document.getElementById('hrsrc').style.display = 'block';
    //$("#tScrollBody").height(325);
    $("#toogle-search").prop('title', 'Hide filter');
  }
  //document.getElementById("btnLostFocus").focus();
  $("#toogle-search").blur();
  //var text = $("#lnsearch-toggle").text();
  //$("#lnsearch-toggle").text(text == "More search criteria" ? "Less search criteria" : "More search criteria");
}


SWAL = {
    confirm: function(text) {
     
        var  swalWithBootstrapButtons = Swal.mixin({
          customClass: {
            confirmButton: 'btn btn-primary btn-lg',
            cancelButton: 'btn btn-danger btn-lg'
          },
          buttonsStyling: false
        });

        return swalWithBootstrapButtons.fire({
          title: 'Confirmation',
          text: text,
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Yes',
          cancelButtonText: 'No',
          reverseButtons: false
        });
      
    }
}
