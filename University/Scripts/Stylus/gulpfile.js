var gulp = require('gulp');
var stylus = require('gulp-stylus');

gulp.task('styles', async function(){
    gulp.src('styles/*.styl')
    .pipe(stylus())
    .pipe (gulp.dest('styles/css/'));
});
gulp.task('watch:styles', async function(){
    gulp.watch('styles/*.styl', gulp.series('styles'));
})