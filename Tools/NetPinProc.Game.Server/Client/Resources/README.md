## About

https://github.com/aksoftware98/multilanguages

- add keys to the resource `en-US.yml`
- a single file can be translated to many .yml at [https://akmultilanguages.azurewebsites.net/](https://akmultilanguages.azurewebsites.net)
- changing languages
- `SetLanguage` must be invoked once, see `Shared/MainLayout`

## use

Inject: `@inject ILanguageContainerService languageContainer`

Get key: `@languageContainer.Keys["HomePage:HelloWorld"]`

```
protected  override  void  OnInitialized()
{
	//This will make the current component gets updated whenever you change the language of the application
	languageContainer.InitLocalizedComponent(this);
}
```

## interpolation
	`welcome: Welcome {username} to our system {version}`

	then use c# anon type

	```
	_language["Welcome", new { Username = "aksoftware98", Version = "v4.0"}]
	```