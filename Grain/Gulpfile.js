/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    less = require("gulp-less"); // добавляем модуль less

//  регистрируем задачу по преобразованию styles.less в файл css
gulp.task("less", function () {
    return gulp.src('Theme/Site.less')
        .pipe(less())
        .pipe(gulp.dest('Content' ))
});
