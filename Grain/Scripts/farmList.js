"use strict";

var farms = function () {
    var _deleteFarm = function(id) {
        var form = $("#FarmDeleteForm");
        var data = {
            __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', form).val(),
            Id: id
        };

        $.post(form.attr('action'), data).done(function() {
            setTimeout(window.myTools.CreateNotification("success", "Ферма удалена", 5000), 1000);

            window.location.reload();
        });
    }

    var _addDeleteHandlers = function () {
        $("[data-type='delete']").on("click", function () {

            var $farm = $(this).closest(".farm-data-item");
            var farmId = $farm.data("farmId");
            var action = '/Farms/DeletePartial/' + farmId;
            $.get(action, function (data) {
                $('#dialogContent').html(data);
                $("#confirm-delete").modal("show");

                $("#confirm-delete button.btn-ok").unbind("click");
                $("#confirm-delete button.btn-ok").click(function () {
                    $("#confirm-delete").modal("hide");
                    _deleteFarm(farmId);
                });
            });
        });
    }

    var init = function () {
        _addDeleteHandlers();
    }

    return {
        init: init
    }
}();