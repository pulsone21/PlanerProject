using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;
using CompanySystem;

namespace UISystem
{
    public class ContractPageController : TableContentController
    {
        private ContractMarket Market = ContractMarket.Instance;
        public override void SetTableContent(string content)
        {
            if (int.TryParse(content, out int enumInt))
            {
                table.SetTableContent(SetContentList(enumInt));
            }
            Debug.LogError("ContractPageController - SetTableContnent - Couldn't parse input to int, content: " + content);
        }

        public void GetContract()
        {
            TransportContract contract = (TransportContract)table.SelectedRow.OriginRecord;
            PlayerCompanyController.Company.AddNewTransportContract(Market.ReceiveContract(contract));
        }

        private List<ITableRow> SetContentList(int enumInt)
        {
            List<ITableRow> rows = new List<ITableRow>();
            foreach (TransportContract contract in Market.GetTransportContractsByTranportType((TransportType)enumInt))
            {
                rows.Add(contract);
            }
            return rows;
        }
    }
}
