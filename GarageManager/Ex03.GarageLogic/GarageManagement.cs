using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageManagement
    {
        private Dictionary<string, VehicleInGarage> m_VehiclesInGarage = new Dictionary<string, VehicleInGarage>();
        private VehicleInGarage m_CurrentVehicleInGarage;

        public Dictionary<string, VehicleInGarage> VehiclesInGarage
        {
            get { return m_VehiclesInGarage; }
        }

        public void InsertVehicleToGarage(string i_OwnerName, string i_OwnerPhoneNumber, string i_LicenseNumber, int i_VehicleType)
        {
            if (!CheckIfLicenseNumberInGarage(i_LicenseNumber))
            {
                Vehicle newVehicle = CreateVehicle.createVehicle(i_LicenseNumber, i_VehicleType);
                m_CurrentVehicleInGarage = new VehicleInGarage(newVehicle, i_OwnerName, i_OwnerPhoneNumber);

                m_VehiclesInGarage.Add(i_LicenseNumber, m_CurrentVehicleInGarage);
            }
            else
            {
                throw new ArgumentException("This vehicle already in the garage.");
            }
        }

        public int GetEngineType(string i_LicenseNumber)
        {
            if (CheckIfLicenseNumberInGarage(i_LicenseNumber))
            {
                m_CurrentVehicleInGarage = m_VehiclesInGarage[i_LicenseNumber];
                return m_CurrentVehicleInGarage.Vehicle.GetEngineType();
            }
            else
            {
                throw new ArgumentException("This vehicle not in the garage.");
            }
        }

        public Dictionary<int, string> GetEngineMembers()
        {
            return m_CurrentVehicleInGarage.Vehicle.GetEngineMembers();
        }

        public void SetEngineMembers(int i_MemberIndexInEnum, string i_MemberValue)
        {
            m_CurrentVehicleInGarage.Vehicle.SetEngineMembers(i_MemberIndexInEnum, i_MemberValue);
        }

        public string GetTypesOfVehicles()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int index = 1;

            stringBuilder.AppendLine();
            foreach (CreateVehicle.eVehicleType currentType in Enum.GetValues(typeof(CreateVehicle.eVehicleType)))
            {
                stringBuilder.AppendFormat("{0} - press {1}{2}", currentType.ToString(), index, Environment.NewLine);
                index++;
            }

            return stringBuilder.ToString();
        }

        public void UpdateEnergy(string i_LicenseNumber, float i_AmountToAdd, int i_Type)
        {
            m_CurrentVehicleInGarage = m_VehiclesInGarage[i_LicenseNumber];
            m_CurrentVehicleInGarage.Vehicle.UpdateEnergy(i_AmountToAdd, i_Type);
        }
        
        public string GetStatusOfVehicles()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int index = 1;

            foreach (VehicleInGarage.eVehicleStatus currentType in Enum.GetValues(typeof(VehicleInGarage.eVehicleStatus)))
            {
                stringBuilder.AppendFormat("{0} - press {1}{2}", currentType.ToString(), index, Environment.NewLine);
                index++;
            }

            return stringBuilder.ToString();
        }

        public void CheckIfUserVehicleTypeChoiceIsLegal(int i_UserVehicleTypeChoice)
        {
            if(!checkIfUserEnumChoiceLegal<CreateVehicle.eVehicleType>(i_UserVehicleTypeChoice))
            {
                throw new ValueOutOfRangeException(1, 5, "This choice not include in options.");
            }
        }

        public void CheckIfUserVehicleStatusChoiceIsLegal(int i_UserVehicleTypeChoice)
        {
            if (!checkIfUserEnumChoiceLegal<VehicleInGarage.eVehicleStatus>(i_UserVehicleTypeChoice))
            {
                throw new ValueOutOfRangeException(1, 3, "This choice not include in options.");
            }
        }

        private bool checkIfUserEnumChoiceLegal<T>(int i_EnumChoise)
        {
            return Enum.IsDefined(typeof(T), i_EnumChoise);
        }

        public void AddWheel(string i_LicenseNumber, string i_ManufacturerName, float i_CurrentAirPressure)
        {
            m_CurrentVehicleInGarage = m_VehiclesInGarage[i_LicenseNumber];
            m_CurrentVehicleInGarage.Vehicle.AddWheel(i_ManufacturerName, i_CurrentAirPressure);
        }

        public void AddWheels(string i_LicenseNumber, string i_ManufacturerName, float i_CurrentAirPressure)
        {
            m_CurrentVehicleInGarage = m_VehiclesInGarage[i_LicenseNumber];
            for (int i = 0; i < m_CurrentVehicleInGarage.Vehicle.NumberOfWheels; ++i)
            {
                m_CurrentVehicleInGarage.Vehicle.AddWheel(i_ManufacturerName, i_CurrentAirPressure);
            }
        }

        public virtual Dictionary<int, string> GetListOfMembersToIntialize(string i_LicenseNumber)
        {
            m_CurrentVehicleInGarage = m_VehiclesInGarage[i_LicenseNumber];

            return m_CurrentVehicleInGarage.Vehicle.GetListOfMembersToIntialize();
        }

        public virtual void SetListOfMembersToIntialize(string i_LicenseNumber, int i_MemberIndexInEnum, string i_MemberValue)
        {
            m_CurrentVehicleInGarage = m_VehiclesInGarage[i_LicenseNumber];
            m_CurrentVehicleInGarage.Vehicle.SetListOfMembersToIntialize(i_MemberIndexInEnum, i_MemberValue);
        }

        public string ShowVehiclesInGarage()
        {
            StringBuilder ListOfVehiclesInGarage = new StringBuilder();
            string strFormat = null;

            if(m_VehiclesInGarage.Count > 0)
            {
                foreach (VehicleInGarage currentVehicle in m_VehiclesInGarage.Values)
                {
                    strFormat = string.Format("License number: {0}  Vehicle status: {1}", currentVehicle.Vehicle.LicenseNumber, currentVehicle.VehicleStatus.ToString());
                    ListOfVehiclesInGarage.AppendLine(strFormat);
                }
            }
            else
            {
                ListOfVehiclesInGarage.AppendLine("There isn't vehicles in the garage.");
            }

            return ListOfVehiclesInGarage.ToString();
        }

        public string ShowVehiclesInGarageByType(int i_VehicleStatus)
        {
            StringBuilder ListOfVehiclesInGarage = new StringBuilder();
            string strFormat = null;
            int vehicleInThisStatus = 0;

            if (m_VehiclesInGarage.Count > 0)
            {
                foreach (VehicleInGarage currentVehicle in m_VehiclesInGarage.Values)
                {
                    if ((int)currentVehicle.VehicleStatus == i_VehicleStatus)
                    {
                        strFormat = string.Format("License number: {0}  Vehicle status: {1}", currentVehicle.Vehicle.LicenseNumber, currentVehicle.VehicleStatus.ToString());

                        ListOfVehiclesInGarage.AppendLine(strFormat);
                        vehicleInThisStatus++;
                    }
                }
                
                if(vehicleInThisStatus == 0)
                {
                    ListOfVehiclesInGarage.AppendLine("There isn't vehicles in that status in the garage.");
                }
            }
            else
            {
                ListOfVehiclesInGarage.AppendLine("There isn't vehicles in the garage.");
            }

            return ListOfVehiclesInGarage.ToString();
        }

        public void ChangeVehicleInGarageStatus(string i_LicenseNumber, int i_NewStatus)
        {
            if (CheckIfLicenseNumberInGarage(i_LicenseNumber))
            {
                VehicleInGarage.eVehicleStatus newStatus = (VehicleInGarage.eVehicleStatus)i_NewStatus;

                m_CurrentVehicleInGarage = m_VehiclesInGarage[i_LicenseNumber];
                m_CurrentVehicleInGarage.VehicleStatus = newStatus;
            }
            else
            {
                throw new ArgumentException("This vehicle not in the garage.");
            }
        }

        public void FillAirPressureInWheelsToMaximum(string i_LicenseNumber)
        {
            if (CheckIfLicenseNumberInGarage(i_LicenseNumber))
            {
                m_CurrentVehicleInGarage = m_VehiclesInGarage[i_LicenseNumber];
                if (!m_CurrentVehicleInGarage.Vehicle.WheelsFilledToMaximum)
                {
                    foreach (Wheel wheelToChange in m_CurrentVehicleInGarage.Vehicle.WheelsCollection)
                    {
                        wheelToChange.FillAirPressureToMaximum();
                    }

                    m_CurrentVehicleInGarage.Vehicle.WheelsFilledToMaximum = true;
                }
                else
                {
                    throw new ArgumentException("The wheels in this vehicle already filled to maximum.");
                }
            }
            else
            {
                throw new ArgumentException("This vehicle not in the garage.");
            }
        }

        public string ShowVehicleDetails(string i_LicenseNumber)
        {
            if (CheckIfLicenseNumberInGarage(i_LicenseNumber))
            {
                m_CurrentVehicleInGarage = m_VehiclesInGarage[i_LicenseNumber];
                return m_CurrentVehicleInGarage.ToString();
            }
            else
            {
                throw new ArgumentException("The License number not found in the garage.");
            }
        }

        public bool CheckIfLicenseNumberInGarage(string i_LicenseNumber)
        {
            return m_VehiclesInGarage.ContainsKey(i_LicenseNumber);
        }

        public bool SetCurrentVehicleInGarage(string i_LicenseNumber)
        {
            bool vehicleExsist = false;

            if(CheckIfLicenseNumberInGarage(i_LicenseNumber))
            {
                m_CurrentVehicleInGarage = m_VehiclesInGarage[i_LicenseNumber];
                vehicleExsist = true;
            }

            return vehicleExsist;
        }
    }
}