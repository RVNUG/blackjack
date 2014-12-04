var Route = function (options) {
    this.viewModel = options.viewModel;
    this.routeName = options.routeName;
    this.caption = options.caption || options.routeName;
    this.viewTemplate = options.viewTemplate || options.routeName;
    this.isCurrent = ko.observable(false);
};