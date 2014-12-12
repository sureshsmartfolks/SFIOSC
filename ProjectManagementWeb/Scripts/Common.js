var useVM = false;

authCookieName = "Storm-PMAuth";

if (useVM == false) {

    var MasterProjectUrl = "http://localhost/ProjectManagement-API";
    var MasterTemplateUrl = "http://localhost/StromTemplates-API";

    var oAuthConfig = {
        client_id: "tt_Storm-PM",
        scope: "urn:Storm-PM",
        response_type: "token",
        redirect_uri: "http://localhost/ProjectManagement/Index.html"
    }

}


var servicesBaseUrl = "";

var endpoints = {
    ServiceEndpointUrl: servicesBaseUrl + "ServiceHost",
    IdpOauthEndpointUrl: "https://oscid.osc-it.com/issue/oauth2/authorize"
};
