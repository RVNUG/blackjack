var RouterViewModel = function () {
    var self = this;

    this.routes = ko.observableArray([
        new Route({
            viewModel: new BlackJackViewModel(),
            routeName: 'Home' // Defaults the caption and viewTemplate to routeName
        }),
        new Route({
            viewModel: new BlackJackBotViewModel(),
            routeName: 'Bot',
            viewTemplate: 'Bot',
            caption: 'Javascript Bot'
        })/*,
        new Route({
            viewModel: new BlackJackBotCsViewModel(),
            routeName: 'BotCs',
            viewTemplate: 'BotCs',
            caption: 'C# Bot',
        })*/
    ]);

    this.currentRouteName = ko.observable();

    this.currentRoute = ko.computed(function () {
        if (!self.currentRouteName()) {
            return;
        }
        return ko.utils.arrayFirst(self.routes(), function (route) {
            return route.routeName.toLowerCase() === self.currentRouteName().toLowerCase();
        });
    });

    // BEFORE ROUTE CHANGES
    this.currentRoute.subscribe(function (oldValue) {
        if (!oldValue) {
            return;
        }

        if (typeof oldValue.viewModel.beforeUnload === 'function') {
            oldValue.viewModel.beforeUnload();
        }

        if (typeof oldValue.viewModel.unload === 'function') {
            oldValue.viewModel.unload();
        }
    }, null, 'beforeChange');

    // AFTER ROUTE CHANGES
    this.currentRoute.subscribe(function (newValue) {
        if (!newValue) {
            return;
        }
        if (typeof newValue.viewModel.load === 'function') {
            newValue.viewModel.load();
        }
        if (typeof newValue.viewModel.afterLoad === 'function') {

            // Executes after the load has finished.
            setTimeout(function () {
                newValue.viewModel.afterLoad();
            }, 0);
        }
    });
};