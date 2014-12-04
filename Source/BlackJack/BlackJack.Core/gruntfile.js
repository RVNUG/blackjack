/*global module: true, require: true */

module.exports = function (grunt) {
    var appFiles = ['./Bots/**/*.js'];

    // Project configuration.
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        jsdoc: {
            dist: {
                src: appFiles,
                options: {
                    destination: './../BlackJack-RVNUG/docs'
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-jsdoc');

    grunt.registerTask('default', ['jsdoc']);
};