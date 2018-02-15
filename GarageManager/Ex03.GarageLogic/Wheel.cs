using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;

        public string ManufacturerName
        {
            get { return r_ManufacturerName; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public void AddAirToWheel(float i_AirAmount)
        {
            if (i_AirAmount >= 0)
            {
                if (m_CurrentAirPressure + i_AirAmount <= r_MaxAirPressure)
                {
                    m_CurrentAirPressure += i_AirAmount;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, r_MaxAirPressure - m_CurrentAirPressure, "You try to add too much air");
                }
            }
            else
            {
                throw new ArgumentException("The amount of air must be positive number.");
            }
        }

        public void FillAirPressureToMaximum()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }
    }
}
