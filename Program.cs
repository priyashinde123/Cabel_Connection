using CABLE_OPERATIONS;
using CABLE_OPERATIONS.Entities;
using CABLE_OPERATIONS.Operations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CABLE_CONNECTION
{
    public class Program
    {
        private static string _connectionString = @"Data Source=DESKTOP-7K173KI\SQL2016EXPRESS;Initial Catalog=DTH_SYSTEM;Persist Security Info=True;User ID=sa;Password=aa";

        /// private static City_Operation cityOperations = new City_Operation(_connectionString);
        private static Customer_Operation cust_Operations = new Customer_Operation(_connectionString);
        private static Customer_Dashboard_Operation customer_dash  = new Customer_Dashboard_Operation(_connectionString);
        private static Usp_Yearly_Statement_Operation yealy_statement = new Usp_Yearly_Statement_Operation(_connectionString);
        private static Customer_Query_Operation cust_query = new Customer_Query_Operation(_connectionString);
        private static Agent_Operation agent_ope = new Agent_Operation(_connectionString);


        // Customer_Query_Operation
        public static void Main(string[] args)
        {
        Option:
            {

                Console.Clear();
                Console.WriteLine("==========================");
                Console.WriteLine("LOGIN");
                Console.WriteLine("==========================");

                Console.WriteLine("1. Customer Login \r\n2. Agent Login");
                Console.WriteLine("==========================");
                String val=Console.ReadLine();

                
                if (val=="1")
                    {    Console.Clear();
                        //Customer Login
                        Console.WriteLine("Enter the Username");
                        String username = Console.ReadLine();
                        Console.WriteLine("enter password");
                        String password = Console.ReadLine();
                        bool flag = cust_Operations.IsValidateLogin(username, password);
                Customer_Option:
                    { 
                        if (flag)
                        {

                            Console.Clear();
                            Console.WriteLine("==========================");
                            Customer_Dashboard c = customer_dash.GetCustomers_Dashboard(username);

                            Console.WriteLine($"Welcome  {c.name}.\r\n Your  Package is {c.package_name}");
                            Console.WriteLine("==========================");
                            Console.WriteLine("1. Get My Infomation \r\n 2.Get Yearly Statement \r\n 3.Raise a Complaint \r\n 4.Get Agent Information");
                            Console.WriteLine("==========================");
                            String choice = Console.ReadLine();

                            bool flag_;
                    
                            switch (choice)
                            {
                                case "1":
                                    Console.WriteLine("==========================");
                                    Customer customer = cust_Operations.GetCustomer(username);
                                    Console.WriteLine($" Customer Id\t\t: {customer.id}\r\n First_Name\t\t: {customer.first_name}" +
                                        $"\r\n Last_Name\t\t: {customer.last_name}\r\n Username\t\t: {customer.user_name}" +
                                        $"\r\n Password\t\t: {customer.password}\r\n Contact\t\t: {customer.contact}" +
                                        $"\r\n Address\t\t: {customer.address}\r\n Package ID\t\t: {customer.package_id}" +
                                        $"\r\n Status ID\t\t: {customer.status_id}\r\n Connection Date\t: {customer.connection_date}" +
                                        $"\r\n Group ID\t\t: {customer.gid} \r\n City ID\t\t: {customer.city_id}");

                                    break;
                                case "2":
                                    //2.Get Yearly Statement
                                    Console.WriteLine("Enter year");
                                    int year = Convert.ToInt32(Console.ReadLine());
                                    List<Usp_Yearly_Statement> usp_yearly_statement = yealy_statement.GetYearlyStatement(username, year);
                                    Console.WriteLine("Month\t\tyear\tPayment Date\tPaid Amount");
                                    Console.WriteLine("========================================================");
                                    foreach (Usp_Yearly_Statement usp in usp_yearly_statement)
                                    {
                                        Console.WriteLine($"{usp.month}\t{usp.year}\t{usp.payment_date}\t{usp.paid_amount}");
                                        Console.WriteLine("---------------------------------------------------------");
                                    }
                                    // { usp.payment_date.ToString("yyyy-MM-dd")}
                                    break;
                                case "3":
                                    //3.Raise a Complaint
                                    Console.WriteLine("Enter  the ID");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Write Your Complaint ");
                                    String Query = Console.ReadLine();

                                    flag_ = cust_query.RaiseQuery(id, Query);
                                    if (flag_)
                                        Console.WriteLine("Complaint Added Successfully");

                                    break;
                                case "4":
                                    //Get Agent Information
                                    Agent agent = agent_ope.GetAgent(username);
                                    Console.WriteLine($"Agent ID\t:\t{agent.id}");
                                    Console.WriteLine($"Agent Name \t:\t{agent.name}");
                                    Console.WriteLine($"Agent Authority :\t{agent.authority_id}");
                                    Console.WriteLine($"Agent Group id \t:\t{agent.gid}");
                                    Console.WriteLine($"Agent Username\t:\t{agent.agent_username}");
                                    Console.WriteLine($"Agent Password\t:\t{agent.agent_password}");
                                    break;
                               default:
                                    goto Customer_Option;


                            }
                         }
                        }
                    }
                    else if (val == "2")
                    {
                        //Agent login
                        Console.WriteLine("1. Add a new customer \r\n 2.Get Todays Complaint \r\n 3.Complaint Resolution Repor \r\n 3.Resolve a complaint");
                        int choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                ///1.Add a new customer
                                Console.WriteLine("enter the Customer Name");
                                string name = Console.ReadLine();
                                Console.WriteLine("");
                                break;
                            case 2:
                                //2.Get Todays Complaint
                                break;
                            case 3:

                                break;

                        }
                    }
                    else if (val == "3")
                    {
                    
                    }
                    else
                     {
                    Console.WriteLine("invalid choice");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    goto Option;
                 }

                        

                

            }
        }

            //string input = Console.ReadLine();
            /*
            List<City> cityList = cityOperations.GetCities(input);

            foreach (City c in cityList)
            {
                Console.WriteLine(c.id);
                Console.WriteLine(c.name);
            }

            Console.ReadLine();*/
        }
    
}
