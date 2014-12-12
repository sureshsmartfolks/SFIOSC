

function LanguageDTO(){
    var obj = new Object();
    obj.translateDirectionID = new Array(); // langDirections
    return obj;
}

function DocumentDTO() {
    var obj = new Object();

    obj.filePath = null;
    obj.name = null;
    obj.sourceLanguageID = null;
    obj.translateDirections = LanguageDTO();// = [];
    obj.version = null;
    obj.documentID = null;
    obj.projectID = null;
    obj.typeID = null;
    obj.status = null;
    return obj;
}

function ClientDTO() {
    var obj = new Object();
    obj.name = null;
    obj.phoneNumber=null;
    obj.email = null;
    obj.ID = null;
    //potentially add file-id
    return obj;
}

function ProjectDTO() {
    var obj = new Object();
    obj.projectID=null;
    obj.client = ClientDTO();
    obj.projectName = null;
    obj.sourceLanguageID = null;
    obj.resources = new Array();//null //{};
    //obj.resources.push(ResourceDTO());
    obj.documents = new Array(); // DocumentDTO
    //obj.documents[obj.documents.length] = DocumentDTO();
    //obj.documents.push(DocumentDTO());
    obj.invoices = new Array();
    //obj.invoices.push(InvoiceDTO());
    obj.trackPath = null;
    obj.trackStatus = null;
    return obj;
}

function InvoiceDTO() {
    var obj = new Object();
    obj.price = null;
    obj.status = null;
    obj.version = null;
    obj.documentID = null;
    obj.invoiceID = null;
    return obj;
}

function ResourceDTO() {
    var obj = new Object();
    obj.firstName = null;
    obj.lastName = null;
    obj.company = null;
    obj.address = null;
    obj.type = null;
    obj.languageAssociations = LanguageDTO();
    obj.documents = new Array();
    obj.resourceID = null;
    return obj;
}

function ProjectManagementRequest() {
    var obj = new Object();
    obj.projects = new Array(); // ProjectDTO
    obj.projects[obj.projects.length] = ProjectDTO();
    obj.status = null;
    obj.errors=null;
    return obj;
}

var langs = [ "English", "French", "Spanish", "Japanese"];

var langDirections = [
    langs[0] + " --> " + langs[0],
    langs[0] + " --> " + langs[1],
    langs[0] + " --> " + langs[2],
    langs[0] + " --> " + langs[3],
    langs[1] + " --> " + langs[0],
    langs[1] + " --> " + langs[1],
    langs[1] + " --> " + langs[2],
    langs[1] + " --> " + langs[3],
];

var docTypes = ["JPG", "PDF", "DOCX", "TXT"];

var resoTypes = ["Resource Manager", "Translator"];