

$(document).ready(initFunc);
var focusFlag = true;

function initFunc(e) {
    getTranslators();
    $('#btnSave').on('click', btnSave);
    $('#btnEditCancel').on('click', editCancel);
    $('#addNew').on('click', addNew);
    $('#btnDelete').on('click', btnDelete_Click);
    $('#btnDeleteConfirmed').on('click', btnDeleteConfirmed);
    $('#btnDeleteConfirmedCancel').on('click', btnDeleteConfirmedCancel);
   
    
    

}


function btnDelete_Click(e)
{
    $('#divConfirmation').dialog({ width: 300, resizable: false, title: "Are you sure?" });        
}



function btnSave(e) {

 
        $('#divEditTranslator').dialog("close");
        var translatorID = $('#translatorID').val();
        var name = $('#txtEditName').val();
        var language = $('#txtEditLanguage').val();
        var phone = $('#txtEditPhone').val();
        var email = $('#txtEditEmail').val();


        var uri = 'api/translators';
        var dataToSend = { translatorID: translatorID, name: name, language: language, phone: phone, email: email };
        $.ajax({
            type: "POST",
            url: uri,
            data: JSON.stringify(dataToSend),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: addEditTranslatorSuccess,
            error: function (msg, status) {
                alert("failed");
                // displayMessage("Category load failed!", "Red");
            }
        });

    }

    function addEditTranslatorSuccess() {
        clearFields();
        getTranslators();
    }

    function editCancel(e) {
        $('#divEditTranslator').dialog("close");
    }

    function btnDeleteConfirmedCancel(e) {        
        $('#divConfirmation').dialog("close");
    }

    function registerEditTranslator() {
        $('input[id*="btnTranslatorEdit_"]').unbind('click', editTranslator);

        $('input[id*="btnTranslatorEdit_"]').on('click', editTranslator);
    }


    //this works
    //$(document).ready(function () {
    //    var dataToSend = { 'obj': '' };
    //    $.ajax({
    //        type: "GET",
    //        url: uri2,
    //        data: JSON.stringify(dataToSend),
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: getTranslatorSuccess,
    //        error: function (msg, status) {
    //            displayMessage("Category load failed!", "Red");
    //        }
    //    });


    function getTranslators() {

        var uri = 'api/translators';
        var dataToSend = { 'obj': '' };
        $.ajax({
            type: "GET",
            url: uri,
            data: JSON.stringify(dataToSend),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: getTranslatorSuccess,
            error: function (msg, status) {
                displayMessage("Category load failed!", "Red");
            }
        });
    }

    function getTranslatorSuccess(msg, status) {

        var results = $.parseJSON(msg.JsonResults);

        if (results.length > 0) {
            var toSee = $('#translatorTableRow').render(results);
            $('#tbTranslator').html("");
            $('#tbTranslator').append(toSee);
            $("#tbTranslator tr:even").css("background-color", "#dedede");

            $("#tbTranslator tr:odd").css("background-color", "#ffffff");
            registerEditTranslator();
        }
        else {

        }
    }


    function btnDeleteConfirmed() {


        var boxes = $('input[id*=chk]:checked');
        for (var i = 0; i < boxes.length; i++) {
            var translatorContainer = boxes.parent().parent()[i];
            var translatorID = translatorContainer.id;

            deleteTranslator(translatorID);
        }
        
        $('#divConfirmation').dialog("close");
    }


    function deleteTranslator(translatorID) {

        var uri = 'api/translators?translatorID=' + translatorID;
        var dataToSend = { 'obj': translatorID };
        $.ajax({
            type: "Delete",
            url: uri,
            data: JSON.stringify(dataToSend),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: deleteTranslatorSuccess,
            error: function (msg, status) {
                alert("failed");
                // displayMessage("Category load failed!", "Red");
            }
        });
    }


    function deleteTranslatorSuccess() {
        getTranslators();
    }
    function deleteTranslatorFail() {
        alert("failed");
    }


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

    function addNew(e) {

        //var language = $('#language_' + id).text();
        //var phone = $('#phone_' + id).text();
        //var email = $('#email_' + id).text();
        $('#txtEditName').val();
        $('#txtEditLanguage').val();
        $('#txtEditPhone').val();
        $('#txtEditEmail').val();

        $('#divEditTranslator').dialog({ width: 600, resizable: false, title: "Add New" });

    }

    function editTranslator(e) {
        var element = e.target || e.srcElement;
        var parent = $(element).parent();
        parent = $(parent).siblings()[1];
        parent = $(parent).children()[0];
        var id = $(parent).attr("id");
        var index = id.indexOf('_') + 1;
        id = id.substr(index);

        var value = $(parent).text();
        $('#txtEditID').val(id);
        $('#txtEditName').val(value);

        var language = $('#language_' + id).text();
        var phone = $('#phone_' + id).text();
        var email = $('#email_' + id).text();

        $('#txtEditLanguage').val(language);
        $('#txtEditPhone').val(phone);
        $('#txtEditEmail').val(email);
        $('#translatorID').val(id);
        $('#divEditTranslator').dialog({ width: 600, resizable: false });

    }

    function clearFields()
    {
        $('#txtEditLanguage').val("");
        $('#txtEditPhone').val("");
        $('#txtEditEmail').val("");
        $('#translatorID').val("");
        $('#txtEditName').val("");
    }