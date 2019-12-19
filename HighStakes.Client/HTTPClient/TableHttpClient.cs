// using System;
// using System.Net;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Threading.Tasks;
// using HighStakes.Client.Models;
// using Newtonsoft.Json;

// namespace HighStakes.Client.HTTPClient
// {
//   public class TableHttpClient
//   {
//     public const string urlBase = "http://api/api/logic/";
//     public HttpClient client = new HttpClient();

//     public async Task<TableData> GetTableAsync()
//     {
//       string urlTable = urlBase + "GetTable";
//       TableData table;
//       string tableString = "";

//       HttpResponseMessage response = await client.GetAsync(urlTable);

//       if (response.IsSuccessStatusCode)
//       {
//         tableString = await response.Content.ReadAsStringAsync();
//       }

//       table = JsonConvert.DeserializeObject<TableData>(tableString);
//       Console.Write("HanError: TableString");
//       Console.WriteLine(tableString);

//       Console.Write("HanError: TableObject");
//       Console.WriteLine(table.TableId);
//       return table;
//     }

//     public async Task<TableData> GetJoinTableAsync(TableData tableInput, PlayerData playerInput)
//     {
//       string tableInputString = JsonConvert.SerializeObject(tableInput);
//       string playerInputString = JsonConvert.SerializeObject(playerInput);

//       string urlTable = urlBase + "JoinTable/" + tableInputString + '/' + playerInputString;
//       TableData table;
//       string tableString = "";

//       HttpResponseMessage response = await client.GetAsync(urlTable);

//       if (response.IsSuccessStatusCode)
//       {
//         tableString = await response.Content.ReadAsStringAsync();
//       }

//       table = JsonConvert.DeserializeObject<TableData>(tableString);
//       return table;
//     }

//     public async Task<PlayerData> RunAsyncForUser(string username, string password)
//     {
//       // Update port # in the following line.
//       client.BaseAddress = new Uri(urlBase);
//       client.DefaultRequestHeaders.Accept.Clear();
//       client.DefaultRequestHeaders.Accept.Add(
//         new MediaTypeWithQualityHeaderValue("application/text"));

//       try
//       {
//         // Create a new product
//         PlayerData loadPlayer;
//         // Get the product
//         loadPlayer = await GetUserAsync(username, password);
//         return loadPlayer;
//       }
//       catch (Exception e)
//       {
//         Console.WriteLine(e.Message);
//         return null;
//       }
//     }

//     public async Task<TableData> RunAsyncForJoinTable(TableData tableInput, PlayerData playerInput)
//     {
//       // Update port # in the following line.
//       client.BaseAddress = new Uri(urlBase);
//       client.DefaultRequestHeaders.Accept.Clear();
//       client.DefaultRequestHeaders.Accept.Add(
//         new MediaTypeWithQualityHeaderValue("application/text"));

//       try
//       {
//         // Create a new product
//         TableData loadTable;
//         // Get the product
//         loadTable = await GetJoinTableAsync(tableInput, playerInput);
//         return loadTable;
//       }
//       catch (Exception e)
//       {
//         Console.WriteLine(e.Message);
//         return null;
//       }
//     }
//     public async Task<TableData> RunAsyncForTable()
//     {
//       // Update port # in the following line.
//       client.BaseAddress = new Uri(urlBase);
//       client.DefaultRequestHeaders.Accept.Clear();
//       client.DefaultRequestHeaders.Accept.Add(
//         new MediaTypeWithQualityHeaderValue("application/text"));

//       try
//       {
//         // Create a new product
//         TableData loadTable;
//         // Get the product
//         loadTable = await GetTableAsync();
//         return loadTable;
//       }
//       catch (Exception e)
//       {
//         Console.WriteLine("HanError: from RunAsyncForTable");
//         Console.WriteLine("HanError :{0} ",e.Message);
//         return null;
//       }
//     }
//   }
// }