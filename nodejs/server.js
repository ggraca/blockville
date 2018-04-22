var express = require('express');
var ethereum = require('./ethereum.js')
var bodyParser = require('body-parser')

var app = express();
app.use( bodyParser.json() );       // to support JSON-encoded bodies
app.use(bodyParser.urlencoded({     // to support URL-encoded bodies
  extended: true
})); 

app.use(express.json());

pollingData = {};
updated = false;

setInterval(function(){
  if(updated == false){
    ethereum.getWorld( function(r, m){
      pollingData = {tiles: r};
      updated = true;
      //res.send();
    });
  }
}, 3000);

app.get('/', function(req, res) {
  res.send("Hello World");
});

app.post('/world', function(req, res) {
  data = req.body;
  ethereum.getMoney(data, function(money){
    pollingData.myMoney = money;
    console.log(pollingData.myMoney);
    updated = false;
    res.send(pollingData);
  });
});

app.post('/occupyTile', function(req, res) {
  data = req.body;
  console.log(data.x, data.y, data.username);
  if('x' in data && 'y' in data && 'username' in data){
    ethereum.occupyTile(data, function(r){
      updated = false;
      res.send(r);
    });
    res.send("OK\n");
  }else{
    res.send("missing parameters\n");
  }
});

app.post('/build', function(req, res) {
  data = req.body;
  if('building' in data && 'id' in data && 'username' in data){
    ethereum.build(data, function(r){
      updated = false;
      res.send(r);
    });
    res.send("OK\n");
  }else{
    res.send("missing parameters\n");
  }
});

app.post('/signIn', function(req, res) {
  console.log("signing in");
  data = req.body;
  if('username' in data){
    console.log("ok");
    ethereum.signIn(data, function(r){
      res.send(r);
    });
  }else{
    console.log("missing parameters\n");
  }
});

app.listen(3000, function() {
  console.log("server open");
});
