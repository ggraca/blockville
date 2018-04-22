pragma solidity ^0.4.17;

contract World {
  uint testVar;
  uint[] BUILDING_COST = [5, 30, 70, 200, 500];
  uint[] BUILDING_PROD = [5, 60, 140, 300, 1000];
  uint TILE_COST = 100;
  uint INITIAL_MONEY;
  uint COOLDOWN = 20 seconds;

  function setTestVar(uint x) public {
    testVar = x;
  }

  function getTestVar() constant public returns (uint, string) {
    // "unit tests"
    require(keccak256(strcat("AAASSS", "|{}{")) == keccak256("AAASSS|{}{"));

    return (testVar, strcat("lkjr", "asda"));
  }

  struct Point {
    int x;
    int y;
    int val;
  }

  struct Tile {
    string owner;
    uint building; // 0 if no building, id of the building type otherwise
    uint readyTime; // time until production finishes and reward can be collected
    int x;
    int y;
  }

  mapping (int => uint) public pointToId; // maps coordinates to tile id
  mapping (bytes32 => uint) public nameToId; // maps username to user id
  mapping (bytes32 => uint) public wallet; // maps username to money
  mapping (uint => string)  public idToName; // maps user id to username

  Tile[] public tiles;
  uint namesCounter = 1;

  constructor() public{
    tiles.push(Tile("", 0, 0, -2000, -2000));
  }

  function _encodePoint(int _x, int _y) internal pure returns (int) {
    require(_x < 2**32 && _x > -(2**32));
    require(_y < 2**32 && _y > -(2**32));
    return _x + (_y * (2**32));
  }

  function decodePoint(int _v) internal pure returns (int, int) {
    int x = int256(int32(_v));
    int y = (_v - x) / (2**32);
    return (x, y);
  }

  function toBytes(int256 x) internal pure returns (bytes memory b) {
    b = new bytes(32);
    assembly { mstore(add(b, 32), x) }
  }

  function strToBytes(string key) internal pure returns (bytes32 ret) {
    require(bytes(key).length <= 32);
    assembly {
      ret := mload(add(key, 32))
    }
  }

  function strcat(string _a, string _b) internal pure returns(string){
    bytes memory a = bytes(_a);
    bytes memory b = bytes(_b);
    bytes memory c = new bytes(a.length + b.length);
    for(uint i=0; i<a.length; i++){
      c[i] = a[i];
    }
    uint o=i;
    for(i=0; i<b.length; i++){
      c[o+i] = b[i];
    }
    return string(c);

  }

  function getWorld() public view returns (bytes, string) {
    bytes memory tilesInfo = new bytes((tiles.length-1) * 5 * 32);
    bytes memory b;
    uint offset = 0;
    uint j = 0;
    string memory names;
    for(uint i=1; i<tiles.length; i++){
      b = toBytes(int(i));
      for(j = 0; j<32; j++){
        tilesInfo[offset+j] = b[j];
      }
      offset += 32;

      b = toBytes(tiles[i].x);
      for(j = 0; j<32; j++){
        tilesInfo[offset+j] = b[j];
      }
      offset += 32;

      b = toBytes(tiles[i].y);
      for(j = 0; j<32; j++){
        tilesInfo[offset+j] = b[j];
      }
      offset += 32;

      bytes32 bName = strToBytes(tiles[i].owner);
      b = toBytes(int(nameToId[bName]));
      for(j = 0; j<32; j++){
        tilesInfo[offset+j] = b[j];
      }
      offset += 32;

      names = strcat(names, tiles[i].owner);
      names = strcat(names, "|");

      b = toBytes(int(tiles[i].building));
      for(j = 0; j<32; j++){
        tilesInfo[offset+j] = b[j];
      }
      offset += 32;
    }

    return (tilesInfo, names);
  }

  event TileOccupied(string owner, int x, int y);

  function occupyTile(string username, int _x, int _y) public returns(uint) {
    require(keccak256(username) != keccak256(""));
    int enc = _encodePoint(_x, _y); // encode x,y into one value
    uint id = pointToId[enc]; // get id of the tile, if it exists
    if(id == 0){ // tile does not exist
      id = tiles.length; // generate tile id
      pointToId[enc] = id; // map x,y to new id
      tiles.push(Tile(username, 0, 0, _x, _y));
    }else{
      require(keccak256(tiles[id].owner) == keccak256("") );
      tiles[id].owner = username;
      tiles[id].x = _x;
      tiles[id].y = _y;
    }
    bytes32 bName = strToBytes(username);
    if(nameToId[bName] == 0){
      nameToId[bName] = namesCounter;
      idToName[namesCounter] = username;
      namesCounter++;
    }
    
    //emit TileOccupied(tiles[id].owner, tiles[id].x, tiles[id].y);
    return (id);
    
  }

  function build(string username, uint id, uint building) public {
    require(id != 0);
    require(keccak256(username) != keccak256(""));
    require(keccak256(username) == keccak256(tiles[id].owner));

    tiles[id].building = building;
    tiles[id].readyTime = now + COOLDOWN;
  }
}
