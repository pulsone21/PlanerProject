using UnityEngine;
using UnityEditor;
using ContractSystem;

namespace CompanySystem
{
    [CustomEditor(typeof(CompanyNamestester))]
    public class CompanyNamestesterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            CompanyNamestester cT = (CompanyNamestester)target;
            if (GUILayout.Button("Save Names"))
            {
                cT.SaveJSONToFile();
            }

            if (GUILayout.Button("Load Names"))
            {
                cT.LoadCompanyNamesFromFile();
            }




            if (GUILayout.Button("Generate Name"))
            {
                int amountOfCategories = System.Enum.GetNames(typeof(GoodCategory)).Length - 1;
                GoodCategory gC = (GoodCategory)Random.Range(0, amountOfCategories);
                string companyName = CompanyNameGenerator.GenerateCompanyName(gC, cT.city);
                Debug.Log(companyName);
            }
        }
    }
}