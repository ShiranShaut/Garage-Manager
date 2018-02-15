using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal sealed class FuelEngine : Engine
    {
        private enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler
        }

        private readonly eFuelType r_FuelType;

        public int FuelType
        {
            get { return (int)r_FuelType; }
        }

        public override int GetEngineType()
        {
            return (int)eEngineType.Fuel;
        }

        internal FuelEngine(float i_MaxLiterFuel, int i_FuelType)
            : base(i_MaxLiterFuel)
        {
            r_FuelType = (eFuelType)i_FuelType;
        }

        public bool CheckIfUserFuelTypeChoiceIsLegal(int i_UserVehicleTypeChoice)
        {
            return checkIfUserEnumChoiceLegal<eFuelType>(i_UserVehicleTypeChoice);
        }

        private bool checkIfUserEnumChoiceLegal<T>(int i_EnumChoise)
        {
            return Enum.IsDefined(typeof(T), i_EnumChoise);
        }

        internal override void UpdateEnergy(float i_AmountOfFuelToAdd, int i_FuelType)
        {
            if (CheckIfUserFuelTypeChoiceIsLegal(i_FuelType))
            {
                eFuelType fuelType = (eFuelType)i_FuelType;

                if (fuelType == r_FuelType)
                {
                    if (i_AmountOfFuelToAdd >= 0)
                    {
                        if (m_CurrentEnergy + i_AmountOfFuelToAdd <= r_MaxEnergy)
                        {
                            m_CurrentEnergy += i_AmountOfFuelToAdd;
                        }
                        else
                        {
                            throw new ValueOutOfRangeException(0, r_MaxEnergy - m_CurrentEnergy, "The fuel you entered exceeds the limit of the fuel tank");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("The amount of fuel must be positive");
                    }
                }
                else
                {
                    throw new ArgumentException("The types of fuel not match");
                }
            }
        }

        public override Dictionary<int, string> GetListOfMembersToIntialize()
        {
            Dictionary<int, string> listOfMembers = new Dictionary<int, string>();

            listOfMembers.Add((int)Engine.eEngineMembersToInitialize.CurrentEnergy, "Please enter the amount of fuel in your tank: ");

            return listOfMembers;
        }

        public override string ToString()
        {
            string details = string.Format(
@"
Fuel type: {0}
Current liter fuel: {1}
Maximum liter fuel: {2}",
r_FuelType.ToString(),
m_CurrentEnergy,
r_MaxEnergy);

            return details + base.ToString();
        }
    }
}