CSC  = csc

SRC  = $(subst /,\,$(shell gfind src -name \*.cs))
PRJC = CSRAPI

default:
	$(CSC) /target:library /out:$(PRJC).dll $(SRC)
