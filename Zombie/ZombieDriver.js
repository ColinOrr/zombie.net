"use strict";

const Zombie = require('zombie');
const zombie = new Zombie();

//  Entry point for Edge.js
return function(data, callback) {
    let bridge = new Bridge(zombie);
    process.on('uncaughtException', e => console.error('Uncaught Exception: ' + e));
    callback(null, bridge);
}