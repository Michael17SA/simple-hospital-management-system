namespace SimpleHospitalManagementSystem;

internal sealed class Patient
{
    public Patient(string name, int age, string condition)
    {
        Name = name;
        Age = age;
        Condition = condition;
    }

    public string Name { get; }
    public int Age { get; }
    public string Condition { get; }
}

internal sealed class Hospital
{
    private const int WardCount = 3;
    private const int WardCapacity = 4;

    private readonly Patient?[,] wards = new Patient?[WardCount, WardCapacity];
    private readonly int[] patientCount = new int[WardCount];

    public void AddPatient(int wardNumber, Patient patient)
    {
        if (wardNumber < 0 || wardNumber >= WardCount)
        {
            Console.WriteLine($"Invalid ward number. Choose between 0 and {WardCount - 1}.");
            return;
        }

        if (patientCount[wardNumber] >= WardCapacity)
        {
            Console.WriteLine($"Ward {wardNumber} is already full. Cannot add more patients.");
            return;
        }

        int position = patientCount[wardNumber];
        wards[wardNumber, position] = patient;
        patientCount[wardNumber]++;

        Console.WriteLine($"Patient added to Ward {wardNumber} (Position {position + 1}).");
    }

    public void ShowPatients()
    {
        for (int ward = 0; ward < WardCount; ward++)
        {
            Console.WriteLine($"\nWard {ward} - Total Patients: {patientCount[ward]}");

            if (patientCount[ward] == 0)
            {
                Console.WriteLine("  No patients in this ward.");
                continue;
            }

            for (int position = 0; position < patientCount[ward]; position++)
            {
                Patient? patient = wards[ward, position];

                if (patient is not null)
                {
                    Console.WriteLine($"  {position + 1}. {patient.Name}, Age: {patient.Age}, Condition: {patient.Condition}");
                }
            }
        }
    }
}

internal static class Program
{
    private static void Main()
    {
        Hospital hospital = new();
        int choice;

        do
        {
            Console.WriteLine("\n=== Simple Hospital Management System ===");
            Console.WriteLine("1. Add Patient");
            Console.WriteLine("2. Show Patients");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddPatient(hospital);
                    break;
                case 2:
                    hospital.ShowPatients();
                    break;
                case 3:
                    Console.WriteLine("Thank you for using the system. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please pick a valid option.");
                    break;
            }
        } while (choice != 3);
    }

    private static void AddPatient(Hospital hospital)
    {
        string name = ReadRequiredText("Patient Name: ");
        int age = ReadPositiveInteger("Patient Age: ");
        string condition = ReadRequiredText("Condition: ");
        int ward = ReadInteger("Ward Number (0-2): ");

        Patient newPatient = new(name, age, condition);
        hospital.AddPatient(ward, newPatient);
    }

    private static string ReadRequiredText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? value = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }

            Console.WriteLine("This field cannot be empty. Please try again.");
        }
    }

    private static int ReadPositiveInteger(string prompt)
    {
        while (true)
        {
            int value = ReadInteger(prompt);

            if (value > 0)
            {
                return value;
            }

            Console.WriteLine("Please enter a positive number.");
        }
    }

    private static int ReadInteger(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }

            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }
}
