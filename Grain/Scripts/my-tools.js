window.myTools = {};

window.myTools.CreateNotification = function(type, text, delay) {
    var $container = $("div.sg-notification");
    var $span = $container.find("span.notification-text");

    $container.addClass("sg-notification " + type);
    $span.text(text);

    setTimeout(function() {
        $container.hide("slow");
    }, delay);
};