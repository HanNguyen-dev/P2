using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HighStakes.Client.Models;
using Newtonsoft.Json;

namespace HighStakes.Client.HTTPClient
{
  public class HighStakesHttpClient
  {
    public const string urlBase = "http://api/api/logic/";
    // public const string urlBase = "http:/localhost:4000/api/logic/";
    public HttpClient client = new HttpClient();

    public async Task<PlayerData> GetUserAsync(string username, string password)
    {
      string urlLogin = urlBase + "Login/" + username + "/" + password;
      PlayerData player;
      string playerString = "";

      HttpResponseMessage response = await client.GetAsync(urlLogin);

      if (response.IsSuccessStatusCode)
      {
        playerString = await response.Content.ReadAsStringAsync();
      }

      player = JsonConvert.DeserializeObject<PlayerData>(playerString);

      return player;
    }

    public async Task<TableData> GetTableAsync()
    {
      string urlTable = urlBase + "GetTable";
      TableData table;
      string tableString = "";

      HttpResponseMessage response = await client.GetAsync(urlTable);

      if (response.IsSuccessStatusCode)
      {
        tableString = await response.Content.ReadAsStringAsync();
      }

      table = JsonConvert.DeserializeObject<TableData>(tableString);

      return table;
    }

    public async Task<TableData> GetStartRoundAsync(TableData table)
    {
      string tableString = JsonConvert.SerializeObject(table);

      string urlTable = urlBase + "StartRound/" + tableString;

      HttpResponseMessage response = await client.GetAsync(urlTable);

      if (response.IsSuccessStatusCode)
      {
        tableString = await response.Content.ReadAsStringAsync();
      }

      table = JsonConvert.DeserializeObject<TableData>(tableString);

      return table;
    }

    public async Task<TableData> GetEndRoundAsync(TableData table)
    {
      string tableString = JsonConvert.SerializeObject(table);

      string urlTable = urlBase + "EndRound/" + tableString;

      HttpResponseMessage response = await client.GetAsync(urlTable);

      if (response.IsSuccessStatusCode)
      {
        tableString = await response.Content.ReadAsStringAsync();
      }

      table = JsonConvert.DeserializeObject<TableData>(tableString);

      return table;
    }

    public async Task<DSeat> GetBidAsync(DSeat seat, int bid)
    {
      string seatString = JsonConvert.SerializeObject(seat);

      string urlBidding = urlBase + "Bid/" + seatString + "/" + bid;

      HttpResponseMessage response = await client.GetAsync(urlBidding);

      if (response.IsSuccessStatusCode)
      {
        seatString = await response.Content.ReadAsStringAsync();
      }

      DSeat returnSeat = JsonConvert.DeserializeObject<DSeat>(seatString);

      return returnSeat;
    }

    public async Task<TableData> GetStartGameAsync(TableData table)
    {
      string tableString = JsonConvert.SerializeObject(table);

      string urlTable = urlBase + "StartGame/" + tableString;

      HttpResponseMessage response = await client.GetAsync(urlTable);

      if (response.IsSuccessStatusCode)
      {
        tableString = await response.Content.ReadAsStringAsync();
      }

      table = JsonConvert.DeserializeObject<TableData>(tableString);

      return table;
    }

    public async Task<TableData> GetJoinTableAsync(TableData tableInput, PlayerData playerInput)
    {
      string tableInputString = JsonConvert.SerializeObject(tableInput);
      string playerInputString = JsonConvert.SerializeObject(playerInput);

      string urlTable = urlBase + "JoinTable/" + tableInputString + '/' + playerInputString;
      TableData table;
      string tableString = "";

      HttpResponseMessage response = await client.GetAsync(urlTable);

      if (response.IsSuccessStatusCode)
      {
        tableString = await response.Content.ReadAsStringAsync();
      }

      table = JsonConvert.DeserializeObject<TableData>(tableString);
      return table;
    }

    public async Task<PlayerData> RunAsyncForUser(string username, string password)
    {
      // Update port # in the following line.
      client.BaseAddress = new Uri(urlBase);
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/text"));

      try
      {
        // Create a new product
        PlayerData loadPlayer;
        // Get the product
        loadPlayer = await GetUserAsync(username, password);
        return loadPlayer;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }

    public async Task<TableData> RunAsyncForStartRound(TableData tableInput)
    {
      // Update port # in the following line.
      client.BaseAddress = new Uri(urlBase);
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/text"));

      try
      {
        TableData loadTable;
        loadTable = await GetStartRoundAsync(tableInput);
        return loadTable;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }

    public async Task<TableData> RunAsyncForEndRound(TableData tableInput)
    {
      // Update port # in the following line.
      client.BaseAddress = new Uri(urlBase);
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/text"));

      try
      {
        TableData loadTable;
        loadTable = await GetEndRoundAsync(tableInput);
        return loadTable;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }
    public async Task<TableData> RunAsyncForStartGame(TableData tableInput)
    {
      // Update port # in the following line.
      client.BaseAddress = new Uri(urlBase);
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/text"));

      try
      {
        TableData loadTable;
        loadTable = await GetStartGameAsync(tableInput);
        return loadTable;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }
    public async Task<TableData> RunAsyncForJoinTable(TableData tableInput, PlayerData playerInput)
    {
      // Update port # in the following line.
      client.BaseAddress = new Uri(urlBase);
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/text"));

      try
      {
        // Create a new product
        TableData loadTable;
        // Get the product
        loadTable = await GetJoinTableAsync(tableInput, playerInput);
        return loadTable;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }
    public async Task<TableData> RunAsyncForTable()
    {
      // Update port # in the following line.
      client.BaseAddress = new Uri(urlBase);
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/text"));

      try
      {
        // Create a new product
        TableData loadTable;
        // Get the product
        loadTable = await GetTableAsync();
        return loadTable;
      }
      catch (Exception e)
      {
        Console.WriteLine("HanError: from RunAsyncForTable");
        Console.WriteLine("HanError :{0} ",e.Message);
        return null;
      }
    }
    public async Task<DSeat> RunAsyncForBid(DSeat seat, int bid)
    {
      // Update port # in the following line.
      client.BaseAddress = new Uri(urlBase);
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/text"));

      try
      {
        seat = await GetBidAsync(seat, bid);
        return seat;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }
  }
}