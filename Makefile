
app: SimpleHttpd.cs
	mcs -r:System.Json -r:System.Runtime.Serialization -r:System.ServiceModel.Web -debug -out:$@ $<

run: app
	mono --debug $<

