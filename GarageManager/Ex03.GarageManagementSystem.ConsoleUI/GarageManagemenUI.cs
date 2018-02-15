using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.GarageManagementSystem.ConsoleUI
{
    public class GarageManagemenUI
    {
        public enum eMainMenu
        {
            InsertVehicleToGarage = 1,
            ShowListOfVehiclesInGarage,
            ChangeVehicleStatus,
            FilWheelAirPressureToMaximum,
            IncreaseEnergyInVehicle,
            ShowVehicleDetails,
            Exit
        }

        private enum eEngineType
        {
            Fuel = 1,
            Electric = 2
        }

        private GarageManagement m_GarageManger = new GarageManagement();

        public void OpenGarage()
        {
            int userChoice;
            bool exit = false;

            do
            {
                try
                {
                    PrintMainManu();
                    userChoice = getUserChoice();
                    exit = RunUserChoice(userChoice);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(@"{3}{0}, The value must be between {1} - {2}{3}", ex.Message, ex.MinValue, ex.MaxValue, Environment.NewLine);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (!exit);
        }

        private int getUserChoice()
        {
            int userChoice;

            if (int.TryParse(Console.ReadLine(), out userChoice))
            {
                if (!(userChoice >= 1 && userChoice <= 7))
                {
                    throw new ValueOutOfRangeException(1, 7, "This is not a legal option.");
                }
                else
                {
                    return userChoice;
                }
            }
            else
            {
                throw new FormatException("You entered illegal input, try again please.");
            }
        }

        public void PrintMainManu()
        {
            Console.Clear();
            Console.WriteLine(@"Main Menu: 
=============================
1 - Insert new vehicle to the garage
2 - Show list of the vehicles at the garage
3 - Change vehicle status in the garage
4 - Fill wheels air pressure to maximum
5 - Increase energy in vehicle (Refueling / Charge Battery)
6 - Show full details on vehicle
7 - Exit");
        }

        public bool RunUserChoice(int i_UserChoice)
        {
            bool exitStatus = false;
            eMainMenu userChoice = (eMainMenu)i_UserChoice;

            switch (userChoice)
            {
                case eMainMenu.InsertVehicleToGarage:
                    {
                        insertNewVehicleToGarage();
                        break;
                    }

                case eMainMenu.ShowListOfVehiclesInGarage:
                    {
                        showListOfVehiclesInGarage();
                        break;
                    }

                case eMainMenu.ChangeVehicleStatus:
                    {
                        changeVehicleStatus();
                        break;
                    }

                case eMainMenu.FilWheelAirPressureToMaximum:
                    {
                        filWheelAirPressureToMaximum();
                        break;
                    }

                case eMainMenu.IncreaseEnergyInVehicle:
                    {
                        increaseEnergyInVehicle();
                        break;
                    }

                case eMainMenu.ShowVehicleDetails:
                    {
                        showVehicleDetails();
                        break;
                    }

                case eMainMenu.Exit:
                    {
                        Console.Clear();
                        Console.WriteLine(@"We will always be happy to serve you,
Come back soon!
Goodbye :)
");
                        exitStatus = true;
                        break;
                    }
            }

            return exitStatus;
        }

        private void increaseEnergyInVehicle()
        {
            string licenseNumber;
            int engineType;

            Console.Clear();
            licenseNumber = insertLicenseNumber();
            try
            {
                engineType = m_GarageManger.GetEngineType(licenseNumber);
                if ((eEngineType)engineType == eEngineType.Electric)
                {
                    chargeBattery(licenseNumber);
                }

                if ((eEngineType)engineType == eEngineType.Fuel)
                {
                    refueling(licenseNumber);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press anything to continue: ");
                Console.ReadLine();
            }
        }

        private void chargeBattery(string i_LicenseNumber)
        {
            float amountOfBattery;
            bool validInput = false;

            do
            {
                try
                {
                    amountOfBattery = getAmountOfBatteryToCharge();
                    m_GarageManger.UpdateEnergy(i_LicenseNumber, amountOfBattery, -1);
                    Console.WriteLine("{0}The battery was charged successfully.{0}", Environment.NewLine);
                    validInput = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("The value must be between {0} to {1}", ex.MinValue, ex.MaxValue);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (!validInput);

            Console.WriteLine("Press anything to continue: ");
            Console.ReadLine();
        }

        private float getAmountOfBatteryToCharge()
        {
            float amountOfBattery;
            bool validInput = false;

            do
            {
                Console.WriteLine("Please enter the amount of batrry to charge: ");
                if (float.TryParse(Console.ReadLine(), out amountOfBattery))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("invalid input, please try again.");
                }
            }
            while (!validInput);

            return amountOfBattery;
        }

        private void refueling(string i_LicenseNumber)
        {
            int fuelType;
            float amountOfFuel;
            bool validInput = false;

            do
            {
                try
                {
                    amountOfFuel = getAmountOfFuelToAdd();
                    fuelType = getFuelTypeFromUser();
                    m_GarageManger.UpdateEnergy(i_LicenseNumber, amountOfFuel, fuelType);
                    Console.WriteLine("{0}The fuel added to the tank successfully.{0}", Environment.NewLine);
                    validInput = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("The value must be between {0} to {1}", ex.MinValue, ex.MaxValue);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (!validInput);

            Console.WriteLine("Press anything to continue: ");
            Console.ReadLine();
        }

        private float getAmountOfFuelToAdd()
        {
            float amountOfFuel;
            bool validInput = false;

            do
            {
                Console.WriteLine("Please enter the amount of fuel to add to the tank: ");
                if (float.TryParse(Console.ReadLine(), out amountOfFuel))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("invalid input, please try again.");
                }
            }
            while (!validInput);

            return amountOfFuel;
        }

        private int getFuelTypeFromUser()
        {
            int userChoice;
            bool validInput = false;

            do
            {
                showFuelTypeMsg();
                if (int.TryParse(Console.ReadLine(), out userChoice))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input, please try again.");
                }
            }
            while (!validInput);

            return userChoice;
        }

        private void showFuelTypeMsg()
        {
            string fuelTypeMsg = string.Format(@"Please choose the fuel type: 
Octan95 - press 1,
Octan96 - press 2,
Octan98 - press 3,
Soler - press 4");

            Console.WriteLine(fuelTypeMsg);
        }

        private void filWheelAirPressureToMaximum()
        {
            string licenseNumber = null;

            Console.Clear();
            try
            {
                licenseNumber = insertLicenseNumber();
                m_GarageManger.FillAirPressureInWheelsToMaximum(licenseNumber);
                Console.WriteLine("{0}The wheels are filled to maximum.{0}", Environment.NewLine);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Press anything to continue: ");
            Console.ReadLine();
        }

        private string insertLicenseNumber()
        {
            string licenseNumber;

            do
            {
                Console.WriteLine("Insert license number please: ");
                licenseNumber = Console.ReadLine();
            }
            while (licenseNumber == string.Empty);

            return licenseNumber;
        }

        private void changeVehicleStatus()
        {
            Console.Clear();
            string licenseNumber = insertLicenseNumber();
            int status = getVehicleStatusFromUser();

            try
            {
                m_GarageManger.ChangeVehicleInGarageStatus(licenseNumber, status);
                Console.WriteLine("{0}The vehicle status was changed successfully.{0}", Environment.NewLine);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("The value must be between {0} to {1}", ex.MinValue, ex.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Press anything to continue: ");
            Console.ReadLine();
        }

        private void insertNewVehicleToGarage()
        {
            try
            {
                Console.Clear();
                string licenseNumber = insertLicenseNumber();

                Console.WriteLine("Insert owner name please: ");
                string ownerName = Console.ReadLine();
                string ownerPhoneNumber = insertPhoneNumber();
                int vehicleType = getVehicleTypeFromUser();

                m_GarageManger.InsertVehicleToGarage(ownerName, ownerPhoneNumber, licenseNumber, vehicleType);
                addWheels(licenseNumber);
                initializeMembers(licenseNumber);
                initializeEngine();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void addWheels(string i_LicenseNumber)
        {
            bool validInput = false;
            int userChoice;

            Console.Clear();
            do
            {
                Console.WriteLine(@"Insert each wheel separately - press 1
Insert all wheels together - press 2");
                validInput = int.TryParse(Console.ReadLine(), out userChoice);
                if (!validInput || (userChoice != 1 && userChoice != 2))
                {
                    Console.WriteLine("you entered invalid input, please try again.");
                    validInput = false;
                }
            }
            while (!validInput);

            if(userChoice == 1)
            {
                addWheelsOneByOne(i_LicenseNumber);
            }
            else
            {
                addAllWheelsTogether(i_LicenseNumber);
            }
        }

        private string insertPhoneNumber()
        {
            bool validInput = false;
            string ownerPhoneNumber = null;
            double phoneNumber;

            do
            {
                Console.WriteLine("Insert owner phone number please: ");
                ownerPhoneNumber = Console.ReadLine();
                validInput = double.TryParse(ownerPhoneNumber, out phoneNumber);
                if(!validInput || phoneNumber < 0)
                {
                    Console.WriteLine("You entered illgal phone number, please try again.{0}", Environment.NewLine);
                    validInput = false;
                }
            }
            while (!validInput);

            return ownerPhoneNumber;
        }

        private void addAllWheelsTogether(string i_LicenseNumber)
        {
            string manufacturerName = insertManufacturerName();
            float currentAirPressure;
            bool validInput = false;

            do
            {
                try
                {
                    currentAirPressure = insertAirPressure();
                    m_GarageManger.AddWheels(i_LicenseNumber, manufacturerName, currentAirPressure);
                    validInput = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("{0}{1}", Environment.NewLine, ex.Message);
                    Console.WriteLine("The value must be between: {0} to {1}{2}", ex.MinValue, ex.MaxValue, Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (!validInput);
        }

        private float insertAirPressure()
        {
            float currentAirPressure;
            bool validInput = false;

            do
            {
                Console.WriteLine("Please enter the current air pressure in the wheels: ");
                if (float.TryParse(Console.ReadLine(), out currentAirPressure))
                {
                    if (currentAirPressure <= 0)
                    {
                        Console.WriteLine("The air pressure must be positive number");
                    }
                    else
                    {
                        validInput = true;
                    }
                }
                else
                {
                    Console.WriteLine("You entered an invalid value, please try again");
                }
            }
            while (!validInput);

            return currentAirPressure;
        }

        private void addWheelsOneByOne(string i_LicenseNumber)
        {
            string manufacturerName;
            float currentAirPressure;
            bool validInput = false;
            int numberOfWheels = m_GarageManger.VehiclesInGarage[i_LicenseNumber].Vehicle.NumberOfWheels;

            for (int i = 0; i < numberOfWheels; ++i)
            {
                Console.Clear();
                Console.WriteLine("Wheel #{0}:", i + 1);
                validInput = false;
                manufacturerName = insertManufacturerName();
                do
                {
                    currentAirPressure = insertAirPressure();
                    try
                    {
                        m_GarageManger.AddWheel(i_LicenseNumber, manufacturerName, currentAirPressure);
                        validInput = true;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine("{0}{1}", Environment.NewLine, ex.Message);
                        Console.WriteLine("The value must be between: {0} to {1}{2}", ex.MinValue, ex.MaxValue, Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                while (!validInput);
            }
        }

        private string insertManufacturerName()
        {
            string manufacturerName;

            do
            {
                Console.WriteLine("Please enter manufacturer name: ");
                manufacturerName = Console.ReadLine();
            }
            while (manufacturerName == string.Empty);

            return manufacturerName;
        }

        private int getVehicleTypeFromUser()
        {
            int userChoice;
            bool validInput = false;

            Console.WriteLine("Choose the type of your vehicle please: ");
            do
            {
                Console.WriteLine(m_GarageManger.GetTypesOfVehicles());
                if (int.TryParse(Console.ReadLine(), out userChoice))
                {
                    try
                    {
                        m_GarageManger.CheckIfUserVehicleTypeChoiceIsLegal(userChoice);
                        validInput = true;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("The value must be between {0} to {1}", ex.MinValue, ex.MaxValue);
                    }
                }
                else
                {
                    Console.WriteLine("You enered an invalid input, please try again.");
                }
            }
            while (!validInput);

            return userChoice;
        }

        private int getVehicleStatusFromUser()
        {
            int userChoice;
            bool validInput = false;

            do
            {
                Console.WriteLine(m_GarageManger.GetStatusOfVehicles());
                if (int.TryParse(Console.ReadLine(), out userChoice))
                {
                    try
                    {
                        m_GarageManger.CheckIfUserVehicleStatusChoiceIsLegal(userChoice);
                        validInput = true;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("The value must be between {0} to {1}", ex.MinValue, ex.MaxValue);
                    }
                }
                else
                {
                    Console.WriteLine("You entered an invalid input, please try again.");
                }
            }
            while (!validInput);

            return userChoice;
        }

        private void initializeEngine()
        {
            Dictionary<int, string> members = m_GarageManger.GetEngineMembers();
            bool isValid;

            for (int i = 1; i <= members.Count; ++i)
            {
                isValid = false;
                Console.WriteLine(members[i]);
                do
                {
                    try
                    {
                        m_GarageManger.SetEngineMembers(i, Console.ReadLine());
                        isValid = true;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("The value must be between {0} to {1}", ex.MinValue, ex.MaxValue);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                while (!isValid);
            }
        }

        private void initializeMembers(string i_LicenseNumber)
        {
            Dictionary<int, string> members = m_GarageManger.GetListOfMembersToIntialize(i_LicenseNumber);
            bool isValid;

            Console.Clear();
            for (int i = 1; i <= members.Count; ++i)
            {
                isValid = false;
                Console.WriteLine(members[i]);
                do
                {
                    try
                    {
                        m_GarageManger.SetListOfMembersToIntialize(i_LicenseNumber, i, Console.ReadLine());
                        isValid = true;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("The value must be between {0} to {1}", ex.MinValue, ex.MaxValue);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                while (!isValid);
            }
        }

        private void showListOfVehiclesInGarage()
        {
            string listToPrint = null;
            int userChoice;
            bool validInput = false;

            Console.Clear();
            do
            {
                Console.WriteLine(@"Do you want the list filter by status?
YES - press 1
NO - press 2");

                if (int.TryParse(Console.ReadLine(), out userChoice))
                {
                    if (userChoice != 1 && userChoice != 2)
                    {
                        Console.WriteLine("This choice not legal.");
                    }
                    else
                    {
                        validInput = true;
                        switch (userChoice)
                        {
                            case 1:
                                {
                                    int filteredStatus = getVehicleStatusFromUser();

                                    listToPrint = m_GarageManger.ShowVehiclesInGarageByType(filteredStatus);
                                    break;
                                }

                            case 2:
                                {
                                    listToPrint = m_GarageManger.ShowVehiclesInGarage();
                                    break;
                                }
                        }

                        Console.WriteLine(listToPrint);
                    }
                }
                else
                {
                    Console.WriteLine("This choice not legal.");
                }
            }
            while (!validInput);

            Console.WriteLine("Press anything to continue: ");
            Console.ReadLine();
        }

        private void showVehicleDetails()
        {
            string licenseNumber = null;
            string details = null;

            Console.Clear();
            try
            {
                licenseNumber = insertLicenseNumber();
                details = m_GarageManger.ShowVehicleDetails(licenseNumber);
                Console.WriteLine(details);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Press anything to continue: ");
            Console.ReadLine();
        }
    }
}