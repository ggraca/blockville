
// Import libraries
var Web3    = require('web3'),
  contract  = require("truffle-contract"),
  path      = require('path')
  WorldJSON = require(path.join(__dirname, '../truffle/build/contracts/World.json'));

// Setup RPC connection   
var provider = new Web3.providers.HttpProvider("http://localhost:9545");

// Read JSON and attach RPC connection (Provider)
var World = contract(WorldJSON);
World.setProvider(provider);

var world;

// Use Truffle as usual
World.deployed().then(function(instance) {
  world = instance;

  console.log("test:", instance.getTestVar.call());

}).then(function(result) {
    console.log(result);

}, function(error) {
    console.log(error);
});

function parseHex(a){
  a = parseInt(a, 16);
  if (a > 2**31 - 1) {
     a = a - 4294967296;
  }
  return a;
}

function decodeIntList(s){
  s = s.substring(2);
  var l = [];
  for(var i=0; i<s.length; i+=64){
    l.push(parseHex(s.substring(i+56, i+64)));
  }
  return l;
}

function getWorld(data) {
  console.log("everything ok here");

  //r = world.occupyTile.call(data.username, data.x, data.y);
  world.getWorld.call().then(
    function(result){
      var tiles = [];
      var l = decodeIntList(result[0]);
      var owners = result[1].slice(0, -1);
      owners = result[1].split("|");

      for(var i=0; i<l.length; i+=5){
        var id =        l[i+0];
        var x =         l[i+1];
        var y =         l[i+2];
        var userId =    l[i+3];
        var building =  l[i+4];
        tiles.push({
          id: id,
          x: x,
          y: y,
          username: owners[i/5],
          building: building
        });
      }
      console.log(tiles);
    });

}

var account = "0x627306090abab3a6e1400e9345bc60c78a8bef57";

function occupyTile(data) {
  console.log("everything ok here");
  r = world.occupyTile(data.username, data.x, data.y, {from: account, gas:3000000});//.then(function(result){console.log(result)}).then(function(error){console.log(error)});
  console.log(r);

  /*World.deployed().then(function(instance) {
    console.log(r);
  }).then(function(result) {
      console.log(result);
  }, function(error) {
      console.log(error);
  });*/

  //r = world.occupyTile.call(data.username, data.x, data.y);
  //r = world.getTestVar.call();
}

module.exports = {
  "occupyTile": occupyTile,
  "getWorld": getWorld
}
