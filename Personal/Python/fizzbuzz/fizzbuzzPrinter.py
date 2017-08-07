for i in range(1,100):
    fizz = False;
    buzz = False;
    if i % 3 == 0:
        fizz = True;
    if i % 5 == 0:
        buzz = True;
    if fizz:
        if buzz:
            print("fizzbuzz");
        else:
            print("fizz");
    elif fizz:
      print("fizz");
    else:
        print(i);