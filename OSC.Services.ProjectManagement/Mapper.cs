using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OSC.ProjectManagement.BusinessObjects;

namespace ProjectManagement_API
{
    public class Mapper
    {

        public static invoice toBO(Models.DTO.invoiceDTO oDTO)
        {
            invoice rc = new invoice();
            rc.price = oDTO.price;
            rc.status = oDTO.status;
            rc.version = oDTO.version;
            rc.documentID = oDTO.documentID;
            rc.invoiceID = oDTO.invoiceID;
            return rc;
        }

        public static Models.DTO.invoiceDTO toDTO(invoice rc)
        {
            Models.DTO.invoiceDTO oDTO = new Models.DTO.invoiceDTO();
            oDTO.price = rc.price;
            oDTO.status = rc.status;
            oDTO.version = rc.version;
            oDTO.documentID = rc.documentID;
            oDTO.invoiceID = rc.invoiceID;
            return oDTO;
        }
        
        public static resource toBO(Models.DTO.resourceDTO oDTO)
        {
            resource rc = new resource();
            rc.firstName = oDTO.firstName;
            rc.lastName = oDTO.lastName;
            rc.company = oDTO.company;
            rc.address = oDTO.address;
            rc.type = oDTO.type;
            rc.languageAssociations = new language();
            rc.languageAssociations.translateDirectionID= new List<int>();
            rc.languageAssociations = Mapper.toBO(oDTO.languageAssociations);
            rc.documents = new List<Document>();
            rc.documents = Mapper.toBO(oDTO.documents);
            rc.resourceID = oDTO.resourceID;
            return rc;
        }

        public static Models.DTO.resourceDTO toDTO(resource rc)
        {
            Models.DTO.resourceDTO oDTO = new Models.DTO.resourceDTO();
            oDTO.firstName= rc.firstName;
            oDTO.lastName = rc.lastName;
            oDTO.company  = rc.company;
            oDTO.address  = rc.address;
            oDTO.type     = rc.type;
            oDTO.languageAssociations = new Models.DTO.languageDTO();
            oDTO.languageAssociations.translateDirectionID = new List<int>();
            oDTO.languageAssociations = Mapper.toDTO(rc.languageAssociations);
            oDTO.documents = new List<Models.DTO.documentDTO>();
            //oDTO.documents = Mapper.toDTO(rc.documents);
            oDTO.resourceID = rc.resourceID;
            return oDTO;
        }

        public static Document toBO(Models.DTO.documentDTO oDTO)
        {
            Document rc = new Document();
            rc.filePath = oDTO.filePath;
            rc.status = oDTO.status;
            rc.version = oDTO.version;
            rc.typeID = oDTO.typeID;
            rc.sourceLanguageID = oDTO.sourceLanguageID;
            rc.translateDirections = new language();
            rc.translateDirections.translateDirectionID = new List<int>();
            rc.translateDirections = Mapper.toBO(oDTO.translateDirections);
            //foreach (Models.DTO.languageDTO transDir in oDTO.translateDirections)
            //{
            //    rc.translateDirections.Add(Mapper.toBO(transDir));
            //}
            rc.documentID = oDTO.documentID;
            rc.projectID = oDTO.projectID;
            rc.name = oDTO.name;
            return rc;
        }

        public static Models.DTO.documentDTO toDTO(Document rc)
        {
            Models.DTO.documentDTO oDTO = new Models.DTO.documentDTO();
            oDTO.filePath = rc.filePath;
            oDTO.status = rc.status;
            oDTO.version = rc.version;
            oDTO.typeID = rc.typeID;
            oDTO.sourceLanguageID = rc.sourceLanguageID;
            oDTO.translateDirections = new Models.DTO.languageDTO();
            oDTO.translateDirections.translateDirectionID = new List<int>();
            oDTO.translateDirections = Mapper.toDTO(rc.translateDirections);
            //foreach (language transDir in rc.translateDirections)
            //{
            //    oDTO.translateDirections.Add(Mapper.toDTO(transDir));
            //}
            oDTO.documentID = rc.documentID;
            oDTO.projectID = rc.projectID;
            oDTO.name = rc.name;
            return oDTO;
        }

        public static STORMProject toBO(Models.DTO.projectDTO oDTO){
            STORMProject rc = new STORMProject();
            rc.client = Mapper.toBO(oDTO.client);
            rc.documents = new List<Document>();
            rc.documents = Mapper.toBO(oDTO.documents);
            rc.projectID = oDTO.projectID;
            rc.projectName = oDTO.projectName;
            rc.sourceLanguageID = oDTO.sourceLanguageID;
            rc.resources = new List<resource>();
            rc.resources = Mapper.toBO(oDTO.resources);
            //rc.invoices = new List<invoice>();
            //rc.invoices = Mapper.toBO(oDTO.invoices);
            rc.trackStatus = oDTO.trackStatus;
            rc.trackPath = oDTO.trackPath;

            return rc;
        }

        public static Models.DTO.projectDTO toDTO(STORMProject rc){
            Models.DTO.projectDTO oDTO = new Models.DTO.projectDTO();
            oDTO.documents = new List<Models.DTO.documentDTO>();
            oDTO.documents = Mapper.toDTO(rc.documents);
            oDTO.client = Mapper.toDTO(rc.client);
            oDTO.projectID = rc.projectID;
            oDTO.projectName = rc.projectName;
            oDTO.sourceLanguageID = rc.sourceLanguageID;
            oDTO.resources = new List<Models.DTO.resourceDTO>();
            oDTO.resources = Mapper.toDTO(rc.resources);
            //oDTO.invoices = new List<Models.DTO.invoiceDTO>();
            //oDTO.invoices = Mapper.toDTO(rc.invoices);
            oDTO.trackStatus = rc.trackStatus;
            oDTO.trackPath = rc.trackPath;

            return oDTO;
        }

        public static Client toBO(Models.DTO.clientDTO oDTO)
        {
            Client rc = new Client();
            rc.ID = oDTO.ID;
            rc.name = oDTO.name;
            rc.email = oDTO.email;
            rc.phoneNumber = oDTO.phoneNumber;
            return rc;
        }

        public static Models.DTO.clientDTO toDTO(Client rc)
        {
            Models.DTO.clientDTO oDTO = new Models.DTO.clientDTO();
            oDTO.ID = rc.ID;
            oDTO.name = rc.name;
            oDTO.email = rc.email;
            oDTO.phoneNumber = rc.phoneNumber;
            return oDTO;
        }

        public static language toBO(Models.DTO.languageDTO oDTO)
        {
            language rc = new language();
            rc.translateDirectionID = new List<int>();
            for (int ID = 0; ID < oDTO.translateDirectionID.Count(); ID++)
            //foreach (int ID in rc.translateDirectionID)
            {
                rc.translateDirectionID.Add(oDTO.translateDirectionID[ID]);
            }
            return rc;
        }

        public static Models.DTO.languageDTO toDTO(language rc)
        {
            Models.DTO.languageDTO oDTO = new Models.DTO.languageDTO();
            oDTO.translateDirectionID = new List<int>();
            for (int ID = 0; ID < rc.translateDirectionID.Count(); ID++)
            //foreach (int ID in oDTO.translateDirectionID)
            {
                oDTO.translateDirectionID.Add(rc.translateDirectionID[ID]);
            }
            return oDTO;
        }

        public static List<STORMProject> toBO(List<Models.DTO.projectDTO> oDTO)
        {
            List<STORMProject> rc = new List<STORMProject>();
            foreach (Models.DTO.projectDTO proj in oDTO)
            {
                rc.Add(Mapper.toBO(proj));
            }
            return rc;
        }

        public static List<Models.DTO.projectDTO> toDTO(List<STORMProject> rc)
        {
            List<Models.DTO.projectDTO> oDTO = new List<Models.DTO.projectDTO>();
            foreach (STORMProject proj in rc)
            {
                oDTO.Add(Mapper.toDTO(proj));
            }
            return oDTO;
        }

        public static List<Document> toBO(List<Models.DTO.documentDTO> oDTO)
        {
            List<Document> rc = new List<Document>();
            foreach (Models.DTO.documentDTO doc in oDTO)
            {
                rc.Add(Mapper.toBO(doc));
            }
            return rc;
        }

        public static List<Models.DTO.documentDTO> toDTO(List<Document> rc)
        {
            List<Models.DTO.documentDTO> oDTO = new List<Models.DTO.documentDTO>();
            foreach (Document doc in rc)
            {
                oDTO.Add(Mapper.toDTO(doc));
            }
            return oDTO;
        }
        
        public static List<invoice> toBO(List<Models.DTO.invoiceDTO> oDTO)
        {
            List<invoice> rc = new List<invoice>();
            foreach (Models.DTO.invoiceDTO inv in oDTO)
            {
                rc.Add(Mapper.toBO(inv));
            }
            return rc;
        }

        public static List<Models.DTO.invoiceDTO> toDTO(List<invoice> rc)
        {
            List<Models.DTO.invoiceDTO> oDTO = new List<Models.DTO.invoiceDTO>();
            foreach (invoice inv in rc)
            {
                oDTO.Add(Mapper.toDTO(inv));
            }
            return oDTO;
        }

        public static List<resource> toBO(List<Models.DTO.resourceDTO> oDTO)
        {
            List<resource> rc = new List<resource>();
            foreach (Models.DTO.resourceDTO res in oDTO)
            {
                rc.Add(Mapper.toBO(res));
            }
            return rc;
        }

        public static List<Models.DTO.resourceDTO> toDTO(List<resource> rc)
        {
            List<Models.DTO.resourceDTO> oDTO = new List<Models.DTO.resourceDTO>();
            foreach (resource res in rc)
            {
                oDTO.Add(Mapper.toDTO(res));
            }
            return oDTO;
        }
    }
}