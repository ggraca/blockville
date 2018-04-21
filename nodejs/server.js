var express = require('express');
var ethereum = require('./ethereum.js')
var bodyParser = require('body-parser')

var app = express();
app.use( bodyParser.json() );       // to support JSON-encoded bodies
app.use(bodyParser.urlencoded({     // to support URL-encoded bodies
  extended: true
})); 

app.use(express.json());

app.get('/', function(req, res) {
  res.send("Hello World");
});

app.get('/world', function(req, res) {
  ethereum.getWorld();
  res.send("OK\n");
});

app.post('/occupyTile', function(req, res) {
  data = req.body
  if(data.x && data.y && data.username){
    ethereum.occupyTile(data);
    res.send("OK\n");
  }else{
    res.send("missing parameters\n");
  }
});

app.listen(3000, function() {
  console.log("server open");
});
