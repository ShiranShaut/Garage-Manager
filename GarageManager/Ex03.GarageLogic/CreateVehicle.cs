using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class CreateVehicle
    {
        public enum eVehicleType
        {
            FuelMotorcycle = 1,
            ElectricMotorcycle,
            FuelCar,
            ElectricCar,
            FuelTruck
        }

        // $G$ CSS-999 (-3) Public/internal methods should start with an Uppercase letter.
        internal static Vehicle createVehicle(string i_LicenseNumber, int i_VehicleType)
        {
            Vehicle newVehicle = null;
            eVehicleType vehicleType = (eVehicleType)i_VehicleType;

            switch (vehicleType)
            {
                case eVehicleType.FuelMotorcycle:
                    {
                        newVehicle = new FuelMotorcycle(i_LicenseNumber);
                        break;
                    }

                case eVehicleType.ElectricMotorcycle:
                    {
                        newVehicle = new ElectricMotorcycle(i_LicenseNumber);
                        break;
                    }

                case eVehicleType.FuelCar:
                    {
                        newVehicle = new FuelCar(i_LicenseNumber);
                        break;
                    }

                case eVehicleType.ElectricCar:
                    {
                        newVehicle = new ElectricCar(i_LicenseNumber);
                        break;
                    }

                case eVehicleType.FuelTruck:
                    {
                        newVehicle = new FuelTruck(i_LicenseNumber);
                        break;
                    }
            }

            return newVehicle;
        }
    }
}
