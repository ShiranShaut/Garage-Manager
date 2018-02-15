using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal sealed class FuelMotorcycle : Motorcycle
    {
        public enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler
        }

        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const float k_MaxLiterFuel = 5.5f;
        private const int k_NumberOfWheels = 2;
        private const float k_MaxAirPressure = 28f;

        internal FuelMotorcycle(string i_LicenseNumber) : base(i_LicenseNumber, k_MaxAirPressure, k_NumberOfWheels)
        {
            m_Engine = new FuelEngine(k_MaxLiterFuel, (int)k_FuelType);
        }

        public override string ToString()
        {
            string details = string.Format("{0}Vehicle type: Fuel motorcycle{0}", Environment.NewLine);

            return base.ToString() + details;
        }
    }
}
