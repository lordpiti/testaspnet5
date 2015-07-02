/// <binding Clean='clean' />

var gulp = require("gulp"),
  rimraf = require("rimraf"),
  fs = require("fs"),
  sass = require("gulp-sass");

eval("var project = " + fs.readFileSync("./project.json"));

var paths = {
  bower: "./bower_components/",
  lib: "./" + project.webroot + "/lib/",
  angularApp: "./" + project.webroot + "/js/",
  css: "./" + project.webroot + "/css/"
};

gulp.task("clean", function (cb) {
    rimraf(paths.lib, cb);
});

gulp.task("cleanApp", function (cb) {
    rimraf(paths.angularApp, cb);
});

gulp.task("copy", ["clean","cleanApp","styles"], function () {
  var bower = {
    "bootstrap": "bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,eot}",
    "bootstrap-touch-carousel": "bootstrap-touch-carousel/dist/**/*.{js,css}",
    "hammer.js": "hammer.js/hammer*.{js,map}",
    "jquery": "jquery/jquery*.{js,map}",
    "jquery-validation": "jquery-validation/jquery.validate.js",
    "jquery-validation-unobtrusive": "jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"
  }

  for (var destinationDir in bower) {
    gulp.src(paths.bower + bower[destinationDir])
      .pipe(gulp.dest(paths.lib + destinationDir));
  }
  console.log("copy!!");
  gulp.src("Scripts/*.js")
      .pipe(gulp.dest(paths.angularApp));

});


gulp.task('styles', function () {
    console.log("styles!!");
    gulp.src('sass/**/*.scss')
        .pipe(sass({
            errLogToConsole: true
        }))
        .pipe(gulp.dest(paths.css));
});

gulp.task('default', function () {
    gulp.run('copy');

    gulp.watch('Scripts/**', function (event) {
        gulp.run('copy');
    });
})
