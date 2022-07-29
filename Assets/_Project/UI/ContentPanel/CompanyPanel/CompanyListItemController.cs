using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanySystem;
using TMPro;


namespace UISystem
{
    public class CompanyListItem : ListItemController<GoodCompany>
    {
        [SerializeField] private TextMeshProUGUI CompanyName;
        [SerializeField] private TextMeshProUGUI City;
        [SerializeField] private TextMeshProUGUI GoodCategory;
        [SerializeField] private TextMeshProUGUI Relationsship;
        public override void Initialize(GoodCompany company)
        {
            if (Initialized) return;
            Initialized = true;
            item = company;
            CompanyName.text = item.Name;
            City.text = item.City.Name;
            GoodCategory.text = item.GoodCategory.ToString();
            Relationship relation = item.GetRelationshipToPlayerCompany();
            Relationsship.text = relation == default ? "currently no relationship" : relation.ToString();
            gameObject.SetActive(true);
        }
        public override void SetContent() => CompanyViewer.Instance.SetContent(item);
    }
}
