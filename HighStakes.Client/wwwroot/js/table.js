// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function cardImage(pocket) {
  var url = 'https://deckofcardsapi.com/static/img/';
  var file = '';

  if (pocket.Value < 10) {
    file = file + pocket.Value;
  } else if (pocket.Value == 10) {
    file = file + 0;
  } else if (pocket.Value == 11) {
    file = file + 'J';
  } else if (pocket.Value == 12) {
    file = file + 'Q';
  } else if (pocket.Value == 13) {
    file = file + 'K';
  } else if (pocket.Value == 14) {
    file = file + 'A';
  }

  file = file + GetSuit(pocket.Suit);
  return url + file + '.png';
}


function GetSuit(suit) {
  if (suit == 'Heart') {
    return 'H';
  } else if (suit == 'Diamond') {
    return 'D';
  } else if (suit == 'Club') {
    return 'C';
  } else {
    return 'S';
  }
}

function showRaise() {
  var container = $('#slidecontainer');
  if (container.css("visibility") == 'visible') {
    container.css('visibility', 'hidden');
  } else {
    container.css('visibility', 'visible');
  }

  var refreshPage = "http://localhost:5000/Home/Update";
  $.getJSON(refreshPage, (data) => {

    data.table.Seats.forEach(element => {
      if (element.Occupied) {
        if (element.Player.UserId == Number(sessionStorage.UserID)) {

          $('#slider').attr("min", (data.HighBid - element.RoundBid));
          $('#slider').attr("max", (element.ChipTotal));
          $('#slider').attr("value", (data.HighBid - element.RoundBid));
        }
      }
    });
  });
}

function raise() {
  var raiseAPI = "http://localhost:5000/Home/Raise/";
  raiseAPI = raiseAPI + sessionStorage.UserID + '/' + $('#slider').val();
  $.getJSON(raiseAPI, (data) => {});
  $('#slidecontainer').css('visibility', 'hidden');
}


function check() {
  var url_bid = "http://localhost:5000/Home/Bid/";
  $('.viewbtn').prop("disabled", false);

  url_bid = url_bid + sessionStorage.UserID + '/' + 0;

  $.getJSON(url_bid, (data) => { });

}

function call() {
  var url_bid = "http://localhost:5000/Home/Bid/";
  var url_data = "http://localhost:5000/Home/Update";

  $('.viewbtn').prop("disabled", false);

  $.getJSON(url_data, (data) => {

    data.table.Seats.forEach( (element) => {
      if (element.Player.UserId == Number(sessionStorage.UserID)) {
        var callAmount = data.HighBid - element.RoundBid;

        url_bid = url_bid + sessionStorage.UserID + "/" + callAmount;
        $.getJSON(url_bid, (data_1) => { });
      }
    });
  });
}


function anotherFunction() {
  setInterval( () => {
    var refreshPage = "http://localhost:5000/Home/Update";
    // $('.allusers').css('visibility', 'visible');
    $.getJSON(refreshPage, (data) => {


      // console.log(data.table.Seats.Pocket);
      var dataNum = data.subround;

      $('#pot').html("Pot: " + data.PotValue);

      $('.flops').attr("src", "https://opengameart.org/sites/default/files/card%20back%20blue.png");

      if (dataNum > 0)
      {
        $('#flop1').attr("src", cardImage(data.table.Flop[0]));
        $('#flop2').attr("src", cardImage(data.table.Flop[1]));
        $('#flop3').attr("src", cardImage(data.table.Flop[2]));
        if (dataNum >= 2) {
          $('#flop4').attr("src", cardImage(data.table.Flop[3]));

          if (dataNum >= 3) {
            $('#flop5').attr("src", cardImage(data.table.Flop[4]));
          }
        }
      }
      var numberSeat = 0;
      var nextTurn = 0;
      data.seatsOrder.forEach(element => {
        if (element.Occupied) {
          if (element.Player.UserId == Number(sessionStorage.UserID)) {


            // console.log(element.Player.FirstName);
            $('#user').html(element.Player.FirstName);
            $('#user').append("<br>Chips: ");
            $('#user').append(element.ChipTotal);
            // console.log(data.nextTurn);
            // console.log(nextTurn);
            if (nextTurn == data.nextTurn) {
              $('.viewbtn').prop("disabled", false);
              if (data.HighBid > element.RoundBid) {
                $('#check').prop("disabled", true);
              }
            } else {
              $('.viewbtn').prop("disabled", true);
            }
            $('#card1').css("visibility", "visible");
            $('#card2').css("visibility", "visible");
            $('#card1').attr("src", cardImage(element.Pocket[0]));
            $('#card2').attr("src", cardImage(element.Pocket[1]));
            if (!element.Active) {
              $('#card1').css("visibility", "hidden");
              $('#card2').css("visibility", "hidden");
              $('.viewbtn').prop("disabled", true);
            }

          } else {
            var currentUser;
            var userBox;
            if (numberSeat == 0) {
              currentUser = $('#user2');
              userBox = $('#player2');

            } else {
              currentUser = $('#user8');
              userBox = $('#player8');

            }
            userBox.css('visibility', 'visible');
            currentUser.html(element.Player.FirstName);
            currentUser.append('<br>Chips: ');
            currentUser.append(element.ChipTotal);
            numberSeat += 1;

          }
          nextTurn += 1;
          // console.log(nextTurn);
        }
      });
    });
  }, 1500);
}

function LoadImageCard() {
  var refreshPage = "http://localhost:5000/Home/Update";
    $.getJSON(refreshPage, (data) => {
      data.table.Seats.forEach(element => {
      if (element.Occupied) {
        if (element.Player.UserId == Number(sessionStorage.UserID)) {

          $('#card1').attr("src", cardImage(element.Pocket[0]));
          $('#card2').attr("src", cardImage(element.Pocket[1]));

        }
      }
    });
  });
}




anotherFunction();

$('#check').on("click", check);
$('#call').on("click", call);
$('#showraise').on("click", showRaise);
$('#fold').on("click", fold);
$('#quit').on("quit", quit);
$('#raise').on("click", raise);
// setTimeout(LoadImageCard, 3000);

// $('#slider').on("change", () => {
//   alert($(this).val());
//   $('#demo').html(this.value);
// });

var slider = document.getElementById("slider");
var output = document.getElementById("demo");
output.innerHTML = slider.value;

slider.oninput = function() {
  output.innerHTML = this.value;
}