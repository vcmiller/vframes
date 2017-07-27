git add Assets/*
git add ProjectSettings/*
git add .gitignore
git add GitGud.bat

set /p msg="Enter Commit Message: "
git commit -m %msg%

pause