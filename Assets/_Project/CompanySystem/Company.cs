using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadSystem;
using System;

namespace CompanySystem
{
    public abstract class Company
    {
        public readonly string Name;
        private int _realationship;
        public int Realationship => _realationship;
        public readonly City City;

        private Action OnRealtionshipChange;

        protected Company(string name, City city)
        {
            Name = name;
            City = city;
            _realationship = 0;
        }

        public void RegisterOnRealtionshipChange(Action action) => OnRealtionshipChange += action;
        public void UnregisterOnRealtionshipChange(Action action) => OnRealtionshipChange -= action;

        private void RealtionshipChange(int change)
        {
            _realationship += change;
            OnRealtionshipChange?.Invoke();
        }




    }
}
