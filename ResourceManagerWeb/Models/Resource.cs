using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManager.Models
{
    public class Resource
    {

        private int resourceID = -1;
        private string note = "";
        private string name = "";
        private string language = "";
        private string phone = "";
        private string email = "";
        private string jsonResults = "";


        public string JsonResults
        {
            get { return jsonResults; }
            set { jsonResults = value; }
        }

        public int ResourceID
        {
            get { return resourceID; }
            set { resourceID = value; }

        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        public string Language
        {
            get { return language; }
            set { language = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}