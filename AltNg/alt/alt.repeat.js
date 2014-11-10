﻿'use strict';
var alt = alt || {};
alt.repeat = angular.module("alt.repeat", ["ng"]);

(function (app) {

    // helpers
    var isFunction = function (obj) {
        return (typeof obj) == "function";
    }
    var isArray = function (obj) {
        if (Array.isArray) return Array.isArray(obj);
        return Object.toString.call(obj) === '[object Array]';
    }
    var isNull = function (obj) {
        return obj === null;
    }
    var isUndefined = function (obj) {
        return obj === undefined;
    }
    // add event cross browser
    var addEvent = function (elem, event, fn) {
        // avoid memory overhead of new anonymous functions for every event handler that's installed
        // by using local functions
        function listenHandler(e) {
            var ret = fn.apply(this, arguments);
            if (ret === false) {
                e.stopPropagation();
                e.preventDefault();
            }
            return (ret);
        }

        function attachHandler() {
            // set the this pointer same as addEventListener when fn is called
            // and make sure the event is passed to the fn also so that works the same too
            var ret = fn.call(elem, window.event);
            if (ret === false) {
                window.event.returnValue = false;
                window.event.cancelBubble = true;
            }
            return (ret);
        }

        if (elem.addEventListener) {
            elem.addEventListener(event, listenHandler, false);
        } else {
            elem.attachEvent("on" + event, attachHandler);
        }
    }

    // GATED SCOPE - https://github.com/scalyr/angular/blob/master/src/js/lib/gatedScope.js
    app.config(['$provide', function ($provide) {
        // We use a decorator to override methods in $rootScope.
        $provide.decorator('$rootScope', ['$delegate', '$exceptionHandler',
            function ($rootScope, $exceptionHandler) {

                // Make a copy of $rootScope's original methods so that we can access
                // them to invoke super methods in the ones we override.
                var scopePrototype = {};
                for (var key in $rootScope) {
                    if (isFunction($rootScope[key]))
                        scopePrototype[key] = $rootScope[key];
                }

                var Scope = $rootScope.constructor;

                // Hold all of our new methods.
                var methodsToAdd = {
                };

                // A constant value that the $digest loop implementation depends on.  We
                // grab it down below.
                var initWatchVal;

                /**
                 * @param {Boolean} isolate Whether or not the new scope should be isolated.
                 * @returns {Scope} A new child scope
                 */
                methodsToAdd.$new = function (isolate) {
                    // Because of how scope.$new works, the returned result
                    // should already have our new methods.
                    var result = scopePrototype.$new.call(this, isolate);

                    // We just have to do the work that normally a child class's
                    // constructor would perform -- initializing our instance vars.
                    result.$$gatingFunction = this.$$gatingFunction;
                    result.$$parentGatingFunction = this.$$gatingFunction;
                    result.$$shouldGateFunction = this.$$shouldGateFunction;
                    result.$$gatedWatchers = [];
                    result.$$cleanUpQueue = this.$$cleanUpQueue;

                    return result;
                };

                /**
                 * Digests all of the gated watchers for the specified gating function.
                 *
                 * @param {Function} targetGatingFunction The gating function associated
                 *   with the watchers that should be digested
                 * @returns {Boolean} True if any of the watchers were dirty
                 */
                methodsToAdd.$digestGated = function gatedScopeDigest(targetGatingFunction) {
                    // Note, most of this code was stolen from angular's Scope.$digest method.
                    var watch, value,
                      watchers,
                      length,
                      next, current = this, target = this, last,
                      dirty = false;

                    do { // "traverse the scopes" loop
                        if (watchers = current.$$gatedWatchers) {
                            // process our watches
                            length = watchers.length;
                            while (length--) {
                                try {
                                    watch = watchers[length];
                                    // Scalyr edit: We do not process a watch function if it is does not
                                    // have the same gating function for which $digestGated was invoked.
                                    if (watch.gatingFunction !== targetGatingFunction)
                                        continue;

                                    // Since we are about to execute the watcher as part of a digestGated
                                    // call, we can remove it from the normal digest queue if it was placed
                                    // there because the watcher was added after the gate function's first
                                    // evaluation.
                                    if (watch && !isNull(watch.cleanUp)) {
                                        watch.cleanUp();
                                        watch.cleanUp = null;
                                    }
                                    // Most common watches are on primitives, in which case we can short
                                    // circuit it with === operator, only when === fails do we use .equals
                                    if (watch && (value = watch.get(current)) !== (last = watch.last) &&
                                        !(watch.eq
                                            ? areEqual(value, last)
                                            : (typeof value == 'number' && typeof last == 'number'
                                              && isNaN(value) && isNaN(last)))) {
                                        dirty = true;
                                        watch.last = watch.eq ? copy(value) : value;
                                        watch.fn(value, ((last === initWatchVal) ? value : last), current);
                                        // Scalyr edit:  Removed the logging code for when the ttl is reached
                                        // here because we don't have access to the ttl in this method.
                                    }
                                } catch (e) {
                                    $exceptionHandler(e);
                                }
                            }
                        }

                        // Insanity Warning: scope depth-first traversal
                        // yes, this code is a bit crazy, but it works and we have tests to prove it!
                        // Scalyr edit: This insanity warning was from angular.  We only modified this
                        // code by checking the $$gatingFunction because it's a good optimization to only go
                        // down a child of a parent that has the same gating function as what we are processing
                        // (since if a parent already has a different gating function, there's no way any
                        // of its children will have the right one).
                        if (!(next = ((current.$$gatingFunction === targetGatingFunction && current.$$childHead)
                              || (current !== target && current.$$nextSibling)))) {
                            while (current !== target && !(next = current.$$nextSibling)) {
                                current = current.$parent;
                            }
                        }
                    } while ((current = next));

                    // Mark that this gating function has digested all children.
                    targetGatingFunction.hasDigested = true;
                    return dirty;
                };

                /**
                 * @inherited $watch
                 * @param directiveName The fourth parameter is a new optional parameter that allows
                 *   directives aware of this abstraction to pass in their own names to identify
                 *   which directive is registering the watch.  This is then passed to the
                 *   shouldGateFunction to help determine if the watcher should be gated by the current
                 *   gatingFunction.
                 */
                methodsToAdd.$watch = function gatedWatch(watchExpression, listener, objectEquality,
                    directiveName) {
                    // Determine if we should gate this watcher.
                    if (!isNull(this.$$gatingFunction) && (isNull(this.$$shouldGateFunction) ||
                        this.$$shouldGateFunction(watchExpression, listener, objectEquality, directiveName))) {
                        // We do a hack here to just switch out the watchers array with our own
                        // gated list and then invoke the original watch function.
                        var tmp = this.$$watchers;
                        this.$$watchers = this.$$gatedWatchers;
                        // Invoke original watch function.
                        var result = scopePrototype.$watch.call(this, watchExpression, listener, objectEquality);
                        this.$$watchers = tmp;
                        this.$$gatedWatchers[0].gatingFunction = this.$$gatingFunction;
                        this.$$gatedWatchers[0].cleanUp = null;

                        // We know that the last field of the watcher object will be set to initWatchVal, so we
                        // grab it here.
                        initWatchVal = this.$$gatedWatchers[0].last;
                        var watch = this.$$gatedWatchers[0];

                        // We should make sure the watch expression gets evaluated fully on at least one
                        // digest cycle even if the gate function is now closed if requested by the gating function's
                        // value for shouldEvalNewWatchers.  We do this by adding in normal watcher that will execute
                        // the watcher we just added and remove itself after the digest cycle completes.
                        if (this.$$gatingFunction.shouldEvalNewWatchers && this.$$gatingFunction.hasDigested) {
                            var self = this;
                            watch.cleanUp = scopePrototype.$watch.call(self, function () {
                                if (!isNull(watch.cleanUp)) {
                                    self.$$cleanUpQueue.unshift(watch.cleanUp);
                                    watch.cleanUp = null;
                                }
                                var value;
                                var last = initWatchVal;

                                if (watch && (value = watch.get(self)) !== (last = watch.last) &&
                                      !(watch.eq
                                          ? areEqual(value, last)
                                          : (typeof value == 'number' && typeof last == 'number'
                                            && isNaN(value) && isNaN(last)))) {
                                    watch.last = watch.eq ? copy(value) : value;
                                    watch.fn(value, ((last === initWatchVal) ? value : last), self);
                                }
                                return watch.last;
                            });
                        }
                        return result;
                    } else {
                        return scopePrototype.$watch.call(this, watchExpression, listener, objectEquality);
                    }
                };

                /**
                 * @inherited $digest
                 */
                methodsToAdd.$digest = function gatedDigest() {
                    // We have to take care if a scope's digest method was invoked that has a
                    // gating function in the parent scope.  In this case, the watcher for that
                    // gating function is registered in the parent (the one added in gatedWatch),
                    // and will not be evaluated here.  So, we have to manually see if the gating
                    // function is true and if so, evaluate any gated watchers for that function on
                    // this scope.  This needs to happen to properly support invoking $digest on a
                    // scope with a parent scope with a gating function.
                    // NOTE:  It is arguable that we are not correctly handling nested gating functions
                    // here since we do not know if the parent gating function was nested in other gating
                    // functions and should be evaluated at all.  However, if a caller is invoking
                    // $digest on a particular scope, we assume the caller is doing that because it
                    // knows the watchers should be evaluated.
                    var dirty = false;
                    if (!isNull(this.$$parentGatingFunction) && this.$$parentGatingFunction()) {
                        var ttl = 5;
                        do {
                            dirty = this.$digestGated(this.$$parentGatingFunction);
                            ttl--;

                            if (dirty && !(ttl--)) {
                                throw Error(TTL + ' $digest() iterations reached for gated watcher. Aborting!\n' +
                                    'Watchers fired in the last 5 iterations.');
                            }
                        } while (dirty);
                    }

                    dirty = scopePrototype.$digest.call(this) || dirty;

                    var cleanUpQueue = this.$$cleanUpQueue;

                    while (cleanUpQueue.length)
                        try {
                            cleanUpQueue.shift()();
                        } catch (e) {
                            $exceptionHandler(e);
                        }

                    return dirty;
                }

                /**
                 * Modifies this scope so that all future watchers registered by $watch will
                 * only be evaluated if gatingFunction returns true.  Optionally, you may specify
                 * a function that will be evaluted on every new call to $watch with the arguments
                 * passed to it, and that watcher will only be gated if the function returns true.
                 *
                 * @param {Function} gatingFunction The gating function which controls whether or not all future
                 *   watchers registered on this scope and its children will be evaluated on a given
                 *   digest cycle.  The function will be invoked (with no arguments) on every digest
                 *   and if it returns a truthy result, will cause all gated watchers to be evaluated.
                 * @param {Function} shouldGateFunction The function that controls whether or not
                 *   a new watcher will be gated using gatingFunction.  It is evaluated with the
                 *   arguments to $watch and should return true if the watcher created by those
                 *   arguments should be gated
                 * @param {Boolean} shouldEvalNewWatchers If true, if a watcher is added
                 *   after the gating function has returned true on a previous digest cycle, the
                 *   the new watcher will be evaluated on the next digest cycle even if the
                 *   gating function is currently return false.
                 */
                methodsToAdd.$addWatcherGate = function (gatingFunction, shouldGateFunction,
                                                        shouldEvalNewWatchers) {
                    var changeCount = 0;
                    var self = this;

                    // Set a watcher that sees if our gating function is true, and if so, digests
                    // all of our associated watchers.  Note, this.$watch could already have a
                    // gating function associated with it, which means this watch won't be executed
                    // unless all gating functions before us have evaluated to true.  We take special
                    // care of this nested case below.

                    // We handle nested gating function in a special way.  If we are a nested gating
                    // function (meaning there is already one or more gating functions on this scope and
                    // our parent scopes), then if those parent gating functions every all evaluate to
                    // true (which we can tell if the watcher we register here is evaluated), then
                    // we always evaluate our watcher until our gating function returns true.
                    var hasNestedGates = !isNull(this.$$gatingFunction);

                    (function () {
                        var promotedWatcher = null;

                        self.$watch(function () {
                            if (gatingFunction()) {
                                if (self.$digestGated(gatingFunction))
                                    ++changeCount;
                            } else if (hasNestedGates && isNull(promotedWatcher)) {
                                promotedWatcher = scopePrototype.$watch.call(self, function () {
                                    if (gatingFunction()) {
                                        promotedWatcher();
                                        promotedWatcher = null;
                                        if (self.$digestGated(gatingFunction))
                                            ++changeCount;
                                    }
                                    return changeCount;
                                });
                            }
                            return changeCount;
                        });
                    })();


                    if (isUndefined(shouldGateFunction))
                        shouldGateFunction = null;
                    if (isUndefined(shouldEvalNewWatchers))
                        shouldEvalNewWatchers = false;
                    this.$$gatingFunction = gatingFunction;
                    this.$$gatingFunction.shouldEvalNewWatchers = shouldEvalNewWatchers;
                    this.$$shouldGateFunction = shouldGateFunction;
                };

                // Extend the original Scope object so that when
                // new instances are created, it has the new methods.
                angular.extend(Scope.prototype, methodsToAdd);

                // Also extend the $rootScope instance since it was created
                // before we got a chance to extend Scope.prototype.
                angular.extend($rootScope, methodsToAdd);

                $rootScope.$$gatingFunction = null;
                $rootScope.$$parentGatingFunction = null;
                $rootScope.$$shouldGateFunction = null;
                $rootScope.$$gatedWatchers = [];
                $rootScope.$$cleanUpQueue = [];

                return $rootScope;
            }]);
    }]);

    // REPEATER - https://github.com/scalyr/angular/blob/master/src/js/directives/slyRepeat.js
    app.directive('altRepeat', ['$animate', '$parse', function ($animate, $parse) {

        /**
         * Sets the scope contained in elementScope to gate all its
         * watchers based on the isActiveForRepeat proprety.
         *
         * @param {Object} elementScope The object containing the
         *   scope and isActiveForRepeat properties.
         */
        function gateWatchersForScope(elementScope) {
            elementScope.scope.$addWatcherGate(function () {
                return elementScope.isActiveForRepeat;
            });
        }

        return {
            restrict: 'AC',
            scope: true,
            transclude: 'element',
            priority: 1000,
            terminal: true,
            compile: function (element, attr, linker) {
                // Most of the work is done in the post-link function.
                return function ($scope, $element, $attr) {
                    // This code is largely based on ngRepeat.

                    // Parse the expression.  It should look like:
                    // x in some-expression
                    var expression = $attr.altRepeat;
                    var match = expression.match(/^\s*(.+)\s+in\s+(.*?)$/);
                    if (!match) {
                        throw Error("Expected slyRepeat in form of '_item_ in _collection_' but got '" +
                            expression + "'.");
                    }

                    var iterVar = match[1];
                    var collectionExpr = match[2];

                    match = iterVar.match(/^(?:([\$\w]+))$/);
                    if (!match) {
                        throw Error("'item' in 'item in collection' should be identifier but got '" +
                            lhs + "'.");
                    }

                    // previousElements will store references to the already existing (DOM) elements
                    // that were last used for the last rendering of this repeat and were visible.
                    // We will re-use these elements when executing the next rendering of the repeat when
                    // the iteration value changes.
                    var previousElements = [];
                    // previousElementsBuffer will store references to the already existing (DOM) elements
                    // that are in the page but were not used for the last rendering of this repeat and were
                    // therefore marked as inactive and not visible.  This happens if the length of the repeat
                    // iteration goes down over time, since we do not remove the elements.  If the repeat length
                    // was first 10, then 5, we will end up with the last 5 elements in the previousElementBuffer.
                    // We keep this in case the length increases again.
                    var previousElementBuffer = [];

                    var deregisterCallback = $scope.$watchCollection(collectionExpr, function (collection) {
                        if (!collection)
                            return;
                        if (!isArray(collection))
                            throw Error("'collection' did not evaluate to an array.  expression was " + collectionExpr);
                        var originalPreviousElementsLength = previousElements.length;
                        // First, reconcile previousElements and collection with respect to the previousElementBuffer.
                        // Basically, try to grow previousElements to collection.length if we can.
                        if ((previousElements.length < collection.length) && (previousElementBuffer.length > 0)) {
                            var limit = previousElements.length + previousElementBuffer.length;
                            if (limit > collection.length)
                                limit = collection.length;
                            previousElements = previousElements.concat(previousElementBuffer.splice(0, limit - previousElements.length));
                        }

                        var currentElements = null;
                        var currentElementBuffer = [];

                        var newElements = [];
                        if (collection.length > previousElements.length) {
                            // Add in enough elements to account for the larger collection.
                            for (var i = previousElements.length; i < collection.length; ++i) {
                                // Need to add in an element for each new item in the collection.
                                var newElement = {
                                    scope: $scope.$new(),
                                    isActiveForRepeat: true,
                                };

                                gateWatchersForScope(newElement);
                                newElement.scope.$index = i;
                                newElement.scope.$first = (i == 0);
                                newElements.push(newElement);
                            }
                            currentElements = previousElements.concat(newElements);
                            currentElementBuffer = previousElementBuffer;
                        } else if (collection.length < previousElements.length) {
                            for (var i = collection.length; i < previousElements.length; ++i)
                                previousElements[i].isActiveForRepeat = false;

                            currentElementBuffer = previousElements.splice(collection.length, previousElements.length - collection.length).concat(
                                previousElementBuffer);
                            currentElements = previousElements;
                        } else {
                            currentElements = previousElements;
                            currentElementBuffer = previousElementBuffer;
                        }

                        // We have to fix up the last and middle values in the scope for each element in
                        // currentElements, since their roles may have changed with the new length.
                        // We always have to fix the last element.
                        if (currentElements.length > 0) {
                            var firstIndexToFix = currentElements.length - 1;
                            var lastIndexToFix = currentElements.length - 1;
                            // We also have to fix any new elements that were added.
                            if (originalPreviousElementsLength < currentElements.length) {
                                firstIndexToFix = originalPreviousElementsLength;
                            }
                            // And we usually have to fix the element before the first element we modified
                            // in case it used to be last.
                            if (firstIndexToFix > 0) {
                                firstIndexToFix = firstIndexToFix - 1;
                            }
                            for (var i = firstIndexToFix; i <= lastIndexToFix; ++i) {
                                currentElements[i].scope.$last = (i == (currentElements.length - 1));
                                currentElements[i].scope.$middle = ((i != 0) && (i != (currentElements.length - 1)));
                                if (!currentElements[i].isActiveForRepeat) {
                                    // If it is not marked as active, make it active.  This is also indicates that
                                    // the element is currently hidden, so we have to unhide it.
                                    currentElements[i].isActiveForRepeat = true;
                                    currentElements[i].element.css('display', '');
                                }
                            }
                        }

                        // Hide all elements that have recently become inactive.
                        for (var i = 0; i < currentElementBuffer.length; ++i) {
                            if (currentElementBuffer[i].isActiveForRepeat)
                                break;
                            currentElementBuffer[i].element.css('display', 'none');
                        }

                        // Assign the new value for the iter variable for each scope.
                        for (var i = 0; i < currentElements.length; ++i) {
                            currentElements[i].scope[iterVar] = collection[i];
                        }

                        // We have to go back now and clone the DOM element for any new elements we
                        // added and link them in.  We clone the last DOM element we had created already
                        // for this Repeat.
                        var prevElement = $element;
                        if (previousElements.length > 0)
                            prevElement = previousElements[previousElements.length - 1].element;
                        for (var i = 0; i < newElements.length; ++i) {
                            linker(newElements[i].scope, function (clone) {
                                $animate.enter(clone, null, prevElement);
                                prevElement = clone;
                                newElements[i].element = clone;
                            });
                        }

                        previousElements = currentElements;
                        previousElementBuffer = currentElementBuffer;
                    });
                    $scope.$on('$destroy', function () {
                        deregisterCallback();
                    });
                };
            }
        };
    }]);

    // BUTTLOAD (ME)
    app.directive("altButtload", [function () {

        return {
            restrict: "AC",
            link: function (scope, jEl, attrs) {

                // set
                var element = jEl[0];

                // check
                var isElement = attrs.altButtloadType == "element";
                
                // busy?
                var isBusy = false;

                // bind to scroll
                addEvent((isElement ? element : window), "scroll", function (e) {

                    if (!isBusy) {
                        isBusy = true;
                        var elToUse = isElement ? element : document.body;
                        var innerH = !isElement ? window.innerHeight : elToUse.offsetHeight;
                        var outerH = elToUse.outerHeight || elToUse.scrollHeight;
                        var scrollT = isElement ? elToUse.scrollTop : (window.scrollY || window.pageYOffset);
                        var isAtBottom = (innerH + scrollT) >= outerH;
                        //for (var ijij in elToUse) {
                        //    console.log("Prop " + ijij + " = ", elToUse[ijij]);
                        //}
                        
                        if (isAtBottom) {
                            scope.$apply(function () {
                                try {
                                    scope
                                        .$eval(attrs.altButtload)
                                        .then(function () {                                            
                                            isBusy = false;
                                        });
                                }
                                catch (err) {
                                    isBusy = false;
                                }


                            })
                        }
                        else {
                            isBusy = false;
                        }
                    }

                });

            }
        }
    }]);

})(alt.repeat);



