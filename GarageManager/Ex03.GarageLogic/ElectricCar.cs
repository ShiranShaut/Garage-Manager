using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal sealed class ElectricCar : Car
    {
        private const int k_NumberOfWheels = 4;
        private const float k_MaxAirPressure = 32f;
        private const float k_BatteryMaxTime = 2.8f;

        internal ElectricCar(string i_LicenseNumber) : base(i_LicenseNumber, k_MaxAirPressure, k_NumberOfWheels)
        {
            m_Engine = new ElectricEngine(k_BatteryMaxTime);
        }

        public override string ToString()
        {
            string details = string.Format("{0}Vehicle type: Electric car{0}", Environment.NewLine);

            return base.ToString() + details;
        }
    }
}
