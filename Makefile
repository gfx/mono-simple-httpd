
app: SimpleHttpd.cs
	mcs -debug -out:$@ $<

run: app
	mono --debug $<

