pragma solidity ^0.4.17;

contract World {
  uint testVar;

  function setTestVar(uint x) public {
    testVar = x;
  }

  function getTestVar() constant public returns (uint) {
    return testVar;
  }

  struct Point {
    int x;
    int y;
    int val;    
  }

  struct Land {
    address owner;
    uint building; // 0 if no building, id of the building type otherwise
    uint readyTime; // time until production finishes and reward can be collected
  }

  mapping (uint => address) public zombieToOwner;

  function _encodePoint(int _x, int _y) internal pure returns (int) {
    require(_x < 2**32 && _x > -2**32);
    require(_y < 2**32 && _y > -2**32);
    return _x + _y * 2**32;
  }

  function decodePoint(int _v) internal pure returns (int, int) {
    int x = int256(int32(_v));
    int y = (_v - x) / (2**32);
    return (x, y);
  }

}
