var container = $('#strings');
$('#addstring').click(function () {
    var index = container.children('input').length;
    var clone = $('#newstring').clone();
    clone.html($(clone).html().replace(/\[#\]/g, '[' + index + ']'));
    container.append(clone.html());
    container.children('input').last().focus();
});