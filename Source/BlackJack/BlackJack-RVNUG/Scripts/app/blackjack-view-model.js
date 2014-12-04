function BlackJackViewModel() {
    var self = this;

    this.leaderBoardRecords = ko.observableArray();
    this.isRunning = ko.observable(false);
    this.lastRunDate = ko.observable(new Date().toLocaleString());
    this.runButtonText = ko.computed(runButtonText_read.bind(null, this));
    setInterval(this.runGame.bind(this), 60000);
};

function runButtonText_read(self) {
    return self.isRunning() ? 'Running...' : 'Run Game';
}

BlackJackViewModel.prototype.runGame = function () {
    var self = this;
    this.isRunning(true);

    return $.ajax({
        method: 'get',
        url: 'api/game',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            self.isRunning(false);
            self.lastRunDate(new Date().toLocaleString());
            self.leaderBoardRecords(data);
            self.leaderBoardRecords.sort(sortByWins)
        }
    });
};

function sortByWins(left, right) {
    return left.wins == right.wins ? 0 : (left.wins > right.wins ? -1 : 1)
}