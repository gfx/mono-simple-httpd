
run: app
	mono --debug $<

app: SimpleHttpd.cs
	mcs -debug -out:$@ $<
