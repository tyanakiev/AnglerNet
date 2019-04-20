// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {

    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });

    var users;

    $.ajax({
        type: 'GET',
        url: '/Home/ReturnAllUsers',
        contentType: 'json',
        async: false,
        success: function (users) {
            console.log('Data received: ');
            console.log(users);
            var friends = new Bloodhound({
                datumTokenizer: Bloodhound.tokenizers.obj.whitespace('username'),
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                local: users
            });
            friends.initialize();
            $('#user_search  .typeahead').typeahead(
                {
                    hint: true,
                    highlight: true,
                    minLength: 1,
                },
                {
                    name: 'friends',
                    displayKey: 'username',
                    source: friends.ttAdapter(),
                    templates: {
                        empty: 'not found', //optional
                        suggestion: function (el) { return '<p><img src="' + el.pictureUrl + '" class="search-img"/><span class="search-name">' + el.username + '</span></p>' }
                    }
                }
            );
        }
    });

    

});

