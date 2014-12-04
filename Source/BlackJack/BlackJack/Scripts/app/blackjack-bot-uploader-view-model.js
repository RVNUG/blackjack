﻿function BlackJackHomeViewModel() {
    var self = this;

    this.gameResults = ko.observableArray();

    BlackJackHomeViewModel.prototype.runGame = function () {
        $.ajax({
            method: 'get',
            url: '/api/game',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                self.gameResults(data);
                console.log(self);
            }
        });
    };
};