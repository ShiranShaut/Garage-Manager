using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        public enum eCarMembersToInitialize
        {
            Model = 1,
            NumberOfDoors,
            CarColor
        }

        public enum eNumberOfDoors
        {
            TwoDoors = 2,
            ThreeDoors = 3,
            FourDoors = 4,
            FiveDoor = 5
        }

        public enum eCarColor
        {
            Green = 1,
            Silver = 2,
            White = 3,
            Black = 4
        }

        private eNumberOfDoors m_NumberOfDoors;
        private eCarColor m_CarColor;

        public eNumberOfDoors NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }

        public eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        internal Car(string i_LicenseNumber, float i_MaxWheelAirPressure, int i_NumberOfWheels)
            : base(i_LicenseNumber, i_NumberOfWheels, i_MaxWheelAirPressure)
        {
        }

        public override Dictionary<int, string> GetListOfMembersToIntialize()
        {
            Dictionary<int, string> listOfMembers = new Dictionary<int, string>();

            listOfMembers.Add((int)eCarMembersToInitialize.Model, "Please enter the model of your car: ");
            listOfMembers.Add((int)eCarMembersToInitialize.CarColor, "Please enter the color of the car: (green-1 / silver-2 / white-3 / black-4) ");
            listOfMembers.Add((int)eCarMembersToInitialize.NumberOfDoors, "Please enter the amount of doors in your car: ");

            return listOfMembers;
        }

        public override void SetListOfMembersToIntialize(int i_MemberIndexInEnum, string i_MemberValue)
        {
            eCarMembersToInitialize vehicleMember = (eCarMembersToInitialize)i_MemberIndexInEnum;
            int memberValue;

            switch (vehicleMember)
            {
                case eCarMembersToInitialize.Model:
                    {
                        m_ModelName = i_MemberValue;
                        break;
                    }

                case eCarMembersToInitialize.CarColor:
                    {
                        if (checkIfEnumContainThisChoice<eCarColor>(i_MemberValue, out memberValue))
                        {
                            m_CarColor = (eCarColor)memberValue;
                        }
                        else
                        {
                            if (int.TryParse(i_MemberValue, out memberValue))
                            {
                                if (Enum.IsDefined(typeof(eCarColor), memberValue))
                                {
                                    m_CarColor = (eCarColor)memberValue;
                                }
                                else
                                {
                                    throw new ArgumentException("There isn't such color.");
                                }
                            }
                            else
                            {
                                throw new FormatException("The value is invalid.");
                            }
                        }

                        break;
                    }

                case eCarMembersToInitialize.NumberOfDoors:
                    {
                        if (checkIfEnumContainThisChoice<eNumberOfDoors>(i_MemberValue, out memberValue))
                        {
                            m_NumberOfDoors = (eNumberOfDoors)memberValue;
                        }
                        else
                        {
                            if (int.TryParse(i_MemberValue, out memberValue))
                            {
                                if (Enum.IsDefined(typeof(eNumberOfDoors), memberValue))
                                {
                                    m_NumberOfDoors = (eNumberOfDoors)memberValue;
                                }
                                else
                                {
                                    throw new ArgumentException("There isn't such option of doors.");
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
            string details = string.Format(
@"
Number of doors: {0}
Car color: {1}",
 (int)m_NumberOfDoors,
 m_CarColor);

            return base.ToString() + details;
        }
    }
}
