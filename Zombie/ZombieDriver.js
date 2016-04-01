"use strict";

process.on('uncaughtException', e => console.error('Uncaught Exception: ' + e));

const Zombie = require('zombie');
const zombie = new Zombie();

//  Entry point for Edge.js
return function(data, callback) {
  callback(null, new Bridge(zombie));
}
