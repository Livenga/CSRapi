CSC  = csc

SRC  = $(subst /,\,$(shell gfind src -name \*.cs))
PRJC = CSRapi

default:
	$(CSC) /target:library /out:$(PRJC).dll $(SRC)

debug:
	$(CSC) /target:library /out:$(PRJC).dll /define:_DEBUG_ $(SRC)
