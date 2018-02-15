using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public enum eEngineMembersToInitialize
        {
            CurrentEnergy = 1
        }

        internal enum eEngineType
        {
            Fuel = 1,
            Electric = 2
        }

        protected readonly float r_MaxEnergy;
        protected float m_CurrentEnergy;

        internal float MaxEnergy
        {
            get { return r_MaxEnergy; }
        }

        internal float CurrentEnergy
        {
            get { return m_CurrentEnergy; }
        }

        internal float EnergyPercentLeft
        {
            get { return (m_CurrentEnergy * 100) / r_MaxEnergy; }
        }

        internal Engine(float i_MaxEnergy)
        {
            r_MaxEnergy = i_MaxEnergy;
        }

        internal abstract void UpdateEnergy(float i_AmountOfEnergyToAdd, int i_Type);

        public virtual Dictionary<int, string> GetListOfMembersToIntialize()
        {
            Dictionary<int, string> listOfMembers = new Dictionary<int, string>();

            listOfMembers.Add((int)eEngineMembersToInitialize.CurrentEnergy, "Please enter the current energy: ");

            return listOfMembers;
        }

        public virtual void SetListOfMembersToIntialize(int i_MemberIndexInEnum, string i_MemberValue)
        {
            float userInput;
            eEngineMembersToInitialize vehicleMember = (eEngineMembersToInitialize)i_MemberIndexInEnum;

            switch (vehicleMember)
            {
                case eEngineMembersToInitialize.CurrentEnergy:
                    {
                        if (float.TryParse(i_MemberValue, out userInput))
                        {
                            if (userInput >= 0)
                            {
                                if (userInput <= r_MaxEnergy)
                                {
                                    m_CurrentEnergy = userInput;
                                }
                                else
                                {
                                    throw new ValueOutOfRangeException(0, r_MaxEnergy, "Out of range");
                                }
                            }
                            else
                            {
                                throw new ArgumentException("The value must be 0 at least");
                            }
                        }
                        else
                        {
                            throw new FormatException("You entered a wrong input.");
                        }

                        break;
                    }
            }
        }

        public abstract int GetEngineType();

        public override string ToString()
        {
            string details = string.Format("{0}Energy percent left: {1}%", Environment.NewLine, EnergyPercentLeft);

            return details;
        }
    }
}