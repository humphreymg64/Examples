COMMENT#
************************************************************************************
Name:		Matthew Humphrey
Program:  	Proj5.asm
Class:		CSCI 2160-001
Date:		12/1/2015
Purpose:	To test the bookorder class.

************************************************************************************  
#
	.486
	.model flat
	.stack 100h
	
BookOrder struct
	author dword ?
	strTitle dword ?
	quantity dword ?
	costPerBook dword ?
	weight dword ?
BookOrder ends
	
	ExitProcess PROTO Near32 stdcall, dwExitCode:dword  ;capitalization not necessary
	ascint32	PROTO Near32 stdcall, lpStringHolding:dword  ;returns int in EAX
	intasc32	PROTO Near32 stdcall, lpStringToHold:dword, dVal:dword
	intasc32Comma PROTO Near32 stdcall, lpStringToHold:dword, dVal:dword	
	getstring	PROTO Near32 stdcall, lpStringToHold:dword, dNumChars:dword
	putstring	PROTO Near32 stdcall, lpStringToDisplay:dword
	getche		PROTO Near32 stdcall
	getch		PROTO Near32 stdcall
	putch		PROTO Near32 stdcall, bVal:byte	
	memoryallocBailey PROTO stdcall, dSize:dword
	BookOrder_1 PROTO Near32 stdcall
	BookOrder_2 PROTO Near32 stdcall, author:dword, strTitle:dword, quantity:dword
	BookOrder_3 PROTO Near32 stdcall, b:dword
	setAuthor 	PROTO Near32 stdcall, ths:dword, author:dword
	setTitle 	PROTO Near32 stdcall, ths:dword, strTitle:dword
	setQuantity PROTO Near32 stdcall, ths:dword, quantity:dword
	setCostPerBook PROTO Near32 stdcall, ths:dword, cost:dword
	setWeight 	PROTO Near32 stdcall, ths:dword, weight:dword
	getAuthor 	PROTO Near32 stdcall, ths:dword
	getTitle 	PROTO Near32 stdcall, ths:dword
	getQuantity PROTO Near32 stdcall, ths:dword
	getCostPerBook PROTO Near32 stdcall, ths:dword
	getWeight 	PROTO Near32 stdcall, ths:dword
	adjustQuantity PROTO Near32 stdcall, ths:dword, amt:dword
	totalWeight PROTO Near32 stdcall, ths:dword
	calcCost 	PROTO Near32 stdcall, ths:dword
	shippingCost PROTO Near32 stdcall, ths:dword, amt:dword
	equals 		PROTO Near32 stdcall, ths:dword, b:dword
	
	.data
strError	byte	10,13,"Error something went wrong",0
strEqual	byte	10,13,"They are equal.",0
strNotEqual	byte	10,13,"They are not equal.",0
strPrompt1	byte	10,13,"Type desired code (N,A,T,Q,W,P,O,J,C,=,X): ",0
strPrompt2	byte	10,13,"How much do you want to adjust by? ",0
charEntered	byte	?
strTemp		byte	100 dup(?)
strNewLine	byte	10,13,0
strHeader1	byte	10,13,9,"Name: ",9,"Matthew Humphrey",0
strHeader2	byte	10,13,9,"Class: ",9,"CSCI 2160-001",0
strHeader3	byte	10,13,9,"Date: ",9,"12/4/2015",0
strHeader4	byte	10,13,9,"Project: ",9,"5",0
strEnterAut	byte	10,13,9,"Enter an author: ",0
strEnterTi	byte	10,13,9,"Enter a title: ",0
strEnterWei	byte	10,13,9,"Enter a weight: ",0
strEnterCos	byte	10,13,9,"Enter a cost per book: ",0
strEnterQun	byte	10,13,9,"Enter a quantity: ",0
addrBO1		dword	?
addrBO2		dword	?

	.code
_start:
	MOV	ax,0								;instruction used to stop the debugger
	
	ASSUME eax: sdword						;so that eax will work as an sdword
	ASSUME ebx: ptr bookorder				;so that ebx will work as a bookorder
	INVOKE putstring, ADDR strHeader1		;tell the name of the creator of the program
	INVOKE putstring, ADDR strHeader2		;tell the class name
	INVOKE putstring, ADDR strHeader3		;tell the date the project was created
	INVOKE putstring, ADDR strHeader4		;tell the project number
	
	.WHILE AL != 'X' && AL != 'x'			;so that the menu will come back up
	INVOKE putstring, ADDR strPrompt1		;ask for input
	INVOKE getche 							;get a character from the keyboard
	
	.IF AL == 'N' || AL == 'n'				;if the user entered an N
	INVOKE BookOrder_1						;gets room for a bookorder
	MOV addrBO1,EAX							;addrBO1 = the address of a bookorder
	INVOKE putstring, ADDR strEnterAut		;asks the user to enter an author
	INVOKE getstring, ADDR strTemp,99		;get a character from the keyboard
	INVOKE setAuthor, addrBO1,ADDR strTemp	;set the author
	INVOKE putstring, ADDR strNewLine		;jump down a line
	INVOKE putstring, ADDR strEnterTi		;asks the user to enter a title
	INVOKE getstring, ADDR strTemp,99		;get a character from the keyboard
	INVOKE setTitle, addrBO1,ADDR strTemp	;set the title
	INVOKE putstring, ADDR strNewLine		;jump down a line
	INVOKE putstring, ADDR strEnterQun		;asks the user to enter a quantity
	INVOKE getstring, ADDR strTemp,15		;get a character from the keyboard
	INVOKE ascint32, ADDR strTemp			;convert strTemp to a dword
	INVOKE setQuantity, addrBO1,eax			;set the quantity
	INVOKE putstring, ADDR strNewLine		;jump down a line
	INVOKE putstring, ADDR strEnterCos		;asks the user to enter a cost
	INVOKE getstring, ADDR strTemp,15		;get a character from the keyboard
	INVOKE ascint32, ADDR strTemp			;convert strTemp to a dword
	INVOKE setCostPerBook,addrBO1,eax		;set the cost
	INVOKE putstring, ADDR strNewLine		;jump down a line
	INVOKE putstring, ADDR strEnterWei		;asks the user to enter a weight
	INVOKE getstring, ADDR strTemp,15		;get a character from the keyboard
	INVOKE ascint32, ADDR strTemp			;convert strTemp to a dword
	INVOKE setWeight, addrBO1,eax			;set the weight
	INVOKE putstring, ADDR strNewLine		;jump down a line
	
	.ELSEIF AL == 'A' || AL == 'a'			;if the user entered A
	MOV ebx,addrBO1							;ebx = addrBO1
	INVOKE putstring, [ebx].author			;display the author
	INVOKE putstring, ADDR strNewLine		;jump down a line
	
	.ELSEIF AL == 'T'||AL == 't'			;if the user entered a T
	MOV ebx,addrBO1							;ebx = addrBO1
	INVOKE putstring, [ebx].strTitle		;display the title
	INVOKE putstring, ADDR strNewLine		;jump down a line
	
	.ELSEIF AL == 'Q'||AL == 'q'			;if the user entered a Q
	MOV ebx,addrBO1							;ebx = addrBO1
	MOV eax,[EBX].quantity					;eax = quantity
	INVOKE intasc32,ADDR strTemp,eax		;strTemp = eax
	INVOKE putstring, ADDR strTemp			;display the quantity
	INVOKE putstring, ADDR strNewLine		;jump down a line

	.ELSEIF AL == 'W'||AL == 'w'			;if the user entered a W
	MOV ebx,addrBO1							;ebx = addrBO1
	MOV eax,[EBX].weight					;eax = weight
	INVOKE intasc32,ADDR strTemp,eax		;strTemp = eax
	INVOKE putstring, ADDR strTemp			;display the weight
	INVOKE putstring, ADDR strNewLine		;jump down a line
	
	.ELSEIF AL == 'P'||AL == 'p'			;if the user entered a P
	MOV ebx,addrBO1							;ebx = addrBO1
	MOV eax,[EBX].costPerBook				;eax = costPerBook
	INVOKE intasc32,ADDR strTemp,eax		;strTemp = eax
	INVOKE putstring, ADDR strTemp			;display the costPerBook
	INVOKE putstring, ADDR strNewLine		;jump down a line
	
	.ELSEIF AL == 'O'||AL == 'o'			;if the user entered an O
	MOV ebx,addrBO1							;ebx = addrBO1
	MOV eax,[EBX].quantity					;eax = quantity
	INVOKE intasc32,ADDR strTemp,eax		;strTemp = eax
	INVOKE putstring, ADDR strTemp			;display the quantity
	INVOKE putstring, ADDR strNewLine		;jump down a line
	MOV ebx,addrBO1							;ebx = addrBO1
	MOV eax,[EBX].weight					;eax = weight
	INVOKE intasc32,ADDR strTemp,eax		;strTemp = eax
	INVOKE putstring, ADDR strTemp			;display the weight
	INVOKE putstring, ADDR strNewLine		;jump down a line
	MOV ebx,addrBO1							;ebx = addrBO1
	INVOKE totalWeight, addrBO1				;eax = total Weight
	INVOKE intasc32,ADDR strTemp,eax		;strTemp = eax
	INVOKE putstring,ADDR  strTemp			;display the total weight
	INVOKE putstring, ADDR strNewLine		;jump down a line

	.ELSEIF AL == 'C'||AL == 'c'			;if the user entered an O
	MOV ebx,addrBO1							;ebx = addrBO1
	MOV eax,[EBX].quantity					;eax = quantity
	INVOKE intasc32,ADDR strTemp,eax		;strTemp = eax
	INVOKE putstring, ADDR strTemp			;display the quantity
	INVOKE putstring, ADDR strNewLine		;jump down a line
	MOV ebx,addrBO1							;ebx = addrBO1
	MOV eax,[EBX].costPerBook				;eax = costPerBook
	INVOKE intasc32,ADDR strTemp,eax		;strTemp = eax
	INVOKE putstring, ADDR strTemp			;display the costPerBook
	INVOKE putstring, ADDR strNewLine		;jump down a line
	MOV ebx,addrBO1							;ebx = addrBO1
	INVOKE calcCost, addrBO1				;eax = total cost
	INVOKE intasc32,ADDR strTemp,eax		;strTemp = eax
	INVOKE putstring, ADDR strTemp			;display the total weight
	INVOKE putstring, ADDR strNewLine		;jump down a line	
	
	.ELSEIF AL == 'J' || AL == 'j'			;if the user entered an N
	INVOKE putstring, ADDR strPrompt2		;asks the user to enter an adjustment
	INVOKE getstring, ADDR strTemp,99		;get a character from the keyboard
	INVOKE ascint32, ADDR strTemp			;convert strTemp to a dword
	INVOKE adjustQuantity, addrBO1, eax		;adjust the quantity
	.IF eax == -1							;if it did not work
	INVOKE putstring, ADDR strError			;tell the user it did not work
	.ENDIF

	.ELSEIF AL == '=' 						;if the user entered an N
	MOV EBX,addrBO1							;ebx = addrBO1
	MOV addrBO2,EBX							;addrBO2 = addrBO1
	INVOKE equals,EBX,addrBO2				;check if addrBO1 = addrBO2
	
	.IF EAX == 1							;if it is true
	INVOKE putstring,ADDR strEqual			;tell the user they are equal
	.ELSE
	INVOKE putstring,ADDR strNotEqual								;tell the user they are not equal
	.ENDIF
	
	INVOKE BookOrder_2,[EBX].author,[EBX].strTitle,[EBX].quantity	;eax = addrBO1
	MOV addrBO2,EAX													;addrBO2 = addrBO1
	INVOKE equals,EBX,addrBO2										;check if addrBO1 = addrBO2
	
	.IF EAX == 1													;if it is true
	INVOKE putstring,ADDR strEqual			;tell the user they are equal
	.ELSE
	INVOKE putstring,ADDR strNotEqual		;tell the user they are not equal
	.ENDIF
	
	INVOKE BookOrder_3,EBX					;eax = addrBO1
	MOV addrBO2,EAX							;addrBO2 = addrBO1
	INVOKE equals,EBX,addrBO2				;check if addrBO1 = addrBO2
	
	.IF EAX == 1							;if it is true
	INVOKE putstring,ADDR strEqual			;tell the user they are equal
	.ELSE
	INVOKE putstring,ADDR strNotEqual		;tell the user they are not equal
	.ENDIF

	.ENDIF
	.ENDW
	
	INVOKE ExitProcess,0
	PUBLIC _start	
	END