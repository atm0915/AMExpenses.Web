var gulp = require('gulp');
var merge = require('merge-stream');

// Dependency Dirs
var deps = {
    "jquery": {
        "dist/*": ""
    },
    "bootstrap": {
        "dist/**/*": ""
    },
    "jquery-validation": {
        "dist/*": ""
    },
    "jquery-validation-unobtrusive-bootstrap": {
        "dist/*": ""
    }
};

gulp.task("scripts", function () {
    var streams = []

    for (var prop in deps){
        console.log("Prepping scripts for: " + prop);
        for (var itemProp in deps[prop]){
            streams.push(gulp.src("node_modules/" + prop + "/" + itemProp)
                .pipe(gulp.dest("wwwroot/vendor/" + prop + "/" + deps[prop][itemProp])));
        }
    }
    return merge(streams);
});

gulp.task("default", gulp.series("scripts"));