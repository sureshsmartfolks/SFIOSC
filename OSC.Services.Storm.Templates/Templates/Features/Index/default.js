checkAuth();

$(document).ready(function () {

    $("#topSection").append(getFeature("Banner"));
    //$("#topFeature").append(getFeature("Notifications"));
    //$("#topSection").append(getFeature("Navigation"));
    //$("#leftFeature").prepend(getFeature("SpecOperations"));
    //$.when($("#mainFeature").append(getFeature("DataCards")))
    $("#mainFeature").append(getFeature("DataCards"));
                //.done($("#leftFeature").append(getFeature("Filters")))
                //.done($("#gridFeatures").append(getFeature("Search")))
                //.done($("#leftFeature").append(getFeature("NotificationMenu")))
                //.done($("#gridFeatures").append(getFeature("DataGridCommands")));


});