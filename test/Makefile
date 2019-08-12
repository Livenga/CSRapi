#CSC  = C:\Windows\Microsoft.NET\Framework64\v3.5\csc.exe
CSC  = csc
PRJC = test

SRC  = $(subst /,\,$(shell gfind src/ -name \*.cs))


default:
	$(CSC) -target:exe -out:$(PRJC).exe -reference:CSRAPI.dll $(SRC)

run:
	.\$(PRJC).exe
