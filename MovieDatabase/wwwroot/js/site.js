var container = $("#strings");
$("#addstring").click(function () {
    var index = container.children("input").length;
    var clone = $("#newstring").clone();
    clone.html($(clone).html().replace(/\[#\]/g, "[" + index + "]"));
    container.append(clone.html());
    container.children("input").last().focus();
});



function ValidateDelete() {
    if (confirm("Do you really want to delete this?"))
        document.forms[0].submit();
    else
        return false;
}