function AppDataModel() {
    var self = this;
    // Routes
    self.userInfoUrl = "/api/Me";
    self.siteUrl = "/";
    self.gameUrl = "/api/Game";

    // Route operations

    // Other private operations

    // Operations

    // Data
    self.returnUrl = self.siteUrl;

    // Data access operations
    self.setAccessToken = function (accessToken) {
        localStorage.setItem("accessToken", accessToken);
    };

    self.getAccessToken = function () {
        return localStorage.getItem("accessToken");
    };
}
