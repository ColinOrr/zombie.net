"use strict";

const Zombie = require('zombie');
const zombie = new Zombie();

//  Entry point for Edge.js
return function(data, callback) {
  callback(null, new Bridge(zombie));
}
