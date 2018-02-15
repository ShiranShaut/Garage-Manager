using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        public enum eVehicleStatus
        {
            InRepair = 1,
            DoneRepair = 2,
            Paid = 3
        }

        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;
        private Vehicle m_Vehicle;

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }

        public string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
            set { m_OwnerPhoneNumber = value; }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
        }

        public VehicleInGarage(Vehicle i_VehicleToInsert, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_VehicleStatus = eVehicleStatus.InRepair;
            m_Vehicle = i_VehicleToInsert;
        }

        public override string ToString()
        {
            string details = string.Format(
@"
Owner name: {0}
Owner phone number: {1}
Vehicle status in garage: {2}",
m_OwnerName,
m_OwnerPhoneNumber,
m_VehicleStatus);

            return details + m_Vehicle.ToString();
        }
    }
}