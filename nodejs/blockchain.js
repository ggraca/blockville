
// Import libraries
var Web3            = require('web3'),
  contract        = require("truffle-contract"),
  path            = require('path')
  WorldJSON     = require(path.join(__dirname, '../truffle/build/contracts/World.json'));

// Setup RPC connection   
var provider = new Web3.providers.HttpProvider("http://localhost:9545");

// Read JSON and attach RPC connection (Provider)
var World = contract(WorldJSON);
World.setProvider(provider);

var world;

// Use Truffle as usual
World.deployed().then(function(instance) {
  //console.log(World);
  /*var event = instance.print();
  console.log(event)
  //console.log("listening for events on ", contractAddress)
  // watch for changes
  event.watch(function(error, result){ //This is where events can trigger changes in UI
    
  });*/

  world = instance;

  return instance.getTestVar.call()
}).then(function(result) {
    console.log(result, result.logs);

}, function(error) {
    console.log(error);
}); 


function occupyLand() {
  console.log("everything ok here");
}
