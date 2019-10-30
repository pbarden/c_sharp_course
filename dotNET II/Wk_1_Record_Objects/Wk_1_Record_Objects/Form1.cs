using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wk_1_Record_Objects
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            // This simply initializes the form used in this application
            InitializeComponent();

        }

        // This will run when the form is loaded
        private void Form1_Load(object sender, EventArgs e)
        {
            // Holds the file path for the base directory (the same one containing the .exe file)
            string DataPath = AppDomain.CurrentDomain.BaseDirectory + "\\data.txt";

            // These will be used to store and assign values to CSV data
            string NextLine;
            string[] SplitLine = new string[7];
            int HeadCount = 0;
            string[] Headers = new string[7];

            // This creates a new list to store Developer objects
            List<Developer> Developers = new List<Developer>();

            // This will read the .txt file and create developer objects with the data in comma delimited format
            System.IO.StreamReader CommaDelimitedInput = new System.IO.StreamReader(@DataPath);
            while ((NextLine = CommaDelimitedInput.ReadLine()) != null)
            {
                SplitLine = NextLine.Split(',');

                // Stores the first line as header information for the table
                if (HeadCount != 1)
                {
                    for (int n = 0; n < SplitLine.Length; n++)
                    {
                        Headers[n] = SplitLine[n].Trim();
                    }
                    HeadCount++;
                }
                // Otherwise, create a new Developer object using the data
                else
                {
                    Developers.Add(new Developer(SplitLine[0].Trim(), SplitLine[1].Trim(), Convert.ToInt32(SplitLine[2].Trim()), Convert.ToDouble(SplitLine[3].Trim()), SplitLine[4].Trim(), SplitLine[5].Trim(), SplitLine[6].Trim()));
                }
            }

            // Closes the data.txt file since the information needed is stored appropriately
            CommaDelimitedInput.Close();

            // Create information for a new DataTable and add header information from the file specifying the data type
            DataTable EmployeeDataTable = new DataTable();
            EmployeeDataTable.Columns.Add(Headers[0], typeof(string));
            EmployeeDataTable.Columns.Add(Headers[1], typeof(string));
            EmployeeDataTable.Columns.Add(Headers[2], typeof(int));
            EmployeeDataTable.Columns.Add(Headers[3], typeof(double));
            EmployeeDataTable.Columns.Add(Headers[4], typeof(string));
            EmployeeDataTable.Columns.Add(Headers[5], typeof(string));
            EmployeeDataTable.Columns.Add(Headers[6], typeof(string));

            // Add entries for each developer object that was read and stored from the data.txt file
            foreach (Developer _dev in Developers)
            {
                EmployeeDataTable.Rows.Add(_dev.Name, _dev.Address, _dev.Age, _dev.GrossPay, _dev.Department, _dev.DeveloperType, _dev.TaxType);
            }

            // Finally, populate the data grid in the form using the information above
            dataGridView1.DataSource = EmployeeDataTable;
        }
    }

    // Creates an interface with data for the Employee from requirements: name, address, age, grodd monthly pay, department id, tax type, annual taxes determined by tax type, annual net pay determined by salary and tax type
    public interface Employee
    {
        string Name { get; set; }
        string Address { get; set; }
        int Age { get; set; }
        double GrossPay { get; set; }
        string Department { get; set; }
        string TaxType { get; set; }

        double GetAnnualTaxes();
        double GetAnnualNetPay();
    }

    // The Developer inherits the Employee interface and adds the attribute for Developer Type
    public class Developer : Employee
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public double GrossPay { get; set; }
        public string Department { get; set; }
        public string TaxType { get; set; }
        public string DeveloperType { get; set; }

        // Defines constructors used to create the Developer object (each of these is included in the CSV)
        public Developer(string _name, string _address, int _age, double _gross, string _department, string _devtype, string _taxtype)
        {
            Name = _name;
            Address = _address;
            Age = _age;
            GrossPay = _gross;
            Department = _department;
            DeveloperType = _devtype;
            TaxType = _taxtype;
        }

        // Defines method to get developer's annual taxes using a sample 7% tax rate for W2 employees
        public double GetAnnualTaxes()
        {
            double TaxesPaid;

            if (TaxType == "W2")
            {
                TaxesPaid = GrossPay * .07;
            }
            else
            {
                TaxesPaid = 0;
            }
            return TaxesPaid;
        }

        // Defines method to get developer's annual net pay, calculated by reducing the gross salary by the taxes paid using the method above
        public double GetAnnualNetPay()
        {
            return GrossPay - GetAnnualTaxes();
        }
    }
}
