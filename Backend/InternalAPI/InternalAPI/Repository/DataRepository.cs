using InternalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ObjectsComparer.Utils;
using ObjectsComparer.Exceptions;
using ObjectsComparer;
using System.ComponentModel;
using Microsoft.AspNetCore.SignalR;

namespace InternalAPI.Repository
{
    public class DataRepository: IDataRepository
    {
        private readonly IHubContext<SignalServer> _context;
        List<Employee>? dataList1 = new List<Employee>();
        List<Employee>? dataList2 = new List<Employee>();
        HttpClient client { get; set; }
        public DataRepository(IHubContext<SignalServer> context)
        {

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);
            var compareObject = new ObjectsComparer.Comparer<Employee>();
            compareData(compareObject);
            _context = context;

        }

        public async void compareData(ObjectsComparer.Comparer<Employee> compareObj)
        {
         
            dataList1 = GetData().Result;
            Console.WriteLine(dataList1.Count);

            while (true)
            {
                dataList2 = GetData().Result;
                if (dataList1.Count != dataList2.Count)
                {
                    dataList1 = dataList2;
                    Data_Changed();
                }
                else
                {
                    for (int i = 0; i < dataList1.Count; i++)
                    {
                        Console.WriteLine(dataList1[i]);
                        Console.WriteLine(dataList2[i]);
                        Console.WriteLine("Data");

                        if (!(compareObj.Compare(dataList1[i], dataList2[i])))
                        {
                            dataList1 = dataList2;
                            Data_Changed();
                            Console.WriteLine("Data has changed");
                            break;
                        }

                    }
                }
                await Task.Delay(1000);
            }
        }

        private void Data_Changed()
        {
            _context.Clients.All.SendAsync("NewData");
        }
        public async Task<List<Employee>> GetData()
        {
            List<Employee>? employeesList = new List<Employee>();
            var response = await client.GetAsync("https://localhost:7002/api/Employee");
            var apiResponse = await response.Content.ReadAsStringAsync();
            employeesList = JsonConvert.DeserializeObject<List<Employee>>(apiResponse);
            return employeesList;
        }


        public List<Employee> GetAllEmployees()
        {
            return dataList1;
        }
    }
}
