// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {

    let today = new Date();
    let dd = today.getDate();

    let mm = today.getMonth() + 1;
    const yyyy = today.getFullYear();
    if (dd < 10) {
        dd = `0${dd}`;
    }

    if (mm < 10) {
        mm = `0${mm}`;
    }
    today = `${dd}/${mm}/${yyyy}`;

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
            //console.log('Data received: ');
            //console.log(users);
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

    $('#post-feed').on('click', function () {
        var content = $('#feedContent').val();
        var feedUserID = $('#profileId').val();
        $.ajax({
            type: 'POST',
            url: '/Home/PostFeed',
            async: false,
            data: { user: feedUserID, feed: content},
            success: function (data) {
                $('#feedList').html(data);
            }
        });
    });



    "use strict";

    //var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

    ////Disable send button until connection is established
    //document.getElementById("sendButton").disabled = true;

    //connection.on("ReceiveMessage", function (user, sender, message) {
    //    if ($('#receiverInput').val() == user) {
    //        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    //        var encodedMsg = user + " says " + msg;
    //        $("#message").text(encodedMsg);
    //    }
        
    //});

    //connection.start().then(function () {
    //    document.getElementById("sendButton").disabled = false;
    //}).catch(function (err) {
    //    return console.error(err.toString());
    //});

    //document.getElementById("sendButton").addEventListener("click", function (event) {
    //    var user = document.getElementById("userInput").value;
    //    var sender = document.getElementById("receiverInput").value;
    //    var message = document.getElementById("messageInput").value;
    //    connection.invoke("SendMessage", user, sender, message).catch(function (err) {
    //        return console.error(err.toString());
    //    });
    //    event.preventDefault();
    //});

});

