function chTree() {
    var sub = $('#subject option:selected').val();
    var gra = $('#grade option:selected').val();
    var gen = $('#genre option:selected').val();
    var val = sub + ';' + gra + ';' + gen;
    $.ajax({
        url: "/Home/Index/" + val,
        type: "GET",
        success: function (result) {
            $('#main').html(result);

            $('#subject').val(sub).select = true;
            $('#grade').val(gra).select = true;
            $('#genre').val(gen).select = true;
        },
        error: function (response) {
            alert("Dop!");
        }
    });
}