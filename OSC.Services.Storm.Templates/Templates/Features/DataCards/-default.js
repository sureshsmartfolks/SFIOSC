//@ sourceURL=DataCards.js

var MasterProjectUrl = MasterProjectUrl;
//var searchServiceUrl = MasterSearchServiceUrl;

var pageUrl = "Index.html";

var popUpEnabled = true;
var categoryVector = "";
var PageCount = 1;
var ResultsPerPage = 6;

var categoryLimit = 2;
var PageAmount = -1;
var orderByDirection = -1;
var orderByColumns = "";
var isFullscreen = false;
var oldheight = 0;
var oldwidth = 0;

filtersEnabled = false;
searchEnabled = false;
commandsEnabled = false;
searchMode = false;


$.ajaxPrefilter(function (options) {
    options.headers = { 'Authorization': 'Bearer ' + readCookie('CCAuth') };
});




//$(document).ready(initDataCards);

function initDataCards(e) {


    $('#lstbTags').on('change', listBox_Click);
    $('#btnPrevPage').on('click', movePage);
    $('#btnNextPage').on('click', movePage);
    $('#btnFirstPage').on('click', movePage);
    $('#btnLastPage').on('click', movePage);
    $('#ddlResultsPerPage').on('change', pageResultChange);
    $('#lbCurrentPage').text(PageCount);
    //$('#CardHolderTemplate input[type=image]').on('click', orderByClick);
    //$('#CardHolderTemplate span[id*="spanToggle"]').on('click', orderByClick);

    $('#btnSortingClear').on('click', sortingClear);
    $('#btnAdvanceFilter').on('click', advanceFilter);

    //$('#btnStartDate').on('click', datePickerCall);
    //$('#btnEndDate').on('click', datePickerCall);

    $(function () { $('#btnStartDate').datepicker(); });
    $(function () { $('#btnEndDate').datepicker(); });

    $('#btnCloseAdvanceFilter').on('click', closeAdvanceFilter);

    $('hiddenDiv').toggle(false);

    categoryVector = new Array();
    Array.prototype.remove = function (from, to) {
        var rest = this.slice((to || from) + 1 || this.length);
        this.length = from < 0 ? this.length + from : from;
        return this.push.apply(this, rest);
    };
    orderByColumns = new Array();
    initCard();

}
function displayMessage(message, color) {
    $('#divMessage').html("");
    $('#divMessage').append(message);
    $('#divMessage').css("color", color);
    $('#divMessage').dialog();
}

function advanceFilter(e) {
    $('#divAdvanceFilter').dialog();
}
function datePickerCall(e) {
    var element = e.target || e.srcElement;
    var id = $(element).attr('id');
    $('#' + id).datepicker();
}
function closeAdvanceFilter(e) {
    $('#divAdvanceFilter').dialog('close');
}
//function orderByClick(e) {
//    var element = e.target || e.srcElement;
//    var id = $(element).attr('id');
//    var columnName = "";
//    var columnDirection = "";
//    var remove = false;

//    if (id.indexOf("spanToggle") > -1) {
//        var color = $(element).css("backgroundColor");
//        if (color == "transparent" || color == "rgb(0, 0, 0)" || color == "rgba(0, 0, 0, 0)") {
//            $(element).css("background-color", "yellow");
//        }
//        else {
//            $(element).css("backgroundColor", "transparent");
//            remove = true;
//        }
//        columnName = $(element).text();
//        var image = $(element).siblings()[0];
//        if ($(image).attr('src').indexOf('sort-up-icon') > -1) {
//            columnDirection = "ASC";
//        }
//        else {
//            columnDirection = "DESC";
//        }
//    }
//    else {
//        var src = $(element).attr('src');
//        if (src.indexOf("sort-down-icon") > -1) {
//            $(element).attr('src', 'Images/sort-up-icon_20.png');
//            columnDirection = "ASC";
//        }
//        else {
//            $(element).attr('src', 'Images/sort-down-icon_10.png');
//            columnDirection = "DESC";
//        }
//        var column = $(element).siblings()[0];
//        columnName = $(column).text();

//        var color = $(column).css("backgroundColor");
//        if (color == "transparent" || color == "rgb(0, 0, 0)" || color == "rgba(0, 0, 0, 0)") {
//            $(column).css("background-color", "yellow");
//        }
//    }
//    var found = false;
//    for (var index = 0; index < orderByColumns.length; ++index) {
//        if (columnName == orderByColumns[index].ColumnName) {
//            if (remove) {
//                orderByColumns.splice(index, 1);
//                element = $(element).siblings()[0];
//                $(element).attr('src', 'Images/sort-down-icon_10.png');
//            }
//            else {
//                orderByColumns[index].ColumnDirection = columnDirection;
//            }
//            found = true;
//            break;
//        }
//    }
//    if (!found && !remove) {
//        var obj = new Object();
//        obj.ColumnName = columnName;
//        obj.ColumnDirection = columnDirection;
//        orderByColumns.push(obj);
//    }
//    if (orderByColumns.length > 0) {
//        var sortDisplay = "";
//        for (var index = 0; index < orderByColumns.length; ++index) {
//            sortDisplay += orderByColumns[index].ColumnName + " " + orderByColumns[index].ColumnDirection + ", ";
//        }
//        sortDisplay = sortDisplay.substr(0, sortDisplay.length - 2);
//        $('#lbSortingDisplay').text(sortDisplay);
//        $('#btnSortingClear').toggle(true);
//    }
//    else {
//        $('#lbSortingDisplay').text("");
//        $('#btnSortingClear').toggle(false);
//    }
//    searchClick(null, null);
//}
function sortingClear(e) {
    for (var index = 0; index < orderByColumns.length;) {
        orderByColumns.pop();
    }
    var headers = $('#CardHolderTemplate th span[id*="spanToggle"]');
    for (var index = 0; index < headers.length; ++index) {
        var element = $(headers)[index];
        var color = $(element).css("backgroundColor");

        if (color != "transparent" || color != "rgb(0, 0, 0)" || color != "rgba(0, 0, 0, 0)") {
            $(element).css("background-color", "transparent");
            element = $(element).siblings()[0];
            $(element).attr('src', 'Images/sort-down-icon_10.png');
        }
    }
    $('#lbSortingDisplay').text("");
    $('#btnSortingClear').toggle(false);
    searchClick(null, null);
}


function pageResultChange(e) {
    var changedResultValue = $('#ddlResultsPerPage').val();
    ResultsPerPage = changedResultValue;
    PageCount = 1;
    $('#lbCurrentPage').text(PageCount);


    if (searchMode && searchEnabled) {
        var val = $('#txtSearchFilter').val();
        search(val);

    }
    else if (filtersEnabled) {
        if (filtersChecked()) {
            filterResults();
        }
        else {
            getCurrentCardPage();
        }
    }
    else {
        getCurrentCardPage();
    }


}
function movePage(e) {
    var element = e.target || e.srcElement;
    var id = element.id;
    var currentPageCount = $('#lbCurrentPage').text();
    currentPageCount = parseInt(currentPageCount);
    if (id.indexOf("Prev") > -1) {
        if (currentPageCount != 1) {
            --currentPageCount;
            if ($('#lbCurrentPage').text().indexOf('/') > -1) {
                var text = $('#lbCurrentPage').text();
                text = text.substr(text.indexOf('/'));
                text = currentPageCount + text;
                $('#lbCurrentPage').text(text);
            }
            else {
                $('#lbCurrentPage').text(currentPageCount);
            }
            PageCount = currentPageCount;
            if (searchMode && searchEnabled) {
                var val = $('#txtSearchFilter').val();
                search(val);
            }
            else if (filtersEnabled) {
                if (filtersChecked()) {
                    filterResults();
                }
                else {
                    getCurrentCardPage();
                }
            }
            else {
                getCurrentCardPage();
            }
        }
        else {
            //TODO: do nothing as the page count cannot go below 1 if it is already 1
        }
    }
    else if (id.indexOf('Next') > -1) {
        if (currentPageCount < PageAmount) {
            ++currentPageCount;
            if ($('#lbCurrentPage').text().indexOf('/') > -1) {
                var text = $('#lbCurrentPage').text();
                text = text.substr(text.indexOf('/'));
                text = currentPageCount + text;
                $('#lbCurrentPage').text(text);
            }
            else {
                $('#lbCurrentPage').text(currentPageCount);
            }
            PageCount = currentPageCount;
            if (searchMode && searchEnabled) {
                var val = $('#txtSearchFilter').val();
                search(val);
            }
            else if (filtersEnabled) {
                if (filtersChecked()) {
                    filterResults();
                }
                else {
                    getCurrentCardPage();
                }
            }
            else {
                getCurrentCardPage();
            }
        }
    }
    else if (id.indexOf('First') > -1) {
        currentPageCount = 1;
        if ($('#lbCurrentPage').text().indexOf('/') > -1) {
            var text = $('#lbCurrentPage').text();
            text = text.substr(text.indexOf('/'));
            text = currentPageCount + text;
            $('#lbCurrentPage').text(text);
        }
        else {
            $('#lbCurrentPage').text(currentPageCount);
        }
        PageCount = currentPageCount;
        if (searchMode && searchEnabled) {
            var val = $('#txtSearchFilter').val();
            search(val);
        }
        else if (filtersEnabled) {
            if (filtersChecked()) {
                filterResults();
            }
            else {
                getCurrentCardPage();
            }
        }
        else {
            getCurrentCardPage();
        }
    }
    else {

        //currentPageCount = 1;
        var text = $('#lbCurrentPage').text();
        var lastPage = parseInt(text.substr(text.indexOf('/') + 1));
        text = lastPage + text.substr(text.indexOf('/'));
        $('#lbCurrentPage').text(text);

        PageCount = lastPage;
        if (searchMode && searchEnabled) {
            var val = $('#txtSearchFilter').val();
            search(val);
        }
        else if (filtersEnabled) {
            if (filtersChecked()) {
                filterResults();
            }
            else {
                getCurrentCardPage();
            }
        }
        else {
            getCurrentCardPage();
        }
    }
}
function getCurrentCardPage() {
    var CardObj = ProjectTransferObject();
    CardObj.PageCount = PageCount;
    CardObj.ResultsPerPage = ResultsPerPage;
    for (var index = 0; index < orderByColumns.length; ++index) {
        CardObj.OrderBy += orderByColumns[index].ColumnName + ":" + orderByColumns[index].ColumnDirection + ",";
    }
    if (orderByColumns.length > 0) {
        CardObj.OrderBy = CardObj.OrderBy.substr(0, CardObj.OrderBy.lastIndexOf(','));
    }
    var funcUrl = MasterProjectUrl + "/getAllCardsWithCategoryInformation";
    var funcUrl = MasterProjectUrl + "/getAllProjects";
    var dataToSend = CardObj;

    $.ajax({
        type: "GET",
        url: funcUrl,
        //data: JSON.stringify(dataToSend),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: initCardSuccess,
        error: initCardError
    });
   // $.proxies.projectmanagement.getallprojects(CardObj).success(initCardSuccess);
}




function initCard() {
    //application/soap+xml; charset=utf-8
    //application/json; charset=utf-8
    if (searchEnabled) {
        if ($('#txtSearchFilter').val() != "") {
            searchClick(null);
        }
        else {
            getCurrentCardPage();
        }
    }
    else {
        getCurrentCardPage();
    }
}

//function handleTooltip(e) {
//    var funcUrl = searchServiceUrl + "/findTranslations";
//    var searchTransObj = LanguageSearchObject();
//    var CardID = parseInt($(e).attr('id').split("_")[1]);
//    searchTransObj.CardID = CardID;
//    var dataToSend = searchTransObj;
//    var note = $(this).attr('rel');
//    if (note == undefined || note == null) {
//        note = "";
//    }
//    //$('.popover-content').html('');
//    $.ajax({
//        type: "POST",
//        headers: { 'Authorization': 'Bearer ' + readCookie('CCAuth') },
//        url: funcUrl,
//        data: JSON.stringify(dataToSend),
//        contentType: "application/json; charset=utf-8",
//        success: function (d) {
//            $(e).popover('hide');
//            $(e).popover('destroy');
//            $(e).popover({
//                title: '',
//                content: renderTooltipHtml(d, status, note),
//                //container: 'body',
//                trigger: 'manual',
//                html: true,
//                placement: 'right',
//                delay: { show: 3000, hide: 100 },
//                template: '<div class="popover"><div class="arrow"></div><div class="popover-inner"><h3 class="popover-title"></h3><div class="popover-content"><p></p></div></div></div>'
//            }).popover('show');
//            //$('.popover-content').html(renderTooltipHtml(d, status, note));
//        },
//        beforeSend: function () {
//            $(e).popover({
//                title: '',
//                content: 'test',
//                //container: 'body',
//                trigger: 'manual',
//                html: true,
//                placement: 'right',
//                delay: { show: 3750, hide: 0 },
//                template: '<div class="popover"><div class="arrow"></div><div class="popover-inner"><h3 class="popover-title"></h3><div class="popover-content"><p></p></div></div></div>'
//            }).popover('show');
//            var loadingimage = '<div align="center"><img id="loadingimage" src="images/ajax-loader.gif" /></div>';
//            $('.popover-content').html(loadingimage);
//        }
//    });
//}

//function renderTooltipHtml(msg, status, note) {
//    var results = $.parseJSON(msg.JsonResults);
//    var htmlstring = '<div class="gridTooltip"><p><span style="font-weight: bold;">Notes:</span> ' + note + '</p>'
//    htmlstring += '<p><span style="font-weight: bold;">Translations: </span>';
//    for (i = 0; i < results.length; i++) {
//        htmlstring += results[i].Value + ' ';
//    }
//    htmlstring += '</div>'
//    return htmlstring;
//}

function initCardSuccess(msg) {
    var toSee;
    var ids = new Array();
    categoryVector = [];
    var results = msg.projects;
    if (results.length > 0) {
        PageAmount = results[0].PageAmount;
        if (results[0].PageAmount != null && results[0].PageAmount != undefined) {
            var current = PageCount + "/" + results[0].PageAmount;
            $('#lbCurrentPage').text(current);
        }

        for (var index = 0; index < results.length; ++index) {
            if (results[index].Value != null && results[index].Value != undefined) {
                var categoryPair = new Object();
                categoryPair.categoryValue = results[index].Value;
                categoryPair.typeValue = results[index].Value1;
                categoryPair.categoryID = results[index].CategoryID;
                categoryPair.typeID = results[index].TypeID;
                categoryPair.typeOrder = results[index].TypeOrder;
                categoryVector.push(categoryPair);
                var idFound = false;
                var id = results[index].CardificationID;
                for (var arrayIndex = 0; arrayIndex < ids.length; ++arrayIndex) {
                    if (id == ids[arrayIndex]) {
                        results.remove(index);
                        --index;
                        idFound = true;
                        break;
                    }
                }

                if (!idFound) {
                    ids.push(id);
                }
            }
        }


        toSee = $('#ProjectCard').render(results);
        //var headers = $('#CardTableHeader').render();
        $('#CardHolderTemplate').html("");
        //$('#tbTemplate').append(headers);
        $('#CardHolderTemplate').append(toSee);

        //$("#CardHolderTemplate tr:even").css("background-color", "#dedede");

        //$("#CardHolderTemplate tr:odd").css("background-color", "#ffffff");

        //$('.hover-tooltip').tooltip({
        //    items: '.hover-tooltip',
        //    content: handleTooltip
        //}
        //);



        //$('.hover-tooltip').on("mouseOver", function () { $(this).popover('show'); });

        //$('.hover-tooltip').hover(function () {

        //    $('.hover-tooltip').each(function () {
        //        $(this).popover('hide');
        //    });
        //    var e = $(this);
        //    //setTimeout(function () { handleTooltip(e); }, 100);
        //    handleTooltip(e);
        //},
        //function () {
        //    $('.hover-tooltip').each(function () {
        //        $(this).popover('hide');
        //    });
        //    $(this).popover('hide');
        //});


        if (filtersEnabled) {
            categorySidebarPopulate();
        }
    }
    else {
        if (PageCount != 1) {
            --PageCount;
            $('#lbCurrentPage').text(PageCount);
        }
        else {
            var noResults = $('#blankTableRowTemplate').render();
            var headers = $('#CardTableHeader').render();
            $('#tbTemplate').html("");
            //$('#tbTemplate').append(headers);
            $('#tbTemplate').append(noResults);
        }
    }

    if (commandsEnabled) {
        registerCheckBoxes();
    }

    $('div[id*="card_"]').click(fullscreen);
}


function initCardError(msg, status) {
    var info = msg.statusText;
    info += " " + msg.statusCode;
    //$('#lbMessage').text(info);
    displayMessage("Initial load has failed!", "Red");
}


function clearMessage() {
    $('#divMessage').append("");
}
function popUpTest(window) {
    var popUpBlocked = (!window || typeof (window.document.getElementById) == undefined)
    if (popUpBlocked) {
        return false;
    }
    else {
        return true;
    }
}
function hrefClick(CardID) {
    //var url = window.location.protocol + "//" + window.location.host + "/Cobra/CardEdit.aspx?n=0&s=";
    var url = "CardEdit.aspx?n=0&s=";
    url += CardID;
    var url2 = url;
    url += "&c=1";
    if (popUpEnabled) {
        var myWindow = window.open(url, CardID, "menubar=1, resizable=1, location=1, width=960, height=550, status=1");
        if (!popUpTest(myWindow)) {
            popUpEnabled = false;
            url2 += "&c=0";
            window.location = (url2);
        }
    }
    else {
        url2 += "&c=0";
        window.location = (url2);
    }
}
function listBox_Click(e) {
    //TODO: remove
    var id = e.target.id;
    var searchValue = $('#' + id).val();
    searchClick(e, searchValue);
}


function searchClick(e, message) {
    searchMode = true;
    PageCount = 1;
    $('#lbCurrentPage').text(PageCount);
    search(null);
}

function search(val) {
    if (filtersEnabled) {
        filterResults();
    }
    else {

        searchMode = false;
        PageCount = 1;
        $('#lbCurrentPage').text(PageCount);
        initCard();
    }
}

function displayMessage(message, color) {
    $('#divMessage').html("");
    $('#divMessage').append(message);
    $('#divMessage').css("color", color);
    $('#divMessage').dialog();
}



function fullscreen(e) {
    var d = {};
    var speed = 900;
    if (!isFullscreen) { // MAXIMIZATION
        $('div[id*="card_"]').hide();
        $(e.currentTarget).show();
        oldwidth = $(e.currentTarget).width();
        oldheight = $(e.currentTarget).height();
        d.width = "90%";
        d.height = (oldheight * 3) + "px";
        
        isFullscreen = true;
        //$("h1").slideUp(speed);
    }
    else { // MINIMIZATION            
        d.width = oldwidth.toString() + "px";
        d.height = ($(e.currentTarget).height() / 3).toString() + "px";
        isFullscreen = false;
        //$("h1").slideDown(speed);
        $('div[id*="card_"]').show();
    }
    $(e.currentTarget).animate(d, speed);

}




