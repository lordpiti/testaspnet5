/// <binding Clean='clean' />

var gulp = require("gulp"),
  rimraf = require("rimraf"),
  fs = require("fs"),
  sass = require("gulp-sass"),
  ngConstant = require("gulp-ng-constant");

eval("var project = " + fs.readFileSync("./project.json"));

var paths = {
  bower: "./bower_components/",
  lib: "./" + project.webroot + "/lib/",
  angularApp: "./" + project.webroot + "/js/",
  css: "./" + project.webroot + "/css/"
};

//delete all of the js libraries used, this includes the ones installed with bower
gulp.task("clean", function (cb) {
    rimraf(paths.lib, cb);
});

//delete all of the js libraries used, this includes the ones installed with bower
gulp.task("cleanCss", function (cb) {
    rimraf(paths.css, cb);
});

//Delete all of the js files for the angular app
gulp.task("cleanApp", function (cb) {
    rimraf(paths.angularApp, cb);
});

gulp.task("copy", ["clean", "cleanApp"], function () {
  var bower = {
    "bootstrap": "bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,eot}",
    "bootstrap-touch-carousel": "bootstrap-touch-carousel/dist/**/*.{js,css}",
    "hammer.js": "hammer.js/hammer*.{js,map}",
    "jquery": "jquery/jquery*.{js,map}",
    "jquery-validation": "jquery-validation/jquery.validate.js",
    "jquery-validation-unobtrusive": "jquery-validation-unobtrusive/jquery.validate.unobtrusive.js",
    "angular-bootstrap": "angular-bootstrap/*.{js,map,css,ttf,svg,woff,eot}",
    "moment": "moment/*.{js,map,css,ttf,svg,woff,eot}",
    "angular-growl-v2": "angular-growl-v2/build/*.{js,map,css,ttf,svg,woff,eot}",
    "ngImgCrop": "ngImgCrop/compile/unminified/*.{js,map,css,ttf,svg,woff,eot}"
  }
    //
  for (var destinationDir in bower) {
    gulp.src(paths.bower + bower[destinationDir])
      .pipe(gulp.dest(paths.lib + destinationDir));
  }
  console.log("copy!!");
  gulp.src("Scripts/*.js")
      .pipe(gulp.dest(paths.angularApp));

});

//Proccess sass files
gulp.task('styles',['cleanCss'], function () {
    console.log("proccessing sass files to generate css");
    gulp.src('sass/**/*.scss')
        .pipe(sass({
            errLogToConsole: true
        }))
        .pipe(gulp.dest(paths.css));
});

//Create an angularjs module with constants based on a config file to be used for the REST api url for example
gulp.task('constants', function () {
    var myConfig = require('./config.json');
    var apiUrl = myConfig.Api[myConfig.AppSettings.apiType];

    var envConfig = { apiUrl: apiUrl };
    return ngConstant({
        name: 'environments.config',
        constants: envConfig,
        stream: true
    }).pipe(gulp.dest('Scripts'));
});

gulp.task('default',['constants','copy','styles'], function () {

    gulp.watch('Scripts/**', function (event) {
        gulp.run('copy');
    });

    gulp.watch('sass/**', function (event) {
        gulp.run('styles');
    });
})
