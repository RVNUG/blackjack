Sammy(function () {
    var routerVm = new RouterViewModel();
    ko.applyBindings(routerVm);

    this.get('#:view', function () {
        // Determine constructor.
        // Invoke constructor. (V4 - system.lang.object.construct)
        // Another approach - in memory, keep a Key-value pair - { RouteName: ConstructorFunction }
        // Set routerVm.currentRoute().
        routerVm.currentRouteName(this.params.view);
    });
}).run('#Home');