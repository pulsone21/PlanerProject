using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;
using CompanySystem;

namespace UISystem
{
    public class ContractPageController : TableContentController
    {
        public override void SetTableContent(string content)
        {
            if (int.TryParse(content, out int enumInt))
            {
                table.SetTableContent(SetContentList(enumInt));
                return;
            }
            Debug.LogError("ContractPageController - SetTableContnent - Couldn't parse input to int, content: " + content + " output from tryParse: " + enumInt);
        }

        public void GetContract()
        {
            TransportContract contract = (TransportContract)table.SelectedRow.OriginRecord;
            PlayerCompanyController.Company.AddNewTransportContract(ContractMarket.ReceiveContract(contract));
        }

        private List<ITableRow> SetContentList(int enumInt)
        {
            List<ITableRow> rows = new List<ITableRow>();
            Debug.Log(enumInt);
            foreach (TransportContract contract in ContractMarket.GetTransportContractsByTranportType((TransportType)enumInt))
            {
                rows.Add(contract);
            }
            return rows;
        }
    }
}
