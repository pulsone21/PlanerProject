using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;
using Utilities;

namespace ContractSystem
{
    [System.Serializable]
    public class Route
    {
        [SerializeField] private Queue<RouteDestination> _destinations;
        [SerializeField] private List<TransportContract> Contracts;
        public Route()
        {
            _destinations = new Queue<RouteDestination>();
            Contracts = new List<TransportContract>();
        }
        public Queue<RouteDestination> Destinations => _destinations;
        public bool HasDestination => _destinations != null && _destinations.Count > 0;
        public void EnqueueDestination(RouteDestination dest)
        {
            _destinations.Enqueue(dest);
        }
        public RouteDestination DequeueDestination()
        {
            if (_destinations.TryDequeue(out RouteDestination dest)) return dest;
            return default;
        }
        public void AddContract(TransportContract contract)
        {
            Contracts.Add(contract);
            contract.SetState(TransportContract.State.assigned);
            contract.OnClose += () => Contracts.Remove(contract);
            _destinations.Enqueue(contract.StartDestination);
            _destinations.Enqueue(contract.TargetDestination);
        }
        public void RemoveContract(TransportContract contract)
        {
            List<RouteDestination> newList = new List<RouteDestination>();
            foreach (RouteDestination dest in _destinations)
            {
                if (dest.Contract != contract)
                {
                    newList.Add(dest);
                }
                else
                {
                    Debug.Log($"Destination {dest} is from Contract {contract} and was not added to new Queue");
                }
            }
            contract.ClearOnClose();
            Contracts.Remove(contract);
            _destinations = newList.ToQueue();
        }
        public void RearrangeRoute(Queue<RouteDestination> Queue) => _destinations = Queue;

    }
}
