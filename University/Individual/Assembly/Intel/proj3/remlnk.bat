del %1.exe
del %1.ilk
del %1.lst
del %1.obj
del %1.pdb
C:\Users\Matthew\Desktop\Assembly\MASM2015Fall\ml /c /coff /Zi /Fl %1.asm
C:\Users\Matthew\Desktop\Assembly\MASM2015Fall\link /debug /subsystem:console /entry:start /out:%1.exe %1.obj C:\Users\Matthew\Desktop\Assembly\MASM2015Fall\kernel32.lib C:\Users\Matthew\Desktop\Assembly\MASM2015Fall\utility201503.obj C:\Users\Matthew\Desktop\Assembly\MASM2015Fall\convutil201510.obj