using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        public enum eMotorcycleMembersToInitialize
        {
            Model = 1,
            EngineVolume,
            LicenseType
        }

        public enum eLicenseType
        {
            A1 = 1,
            B1,
            AA,
            BB
        }

        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public int EngineVolume
        {
            get { return m_EngineVolume; }
        }

        internal Motorcycle(string i_LicenseNumber, float i_MaxWheelAirPressure, int i_NumberOfWheels)
            : base(i_LicenseNumber, i_NumberOfWheels, i_MaxWheelAirPressure)
        {
        }

        public override Dictionary<int, string> GetListOfMembersToIntialize()
        {
            Dictionary<int, string> listOfMembers = new Dictionary<int, string>();

            listOfMembers.Add((int)eMotorcycleMembersToInitialize.Model, "Please enter the model of your motorcycle: ");
            listOfMembers.Add((int)eMotorcycleMembersToInitialize.EngineVolume, "Please enter the engine volume: ");
            listOfMembers.Add((int)eMotorcycleMembersToInitialize.LicenseType, "Please enter your license type: (A1 - 1 / B1 - 2 / AA - 3 / BB - 4)");

            return listOfMembers;
        }

        public override void SetListOfMembersToIntialize(int i_MemberIndexInEnum, string i_MemberValue)
        {
            int memberValue;
            eMotorcycleMembersToInitialize vehicleMember = (eMotorcycleMembersToInitialize)i_MemberIndexInEnum;

            switch (vehicleMember)
            {
                case eMotorcycleMembersToInitialize.Model:
                    {
                        m_ModelName = i_MemberValue;
                        break;
                    }

                case eMotorcycleMembersToInitialize.EngineVolume:
                    {
                        if (int.TryParse(i_MemberValue, out memberValue))
                        {
                            if (memberValue <= 0 || memberValue > 2000)
                            {
                                throw new ValueOutOfRangeException(0, 2000, "You entered an invalid input");
                            }
                            else
                            {
                                m_EngineVolume = memberValue;
                            }
                        }
                        else
                        {
                            throw new FormatException("The value is invalid.");
                        }

                        break;
                    }

                case eMotorcycleMembersToInitialize.LicenseType:
                    {
                        if (checkIfEnumContainThisChoice<eLicenseType>(i_MemberValue, out memberValue))
                        {
                            m_LicenseType = (eLicenseType)memberValue;
                        }
                        else
                        {
                            if (int.TryParse(i_MemberValue, out memberValue))
                            {
                                if (Enum.IsDefined(typeof(eLicenseType), memberValue))
                                {
                                    m_LicenseType = (eLicenseType)memberValue;
                                }
                                else
                                {
                                    throw new ArgumentException("There isn't such license type.");
                                }
                            }
                            else
                            {
                                throw new FormatException("The value is invalid.");
                            }
                        }

                        break;
                    }
            }
        }
        
        public override string ToString()
        {
            string details = string.Format("{0}Engine volume: {1}", Environment.NewLine, m_EngineVolume);

            return base.ToString() + details;
        }
    }
}
