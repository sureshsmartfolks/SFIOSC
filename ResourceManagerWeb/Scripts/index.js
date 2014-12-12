var uri = 'api/products';
var uri2 = 'api/translators';

$(document).ready(function () {
    var dataToSend = { 'obj': '' };
    $.ajax({
        type: "GET",
        url: uri2,
        data: JSON.stringify(dataToSend),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: getTranslatorSuccess,
        error: function (msg, status) {
            displayMessage("Category load failed!", "Red");
        }
    });


    function getTranslatorSuccess(msg, status) {
        
        var results = $.parseJSON(msg.JsonResults);
        
        if (results.length > 0) {
            var toSee = $('#translatorTableRow').render(results);
            $('#tbTranslator').html("");
            $('#tbTranslator').append(toSee);
            //$('.popup').popupWindow({
            //    height: 500,
            //    width: 800,
            //    top: 50,
            //    left: 50
            //});
            //registerRotations();
        }
        else {

        }
    }
    // Send an AJAX request
    //$.getJSON(uri)
    //    .done(function (data) {
    //        // On success, 'data' contains a list of products.
    //        $.each(data, function (key, item) {
    //            // Add a list item for the product.
    //            $('<li>', { text: formatItem(item) }).appendTo($('#products'));
    //        });
    //    });


    //var $form = $("#myForm");
    //var url = $form.attr("action") + "?" + $form.serialize();
    //$("#" + id).html(url);

    //$.ajax({
    //    url: uri2,
    //    type: 'GET',
    //   // data: 'twitterUsername=jquery4u',
    //    success: function (data) {
    //        //called when successful
    //                $.each(data, function (key, item) {
    //                    // Add a list item for the product.
    //                    $('<li>', { text: formatTranslator(item) }).appendTo($('#translators'));
    //                });
    //        $('#translators').html(data);
    //    },
    //    error: function (e) {
    //        //called when there is an error
    //        //console.log(e.message);
    //    }
    //});


    //// Send an AJAX request
    //$.getJSON(uri2)
    //    .done(function (data) {
    //        // On success, 'data' contains a list of products.
    //        $.each(data, function (key, item) {
    //            // Add a list item for the product.
    //            $('<li>', { text: formatTranslator(item) }).appendTo($('#translators'));
    //        });
    //    });

});

function formatItem(item) {
    return item.Name + ': $' + item.Note;
}

function formatTranslator(item) {
    return item.Name + ': $' + item.Note;
}


function find() {
    var id = $('#prodId').val();
    $.getJSON(uri + '/' + id)
        .done(function (data) {
            $('#product').text(formatItem(data));
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#product').text('Error: ' + err);
        });
}
