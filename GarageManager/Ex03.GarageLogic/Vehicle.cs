using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly int r_NumberOfWheels;
        private readonly string r_LicenseNumber;
        private readonly float r_MaxAirPressure;
        private List<Wheel> m_WheelsCollection;
        private bool m_WheelFilledToMaximum;
        protected string m_ModelName;
        protected Engine m_Engine;

        public bool WheelsFilledToMaximum
        {
            get { return m_WheelFilledToMaximum; }
            set { m_WheelFilledToMaximum = value; }
        }

        public float EnergyPercentLeft
        {
            get { return m_Engine.EnergyPercentLeft; }
        }

        public List<Wheel> WheelsCollection
        {
            get { return m_WheelsCollection; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public string ModelName
        {
            get { return m_ModelName; }
        }

        public string LicenseNumber
        {
            get { return r_LicenseNumber; }
        }

        public int NumberOfWheels
        {
            get { return r_NumberOfWheels; }
        }

        public Vehicle(string i_LicenseNumber, int i_NumberOfWheels, float i_MaxAirPressure)
        {
            r_LicenseNumber = i_LicenseNumber;
            r_NumberOfWheels = i_NumberOfWheels;
            r_MaxAirPressure = i_MaxAirPressure;
            m_WheelsCollection = new List<Wheel>(r_NumberOfWheels);
        }

        public int GetEngineType()
        {
            return m_Engine.GetEngineType();
        }

        public void AddWheel(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            if(i_CurrentAirPressure <= r_MaxAirPressure)
            { 
                Wheel newWheel = new Wheel(i_ManufacturerName, i_CurrentAirPressure, r_MaxAirPressure);
                m_WheelsCollection.Add(newWheel);
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure, "The value must be less then maximum air pressure.");
            }
        }

        public abstract Dictionary<int, string> GetListOfMembersToIntialize();

        public abstract void SetListOfMembersToIntialize(int i_MemberIndexInEnum, string i_MemberValue);

        public Dictionary<int, string> GetEngineMembers()
        {
            return m_Engine.GetListOfMembersToIntialize();
        }

        public void SetEngineMembers(int i_MemberIndexInEnum, string i_MemberValue)
        {
            m_Engine.SetListOfMembersToIntialize(i_MemberIndexInEnum, i_MemberValue);
        }

        public void UpdateEnergy(float i_AmountToAdd, int i_Type)
        {
            m_Engine.UpdateEnergy(i_AmountToAdd, i_Type);
        }

        protected bool checkIfEnumContainThisChoice<T>(string i_Choice, out int o_ChoiceInt)
        {
            bool validChoice = false;
            string[] enumNames = Enum.GetNames(typeof(T));

            o_ChoiceInt = -1;
            for (int i = 0; i < enumNames.Length; ++i)
            {
                if (enumNames[i].ToLower() == i_Choice.ToLower())
                {
                    o_ChoiceInt = i + 1;
                    validChoice = true;
                    break;
                }
            }

            return validChoice;
        }

        public override string ToString()
        {
            StringBuilder details = new StringBuilder();
            string strFormat = string.Format(
@"
Model name: {0}
License number: {1}

Number of wheels: {2}",
m_ModelName,
r_LicenseNumber,
r_NumberOfWheels);

            details.AppendLine(strFormat);
            int i = 1;
            foreach (Wheel wheelDetails in m_WheelsCollection)
            {
                strFormat = string.Format(@"wheel #{0} - Manufacturer name: {1} | Current air pressure: {2} | Max air pressure: {3}", i, wheelDetails.ManufacturerName, wheelDetails.CurrentAirPressure, wheelDetails.MaxAirPressure);
                details.AppendLine(strFormat);
                i++;
            }

            return details.ToString() + m_Engine.ToString();
        }
    }
}