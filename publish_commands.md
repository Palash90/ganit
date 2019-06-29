# Commands
This file keeps track of the commands used to publish.

## C
Not required, not following any package manager

## dos
Not required, not following any package manager

## dotnet
* `nuget pack ganit.nuspec`

* Upload generated .nupkg in the nuget.org site

## java
Not yet published, waiting for JIRA Space to be created
```
mvn clean
mvn release:prepare
mvn release: perform
```
or
```
mvn clean deploy
```

## js
```
npm login
npm publish
```

## python
```
python3 setup.py sdist bdist_wheel
python3 -m twine upload dist/*
```

## Rust
```
cargo package --allow-dirty
cargo publish --allow-dirty
```

## sh
Not required, not following any package manager