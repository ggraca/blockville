curl -d '{"username":"michel"}' -H "Content-Type: application/json" -X POST http://localhost:3000/signIn
curl -d '{"username":"graca"}' -H "Content-Type: application/json" -X POST http://localhost:3000/signIn
curl -d '{"username":"ines"}' -H "Content-Type: application/json" -X POST http://localhost:3000/signIn
sleep 3

curl -d '{"username":"michel", "x":-1, "y":2}' -H "Content-Type: application/json" -X POST http://localhost:3000/occupyTile
curl -d '{"username":"michel", "x":-1, "y":1}' -H "Content-Type: application/json" -X POST http://localhost:3000/occupyTile
curl -d '{"username":"michel", "x":0, "y":1}' -H "Content-Type: application/json" -X POST http://localhost:3000/occupyTile
curl -d '{"username":"michel", "x":0, "y":-1}' -H "Content-Type: application/json" -X POST http://localhost:3000/occupyTile
curl -d '{"username":"graca", "x":-1, "y":1}' -H "Content-Type: application/json" -X POST http://localhost:3000/occupyTile
curl -d '{"username":"graca", "x":0, "y":2}' -H "Content-Type: application/json" -X POST http://localhost:3000/occupyTile
curl -d '{"username":"graca", "x":1, "y":1}' -H "Content-Type: application/json" -X POST http://localhost:3000/occupyTile
curl -d '{"username":"graca", "x":2, "y":1}' -H "Content-Type: application/json" -X POST http://localhost:3000/occupyTile
curl -d '{"username":"ines", "x":3, "y":-1}' -H "Content-Type: application/json" -X POST http://localhost:3000/occupyTile
curl -d '{"username":"ines", "x":3, "y":0}' -H "Content-Type: application/json" -X POST http://localhost:3000/occupyTile
curl -d '{"username":"ines", "x":3, "y":1}' -H "Content-Type: application/json" -X POST http://localhost:3000/occupyTile
curl -d '{"username":"ines", "x":3, "y":2}' -H "Content-Type: application/json" -X POST http://localhost:3000/occupyTile
curl -d '{"username":"ines", "x":3, "y":3}' -H "Content-Type: application/json" -X POST http://localhost:3000/occupyTile


curl -d '{"username":"michel", "building": 1, "id":1}' -H "Content-Type: application/json" -X POST http://localhost:3000/build
curl -d '{"username":"michel", "building": 2, "id":2}' -H "Content-Type: application/json" -X POST http://localhost:3000/build
curl -d '{"username":"graca", "building": 1, "id":5}' -H "Content-Type: application/json" -X POST http://localhost:3000/build
curl -d '{"username":"graca", "building": 2, "id":6}' -H "Content-Type: application/json" -X POST http://localhost:3000/build
curl -d '{"username":"graca", "building": 2, "id":7}' -H "Content-Type: application/json" -X POST http://localhost:3000/build
curl -d '{"username":"ines", "building": 1, "id":9}' -H "Content-Type: application/json" -X POST http://localhost:3000/build
curl -d '{"username":"ines", "building": 2, "id":10}' -H "Content-Type: application/json" -X POST http://localhost:3000/build

curl -H "Content-Type: application/json" -X GET http://localhost:3000/world
