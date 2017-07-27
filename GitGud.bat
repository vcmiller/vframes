git add Assets/*
git add ProjectSettings/*
git add .gitignore
git add GitGud.bat

set /p msg="Commit Message: "
git commit -m ^"%msg%^"

set /p remote="Remote (blank for origin): "
set /p branch="Branch (blank for master): "

if ^"%remote%^" == "" (
  set remote="origin"
)

if ^"%branch%^" == "" (
  set branch="master"
)

git push ^"%remote%^" ^"%branch%^"

pause