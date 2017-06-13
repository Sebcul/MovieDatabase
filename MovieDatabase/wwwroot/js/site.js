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


$("#saveMovieButton").click(function () {
    if ($("#movieTitle").val() !== "") {

        $.ajax({
            method: 'POST',
            url: '/Movie/EditTitleAndProductionYear',
            data: {
                movieId: $("#movieId").val(),
                movieTitle: $("#movieTitle").val(),
                movieProductionYear: $("#movieProductionYear").val()
    }
        });
    } else {
        alert("Input field can not be empty!");
    }
});

$("#removeActorButton").click(function () {
        $.ajax({
            method: 'POST',
            url: '/Movie/RemoveActor',
            data: {
                movieId: $("#movieId").val(),
                actorId: $("#movieActors").val()
            }
    });
        var value = $("#movieActors").val();
    $('#movieActors option[value="' + value + '"]').remove();
});

$(".removeRatingButton").click(function () {
    var id = $(this).attr('data-id');
    $.ajax({
        method: 'POST',
        url: '/Movie/RemoveRating',
        data: {
            movieId: $("#movieId").val(),
            ratingId: id
        }
    });

    $("#reviewBox" + id).last().addClass("hidden");
});