namespace OwinSelfhostSample
{
    using System;
    using System.Globalization;
    using System.IO;
    using Sdl.Core.Globalization;
    using Sdl.Core.Settings;
    using Sdl.LanguagePlatform.TranslationMemoryApi;
    using Sdl.ProjectAutomation.Core;
    using Sdl.ProjectAutomation.FileBased;
    using Sdl.ProjectAutomation.Settings;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class ProjectCreator
    {

        #region "ReportId"
        private Guid reportId;
        #endregion

        #region "Create"
        #region "CreateMainFunction"
        /// <summary>
        /// Creates the actual project that is used as a container for
        /// the files to analyze. Triggers all subsequent helper function
        /// in sequence, i.e. adding the source files, the TM, configuring
        /// the task settings, and running the required tasks, 
        /// if required publishing the result to a project server
        /// </summary> 
        public FileBasedProject Create(
            string docFolder,
            List<string> tmFiles,
            bool recursion,
            bool reportCrossFileRepetitions,
            bool reportInternalFuzzyMatchLeverage,
            bool keepProjectFiles,
            bool publishToServer)
        #endregion

        {
            #region "RetrieveTmLanguages"

            List<FileBasedTranslationMemory> tmList = new List<FileBasedTranslationMemory>();
            foreach (var tm in tmFiles)
            {
                FileBasedTranslationMemory fileTm = new FileBasedTranslationMemory(tm);
                
                tmList.Add(fileTm);
            }

            string srcLocale = tmList[0].LanguageDirection.SourceLanguage.ToString();
            string trgLocale = tmList[0].LanguageDirection.TargetLanguage.ToString();

            #endregion

            #region "newProject"

            FileBasedProject newProject = new FileBasedProject(this.GetProjectInfo(srcLocale, trgLocale));

            #endregion

            #region "CallAddFiles"

            this.AddFiles(newProject, docFolder, recursion);

            #endregion

           
                #region "CallAddTm"

            foreach (var tm in tmFiles)
            {
                this.AddTm(newProject, tm);
            }
            


            #region "CallConvert"
            this.ConvertFiles(newProject);
            #endregion

            //#region "CallGetAnalyzeSettings"
            //this.GetAnalyzeSettings(
            //    newProject,
            //    trgLocale,
            //    reportCrossFileRepetitions,
            //    reportInternalFuzzyMatchLeverage);
            //#endregion

          
            //#region "Save"
            
            //newProject.Save();

            return newProject;
            //newProject.SavePackageAs(Guid.NewGuid(), "C:\test\newProject");
            //#endregion

           
        }
        #endregion

        #region "GetProjectInfo"
        #region "GetProjectInfoFunction"
        /// <summary>
        /// Configures the project information, i.e. the project location (folder), the project name,
        /// and the source/target language.
        /// </summary> 
        private ProjectInfo GetProjectInfo(string srcLocale, string trgLocale)
        #endregion
        {
            #region "ProjectInfo"
            ProjectInfo info = new ProjectInfo();
            #endregion

            #region "ProjectName"
            string nameExt = DateTime.Now.ToString();
            nameExt = nameExt.Replace('.', '_');
            nameExt = nameExt.Replace(':', '_');
            nameExt = nameExt.Replace('/', '_');
            nameExt = nameExt.Replace(' ', 'T');
            info.Name = "BatchAnalyzer_" + nameExt;
            #endregion

            #region "ProjectFolder"
            string localProjectFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() +
                Path.DirectorySeparatorChar + @"Studio 2011\Projects\" + info.Name;
            info.LocalProjectFolder = localProjectFolder;
            #endregion

            #region "SetProjectSourceLanguage"
            Language srcLang = new Language(CultureInfo.GetCultureInfo(srcLocale));
            info.SourceLanguage = srcLang;
            #endregion

            #region "SetProjectTargetLanguage"
            Language[] trgLang = new Language[] { new Language(CultureInfo.GetCultureInfo(trgLocale)) };
            info.TargetLanguages = trgLang;
            #endregion

            #region "ReturnInfo"
            return info;
            #endregion
        }
        #endregion

        #region "Addfolder"
        /// <summary>
        /// Adds the files from the specified folder to the project and sets the file use, e.g. translatable or reference.            
        /// </summary> 
        #region "AddFilesFunction"
        private void AddFiles(FileBasedProject project, string folder, bool recursion)
        #endregion
        {
            #region "AddFolderWithFiles"
            project.AddFolderWithFiles(folder, recursion);
            #endregion

            #region "GetSourceLanguageFiles"
            ProjectFile[] projectFiles = project.GetSourceLanguageFiles();

            AutomaticTask scan = project.RunAutomaticTask(
                projectFiles.GetIds(),
                AutomaticTaskTemplateIds.Scan);
            #endregion
        }
        #endregion

        #region "AddTm"
        /// <summary>
        /// Adds the TM that should be used for the analysis. The project languages are
        /// set according to the TM.
        /// </summary> 
        #region "AddTmFunction"
        private void AddTm(FileBasedProject project, string tmFilePath)
        #endregion
        {
            #region "TranslationProviderConfiguration"
            TranslationProviderConfiguration config = project.GetTranslationProviderConfiguration();
            #endregion

            #region "TranslationProviderCascadeEntry"
            TranslationProviderCascadeEntry tm = new TranslationProviderCascadeEntry(
                tmFilePath,
                true,
                true,
                false);
            config.Entries.Add(tm);
            project.UpdateTranslationProviderConfiguration(config);
            TranslationProviderConfiguration newconfig = project.GetTranslationProviderConfiguration();
            #endregion
        }
        #endregion

        

        #region "ConvertAndCopy"
        /// <summary>
        /// Runs the two automatic tasks: Convert translatable files to a translatable format (i.e. SDL XLIFF)
        /// and creates target file copies.
        /// </summary> 
        private void ConvertFiles(FileBasedProject project)
        {
            #region "GetFilesForProcessing"
            ProjectFile[] files = project.GetSourceLanguageFiles();
            #endregion

            #region "RunConversion"
            for (int i = 0; i < project.GetSourceLanguageFiles().Length; i++)
            {
                if (files[i].Role == FileRole.Translatable)
                {
                    Guid[] currentFileId = { files[i].Id };
                    AutomaticTask convertTask = project.RunAutomaticTask(
                        currentFileId,
                        AutomaticTaskTemplateIds.ConvertToTranslatableFormat);

                    #region "CopyToTarget"
                    AutomaticTask copyTask = project.RunAutomaticTask(
                        currentFileId,
                        AutomaticTaskTemplateIds.CopyToTargetLanguages);
                    #endregion
                }
            }
            #endregion
        }
        #endregion

        
    }
}
        #endregion