
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

// Use Truffle as usual
World.deployed().then(function(instance) {
  return instance.getTestVar.call()
}).then(function(result) {
    console.log(result);

}, function(error) {
    console.log(error);
}); 

