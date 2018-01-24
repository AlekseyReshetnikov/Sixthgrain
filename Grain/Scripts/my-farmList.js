"use strict";

var farms = function () {

    var buildElement = function (tagName, props) {
        var element = document.createElement(tagName);
        for (var propName in props) element[propName] = props[propName];
        return element;
    }

    var submit = function (link, props) {
        var form = buildElement('form', {
            method: 'post',
            action: link
        });
        for (var propName in props) form.appendChild(
            buildElement('input', {
                type: 'hidden',
                name: propName,
                value: props[propName]
            })
        );
        form.style.display = 'none';
        document.body.appendChild(form);
        form.submit();
    }


    var _deleteFarm = function (id) {
        var form = $("#FarmDeleteForm");
        var data = {
            __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', form).val(),
            Id: id
        };
        // Отправка формы
        // куда
        // post параметры
        submit(form.attr('action'), data);
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