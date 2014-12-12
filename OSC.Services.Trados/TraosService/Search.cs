using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sdl.LanguagePlatform.TranslationMemoryApi;
using Sdl.LanguagePlatform.TranslationMemory;
using Sdl.LanguagePlatform.Core.Tokenization;
using Sdl.LanguagePlatform.Core;

namespace TraosService
{
    public class Search
    {
        #region "searchTU"
        public string SearchForText(string tmPath, string searchText, SearchMode mode)
        {
            #region "ExecuteSearch"
            FileBasedTranslationMemory tm = new FileBasedTranslationMemory(tmPath);
            SearchResults results = tm.LanguageDirection.SearchText(this.GetSearchSettings(mode), searchText);
            #endregion

            #region "histlist"
            string hitList = "Number of hits found: " + results.Count.ToString() + "\n\n";

            foreach (SearchResult result in results)
            {
                hitList += "Source segment: " + result.MemoryTranslationUnit.SourceSegment.ToPlain() + "\n";
                hitList += "Target segment: " + result.MemoryTranslationUnit.TargetSegment.ToPlain() + "\n";
                hitList += "Created on: " + result.MemoryTranslationUnit.SystemFields.CreationDate.ToString() + "\n";
                hitList += "Origin: " + result.MemoryTranslationUnit.Origin.ToString() + "\n";
                hitList += "Match value: " + result.ScoringResult.Match.ToString() + "\n\n";
            }

            //MessageBox.Show(hitList);
            return hitList;
            #endregion
        }
        #endregion

        #region "settings"
        private SearchSettings GetSearchSettings(SearchMode mode)
        {
            SearchSettings settings = new SearchSettings();

            settings.MaxResults = 5;
            settings.MinScore = 70;
            settings.Mode = mode;
            settings.FindPenalty(PenaltyType.FilterPenalty);

            return settings;
        }
        #endregion

        #region "SettingsWithFilter"
        //private SearchSettings GetSearchSettingsWithFilter(SearchMode mode)
        //{
        //    SearchSettings settings = new SearchSettings();

        //    settings.MaxResults = 5;
        //    settings.MinScore = 70;
        //    settings.Mode = mode;

        //    #region "SettingFilter"
        //    Filter filter = new Filter(this.GetFilter(), "Microsoft", 1);
        //    settings.AddFilter(filter);
        //    #endregion

        //    return settings;
        //}
        #endregion

        #region "GetFilter"
        private FilterExpression GetFilter()
        {
            #region "SimpleCriterion"
            PicklistItem fieldName = new PicklistItem("Customer");
            MultiplePicklistFieldValue fieldValue = new MultiplePicklistFieldValue("Microsoft");
            fieldValue.Add(fieldName);
            #endregion

            #region "SimpleFilter"
            AtomicExpression filter = new AtomicExpression(fieldValue, AtomicExpression.Operator.Equal);
            return filter;
            #endregion
        }
        #endregion

        #region "SearchForSegment"
        public string SearchForSegment(string tmPath, string srchterm)
        {
            FileBasedTranslationMemory tm = new FileBasedTranslationMemory(tmPath);
            SearchSettings settings = new SearchSettings();
            settings.MaxResults = 5;

            Segment srcSegment = new Segment(tm.LanguageDirection.SourceLanguage);
            srcSegment.Add(srchterm);
            SearchResults results = tm.LanguageDirection.SearchSegment(settings, srcSegment);

            string output = "";
            foreach (SearchResult result in results)
            {
                output += result.TranslationProposal.TargetSegment.ToPlain() + '\n';
            }

            return output;
        }
        #endregion

        #region "SearchForTu"
        public void SearchForTu(string tmPath)
        {
            FileBasedTranslationMemory tm = new FileBasedTranslationMemory(tmPath);
            SearchSettings settings = new SearchSettings();
            settings.MaxResults = 5;

            TranslationUnit tu = new TranslationUnit();
            tu.SourceSegment = new Segment(tm.LanguageDirection.SourceLanguage);
            tu.TargetSegment = new Segment(tm.LanguageDirection.TargetLanguage);

            tu.SourceSegment.Add("Configure the spelling checker as shown below:");
            tu.TargetSegment.Add("Konfigurieren Sie die Rechtschreibprüfung wie unten gezeigt:");

            SearchResults results = tm.LanguageDirection.SearchTranslationUnit(settings, tu);

            foreach (SearchResult result in results)
            {
                //MessageBox.Show(result.TranslationProposal.TargetSegment.ToPlain());
            }
        }
        #endregion
    }
}
