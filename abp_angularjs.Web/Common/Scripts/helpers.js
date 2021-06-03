var App = App || {};
(function () {

    var appLocalizationSource = abp.localization.getSource('abp_angularjs');
    App.localize = function () {
        return appLocalizationSource.apply(this, arguments);
    };

})(App);