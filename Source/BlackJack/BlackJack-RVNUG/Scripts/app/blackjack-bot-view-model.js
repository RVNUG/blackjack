function BlackJackBotViewModel() {
    this.id = ko.observable();

    // Code / code editor...
    this.javaScriptCodeDefault = null;
    this.javaScriptCode = null;
    this.editor = null;

    // Theming...
    this.themes = [
        { name: 'Eclipse', path: 'ace/theme/eclipse' },
        { name: 'Twilight', path: 'ace/theme/twilight' },
        { name: 'GitHub', path: 'ace/theme/github' }
    ];
    this.selectedTheme = ko.observable(this.themes[0]);
    this.selectedTheme.subscribe(selectedTheme_change.bind(null, this));

    // Display results...
    this.allResults = ko.observableArray();
    this.winResults = ko.computed(winResults_read.bind(null, this));
    this.lossResults = ko.computed(lossResults_read.bind(null, this));
    this.pushResults = ko.computed(pushResults_read.bind(null, this));
    this.showResultPanel = ko.observable(false);
};

BlackJackBotViewModel.prototype.afterLoad = function () {
    if (!this.id()) {
        this.getSampleBot().done(initializeEditor.bind(null, this));
    } else {
        initializeEditor(this);
    }
};

function initializeEditor(self) {
    self.editor = ace.edit('editor');
    self.editor.setTheme(self.selectedTheme().path);
    self.editor.getSession().setMode('ace/mode/javascript');
    self.editor.setValue(self.javaScriptCode);
    self.editor.moveCursorTo(0, 0);
    self.editor.focus();
}

function selectedTheme_change(self, newValue) {
    self.editor.setTheme(newValue.path);
}

function winResults_read(self) {
    return ko.utils.arrayFilter(self.allResults(), filterResultsByResult.bind(null, 'Win'));
}

function lossResults_read(self) {
    return ko.utils.arrayFilter(self.allResults(), filterResultsByResult.bind(null, 'Loss'));
}

function pushResults_read(self) {
    return ko.utils.arrayFilter(self.allResults(), filterResultsByResult.bind(null, 'Push'));
}

function filterResultsByResult(result, item) {
    return item.result === result;
}

BlackJackBotViewModel.prototype.getSampleBot = function () {
    var self = this;

    return $.ajax({
        method: 'get',
        url: 'api/player',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            self.id(data.id);
            self.javaScriptCode = data.javaScriptCode;
            self.javaScriptCodeDefault = data.javaScriptCode;
        },
        fail: function (data) {
            alert('Error ocurred getting the sample bot.');
        }
    });
};

BlackJackBotViewModel.prototype.reset = function () {
    this.javaScriptCode = this.javaScriptCodeDefault;
    this.editor.setValue(this.javaScriptCode);
    this.editor.moveCursorTo(0, 0);
};

BlackJackBotViewModel.prototype.putBot = function () {
    var self = this,
        botDto;

    self.javaScriptCode = self.editor.getValue();

    botDto = {
        id: self.id(),
        javaScriptCode: self.javaScriptCode
    };

    return $.ajax({
        method: 'put',
        url: 'api/player/' + self.id(),
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(botDto),
        success: function (data) {
            self.allResults(data);
            self.showResultPanel(true);
        },
        fail: function (data) {
            alert('Bot update failed');
        }
    });
};