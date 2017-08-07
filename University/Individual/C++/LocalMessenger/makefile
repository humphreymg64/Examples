part2: log.o hw1.o
	g++ log.o hw1.o -o hw3
log.o: log.cc log.h
	g++ -c log.cc
hw1.o: hw1.cc
	g++ -c hw1.cc
clean: 
	rm *.o
	rm -f log