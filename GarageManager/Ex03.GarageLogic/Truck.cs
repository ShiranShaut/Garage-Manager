using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Truck : Vehicle
    {
        public enum eTruckMembersToInitialize
        {
            Model = 1,
            CarryingDangerousMaterials,
            MaxCarryingWeight
        }

        private bool m_CarryingDangerousMaterials;
        private float m_MaxCarryingWeight;

        public bool CarryingDangerousMaterials
        {
            get { return m_CarryingDangerousMaterials; }
            set { m_CarryingDangerousMaterials = value; }
        }

        public float MaxCarryingWeight
        {
            get { return m_MaxCarryingWeight; }
        }

        internal Truck(string i_LicenseNumber, float i_MaxWheelAirPressure, int i_NumberOfWheels)
            : base(i_LicenseNumber, i_NumberOfWheels, i_MaxWheelAirPressure)
        {
        }

        public override Dictionary<int, string> GetListOfMembersToIntialize()
        {
            Dictionary<int, string> listOfMembers = new Dictionary<int, string>();

            listOfMembers.Add((int)eTruckMembersToInitialize.Model, "Please enter the model of your truck: ");
            listOfMembers.Add((int)eTruckMembersToInitialize.CarryingDangerousMaterials, "Do you carrying dangerousmaterials? press y / n ");
            listOfMembers.Add((int)eTruckMembersToInitialize.MaxCarryingWeight, "Please enter the maximum carrying weight: ");

            return listOfMembers;
        }

        public override void SetListOfMembersToIntialize(int i_MemberIndexInEnum, string i_MemberValue)
        {
            eTruckMembersToInitialize vehicleMember = (eTruckMembersToInitialize)i_MemberIndexInEnum;
            float userChoice;

            switch (vehicleMember)
            {
                case eTruckMembersToInitialize.Model:
                    {
                        m_ModelName = i_MemberValue;
                        break;
                    }

                case eTruckMembersToInitialize.CarryingDangerousMaterials:
                    {
                        if (i_MemberValue == "y" || i_MemberValue == "Y")
                        {
                            m_CarryingDangerousMaterials = true;
                        }
                        else
                        {
                            if (i_MemberValue == "n" || i_MemberValue == "N")
                            {
                                m_CarryingDangerousMaterials = false;
                            }
                            else
                            {
                                throw new ArgumentException("The value is invalid, please try again");
                            }
                        }

                        break;
                    }

                case eTruckMembersToInitialize.MaxCarryingWeight:
                    {
                        if(float.TryParse(i_MemberValue, out userChoice))
                        {
                            if(userChoice < 0)
                            {
                                throw new ArgumentException("The value must be at least 0");
                            }
                            else
                            {
                                m_MaxCarryingWeight = userChoice;
                            }
                        }
                        else
                        {
                            throw new FormatException("The value is invalid, please try again");
                        }

                        break;
                    }
            }
        }

        public override string ToString()
        {
            string danerousMaterials = m_CarryingDangerousMaterials ? "Yes" : "No";
            string details = string.Format(
@"
The truck carrying dengerous materials? {0}
Maximum carrying weight - {1}",
danerousMaterials,
m_MaxCarryingWeight);
            
            return base.ToString() + details;
        }
    }
}