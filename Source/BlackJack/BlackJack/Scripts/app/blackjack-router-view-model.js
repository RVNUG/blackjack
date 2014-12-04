var Route = function (viewModel, title, routeName, viewTemplate, appData) {
    this.viewModel = viewModel;
    this.title = title;
    this.routeName = routeName || title;
    this.viewTemplate = viewTemplate || title;
    this.isCurrent = ko.observable(false);
    this.appData = appData;
};

var AppDataModel = function () {
    // This object holds any data that would need to be shared
    // between routes.
    this.botName = ko.observable('New Bot');
    this.
};

var RouterViewModel = function () {
    var self = this,
        appData = new AppDataModel();

    this.routes = ko.observableArray([
        new Route(homeViewModel, 'Home', 'home', 'home'),
        new Route(gameResultsViewModel, 'Game Results', 'results', 'results'),
        new Route(botUploaderViewModel, 'Upload Bot', 'upload', 'upload')]);

    this.currentRouteName = ko.observable();

    this.currentRoute = ko.computed(function () {
        if (!self.currentRouteName()) {
            return;
        }
        return ko.utils.arrayFirst(self.routes(), function (route) {
            return route.routeName.toLowerCase() === self.currentRouteName().toLowerCase();
        });
    });
};
