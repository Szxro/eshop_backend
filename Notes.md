# Notes about the project

## Uploading files to the api

1. Can do this using FileStream

```c#
string pathFileName = Path.Combine(Directory.GetCurrentDirectory(),"Upload\\Files");
// This is going to return a path with the current directory and other params you use

await using FileStream fs = new FileStream(pathFileName,FileMode.Create); // new instance of FileStream

await file.CopyAsync(fs); // Copying the file upload to the current path in fs
```

> This will throw an exception with the message that the access is denied, why? because can use the volumen you are currently using , if you want this to work , need to permit this but this can be dangerous, instead use a more freely path in the current project.
