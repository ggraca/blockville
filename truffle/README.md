# install truffle (should be enough):
npm install -g truffle

# build code:
cd truffle
truffle compile

# run in separate terminal window:
truffle develop 	# should be kept open
> migrate			# after compiling, run 'migrate --reset' if the command failed

# some checks:
World.deployed().then(function(instance){return instance.getTestVar.call();}).then(function(value){return value.toNumber()}); 	// returns 0
World.deployed().then(function(instance){return instance.setTestVar(4);}); 														// returns garbage
World.deployed().then(function(instance){return instance.getTestVar.call();}).then(function(value){return value.toNumber()}); 	// now returns 4

