using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sdl.LanguagePlatform.TranslationMemory;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using OwinSelfhostSample.Messaging;
using Sdl.ProjectAutomation.FileBased;
using System.Xml.Linq;

namespace OwinSelfhostSample
{
    public class ProjectController : ApiController
    {
        [HttpPost]
        public List<TranslationFile> Post()
        {
            ProjectCreator project = new ProjectCreator();
            List<String> tmList = new List<String>() {@"C:\TranslationMemories\French\AF_SPEC_FREN_fr-FR_en-US.sdltm"};

            FileBasedProject newProject = project.Create("C:\\test", tmList, false, true, false, true, false);

            var SearchControl = new TmSearchController();

            var langFiles = newProject.GetTargetLanguageFiles();
            var sourceLang = "fr-FR";
            var targetLang = "en-US";
            List<TranslationFile> TranslatedFiles = new List<TranslationFile>();
            foreach (var file in langFiles)
            {
                XElement root = XElement.Load(file.LocalFilePath);
                XNamespace ns = "urn:oasis:names:tc:xliff:document:1.2";
                var desc = root.Descendants(ns + "source");
                desc.Elements(ns + "seg-source").Remove();
                TranslationFile transFile = new TranslationFile();
                transFile.SourceFileName = file.Name;
                transFile.SourceLanguage = sourceLang;
                transFile.TargetLanguage = targetLang;
                transFile.SegmentList = new List<TranslationObject>();
                foreach (XElement el in desc)
                {
                    if (el.Value.Length > 0)
                    {

                        TranslationObject NewTranslation = new TranslationObject();
                        NewTranslation.SourceSegment = el.Value;
                        TranslationRequestDTO newSearchDTO = new TranslationRequestDTO();
                        newSearchDTO.SourceLanguage = sourceLang;
                        newSearchDTO.TargetLanguage = targetLang;
                        newSearchDTO.Request = el.Value;
                        List<TranslationResponse> response = SearchControl.Post(newSearchDTO);
                        if (response.Count > 0)
                        {
                            if (response[0].Score >= 80)
                            {
                                NewTranslation.TranslatedSegment = response[0].Translation;
                                NewTranslation.score = response[0].Score;
                            }
                        }
                        transFile.SegmentList.Add(NewTranslation);
                    }
                }
                TranslatedFiles.Add(transFile);
            }

            string tempFile = langFiles[0].LocalFilePath;
            
            FileInfo fi = new FileInfo(tempFile);
            DirectoryInfo di1 = fi.Directory;
            DirectoryInfo di = Directory.GetParent(di1.FullName);
            Directory.Delete(di.FullName, true);

            



            return TranslatedFiles;
        }
    }
}
