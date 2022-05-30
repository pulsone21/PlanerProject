using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;

namespace CompanySystem
{
    [System.Serializable]
    public class CompanyNames
    {
        public List<string> Names;
        public List<string> CompanyForms;
        public List<GoodCategoryNames> CategoryNames;

        public CompanyNames(List<string> names, List<string> companyForms, List<GoodCategoryNames> categoryNames)
        {
            Names = names;
            CompanyForms = companyForms;
            CategoryNames = categoryNames;
        }
    }

    [System.Serializable]
    public struct GoodCategoryNames
    {
        public GoodCategory Category;
        public List<string> Names;

        public GoodCategoryNames(GoodCategory category, List<string> names)
        {
            Category = category;
            Names = names;
        }
    }
}
