using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal sealed class FuelTruck : Truck
    {
        private enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler
        }

        private const eFuelType k_FuelType = eFuelType.Soler;
        private const int k_NumberOfWheels = 12;
        private const float k_MaxAirPressure = 34f;
        private const float k_MaxLiterFuel = 130;

        internal FuelTruck(string i_LicenseNumber) : base(i_LicenseNumber, k_MaxAirPressure, k_NumberOfWheels)
        {
            m_Engine = new FuelEngine(k_MaxLiterFuel, (int)k_FuelType);
        }

        public override string ToString()
        {
            string details = string.Format("{0}Vehicle type: Fuel truck{0}", Environment.NewLine);

            return base.ToString() + details;
        }
    }
}