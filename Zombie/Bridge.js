"use strict";

//  Wraps a target object providing access to its fields and functions using the
//  callback pattern
const Bridge = function(target) {

  //  Evaluates a script within the context of the target object
  this.eval = function(script, callback) {
    let result = eval(script);
    send(result, callback);
  }.bind(target);

  //  Gets the value of a field on the target object
  this.get = function(key, callback) {
    send(target[key], callback);
  }

  //  Sets the value of a field on the target object
  this.set = function(kvp, callback) {
    target[kvp.key] = kvp.value;
    callback();
  }

  //  Calls a function on the target object and returns the results
  this.call = function(input, callback) {
    let result = target[input.key].apply(target, input.args);
    send(result, callback);
  }

  //
  //  Helper functions

  //  Sends primitive results back directly, wraps complex objects in another
  //  bridge, also detects and waits for promises before returning their results
  function send(result, callback) {
    if (isPrimitive(result)) callback(null, result);
    else if (result.then) result.then(data => { send(data, callback) }).error(e => callback(e));
    else callback(null, new Bridge(result));
  }

  //  Determines whether a value is a primitive type
  function isPrimitive(value) {
    if (value == null) return true;

    if (value instanceof Array) {
      for (let item of value) {
        if (!isPrimitive(item)) return false;
      }
    }
    else if (typeof value == 'object') return false;
    else if (typeof value == 'function') return false;

    return true;
  }
}
