using System;
using System.Text;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal sealed class ElectricEngine : Engine
    {
        internal ElectricEngine(float i_BatteryMaxTime)
            : base(i_BatteryMaxTime)
        {
        }

        public override int GetEngineType()
        {
            return (int)eEngineType.Electric;
        }

        public override Dictionary<int, string> GetListOfMembersToIntialize()
        {
            Dictionary<int, string> listOfMembers = new Dictionary<int, string>();

            listOfMembers.Add((int)Engine.eEngineMembersToInitialize.CurrentEnergy, "Please enter the time left in the battery: ");

            return listOfMembers;
        }

        // $G$ DSN-001 (-3) Code duplication. except in Fuel type, Fuel and Electric Energy Sources are identical.
        internal override void UpdateEnergy(float i_AmountOfEnergyToAdd, int i_Type)
        {
            if (i_AmountOfEnergyToAdd >= 0)
            {
                if (m_CurrentEnergy + i_AmountOfEnergyToAdd <= r_MaxEnergy)
                {
                    m_CurrentEnergy += i_AmountOfEnergyToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, r_MaxEnergy - m_CurrentEnergy, "The time you entered exceeds the limit of the battery");
                }
            }
            else
            {
                throw new ArgumentException("The amount of time must be positive");
            }
        }

        public override string ToString()
        {
            string details = string.Format(
@"
Battery time left: {0}
Maximum battery time: {1}",
m_CurrentEnergy,
r_MaxEnergy);

            return base.ToString() + details;
        }
    }
}