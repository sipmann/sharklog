# SharkLog

[![Build Status](https://sipmann.visualstudio.com/github%20OSS/_apis/build/status/sipmann.sharklog?branchName=master)](https://sipmann.visualstudio.com/github%20OSS/_build/latest?definitionId=2&branchName=master)

This is supposed to be a simple Real Time Log based on [Sand Castle Logger](https://github.com/benhurott/sand-castle-logger). With a few changes on how it works. You can use it right now at [https://sharklog.sipmann.com](https://sharklog.sipmann.com), it's free BUT we'll clear the logs every now and then.

## Running

Create the database and the tables from `create.sql` and configure the connection at the `appsettings.json` or `appsettings.Development.json`.

```shell
dotnet restore
dotnet ef database update
dotnet run
```

And then, go ahead and look at your `http://localhot:5000` and it's up and running. After that, any post request that you send to `http://localhost:5000/log/$app_name` will be stored. You can send what you using the following structure.

```json
{
    "Title": "Log Title",
    "LogType": "Bug|Info|Cat|Whatever",
    "Body": "Any string that you want",
    "Token": "UNINQUE-TOKEN-HERE"
}
```

Sample post 

```shell
curl -X POST http://localhost:5000/log/abc -H 'content-type: application/json' -d '{"Title": "Opsy","LogType": "bug","Body": "Exception on file abc.cs on line 17", "Token": "UNIQUE"}'
```



## Requirements

* DotNet Core 2.2
* MySQL >= 5.7


## Todos

- [ ] Custom post processors
- [ ] Cassandra support
- [ ] User account to manage applications (if desired)
